using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

namespace UNO
{
    public partial class ReadCSNR : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

            AllCSNR();
            getInventoryNIshhued();


        }
      
        public void getInventoryNIshhued()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PROC_GET_ISSUED_CSNR");
                cmd.Connection = conn;
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);
                lstIshued.DataSource = ds.Tables[0];
                lstIshued.DataTextField = "s";
                lstIshued.DataBind();

                lstInventory.DataSource = ds.Tables[1];
                lstInventory.DataTextField = "varcsnr";
                lstInventory.DataBind();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReadCSNR.aspx");
            }


        }
        protected void AllCSNR()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_INSERT_CSNR @strCommand='Select'", conn);
                lblTotal.Text = Convert.ToString(cmd.ExecuteScalar());
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReadCSNR.aspx");
            }
        }
        [WebMethod]
        public static string SaveCSNR(string data)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            string Result = "";
            try
            {
                #region New Code // added by vaibhav

                List<string> mystring = new List<string>();
                mystring.AddRange(data.Split(new char[] { ',' }));
                string[] str = mystring.ToArray();
              
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("<CSNRInventory>");
                for (int i = 0; i < str.Length; i++)
                {

                    sb.Append("<Transaction>");

                    sb.Append("<CSNR>" + str[i].ToString() + "</CSNR>");

                    sb.Append("</Transaction>");

                }
                sb.Append("</CSNRInventory>");
                string s = sb.ToString();

                #endregion  

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                SqlCommand cmd = new SqlCommand("USP_INSERT_CSNR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@strCommand", "Insert");
                cmd.Parameters.AddWithValue("@CSNR", s);
                //cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = "ReadCSNR.aspx";
                //cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = HttpContext.Current.Session["uid"].ToString();
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    Result = "Record Saved Successfully";
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ReadCSNR.aspx");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return Result;

        }
    }
}