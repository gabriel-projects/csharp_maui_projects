using Api.GRRInnovations.Infrastructure.Context;
using Api.GRRInnovations.Infrastructure.Repositories;
using Api.GRRInnovations.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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
            var sqlConnection = Configuration.GetConnectionString("SqlConnectionString");

            services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(sqlConnection));

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
