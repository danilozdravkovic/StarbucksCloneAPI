﻿using StarbuckClone.Domain;
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
    public class EFSearchAuditLogs : ISearchAuditLogsQuery
    {
        public int Id => 2;

        public string Name => "Search audit logs";

        public readonly SCContext _context;

        public EFSearchAuditLogs(SCContext context)
        {
            _context = context;
        }

        public IEnumerable<AuditLogDto> Execute(AuditLogSearchDto search)
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

            IEnumerable<AuditLogDto> data = query.Select(x => new AuditLogDto
            {
                Username=x.Username,
                UseCaseName=x.UseCaseName,
                ExecutedAt=x.ExecutedAt,
                Data=x.Data
            }).ToList();

            return data;
            
        }
    }
}
