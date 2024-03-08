using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Ioc")]
namespace AdaTech.AIntelligence.Service.Services.SeedUser
{
    internal interface ISeedUserInitial
    {
        Task SeedUsersAsync();
        Task SeedRolesAsync();
    }
}
