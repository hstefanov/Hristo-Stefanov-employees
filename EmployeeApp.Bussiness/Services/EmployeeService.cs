using EmployeeApp.Core.Repositories;
using EmployeeApp.Core.Services;
using EmployeeApp.Models.Domains;
using EmployeeApp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApp.Bussiness.Services
{
    /// <summary>
    /// EmployeeService
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Returns top two employees who have been worked in same project for longest time.
        /// </summary>
        /// <returns>Collection of ProjectDto</returns>
        public IEnumerable<ProjectDto> GetEmployeesWithLongestOverlappingPeriod()
        {
            IEnumerable<Employee> employees = _employeeRepository.GetAll();

            IEnumerable<ProjectDto> result = employees
                            .GroupBy(e => e.ProjectId)
                            .Select(g => 
                                new ProjectDto
                                {
                                    ProjectId = g.Key,
                                    Employees = g.Select(e => e)
                                                 .OrderByDescending(e => e, new EmployeeComparer())
                                                 .Take(2)
                                });

            return result;
        }
    }
}
