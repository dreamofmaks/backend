using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using User.Data.Model;

namespace User.Domain.Services.Implementation
{
    public class PasswordService : IPasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PasswordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PasswordDTO> GetPasswordByUserId(int userId)
        {
            var passwords = await _unitOfWork.GetPasswordRepository().GetAllAsync();
            return _mapper.Map<PasswordDTO>(passwords.FirstOrDefault(u => u.UserId == userId));
        }

        public PasswordDTO HashPassword(PasswordDTO password)
        {
            byte[] salt = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] hashedPass = KeyDerivation.Pbkdf2(password.Password1, salt, KeyDerivationPrf.HMACSHA256, 1000, 32);
            var passwordDTO = new PasswordDTO
            {
                Password1 = Convert.ToBase64String(hashedPass),
                Salt = Convert.ToBase64String(salt)
            };

            return passwordDTO;
        }

        public PasswordDTO HashPassword(string salt, string password)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(Convert.FromBase64String(salt));
            }

            byte[] hashedPass = KeyDerivation.Pbkdf2(password, Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA256, 1000, 32);
            return new PasswordDTO
            {
                Password1 = Convert.ToBase64String(hashedPass)
            };
        }
    }
}
