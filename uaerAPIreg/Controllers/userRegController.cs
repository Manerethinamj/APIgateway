using AirlineAPIservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using userAPIreg.Method;
using userAPIreg.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace userAPIreg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userRegController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        string key = "string";

        public userRegController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //get all users
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


        //login.
        // GET api/<userRegController>/5
        [HttpGet("/api/v1.0/flight/Login")]
        public bool Get(string user_name, string password)
        {
            userModel user_login = new userModel();
            string userDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            fetchuserMethod fetchuser = new fetchuserMethod();
            bool isactiveuser = fetchuser.loginsucccess(user_name, password, userDbConnectionString);                
            return isactiveuser;
        }

        [HttpPost("/api/v1.0/user/register")]
        public void adduserreg(string name, string mail , string password, int role_id , string Place)
        {
            string usserreg = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(usserreg);
            UserviceMethod reg = new UserviceMethod();
            reg.adduserreg(name,mail,password,role_id,Place, sqlcon);

        }

        //booking flight.
        //post : book flight
        [HttpPost("/api/v1.0/flight/booking")]
        public void Bookingticket(int InvrntoryID, string username, string meal,string discount_code, List<travelerinformation> travelerinformations)
        {
            int n = travelerinformations.Count;
            UserviceMethod userviceMethod = new UserviceMethod();
            string bookconnection = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(bookconnection);
            userviceMethod.bookticket(sqlcon, InvrntoryID, username, n, meal,discount_code, travelerinformations);


        }

        //fetch PNR
        //get flight travel detail based on PNR
        [HttpGet("/api/v1.0/flight/ticket/{pnr}")]
        public List<FetchuserModel> Fetchticketonpnr(string pnr)
        {

            UserviceMethod userviceMethod = new UserviceMethod();
            string bookconnection = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(bookconnection);
            List<FetchuserModel> fetchusersbyPNR = userviceMethod.fetchuserbypnr(sqlcon, pnr);
            return fetchusersbyPNR;
        }

        //Fetch History with Email_ID
        //Get menthod
        [HttpGet("/api/v1.0/flight/history/email")]
        public List<FetchuserModel> Fetchticketonemail(string email_id)
        {
            UserviceMethod userviceMethod = new UserviceMethod();
            string bookconnection = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(bookconnection);
            List<FetchuserModel> fetchusersbyPNR = userviceMethod.fetchuserbyemail(sqlcon, email_id);
            return fetchusersbyPNR;
        }

        //Fetch ticket to UI
        //Get menthod
        [HttpGet("/api/v1.0/flight/fetchtoui")]
        public List<FetchuserModel> Fetchticketwhilebooking(string email_id,int number)
        {
            UserviceMethod userviceMethod = new UserviceMethod();
            string bookconnection = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(bookconnection);
            List<FetchuserModel> fetchusersbyPNR = userviceMethod.showtickettoIU(sqlcon, email_id,number);
            return fetchusersbyPNR;
        }

        //cancel ticket with PNR
        //update's Booking status
        [HttpDelete("/api/v1.0/flight/booking/cancel/{pnr}")]
        public void CancelBooking(string pnr)
        {
            UserviceMethod userviceMethod = new UserviceMethod();
            string cancelbookconnection = _configuration.GetValue<string>("ConnectionStrings:Accountcon");
            SqlConnection sqlcon = new SqlConnection(cancelbookconnection);
            DateTime getdate = DateTime.Now;
            userviceMethod.Cancelbookedticket(sqlcon, pnr,getdate);
        }

    }
}
