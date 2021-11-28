using NUnit.Framework;
using Security.Common.Converter;
using Security.Infraestructure.Converter;
using Security.Infraestructure.Entity;
using Security.Model.Model;

namespace Security.Infraestructure.Test.Converter
{
    public class SecurityUserConverterTest
    {
        private SecurityUserConverter oSecurityUserConverter;

        [SetUp]
        public void Setup()
        {
            oSecurityUserConverter = new SecurityUserConverter();
        }

        [Test]
        public void SecurityUserConverter_ImplementIRequest_GetInterface()
        {
            bool IsIRequestInterface = oSecurityUserConverter is IEntityConverter<User, SecurityUserEntity>;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void SecurityUserConverter_FromEntityToModel_GetNullValue()
        {
            User oUser = oSecurityUserConverter.FromEntityToModel(null);
            Assert.IsNull(oUser);
        }

        [Test]
        public void SecurityUserConverter_FromEntityToModel_GetModel()
        {
            SecurityUserEntity oSecurityUserEntity = new SecurityUserEntity()
            {
                Code = "Code",
                FullName = "FullName",
                Password = "123456",
                SecurityUserId = 1,
                UserName = "UserName"
            };
            User oUser = oSecurityUserConverter.FromEntityToModel(oSecurityUserEntity);
            Assert.IsNotNull(oUser);
            Assert.AreEqual(oSecurityUserEntity.Code, oUser.Code);
            Assert.AreEqual(oSecurityUserEntity.FullName, oUser.FullName);
            Assert.AreEqual(oSecurityUserEntity.Password, oUser.Password);
            Assert.AreEqual(oSecurityUserEntity.SecurityUserId, oUser.Id);
            Assert.AreEqual(oSecurityUserEntity.UserName, oUser.UserName);
        }

        [Test]
        public void SecurityUserConverter_FromModelToEntity_GetEntity()
        {
            User oUser = new User()
            {
                Code = "Code",
                FullName = "FullName",
                Password = "123456",
                Id = 1,
                UserName = "UserName"
            };
            SecurityUserEntity oSecurityUserEntity = oSecurityUserConverter.FromModelToEntity(oUser);
            Assert.IsNotNull(oSecurityUserEntity);
            Assert.AreEqual(oUser.Code, oSecurityUserEntity.Code);
            Assert.AreEqual(oUser.FullName, oSecurityUserEntity.FullName);
            Assert.AreEqual(oUser.Password, oSecurityUserEntity.Password);
            Assert.AreEqual(oUser.Id, oSecurityUserEntity.SecurityUserId);
            Assert.AreEqual(oUser.UserName, oSecurityUserEntity.UserName);
        }

    }
}
