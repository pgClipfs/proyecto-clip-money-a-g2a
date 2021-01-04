using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Clip_money.Models;
using System.Net.Http;
using System.Net;
using System.Net.Mail;

namespace clip_money.Models
{
    public class GestorCliente
    {
   
        public Cliente obtenerPorId(int id)

        {
            Cliente cli = null;
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerCliente", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {

                    string nombre = dr.GetString(1);
                    string apellido = dr.GetString(2);
                    string sexo = dr.GetString(3);
                    int idTipoDni = dr.GetInt32(4);
                    string numDni = dr.GetString(5);
                    string fotoFrenteDni = dr.GetString(6);
                    string fechaNacimiento = dr.GetDateTime(7).Date.ToString("dd-MM-yyyy");
                    string fotoDorsoDni = dr.GetString(8);
                    int idLocalidad = dr.GetInt32(9);
                    string domicilio = dr.GetString(10);
                    string telefono = dr.GetString(11);
                    string email = dr.GetString(12);
                    string nombreUsuario = dr.GetString(14);
                    string password = dr.GetString(15);
                    int cuentaValida = dr.GetInt32(17);
                    string selfie = dr.GetString(18);
                    
                    cli = new Cliente(id, nombre, apellido, sexo, idTipoDni, numDni, fotoFrenteDni, fechaNacimiento,  fotoDorsoDni, idLocalidad, domicilio, telefono, email, nombreUsuario, password, cuentaValida, selfie);
                   
                }
                dr.Close();
            }
            return cli;
        }

        public int nuevoCliente(Cliente nuevo)
        {
            GestorValidarPassword gvPassword = new GestorValidarPassword();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            int message = 0;


            //Verifico que el mail, dni o usuario no exista en la BD

            try
            {
                if (existeDni(nuevo.NumDni) == true)
                {
                    message = 1;
                    return message;
                }
                else if (existeEmail(nuevo.Email) == true)
                {
                    message = 2;
                    return message;
                }
                else if (existeUsuario(nuevo.NombreUsuario) == true)
                {
                    message = 3;
                    return message;
                }

                //Si no existe, agrego el cliente
                else
                {
                    using (SqlConnection conn = new SqlConnection(StrConn))
                    {
                        conn.Open();

                        SqlCommand comm = conn.CreateCommand();

                        //Encriptación de la contraseña
                        string encriptedpassword = gvPassword.GetSha256(nuevo.Password);

                        comm.CommandText = "nuevoCliente";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@nombre", nuevo.Nombre));
                        comm.Parameters.Add(new SqlParameter("@apellido", nuevo.Apellido));
                        comm.Parameters.Add(new SqlParameter("@sexo", nuevo.Sexo));
                        comm.Parameters.Add(new SqlParameter("@fecha_nacimiento", nuevo.FechaNacimiento));
                        comm.Parameters.Add(new SqlParameter("@id_tipo_dni", nuevo.IdTipoDni));
                        comm.Parameters.Add(new SqlParameter("@num_dni", nuevo.NumDni));

                        //comm.Parameters.Add(new SqlParameter("@foto_frente_dni", nuevo.FotoFrenteDni));
                        //comm.Parameters.Add(new SqlParameter("@foto_dorso_dni", nuevo.FotoDorsoDni));

                        comm.Parameters.Add(new SqlParameter("@id_localidad", nuevo.IdLocalidad));
                        comm.Parameters.Add(new SqlParameter("@domicilio", nuevo.Domicilio));
                        comm.Parameters.Add(new SqlParameter("@telefono", nuevo.Telefono));
                        comm.Parameters.Add(new SqlParameter("@email", nuevo.Email));
                        comm.Parameters.Add(new SqlParameter("@nombre_usuario", nuevo.NombreUsuario));
                        comm.Parameters.Add(new SqlParameter("@password", encriptedpassword));

                        comm.ExecuteNonQuery();

                        return message;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //VALIDACIONES PARA COMPROBAR SI EXISTEN USUARIOS CON EL MISMO: Nombre de usuario, Email o Número de DNI
        public bool existeUsuario (string nombreUsuario)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeUsuario", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool existeEmail(string email)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", email));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool existeDni(string numDni)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeDni", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@numDni", numDni));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Validación que busca el email filtrando por id. Si el email y el id coinciden, se permite editarlo ya que se excluye (no se muestra este registro) ese campo de la búsqueda. Si el email coincide pero el id no, se detecta un registro, entonces no se permite la edición.

        public bool existeEmail_Alt(string email_Alt, int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();


            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeEmail_Alt", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email_Alt", email_Alt));
                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Validación que busca el usuario filtrando por id. Si el usuario y el id coinciden, se permite editarlo ya que se excluye (no se muestra este registro) ese campo de la búsqueda. Si el usuario coincide pero el id no, se detecta un registro, entonces no se permite la edición.

        public bool existeUsuario_Alt(string usuario_Alt, int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeUsuario_Alt", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@nombreUsuario_Alt", usuario_Alt));
                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Método para obtener el usuario a través del email
        public string obtenerUsuario(string email)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerUsuario", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", email));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string nom_usuario;
                    nom_usuario = dr.GetString(0).Trim();
                    
                    return nom_usuario;
                }
                else
                {
                    return "DefaultUser";
                }
            }
        }

        /*MÉTODO PARA INSERTAR LAS FOTOS DEL DNI EN EL CLIENTE Y ACTIVAR LA CUENTA*/
        public bool insertarFotosDni(Cliente fotoDni)
        {
            CuentaVirtual cv = null;
            Operaciones op = null;
            GestorCuentaVirtual Gcv = new GestorCuentaVirtual();
            GestorOperaciones Gop = new GestorOperaciones();
            string cvu;
            string nroCuenta;
            long idcuenta;
            TipoCuentaVirtual tipoCuentaVirtual = null;
            string nroOperacion;
            string hora = DateTime.Now.ToString("HH:mm");
            string fecha = DateTime.Today.ToString("dd-MM-yyyy");
            TipoOperacion idtipoOperacion = null;
            Estado estado = null;


            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("insertarFotosDni", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@id", fotoDni.Id));
                comm.Parameters.Add(new SqlParameter("@selfieCliente", fotoDni.SelfieCliente));
                comm.Parameters.Add(new SqlParameter("@frenteDni", fotoDni.FotoFrenteDni));
                comm.Parameters.Add(new SqlParameter("@dorsoDni", fotoDni.FotoDorsoDni));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                {

                    //GENERAMOS LA CUENTA ALEATORIA
                    nroCuenta = Gcv.generarCuentaAleatoria();
                    cvu = Gcv.generarcvuAleatorio();

                    while (Gcv.existeCuentaCvu(nroCuenta, cvu))
                    {
                        nroCuenta = Gcv.generarCuentaAleatoria();
                        cvu = Gcv.generarcvuAleatorio();
                    }

                    tipoCuentaVirtual = new TipoCuentaVirtual(1);
                    cv = new CuentaVirtual(0, "ALIAS-" + nroCuenta, cvu, nroCuenta, 0, fotoDni.Id, tipoCuentaVirtual, 1);

                    Gcv.nuevaCuentaVirtual(cv);
                    //-------------------------------------

                    //GENERAMOS UNA OPERACION DE APERTURA A LA CUENTA CREADA
                    idcuenta = Gcv.obtenerPorNroCuenta(nroCuenta);
                    //generamos el numero de operacion aleatorio
                    nroOperacion = Gcv.generarNroOperacionAleatorio();

                    while(Gcv.existeNroOperacion(idcuenta,nroOperacion))
                    {
                      nroOperacion = Gcv.generarNroOperacionAleatorio();
                    }

                    idtipoOperacion = new TipoOperacion(5);
                    estado = new Estado(3);
                    op = new Operaciones(nroOperacion,fecha,hora, 0, "sin destino", idtipoOperacion, estado, idcuenta);

                    Gop.insertarOperacion(op);

                    //ENVIAMOS EL MAIL DE CONFIRMACION
                    enviarEmailCuenta(fotoDni.Email, nroCuenta, cvu);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void eliminarCliente(int id)
        {
            //string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            //using (SqlConnection conn = new SqlConnection(StrConn))
            //{
            //    conn.Open();

            //    SqlCommand comm = new SqlCommand("eliminarCliente", conn);
            //    comm.CommandType = System.Data.CommandType.StoredProcedure;

            //    comm.Parameters.Add(new SqlParameter("@id", id));

            //    comm.ExecuteNonQuery();
            //}
        }

        public int modificarCliente(Cliente mod)
        {
            GestorValidarPassword gvPassword = new GestorValidarPassword();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            int message = 0;

            //Verifico que el Email o Usuario no coincida con algún cliente ya registrado en la BD
            try
            {
                if (existeEmail_Alt(mod.Email, mod.Id) == true)
                {
                    message = 2;
                    return message;
                }
                else if (existeUsuario_Alt(mod.NombreUsuario, mod.Id) == true)
                {
                    message = 3;
                    return message;
                }

                //Si ninguno de los 2 campos coinciden con los de otro cliente registrado en la BD, permito la modificación
                else
                {
                    using (SqlConnection conn = new SqlConnection(StrConn))
                    {
                        conn.Open();

                        SqlCommand comm = conn.CreateCommand();

                        //Encriptacion de la contraseña
                        string encriptedpassword = gvPassword.GetSha256(mod.Password);

                        comm.CommandText = "modificarCliente";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                        comm.Parameters.Add(new SqlParameter("@nombre", mod.Nombre));
                        comm.Parameters.Add(new SqlParameter("@apellido", mod.Apellido));
                        comm.Parameters.Add(new SqlParameter("@sexo", mod.Sexo));
                        comm.Parameters.Add(new SqlParameter("@fechaNacimiento", mod.FechaNacimiento));
                        comm.Parameters.Add(new SqlParameter("@idTipoDni", mod.IdTipoDni));
                        comm.Parameters.Add(new SqlParameter("@numDni", mod.NumDni));

                        //comm.Parameters.Add(new SqlParameter("@foto_frente_dni", mod.FotoFrenteDni));
                        //comm.Parameters.Add(new SqlParameter("@foto_dorso_dni", mod.FotoDorsoDni));

                        comm.Parameters.Add(new SqlParameter("@idLocalidad", mod.IdLocalidad));
                        comm.Parameters.Add(new SqlParameter("@domicilio", mod.Domicilio));
                        comm.Parameters.Add(new SqlParameter("@telefono", mod.Telefono));
                        comm.Parameters.Add(new SqlParameter("@email", mod.Email));
                        comm.Parameters.Add(new SqlParameter("@nombreUsuario", mod.NombreUsuario));
                        comm.Parameters.Add(new SqlParameter("@password", encriptedpassword));

                        comm.ExecuteNonQuery();

                        return message;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int modificarCliente_Alt(Cliente mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            int message = 0;

            //Verifico que el Email o Usuario no coincida con algún cliente ya registrado en la BD
            try
            {
                if (existeEmail_Alt(mod.Email, mod.Id) == true)
                {
                    message = 2;
                    return message;
                }
                else if (existeUsuario_Alt(mod.NombreUsuario, mod.Id) == true)
                {
                    message = 3;
                    return message;
                }

                //Si ninguno de los 2 campos coinciden con los de otro cliente registrado en la BD, permito la modificación
                else
                {
                    using (SqlConnection conn = new SqlConnection(StrConn))
                    {
                        conn.Open();

                        SqlCommand comm = conn.CreateCommand();

                        comm.CommandText = "modificarCliente_Alt";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                        comm.Parameters.Add(new SqlParameter("@fechaNacimiento", mod.FechaNacimiento));
                        comm.Parameters.Add(new SqlParameter("@idLocalidad", mod.IdLocalidad));
                        comm.Parameters.Add(new SqlParameter("@domicilio", mod.Domicilio));
                        comm.Parameters.Add(new SqlParameter("@telefono", mod.Telefono));
                        comm.Parameters.Add(new SqlParameter("@email", mod.Email));
                        comm.Parameters.Add(new SqlParameter("@nombreUsuario", mod.NombreUsuario));

                        comm.ExecuteNonQuery();

                        return message;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CuentaVirtual> obtenerCuentasDeCliente(int idCliente)
        {
            GestorCuentaVirtual gCuentaVirtual = new GestorCuentaVirtual();
            return gCuentaVirtual.obtenerPorIdCliente(idCliente);
        }

        //METODO PARA ENVIAR EMAIL DE ACTIVACION DE LA CUENTA
        public void enviarEmailCuenta(string emailDestino, string nrocuenta, string cvu)
        {
            string emailOrigen = "clip.money.cba@gmail.com";
            string contraseña = "clipmoney2020";

            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, "Activacion Cuenta",
                "<p>Enhorabuena, has activado tu cuenta satisfactoriamente.<p><br>" +
                "<p>Ya posees todos los beneficios de Clip Money. A disfrutar!<p><br>" +
                "<p>Los siguentes datos son: <p><br>" +
                "<p>Cuenta: <p>" + nrocuenta + "<br>" +
                "<p> CVU: <p>" + cvu); ;

            oMailMessage.IsBodyHtml = true;

            SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");
            osmtpClient.EnableSsl = true;
            osmtpClient.UseDefaultCredentials = false;
            osmtpClient.Port = 587;
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contraseña);

            osmtpClient.Send(oMailMessage);
        }

    }
}
