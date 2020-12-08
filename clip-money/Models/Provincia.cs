namespace Clip_money.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Provincia
    {
        //contructor
        public Provincia()
        {
            //localidad = new HashSet<Localidad>();
        }

        //contructor
        public Provincia(int id, string nombre, int idPais)
        {
            this.id = id;
            this.nombre = nombre;
            this.id_pais = idPais;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        
        public int id_pais { get; set; }
        //public virtual ICollection<Localidad> localidad { get; set; }
        //public virtual Pais pais { get; set; }
		
		
    }
}
