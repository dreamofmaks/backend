using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using User.Data.Interfaces;
using User.Data.Model;

namespace User.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        

        public UnitOfWork(Context context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
        }

        public IRepository<Person> UserRepository { get; }

        public IRepository<Address> GetAddressRepository()
        {
            return new Repository<Address>(_context);
        }

        public IRepository<City> GetCityRepository()
        {
            return new Repository<City>(_context);
        }

        public IRepository<DCountry> GetCountryRepository()
        {
            return new Repository<DCountry>(_context);
        }

        public async Task SaveChangesAsync()
        { 
            await _context.SaveChangesAsync();
        }

        public IRepository<Password> GetPasswordRepository()
        {
            return new Repository<Password>(_context);
        }
    }
}
