using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Dapper;
using MODEL;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

namespace REPOSITORY
{
    public class EmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EmployeeModel> GetAll()
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY ID DESC) AS RowNum, * FROM EMPLOYEE WHERE ACTIVE = 1";

                return conn.Query<EmployeeModel>(query).ToList();
            }
        }

        public EmployeeModel GetById(int id)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT  * FROM EMPLOYEE WHERE ACTIVE = 1 and Id  = " + id + " ";

                return conn.Query<EmployeeModel>(query).SingleOrDefault();
            }
        }

        public ResponseStatusModel Add(EmployeeModel EM)
        {
            ResponseStatusModel res = new ResponseStatusModel();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                string query = "INSERT INTO EMPLOYEE(NAME, AGE, NUMBER, ADDRESS, ACTIVE) VALUES (@Name, @Age, @Number, @Address, 1)";


                int rowsAffected = conn.Execute(query, EM);

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

        public ResponseStatusModel Update(EmployeeModel EM)
        {
            ResponseStatusModel res = new ResponseStatusModel();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string queryExist = "SELECT COUNT(*) FROM EMPLOYEE WHERE ACTIVE = 1 AND Id = @Id";
                int count = conn.QuerySingleOrDefault<int>(queryExist, new { EM.ID });

                if (count > 0)
                {

                    string query = "UPDATE EMPLOYEE SET NAME = @Name, Age = @Age, Number = @Number, Address = @Address where ID = @Id";

                    int rowsAffected = conn.Execute(query, EM);

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
                else
                {
                    res.STATUS = "No Exists";
                    res.MSG = "This Data Does Not Exists";
                    res.STATUSCODE = 700;
                }
            }
            return res;
        }

        public ResponseStatusModel Delete(int Id)
        {
            ResponseStatusModel res = new ResponseStatusModel();

            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                string queryExist = "SELECT COUNT(*) FROM EMPLOYEE WHERE ACTIVE = 1 AND Id = @Id";
                int count = conn.QuerySingleOrDefault<int>(queryExist, new { Id });

                if (count > 0)
                {
                    string queryDelete = "UPDATE EMPLOYEE SET Active = 0 WHERE ID = @Id";
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
                        res.MSG = "Failed to delete employee";
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

    }
}
