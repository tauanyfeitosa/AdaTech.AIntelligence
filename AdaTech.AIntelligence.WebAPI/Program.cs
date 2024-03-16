using AdaTech.AIntelligence.DbLibrary.Roles;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Extensions;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


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

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new { message = "Recurso nao encontrado - certifique-se de estar logado" };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }
});

app.MapControllers();

app.Run();
