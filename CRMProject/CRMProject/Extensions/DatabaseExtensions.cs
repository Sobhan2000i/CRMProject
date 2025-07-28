using CRMProject.DataBase;
using Microsoft.EntityFrameworkCore;


namespace CRMProject.Extensions
{
    public  static class DatabaseExtensions
    {
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await using ApplicationIdentityDbContext identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
            try
            {
                await dbContext.Database.MigrateAsync();
                app.Logger.LogInformation("Application Database Migrations applied Successfully.");

                await identityDbContext.Database.MigrateAsync();
                app.Logger.LogInformation("Identity Database Migrations applied Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
