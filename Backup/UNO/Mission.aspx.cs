using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Management;



namespace UNO
{
    public partial class Mission1 : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

   

        protected void Page_Load(object sender, EventArgs e)
        {
           
            BindVisionMission();
            

        }
       
        private void BindVisionMission()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("select * from dbo.CMS_VISION_MISSION", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    vision.InnerText = Convert.ToString(dt.Rows[0]["VISION"]);
                    Mission.InnerHtml = Convert.ToString(dt.Rows[0]["MISSION"]);
                }
             
            }
            catch (Exception ex)
            {
            }

        }
       

    }

}