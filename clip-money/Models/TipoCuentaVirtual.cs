using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class TipoCuentaVirtual
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string moneda;

        public TipoCuentaVirtual()
        {

        }

        public TipoCuentaVirtual(string nombre)
        {
            this.nombre = nombre;
        }

        public TipoCuentaVirtual(int id)
        {
            this.Id = id;
        }


        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Moneda { get => moneda; set => moneda = value; }
    }
}