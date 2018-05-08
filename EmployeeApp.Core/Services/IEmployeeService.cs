using EmployeeApp.Models.Domains;
using EmployeeApp.Models.Dto;
using System.Collections.Generic;

namespace EmployeeApp.Core.Services
{
    public interface IEmployeeService
    {
        IEnumerable<ProjectDto> GetEmployeesWithLongestOverlappingPeriod();
    }
}
