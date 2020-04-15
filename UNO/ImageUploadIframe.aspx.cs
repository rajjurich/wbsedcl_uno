using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Net.Mail;
using System.Net;

namespace UNO
{
    public partial class ImageUploadIframe : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public string EmpCode = "";
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["EmpCode"] != "")
            {
                EmpCode = Convert.ToString(Request.QueryString["EmpCode"]);
            }
          
        }

        protected void BtSaveImage_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                if (FileUploadImages.PostedFile.FileName != "")
                {
                    if (FileUploadImages.PostedFile != null)
                    {
                        byte[] fileBytes = FileUploadImages.FileBytes;

                        string base64String = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);
                        imgEmployeeImage.ImageUrl = "data:image/png;base64," + base64String;
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("USP_EmpPersonalisationInfo", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@strCommand", "Insert");
                        cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
                        cmd.Parameters.AddWithValue("@EmpImage", base64String);
                        cmd.ExecuteNonQuery();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                    }
                }
                else
                {
                    lblMsg.Text = "Please Select Proper Image format i.e JPG,PNG,BMP";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

     
     

    }
}