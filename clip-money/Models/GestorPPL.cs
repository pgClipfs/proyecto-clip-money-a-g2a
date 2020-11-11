using Clip_money.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace clip_money.Models
{
    //Gestor integral que contiene los metodos de PAIS,PROVINCIA Y LOCALIDAD
    public class GestorPPL
    {
        //Metodo para obtener la lista completa  de todos los paises
        public List<Pais> obtenerPais()
        {
            List<Pais> lista = new List<Pais>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "pListarPaises";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    byte id = dr.GetByte(0);
                    string nombre = dr.GetString(1).Trim();

                    Pais p = new Pais(id, nombre);
                    lista.Add(p);
                }

                dr.Close();
            }

            return lista;
        }

        //Metodo obtener pais por id
        public Pais ObtenerPaisPorId(int id)
        {
            Pais p = null;
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("pObtenerPais", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    byte idpais = dr.GetByte(0);
                    string nombre = dr.GetString(1);
                   

                    p = new Pais(idpais, nombre);
                }

                dr.Close();
            }

            return p;

        }

        //Metodo para obtener la lista completa de provincias de todos los paises
        public List<Provincia> obtenerProvincias()
        {
            List<Provincia> lista = new List<Provincia>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "pListarProvinciasTodas";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string nombre = dr.GetString(1).Trim();

                        Provincia p = new Provincia(id, nombre);
                        lista.Add(p);
                    }
                }

                dr.Close();
            }

            return lista;
        }

        //Metodo para obtener la lista completa de provincias por pais
        public List<Provincia> obtenerProvincia(int idpais)
        {
            

            List<Provincia> lista = new List<Provincia>();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("pListarProvincias", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@idpais", idpais));

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();

                    Provincia prov = new Provincia(id, nombre);
                    lista.Add(prov);
                }

                dr.Close();
            }

            return lista;

        }

        //Metodo para objeter todas las localidades de todas las provincias
        public List<Localidad> obtenerLocalidades()
        {
            List<Localidad> lista = new List<Localidad>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "pListarLocalidadesTodas";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string nombre = dr.GetString(1).Trim();

                        Localidad l = new Localidad(id, nombre);
                        lista.Add(l);
                    }
                }

                dr.Close();
            }

            return lista;
        }

        //Metodo para listar todas las localidades de una provincia
        public List<Localidad> obtenerLocalidad(int idprovincia)
        {
            
            List<Localidad> lista = new List<Localidad>();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("pListarLocalidades", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@idprovincia", idprovincia));

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();

                    Localidad l = new Localidad(id, nombre);
                    lista.Add(l);
                }

                dr.Close();
            }

            return lista;

        }
    }
}