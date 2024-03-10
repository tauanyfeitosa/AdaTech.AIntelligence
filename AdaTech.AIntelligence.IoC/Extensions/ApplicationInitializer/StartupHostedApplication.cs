using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer
{
    public class StartupHostedApplication: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupHostedApplication(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var seedUserRoleInitial = scope.ServiceProvider.GetRequiredService<ISeedUserInitial>();
                await seedUserRoleInitial.SeedRolesAsync();
                
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
