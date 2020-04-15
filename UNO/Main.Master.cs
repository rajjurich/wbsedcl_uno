using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Management;
using CMS.UNO.Login.Handler;
using System.Text;
using System.Web.Script.Serialization;

namespace UNO
{
    public partial class Main : System.Web.UI.MasterPage
    {

        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string licensekey = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {

            // WriteLog(" Message 1");
            //coment for development purpose
            if (IsPostBack)
            {
                //  WriteLog(" Message 2");
                if (!Request.FilePath.Contains("Login"))
                {
                    //    WriteLog(" Message 3");
                    string strPreviousPage = "";
                    if (Request.UrlReferrer != null)
                    {
                        strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    }
                    if (strPreviousPage == "")
                    {
                        Session.Abandon();
                        Response.Redirect("Login.aspx");

                    }
                    ///  WriteLog(" Message 4");
                }
            }
            string page = Path.GetFileName(Request.Url.AbsolutePath);
            licensekey = GetMacAddress();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //WriteLog(" Message 5");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1)); Response.Cache.SetNoStore();

            //WriteLog(" Message 6");


            if (Session["uid"] == null || Session["uid"].ToString() == "")
            {
                //if (Request.QueryString["Missionflag"] != null)
                //{
                //    if (page == "Home.aspx")
                //    {

                //    }
                //    else if (Request.QueryString["Missionflag"].ToString() == "1")
                //    {
                //       Response.Redirect("Mission.aspx");
                //    }
                //    else
                //    {
                //        Response.Redirect("Login.aspx?redirect=" + HttpUtility.UrlEncode(url), true);
                //    }
                //}


                //else
                //{

                if (page == "Home.aspx" || page == "home.aspx")
                {

                }
                else if (page == "Mission.aspx")
                {

                }
                else if (page == "FeedBack.aspx")
                {

                }
                else if (page == "Aboutus.aspx")
                {

                }
                else
                {
                    Session.Abandon();
                    Session.RemoveAll();
                    Response.Redirect("Login.aspx");
                    //Response.Redirect("Login.aspx?redirect=" + HttpUtility.UrlEncode(url), true);
                }

            }
            else
            {
                if (Session["loginName"].ToString().Trim() == "(0)")
                {
                    lblUserName.Text = Session["uid"].ToString();
                }
                else
                {
                    lblUserName.Text = Session["loginName"].ToString();
                }
            }

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

                licensekey = GetMacAddress();
                string ClientLicenseKey = clsLoginHandler.GetLicenceKey("Licence");
             
                string[] strKey = ClientLicenseKey.Split('µ');
                if (ClientLicenseKey != "")
                {
                    ClientLicenseKey = Encryption.EncryptDecrypt.Decrypt(strKey[0], true);
                }
                if (licensekey != ClientLicenseKey)
                {
                    objData = new clsLogin();
                    objData.UserID = txtUserName.Text.Trim();
                    objData.Password = txtPassword.Text;
                    
                    clsLogin objLogin = clsLoginHandler.GetLoginDetails("Login", txtUserName.Text.Trim(), Encryption.EncryptDecrypt.Encrypt(txtPassword.Text.Trim(), true));
                    if (objLogin != null)
                    {
                        if (objLogin.UserID.ToString().ToUpper().Trim() == txtUserName.Text.ToUpper().Trim())  //changes made by Shrinith On 12/Sept/2014
                        {
                            if (objLogin.UserID.ToLower() == "admin")
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
                                                    //Response.Redirect("Manager_Dashboard.aspx", false);only for wbsedcl
                                                    Response.Redirect("ESS_Dashboard.aspx", true);
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
                                            Response.Redirect("Uno_Dashboard.aspx", false);

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
                WriteLog(ex.Message);

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
            //System.IO.StreamWriter sw = null;
            //try
            //{
            //    sw = new StreamWriter("D:\\LOG\\Msg.Log", true);
            //    sw.WriteLine(strMesCsage);
            //}
            //finally
            //{
            //    sw.Close();
            //}
        }
        public void InsertLog(string Message)
        {

        }
        protected void signout_Click(object sender, EventArgs e)
        {
            SaveIpaddresslogout();
            Session.Abandon();
           
            Session.RemoveAll();
         
            Response.Redirect("Home.aspx", true);
        }

        private void SaveIpaddresslogout()
        {

            clsCommonHandler obj = new clsCommonHandler();
            string valueip = obj.GetIpAddress();
            var query = "insert into IpaddressLog values(@ipaddress,getdate(),@action,@controller_id)";
            using (SqlConnection con = new SqlConnection(m_connections))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ipaddress", valueip);
                    cmd.Parameters.AddWithValue("@action", "Admin Logged Out");
                    cmd.Parameters.AddWithValue("@controller_id", 0);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        }
        protected void btnOk_Click(object sender, EventArgs e)
        {

            /* try
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
             }*/

            string uniqueCode = string.Empty;

            try
            {
                clsLogin objSendData = new clsLogin();
                objSendData.EmployeeID = txtEmployeeID.Text.Trim();
                clsLogin objLogin = clsLoginHandler.GetPassword("GetPassword", objSendData);
                if (objLogin != null && objLogin.Password != "" && objLogin.EmailID != "")
                {
                    uniqueCode = Convert.ToString(System.Guid.NewGuid());

                    clsLogin objData = new clsLogin();
                    objData.UserID = objLogin.UserID;
                    objData.EmailID = objLogin.EmailID;
                    objData.UniqueCode = uniqueCode;
                    clsLoginHandler.UpdateUniqueID("UpdateUniqueCode", objData);

                    StringBuilder strBody = new StringBuilder();
                    clsLoginView objSend = new clsLoginView();
                    objSend.UserID = objLogin.UserID;
                    objSend.EmailID = objLogin.EmailID;
                    objSend.UniqueCode = uniqueCode;
                    var json = new JavaScriptSerializer().Serialize(objSend);
                    // strBody.Append("<a href=http://localhost/STD/objSend.aspx?emailId=" + objLogin.EmailID+ "&uName=" + objLogin.UserID + "&uCode=" + uniqueCode + ">Click here to change your password</a>");
                    strBody.Append("<HTML><BODY>" + "<font family=Times New Roman;>Hello!</br></br>");
                    strBody.Append("We have received a password change request for your UNO account (" + txtEmployeeID.Text.Trim() + ")");
                    strBody.Append("If you did not ask to change your password, then you can ignore this email ");
                    strBody.Append(" and your password will not be changed. The link below will remain active for 24 hours.:" + System.Environment.NewLine);
                    strBody.Append("</br></br>");
                    strBody.Append("<a href=http://localhost/STD/ResetPassword.aspx?dataobj=" + clsLoginHandler.Encryptdata(json) + ">Click here to change your password</a>");
                    strBody.Append("</br></br>");
                    strBody.Append("Regards,</br>"+Application["ClientName"].ToString()+"</font>");
                    strBody.Append("</BODY></HTML>");
                    strBody.Append("</br></br>");
                    Mail.SendMail("uno@cms.co.in", objLogin.EmailID, "", "Reset Password Link", strBody.ToString());
                    lblMessage.Text = "Reset password link has been sent to your email address";
                    txtEmployeeID.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "No Email Address Registered.";
                }
                mpeMessage.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                mpeMessage.Show();
            }

        }
        protected void ibHome_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToString(Session["uid"]) != "")
            {
                if (Convert.ToString(clsCommonHandler.GetLevelCode(Convert.ToInt32(Session["levelId"]))).ToLower() != "admin")
                {
                    if (Session["EssEnabled"].ToString() == "True")
                    {

                        if (CheckReporting.IsManager(Convert.ToString(Session["uid"])))
                        {
                            //Response.Redirect("Manager_Dashboard.aspx", true);only for wbsedcl
                            Response.Redirect("ESS_Dashboard.aspx", true);
                        }
                        else
                        {
                            Response.Redirect("ESS_Dashboard.aspx", true);
                        }
                    }
                }
                else
                {
                    Response.Redirect("Uno_Dashboard.aspx", true);

                }

            }
            else
            {
                Response.Redirect("Home.aspx", true);
            }
        }
    }
}