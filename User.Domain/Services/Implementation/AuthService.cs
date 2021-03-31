using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Data.DTO;
using User.Data.Models;
using User.Domain.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace User.Domain.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public AuthService(IUserService userService, IOptions<AuthOptions> authOptions, IMapper mapper, IPasswordService passwordService)
        {
            _userService = userService;
            _authOptions = authOptions;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<PersonDTO> AuthenticateUser(string email, string password)
        {
            var users = await _userService.GetAllAsync();
            var currentUser = users.FirstOrDefault(u => u.Email == email);
            
            var userPassword = await _passwordService.GetPasswordByUserId((int)currentUser.Id);
            var passwordForCheck = _passwordService.HashPasswordWithSalt(userPassword.Salt, password);
            if (userPassword.Password == passwordForCheck.Password)
            {
                currentUser.Token = GenerateJWT(currentUser);
                return currentUser;
            }

            return null;
        }

        public string GenerateJWT(PersonDTO user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer, authParams.Audience, claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
