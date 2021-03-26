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
                .ForPath(dist => dist.City.Id, opt => opt.MapFrom(src => src.City.Id))
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.CountryId, opt => opt.MapFrom(src => src.Country.Id))
                .ForPath(dist => dist.Country, opt => opt.Ignore());
            CreateMap<PersonDTO, Person>()
                .ForPath(dist => dist.Address.CountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForPath(dist => dist.Address.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForPath(dist => dist.Address.CityId, opt => opt.MapFrom(src => src.Address.City.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId));


            CreateMap<Address, AddressDTO>()
                .ForPath(dist => dist.City.Id, opt => opt.MapFrom(src => src.CityId))
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.Country.Name, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<Person, PersonDTO>()
                .ForPath(dist => dist.Address.Country.Id, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId));

            CreateMap<CountryDTO, DCountry>();

            CreateMap<DCountry, CountryDTO>();
        }
    }
}
