using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TransferDemo.API.Infraestructure;
using TransferDemo.API.Infraestructure.ActionFilters;
using TransferDemo.API.Infraestructure.DataSeed;

namespace TransferDemo.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Habilita la política CORS para que otros servicios puedan conectarse a este servicio.
        /// </summary>
        /// <param name="services">Un IServiceCollection que permite facilitar la implementación del patrón de diseño Dependency Inyection y otras configuraciones del servicio.</param>
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        });

        /// <summary>
        /// Permite generar la autodocumentación de la API de permisos.
        /// </summary>
        /// <param name="services">Un IServiceCollection que permite facilitar la implementación del patrón de diseño Dependency Inyection y otras configuraciones del servicio.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer API", Version = "v1" });
                //c.ResolveConflictingActions(apiDescription => apiDescription.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


            });
        }

        /// <summary>
        /// Configura el monitor de estado del servicio.
        /// </summary>
        /// <param name="services">Un IServiceCollection que permite facilitar la implementación del patrón de diseño Dependency Inyection y otras configuraciones del servicio.</param>
        public static void ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());
        }

        /// <summary>
        /// Configura las inyecciones de dependencia del proyecto.
        /// </summary>
        /// <param name="services">Un IServiceCollection que permite facilitar la implementación del patrón de diseño Dependency Inyection y otras configuraciones del servicio.</param>
        public static void ConfigureInyections(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<TransferDbContext>();
            services.AddScoped<ValidateAccountBalanceFilterAttribute>();
            services.AddTransient<AccountDataSeeder>();
        }
    }
}
