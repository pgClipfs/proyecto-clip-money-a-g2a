using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Transferencia : Operaciones
    {
        private string destino;

        public string Destino { get => destino; set => destino = value; }
    }
}