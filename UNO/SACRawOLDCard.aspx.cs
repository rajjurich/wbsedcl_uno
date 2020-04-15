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
    public partial class SACRawOLDCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string UpdateDatabase(string CSNR)
        {
         
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spRawCard", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value ="SACRawCard.aspx";
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = HttpContext.Current.Session["uid"].ToString();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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

      
    }
}