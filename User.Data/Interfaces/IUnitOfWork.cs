using System.Threading.Tasks;
using User.Data.Interfaces;
using User.Data.Model;

namespace User.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Person> UserRepository { get; }
        IRepository<Address> GetAddressRepository();
        IRepository<City> GetCityRepository();
        IRepository<DCountry> GetCountryRepository();

        IRepository<Password> GetPasswordRepository();

        IRepository<Password> PasswordRepository { get; }

        Task SaveChangesAsync();
    }
}
