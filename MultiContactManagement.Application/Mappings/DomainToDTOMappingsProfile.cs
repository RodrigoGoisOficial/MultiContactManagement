using AutoMapper;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Domain.Entities;

namespace MultiContactManagement.Application.Mappings
{
    public class DomainToDTOMappingsProfile : Profile
    {
        public DomainToDTOMappingsProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}
