using System;
using System.Collections.Generic;

namespace EmployeeApp.Models.Domains
{
    public class EmployeeComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            int result = 1;

            if ((x.DateFrom < y.DateTo) && (y.DateFrom < x.DateTo))
            {
                result = x.DateTo.Value.Subtract(y.DateFrom).Days;
            }
            else if((y.DateFrom < x.DateTo) && (x.DateFrom < y.DateTo))
            {
                result = y.DateTo.Value.Subtract(x.DateFrom).Days;
            }
            else if((y.DateFrom < x.DateFrom) && (y.DateTo > x.DateTo))
            {
                result = x.DateTo.Value.Subtract(x.DateFrom).Days;
            }

            return result;
        }
    }
}
