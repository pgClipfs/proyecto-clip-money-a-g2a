namespace Clip_money.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoDni
    {
        //Constructor
        public TipoDni()
        {
            //this.cliente = new HashSet<cliente>();
        }

        public TipoDni(int id)
        {
            this.id = id;
        }

        //Constructor
        public TipoDni(int id, string nombre)
        {
            this.id = id;
            this.nombre_dni = nombre;
        }
        public int id { get; set; }
        public string nombre_dni { get; set; }
        //public virtual ICollection<cliente> cliente { get; set; }
    }
}
