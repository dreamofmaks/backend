using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Data.DTO;
using User.Data.Model;

namespace User.API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //CreateMap<CityDTO, City>()
            //    .ForMember(dist=> dist.Name, opt => opt.MapFrom(src => src.Name));
            //CreateMap<CountryDTO, CountryDTO>()
            //    .ForMember(dist=> dist.Name, opt=>opt.MapFrom(src=> src.Name));
            CreateMap<AddressDTO, Address>()
                .ForPath(dist => dist.City.Name, opt=> opt.MapFrom(src=> src.City))
                .ForPath(dist => dist.Country.Name, opt => opt.MapFrom(src => src.Country));
            CreateMap<PersonDTO, Person>()
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src=> src.Address));
        }
    }
}
