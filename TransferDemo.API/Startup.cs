using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferDemo.API.Extensions;
using TransferDemo.API.Infraestructure;
using TransferDemo.API.Infraestructure.DataSeed;
using TransferDemo.API.Infraestructure.Exceptions;
using TransferDemo.API.Models;

namespace TransferDemo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureInyections();
            services.ConfigureCors();
            services.ConfigureHealthChecks();

            // Esto permite cambiar el código de error de 400 (Bad request) a 422 (Unprocessable entity) 
            // cuando el modelo enviado desde el cliente es inválido.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddControllers();
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransferDemo.API v1"));
            }

            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
           
            SeedData(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }

        private void SeedData(IApplicationBuilder app)
        {
            var scopedFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<AccountDataSeeder>();
                service.Seed();
            }
        }
    }
}
