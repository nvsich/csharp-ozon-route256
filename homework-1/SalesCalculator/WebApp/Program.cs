using ApplicationCore.Interfaces;
using ApplicationCore.Service;
using DataAccess.Repositories;

namespace WebApp;

internal static class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IUnitOfWork>(_ =>
        {
            var config = builder.Configuration;

            var salesDateFilePath = config.GetValue<string>("UnitOfWorkOptions:SalesDateFilePath");
            var seasonalityFactorFilePath = config.GetValue<string>("UnitOfWorkOptions:SeasonalityFactorFilePath");

            if (string.IsNullOrEmpty(salesDateFilePath) || string.IsNullOrEmpty(seasonalityFactorFilePath))
                throw new InvalidOperationException("Missing configuration for " +
                                                    "UnitOfWorkOptions:SalesDateFilePath or " +
                                                    "UnitOfWorkOptions:SeasonalityFactorFilePath.");

            return new UnitOfWork(
                salesDateFilePath,
                seasonalityFactorFilePath
            );
        });

        builder.Services.AddScoped<IOrderCalculationService, OrderCalculationService>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseRouting();

        app.MapControllers();

        app.Run();
    }
}