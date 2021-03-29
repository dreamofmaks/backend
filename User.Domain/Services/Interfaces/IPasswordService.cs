using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;

namespace User.Domain.Services.Interfaces
{
    public interface IPasswordService
    {
        //Task<PasswordDTO> AddPassword(PasswordDTO password);

        PasswordDTO HashPassword(PasswordDTO password);

        Task<PasswordDTO> GetPasswordByUserId(int userId);

        public PasswordDTO HashPassword(string salt, string password);
    }
}
