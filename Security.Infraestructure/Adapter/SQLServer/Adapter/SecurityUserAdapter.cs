using Security.Application.Port;
using Security.Common.Converter;
using Security.Infraestructure.Adapter.SQLServer.Repository;
using Security.Infraestructure.Entity;
using Security.Model.Model;

namespace Security.Infraestructure.Adapter.SQLServer.Adapter
{
    public class SecurityUserAdapter : IUserManagerPort
    {
        private ISecurityUser _securityUser;
        private IEntityConverter<User, SecurityUserEntity> _entityConverter;
        public SecurityUserAdapter(ISecurityUser securityUser, IEntityConverter<User, SecurityUserEntity> entityConverter) {
            _securityUser = securityUser;
            _entityConverter = entityConverter;
        }
        public User GetUserByUserName(string userName)
        {
            SecurityUserEntity oSecurityUserEntity = _securityUser.GetUserByUserName(userName);
            return (oSecurityUserEntity == null) ? null : _entityConverter.FromEntityToModel(oSecurityUserEntity);
        }

        public bool VerifyUser(string userName, string password)
        {
            return _securityUser.VerifyUser(userName, password);
        }
    }
}
