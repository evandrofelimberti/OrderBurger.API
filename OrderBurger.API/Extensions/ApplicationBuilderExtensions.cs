using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;

namespace OrderBurger.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope   = app.ApplicationServices.CreateScope();
        var context       = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger        = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrações aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao aplicar migrações.");
            throw;
        }
    }    
}