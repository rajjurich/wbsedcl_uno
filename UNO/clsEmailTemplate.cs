using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace UNO
{
    public class clsEmailTemplate
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public DataSet GetEmailTemplate(string emailCode, string[] values)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_GetEmailBody", conn);
            cmd.Parameters.AddWithValue("@EmailCode", emailCode);
            for (int i = 0; i < values.Length; i++)
            {
                cmd.Parameters.AddWithValue("@value" + (i + 1).ToString(), values[i].ToString());
            }
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds;
        }

        public string fromMail()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd1 = new SqlCommand("select value from ent_params where code='ESS' and identifier='MAILSENDER'", conn);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows[0][0].ToString();
        }


        public void sendMail(string UserMailId, string managerMail_id, string subject, string message, string datefield)
        {
            SqlCommand cmdadd = new SqlCommand("Proc_Mail_Info", conn);
            cmdadd.CommandType = CommandType.StoredProcedure;
            cmdadd.Parameters.AddWithValue("@strCommand", "Insert");
            cmdadd.Parameters.AddWithValue("@frommail", UserMailId);
            cmdadd.Parameters.AddWithValue("@tomail", managerMail_id);
            cmdadd.Parameters.AddWithValue("@subject", subject);
            cmdadd.Parameters.AddWithValue("@contain", message);
            cmdadd.Parameters.AddWithValue("@date", datefield);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmdadd.ExecuteNonQuery();

        }

    }
}