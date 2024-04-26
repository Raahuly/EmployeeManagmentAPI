using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace REPOSITORY
{
    public class Connection
    {
        private readonly IConfiguration _configuration;

        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IDbConnection GetConnection(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}
