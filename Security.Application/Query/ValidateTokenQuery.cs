using MediatR;
using Security.Model.Dto;

namespace Security.Application.Query
{
    public class ValidateTokenQuery : IRequest<TokenResultDto>
    {
        public string Token { get; set; }
        public ValidateTokenQuery(string token)
        {
            Token = token;
        }
    }
}
