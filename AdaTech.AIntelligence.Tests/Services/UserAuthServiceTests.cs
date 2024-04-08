using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services.EmailService;
using Microsoft.Extensions.Configuration;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

namespace AdaTech.AIntelligence.Tests.Services
{
    public class UserAuthServiceTests
    {
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly UserManager<UserInfo> _userManager;
        private readonly ILogger<UserAuthService> _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _appSettings;
        private readonly IUserRegister _userRegister;

        public UserAuthServiceTests()
        {
            _signInManager = Substitute.For<SignInManager<UserInfo>>(
                Substitute.For<UserManager<UserInfo>>(
                    Substitute.For<IUserStore<UserInfo>>(),
                    null, null, null, null, null, null, null, null),
                Substitute.For<IHttpContextAccessor>(),
                Substitute.For<IUserClaimsPrincipalFactory<UserInfo>>(),
                null, null, null, null);
            _userManager = Substitute.For<UserManager<UserInfo>>(
                Substitute.For<IUserStore<UserInfo>>(),
                null, null, null, null, null, null, null, null);
            _logger = Substitute.For<ILogger<UserAuthService>>();
            _emailService = Substitute.For<IEmailService>();
            _appSettings = Substitute.For<IConfiguration>();
            _userRegister = Substitute.For<IUserRegister>();
        }

        // [Fact]
        // public async Task AuthenticateAsync_ComCredenciaisValidas_DeveRetornarTrue()
        // {
        //     // Arrange
        //     var email = "test@example.com";
        //     var password = "Password@123";
        //     var signInResult = SignInResult.Success;
        //     _signInManager.PasswordSignInAsync(email, password, false, false)
        //                   .Returns(Task.FromResult(signInResult));
        //     _userManager.FindByEmailAsync(email).Returns(Task.FromResult(new UserInfo()));
        //     var authService = new UserAuthService(_signInManager, _userManager, _logger, _emailService, _appSettings);
        //
        //     // Act
        //     var resultado = await authService.AuthenticateAsync(email, password);
        //
        //     // Assert
        //     resultado.Should().BeTrue();
        // }

        // [Fact]
        // public async Task AuthenticateAsync_ComUsuarioInvalido_DeveRetornarFalse()
        // {
        //     // Arrange
        //     var signInResult = SignInResult.Failed;
        //
        //     _signInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), false, false)
        //         .Returns(Task.FromResult(signInResult));
        //
        //     var authService = new UserAuthService(_signInManager, _userManager, _logger, null, null);
        //
        //     // Act
        //     var resultado = await authService.AuthenticateAsync("usuario@exemplo.com", "senha456");
        //
        //     // Assert
        //     resultado.Should().BeFalse();
        // }

        [Fact]
        public async Task AuthenticateAsync_ComExcecao_DeveRetornarThrowArgumentException()
        {
            // Arrange
            _signInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), false, false)
                .Throws(new Exception("Erro na autenticação"));
            var authService = new UserAuthService(_signInManager, _userManager, _logger, _emailService, _appSettings);

            // Act & Assert
            Func<Task> act = async () => await authService.AuthenticateAsync("usuario@exemplo.com", "senha789");
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task LogoutAsync_DeveChamarSignOutAsync()
        {
            // Arrange
     
            var authService = new UserAuthService(_signInManager, null, _logger, _emailService, _appSettings);

            // Act
            await authService.LogoutAsync();

            // Assert
            await _signInManager.Received(1).SignOutAsync();
        }

        [Fact]
        public async Task RegisterUserAsync_ComRegistroInvalido_DeveRegistrarLogErroEThrowArgumentException()
        {
            // Arrange
            var exceptionMessage = "Teste de exceção";
            _userRegister.RegisterUserAsync().Throws(new Exception(exceptionMessage));
            var appSettings = Substitute.For<IConfiguration>();
            var authService = new UserAuthService(null, _userManager, _logger, _emailService, _appSettings);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await authService.RegisterUserAsync(_userRegister));
        }
    }
}

