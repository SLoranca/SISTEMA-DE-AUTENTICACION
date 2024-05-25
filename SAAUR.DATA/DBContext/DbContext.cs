using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SAAUR.DATA.DBContext
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _config;
        public DbContext(IConfiguration config)
		{	
			_config = config;
		}

        public IDbConnection Get()
		{
			string mode = _config["Mode"];
			IDbConnection conn = new SqlConnection(_config["ConnectionStrings:" + mode]);
			conn.Open();
			return conn;
		}
    }
}