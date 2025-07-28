using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using Ambev.DeveloperEvaluation.Common.Logging;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDefaultLogging();

            Log.Information("Starting web application");

            builder.Services.AddControllers();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            //HealthChecks
            builder.Services.AddEndpointsApiExplorer();
            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            //Conf do Postgree
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.RegisterDependencies();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            //MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            //MassTransit
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SaleCreatedConsumer>();
                x.AddConsumer<SaleModifiedConsumer>();
                x.AddConsumer<SaleCancelledConsumer>();
                x.AddConsumer<ItemCancelledConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Middleware de validação customizada
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                //// Executa migrations automaticamente ao subir
                //using (var scope = app.Services.CreateScope())
                //{
                //    var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
                //    dbContext.Database.Migrate();
                //}
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBasicHealthChecks();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
