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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, y => y.MapFrom(p => p.Category.Name))
                .ForMember(x => x.Price, y => y.MapFrom(p => p.InitialPrice));
        }
    }
}
