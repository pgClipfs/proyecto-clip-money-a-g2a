using Clip_money.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    //Gestor para los metodos de tipo_dni y situacion crediticia
    public class GestorDniSitCrediticia
    {
        //Metodo para obtener la lista completa  de todos los tipos de dni
        public List<TipoDni> obtenerTipoDni()
        {
            List<TipoDni> lista = new List<TipoDni>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "pListarTipoDni";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();

                    TipoDni td = new TipoDni(id, nombre);
                    lista.Add(td);
                }

                dr.Close();
            }

            return lista;
        }

        //Metodo para obtener la lista completa  de todos los tipos de situacion crediticia
        public List<SituacionCrediticia> obtenerSitCrediticia()
        {
            List<SituacionCrediticia> lista = new List<SituacionCrediticia>();
            
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "pListarSituacionCrediticia";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    byte id = dr.GetByte(0);
                    string nombre = dr.GetString(1).Trim();
                    string descripcion = dr.GetString(2).Trim();

                    SituacionCrediticia sc = new SituacionCrediticia(id, nombre, descripcion);
                    lista.Add(sc);
                }

                dr.Close();
            }

            return lista;
        }
    }
}