using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Interfaces;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRepository<Address> GetAddressRepository();
        IRepository<City> GetCityRepository();
        IRepository<DCountry> GetCountryRepository();

        Task SaveChangesAsync();
    }
}
