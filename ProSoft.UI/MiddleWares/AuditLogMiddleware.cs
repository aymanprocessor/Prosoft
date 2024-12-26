using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.UI.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;


        public AuditLogMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, AuditScope auditScope)
        {

            var stopwatch = Stopwatch.StartNew();
            var executionTime = DateTime.UtcNow;
            //var applicationName = _configuration.GetApplicationName();
            var correlationId = context.TraceIdentifier;

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();

            if (auditScope.Logs.Any())
            {
                foreach (var auditLog in auditScope.Logs)
                {
                    var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                    var ipAddress = string.IsNullOrEmpty(forwardedHeader)
                        ? context.Connection.RemoteIpAddress?.ToString()
                        : forwardedHeader.Split(',')[0];

                    auditLog.CorrelationId = correlationId;
                    //auditLog.SessionId = context.User.GetUserCustomProperty("SessionId");
                    auditLog.ClientIpAddress = ipAddress;
                    auditLog.BrowserInfo = context.Request.Headers["User-Agent"];
                   // auditLog.ApplicationName = applicationName;
                    auditLog.MachineName = Environment.MachineName;
                    auditLog.MachineVersion = Environment.Version.ToString();
                    auditLog.MachineOsVersion = Environment.OSVersion.ToString();
                    var routeData = context.GetRouteData();
                    auditLog.ServiceName = routeData?.Values["controller"]?.ToString();
                    auditLog.MethodName = routeData?.Values["action"]?.ToString();
                    auditLog.HttpMethod = context.Request.Method;
                    auditLog.RequestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                    auditLog.ResponseStatus = context.Response.StatusCode.ToString();
                    auditLog.ExecutionTime = executionTime;
                    auditLog.ExecutionDuration = stopwatch.ElapsedMilliseconds.ToString();
                    auditLog.RequestHeaders = JsonConvert.SerializeObject(
                        context.Request.Headers.ToDictionary(
                            header => header.Key, header => header.Value.ToString()
                            )
                        );
                    auditLog.QueryParameters = JsonConvert.SerializeObject(
                        context.Request.Query.ToDictionary(
                            query => query.Key, query => query.Value.ToString()
                            )
                        );

                    if (context.Request.Body.CanSeek == true)
                    {
                        context.Request.Body.Position = 0;
                    }
                    else
                    {
                        context.Request.EnableBuffering();
                    }

                    string originalContent;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
                    {
                        originalContent = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0;
                    }

                    var bodyParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(originalContent);
                    if (bodyParameters != null && bodyParameters.Any())
                    {
                        var bodyContent = JsonConvert.SerializeObject(bodyParameters);
                        auditLog.BodyParameters = bodyContent;
                    }

                    if (context.Response.StatusCode >= 400)
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        var responseText = await new StreamReader(responseBody).ReadToEndAsync();

                        auditLog.Exception = responseText;

                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }

        }
    }
    

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuditLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditLogMiddleware>();
        }
    }
}
