using Moq;
using NUnit.Framework;
using Security.Common.Configuration;
using Security.Infraestructure.Adapter.Service;
using Security.Model.Dto;
using System;
using System.Linq;


namespace Security.Infraestructure.Test.Adapter.Service
{
    public class SecurityServiceTest
    {
        private Mock<IGeneralSettings> _mockIGeneralSettings;
        private SecurityService oSecurityService;

        [SetUp]
        public void Setup()
        {
            _mockIGeneralSettings = new Mock<IGeneralSettings>();
            oSecurityService = new SecurityService(_mockIGeneralSettings.Object);
        }

        [Test]
        public void SecurityAdapter_ImplementISecurityService_GetInterface()
        {
            bool IsIRequestInterface = oSecurityService is ISecurityService;
            Assert.IsTrue(IsIRequestInterface);
        }
        [Test]
        public void Encrypt_SetNullValue_TrowException()
        {
            Assert.That(() => oSecurityService.Encrypt(null), Throws.InstanceOf(typeof(ArgumentNullException)));
        }

        [Test]
        public void Encrypt_SetValidValue_GetEncryptValue()
        {
            _mockIGeneralSettings.Setup(m => m.GetKeyPassword()).Returns("This is the secret password");
            string strResult = oSecurityService.Encrypt("abc");
            Assert.IsNotEmpty(strResult);
            Assert.AreEqual("d1hDZVdaK1Nldm89", strResult);
        }

        [Test]
        public void GetToken_SetNullValue_TrowException()
        {
            _mockIGeneralSettings.Setup(m => m.GetKeyToken()).Returns("This is the secret token");
            _mockIGeneralSettings.Setup(m => m.GetTimeToken()).Returns(1);
            Assert.That(() => oSecurityService.GetToken(null), Throws.InstanceOf(typeof(NullReferenceException)));
        }

        [Test]
        public void GetToken_SetValidValue_GetToken()
        {
            _mockIGeneralSettings.Setup(m => m.GetKeyToken()).Returns("This is the secret token");
            _mockIGeneralSettings.Setup(m => m.GetTimeToken()).Returns(1);
            string strResult = oSecurityService.GetToken(new Model.Model.User() { Code = "", FullName = "", Id = 1, Password = "", UserName = "" });
            Assert.IsNotEmpty(strResult);
            Assert.AreEqual(2, strResult.Count(c => c == '.'));
        }

        [Test]
        public void ValidateToken_SetNullValue_GetNullValue()
        {
            _mockIGeneralSettings.Setup(m => m.GetKeyToken()).Returns("This is the secret token");
            TokenResultDto oTokenResultDto = oSecurityService.ValidateToken(null);
            Assert.IsNull(oTokenResultDto);
        }

        [Test]
        public void ValidateToken_SetInvalidToken_GetNullValue()
        {
            _mockIGeneralSettings.Setup(m => m.GetKeyToken()).Returns("This is the secret token");
            TokenResultDto oTokenResultDto = oSecurityService.ValidateToken("abc.abc.abc");
            Assert.IsNull(oTokenResultDto);
        }

    }
}
