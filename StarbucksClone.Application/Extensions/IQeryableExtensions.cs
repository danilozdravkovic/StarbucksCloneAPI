using StarbucksClone.Application.DTO;


namespace StarbuckClone.Implementation.Extensions
{
    public static class IQeryableExtensions
    {
        public static PagedResponse<TResult> AddPagination<TSource, TResult>(this IQueryable<TSource> source, int? page, int? perPage, Func<TSource, TResult> selector)
            where TResult : class
        {

            int totalCount = source.Count();

            int currentPage = page.HasValue ? (int)Math.Abs((double)page) : 1;
            int itemsPerPage = perPage.HasValue ? (int)Math.Abs((double)perPage) : 10;

            int skip = itemsPerPage * (currentPage - 1);
            List<TResult> data = source.Skip(skip).Take(itemsPerPage).Select(selector).ToList();

            return new PagedResponse<TResult>
            {
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PerPage = itemsPerPage,
                Data = data
            };


            
        }
    }

  

}
