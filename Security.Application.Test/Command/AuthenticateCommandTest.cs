using MediatR;
using NUnit.Framework;
using Security.Application.Command;
using Security.Model.Dto;

namespace Security.Application.Test.Command
{
    public class AuthenticateCommandTest
    {
        private AuthenticateCommand oAuthenticateCommand;

        [SetUp]
        public void Setup()
        {
            oAuthenticateCommand = new AuthenticateCommand("TestUser", "123456");
        }

        [Test]
        public void AuthenticateCommand_ImplementIRequest_GetInterface()
        {
            bool IsIRequestInterface = oAuthenticateCommand is IRequest<AuthenticateToken>;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void AuthenticateCommand_Username_GetUserName()
        {
            bool blnCompareUserName = oAuthenticateCommand.Username == "TestUser";
            Assert.IsTrue(blnCompareUserName);

            oAuthenticateCommand.Username = "TestUser2";
            blnCompareUserName = oAuthenticateCommand.Username == "TestUser";
            Assert.IsFalse(blnCompareUserName);
        }

        [Test]
        public void AuthenticateCommand_Password_GetPassword()
        {
            bool blnCompareUserName = oAuthenticateCommand.Password == "123456";
            Assert.IsTrue(blnCompareUserName);

            oAuthenticateCommand.Password = "abc";
            blnCompareUserName = oAuthenticateCommand.Password == "123456";
            Assert.IsFalse(blnCompareUserName);
        }

    }
}
