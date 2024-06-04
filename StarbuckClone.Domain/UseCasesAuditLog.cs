using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class UseCasesAuditLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string Data { get; set; }
    }
}
