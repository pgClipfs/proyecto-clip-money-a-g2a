namespace Clip_money.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pais
    {
        //Constructor
        public Pais()
        {
            //provincia = new HashSet<Provincia>();
        }

        //Constructor
        public Pais(byte id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
        public byte id { get; set; }
        public string nombre { get; set; }
       // public virtual ICollection<Provincia> provincia { get; set; }

       
    }
}
