using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using CMS.UNO.Login.Handler;

namespace UNO
{
    public partial class Login : System.Web.UI.Page
    {
        string url = "";
        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string licensekey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
                if (Request.QueryString["redirect"] != null)
                {
                    url = Request.QueryString["redirect"].ToString();
                }
                else
                {
                    Response.Redirect("Home.aspx", true);
                }
          
        }       
        private string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address 
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }

            return sMacAddress + GetHDDSerialNo(); //MAC+HDD
        }
        private string GetHDDSerialNo()
        {
            string strDriveLetter = "C";
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + strDriveLetter + ":\"");
            disk.Get();
            return disk["VolumeSerialNumber"].ToString();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            clsLogin objData = null;

            //if (DateTime.Now > CMSDateTime.CMSDateTime.ConvertToDateTime("20/01/2018", "dd/MM/yyyy"))
            //{
            //    lblMessage.Visible = true;
            //    string str = " <table><tr><td rowspan='2'><img  src='images/Invalid.png' width='40'/></td><td align='left'>License is expired.</td></tr>";
            //    str += "<tr><td align='left'>please contact CMS Computers Ltd. -</td></tr>";
            //    str += "<tr><td></td><td align='left'>send an email to -</td></tr>";
            //    str += "<tr><td></td><td align='left'>uno@cms.co.in</td></tr></table>";

            //    lblMessage.Text = str;
            //    mpeMessage.Show();
            //    return;
            //}

            try
            {
                WriteLog("Getting Licence");
                licensekey = GetMacAddress();
                string ClientLicenseKey = clsLoginHandler.GetLicenceKey("Licence");
                string[] strKey = ClientLicenseKey.Split('µ');
                if (ClientLicenseKey != "")
                {
                    WriteLog("License Decrypt");
                    ClientLicenseKey = Encryption.EncryptDecrypt.Decrypt(strKey[0], true);
                }
                if (licensekey == ClientLicenseKey)
                {
                    WriteLog("License Check Ok");
                    objData = new clsLogin();
                    objData.UserID = txtUserName.Text.Trim();
                    objData.Password = txtPassword.Text;

                    clsLogin objLogin = clsLoginHandler.GetLoginDetails("Login", txtUserName.Text.Trim(), Encryption.EncryptDecrypt.Encrypt(txtUserName.Text.Trim(), true));
                    if (objLogin != null)
                    {
                        if (objLogin.UserID.ToString().ToUpper().Trim() == txtUserName.Text.ToUpper().Trim())  //changes made by Shrinith On 12/Sept/2014
                        {
                            if (objLogin.UserID.ToLower()=="admin")
                            {
                                SaveIpaddress();
                            }
                            string strpass = Encryption.EncryptDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                            if (Encryption.EncryptDecrypt.Decrypt(objLogin.Password, true) == txtPassword.Text)
                            {
                                if (objLogin.Active == true)
                                {

                                    Session["uid"] = objLogin.UserID;
                                    Session["loginName"] = objLogin.EmpName;
                                    Session["levelId"] = Convert.ToString(objLogin.LevelID);
                                    Session["EssEnabled"] = Convert.ToString(objLogin.EssEnabled);

                                    if (objLogin.EssEnabled.ToString() == "True")
                                    {
                                        if (objLogin.IsFirstLogin)
                                        {
                                            Response.Redirect("UpdatePassword.aspx", false);
                                        }
                                        else
                                        {
                                            if (Convert.ToString(clsCommonHandler.GetLevelCode(Convert.ToInt32(Session["levelId"]))).ToLower() != "admin")
                                            {
                                                if (CheckReporting.IsManager(Convert.ToString(Session["uid"])))
                                                {
                                                    Response.Redirect("Manager_Dashboard.aspx", false);
                                                }
                                                else
                                                {
                                                    Response.Redirect("ESS_Dashboard.aspx", false);
                                                }
                                            }
                                            else
                                            {
                                                Response.Redirect("Uno_Dashboard.aspx", false);
                                            }

                                        }
                                    }
                                    else
                                    {

                                        if (objLogin.IsFirstLogin)
                                        {
                                            Response.Redirect("UpdatePassword.aspx", false);
                                        }
                                        else
                                        {
                                          //  Response.Redirect("Uno_Dashboard.aspx", false);

                                            Response.Redirect("Personalization.aspx", false);

                                        }
                                    }

                                    objData.Message = "Success";
                                    clsLoginHandler.InsertLog("InsertLog", objData);

                                }
                                else
                                {
                                    objData.Message = "Failed";
                                    clsLoginHandler.InsertLog("InsertLog", objData);

                                    txtUserName.Text = "";
                                    txtPassword.Text = "";
                                    lblMessage.Visible = true;
                                    lblMessage.Text = "Inactive User";
                                    mpeMessage.Show();
                                    return;
                                }
                            }
                            else
                            {
                                objData.Message = "Failed";
                                clsLoginHandler.InsertLog("InsertLog", objData);

                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                lblMessage.Visible = true;
                                lblMessage.Text = "Invalid Password";
                                mpeMessage.Show();
                                txtPassword.Focus();
                                return;
                            }
                        }
                        else
                        {
                            objData.Message = "Failed";
                            clsLoginHandler.InsertLog("InsertLog", objData);

                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            lblMessage.Visible = true;
                            lblMessage.Text = "Invalid User id";
                            mpeMessage.Show();
                            return;
                        }
                    }
                    else
                    {
                        objData.Message = "Failed";
                        clsLoginHandler.InsertLog("InsertLog", objData);

                        txtUserName.Text = "";
                        txtPassword.Text = "";
                        lblMessage.Visible = true;
                        lblMessage.Text = "Invalid User";
                        mpeMessage.Show();
                        return;
                    }

                }
                else
                {


                    lblMessage.Visible = true;
                    string str = " <table><tr><td rowspan='2'><img  src='images/Invalid.png' width='40'/></td><td align='left'>A valid license could not be obtained by the UNO license manager.</td></tr>";
                    str += "<tr><td align='left'>If you are an authorized user, please contact CMS Computers Ltd. on the below number(s) -</td></tr>";
                    str += "<tr><td></td><td align='left' style='padding-top:10px'>022-4125 9051</td></tr>";
                    str += "<tr><td></td><td align='left'>send an email to -</td></tr>";
                    str += "<tr><td></td><td align='left'>uno@cms.co.in</td></tr></table>";
                    lblMessage.Text = str;
                    mpeMessage.Show();

                }
            }
            catch (Exception ex)
            {
                WriteLog("Message 8");

                WriteLog(ex.Message.ToString());
                if (ex.Message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections."))
                {
                    lblMessage.Text = "Database Connectivity Issue";
                    lblMessage.Visible = true;
                    mpeMessage.Show();
                    return;
                }
                else if (ex.Message.Contains("Cannot open database"))
                {
                    lblMessage.Text = "Database not found";
                    lblMessage.Visible = true;
                    mpeMessage.Show();
                    return;
                }
                else if (ex.Message.Contains("Login failed for user"))
                {
                    lblMessage.Text = "Please enter correct user id and password in web config";
                    lblMessage.Visible = true;
                    mpeMessage.Show();
                    return;
                }
                else
                {
                    lblMessage.Text = ex.Message;//"Unexpected error";
                    lblMessage.Visible = true;
                    mpeMessage.Show();

                }





                WriteLog("Message 10");
            }
            finally
            {
                WriteLog("Message 11");

                WriteLog("Message 12");
            }
        }
        private void SaveIpaddress()
        {
            clsCommonHandler obj = new clsCommonHandler();

            string valueip = obj.GetIpAddress();
            var query = "insert into IpaddressLog values(@ipaddress,getdate(),@action,@controller_id)";
            using (SqlConnection con = new SqlConnection(m_connections))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ipaddress", valueip);
                    cmd.Parameters.AddWithValue("@action", "Admin Logged In");
                    cmd.Parameters.AddWithValue("@controller_id", 0);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        private void WriteLog(string strMessage)
        {
            //System.IO.StreamWriter sw=null;
            //try 
            //{
            //    sw=new StreamWriter("D:\\LOG\\Msg.Log",true);
            //    sw.WriteLine(strMessage);
            //}
            //finally
            //{
            //    sw.Close();
            //}
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
               
                //clsLogin objLogin = clsLoginHandler.GetPassword("GetPassword", txtEmployeeID.Text.Trim());
                //if (objLogin != null && objLogin.Password != "" && objLogin.EmailID != "")
                //{
                //    Mail.SendMail(objLogin.EmailID, Encryption.EncryptDecrypt.Decrypt(objLogin.Password, true));

                //    lblMessage.Visible = true;

                //    lblMessage.Text = "Password has been sent successfully to your mail id";
                //}
                //else
                //{
                //    lblMessage.Text = "No Email Address Registered.";
                //}
                //mpeMessage.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                mpeMessage.Show();
            }
        }
       

    }
}