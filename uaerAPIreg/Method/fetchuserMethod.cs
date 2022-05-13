using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using userAPIreg.Models;

namespace userAPIreg.Method
{
    public class fetchuserMethod
    {
        internal bool loginsucccess(string user_name, string password, string userDbConnectionString)
        {
            SqlConnection usertblDbConnection = new SqlConnection(userDbConnectionString);
            SqlCommand cmd = new SqlCommand("select id,user_name,role_id from [dbo].[ar_user] where user_name ='" + user_name + "' and password ='" + password + "';",
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

                            isactive = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            usertblDbConnection.Close();
            return isactive;
        }

    }
}
