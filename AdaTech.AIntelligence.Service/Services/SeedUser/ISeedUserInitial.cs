using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Ioc")]
namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    internal interface ISeedUserInitial
    {
        Task SeedRolesAsync();
    }
}
