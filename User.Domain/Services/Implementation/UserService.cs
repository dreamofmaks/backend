using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Models;
using User.Domain.Services.Interfaces;

namespace User.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PersonDTO> AddUserAsync(PersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);

            await _unitOfWork.GetPersonRepository().AddAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            var user = _unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.Country)
                .FirstOrDefault(u => u.Id == mappedUser.Id);
            var dtoMapped = _mapper.Map<PersonDTO>(user);
            return dtoMapped;
        }

        public Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var users = _unitOfWork.GetPersonRepository()
                .Query(user => user.Address, c => c.Address.City, c => c.Address.Country);
            var mappedUsers = _mapper.Map<IEnumerable<PersonDTO>>(users);

            return Task.FromResult(mappedUsers);
        }

        public Task<PersonDTO> GetByIdAsync(int id)
        {
            var user = _unitOfWork.GetPersonRepository()
                .Query(u => u.Address, c => c.Address.City, c => c.Address.Country)
                .FirstOrDefault(u => u.Id == id);
            var mappedDto = _mapper.Map<PersonDTO>(user);
            return Task.FromResult(mappedDto);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var person = _unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.City, u => u.Address.Country).FirstOrDefault(user => user.Id == id);
            await _unitOfWork.GetCityRepository().DeleteById(person.Address.City.Id);
            await _unitOfWork.GetAddressRepository().DeleteById(person.Address.Id);
            await _unitOfWork.GetPersonRepository().DeleteAsync(person);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PersonDTO> UpdateUserAsync(PersonDTO personForUpdate)
        {
            var mappedUser = _mapper.Map<Person>(personForUpdate);
            mappedUser.Address.City = null;
            await _unitOfWork.GetPersonRepository().UpdateAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            var mappedDto = _mapper.Map<PersonDTO>(mappedUser);
            return mappedDto;
        }
    }
}
