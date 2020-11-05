using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Deposito : Operaciones
    {
        private string origen;

        public string Origen { get => origen; set => origen = value; }
    }
}