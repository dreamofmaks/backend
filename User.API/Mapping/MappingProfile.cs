using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User.Data.DTO;
using User.Data.Models;

namespace User.API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressDTO, Address>()
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.Countryid, opt => opt.MapFrom(src => src.Country.Id))
                .ForPath(dist => dist.Country, opt => opt.Ignore());
            CreateMap<PersonDTO, Person>()
                .ForPath(dist => dist.Address.Countryid, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.Addressid, opt => opt.MapFrom(src => src.Addressid));


            CreateMap<Address, AddressDTO>()
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.Country.Name, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<Person, PersonDTO>()
                .ForPath(dist => dist.Address.Country.Id, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.Addressid, opt => opt.MapFrom(src => src.Addressid));

            CreateMap<CountryDTO, DCountry>()
                .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<DCountry, CountryDTO>();
        }
    }
}
