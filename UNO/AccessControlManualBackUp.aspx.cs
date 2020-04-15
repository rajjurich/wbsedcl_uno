using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using CMS.UNO.Core.Handler;

namespace UNO
{
    public partial class AccessControlManualBackUp : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        static string _strpath = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
        }
        

        
        
        
        
        
        
        private void WriteLog(string error, string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                StreamWriter SW = new StreamWriter(path + "\\Log.txt", true);
                SW.WriteLine(error);
                SW.Close();
            }
            catch (Exception EX)
            {

            }
        }
        [System.Web.Services.WebMethod]
        public static string AccessControlArchival(DateTime TillDate)
        {
            //System.Threading.Thread.Sleep(100000);
            return "a";
        }
        
        


    }
}