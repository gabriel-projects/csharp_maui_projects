using Api.GRRInnovations.Infrastructure.Context;
using Api.GRRInnovations.Infrastructure.Helpers;
using Api.GRRInnovations.Infrastructure.Repositories;
using Api.GRRInnovations.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Api.GRRInnovations.AgendaPro
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SqlConnectionString");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            Console.WriteLine($"sqlConnection Startup: {connectionString}");
            Console.WriteLine($"databaseUrl Startup: {databaseUrl}");

            var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : ConnectionHelper.BuildConnectionString(databaseUrl);


            services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connection));

            services.AddControllers();

            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddRouting(options => options.LowercaseUrls = true);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //todo: mover para metodo async
            //cron
            var scope = app.ApplicationServices.CreateScope();
            MigrationHelper.ManageDataAsync(scope.ServiceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
