using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Transferencia
    {
        private long id_cuenta_virtual;
        private decimal monto;
        private string alias;
        private string cvu;

        public long Id_cuenta_virtual { get => id_cuenta_virtual; set => id_cuenta_virtual = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Cvu { get => cvu; set => cvu = value; }
    }
}