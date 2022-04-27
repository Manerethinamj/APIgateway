using AirlineAPIservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlineAPIservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineServiceController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;

        // GET: api/<AirlineServiceController>
        public AirlineServiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //search Airline
        // GET api/<AirlineServiceController>/5
        [HttpGet("/api/v1.0/flight/search")]
        public List<InvrntoryModel> Get(string OnboardingPlace,string DistinationPlace,DateTime OnboardingDate)
        { 
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnection");
            
            SqlConnection AirlinetblDbConnection = new SqlConnection(AirlineDbConnectionString);
            var Inv_airlines = new List<InvrntoryModel>();
            AirlineServiceMethod serviceMethod = new AirlineServiceMethod();
            Inv_airlines = serviceMethod.searchAirline(OnboardingPlace, DistinationPlace, OnboardingDate, AirlinetblDbConnection);
            return Inv_airlines;
        }


        //Add inventory
        // POST api/<AirlineServiceController>
        [HttpPost("/api/v1.0/flight/airline/inventory/add")]
        public void Addinvrntory(string AirlineCode, DateTime OnboardingTime, string OnboardingPlace, string DistinationPlace)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnection");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod addtoInventoryclass = new AirlineServiceMethod();
            addtoInventoryclass.AddtoinventoryMethod(AirlineCode, OnboardingTime, OnboardingPlace, DistinationPlace, sqlcon);

        }
        
        
        //Airline  Register
        //POST Airline register
        [HttpPost("/api/v1.0/flight/airline/register")]
        public void AddnewAirline(List<AirlineModel> airlines)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnection");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod addtoInventoryclass = new AirlineServiceMethod();
            addtoInventoryclass.AddtoAirlinereg(airlines, sqlcon);

        }


    }
}
