using Newtonsoft.Json;
using StarbuckClone.Domain;
using StarbucksClone.Application;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Logging
{
    public class DBUseCaseLogger : IUseCaseLogger
    {
        private SCContext _context;

        public DBUseCaseLogger(SCContext context)
        {
            _context = context;
        }

        public void Log(UseCaseLog log)
        {
                UseCasesAuditLog auditLog = new UseCasesAuditLog
                {
                    UseCaseName = log.UseCaseName,
                    Data = JsonConvert.SerializeObject(log.UseCaseData),
                    Username = log.Username,
                    ExecutedAt = DateTime.UtcNow
                };

                _context.UseCasesAuditLogs.Add(auditLog);
                _context.SaveChanges();
           
        }
    }
}
