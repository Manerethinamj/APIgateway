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

        //check connection string
        //testing - validate connection string
        [HttpGet("/api/helloworld")]
        public string helloworld()
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            sqlcon.Open();
            sqlcon.Close();

            return "hello world";
        }


        //search Airline
        // GET api/<AirlineServiceController>/5
        [HttpGet("/api/v1.0/flight/search")]
        public List<InvrntoryModel> Get(string OnboardingPlace,string DistinationPlace)
        { 
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            
            SqlConnection AirlinetblDbConnection = new SqlConnection(AirlineDbConnectionString);
            var Inv_airlines = new List<InvrntoryModel>();
            AirlineServiceMethod serviceMethod = new AirlineServiceMethod();
            Inv_airlines = serviceMethod.searchAirline(OnboardingPlace, DistinationPlace, DateTime.Now , AirlinetblDbConnection);
            return Inv_airlines;
        }


        //Add inventory
        // POST api/<AirlineServiceController>
        [HttpPost("/api/v1.0/flight/airline/inventory/add")]
        public void Addinvrntory(string AirlineCode, DateTime OnboardingTime, string OnboardingPlace, string DistinationPlace)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod addtoInventoryclass = new AirlineServiceMethod();
            addtoInventoryclass.AddtoinventoryMethod(AirlineCode, OnboardingTime, OnboardingPlace, DistinationPlace, sqlcon);

        }
        
        
        //Airline  Register
        //POST Airline register
        [HttpPost("/api/v1.0/flight/airline/register")]
        public void AddnewAirline([FromBody] AirlineModel airlines)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod addtoInventoryclass = new AirlineServiceMethod();
            addtoInventoryclass.AddtoAirlinereg(airlines, sqlcon);

        }

        //Add Discount
        //discount to be added..
        [HttpPost("/api/v1.0/flight/add/admin/discount")]
        public void AddDiscount(string discountCode , string amount)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod adddis = new AirlineServiceMethod();
            adddis.AddDiscount(discountCode, amount, sqlcon);

        }

        //get inventory
        //get the inventory detail
        [HttpGet("/api/v1.0/flight/airlineinventory")]
        public List<InvrntoryModel> getInvrntory()
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod getinvent = new AirlineServiceMethod();
            List<InvrntoryModel> invrntories = getinvent.getAirlineinvent(sqlcon);
            return invrntories;
        }



        //get airline
        //get the airline
        [HttpGet("/api/v1.0/flight/get/airline")]
        public List<AirlineModel> getAirline()
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            List<AirlineModel> airlineModels = airlineService.getAirlinedetails(sqlcon);
            return airlineModels;
        }


        // get cancelled airline
        // airline cancelation 
        [HttpGet("/api/v1.0/flight/getcancelledairline")]
        public List<AirlineModel> getcancelledAirline()
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            List<AirlineModel> airlineModels = airlineService.getcancelledAirlinedetails(sqlcon);
            return airlineModels;
        }




        //get discount.
        //get the discount
        [HttpGet("/api/v1.0/flight/getdiscount")]
        public List<DiscountModel> getdiscount()
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            List<DiscountModel> airlineModels = airlineService.getDiscount(sqlcon);
            return airlineModels;
        }


        //get discount api
        // discount model.
        [HttpGet("/api/v1.0/getdiscountbycode")]
        public DiscountModel getdiscountbycode(string discountcode)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            DiscountModel airlineModels = airlineService.getDiscountbycode(discountcode,sqlcon);
            return airlineModels;
        }


        //cancel schedule - post menthod
        //post cancel schedule
        [HttpPost("/cancelschedule")]
        public void cancelschedule(int inventoryID)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            airlineService.cancelSchedule(inventoryID,sqlcon);
            
        }

        [HttpPost("/cancelAirline")]
        public void cancelAirline(int AirlineID, string Airlinecode)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            airlineService.Stopairlineservice(AirlineID, Airlinecode, sqlcon);

        }


        [HttpPost("/activateAirline")]
        public void reactivateAirline(int AirlineID)
        {
            string AirlineDbConnectionString = _configuration.GetValue<string>("ConnectionStrings:AirlineConnectionnewdatabase1");
            SqlConnection sqlcon = new SqlConnection(AirlineDbConnectionString);
            AirlineServiceMethod airlineService = new AirlineServiceMethod();
            airlineService.reactivateAirline(AirlineID, sqlcon);

        }

    }
}
