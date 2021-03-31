using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using User.Data.Infrastructure;
using User.Data.Interfaces;
using User.Data.Models;
using User.Domain.Services.Interfaces;
using User.Domain.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace User.API.ServiceExtension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<DCountry>, Repository<DCountry>>();
            services.AddScoped<IRepository<Person>, UserRepository>();
            services.AddScoped<IRepository<UserPassword>, PasswordRepository>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordService, PasswordService>();
        }
    }
}
