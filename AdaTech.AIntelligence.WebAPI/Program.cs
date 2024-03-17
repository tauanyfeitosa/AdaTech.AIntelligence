using AdaTech.AIntelligence.DbLibrary.Roles;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Extensions;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

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
