using AdaTech.AIntelligence.IoC.Extensions.Injections;
using AdaTech.AIntelligence.DbLibrary.Roles;
using AdaTech.AIntelligence.IoC.Extensions;
using Microsoft.AspNetCore.Identity;
using Hangfire;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.UserSystem;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseReporting;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));



var app = builder.Build();

IdentityDataInitializer.SeedData(app.Services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add middlewares
app.UseHttpsRedirection();
app.ResolveDependenciesMiddleware();

app.UseHangfireServer();
app.UseHangfireDashboard();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseDefaultFiles();
app.MapControllers();


var serviceProvider = app.Services.CreateScope().ServiceProvider;
var userManager = serviceProvider.GetService<UserManager<UserInfo>>();
var deleteUsersService = new DeleteUsersNotConfirmed(userManager);

RecurringJob.AddOrUpdate("DeleteUsersNotConfirmed", () => deleteUsersService.DeleteUsers(), Cron.Daily);

app.Run();
