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
        IRepository<Person> GetPersonRepository();
        IRepository<Address> GetAddressRepository();
        IRepository<City> GetCityRepository();
        IRepository<Country> GetCountryRepository();

        void SaveChanges();
    }
}
