using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.Services;
using System.Net.Mail;
using System.Net;
namespace UNO
{
    public partial class SACPersonalisation : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {
                if (Request.Form["__EVENTARGUMENT"] == "Perso")
                {
                    GetEmployeeDetails(txtEmployeeCode.Text);
                }
            }
        }
        [WebMethod]
        public static string GetEmployeeDetails(string EmployeeCode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strsql = " SELECT EPD_FIRST_NAME + ' ' + EPD_LAST_NAME AS EMP_NAME,EPD_DOB,EPD_GENDER,EPD_CARD_ID, " +
                            " (CASE WHEN EPD_NUMCARDS is NULL THEN 0 ELSE EPD_NUMCARDS END) as Card_Num,EPD_PERDATE " +
                            " FROM ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + EmployeeCode.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (dt.Rows.Count > 0)
                {
                    string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                    DateTime DOB = DateTime.ParseExact(dt.Rows[0]["EPD_DOB"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));

                    strsql = "select VALUE from ENT_PARAMS WHERE MODULE = 'CM' AND CODE = 'ED'";
                    SqlDataAdapter da1 = new SqlDataAdapter(strsql, conn);
                    DataTable dt2 = new DataTable();
                    da1.Fill(dt2);

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("EmpCode", typeof(string));
                    dt1.Columns.Add("EmpName", typeof(string));
                    dt1.Columns.Add("DOB", typeof(string));
                    dt1.Columns.Add("Gender", typeof(string));
                    dt1.Columns.Add("ExpiryDate", typeof(string));
                    dt1.Columns.Add("ExpiryTime", typeof(string));
                    DataRow dr = dt1.NewRow();
                    dr["EmpCode"] = EmployeeCode;
                    dr["EmpName"] = dt.Rows[0]["EMP_NAME"].ToString();
                    dr["DOB"] = DOB.ToString("dd/MM/yyyy");
                    dr["Gender"] = dt.Rows[0]["EPD_GENDER"].ToString();
                    dr["ExpiryDate"] = dt2.Rows.Count < 1 ? "" : dt2.Rows[0][0].ToString();
                    dr["ExpiryTime"] = "23:59";
                    dt1.Rows.Add(dr);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt1);
                    ds.AcceptChanges();
                    return ds.GetXml();

                    //string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                    //DateTime DOB = DateTime.ParseExact(dt.Rows[0]["EPD_DOB"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));

                    //if (dt.Rows[0]["EPD_PERDATE"].ToString() != "")
                    //{
                    //    DateTime PersoDate = DateTime.ParseExact(dt.Rows[0]["EPD_PERDATE"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));
                    //    hdnPerDate.Value = PersoDate.ToString();
                    //}
                    //else
                    //{
                    //    hdnPerDate.Value = "";
                    //}

                    //txtEmployeeName.Text = dt.Rows[0]["EMP_NAME"].ToString();
                    //txtDOB.Text = DOB.ToString("dd/MM/yyyy");
                    //ddlGender.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();
                    //txtCardExpiryTime.Text = "23:59";
                    //hdnCardNum.Value = dt.Rows[0]["Card_Num"].ToString();
                    //hdnCSNR.Value = dt.Rows[0]["EPD_CARD_ID"].ToString();


                    //txtCardExpiryDate.Text = dt1.Rows[0][0].ToString();

                    //chkApplication.Items[0].Selected = true;
                    //chkApplication.Items[1].Selected = true;
                    //imgiFrame.Attributes.Add("src", "ImageUploadIframe.aspx?EmpCode=" + txtEmployeeCode.Text.Trim() + "");

                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        //public void GetEmployeeDetails(string EmployeeCode)
        //{
        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //    try
        //    {

        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        string strsql = " SELECT EPD_FIRST_NAME + ' ' + EPD_LAST_NAME AS EMP_NAME,EPD_DOB,EPD_GENDER,EPD_CARD_ID, " +
        //                    " (CASE WHEN EPD_NUMCARDS is NULL THEN 0 ELSE EPD_NUMCARDS END) as Card_Num,EPD_PERDATE " +
        //                    " FROM ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID = '" + EmployeeCode.Trim() + "' ";
        //        SqlCommand cmd = new SqlCommand(strsql, conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + ' ' + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

        //            DateTime DOB = DateTime.ParseExact(dt.Rows[0]["EPD_DOB"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));

        //            if (dt.Rows[0]["EPD_PERDATE"].ToString() != "")
        //            {
        //                DateTime PersoDate = DateTime.ParseExact(dt.Rows[0]["EPD_PERDATE"].ToString(), sysFormat, new System.Globalization.CultureInfo("en-US"));
        //                hdnPerDate.Value = PersoDate.ToString();
        //            }
        //            else
        //            {
        //                hdnPerDate.Value = "";
        //            }

        //            txtEmployeeName.Text = dt.Rows[0]["EMP_NAME"].ToString();
        //            txtDOB.Text = DOB.ToString("dd/MM/yyyy");
        //            ddlGender.SelectedValue = dt.Rows[0]["EPD_GENDER"].ToString();
        //            txtCardExpiryTime.Text = "23:59";
        //            hdnCardNum.Value = dt.Rows[0]["Card_Num"].ToString();
        //            hdnCSNR.Value = dt.Rows[0]["EPD_CARD_ID"].ToString();

        //            strsql = "select VALUE from ENT_PARAMS WHERE MODULE = 'CM' AND CODE = 'ED'";
        //            SqlDataAdapter da1 = new SqlDataAdapter(strsql, conn);
        //            DataTable dt1 = new DataTable();
        //            da1.Fill(dt1);
        //            txtCardExpiryDate.Text = dt1.Rows[0][0].ToString();

        //            chkApplication.Items[0].Selected = true;
        //            chkApplication.Items[1].Selected = true;
        //            imgiFrame.Attributes.Add("src", "ImageUploadIframe.aspx?EmpCode=" + txtEmployeeCode.Text.Trim() + "");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        [WebMethod]
        public static string CheckCSNR(string CSNR)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spValidateCSNR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count == 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }


        [WebMethod]
        public static string SendMail(string EmpCode, string pin, string CSNR)
        {
            string Result = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {

                string strMailOption = "";
                string strMailServer = "";
                string strMailUserName = "";
                string strMailPassword = "";
                int strMailPort = 0;
                string UserMailId = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd1 = new SqlCommand("spSACCardPersonalisationComplete", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmpCode;
                cmd1.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = pin;
                cmd1.Parameters.Add("@Session", SqlDbType.VarChar).Value = HttpContext.Current.Session["uid"].ToString();
                cmd1.ExecuteNonQuery();
                String strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlDataAdapter da1 = new SqlDataAdapter("USP_DashBoard @strCommand='getEmailId', @userId=" + EmpCode + "", conn);
                DataSet ds = new DataSet();
                da1.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    UserMailId = Convert.ToString(ds.Tables[0].Rows[0]["epd_email"]);
                    //UserMailId = "shraddha_parihar@cms.co.in";
                }
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                    {
                        strMailOption = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                    {
                        strMailServer = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                    {
                        strMailUserName = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                    {
                        strMailPassword = dt.Rows[i]["Value"].ToString();
                    }
                    if (dt.Rows[i]["Identifier"].ToString() == "MAILPORT")
                    {
                        strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "Dear Employee," + System.Environment.NewLine + "    The PIN to be used for authentication at the Access Control Readers is " + pin + "." +
                                  System.Environment.NewLine + "Kindly keep this mail safe for future reference." +
                                 System.Environment.NewLine + "Thanks," +
                                 System.Environment.NewLine + "UNO" +
                                 System.Environment.NewLine + "--This is an auto-generated email. Kindly donot reply to it.";
                objMailMessage.From = new MailAddress("uno@cms.co.in");
                objMailMessage.To.Add(UserMailId.Trim());
                objMailMessage.Subject = "PIN for Authentication";
                objMailMessage.Body = message.Trim();
                objMailMessage.Priority = MailPriority.High;
                objSMTPCLIENT.Port = strMailPort;
                objSMTPCLIENT.Host = strMailServer;

                if (strMailUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

                }
                objSMTPCLIENT.Send(objMailMessage);
                Result = "True";
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Result = "False";
            }
            return Result;

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

        protected void btnCancelPerso_Click(object sender, EventArgs e)
        {
            Response.Redirect("SACPersonalisationView.aspx");
        }


    }
}