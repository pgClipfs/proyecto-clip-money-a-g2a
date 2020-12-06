using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class NewPasswordValidator
    {

        public string email { get; set; }
        public string token { get; set; }
        public string newpassword { get; set; }
    }
}