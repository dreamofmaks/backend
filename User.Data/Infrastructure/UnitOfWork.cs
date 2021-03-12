using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Interfaces;
using User.Data.Model;

namespace User.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext _context;

        public UnitOfWork(UserContext context)
        {
            _context = context;
        }
        public IRepository<Person> GetPersonRepository()
        {
            return new Repository<Person>(_context);
        }

        public IRepository<Address> GetAddressRepository()
        {
            return new Repository<Address>(_context);
        }

        public IRepository<City> GetCityRepository()
        {
            return new Repository<City>(_context);
        }

        public IRepository<Country> GetCountryRepository()
        {
            return new Repository<Country>(_context);
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
