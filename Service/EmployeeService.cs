using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _loggerManager = logger;
        }
    }
}
