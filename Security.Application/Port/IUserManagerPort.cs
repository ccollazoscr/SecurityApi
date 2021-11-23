using Security.Model.Model;

namespace Security.Application.Port
{
    public interface IUserManagerPort
    {
        public User GetUserByUserName(string userName);
        public bool VerifyUser(string userName, string password);
    }
}
