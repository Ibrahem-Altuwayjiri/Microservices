using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Services;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.LookupService;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// ? DEVELOPMENT ONLY - Allow self-signed SSL certificates
if (builder.Environment.IsDevelopment())
{
    ServicePointManager.ServerCertificateValidationCallback += 
        (sender, certificate, chain, sslPolicyErrors) => true;
}

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure HttpClient for gateway API calls
var gatewayUrl = builder.Configuration["Gateway:BaseUrl"] ?? "http://localhost:5295";
builder.Services.AddHttpClient("GatewayClient", client =>
{
    client.BaseAddress = new Uri(gatewayUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpContextAccessor();

// Register core services
builder.Services.AddTransient<ITokenProvider, TokenProvider>();
builder.Services.AddTransient<IBaseService, BaseService>();

// Register ManageServices
builder.Services.AddTransient<IMainServicesService, MainServicesService>();
builder.Services.AddTransient<ISubServicesService, SubServicesService>();
builder.Services.AddTransient<ISubSubServicesService, SubSubServicesService>();
builder.Services.AddTransient<IServiceDetailsService, ServiceDetailsService>();

// Register LookupServices
builder.Services.AddTransient<IActivitiesService, ActivitiesService>();
builder.Services.AddTransient<IDomainsService, DomainsService>();
builder.Services.AddTransient<ITagsService, TagsService>();
builder.Services.AddTransient<IHeadersService, HeadersService>();

// Register API services
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IFileManagementService, FileManagementService>();

// Add authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

// Add global exception middleware
app.UseMiddleware<MiddlewareExceptionHandler>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
