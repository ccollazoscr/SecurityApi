using MediatR;
using Microsoft.AspNetCore.Mvc;
using Security.Api.EntryModel;
using Security.Application.Command;
using Security.Application.Query;
using Security.Model.Dto;
using SecurityApi.EntryModel;
using System.Threading.Tasks;

namespace SecurityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        IMediator _mediator;
        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateEntryModel oAuthenticateEntryModel)
        {
            AuthenticateCommand oAuthenticateCommand = new AuthenticateCommand(oAuthenticateEntryModel.Username, oAuthenticateEntryModel.Password);
            AuthenticateToken token = await _mediator.Send(oAuthenticateCommand);
            return Ok(token);
        }

        [HttpGet("validatetoken")]
        public async Task<IActionResult> ValidateToken([FromQuery] string token)
        {
            ValidateTokenQuery oValidateTokenQuery = new ValidateTokenQuery(token);
            TokenResultDto oTokenResultDto = await _mediator.Send(oValidateTokenQuery);
            return Ok(oTokenResultDto);
        }
    }
}
