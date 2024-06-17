using StarbucksClone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.UseCases.Queries.Products
{
    public interface ISearchProductsQuery : IQuery<PagedResponse<ProductDto>,ProductSearchDto>
    {
    }
}
