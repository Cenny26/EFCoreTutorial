using EFCoreTutorial.Common;
using EFCoreTutorial.Data.Context;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Custom services
        builder.Services.AddLogging();
        builder.Services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseLazyLoadingProxies();
            config.UseSqlServer(StringConstants.DbConnectionString);
            config.EnableSensitiveDataLogging();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}