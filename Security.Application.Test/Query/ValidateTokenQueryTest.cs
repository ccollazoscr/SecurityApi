using MediatR;
using NUnit.Framework;
using Security.Application.Query;
using Security.Model.Dto;

namespace Security.Application.Test.Query
{
    public class ValidateTokenQueryTest
    {
        private ValidateTokenQuery oValidateTokenQuery;

        [SetUp]
        public void Setup()
        {
            oValidateTokenQuery = new ValidateTokenQuery("abc.abc.abc");
        }

        [Test]
        public void ValidateTokenQuery_ImplementIRequest_GetInterface()
        {
            bool IsIRequestInterface = oValidateTokenQuery is IRequest<TokenResultDto>;
            Assert.IsTrue(IsIRequestInterface);
        }

        [Test]
        public void ValidateTokenQuery_Token_GetToken()
        {
            bool blnCompareUserName = oValidateTokenQuery.Token == "abc.abc.abc";
            Assert.IsTrue(blnCompareUserName);

            oValidateTokenQuery.Token = "abc.abc.abc1";
            blnCompareUserName = oValidateTokenQuery.Token == "abc.abc.abc";
            Assert.IsFalse(blnCompareUserName);
        }
    }
}
