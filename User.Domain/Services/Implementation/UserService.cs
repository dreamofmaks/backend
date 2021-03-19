using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Models;
using User.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace User.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PersonDTO> AddUserAsync(PersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);

            await unitOfWork.GetPersonRepository().AddAsync(mappedUser);
            await unitOfWork.SaveChangesAsync();
            var user = await unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.Country)
                .FirstOrDefaultAsync(user => user.Id == mappedUser.Id);
            var dtoMapped = _mapper.Map<PersonDTO>(user);
            return dtoMapped;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var users =  await unitOfWork.GetPersonRepository()
                .Query(user => user.Address, c => c.Address.City, c => c.Address.Country).ToListAsync();
            var mappedUsers = _mapper.Map<IEnumerable<PersonDTO>>(users);

            return mappedUsers;
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var user =await unitOfWork.GetPersonRepository()
                .Query(u => u.Address, c => c.Address.City, c => c.Address.Country)
                .FirstOrDefaultAsync(u => u.Id == id);
            var mappedDto = _mapper.Map<PersonDTO>(user);
            return mappedDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var person = await unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.City, u => u.Address.Country).FirstOrDefaultAsync(user => user.Id == id);
            await unitOfWork.GetCityRepository().DeleteById(person.Address.City.Id);
            await unitOfWork.GetAddressRepository().DeleteById(person.Address.Id);
            await unitOfWork.GetPersonRepository().DeleteAsync(person);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<PersonDTO> UpdateUserAsync(PersonDTO personForUpdate)
        {
            var mappedUser = _mapper.Map<Person>(personForUpdate);
            await unitOfWork.GetPersonRepository().UpdateAsync(mappedUser);
            await unitOfWork.SaveChangesAsync();
            var mappedDTO = _mapper.Map<PersonDTO>(mappedUser);
            return mappedDTO;
        }
    }
}
