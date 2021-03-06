using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Data.DTO;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class PasswordRepository : Repository<UserPassword>
    {
        private readonly Context _context;
         private readonly IMapper _mapper;
        public PasswordRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<PasswordDTO> GetPasswordByUserId(int id)
        {
            var pass = await _context.UserPasswords.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
            var mapped = _mapper.Map<PasswordDTO>(pass);
            return mapped;
        }
    }
}
