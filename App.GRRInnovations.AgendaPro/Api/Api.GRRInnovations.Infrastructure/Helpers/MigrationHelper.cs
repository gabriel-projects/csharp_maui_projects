using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Api.GRRInnovations.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.Infrastructure.Helpers
{
    public static class MigrationHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            var dbContextSvc = svcProvider.GetRequiredService<ApiDbContext>();

            await dbContextSvc.Database.MigrateAsync();
        }
    }
}
