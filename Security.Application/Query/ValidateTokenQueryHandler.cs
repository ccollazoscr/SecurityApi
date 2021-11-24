using MediatR;
using Security.Application.Port;
using Security.Common.Exception;
using Security.Model.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Application.Query
{

    public class ValidateTokenQueryHandler : IRequestHandler<ValidateTokenQuery, TokenResultDto>
    {
        private ISecurityPort _securityPort;
        public ValidateTokenQueryHandler(ISecurityPort securityPort)
        {
            _securityPort = securityPort;
        }

        public Task<TokenResultDto> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
        {
            TokenResultDto oTokenResultDto = _securityPort.ValidateToken(request.Token);
            if (oTokenResultDto == null)
            {
                throw new CustomErrorException(EnumErrorCode.InvalidToken);
            }
            return Task.FromResult(oTokenResultDto);
        }
    }
}
