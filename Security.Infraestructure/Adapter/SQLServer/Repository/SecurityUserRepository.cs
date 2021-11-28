using Security.Common.Configuration;
using Security.Infraestructure.Common.SQLServer;
using Security.Infraestructure.Entity;
using System.Linq;

namespace Security.Infraestructure.Adapter.SQLServer.Repository
{
    public class SecurityUserRepository : SQLServerBase, ISecurityUser
    {
        public SecurityUserRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }
        public SecurityUserEntity GetUserByUserName(string userName)
        {
            string sql = @"SELECT SecurityUserId,FullName,UserName,Code FROM SecurityUser WHERE UserName=@UserName";
            using (var connection = GetConnection())
            {
                return connection.Query<SecurityUserEntity>(sql, new { UserName = userName }).FirstOrDefault();
            }
        }

        public bool VerifyUser(string userName, string password)
        {
            string sql = "SELECT COUNT(1) FROM SecurityUser WHERE UserName=@UserName and Password=@Password";
            using (var connection = GetConnection())
            {
                return connection.ExecuteScalar<bool>(sql, new { UserName = userName, Password = password });
            }
        }
    }
}
