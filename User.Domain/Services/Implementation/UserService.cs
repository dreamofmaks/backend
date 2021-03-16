using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Model;
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
        public async Task<Person> AddUserAsync(PersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);

            await unitOfWork.GetPersonRepository().AddAsync(mappedUser);
            await unitOfWork.SaveChangesAsync();
            return mappedUser;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await unitOfWork.GetPersonRepository()
                .Query(user => user.Address, c => c.Address.City, c => c.Address.Country).ToListAsync();
        }

        public Task<Person> GetByIdAsync(int id)
        {
            return unitOfWork.GetPersonRepository()
                .Query(u => u.Address, c => c.Address.City, c => c.Address.Country)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var person = await unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.City, u => u.Address.Country).FirstOrDefaultAsync(user => user.Id == id);
            await unitOfWork.GetPersonRepository().DeleteAsync(person);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<Person> UpdateUserAsync(PersonDTO personForUpdate)
        {
            var user = await unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.City, u => u.Address.Country).FirstOrDefaultAsync(user => user.Id == personForUpdate.Id);
            if(user != null)
            {
                user = _mapper.Map<Person>(personForUpdate);
            }
            return user;
        }
    }
}
