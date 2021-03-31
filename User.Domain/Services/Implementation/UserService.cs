using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Interfaces;
using User.Data.Model;
using User.Domain.Services.Interfaces;

namespace User.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<PersonDTO> AddUserAsync(PersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);
            var password = _mapper.Map<Password>(_passwordService.HashPassword(person.Password.Password1));
            mappedUser.Passwords.Add(password);
            var personRepository =  _unitOfWork.UserRepository;
            await personRepository.AddAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            var user = await personRepository
                .GetByIdAsync(mappedUser.Id);
            var dtoMapped = _mapper.Map<PersonDTO>(user);
            return dtoMapped;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var personRepository = _unitOfWork.UserRepository;
            var users = await personRepository
                .GetAllAsync();
            var mappedUsers = _mapper.Map<IEnumerable<PersonDTO>>(users);
            return mappedUsers;
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository
                .GetByIdAsync(id);
            var mappedDto = _mapper.Map<PersonDTO>(user);
            return (mappedDto);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var passwordDto = await _passwordService.GetPasswordByUserId(id);
            var password = _mapper.Map<Password>(passwordDto);
            await _unitOfWork.PasswordRepository.DeleteAsync(password);
            var personRepository = _unitOfWork.UserRepository;
            var person = await personRepository.GetByIdAsync(id);
            await _unitOfWork.GetCityRepository().DeleteByIdAsync(person.Address.City.Id);
            await _unitOfWork.GetAddressRepository().DeleteByIdAsync(person.Address.Id);
            await personRepository.DeleteAsync(person);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PersonDTO> UpdateUserAsync(PersonDTO personForUpdate)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithoutTrackingAsync((int) personForUpdate.Id);
            var mappedUser = _mapper.Map<Person>(personForUpdate);
            mappedUser.Email = user.Email;
            await _unitOfWork.UserRepository.UpdateAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            var mappedDto = _mapper.Map<PersonDTO>(mappedUser);
            return mappedDto;
        }
    }
}
