using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer
{
    /// <summary>
    /// Hosted service to initialize the application.
    /// </summary>
    public class StartupHostedApplication : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes the StartupHostedApplication with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public StartupHostedApplication(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Starts the hosted service asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var seedUserRoleInitial = scope.ServiceProvider.GetRequiredService<ISeedUserInitial>();
                await seedUserRoleInitial.SeedRolesAsync();
            }
        }

        /// <summary>
        /// Stops the hosted service asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
