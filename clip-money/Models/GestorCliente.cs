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
        public List<Cliente> obtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "obtenerClientes";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();
                    string apellido = dr.GetString(2).Trim();
                    string sexo = dr.GetString(3).Trim();
                    string fechaNacimiento = dr.GetDateTime(4).Date.ToString("dd-MM-yyyy");
                    byte idTipoDni = dr.GetByte(5);
                    string numDni = dr.GetString(6).Trim();
                    //string fotoFrenteDni = dr.GetString(7).Trim();
                    //string fotoDorsoDni = dr.GetString(8).Trim();
                    int idLocalidad = dr.GetInt32(9);
                    string domicilio = dr.GetString(10).Trim();
                    string telefono = dr.GetString(11).Trim();
                    string email = dr.GetString(12).Trim();
                    byte idSituacionCrediticia = dr.GetByte(13);
                    string nombreUsuario = dr.GetString(14).Trim();
                    string password = dr.GetString(15).Trim();
                    
                    Cliente cli = new Cliente(id, nombre, apellido, sexo, fechaNacimiento, idTipoDni, numDni/*, fotoFrenteDni, fotoDorsoDni*/, idLocalidad, domicilio, telefono, email, idSituacionCrediticia, nombreUsuario, password);

                    lista.Add(cli);
                }
                dr.Close();
            }
            return lista;
        }

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
                    string fechaNacimiento = dr.GetDateTime(4).Date.ToString("dd-MM-yyyy");
                    byte idTipoDni = dr.GetByte(5);
                    string numDni = dr.GetString(6);
                    //string fotoFrenteDni = dr.GetString(7).Trim();
                    //string fotoDorsoDni = dr.GetString(8).Trim();
                    int idLocalidad = dr.GetInt32(9);
                    string domicilio = dr.GetString(10);
                    string telefono = dr.GetString(11);
                    string email = dr.GetString(12);
                    byte idSituacionCrediticia = dr.GetByte(13);
                    string nombreUsuario = dr.GetString(14);
                    string password = dr.GetString(15);

                    cli = new Cliente(id, nombre, apellido, sexo, fechaNacimiento, idTipoDni, numDni/*, fotoFrenteDni, fotoDorsoDni*/, idLocalidad, domicilio, telefono, email, idSituacionCrediticia, nombreUsuario, password);
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

        //Validaciones para que se permitan editar campos puntuales (solo el teléfono, solo el email, solo el email y el teléfono, etc.). Son extras a las validaciones de nombreUsuario, email y numDni.

        public bool existePPL(string pais, string provincia, string localidad)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existePPL", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@pais", pais));
                comm.Parameters.Add(new SqlParameter("@provincia", provincia));
                comm.Parameters.Add(new SqlParameter("@localidad", localidad));

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

        public bool existeDomicilio(string domicilio)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeDomicilio", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@domicilio", domicilio));

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

        public bool existeTelefono(string telefono)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeTelefono", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@telefono", telefono));

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

        public bool existeEmail_Alt(string email_alt)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeEmail_Alt", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", email_alt));

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

            //Verifico que el DNI, Email o Usuario no coincidan con algún cliente ya registrado en la BD
            try
            {
                if (existeDni(mod.NumDni) == true)
                {
                    if (existeDomicilio(mod.Domicilio) == false || existeTelefono(mod.Telefono) || existeEmail_Alt(mod.Email))
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

                    message = 1;
                    return message;
                }
                else if (existeEmail(mod.Email) == true)
                {
                    message = 2;
                    return message;
                }
                else if (existeUsuario(mod.NombreUsuario) == true)
                {
                    message = 3;
                    return message;
                }

                //Si ninguno de los 3 campos coinciden con los de otro cliente registrado en la BD, permito la modificación
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

    }
}