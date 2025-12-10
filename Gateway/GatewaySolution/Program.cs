using Gateway.GatewaySolution.Extensions;
using MMLib.SwaggerForOcelot;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Development-only: accept untrusted downstream TLS certificates (e.g. self-signed)
// Remove this in production and trust proper certificates instead.
ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

// Load single merged Ocelot configuration
//builder.Configuration.AddJsonFile("Ocelot/ocelot.json", optional: false, reloadOnChange: true);

builder.Configuration.AddJsonFile("Ocelot/Ocelot.Global.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Ocelot/Ocelot.Auth.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Ocelot/Ocelot.Email.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Ocelot/Ocelot.FileManagement.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Ocelot/Ocelot.ServicesManagement.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Ocelot/Ocelot.ServicesManagementLookUp.json", optional: false, reloadOnChange: true);

// Load SwaggerForOcelot configuration (aggregates downstream swagger endpoints)
builder.Configuration.AddJsonFile("Ocelot/swaggerForOcelot.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("Ocelot/swaggerForOcelot.json", optional: false, reloadOnChange: true);


// Add Ocelot configuration provider
builder.Configuration.AddOcelot("Ocelot", builder.Environment);

builder.Services.AddOcelot(builder.Configuration);

// Register SwaggerForOcelot to aggregate downstream Swagger docs
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Log loaded Ocelot routes for debugging
builder.Services.AddSingleton<IStartupFilter>(new OcelotRoutesLoggerStartupFilter(builder.Configuration));

// Inject/Initialize static utilities
ConfigurationUtil.Initialize(builder.Configuration);

// Authorization JWT configuration
builder.AddAppAuthetication();


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

// Enable SwaggerForOcelot UI to view aggregated Swagger from downstream services
app.UseSwaggerForOcelotUI();

app.UseOcelot().GetAwaiter().GetResult();

app.Run();
