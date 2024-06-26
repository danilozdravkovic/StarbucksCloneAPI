﻿using AutoMapper;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.AuditLogs
{
    public class EFSearchAuditLogsQuery : ISearchAuditLogsQuery
    {
        public int Id => 2;

        public string Name => "Search audit logs";

        public readonly SCContext _context;
        private readonly IMapper _mapper;

        public EFSearchAuditLogsQuery(SCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PagedResponse<AuditLogDto> Execute(AuditLogSearchDto search)
        {
            IQueryable<UseCasesAuditLog> query = _context.UseCasesAuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.ExecutedAt > search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.ExecutedAt < search.DateTo);
            }

            return query.AddPagination<UseCasesAuditLog,AuditLogDto>(search.Page, search.PerPage, _mapper);

            

          
            
        }
    }
}
