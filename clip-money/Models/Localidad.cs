namespace Clip_money.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Localidad
    {
        //Constructor
        public Localidad()
        {
            //cliente = new HashSet<cliente>();
        }

        public Localidad(int id)
        {
            this.id = id;
        }

        //Constructor
        public Localidad(int id, string nombre, int idProvincia)
        {
            this.id = id;
            this.nombre = nombre;
            this.id_provincia = idProvincia;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public int id_provincia { get; set; }
        //public virtual ICollection<cliente> cliente { get; set; }
        //public virtual Provincia provincia { get; set; }
		
	
    }
}
