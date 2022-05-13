using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPIservice.Models
{
    public class DiscountModel
    {
        public int Discount_ID { get; set; }
        public DateTime Discount_startDate { get; set; }
        public DateTime Discount_EndDate { get; set; }
        public decimal Discount_Amount { get; set; }
        public string Discount_Code { get; set; }
    }
}
