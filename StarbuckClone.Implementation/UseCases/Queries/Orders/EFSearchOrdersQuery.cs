﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.Orders;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.Orders
{
    public class EFSearchOrdersQuery : ISearchOrdersQuery
    {
        public readonly SCContext _context;
        public readonly IMapper _mapper;

        public EFSearchOrdersQuery(SCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 14;

        public string Name => "Search orders";

        public PagedResponse<OrderDto> Execute(SearchOrderDto search)
        {
            IQueryable<Order> query = _context.Orders
                                    .Include(o => o.OrderLines)
                                        .ThenInclude(ol => ol.OrderLineAddIns)
                                    .Include(o => o.OrderLines)
                                        .ThenInclude(ol => ol.Product)
                                    .Include(o=>o.User).Where(o=>o.IsActive)
                                    .OrderByDescending(o=>o.CreatedAt)
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.User.Username.ToLower().Contains(search.Username.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Email))
            {
                query = query.Where(x => x.User.Email.ToLower().Contains(search.Email.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.CreatedAt > search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.CreatedAt < search.DateTo);
            }


            return query.AddPagination<Order,OrderDto>(search.Page, search.PerPage, _mapper) ;
        }
    }
}
