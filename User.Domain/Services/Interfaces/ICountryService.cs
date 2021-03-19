using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Models;

namespace User.Domain.Services.Interfaces
{
    public interface ICountryService
    {
        public Task<IEnumerable<CountryDTO>> GetCountries();
    }
}
