using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    }
}
