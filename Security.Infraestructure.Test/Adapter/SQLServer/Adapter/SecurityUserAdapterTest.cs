using Moq;
using NUnit.Framework;
using Security.Application.Port;
using Security.Common.Converter;
using Security.Infraestructure.Adapter.SQLServer.Adapter;
using Security.Infraestructure.Adapter.SQLServer.Repository;
using Security.Infraestructure.Entity;
using Security.Model.Model;

namespace Security.Infraestructure.Test.Adapter.SQLServer.Adapter
{
    public class SecurityUserAdapterTest
    {
        private Mock<ISecurityUser> _mockSecurityUser;
        private Mock<IEntityConverter<User, SecurityUserEntity>> _mockEntityConverter;

        private SecurityUserAdapter oSecurityUserAdapter;

        [SetUp]
        public void Setup()
        {
            _mockSecurityUser = new Mock<ISecurityUser>();
            _mockEntityConverter = new Mock<IEntityConverter<User, SecurityUserEntity>>();
            oSecurityUserAdapter = new SecurityUserAdapter(_mockSecurityUser.Object,_mockEntityConverter.Object);
        }

        [Test]
        public void SecurityAdapter_ImplementISecurityService_GetInterface()
        {
            bool IsIRequestInterface = oSecurityUserAdapter is IUserManagerPort;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void GetUserByUserName_SetNullUserName_GetNullValue()
        {
            User oUser = oSecurityUserAdapter.GetUserByUserName(null);
            Assert.IsNull(oUser);
        }

        [Test]
        public void GetUserByUserName_SetValidUserName_GetValidUser()
        {
            User oUser = new User() { Code = "Code", FullName = "FullName", Password = "123456", Id = 1, UserName = "cUser" };
            SecurityUserEntity oSecurityUserEntity = new SecurityUserEntity() { Code = "Code", FullName = "FullName", Password = "123456", SecurityUserId = 1, UserName = "cUser" };
            _mockSecurityUser.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns(oSecurityUserEntity);
            _mockEntityConverter.Setup(m => m.FromEntityToModel(It.IsAny<SecurityUserEntity>())).Returns(oUser);
            User oUserRes = oSecurityUserAdapter.GetUserByUserName("cUser");
            Assert.IsNotNull(oUserRes);
            Assert.AreEqual(oUser, oUserRes);
        }

        [Test]
        public void VerifyUser_SetNullParams_GetFalseValue()
        {
            _mockSecurityUser.Setup(m => m.VerifyUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            bool blnRes = oSecurityUserAdapter.VerifyUser("user","123456");
            Assert.IsFalse(blnRes);
        }

        [Test]
        public void VerifyUser_SetNullParams_GetTrueValue()
        {
            _mockSecurityUser.Setup(m => m.VerifyUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            bool blnRes = oSecurityUserAdapter.VerifyUser("user", "123456");
            Assert.IsTrue(blnRes);
        }

    }
}
