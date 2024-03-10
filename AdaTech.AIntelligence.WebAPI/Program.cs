using AdaTech.AIntelligence.DateLibrary.Roles;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Extensions;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

IdentityDataInitializer.SeedData(app.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<UserInfo>>(),
                                 app.Services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add middlewares
app.UseHttpsRedirection();
app.ResolveDependenciesMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
