﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;

namespace User.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegistrationPersonDTO> AuthenticateUser(string email, string password);
        string GenerateJWT(RegistrationPersonDTO user);
    }
}
