using MediatR;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Command;
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
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
