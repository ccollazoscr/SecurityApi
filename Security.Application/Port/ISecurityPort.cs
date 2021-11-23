using Security.Model.Dto;
using Security.Model.Model;

namespace Security.Application.Port
{
    public interface ISecurityPort
    {
        public string Encrypt(string text);
        public string GetToken(User oUser);
        public TokenResultDto ValidateToken(string token);
    }
}
