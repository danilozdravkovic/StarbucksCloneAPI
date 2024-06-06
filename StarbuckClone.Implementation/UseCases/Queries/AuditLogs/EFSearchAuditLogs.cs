using StarbuckClone.Domain;
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
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;
            int skip = perPage * (page - 1);
            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<AuditLogDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new AuditLogDto
                {
                    Username = x.Username,
                    UseCaseName = x.UseCaseName,
                    ExecutedAt = x.ExecutedAt,
                    Data = x.Data
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount
        };

          
            
        }
    }
}
