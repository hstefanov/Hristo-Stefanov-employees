using EmployeeApp.Core.Services;
using System.Linq;

namespace EmployeeApp.Console.App
{
    public class App
    {
        private readonly IEmployeeService _employeeService;

        public App(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Run()
        {
            var projects = _employeeService.GetEmployeesWithLongestOverlappingPeriod();

            foreach (var project in projects)
            {
                System.Console.Write($"Project with id : {project.ProjectId} \n Employees id with longest time in same project : {project.Employees.FirstOrDefault()?.EmpId} \t {project.Employees.LastOrDefault()?.EmpId} ");
                System.Console.WriteLine();
            }
        }
    }
}
