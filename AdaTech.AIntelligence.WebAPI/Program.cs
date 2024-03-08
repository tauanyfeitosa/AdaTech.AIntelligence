using AdaTech.AIntelligence.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AdaTech.AIntelligence.DateLibrary.Context;
using Microsoft.AspNetCore.Identity;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Ioc.Filters;
using AdaTech.AIntelligence.WebAPI.Utils.Middleware;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentity<UserInfo, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ExpenseReportingDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserAuthService, UserAuthService>();

// add filters
builder.Services.AddScoped<MustHaveAToken>();

// add identity configuration and db context
builder.Services.AddDbContext<ExpenseReportingDbContext>();

// add authentication configuration
builder.Services.AddAuthentication(
    config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("3h9RtE2F#pW!b5Z^Kx)vDc6S7GyP4NqX")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "AIntelligence_issuer",
            ValidAudience = "AIntelligence_users",
            ValidateLifetime = true
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AIntelligenceAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add middlewares
app.UseHttpsRedirection();
app.UseMiddleware<MiddlewareException>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
