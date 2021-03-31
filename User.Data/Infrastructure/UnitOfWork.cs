using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using User.Data.Interfaces;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        

        public UnitOfWork(Context context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            PasswordRepository = new PasswordRepository(_context);
        }

        public IRepository<Person> UserRepository { get; }

        public IRepository<UserPassword> GetPasswordRepository()
        {
            return new PasswordRepository(_context);
        }

        public IRepository<UserPassword> PasswordRepository { get; }

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
    }
}
