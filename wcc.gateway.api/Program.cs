using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using wcc.gateway.api.Models.Discord;
using wcc.gateway.api.Models.Jwt;
using wcc.gateway.data;
using wcc.gateway.kernel.Models.Microservices;
using wcc.gateway.kernel.RequestHandlers;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();

RatingConfig ratingConfig = new RatingConfig();
builder.Configuration.GetSection("Rating").Bind(ratingConfig);
builder.Services.AddSingleton<RatingConfig>(ratingConfig);

DiscordConfig discordConfig = new DiscordConfig();
builder.Configuration.GetSection("Discord").Bind(discordConfig);
builder.Services.AddSingleton<DiscordConfig>(discordConfig);

JwtConfig jwtConfig = new JwtConfig();
builder.Configuration.GetSection("Jwt").Bind(jwtConfig);
builder.Services.AddSingleton<JwtConfig>(jwtConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Ping>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetNewsDetailQuery).Assembly));

builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
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
