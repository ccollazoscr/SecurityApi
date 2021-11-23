
namespace Security.Common.Configuration
{
    public class RepositorySettings : IRepositorySettings
    {
        private string ConnectionString;
        private string DatabaseName;
        public RepositorySettings SetConnectionString(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            return this;
        }
        public RepositorySettings SetDatabaseName(string DatabaseName)
        {
            this.DatabaseName = DatabaseName;
            return this;
        }

        public string GetConnectionString()
        {
            return ConnectionString;
        }
        public string GetDatabaseName()
        {
            return DatabaseName;
        }
    }
}
