using FluentValidation.TestHelper;
using NUnit.Framework;
using Security.Application.Command;
using Security.Application.Validator;

namespace Security.Application.Test.Validator
{
    public class AuthenticateCommandValidatorTest
    {
        private AuthenticateCommandValidator oAuthenticateCommandValidator;
        private AuthenticateCommand oAuthenticateCommand;

        [SetUp]
        public void Setup()
        {
            oAuthenticateCommandValidator = new AuthenticateCommandValidator();
            oAuthenticateCommand = new AuthenticateCommand("TestUser", "123456");
        }

        #region Validation General

        [Test]
        public void PropertyObject_AllValidation_WithoutException()
        {
            var result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            Assert.IsTrue(result.IsValid);
        }

        #endregion

        #region Validation Authenticate Username
        [Test]
        public void AuthenticateUsername_NotEmpty_ThrowException()
        {
            oAuthenticateCommand.Username = null;
            var result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Username);

            oAuthenticateCommand.Username = "";
            result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Test]
        public void AuthenticateUsername_MaximumLength_ThrowException()
        {
            oAuthenticateCommand.Username = new string('a', 64);
            var result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldNotHaveValidationErrorFor(model => model.Username);

            oAuthenticateCommand.Username += "a";
            result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }
        #endregion

        #region Validation Authenticate Password
        [Test]
        public void AuthenticatePassword_NotEmpty_ThrowException()
        {
            oAuthenticateCommand.Password = null;
            var result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Password);

            oAuthenticateCommand.Password = "";
            result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void AuthenticatePassword_MaximumLength_ThrowException()
        {
            oAuthenticateCommand.Password = new string('a', 128);
            var result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldNotHaveValidationErrorFor(model => model.Password);

            oAuthenticateCommand.Password += "a";
            result = oAuthenticateCommandValidator.TestValidate(oAuthenticateCommand);
            result.ShouldHaveValidationErrorFor(model => model.Password);
        }
        #endregion
    }
}
