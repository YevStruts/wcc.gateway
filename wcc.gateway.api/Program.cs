using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using wcc.gateway.api.Models.Discord;
using wcc.gateway.api.Models.Jwt;
using wcc.gateway.data;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using wcc.gateway.kernel.RequestHandlers;
using Amazon.Runtime;
using Amazon.SimpleSystemsManagement;
using Amazon;
using Amazon.SimpleSystemsManagement.Model;
using wcc.gateway.api.Helpers;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllers();

var environment = builder.Configuration.GetValue("Environment", "dev");

string microservicesSettingsPath = $"/{environment}/microservices";
var microservicesSettings = await AWSParameterStore.Instance().GetParametersByPathAsync(microservicesSettingsPath);
Microservices.Config mcsvcConfig = new Microservices.Config
{
    CoreUrl = microservicesSettings[$"{microservicesSettingsPath}/core-url"],
    RatingUrl = microservicesSettings[$"{microservicesSettingsPath}/rating-url"],
    WidgetUrl = microservicesSettings[$"{microservicesSettingsPath}/widget-url"]
};
builder.Services.AddSingleton<Microservices.Config>(mcsvcConfig);

string discordSettingsPath = $"/{environment}/discord";
var discordSettings = await AWSParameterStore.Instance().GetParametersByPathAsync(discordSettingsPath);
DiscordConfig discordConfig = new DiscordConfig
{
    ClientID = discordSettings[$"{discordSettingsPath}/client-id"],
    ClientSecret = discordSettings[$"{discordSettingsPath}/client-secret"],
    RedirectUrl = discordSettings[$"{discordSettingsPath}/redirect-url"]
};
builder.Services.AddSingleton<DiscordConfig>(discordConfig);

string jwtSettingsPath = $"/{environment}/jwt";
var jwtSettings = await AWSParameterStore.Instance().GetParametersByPathAsync(jwtSettingsPath);
JwtConfig jwtConfig = new JwtConfig
{
    Audience = jwtSettings[$"{jwtSettingsPath}/audience"],
    Issuer = jwtSettings[$"{jwtSettingsPath}/issuer"],
    Key = jwtSettings[$"{jwtSettingsPath}/key"]
};
builder.Services.AddSingleton<JwtConfig>(jwtConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Ping>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetNewsDetailQuery).Assembly));

string gatewaySettingsPath = $"/{environment}/wcc-gateway";
var gatewaySettings = await AWSParameterStore.Instance().GetParametersByPathAsync(gatewaySettingsPath);
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(gatewaySettings[$"{gatewaySettingsPath}/mssql"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// if (!app.Environment.IsDevelopment())
// {
//     app.UseHttpsRedirection();
// }

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:3000", 
                 "https://localhost:7258",
                 "https://wcc-cossacks.com:3000",
                 "https://wcc-cossacks.com"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
 
app.Run();
