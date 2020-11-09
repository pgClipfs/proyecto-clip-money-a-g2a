using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

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
                comm.CommandText = "SELECT * FROM cliente";
                //comm.CommandText = "obtenerClientes";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();
                    string apellido = dr.GetString(2).Trim();
                    string sexo = dr.GetString(3).Trim();
                    DateTime fechaNacimiento = dr.GetDateTime(4);
                    //TipoDni idTipoDni = dr.GetByte(5);
                    string numDni = dr.GetString(6).Trim();
                    //byte fotoFrenteDni = dr.GetByte(7);
                    //byte fotoDorsoDni = dr.GetByte(8);
                    //Localidad idLocalidad = dr.GetInt32(9);
                    string domicilio = dr.GetString(10).Trim();
                    string telefono = dr.GetString(11).Trim();
                    string email = dr.GetString(12).Trim();
                    //SituacionCrediticia idSituacionCrediticia = dr.GetByte(13);
                    string nombreUsuario = dr.GetString(14).Trim();
                    string password = dr.GetString(15).Trim();

                    Cliente cli = new Cliente(id, nombre, apellido, sexo, fechaNacimiento/*, idTipoDni*/, numDni/*, fotoFrenteDni, fotoDorsoDni*//*, idLocalidad*/, domicilio, telefono, email/*, idSituacionCrediticia*/, nombreUsuario, password);

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

                SqlCommand comm = new SqlCommand("SELECT * FROM cliente WHERE id=@id", conn);
                //SqlCommand comm = new SqlCommand("obtenerCliente", conn);
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string nombre = dr.GetString(1);
                    string apellido = dr.GetString(2);
                    string sexo = dr.GetString(3);
                    DateTime fechaNacimiento = dr.GetDateTime(4);
                    //TipoDni idTipoDni = dr.GetByte(5);
                    string numDni = dr.GetString(6);
                    //byte fotoFrenteDni = dr.GetByte(7);
                    //byte fotoDorsoDni = dr.GetByte(8);
                    //Localidad idLocalidad = dr.GetInt32(9);
                    string domicilio = dr.GetString(10);
                    string telefono = dr.GetString(11);
                    string email = dr.GetString(12);
                    //SituacionCrediticia idSituacionCrediticia = dr.GetByte(13);
                    string nombreUsuario = dr.GetString(14);
                    string password = dr.GetString(15);

                    cli = new Cliente(id, nombre, apellido, sexo, fechaNacimiento/*, idTipoDni*/, numDni/*, fotoFrenteDni, fotoDorsoDni*//*, idLocalidad*/, domicilio, telefono, email/*, idSituacionCrediticia*/, nombreUsuario, password);
                }
                dr.Close();
            }
            return cli;
        }

        public void nuevoCliente(Cliente nuevo)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();

                comm.CommandText = "INSERT INTO cliente(nombre, apellido, sexo, fecha_nacimiento, id_tipo_dni, num_dni, foto_frente_dni, foto_dorso_dni, id_localidad, domicilio, telefono, email, id_situacion_crediticia, nombre_usuario, password) VALUES (@nombre, @apellido, @sexo, @fechaNacimiento, @idTipoDni, @numDni, @fotoFrenteDni, @fotoDorsoDni, @idLocalidad, @domicilio, @telefono, @email, @idSituacionCrediticia, @nombreUsuario, @password)";
                //comm.CommandText = "nuevoCliente";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@nombre", nuevo.Nombre));
                comm.Parameters.Add(new SqlParameter("@apellido", nuevo.Apellido));
                comm.Parameters.Add(new SqlParameter("@sexo", nuevo.Sexo));
                comm.Parameters.Add(new SqlParameter("@fechaNacimiento", nuevo.FechaNacimiento));
                //comm.Parameters.Add(new SqlParameter("@idTipoDni", nuevo.IdTipoDni));
                comm.Parameters.Add(new SqlParameter("@numDni", nuevo.NumDni));
                comm.Parameters.Add(new SqlParameter("@fotoFrenteDni", nuevo.FotoFrenteDni));
                comm.Parameters.Add(new SqlParameter("@fotoDorsoDni", nuevo.FotoDorsoDni));
                //comm.Parameters.Add(new SqlParameter("@idLocalidad", nuevo.IdLocalidad));
                comm.Parameters.Add(new SqlParameter("@domicilio", nuevo.Domicilio));
                comm.Parameters.Add(new SqlParameter("@telefono", nuevo.Telefono));
                comm.Parameters.Add(new SqlParameter("@email", nuevo.Email));
                //comm.Parameters.Add(new SqlParameter("@idSituacionCrediticia", nuevo.IdSituacionCrediticia));
                comm.Parameters.Add(new SqlParameter("@nombreUsuario", nuevo.NombreUsuario));
                comm.Parameters.Add(new SqlParameter("@password", nuevo.Password));

                comm.ExecuteNonQuery();
            }
        }

        public void eliminarCliente(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("DELETE FROM cliente WHERE id=@id", conn);
                //SqlCommand comm = new SqlCommand("eliminarCliente", conn);
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                comm.ExecuteNonQuery();
            }
        }

        public void modificarCliente(Cliente mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "UPDATE cliente SET nombre=@nombre, apellido=@apellido, sexo=@sexo, fechaNacimiento=@fechaNacimiento, idTipoDni=@idTipoDni, numDni=@numDni, fotoFrenteDni=@fotoFrenteDni, fotoDorsoDni=@fotoDorsoDni, idLocalidad=@idLocalidad, domicilio=@domicilio, telefono=@telefono, email=@email, idSituacionCrediticia=@idSituacionCrediticia, nombreUsuario=@nombreUsuario, password=@password WHERE id=@Id";
                //comm.CommandText = "modificarCliente";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                comm.Parameters.Add(new SqlParameter("@nombre", mod.Nombre));
                comm.Parameters.Add(new SqlParameter("@apellido", mod.Apellido));
                comm.Parameters.Add(new SqlParameter("@sexo", mod.Sexo));
                comm.Parameters.Add(new SqlParameter("@fechaNacimiento", mod.FechaNacimiento));
                //comm.Parameters.Add(new SqlParameter("@idTipoDni", mod.IdTipoDni));
                comm.Parameters.Add(new SqlParameter("@numDni", mod.NumDni));
                comm.Parameters.Add(new SqlParameter("@fotoFrenteDni", mod.FotoFrenteDni));
                comm.Parameters.Add(new SqlParameter("@fotoDorsoDni", mod.FotoDorsoDni));
                //comm.Parameters.Add(new SqlParameter("@idLocalidad", mod.IdLocalidad));
                comm.Parameters.Add(new SqlParameter("@domicilio", mod.Domicilio));
                comm.Parameters.Add(new SqlParameter("@telefono", mod.Telefono));
                comm.Parameters.Add(new SqlParameter("@email", mod.Email));
                //comm.Parameters.Add(new SqlParameter("@idSituacionCrediticia", mod.IdSituacionCrediticia));
                comm.Parameters.Add(new SqlParameter("@nombreUsuario", mod.NombreUsuario));
                comm.Parameters.Add(new SqlParameter("@password", mod.Password));

                comm.ExecuteNonQuery();
            }
        }
    }
}