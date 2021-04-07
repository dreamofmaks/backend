using AutoMapper;
using User.Data.DTO;
using User.Data.Models;

namespace User.API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<PasswordDTO, UserPassword>().ReverseMap();

            CreateMap<AddressDTO, Address>()
                .ForPath(dist => dist.City.Id, opt => opt.MapFrom(src => src.City.Id))
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.CountryId, opt => opt.MapFrom(src => src.Country.Id))
                .ForPath(dist => dist.Country, opt => opt.Ignore());
            CreateMap<RegistrationPersonDTO, Person>()
                .ForPath(dist => dist.Address.CountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForPath(dist => dist.Address.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForPath(dist => dist.Address.CityId, opt => opt.MapFrom(src => src.Address.City.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId));


            CreateMap<Address, AddressDTO>()
                .ForPath(dist => dist.City.Id, opt => opt.MapFrom(src => src.CityId))
                .ForPath(dist => dist.City.Name, opt => opt.MapFrom(src => src.City.Name))
                .ForPath(dist => dist.Country.Name, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<Person, RegistrationPersonDTO>()
                .ForPath(dist => dist.Address.Country.Id, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dist => dist.UserPasswords, opt => opt.Ignore());

            CreateMap<Person, PersonDTO>()
                .ForPath(dist => dist.Address.Country.Id, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId));

            CreateMap<PersonDTO, Person>()
                .ForPath(dist => dist.Address.CountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForPath(dist => dist.Address.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForPath(dist => dist.Address.CityId, opt => opt.MapFrom(src => src.Address.City.Id))
                .ForMember(dist => dist.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dist => dist.AddressId, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dist => dist.UserPasswords, opt => opt.Ignore());

            CreateMap<CountryDTO, DCountry>();

            CreateMap<DCountry, CountryDTO>();

            CreateMap<PasswordDTO, UserPassword>()
                .ForPath(dist => dist.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
