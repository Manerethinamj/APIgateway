using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPIservice.Models
{
    public class AirlineModel
    {
        public int AirlineID { get; set; }
        public string AirlineIDCompany { get; set; }
        public string AirlineCode { get; set; }
        public string AirplanType { get; set; }
        public decimal AirplanBusFare { get; set; }
        public decimal AirplanECOFare { get; set; }
        public int MaxSeat { get; set; }
        public DateTime AirplanStartTime { get; set; }
        public DateTime AirplanEndTime { get; set; }
        public int AIRPORTID { get; set; }
        public string AIRPORTIState { get; set; }
    }
}
