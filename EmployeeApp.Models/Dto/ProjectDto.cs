using EmployeeApp.Models.Domains;
using System.Collections.Generic;

namespace EmployeeApp.Models.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
