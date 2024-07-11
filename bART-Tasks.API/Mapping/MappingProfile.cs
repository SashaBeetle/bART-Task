using AutoMapper;
using bART_Task.Core.Entities;
using bART_Tasks.API.Mapping.DTOs;
using bART_Tasks.API.Mapping.Models;

namespace bART_Tasks.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Incident, IncidentDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>();
            CreateMap<Account, AccountRequest>().ReverseMap();

        }
    }
}
