using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Security.Application.Command;
using Security.Application.Query;
using Security.Model.Dto;
using SecurityApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Api.Test.Controllers
{
    public class TokenControllerTest
    {
        private Mock<IMediator> _mockIMediator;
        private TokenController oTokenController;

        [SetUp]
        public void Setup()
        {
            _mockIMediator = new Mock<IMediator>();
            oTokenController = new TokenController(_mockIMediator.Object);
        }

        [Test]
        public void Authenticate_SetNullParam_ThrowException()
        {
            Assert.That(() => oTokenController.Authenticate(null), Throws.InstanceOf(typeof(NullReferenceException)));
        }

        [Test]
        public async Task Authenticate_Send_ReturnAuthenticateToken()
        {
            AuthenticateToken oTokenResultDto = new AuthenticateToken() {  Token = "abc.abc.abc" };
            _mockIMediator.Setup(x => x.Send(It.IsAny<AuthenticateCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(oTokenResultDto)
                .Verifiable();
            var res = await oTokenController.Authenticate(new SecurityApi.EntryModel.AuthenticateEntryModel() { Username = "UserName", Password = "123456" });
            var okResult = res as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task ValidateToken_Send_TokenResultDto()
        {
            TokenResultDto oTokenResultDto = new TokenResultDto() { Code="Code", Name="Name", UserName="UserName"};
            _mockIMediator.Setup(x => x.Send(It.IsAny<ValidateTokenQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(oTokenResultDto)
                .Verifiable();
            var res = await oTokenController.ValidateToken("abc.abc.abc");
            var okResult = res as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
