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

        public TipoDni(byte id)
        {
            this.id = id;
        }

        //Constructor
        public TipoDni(byte id, string nombre)
        {
            this.id = id;
            this.nombre_dni = nombre;
        }
        public byte id { get; set; }
        public string nombre_dni { get; set; }
        //public virtual ICollection<cliente> cliente { get; set; }
    }
}
