using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Interfaces;
using User.Data.Model;

namespace User.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Person> UserRepository { get; }
        IRepository<Address> GetAddressRepository();
        IRepository<City> GetCityRepository();
        IRepository<DCountry> GetCountryRepository();

        IRepository<Password> GetPasswordRepository();

        Task SaveChangesAsync();
    }
}
