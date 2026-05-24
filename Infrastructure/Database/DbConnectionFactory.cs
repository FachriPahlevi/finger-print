using System.Configuration;
using System.Data.SqlClient;

namespace FingerPrint4
{
    internal static class DbConnectionFactory
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public static string GetConnectionString()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["FingerprintDb"];

            if (connectionString == null || string.IsNullOrWhiteSpace(connectionString.ConnectionString))
            {
                throw new ConfigurationErrorsException("FingerprintDb connection string is not configured.");
            }

            return connectionString.ConnectionString;
        }
    }
}
