using System;
using System.Collections;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Interfaces;
using User.Data.Models;
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

        public async Task<PersonDTO> SignUpUserAsync(RegistrationPersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);
            var password = _mapper.Map<UserPassword>(_passwordService.HashPassword(person.Password.Password));
            mappedUser.UserPasswords.Add(password);
            var personRepository =  _unitOfWork.UserRepository;
            await personRepository.AddAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            var user = await personRepository
                .GetByIdAsync(mappedUser.Id);
            var dtoMapped = _mapper.Map<PersonDTO>(user);

            return dtoMapped;
        }

        public async Task<PersonDTO> AddUserAsync(PersonDTO user)
        {
            var mappedUser = _mapper.Map<Person>(user);
            await _unitOfWork.UserRepository.AddAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            return user;
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
            var password = _mapper.Map<UserPassword>(passwordDto);
            if (password != null)
            {
                await _unitOfWork.PasswordRepository.DeleteAsync(password);
            }
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

        public async Task<IEnumerable<PersonDTO>> GetLimitedUsers(int skip, int take)
        {
            var personRepository = _unitOfWork.UserRepository;
            var users = await personRepository.GetLimited(skip, take);
            return _mapper.Map<IEnumerable<PersonDTO>>(users);
        }

        public async Task<int> GetCountOfUsers()
        {
            return await _unitOfWork.UserRepository.GetCountOfEntities();
        }

        public async Task<IEnumerable<PersonDTO>> GetSortedUsers(string sortBy, int skip, int take, string order)
        {
            var sortingProperty = sortBy;
            var propertyDoesNotExist = typeof(Person).GetProperty(sortingProperty, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase) is null;
            if (propertyDoesNotExist)
            {
                throw new Exception("property does not exist on type");
            }

            var sortedUsers = await _unitOfWork.UserRepository.GetSorted(sortBy, skip, take, order);
            var users = _mapper.Map<IEnumerable<PersonDTO>>(sortedUsers);
            return users;
        }
    }
}