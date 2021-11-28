using Moq;
using NUnit.Framework;
using Security.Application.Port;
using Security.Infraestructure.Adapter.Security;
using Security.Infraestructure.Adapter.Service;
using Security.Model.Dto;
using Security.Model.Model;

namespace Security.Infraestructure.Test.Adapter.Security
{
    public class SecurityAdapterTest
    {
        private Mock<ISecurityService> _mockISecurityService;
        private SecurityAdapter oSecurityAdapter;

        [SetUp]
        public void Setup()
        {
            _mockISecurityService = new Mock<ISecurityService>();
            oSecurityAdapter = new SecurityAdapter(_mockISecurityService.Object);
        }

        [Test]
        public void SecurityAdapter_ImplementISecurityPort_GetInterface()
        {
            bool IsIRequestInterface = oSecurityAdapter is ISecurityPort;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void Encrypt_SetNullString_GetNullEncrypt()
        {
            _mockISecurityService.Setup(m => m.Encrypt(It.IsAny<string>())).Returns((string)null);
            string strResult = oSecurityAdapter.Encrypt(null);
            Assert.IsNull(strResult);
        }

        [Test]
        public void Encrypt_SetStringValue_GetEncryptValue()
        {
            _mockISecurityService.Setup(m => m.Encrypt(It.IsAny<string>())).Returns("abc");
            string strResult = oSecurityAdapter.Encrypt("123");
            Assert.AreNotEqual("123", strResult);
            Assert.IsNotEmpty(strResult);
            Assert.AreEqual("abc", strResult);
        }

        [Test]
        public void GetToken_SetNullString_GetNullValue()
        {
            _mockISecurityService.Setup(m => m.GetToken(It.IsAny<User>())).Returns((string)null);
            string strResult = oSecurityAdapter.GetToken(new User() { Id = 1 });
            Assert.IsNull(strResult);
        }

        [Test]
        public void Encrypt_SetStringValue_GetTokenValue()
        {
            _mockISecurityService.Setup(m => m.GetToken(It.IsAny<User>())).Returns("abc.abc.abc");
            string strResult = oSecurityAdapter.GetToken(new User() { Id = 1 });
            Assert.IsNotEmpty(strResult);
            Assert.AreEqual("abc.abc.abc", strResult);
        }

        [Test]
        public void ValidateToken_SetNullString_GetNullValue()
        {
            _mockISecurityService.Setup(m => m.ValidateToken(It.IsAny<string>())).Returns((TokenResultDto)null);
            TokenResultDto oResult = oSecurityAdapter.ValidateToken("abc.abc.abc");
            Assert.IsNull(oResult);
        }

        [Test]
        public void ValidateToken_SetToken_GetValidTokenResult()
        {
            TokenResultDto oTokenResultDto = new TokenResultDto()
            {
                Code = "Code",
                Name = "Name",
                UserName = "UserName"
            };
            _mockISecurityService.Setup(m => m.ValidateToken(It.IsAny<string>())).Returns(oTokenResultDto);
            TokenResultDto oResult = oSecurityAdapter.ValidateToken("abc.abc.abc");
            Assert.IsNotNull(oResult);
            Assert.AreEqual(oTokenResultDto, oResult);
        }
    }
}
