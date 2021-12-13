using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

namespace TransferDemo.API.Infraestructure.Exceptions
{
    /// <summary>
    /// Maneja las exepciones globales del proyecto.
    /// </summary>
    public static class ExceptionMiddleware
    {
        /// <summary>
        /// Permite que se muestre un mensaje de error genérico cuando se produce una exepción no controlada.
        /// </summary>
        /// <param name="app">Define una clase que proporciona los mecanismos para configurar la canalización de solicitudes de una aplicación.</param>
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var language = context.Response.Headers["Accept-Language"];

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"{contextFeature.Error}");
                        await context.Response.WriteAsync(new 
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature?.Error?.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
