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
        Task<PersonDTO> AddUserAsync(PersonDTO person);

        Task<IEnumerable<PersonDTO>> GetAllAsync();

        Task<PersonDTO> GetByIdAsync(int id);

        Task DeleteByIdAsync(int id);

        Task<PersonDTO> UpdateUserAsync(PersonDTO person);
    }
}
