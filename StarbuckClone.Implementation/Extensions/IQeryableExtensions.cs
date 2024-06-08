using StarbucksClone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StarbuckClone.Implementation.Extensions
{
    public static class IQeryableExtensions
    {
        public static PaginatedResult<T> AddPagination<T>(this IQueryable<T> source,int? page,int? perPage)
        {

            int totalCount = source.Count();

            int currentPage = page.HasValue ? (int)Math.Abs((double)page) : 1;
            int itemsPerPage = perPage.HasValue ? (int)Math.Abs((double)perPage) : 10;

            int skip = itemsPerPage * (currentPage - 1);
            List<T> data = source.Skip(skip).Take(itemsPerPage).ToList();

            return new PaginatedResult<T>
            {
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PerPage = itemsPerPage,
                Data = data
            };
        }
    }

    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public List<T> Data { get; set; }
    }

}
