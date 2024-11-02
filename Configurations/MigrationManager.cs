using blog_c_.Data;
using blog_c_.Helper;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Configurations;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
            {
                try
                {
                    // atualizar o db
                    appContext.Database.Migrate();
    
                    // fazer o seeding
                    var seeding = new Seeding(appContext);
    
                    // descomentar para fazer
                    // seeding.CreateAll();
    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Erro ao aplicar as migartions/seed");
                    throw;
                }
            }
        }
    
        return webApp;
    }
}