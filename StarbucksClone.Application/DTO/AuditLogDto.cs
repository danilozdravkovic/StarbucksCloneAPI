using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class AuditLogDto
    {
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string Data { get; set; }
    }

    public class AuditLogSearchDto : PagedSearchDto
    {
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
