using MediatR;
using Moq;
using NUnit.Framework;
using Security.Application.Port;
using Security.Application.Query;
using Security.Common.Exception;
using Security.Model.Dto;
using System.Threading.Tasks;

namespace Security.Application.Test.Query
{
    public class ValidateTokenQueryHandlerTest
    {
        private Mock<ISecurityPort> _mockISecurityPort;
        
        private ValidateTokenQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockISecurityPort = new Mock<ISecurityPort>();
            _handler = new ValidateTokenQueryHandler(_mockISecurityPort.Object);
        }

        [Test]
        public void ValidateTokenQueryHandler_ImplementIRequestHandler_GetInterface()
        {
            bool IsIRequestInterface = _handler is IRequestHandler<ValidateTokenQuery, TokenResultDto>;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void Handle_ValidateToken_GetNullValue()
        {
            _mockISecurityPort.Setup(m => m.ValidateToken(It.IsAny<string>())).Returns((TokenResultDto)null);
            ValidateTokenQuery oValidateTokenQuery = new ValidateTokenQuery("abc.abc.abc");
            Assert.That(() => _handler.Handle(oValidateTokenQuery, default), Throws.InstanceOf(typeof(CustomErrorException)));
        }

        [Test]
        public async Task Handle_TokenResult_GetValidValueAsync()
        {
            var oTokenResult = new TokenResultDto() { Code = "1234", Name = "abc", UserName = "testUserName" };
            _mockISecurityPort.Setup(m => m.ValidateToken(It.IsAny<string>())).Returns(oTokenResult);
            ValidateTokenQuery oValidateTokenQuery = new ValidateTokenQuery("abc.abc.abc");
            TokenResultDto oTokenResultDto = await _handler.Handle(oValidateTokenQuery, default);
            Assert.That(oTokenResultDto, Is.Not.Null);
            Assert.That(oTokenResultDto, Is.EqualTo(oTokenResult));
        }

    }
}
