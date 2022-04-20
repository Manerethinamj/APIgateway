using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace userAPIreg.Models
{
    public class userModel
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string email_id { get; set; }
        public string password { get;set; }
        public int role_id { get; set; }
    }
}
