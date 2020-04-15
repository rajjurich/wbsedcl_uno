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



namespace UNO
{
    public partial class Aboutus : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

   

        protected void Page_Load(object sender, EventArgs e)
        {

            BindModules();
            

        }

        private void BindModules()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter("EXEC USP_FeedBack @strCommand='Select'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string[] Module = null;
            string ModuleName = "";
            if (dt.Rows.Count != 0)
            {
                DataTable lstItem = new DataTable();
                lstItem.TableName = "list";
                lstItem.Columns.Add("ID", typeof(Int32));
                lstItem.Columns.Add("LicenseKey", typeof(string));
                lstItem.AcceptChanges();
                string ClientLicenseKey = Convert.ToString(dt.Rows[0]["LicenseKey"]);
                lblName.Text = Convert.ToString(dt.Rows[0]["c_Name"]);
                lblAddress.Text = Convert.ToString(dt.Rows[0]["c_Address"]);
                if (ClientLicenseKey != "")
                {
                    string[] strKey = ClientLicenseKey.Split('µ');
                    Module = strKey[1].Split('¶');
                    if (Module != null)
                    {

                        for (int i = 0; i < Module.Length; i++)
                        {
                            if (Module[i] != "")
                            {
                                ModuleName = Encryption.EncryptDecrypt.DecryptModule(Module[i]);

                                lstItem.Rows.Add(i, ModuleName);

                            }

                        }

                        lstModule.DataSource = lstItem;
                        lstModule.DataBind();
                       

                    }
                }
               

            }
            else
            {
                Visi.Visible = false;
                string str = " <table><tr><td rowspan='2'><img  src='images/Invalid.png' width='40'/></td><td align='left'>A valid license could not be obtained by the UNO license manager.</td></tr>";
                str += "<tr><td align='left'>If you are an authorized user, please contact CMS Computers Ltd. on the below number(s) -</td></tr>";
                str += "<tr><td></td><td align='left' style='padding-top:10px'>022-4125 9051</td></tr>";
                str += "<tr><td></td><td align='left'>send an email to -</td></tr>";
                str += "<tr><td></td><td align='left'>uno@cms.co.in</td></tr></table>";
                lblMessage.Text = str;
                mpeMessage.Show();
            }

        }
      
      

       
    }


    

}