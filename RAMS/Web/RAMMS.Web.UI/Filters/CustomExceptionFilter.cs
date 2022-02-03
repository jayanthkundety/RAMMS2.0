using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RAMMS.Web.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = string.Empty;
            string traceId = string.Empty;

            var exceptionType = context.Exception.GetType();
            if (exceptionType is AppException) //Checking for my custom exception type
            {
                message = context.Exception.Message;
            }
            else
            {
                traceId = Guid.NewGuid().ToString();
                message = $"Server error occurred. Kindly contact Administrator for more details.{Environment.NewLine} Use the trace id {traceId}, to report to your admin.";
            }

            //You can enable logging error
            LogErrorMessage(context, traceId);

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            if (context.HttpContext.Request.Method.Contains("GET"))
            {
                response.ContentType = "text/html";
                context.Result = new RedirectToActionResult("500", "error", null);

                //response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //response.WriteAsync("ERROR!<br><br>\r\n");
                //response.WriteAsync($"<p>{message}</p></body></html>\r\n");
                //response.WriteAsync(new string(' ', 512));
            }
            else
            {
                response.ContentType = "application/json";
                context.Result = new ObjectResult(new { Message = message });
                //context.Result = JsonSerializer.Serialize(new { message = message });
            }
        }

        private void LogErrorMessage(ExceptionContext context, string traceId)
        {
            string errorMessage = string.Empty;
            errorMessage += $"{Environment.NewLine}-----------------------------------------";
            errorMessage += string.IsNullOrEmpty(traceId) ? "" : $"{Environment.NewLine}Trace Id : {traceId}";
            errorMessage += $"{Environment.NewLine}{context.Exception.GetBaseException()}";
            errorMessage += $"{Environment.NewLine}-----------------------------------------{Environment.NewLine}";
            _logger.LogError(errorMessage);
        }
    }
}
