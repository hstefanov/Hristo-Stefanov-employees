using EmployeeApp.Models.Domains;
using System.Collections.Generic;

namespace EmployeeApp.Core.Repositories
{
    public interface IEmployeeRepository : IFileRepository<Employee>
    {
    }
}
