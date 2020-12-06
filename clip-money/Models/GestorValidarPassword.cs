using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class GestorValidarPassword
    {

        public bool validarEmail(EmailValidator email)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            bool result = false;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", email.email));

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }

        public void insertarTokenEmail(string token, string email)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("insertarTokenEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@email", email));
                comm.Parameters.Add(new SqlParameter("@token", token));

                SqlDataReader dr = comm.ExecuteReader();
            }
        }

        public bool validarTokenEmail(NewPasswordValidator np)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            bool result = false;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("validarTokenEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", np.email));
                comm.Parameters.Add(new SqlParameter("@token", np.token));

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }

        public void modificarPassword(NewPasswordValidator np)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("modificarPassword", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@password", np.newpassword));
                comm.Parameters.Add(new SqlParameter("@email", np.email));

                SqlDataReader dr = comm.ExecuteReader();
            }
        }
    }
}