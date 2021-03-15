using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Model;
using User.Domain.Services.Interfaces;

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
        public Person AddUser(PersonDTO person)
        {
            var mappedUser = _mapper.Map<Person>(person);

            unitOfWork.GetPersonRepository().Add(mappedUser);
            unitOfWork.SaveChanges();
            return mappedUser;
        }

        public IEnumerable<Person> GetAll()
        {
            return unitOfWork.GetPersonRepository()
                .Query(user => user.Address, c => c.Address.City, c => c.Address.Country);
        }

        public Person GetById(int id)
        {
            return unitOfWork.GetPersonRepository()
                .Query(u => u.Address, c => c.Address.City, c => c.Address.Country)
                .FirstOrDefault(u => u.Id == id);
        }

        public void DeleteById(int id)
        {
            var person = unitOfWork.GetPersonRepository().Query(u => u.Address, u => u.Address.City, u => u.Address.Country).FirstOrDefault(user => user.Id == id);
            unitOfWork.GetPersonRepository().Delete(person);
            unitOfWork.SaveChanges();
        }
    }
}
