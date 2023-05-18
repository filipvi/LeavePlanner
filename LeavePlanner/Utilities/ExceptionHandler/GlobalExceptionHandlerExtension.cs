using LeavePlanner.Utilities.Extensions;
using LeavePlanner.Utilities.Logger;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace LeavePlanner.Utilities.ExceptionHandler
{
    public static class GlobalExceptionHandlerExtension
    {
        //This method will globally handle logging unhandled execeptions.
        //It will respond json response for ajax calls that send the json accept header
        //otherwise it will redirect to an error page
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, string errorPagePath,
            bool respondWithJsonErrorDetails = true)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    //============================================================
                    //Log Exception
                    //============================================================
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    string errorTitle = "Došlo je do pogreške prilikom obrade vašeg zahtjeva.";
                    string dateTime = DateTime.Now.DateToString();
                    string message = exception.Message;
                    string fullException = exception.StackTrace;
                    string instance = Guid.NewGuid().ToString();

                    var sb = new StringBuilder();
                    sb.Append(errorTitle);
                    sb.AppendLine();
                    sb.Append(dateTime);
                    sb.AppendLine();
                    sb.Append(message);
                    sb.Append(fullException);
                    sb.AppendLine();
                    sb.Append(instance);

                    Log.ErrorLog(sb.ToString());

                    var json = JsonConvert.SerializeObject(errorTitle);

                    //============================================================
                    //Return response
                    //============================================================
                    var matchText = "JSON";

                    bool requiresJsonResponse = context.Request
                        .GetTypedHeaders()
                        .Accept
                        .Any(t => t.Suffix.Value?.ToUpper() == matchText
                                  || t.SubTypeWithoutSuffix.Value?.ToUpper() == matchText);

                    if (requiresJsonResponse)
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        respondWithJsonErrorDetails = false;
                        if (!respondWithJsonErrorDetails)
                            json = exception.Message;

                        await context.Response
                            .WriteAsync(json, Encoding.UTF8);
                    }
                    else
                    {
                        context.Response.Redirect(errorPagePath);
                        await Task.CompletedTask;
                    }
                });
            });
        }
    }

}
