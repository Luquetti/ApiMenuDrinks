using System.Linq;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Migracoes.Migracoes;
namespace ApiMenuDrinks
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString("Server=DESKTOP-0S13DMG\\SQLEXPRESS;Database=Bebidas;TrustServerCertificate=True;User Id=sa;Password=debora806")
                    .ScanIn(typeof(MigratorDrinks).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
