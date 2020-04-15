

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;


namespace UNO
{
    public partial class VEH_RFID_Inventory_New : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>start();</script>");
        }
        [WebMethod()]
        public static void GetData(string data)
        {
            string d = data;
            
        }
        [WebMethod()]
        public static string SaveDate(string data)
        {
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                //JavaScriptSerializer json = new JavaScriptSerializer();
                //List<string> mystring = json.Deserialize<List<string>>(data);
                List<string> mystring = new List<string>();
                mystring.AddRange(data.Split(new char[] { ',' }));
                string[] str = mystring.ToArray();
               // var lst = Request.Form["ContentPlaceHolder1_ContentPlaceHolder1_LstRFIDList"];
                #region New Code
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("<RFIDInventory>");
                for (int i = 0; i < str.Length; i++)
                {

                    sb.Append("<Transaction>");

                    sb.Append("<RFID>" + str[i].ToString() + "</RFID>");

                    sb.Append("</Transaction>");

                }
                sb.Append("</RFIDInventory>");
                string s = sb.ToString();

                if (conn1.State == ConnectionState.Closed)
                {
                    conn1.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_InsertRFIDInventory", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = sb.ToString();
                cmd.ExecuteNonQuery();
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
                #endregion


            }
            catch (Exception ex)
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
           
            return "succses";
        }
    }
}