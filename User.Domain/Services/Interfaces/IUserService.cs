using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Model;

namespace User.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public Person AddUser(PersonDTO person);

    }
}
