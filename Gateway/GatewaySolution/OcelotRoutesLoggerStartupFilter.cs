using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;

public class OcelotRoutesLoggerStartupFilter : IStartupFilter
{
    private readonly IConfiguration _configuration;
    public OcelotRoutesLoggerStartupFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            var logger = app.ApplicationServices.GetService(typeof(ILogger<OcelotRoutesLoggerStartupFilter>)) as ILogger;
            try
            {
                var routes = _configuration.GetSection("Routes").GetChildren();
                foreach (var r in routes)
                {
                    var key = r.GetValue<string>("Key");
                    var upstream = r.GetValue<string>("UpstreamPathTemplate");
                    logger?.LogInformation("Loaded Ocelot route Key='{Key}' Upstream='{Upstream}'", key, upstream);
                }
            }
            catch { }

            next(app);
        };
    }
}
