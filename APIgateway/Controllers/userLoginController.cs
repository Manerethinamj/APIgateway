using APIgateway.usermethod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIgateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class userLoginController : ControllerBase
    {
        private readonly IAurthenticationManager aurthenticationManager;
        public userLoginController(IAurthenticationManager aurthenticationManager)
        {
            this.aurthenticationManager = aurthenticationManager;
        }
        // GET: api/<userLoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<userLoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("Aurthenticate")]
        public usercred Aurthenticate([FromBody] usercred usercred)
        
        {
            usercred user_login = new usercred();
            string userDbConnectionString = "Data Source=JRDOTNETFSECO-2;Initial Catalog=AirlineAccountInfomation;User id=sa;Password=pass@word1;";
            userFetchmentod fetchuser = new userFetchmentod();
            usercred isactiveuser = fetchuser.loginsucccess(usercred.Username, usercred.Password, userDbConnectionString);
            if (usercred.Username == isactiveuser.Username && usercred.Password == isactiveuser.Password)
            {
                var token = aurthenticationManager.Aurthenticate(usercred.Username, usercred.Password);
                isactiveuser.token = token;
                return isactiveuser;
            }
            else { return null; }
            
        }
    }
}
