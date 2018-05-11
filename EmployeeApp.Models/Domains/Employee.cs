using System;

namespace EmployeeApp.Models.Domains
{
    public class Employee
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
