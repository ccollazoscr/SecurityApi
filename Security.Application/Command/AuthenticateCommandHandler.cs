using MediatR;
using Security.Application.Port;
using Security.Common.Exception;
using Security.Model.Dto;
using Security.Model.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Application.Command
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateToken>
    {
        private ISecurityPort _securityPort;
        private IUserManagerPort _userManagerPort;
        public AuthenticateCommandHandler(ISecurityPort securityPort, IUserManagerPort userManagerPort) {
            _securityPort = securityPort;
            _userManagerPort = userManagerPort;
        }
        public Task<AuthenticateToken> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            AuthenticateToken oAuthenticateToken = null;
            string password = _securityPort.Encrypt(request.Password);
            if (_userManagerPort.VerifyUser(request.Username, password))
            {
                User oUser = _userManagerPort.GetUserByUserName(request.Username);
                oAuthenticateToken = new AuthenticateToken
                {
                    Token = _securityPort.GetToken(oUser)
                };
            }
            else {
                throw new CustomErrorException(EnumErrorCode.UnauthorizedUser);
            }
            return Task.FromResult(oAuthenticateToken);
        }
    }
}
