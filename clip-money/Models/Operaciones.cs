using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Operaciones
    {
        private int id;
        private string numOperacion;
        private string fecha;
        private string hora;
        private decimal monto;
        private Estado estado;
        private TipoOperacion tipoOperacion;
        private string destino;
        private string origen;

        public Operaciones()
        {
        }

        public Operaciones(decimal monto, TipoOperacion tipoOperacion)
        {
            this.Monto = monto;
            this.TipoOperacion = tipoOperacion;
        }

        public Operaciones(string fecha,string hora,TipoOperacion tipoOperacion,decimal monto)
        {
            this.fecha = fecha;
            this.hora = hora;
            this.tipoOperacion = tipoOperacion;
            this.monto = monto;
        }

        public string NumOperacion { get => numOperacion; set => numOperacion = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public int Id { get => id; set => id = value; }
        public TipoOperacion TipoOperacion { get => tipoOperacion; set => tipoOperacion = value; }
        public Estado Estado { get => estado; set => estado = value; }
        public string Destino { get => destino; set => destino = value; }
        public string Origen { get => origen; set => origen = value; }
    }
}