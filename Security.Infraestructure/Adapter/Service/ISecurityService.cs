using Security.Model.Dto;
using Security.Model.Model;

namespace Security.Infraestructure.Adapter.Service
{
    public interface ISecurityService
    {
        public string Encrypt(string text);
        public string GetToken(User oUser);
        public TokenResultDto ValidateToken(string token);
    }
}
