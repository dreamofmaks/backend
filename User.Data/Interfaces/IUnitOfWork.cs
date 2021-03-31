using System.Threading.Tasks;
using User.Data.Interfaces;
using User.Data.Models;

namespace User.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Person> UserRepository { get; }
        IRepository<Address> GetAddressRepository();
        IRepository<City> GetCityRepository();
        IRepository<DCountry> GetCountryRepository();

        IRepository<UserPassword> GetPasswordRepository();

        IRepository<UserPassword> PasswordRepository { get; }

        Task SaveChangesAsync();
    }
}
