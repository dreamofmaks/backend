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
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Person>, Repository<Person>>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRepository<DCountry>, Repository<DCountry>>();
        }


        public static void DetachAllEntries(this Context context)
        {
            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                context.Entry(entry.Entity).State = EntityState.Detached;
            }
        }
    }
}
