using EmployeeApp.Bussiness.Services;
using EmployeeApp.Core.Repositories;
using EmployeeApp.Core.Services;
using EmployeeApp.Models.Domains;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmployeeApp.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void EmployeeService_Should_Return_Top_2_Employees()
        {
            // Arrange
            IEnumerable<Employee> models = new List<Employee>()
            {
                new Employee
                {
                    EmpId = 1,
                    ProjectId = 1,
                    DateFrom = new DateTime(2016,01,01),
                    DateTo = new DateTime(2016,06,01)
                },
                new Employee
                {
                    EmpId = 2,
                    ProjectId = 1,
                    DateFrom = new DateTime(2015,12,01),
                    DateTo = new DateTime(2016,06,01)
                },
                new Employee
                {
                    EmpId = 3,
                    ProjectId = 1,
                    DateFrom = new DateTime(2016,06,01),
                    DateTo = new DateTime(2016,12,31)
                }
            };

            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(x => x.GetAll()).Returns(models);
            IEmployeeService employeeService = new EmployeeService(employeeRepository.Object);

            // Act
            var projects = employeeService.GetEmployeesWithLongestOverlappingPeriod().ToList();

            // Assert
            Assert.NotNull(projects);
            Assert.Single(projects);

            var employees = projects[0].Employees.ToList();
            Assert.Equal(2, employees[0].EmpId);
            Assert.Equal(1, employees[1].EmpId);
        }

        [Fact]
        public void EmployeeService_Should_Not_Return_Result_With_Same_Id()
        {
            // Arrange
            IEnumerable<Employee> models = new List<Employee>()
            {
                new Employee
                {
                    EmpId = 1,
                    ProjectId = 1,
                    DateFrom = new DateTime(2016,01,01),
                    DateTo = new DateTime(2016,06,01)
                },
                new Employee
                {
                    EmpId = 1,
                    ProjectId = 1,
                    DateFrom = new DateTime(2015,12,01),
                    DateTo = new DateTime(2016,06,01)
                }
            };

            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(x => x.GetAll()).Returns(models);
            IEmployeeService employeeService = new EmployeeService(employeeRepository.Object);

            // Act
            var projects = employeeService.GetEmployeesWithLongestOverlappingPeriod().ToList();

            // Assert
            Assert.NotNull(projects);
            Assert.Single(projects);

            var employees = projects[0].Employees.ToList();
            Assert.Equal(1, employees[0].EmpId);
        }
    }
}
