using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Extraccion
    {
        private decimal monto;
        private long id_cuenta_virtual;

        public decimal Monto { get => monto; set => monto = value; }
        public long Id_cuenta_virtual { get => id_cuenta_virtual; set => id_cuenta_virtual = value; }
    }
}