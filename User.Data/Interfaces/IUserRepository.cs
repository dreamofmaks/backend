using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Data.Models;

namespace User.Data.Interfaces
{
    public interface IUserRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllUsersWithRelatedData();

        Task<Person> GetUserByIdWithRelatedData(int id); 
    }
}
