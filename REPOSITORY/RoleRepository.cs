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
    public class RoleRepository
    {
        private readonly IConfiguration _configuration;

        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResponseStatusModel Add(RoleClass RM)
        {
            ResponseStatusModel res = new ResponseStatusModel();

            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "insert into RoleTable(RoleName, Active) values (@RoleName, 1)";

                int rowsAffected = conn.Execute(query, RM);

                if (rowsAffected > 0)
                {
                    res.STATUS = "Success";
                    res.MSG = "Add Successfully";
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

        public List<RoleClass> GetAll()
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY RoleId DESC) AS RowNum, * FROM RoleTable WHERE ACTIVE = 1";

                return conn.Query<RoleClass>(query).ToList();
            }
        }

        public RoleClass GetById(int id)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT  * FROM RoleTable WHERE ACTIVE = 1 and RoleId  = " + id + " ";
                return conn.Query<RoleClass>(query).SingleOrDefault();
            }
        }

        public ResponseStatusModel Delete(int Id)
        {
            ResponseStatusModel res = new ResponseStatusModel();

            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                string queryExist = "SELECT COUNT(*) FROM RoleTable WHERE ACTIVE = 1 AND RoleId = @RoleId";
                int count = conn.QuerySingleOrDefault<int>(queryExist, new {Id});

                if (count > 0)
                {
                    string queryDelete = "UPDATE RoleTable SET Active = 0 WHERE RoleId = @RoleId";
                    int rowsAffected = conn.Execute(queryDelete, new { Id });

                    if (rowsAffected > 0)
                    {
                        res.STATUS = "Success";
                        res.MSG = "Deleted Successfully";
                        res.STATUSCODE = 200;
                    }
                    else
                    {
                        res.STATUS = "Error";
                        res.MSG = "Failed to delete RoleTable";
                        res.STATUSCODE = 500;
                    }
                }
                else
                {
                    res.STATUS = "No Exists";
                    res.MSG = "This Data Does Not Exist";
                    res.STATUSCODE = 700;
                }
        }
            return res;
        }

        public ResponseStatusModel Update(RoleClass RC)
        {
            ResponseStatusModel res = new ResponseStatusModel();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string queryExist = "SELECT COUNT(*) FROM RoleTable WHERE ACTIVE = 1 AND RoleId = @RoleId";
                int count = conn.QuerySingleOrDefault<int>(queryExist, new { RC.RoleId });
                if(count > 0)
                {
                    string query = "UPDATE RoleTable SET RoleName = @RoleName WHERE RoleId = @RoleId";

                    int rowsAffected = conn.Execute(query, RC);

                    if (rowsAffected > 0)
                    {
                        res.STATUS = "Success";
                        res.MSG = "Update Successfully";
                        res.STATUSCODE = 200;
                    }
                    else
                    {
                        res.STATUS = "Error";
                        res.MSG = "Failed to add employee";
                        res.STATUSCODE = 500;
                    }
                }
                else
                {
                    res.STATUS = "No Exists";
                    res.MSG = "This Data Does Not Exists";
                    res.STATUSCODE = 700;
                }
            }
            return res;
        }
    }
}
