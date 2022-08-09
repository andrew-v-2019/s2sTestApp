

namespace S2STestApp.Web
{
    using System.Net;
    using Microsoft.AspNetCore.Http;


    public static class Extensions
    {
        public static string GetUserData(HttpContext httpContext)
        {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress.ToString().Trim().ToLower();
            //var hostName = Dns.GetHostEntry(httpContext.Connection.RemoteIpAddress)?.HostName?.Trim().ToLower() ?? string.Empty;
            var port =  httpContext.Connection.RemotePort;

            var userData = $"{remoteIpAddress}-{port}";
            return userData;
        }
    }
}
