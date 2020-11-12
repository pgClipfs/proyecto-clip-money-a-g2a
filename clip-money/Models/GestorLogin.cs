using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class GestorLogin
    {
        public bool validarLogin(LoginRequest loginRequest)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            bool result = false;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerLogin", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@usuario", loginRequest.NombreUsuario));
                comm.Parameters.Add(new SqlParameter("@password", loginRequest.Password));

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}