using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPIservice.Models
{
    public class InvrntoryModel
    {
        public int AirlineID { get; set; }
        public string AirlineIDCompany { get; set; }
        public string AirlineCode { get; set; }
        public string AirplanType { get; set; }
        public decimal AirplanECOFare { get; set; }
        public int MaxSeat { get; set; }
        public DateTime OnboardingTime { get; set; }
        public DateTime DistinationTime { get; set; }
        public string OnboardingPlace { get; set; }
        public string DistinationPlace { get; set; }
        public int Invent_ID { get; set; }
    }
}
