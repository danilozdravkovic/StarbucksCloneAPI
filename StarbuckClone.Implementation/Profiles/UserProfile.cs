using AutoMapper;
using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Role, y => y.MapFrom(p => p.Role == null ? "No role" : p.Role.Name))
                .ForMember(x => x.IsActive, y => y.MapFrom(p => p.IsActive.ToString()))
                .ForMember(x => x.UseCases, y => y.MapFrom(p => p.UseCases.Select(y => y.UseCaseId)));
        }
    }
}
