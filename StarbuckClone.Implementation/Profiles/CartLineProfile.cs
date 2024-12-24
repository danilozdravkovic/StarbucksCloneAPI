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
    internal class CartLineProfile : Profile
    {
        public CartLineProfile()
        {
            CreateMap<CartLine, CartLineDto>()
                .ForMember(x => x.CartLineId, y => y.MapFrom(p=>p.Id))
                .ForMember(x => x.ProductImage, y => y.MapFrom(p=>p.Product.ImageSrc))
                .ForMember(x => x.ProductName, y => y.MapFrom(p=>p.Product.Name))
                .ForMember(x => x.ProductSize, y => y.MapFrom(p=>p.SizeVolume))
                .ForMember(x => x.isFavourite, y => y.MapFrom(p=>p.IsFavourite))
                .ForMember(x => x.ProductPrice, y => y.MapFrom(p => p.Product.InitialPrice + p.CartLinesAddIns.Sum(cl => cl.AddInPrice)))
                .ForMember(x=>x.AddIns,y=>y.MapFrom(p=> p.CartLinesAddIns.Select(cl => new GetingAddInForCartDto
                {
                    AddInName = cl.AddIn.Name,
                    Pump = cl.Pump,
                }).ToList()));


      
        }
    }
}
