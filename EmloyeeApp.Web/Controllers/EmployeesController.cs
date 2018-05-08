using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Core.Services;
using EmployeeApp.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmloyeeApp.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly AppSettings _config;

        public EmployeesController(IEmployeeService employeeService, IOptions<AppSettings> config)
        {
            _employeeService = employeeService;
            _config = config.Value;
        }

        public IActionResult Index()
        {
            var projects = _employeeService.GetEmployeesWithLongestOverlappingPeriod();

            return View(projects);
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile files)
        {
            var fileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _config.DataFilePath));

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                 files.CopyTo(stream);
            }

            return RedirectToAction("Index");
        }
    }
}