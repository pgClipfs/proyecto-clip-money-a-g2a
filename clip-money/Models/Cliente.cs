using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string apellido;
        private string sexo;
        private DateTime fechaNacimiento;
        //private TipoDni idTipoDni;
        private string numDni;
        private byte[] fotoFrenteDni;
        private byte[] fotoDorsoDni;
        //private Localidad idLocalidad;
        private string domicilio;
        private string telefono;
        private string email;
        //private SituacionCrediticia idSituacionCrediticia;
        private string nombreUsuario;
        private string password;

        public Cliente()
        {

        }

        public Cliente(int id, string nombre, string apellido, string sexo, DateTime fechaNacimiento/*, TipoDni idTipoDni*/, string numDni/*, byte[] fotoFrenteDni, byte[] fotoDorsoDni*//*, Localidad idLocalidad*/, string domicilio, string telefono, string email/*, SituacionCrediticia idSituacionCrediticia*/, string nombreUsuario, string password)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.sexo = sexo;
            this.fechaNacimiento = fechaNacimiento;
            //this.idTipoDni = idTipoDni;
            this.numDni = numDni;
            //this.idLocalidad = idLocalidad;
            this.domicilio = domicilio;
            this.telefono = telefono;
            this.email = email;
            //this.idSituacionCrediticia = idSituacionCrediticia;
            this.nombreUsuario = nombreUsuario;
            this.password = password;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        //public TipoDni IdTipoDni { get => idTipoDni; set => idTipoDni = value; }
        public string NumDni { get => numDni; set => numDni = value; }
        public byte[] FotoFrenteDni { get => fotoFrenteDni; set => fotoFrenteDni = value; }
        public byte[] FotoDorsoDni { get => fotoDorsoDni; set => fotoDorsoDni = value; }
        //public Localidad IdLocalidad { get => idLocalidad; set => idLocalidad = value; }
        public string Domicilio { get => domicilio; set => domicilio = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        //public SituacionCrediticia IdSituacionCrediticia { get => idSituacionCrediticia; set => idSituacionCrediticia = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Password { get => password; set => password = value; }

        public void obtenerEdad()
        {

        }

        public void obtenerTipoDni()
        {

        }

        public void obtenerLocalidad()
        {

        }

        public void obtenerSituacionCrediticia()
        {

        }
    }
}