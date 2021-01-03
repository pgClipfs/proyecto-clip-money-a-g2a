using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Deposito
    {
        private long id_cuenta_virtual;
        private decimal monto;

        public long Id_cuenta_virtual { get => id_cuenta_virtual; set => id_cuenta_virtual = value; }
        public decimal Monto { get => monto; set => monto = value; }
    }
}