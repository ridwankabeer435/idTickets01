using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure
{
    public class Program
    {

        
        private static IConfiguration _iconfiguration;
        private static IServiceCollection _iservices;

        public static void Main(String[] args)
        {
            GetAppSettingsFile();
        }

        // set up applicaition.json file in the current project to identify configuration data
        protected static IConfiguration GetAppSettingsFile()
        {
            
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            _iconfiguration = builder.Build();
            return _iconfiguration;
            // possibly add another method to configure database connection and register its context with EFCore
            // obtain connection string from appsettings.json file
            
            //string connectionString = _iconfiguration.GetConnectionString("TicketsDbConnectionString");

            // upon starting, the class library should be able to connect to the database
        }
    }
}
