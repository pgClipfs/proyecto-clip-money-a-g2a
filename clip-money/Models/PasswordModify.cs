using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class PasswordModify
    {
        public long idCliente { get; set; }
        public string passwordActual { get; set; }
        public string passwordNueva { get; set; }
    }
}