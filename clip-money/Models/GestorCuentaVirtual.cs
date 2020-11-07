using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class GestorCuentaVirtual
    {
        /*
        public void AgregarPersona(Persona nueva)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO Persona(nombre, apellido) VALUES (@Nombre, @Apellido)";
                comm.Parameters.Add(new SqlParameter("@Nombre", nueva.Nombre));
                comm.Parameters.Add(new SqlParameter("@Apellido", nueva.Apellido));

                comm.ExecuteNonQuery();

            }
        }

        public List<Persona> ObtenerPersonas()
        {
            List<Persona> lista = new List<Persona>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "obtener_personas";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();
                    string apellido = dr.GetString(2).Trim();

                    Persona p = new Persona(id, nombre, apellido);
                    lista.Add(p);
                }

                dr.Close();
            }

            return lista;
        }

        public void Eliminar(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("eliminar_persona", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@Id", id));

                comm.ExecuteNonQuery();
            }

        }

        public Persona ObtenerPorId(int id)
        {
            Persona p = null;
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtener_persona", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string nombre = dr.GetString(1);
                    string apellido = dr.GetString(2);

                    p = new Persona(id, nombre, apellido);
                }

                dr.Close();
            }

            return p;

        }

        public void ModificarPersona(Persona p)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "UPDATE Persona SET nombre=@Nombre, apellido=@Apellido WHERE id=@Id";
                comm.Parameters.Add(new SqlParameter("@Nombre", p.Nombre));
                comm.Parameters.Add(new SqlParameter("@Apellido", p.Apellido));
                comm.Parameters.Add(new SqlParameter("@Id", p.Id));

                comm.ExecuteNonQuery();

            }
        }
        */
    }
}