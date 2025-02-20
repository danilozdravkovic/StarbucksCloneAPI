using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.OrderLines;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.OrderLines
{
    public class EFGetFavouriteProductsQuery : IGetFavouriteProductsQuery
    {
        public readonly IApplicationActor _actor;
        public readonly SCContext _context;
        public readonly IMapper _mapper;


        public EFGetFavouriteProductsQuery(IApplicationActor actor, SCContext context, IMapper mapper)
        {
            _actor = actor;
            _context = context;
            _mapper = mapper;
        }
        public int Id => 27;

        public string Name => "Get users favourite products";

        public PagedResponse<OrderLineDto> Execute(PagedSearchDto search)
        {
            var userId = _actor.Id;
            var userOrderLines = _context.OrderLines.Include(c => c.Product).Include(c => c.OrderLineAddIns).Where(c => c.Order.UserId == userId && c.IsFavourite==true);

            return userOrderLines.AddPagination<OrderLine, OrderLineDto>(search.Page, search.PerPage, _mapper);
        }
    }
}
