using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Infrastructure.Helper
{
    public static class ClientInfoHelper
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
        public static string GetUserId(HttpContext context)
        {
            var userIdClaim = context?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;
            return userId ?? "Unknown";
        }
    }
}
