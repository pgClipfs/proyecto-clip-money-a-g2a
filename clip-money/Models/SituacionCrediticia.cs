namespace Clip_money.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SituacionCrediticia
    {
        //Constructor
        public SituacionCrediticia()
        {
            //cliente = new HashSet<cliente>();
        }

        //Constructor
        public SituacionCrediticia(byte id, string nivel, string descripcion)
        {
            this.id = id;
            this.nivel = nivel;
            this.descripcion = descripcion;
        }

        public byte id { get; set; }
        public string nivel { get; set; }
        public string descripcion { get; set; }
        //public virtual ICollection<cliente> cliente { get; set; }
    }
}
