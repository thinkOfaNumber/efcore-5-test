using System;
using System.Collections.Generic;
using efcore_test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace efcore_test
{
    class Program
    {
        public const string ConnectionString = "Data Source=.\\SQLSERVER2019; Initial Catalog=API_DEV; Integrated Security=true";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IServiceProvider serviceProvider = ConfigureServices();

            RunInScope(serviceProvider, app => app.DeleteAll());
            RunInScope(serviceProvider, app => app.AddLocations());
            RunInScope(serviceProvider, app => app.AddDevices());

            List<Device> devices = new List<Device>();
            RunInScope(serviceProvider, app => devices = app.PrintDevices());

            var device = devices[new Random().Next(devices.Count)];
            RunInScope(serviceProvider, app => app.PrintSingleDevice(device.DeviceId));
        }

        private static void RunInScope(IServiceProvider serviceProvider, Action<App> method)
        {
            using var scope = serviceProvider.CreateScope();
            var app = scope.ServiceProvider.GetService<App>();
            method(app);
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection sc = new ServiceCollection()
                .AddDbContext<ApiContext>(options =>
                {
                    options.UseSqlServer(ConnectionString);
                })
                .AddScoped<App>();

            return sc.BuildServiceProvider();
        }
    }

    // for dotnet tools
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer(Program.ConnectionString);

            return new ApiContext(optionsBuilder.Options);
        }
    }

}
