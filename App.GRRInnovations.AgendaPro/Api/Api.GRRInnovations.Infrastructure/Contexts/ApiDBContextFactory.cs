using Api.GRRInnovations.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace Api.GRRInnovations.Infrastructure.Context
{
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.json", true)
               .AddEnvironmentVariables()
               .Build();

            var connectionString = config.GetConnectionString("SqlConnectionString");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : ConnectionHelper.BuildConnectionString(databaseUrl);

            Debug.WriteLine(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseNpgsql(connection);

            return new ApiDbContext(optionsBuilder.Options);
        }
    }
}
