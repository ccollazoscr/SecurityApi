using Security.Application.Port;
using Security.Infraestructure.Adapter.Service;
using Security.Model.Dto;
using Security.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Infraestructure.Adapter.Security
{
    public class SecurityAdapter : ISecurityPort
    {
        private ISecurityService _securityService;
        public SecurityAdapter(ISecurityService securityService) {
            _securityService = securityService;
        }

        public string Encrypt(string text)
        {
            return _securityService.Encrypt(text);
        }

        public string GetToken(User oUser)
        {
            return _securityService.GetToken(oUser);
        }

        public TokenResultDto ValidateToken(string token)
        {
            return _securityService.ValidateToken(token);
        }
    }
}
