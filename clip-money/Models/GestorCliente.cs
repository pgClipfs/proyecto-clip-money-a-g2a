﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Clip_money.Models;

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
                    TipoDni idTipoDni = new TipoDni((byte)id);
                    string numDni = dr.GetString(6).Trim();
                    //byte fotoFrenteDni = dr.GetByte(7);
                    //byte fotoDorsoDni = dr.GetByte(8);
                    Localidad idLocalidad = new Localidad(id);
                    string domicilio = dr.GetString(10).Trim();
                    string telefono = dr.GetString(11).Trim();
                    string email = dr.GetString(12).Trim();
                    SituacionCrediticia idSituacionCrediticia = new SituacionCrediticia();
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
                    TipoDni idTipoDni = new TipoDni((byte)id);
                    string numDni = dr.GetString(6);
                    //byte fotoFrenteDni = dr.GetByte(7);
                    //byte fotoDorsoDni = dr.GetByte(8);
                    Localidad idLocalidad = new Localidad(id);
                    string domicilio = dr.GetString(10);
                    string telefono = dr.GetString(11);
                    string email = dr.GetString(12);
                    SituacionCrediticia idSituacionCrediticia = new SituacionCrediticia();
                    string nombreUsuario = dr.GetString(14);
                    string password = dr.GetString(15);

                    cli = new Cliente(id, nombre, apellido, sexo, fechaNacimiento, idTipoDni, numDni/*, fotoFrenteDni, fotoDorsoDni*/, idLocalidad, domicilio, telefono, email, idSituacionCrediticia, nombreUsuario, password);
                }
                dr.Close();
            }
            return cli;
        }

        public void nuevoCliente(Cliente nuevo)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            //if (nuevo.)
            //{
                
            //}

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();

                comm.CommandText = "nuevoCliente";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@nombre", nuevo.Nombre));
                comm.Parameters.Add(new SqlParameter("@apellido", nuevo.Apellido));
                comm.Parameters.Add(new SqlParameter("@sexo", nuevo.Sexo));
                comm.Parameters.Add(new SqlParameter("@fecha_nacimiento", nuevo.FechaNacimiento));

                TipoDni idTipoDni = new TipoDni(nuevo.IdTipoDni.id);
                comm.Parameters.Add(new SqlParameter("@id_tipo_dni", idTipoDni.id));

                comm.Parameters.Add(new SqlParameter("@num_dni", nuevo.NumDni));
                //comm.Parameters.Add(new SqlParameter("@foto_frente_dni", nuevo.FotoFrenteDni));
                //comm.Parameters.Add(new SqlParameter("@foto_dorso_dni", nuevo.FotoDorsoDni));

                Localidad idLocalidad = new Localidad(nuevo.IdLocalidad.id);
                comm.Parameters.Add(new SqlParameter("@id_localidad", idLocalidad.id));

                comm.Parameters.Add(new SqlParameter("@domicilio", nuevo.Domicilio));
                comm.Parameters.Add(new SqlParameter("@telefono", nuevo.Telefono));
                comm.Parameters.Add(new SqlParameter("@email", nuevo.Email));
                comm.Parameters.Add(new SqlParameter("@nombre_usuario", nuevo.NombreUsuario));
                comm.Parameters.Add(new SqlParameter("@password", nuevo.Password));

                comm.ExecuteNonQuery();
            }
        }

        public bool existeCliente(string numDni, string email, string nombreUsuario)
        {
            Cliente nuevoCliente = new Cliente();

            if (nuevoCliente.NumDni == numDni || nuevoCliente.Email == email || nuevoCliente.NombreUsuario == nombreUsuario)
            {
                return false;
            }

            else
                return true;
        }

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

        public void modificarCliente(Cliente mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

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
                comm.Parameters.Add(new SqlParameter("@fecha_nacimiento", mod.FechaNacimiento));

                TipoDni idTipoDni = new TipoDni(mod.IdTipoDni.id);
                comm.Parameters.Add(new SqlParameter("@id_tipo_dni", idTipoDni.id));

                comm.Parameters.Add(new SqlParameter("@num_dni", mod.NumDni));
                //comm.Parameters.Add(new SqlParameter("@foto_frente_dni", mod.FotoFrenteDni));
                //comm.Parameters.Add(new SqlParameter("@foto_dorso_dni", mod.FotoDorsoDni));

                Localidad idLocalidad = new Localidad(mod.IdLocalidad.id);
                comm.Parameters.Add(new SqlParameter("@id_localidad", idLocalidad.id));

                comm.Parameters.Add(new SqlParameter("@domicilio", mod.Domicilio));
                comm.Parameters.Add(new SqlParameter("@telefono", mod.Telefono));
                comm.Parameters.Add(new SqlParameter("@email", mod.Email));
                comm.Parameters.Add(new SqlParameter("@nombre_usuario", mod.NombreUsuario));
                comm.Parameters.Add(new SqlParameter("@password", mod.Password));

                comm.ExecuteNonQuery();
            }
        }
    }
}