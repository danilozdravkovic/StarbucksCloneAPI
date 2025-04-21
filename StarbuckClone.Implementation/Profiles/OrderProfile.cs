using AutoMapper;
using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.OrderId, y => y.MapFrom(p => p.Id))
                .ForMember(x => x.UserName, y => y.MapFrom(p => p.User.FirstName+" "+p.User.LastName))
                .ForMember(x => x.UserEmail, y => y.MapFrom(p => p.User.Email))
                .ForMember(x => x.PaymentOption,y => y.MapFrom(p=> p.PaymentOption))
                .ForMember(x => x.Products, y => y.MapFrom(p => p.OrderLines.Select(o => new OrderProductDto
                {
                    Name = o.Product.Name,
                    AddIns = o.OrderLineAddIns.Select(ola => new GetingAddInForCartDto
                    {
                        AddInName = ola.AddIn,
                        Pump = ola.Pump
                    }).ToList()
                }).ToList()));
        }
    }
}





