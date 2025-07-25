using Application.Dtos;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();

            CreateMap<Policy, PolicyResponseDto>();

            CreateMap<PolicyCreateDto, Policy>();
            CreateMap<PolicyUpdateDto, Policy>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
