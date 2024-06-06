using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class PagedSearchDto
    {
        public int? PerPage { get; set; } 
        public int? Page { get; set; }
    }
}
