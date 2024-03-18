using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Tests.Mock
{
    public class UserInfoMock: UserManager<UserInfo>
    {
        public UserInfoMock() : base(Substitute.For<IUserStore<UserInfo>>(),
        Substitute.For<IOptions<IdentityOptions>>(),
        Substitute.For<IPasswordHasher<UserInfo>>(),
        new IUserValidator<UserInfo>[0],
        new IPasswordValidator<UserInfo>[0],
        Substitute.For<ILookupNormalizer>(),
        Substitute.For<IdentityErrorDescriber>(),
        Substitute.For<IServiceProvider>(),
        Substitute.For<ILogger<UserManager<UserInfo>>>())
        {
        }
    }
}
