using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace REPOSITORY
{
    public class RegisterRepository
    {
        private readonly IConfiguration _configuration;

        public RegisterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResponseStatusModel Add(RegisterModel EM)
        {
            ResponseStatusModel res = new ResponseStatusModel();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                string query = "INSERT INTO Register( [USER], PASSWORD) VALUES (@USER, @PASSWORD)";

                int rowsAffected = conn.Execute(query, EM);


                if (rowsAffected > 0)
                {
                    string COUNTUSER = "SELECT COUNT(*) FROM Register WHERE [USER] = @USER AND PASSWORD = @PASSWORD";

                    int CountAffect = conn.Execute(COUNTUSER, EM);

                    res.STATUS = "Success";
                    res.MSG = "Regiser SuccessFully";
                    res.STATUSCODE = 200;
                }
                else
                {
                    res.STATUS = "Error";
                    res.MSG = "Failed to add employee";
                    res.STATUSCODE = 500;
                }
            }
            return res;
        }

        public RegisterModel GetRegister(RegisterModel emp)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT * FROM Register WHERE [USER] = @USER AND PASSWORD = @PASSWORD";

                return conn.Query<RegisterModel>(query, new { USER = emp.USER, PASSWORD = emp.PASSWORD }).SingleOrDefault();
            }
        }

        public ResponseStatusModel Login(RegisterModel EM)
        {
            ResponseStatusModel res = new ResponseStatusModel();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "select count(*) from Register where [USER] = @USER and PASSWORD = @PASSWORD";
                int count = conn.QuerySingleOrDefault<int>(query, new { EM.USER,EM.PASSWORD });
               // int rowsAffected = conn.Execute(query, EM);
                if (count > 0)
                {
                    res.STATUS = "Success";
                    res.MSG = "Login SuccessFully";
                    res.STATUSCODE = 200;
                }
                else
                {
                    res.STATUS = "Error";
                    res.MSG = "Login Failed";
                    res.STATUSCODE = 500;
                }
            }
            return res;
        }
    }
}
