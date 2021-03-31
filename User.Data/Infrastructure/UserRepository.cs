using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Data.Interfaces;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class UserRepository : Repository<Person>
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        protected override IQueryable<Person> IncludedEntities =>base.IncludedEntities
            .Include(u => u.Address)
            .ThenInclude(u => u.City)
            .Include(u => u.Address)
            .ThenInclude(u => u.Country);
        
    }
}
