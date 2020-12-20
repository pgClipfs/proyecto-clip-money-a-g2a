using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Clip_money.Models;
using System.Net.Http;
using System.Net;

namespace clip_money.Models
{
    public class GestorCliente
    {

        public Cliente obtenerCliente(int id)
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
                    
                    cli = new Cliente(id, nombre, apellido, sexo, idTipoDni, numDni, fotoFrenteDni, fechaNacimiento,  fotoDorsoDni, idLocalidad, domicilio, telefono, email, nombreUsuario, password, cuentaValida,selfie);
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
           
            //Verifico primero que el mail dni o usuario no exista
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

                        //Encriptacion de la contraseña
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


        //VALIDACION POR SEPARADO PARA COMPROBAR SI EXISTE USUARIO, EMAIL, Y DNI
        public bool existeUsuario ( string nombreUsuario)
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

        //----------------------------------------------------------------------------------------------


        //Metodo para obtener el usuario a traves del email
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


        /*METODO PARA INSERTAR LAS FOTOS DEL DNI EN EL CLIENTE Y ACTIVA LA CUENTA*/
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
            string hora = DateTime.Now.ToString("hh:mm");
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

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*
        public void eliminarCliente(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("eliminarCliente", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                comm.ExecuteNonQuery();
            }
        }

        public HttpResponseMessage modificarCliente(Cliente mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            HttpResponseMessage responseMod = new HttpResponseMessage();
            responseMod.StatusCode = HttpStatusCode.OK;
            responseMod.Content = new StringContent("¡El cliente se modificó con éxito!");

            HttpResponseMessage responseErrorMod = new HttpResponseMessage();
            responseErrorMod.StatusCode = HttpStatusCode.BadRequest;
            responseErrorMod.Content = new StringContent("Error, la modificación coincide con un mismo número de DNI, email o nombre de usuario de otro cliente");

            try
            {
                if (existeCliente(mod.NumDni, mod.Email, mod.NombreUsuario) == true)
                {
                    return responseErrorMod;
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(StrConn))
                    {
                        conn.Open();

                        SqlCommand comm = conn.CreateCommand();
                        comm.CommandText = "modificarCliente";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                        comm.Parameters.Add(new SqlParameter("@nombre", mod.Nombre));
                        comm.Parameters.Add(new SqlParameter("@apellido", mod.Apellido));
                        comm.Parameters.Add(new SqlParameter("@sexo", mod.Sexo));
                        comm.Parameters.Add(new SqlParameter("@fechaNacimiento", mod.FechaNacimiento));

                        TipoDni idTipoDni = new TipoDni(mod.IdTipoDni.id);
                        comm.Parameters.Add(new SqlParameter("@idTipoDni", idTipoDni.id));

                        comm.Parameters.Add(new SqlParameter("@numDni", mod.NumDni));
                        //comm.Parameters.Add(new SqlParameter("@foto_frente_dni", mod.FotoFrenteDni));
                        //comm.Parameters.Add(new SqlParameter("@foto_dorso_dni", mod.FotoDorsoDni));

                        Localidad idLocalidad = new Localidad(mod.IdLocalidad.id);
                        comm.Parameters.Add(new SqlParameter("@idLocalidad", idLocalidad.id));

                        comm.Parameters.Add(new SqlParameter("@domicilio", mod.Domicilio));
                        comm.Parameters.Add(new SqlParameter("@telefono", mod.Telefono));
                        comm.Parameters.Add(new SqlParameter("@email", mod.Email));
                        comm.Parameters.Add(new SqlParameter("@nombreUsuario", mod.NombreUsuario));
                        comm.Parameters.Add(new SqlParameter("@password", mod.Password));

                        comm.ExecuteNonQuery();

                        return responseMod;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }*/

        public List<CuentaVirtual> obtenerCuentasDeCliente(int idCliente)
        {
            GestorCuentaVirtual gCuentaVirtual = new GestorCuentaVirtual();
            return gCuentaVirtual.obtenerPorIdCliente(idCliente);
        }


    }
}