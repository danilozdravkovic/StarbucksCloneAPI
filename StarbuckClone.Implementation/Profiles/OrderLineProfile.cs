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
    internal class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<OrderLine, OrderLineDto>()
                .ForMember(x => x.OrderLineId, y => y.MapFrom(p => p.Id))
                .ForMember(x=>x.ProductId,y=>y.MapFrom(p=>p.ProductId))
                .ForMember(x => x.ProductImage, y => y.MapFrom(p => p.Product.ImageSrc))
                .ForMember(x => x.ProductName, y => y.MapFrom(p => p.Product.Name))
                .ForMember(x => x.ProductSize, y => y.MapFrom(p => p.SizeVolume))
                .ForMember(x=>x.ProductSizeId,y=>y.MapFrom(p=>p.ProductSizeId))
                .ForMember(x => x.AddIns, y => y.MapFrom(p => p.OrderLineAddIns.Select(cl => new GetingAddInForCartDto
                {
                    Id= cl.AddInId,
                    AddInName = cl.AddIn,
                    Pump = cl.Pump,
                }).ToList()));



        }
    }
}
