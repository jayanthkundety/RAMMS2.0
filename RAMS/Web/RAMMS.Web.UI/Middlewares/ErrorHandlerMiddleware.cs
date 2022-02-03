using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RAMMS.Web.UI.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string HTTP_POST_CONTENT_CODE = "urlen";

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;

                if (context.Request.ContentType != null && context.Request.ContentType.Contains(HTTP_POST_CONTENT_CODE))
                {
                    context.Response.StatusCode = 500;

                    var result = JsonSerializer.Serialize(new { message = error?.Message });
                    await response.WriteAsync(result);
                }
                else
                {
                    context.Response.ContentType = "text/html";
                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                    await context.Response.WriteAsync("ERROR!<br><br>\r\n");
                    await context.Response.WriteAsync("</body></html>\r\n");
                    await context.Response.WriteAsync(new string(' ', 512));
                }
            }
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    //await _nextOne();

        //    //string originalPath = context.Request.Path.Value;
        //    //context.Items["originalPath"] = originalPath;
        //    //context.Request.Path = "/error/500";
        //    //await _nextOne();


        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception error)
        //    {
        //        var response = context.Response;

        //        //response.ContentType = "application/json";
        //        /*
        //        switch (error)
        //        {
        //            case AppException e:
        //                // custom application error
        //                //response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                //response.Redirect("~/AppError/Index");
        //                //_logger.Error(error, "Application Exception Occured");

        //                //string originalPath = context.Request.Path.Value;
        //                //context.Items["originalPath"] = originalPath;
        //                //context.Request.Path = "/error/500";
        //                //response.Redirect("/error/500");
        //                //await _next(context);

        //                await context.Response.WriteAsync("Woops! Error occured");
        //                break;
        //            case KeyNotFoundException e:
        //                // not found error
        //                response.StatusCode = (int)HttpStatusCode.NotFound;
        //                //response.Redirect("AppError/Index");
        //                _logger.Error(error, "No Found Exception Occured");
        //                break;
        //            default:
        //                // unhandled error
        //                //response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //                string originalPath = context.Request.Path.Value;
        //                context.Items["originalPath"] = originalPath;
        //                _logger.Error(error, "Unhandled Exception Occured");
        //                context.Request.Path = "/error/500";
        //                await _next(context);
        //                break;
        //        }
        //        */

        //        //Get Request ERROR - 500 - Solved
        //        //Ajax Request ERROR - Captured - Solved
        //        //jQDataTable - Error - unhandled
        //        //404 not yet worked

        //        if (context.Request.ContentType != null && context.Request.ContentType.Contains(HTTP_POST_CONTENT_CODE))
        //        {
        //            context.Response.StatusCode = 500;

        //            var result = JsonSerializer.Serialize(new { message = error?.Message });
        //            await response.WriteAsync(result);
        //        }
        //        else
        //        {
        //            context.Response.ContentType = "text/html";
        //            context.Response.StatusCode = 500;

        //            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
        //            await context.Response.WriteAsync("ERROR!<br><br>\r\n");
        //            await context.Response.WriteAsync("</body></html>\r\n");
        //            await context.Response.WriteAsync(new string(' ', 512));
        //        }

        //    }
        //}
    }
}
