using MediatR;
using Moq;
using NUnit.Framework;
using Security.Application.Command;
using Security.Application.Port;
using Security.Common.Exception;
using Security.Model.Dto;
using Security.Model.Model;
using System.Threading.Tasks;

namespace Security.Application.Test.Command
{
    public class AuthenticateCommandHandlerTest
    {
        private Mock<ISecurityPort> _mockISecurityPort;
        private Mock<IUserManagerPort> _mockIUserManagerPort;

        private AuthenticateCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockISecurityPort = new Mock<ISecurityPort>();
            _mockIUserManagerPort = new Mock<IUserManagerPort>();

            _mockISecurityPort.Setup(m => m.Encrypt(It.IsAny<string>())).Returns("abcde");

            _handler = new AuthenticateCommandHandler(_mockISecurityPort.Object, _mockIUserManagerPort.Object);
        }

        [Test]
        public void AuthenticateCommandHandler_ImplementIRequestHandler_GetInterface()
        {
            bool IsIRequestInterface = _handler is IRequestHandler<AuthenticateCommand, AuthenticateToken>;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void Handle_VerifyUser_ReturnFalse() {
            _mockIUserManagerPort.Setup(m => m.VerifyUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            AuthenticateCommand oAuthenticateCommand = new AuthenticateCommand("TestUser", "123456");
            Assert.That(() => _handler.Handle(oAuthenticateCommand, default), Throws.InstanceOf(typeof(CustomErrorException)));
        }

        [Test]
        public async Task Handle_GetToken_GetValidStringAsync()
        {
            _mockIUserManagerPort.Setup(m => m.VerifyUser("TestUser", "abcde")).Returns(true);
            _mockIUserManagerPort.Setup(m => m.GetUserByUserName("TestUser")).Returns(new Model.Model.User() {Id=1});
            _mockISecurityPort.Setup(m => m.GetToken(It.IsAny<User>())).Returns("abc.abc.abc");
            AuthenticateCommand oAuthenticateCommand = new AuthenticateCommand("TestUser", "123456");

            AuthenticateToken oAuthenticateToken = await _handler.Handle(oAuthenticateCommand, default);
            Assert.That(oAuthenticateToken, Is.Not.Null);
            Assert.That(oAuthenticateToken.Token, Is.EqualTo("abc.abc.abc"));
        }
    }
}
