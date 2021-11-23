using Security.Infraestructure.Entity;

namespace Security.Infraestructure.Adapter.SQLServer.Repository
{
    public interface ISecurityUser
    {
        public SecurityUserEntity GetUserByUserName(string userName);
        public bool VerifyUser(string userName, string password);
    }
}
