using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;

namespace User.Domain.Services.Interfaces
{
    public interface IPasswordService
    {
        PasswordDTO HashPassword(string password);

        Task<PasswordDTO> GetPasswordByUserId(int userId);

        PasswordDTO HashPasswordWithSalt(string salt, string password);
    }
}
