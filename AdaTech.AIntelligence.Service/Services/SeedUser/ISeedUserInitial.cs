using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Ioc")]
namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    /// <summary>
    /// Interface for seeding initial user.
    /// </summary>
    internal interface ISeedUserInitial
    {
        /// <summary>
        /// Seeds initial user roles asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SeedRolesAsync();
    }
}
