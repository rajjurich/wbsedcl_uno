using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UNO
{
    public partial class VisitorRawCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string UpdateDatabase(string CSNR,string EMPCODE)
        {
         
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spRawCardNew", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                cmd.Parameters.Add("@EMPCODE", SqlDbType.VarChar).Value = EMPCODE;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "True";
            }
            catch (Exception ex)
            {
                return "False";
            }
            
        }

        [WebMethod]
        public static string GetMasterKey()
        {
            try
            {
                string MasterKey = HttpContext.Current.Session["MasterKey"].ToString();
                return MasterKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        public static string GetDOSKey()
        {
            try
            {
                string DosKey = HttpContext.Current.Session["DosKey"].ToString();
                return DosKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public static string GenerateKey(string CSNR, string EMPCODE)
        {
            string KeyA = "121212121212";
            string KeyB = "343434343434";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spGetISROKeys", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EMPCODE;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                KeyA = dt.Rows[0][0].ToString();
                KeyB = dt.Rows[1][0].ToString();
                //if (!OnlyHexInString(KeyA))
                //{
                //    lblError.Text = "Only Hex allowed in Key A";
                //    txtKeyA.Focus();
                //}
                //else if (!OnlyHexInString(CSNR))
                //{
                //    lblError.Text = "Only Hex allowed in CSN";
                //    txtCSNR.Focus();
                //}
                //else
                //{
                KeyA = KeyA.PadLeft(12, '0');
                KeyB = KeyB.PadLeft(12, '0');
                CSNR = CSNR.PadLeft(0x10, '0');
                //this.txtKeyA1.Text = TripleDESImplementation.ReverseKey(this.txtKeyA.Text, this.txtCSN.Text);
                //byte[] buffer = TripleDESImplementation.HEXToByteArray(this.txtCSN.Text);
                return TripleDESImplementation.EncryptKey(KeyA, CSNR) + "," + TripleDESImplementation.EncryptKey(KeyB, CSNR);
                //}
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "";
            }

        }

        [WebMethod]
        public static string GenerateKeyWithoutEmployeeType(string CSNR)
        {
            string KeyA = "121212121212";
            string KeyB = "343434343434";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spGetISROKeysWithoutType", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                KeyA = dt.Rows[0][0].ToString();
                KeyB = dt.Rows[1][0].ToString();
                KeyA = KeyA.PadLeft(12, '0');
                KeyB = KeyB.PadLeft(12, '0');
                CSNR = CSNR.PadLeft(0x10, '0');
                //this.txtKeyA1.Text = TripleDESImplementation.ReverseKey(this.txtKeyA.Text, this.txtCSN.Text);
                //byte[] buffer = TripleDESImplementation.HEXToByteArray(this.txtCSN.Text);
                return TripleDESImplementation.EncryptKey(KeyA, CSNR) + "," + TripleDESImplementation.EncryptKey(KeyB, CSNR);
                //}
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "";
            }

        }
        [WebMethod]
        public static string CheckCSNR(string CSNR)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spChekVisitorCSNR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count > 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }

        
    }
}