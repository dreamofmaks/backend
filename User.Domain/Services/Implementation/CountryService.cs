using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Data.DTO;
using User.Data.Infrastructure;
using User.Data.Interfaces;
using User.Data.Models;
using User.Domain.Services.Interfaces;

namespace User.Domain.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDTO>> GetCountries()
        {
            var countries = await _unitOfWork.GetCountryRepository().GetAllAsync();
            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }
    }
}
