using DataAbstractions.Dapper;
using Security.Common.Configuration;
using System.Data.SqlClient;

namespace Security.Infraestructure.Common.SQLServer
{
    public class SQLServerBase
    {
        private IRepositorySettings _repositorySettings;
        public SQLServerBase(IRepositorySettings repositorySettings)
        {
            _repositorySettings = repositorySettings;
        }

        public IDataAccessor GetConnection()
        {
            return new DataAccessor(new SqlConnection(_repositorySettings.GetConnectionString()));
        }

    }
}
