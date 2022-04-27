using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace userAPIreg.Models
{
    public class FetchuserModel
    {
        public string PNR { get; set; }
        public string traveller_name { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string AirlineIDCompany { get; set; }
        public string AirlineCode { get; set; }
        public string email_id { get; set; }
        public string booking_state { get; set; }
        public string OnboardingPlace { get; set; }
        public string DistinationPlace { get; set; }
        public int seat_id { get; set; }
        public DateTime OnboardingTime { get; set; }
        public DateTime DistinationTime { get; set; }
        public string meal_type { get; set; }
    }
}
