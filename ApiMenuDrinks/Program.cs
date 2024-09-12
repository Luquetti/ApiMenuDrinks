using domain.Interface;
using FluentMigrator.Runner;
using Migracoes.Migracoes;
using Migracoes.Repositorios;

namespace ApiMenuDrinks
{
    class Program
    {
        public static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);

            using var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IDrinkRepositorio, DrinksRepositorio>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var AllowSpecificOrigins = "_allowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7116",
                                                          "http://localhost:5186");
                                  });
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static ServiceProvider CreateServices()
        {
            var str  = System.Configuration.ConfigurationManager.ConnectionStrings["conexao"]?.ConnectionString;

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(str)
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
