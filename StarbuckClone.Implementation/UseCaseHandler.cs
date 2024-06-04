using StarbucksClone.Application;
using StarbucksClone.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StarbuckClone.Implementation
{
    public class UseCaseHandler
    {
        private readonly IUseCaseLogger _logger;

        public UseCaseHandler(IUseCaseLogger logger)
        {
            _logger = logger;
        }

        public void HandleCommand<TData>(ICommand<TData> command,TData data)
        {
            UseCaseLog log = new UseCaseLog
            {
                UseCaseData = data,
                UseCaseName = command.Name,
                Username = "Test"
            };

            _logger.Log(log);

            command.Execute(data);
        }

        public TResult HandleQuery<TResult, TSearch>(IQuery<TResult, TSearch> query, TSearch search)
            where TResult : class
        {
            UseCaseLog log = new UseCaseLog
            {
                UseCaseData = search,
                UseCaseName = query.Name,
                Username = "Test"
            };

            _logger.Log(log);

            var result = query.Execute(search);

            return result;
        }
    }
}
