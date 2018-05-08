using EmployeeApp.Bussiness.Services;
using EmployeeApp.Core.Repositories;
using EmployeeApp.Core.Services;
using EmployeeApp.Data.Repositories;
using EmployeeApp.Models.Common;
using EmployeeApp.Models.Domains;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EmployeeApp.Console
{
    public class Program
    {
        

        public static void Main(string[] args)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .Build();

            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<App.App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));

            // add repositories
            serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();

            // add services
            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            // add app
            serviceCollection.AddTransient<App.App>();
        }
    }
}
