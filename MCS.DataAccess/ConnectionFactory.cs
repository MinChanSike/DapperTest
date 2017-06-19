using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MCS.DataAccess {
    public static class ConnectionFactory {

        private static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            }
        }

        public static IDbConnection GetDBConnection() {
            return new SqlConnection(ConnectionString);
        }

    }
}
