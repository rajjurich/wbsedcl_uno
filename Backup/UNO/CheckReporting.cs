using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace UNO
{
    public class CheckReporting
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static bool IsManager(string userid)
        {
            bool Flag = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_GetManager", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpCode", userid).Value = userid;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    Flag = true;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return Flag;

        }
    }
}