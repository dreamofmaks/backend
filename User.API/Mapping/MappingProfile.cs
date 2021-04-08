using AutoMapper;
using User.Data.DTO;
using User.Data.Models;

namespace User.API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<PasswordDTO, UserPassword>().ReverseMap();

            CreateMap<AddressDTO, Address>()
                .ForPath(dist => dist.CountryId, opt => opt.MapFrom(src => src.Country.Id))
                .ForPath(dist => dist.Country, opt => opt.Ignore());
            CreateMap<RegistrationPersonDTO, Person>()
                .ForPath(dist => dist.Address.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForPath(dist => dist.Address.CityId, opt => opt.MapFrom(src => src.Address.City.Id));


            CreateMap<Address, AddressDTO>()
                .ForPath(dist => dist.City.Id, opt => opt.MapFrom(src => src.CityId))
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name));

            CreateMap<Person, RegistrationPersonDTO>()
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.UserPasswords, opt => opt.Ignore());

            CreateMap<Person, PersonDTO>()
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<PersonDTO, Person>()
                .ForPath(dist => dist.Address.CountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForPath(dist => dist.Address.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForPath(dist => dist.Address.CityId, opt => opt.MapFrom(src => src.Address.City.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.UserPasswords, opt => opt.Ignore());

            CreateMap<RegistrationPersonDTO, PersonDTO>();

            CreateMap<CountryDTO, DCountry>();

            CreateMap<DCountry, CountryDTO>();

            CreateMap<PasswordDTO, UserPassword>();
        }
    }
}
