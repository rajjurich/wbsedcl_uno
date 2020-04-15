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
    public partial class FeedBack : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

   

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
             
                if (ClientLicenseKey != "")
                {
                    string[] strKey = ClientLicenseKey.Split('µ');
                    Module = strKey[1].Split('¶');
                    if (Module != null)
                    {
                        drpModule.Items.Insert(0, new ListItem("Select One", ""));
                        for (int i = 0; i < Module.Length; i++)
                        {
                            if (Module[i] != "")
                            {
                                ModuleName = Encryption.EncryptDecrypt.DecryptModule(Module[i]);
                                drpModule.Items.Insert(i + 1, new ListItem(ModuleName, ModuleName));
                                lstItem.Rows.Add(i, ModuleName);

                            }

                        }

                       

                    }
                }

            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("EXEC USP_FeedBack  @strCommand='Insert',@Module='" + drpModule.SelectedItem + "',@comment='" + txtComment.InnerText.Trim() + "',@ImageName='" + lblImageName.Text + "',@userId='" + Convert.ToString(Session["uid"]) + "'", conn);
                string strFeedBackId = Convert.ToString(cmd.ExecuteScalar());
                if (strFeedBackId != "")
                {
                    lblFeedBackId.Text = "Thank you for FeedBack your FeedBack Id Is : " + strFeedBackId + "";
                    lblFeedBackId.Visible = true;
                    lblfile.Text = "";
                    lblfile.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblfile.Visible = true;
                lblfile.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImageUpload.HasFile)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("EXEC USP_FeedBack  @strCommand='ImageName'", conn);
                    string filename = Convert.ToString(cmd.ExecuteScalar());
                    string ext = System.IO.Path.GetExtension(ImageUpload.PostedFile.FileName);
                    //  string filename = Path.GetFileName(ImageUpload.FileName);
                    ImageUpload.SaveAs(Server.MapPath("~/FeedBackImage/") + filename + ext);
                    lblImageName.Text = filename + ext;
                    lblfile.Text = "Upload status: File uploaded";
                    lblfile.Visible = true;

                }
                else
                {
                    lblfile.Visible = true;
                    lblfile.Text = "Upload status: File not uploaded!";
                   
                }
               // mpeFeedBack.Show();
            }
            catch (Exception ex)
            {
                lblfile.Visible = true;
                lblfile.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                
               // mpeFeedBack.Show();
            }
        }

       
    }


    

}