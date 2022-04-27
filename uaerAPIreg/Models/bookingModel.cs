using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace userAPIreg.Models
{
    public class bookingModel
    {
        public int Booking_ID { get; set; }
        public int user_ID { get; set; }
        public string PNR { get; set; }
        public string Booking_Status { get; set; }
        public string OnboardingPlace { get; set; }
        public string DistinationPlace { get; set; }
        public DateTime OnboardingTime { get; set; }
        public DateTime DistinationTime { get; set; }
        public int MealID { get; set; }
        public int AirlineID { get; set; }
        public int SeatBooked { get; set; }
        public int totalavaliable { get; set; }
    }
}
