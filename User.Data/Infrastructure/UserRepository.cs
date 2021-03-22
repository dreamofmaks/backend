using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Data.Interfaces;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class UserRepository : Repository<Person>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllUsersWithRelatedData()
        {
            return await _context.People
                .Include(u => u.Address)
                .ThenInclude(u => u.City)
                .Include(u => u.Address)
                .ThenInclude(u => u.Country).ToListAsync();
        }

        public async Task<Person> GetUserByIdWithRelatedData(int id)
        {
            return await _context.People
                .Include(u => u.Address)
                .ThenInclude(u => u.City)
                .Include(u => u.Address)
                .ThenInclude(u => u.Country).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
