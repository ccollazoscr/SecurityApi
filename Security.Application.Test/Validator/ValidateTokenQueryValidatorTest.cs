using FluentValidation.TestHelper;
using NUnit.Framework;
using Security.Application.Query;
using Security.Application.Validator;

namespace Security.Application.Test.Validator
{
    public class ValidateTokenQueryValidatorTest
    {
        private ValidateTokenQueryValidator oValidateTokenQueryValidator;
        private ValidateTokenQuery oValidateTokenQuery;

        [SetUp]
        public void Setup()
        {
            oValidateTokenQueryValidator = new ValidateTokenQueryValidator();
            oValidateTokenQuery = new ValidateTokenQuery("abc.abc.abc");
        }

        #region Validation General

        [Test]
        public void PropertyObject_AllValidation_WithoutException()
        {
            var result = oValidateTokenQueryValidator.TestValidate(oValidateTokenQuery);
            Assert.IsTrue(result.IsValid);
        }

        #endregion

        #region Validation Token
        [Test]
        public void AuthenticateUsername_NotEmpty_ThrowException()
        {
            oValidateTokenQuery.Token = null;
            var result = oValidateTokenQueryValidator.TestValidate(oValidateTokenQuery);
            result.ShouldHaveValidationErrorFor(model => model.Token);

            oValidateTokenQuery.Token = "";
            result = oValidateTokenQueryValidator.TestValidate(oValidateTokenQuery);
            result.ShouldHaveValidationErrorFor(model => model.Token);
        }
        #endregion
    }
}
