using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using userAPIreg.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace userAPIreg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userRegController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public userRegController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<userRegController>
        [HttpGet]
        public IEnumerable<userModel> Get()
        {
            string userDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            var users = new List<userModel>();
            SqlConnection usertblDbConnection = new SqlConnection(userDbConnectionString);
            SqlCommand cmd = new SqlCommand("select * from [dbo].[ar_user]", usertblDbConnection);
            usertblDbConnection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        userModel userrow = new userModel();
                        userrow.id = reader.GetInt32(reader.GetOrdinal("id"));
                        userrow.user_name = reader.GetString(reader.GetOrdinal("user_name"));
                        userrow.password = reader.GetString(reader.GetOrdinal("password"));
                        userrow.email_id = reader.GetString(reader.GetOrdinal("email_id"));
                        userrow.role_id = reader.GetInt32(reader.GetOrdinal("role_id"));
                        users.Add(userrow);
                    }
                }
            usertblDbConnection.Close();
            
            return users;
        }




        // GET api/<userRegController>/5
        [HttpGet("{id}")]
        public string Get(string user_name,string password)
        {
            userModel user_login = new userModel();
            return loginsucccess(user_name, password).ToString();

        }

        // POST api/<userRegController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<userRegController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<userRegController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public bool loginsucccess(string user_name, string password)
        {

            string userDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:Accountcon");

            bool activeuser = true;
            SqlConnection usertblDbConnection = new SqlConnection(userDbConnectionString);
            SqlCommand cmd = new SqlCommand("select id,user_name,role_id from [dbo].[ar_user] where user_name ='" + user_name + "' and password ='" + password + "';",
                usertblDbConnection);
            usertblDbConnection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userModel userrow = new userModel();
                        userrow.id = reader.GetInt32(reader.GetOrdinal("id"));
                        userrow.user_name = reader.GetString(reader.GetOrdinal("user_name"));
                        userrow.role_id = reader.GetInt32(reader.GetOrdinal("role_id"));
                    }
                    activeuser = true;
                }
                else
                    activeuser = false;
            }
            usertblDbConnection.Close();



            return activeuser;
        }

    }
}
