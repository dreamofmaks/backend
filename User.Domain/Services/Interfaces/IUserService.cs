using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Models;

namespace User.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public Task<PersonDTO> AddUserAsync(PersonDTO person);

        public Task<IEnumerable<PersonDTO>> GetAllAsync();

        public Task<PersonDTO> GetByIdAsync(int id);

        public Task DeleteByIdAsync(int id);

        public Task<PersonDTO> UpdateUserAsync(PersonDTO person);
    }
}
