using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class TipoOperacion
    {
        private long id;
        private string nombre;
        private string descripcion;

        public TipoOperacion(string nombre)
        {
            this.Nombre = nombre;
        }

        public TipoOperacion(string nombre, string descripcion) : this(nombre)
        {
            this.Descripcion = descripcion;
        }

        public TipoOperacion(long id)
        {
            this.Id = id;
        }

        public long Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}