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
        public Task<Person> AddUserAsync(PersonDTO person);

        public Task<IEnumerable<Person>> GetAllAsync();

        public Task<Person> GetByIdAsync(int id);

        public Task DeleteByIdAsync(int id);

        public Task<Person> UpdateUserAsync(PersonDTO person);
    }
}
