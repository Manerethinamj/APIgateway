using APIgateway.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIgateway.usermethod
{
    public class userFetchmentod
    {
        internal usercred loginsucccess(string user_name, string password, string userDbConnectionString)
        {
            usercred usercred = new usercred();
            SqlConnection usertblDbConnection = new SqlConnection(userDbConnectionString);
            SqlCommand cmd = new SqlCommand("select id,user_name,password,role_id,email_id from [dbo].[ar_user] where user_name ='" + user_name + "' and password ='" + password + "';",
                usertblDbConnection);
            usertblDbConnection.Open();
            bool isactive = false;
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usercred.Username = reader["user_name"].ToString();
                            usercred.Password = reader["password"].ToString();
                            usercred.role_id = Convert.ToInt32(reader["role_id"].ToString());
                            usercred.email_id = reader["email_id"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            usertblDbConnection.Close();
            return usercred;
        }
    }
}
