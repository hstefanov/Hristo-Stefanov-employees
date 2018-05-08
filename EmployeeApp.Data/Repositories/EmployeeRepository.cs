using EmployeeApp.Core.Repositories;
using EmployeeApp.Models.Common;
using EmployeeApp.Models.Domains;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EmployeeApp.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly string[] _dateTimeFormats = DateTimeFormatInfo.InvariantInfo.GetAllDateTimePatterns();

        private string _fileName;
        private readonly AppSettings _config;

        public EmployeeRepository(IOptions<AppSettings> config)
        {
            _config = config.Value;
            _fileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _config.DataFilePath));
        }

        public IEnumerable<Employee> GetAll()
        {
            IList<Employee> _employees = new List<Employee>();

            using (var reader = new StreamReader(_fileName))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    int employeeId;
                    if (!int.TryParse(line[0].Trim(), out employeeId))
                    {
                        throw new ArgumentException($"Cannot parse employee id : {line[0]}");
                    }

                    int projectId;
                    if (!int.TryParse(line[1].Trim(), out projectId))
                    {
                        throw new ArgumentException($"Cannot parse projectId id : {line[1]}");
                    }

                    DateTime dateFrom;
                    if (!DateTime.TryParseExact(line[2].Trim(), _dateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom))
                    {
                        throw new ArgumentException($"Cannot parse start date time : {line[2]}");
                    }

                    DateTime dateTo;
                    DateTime.TryParseExact(line[3].Trim(), _dateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out dateTo);

                    Employee employee = new Employee()
                    {
                        EmpId = employeeId,
                        ProjectId = projectId,
                        DateFrom = dateFrom,
                        DateTo = dateTo
                    };

                    _employees.Add(employee);
                }
            }

            return _employees;
        }
    }
}
