using Microsoft.AspNetCore.Http;

namespace Services.Email.Infrastructure.Helper
{
    public static class IpHelper
    {
        public static string GetClientIp(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }
            else
            {
                // Handle multiple IPs (in case of proxies)
                ip = ip.Split(',').FirstOrDefault();
            }

            return ip ?? "Unknown";
        }
    }
}
