using AdaTech.AIntelligence.DateLibrary.Roles;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Extensions;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using AdaTech.AIntelligence.IoC.Middleware;
using AdaTech.AIntelligence.Service.Services.UserSystem.IUserPromote;
using AdaTech.AIntelligence.Service.Services.UserSystem.UserPromote;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

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
