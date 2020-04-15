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
    public partial class AutoScheduledBackupConfig : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        static string _strpath = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                intializeControl();
            }
            if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
            {
                txtPassword.Attributes["value"] = txtPassword.Text;
                txtPasswordConfirm.Attributes["value"] = txtPasswordConfirm.Text;
            }
        }
        public void intializeControl()
        {
            try
            {


                DataTable _dtRecord = clsAutoScheduleBackupHandler.GetBackupDetails("select");
                if (_dtRecord.Rows.Count != 0)
                {

                    txtPassword.Attributes.Add("Value", Encryption.EncryptDecrypt.Decrypt(_dtRecord.Rows[0]["Password"].ToString(), true));
                    txtPasswordConfirm.Attributes.Add("Value", Encryption.EncryptDecrypt.Decrypt(_dtRecord.Rows[0]["Password"].ToString(), true));
                    string[] _Days;
                    _Days = _dtRecord.Rows[0]["Days"].ToString().Split('-');
                    for (int i = 0; i < _Days.Length; i++)
                    {
                        if (_Days[i].ToString() == "SUN") { ChkSunday.Checked = true; }
                        if (_Days[i].ToString() == "MON") { ChkMonday.Checked = true; }
                        if (_Days[i].ToString() == "TUE") { ChkTuesday.Checked = true; }
                        if (_Days[i].ToString() == "WED") { ChkWednesday.Checked = true; }
                        if (_Days[i].ToString() == "THU") { ChkThursday.Checked = true; }
                        if (_Days[i].ToString() == "FRI") { ChkFriday.Checked = true; }
                        if (_Days[i].ToString() == "SAT") { ChkSaturday.Checked = true; }
                    }
                    //Days

                    string afterdays = _dtRecord.Rows[0]["DeleteBackupsAfterDays"].ToString();
                    txtStartTime.Text = _dtRecord.Rows[0]["StartTime"].ToString();
                    txtSQLBackupPath.Text = _dtRecord.Rows[0]["BackupPath"].ToString();
                    txtMailId.Text = _dtRecord.Rows[0]["email_id"].ToString();



                    //changes by shrinith on 12/Sept/2014 End
                }
                else
                {
                    txtPassword.Text = "";
                    txtPasswordConfirm.Text = "";
                    //Days
                    txtStartTime.Text = "";
                    txtSQLBackupPath.Text = "";
                    ChkSunday.Checked = false;
                    ChkMonday.Checked = false;
                    ChkTuesday.Checked = false;
                    ChkWednesday.Checked = false;
                    ChkThursday.Checked = false;
                    ChkFriday.Checked = false;
                    ChkSaturday.Checked = false;
                    txtMailId.Text = txtPassword.Text = txtPasswordConfirm.Text = "";
                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        public string getValue(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }
            catch (Exception ex)
            { return ""; }
        }
        public bool RunExecuteNonQuery(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            { return false; }
        }
        protected void CmdOk_Click(object sender, EventArgs e)
        {
            //if (Page.IsValid && Validate())
            //{
            try
            {
                
                String _days = "";
                if (ChkSunday.Checked == true) { _days = "SUN-"; }
                if (ChkMonday.Checked == true) { _days = _days + "MON-"; }
                if (ChkTuesday.Checked == true) { _days = _days + "TUE-"; }
                if (ChkWednesday.Checked == true) { _days = _days + "WED-"; }
                if (ChkThursday.Checked == true) { _days = _days + "THU-"; }
                if (ChkFriday.Checked == true) { _days = _days + "FRI-"; }
                if (ChkSaturday.Checked == true) { _days = _days + "SAT"; }

                int _position = _days.LastIndexOf("-");
                if ((_position + 1) == _days.Length) { _days = _days.Substring(0, _position); }


                clsAutoScheduleBackup objData = new clsAutoScheduleBackup();
                objData.Password = Encryption.EncryptDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                objData.Days = _days;
                objData.StartTime = txtStartTime.Text.Trim();
                objData.BackupPath = txtSQLBackupPath.Text.TrimEnd('\\');
                objData.EmailID = txtMailId.Text.Trim();
                objData.UserName = Session["uid"].ToString().Trim();
                string SuccMsg = string.Empty;
                clsAutoScheduleBackupHandler.insertBackupDetails("Insert", objData, clsCommonHandler.PageName(), ref SuccMsg);
                if (SuccMsg.Trim().Length > 1)
                {
                    this.messageDiv.InnerHtml = SuccMsg.Trim();
                    this.messageDiv.Visible = true;
                    this.messageDiv1.Visible = false;
                }
            }
            catch (SqlException sex)
            {
                this.messageDiv.InnerHtml = sex.Message;
                this.messageDiv.Visible = true;
                this.messageDiv1.Visible = false;
            }
            catch (Exception ex)
            {
                this.messageDiv.InnerHtml = ex.Message;
                this.messageDiv.Visible = true;
                this.messageDiv1.Visible = false;
            }
            //}
        }
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            intializeControl();
        }
        protected void btnDelete_Click(object sender, EventArgs e) //changes made by shrinith on 12/Sept/2014 
        {

            Boolean _result;
            string _strQuery = "";
            String _days = "";

            String _selectQuery = "SELECT count(*) FROM AutoScheduledBackupConfig";

            if (Convert.ToInt16(getValue(_selectQuery, _sqlConnection)) > 0)
            {
                _strQuery = "Truncate Table AutoScheduledBackupConfig";
                // "WHERE id='1'";

            }

            _result = RunExecuteNonQuery(_strQuery, _sqlConnection);
            txtPassword.Attributes["value"] = "";
            txtPasswordConfirm.Attributes["value"] = "";
            intializeControl();



        }
        protected void btnSaveManualBackup_Click(object sender, EventArgs e)
        {
            Backup();

        }
        protected void Backup()
        {
            string path = System.Configuration.ConfigurationSettings.AppSettings["LogPath"];
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("USP_AutoScheduledBackupConfig @strCommand='select'", _sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    string strPassword = Encryption.EncryptDecrypt.Decrypt(dt.Rows[0]["Password"].ToString(), true);
                    string[] _Days;
                    _Days = dt.Rows[0]["Days"].ToString().Split('-');
                    string todyDay = "";
                    DateTime ClockInfoFromSystem = DateTime.Now;

                    string day2;

                    //day2 = ClockInfoFromSystem.DayOfWeek.ToString();
                    //int day = (int)DateTime.Now.DayOfWeek;

                    //if (todyDay != "")
                    //{
                    string strStartTime = dt.Rows[0]["StartTime"].ToString();

                    string strBackup = txtmanuPath.Text;
                    string strEmailId = txtManualMailId.Text;

                    if (!Directory.Exists(strBackup))
                        Directory.CreateDirectory(strBackup);

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                    builder.ConnectionString = _sqlConnection.ConnectionString;


                    string database = builder.InitialCatalog.ToUpper();

                    string dbName = "\\" + database + "Backup" + System.DateTime.Now.ToString("ddMMyy_HHmm") + ".bak";


                    if (_sqlConnection.State == ConnectionState.Closed)
                    {
                        _sqlConnection.Open();
                    }
                

                    if (_sqlConnection.State == ConnectionState.Closed)
                    {
                        _sqlConnection.Open();
                    }
                    string Result = "";
                    try
                    {
                        SqlCommand cmd = new SqlCommand("USP_Backup", _sqlConnection);
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@database", SqlDbType.NVarChar).Value = database;
                        cmd.Parameters.Add("@strBackup", SqlDbType.VarChar).Value = strBackup + dbName;
                        cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "backup";
                        cmd.Parameters.Add("@error", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters["@error"].Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                        int result = cmd.ExecuteNonQuery();
                        Result = Convert.ToString(cmd.Parameters["@error"].Value);
                    }
                    catch (Exception er)
                    {
                        Result = er.Message;
                        WriteLog("Query-Error: " + er.Message + ";" + System.DateTime.Now, path);
                        _sqlConnection.Close();
                    }

                    if (Result != "0")
                    {
                        WriteLog("Result: Error in backup now mail will be send :" + System.DateTime.Now, path);
                        string strMailOption = "";
                        string strMailServer = "";
                        string strMailUserName = "";
                        string strMailPassword = "";
                        int strMailPort = 0;
                        string UserMailId = "";
                        if (_sqlConnection.State == ConnectionState.Closed)
                        {
                            _sqlConnection.Open();
                        }
                        string strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
                        SqlDataAdapter da1 = new SqlDataAdapter(strSql, _sqlConnection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        UserMailId = strEmailId;

                        for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                        {
                            if (dt1.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                            {
                                strMailOption = dt1.Rows[i]["Value"].ToString();
                            }
                            if (dt1.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                            {
                                strMailServer = dt1.Rows[i]["Value"].ToString();
                            }
                            if (dt1.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                            {
                                strMailUserName = dt1.Rows[i]["Value"].ToString();
                            }
                            if (dt1.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                            {
                                strMailPassword = dt1.Rows[i]["Value"].ToString();
                            }
                            if (dt1.Rows[i]["Identifier"].ToString() == "MAILPORT")
                            {
                                strMailPort = Convert.ToInt32(dt1.Rows[i]["Value"]);
                            }
                            _sqlConnection.Close();
                        }
                        SmtpClient objSMTPCLIENT = new SmtpClient();
                        MailMessage objMailMessage = new MailMessage();
                        string message = "Dear User," + System.Environment.NewLine + "     Error while taking database backup." +
                                         System.Environment.NewLine + "Description-" +
                                         System.Environment.NewLine + Result +
                                         System.Environment.NewLine + "Thanks," +
                                         System.Environment.NewLine + "UNO" +
                                         System.Environment.NewLine + "--This is an auto-generated email. Kindly donot reply to it.";
                        objMailMessage.From = new MailAddress("uno@cms.co.in");
                        objMailMessage.To.Add(UserMailId.Trim());
                        objMailMessage.Subject = "DataBase Backup";
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


                    }
                    //}
                    //  }

                    //}
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());

                WriteLog("Error: " + ex.Message + ";" + System.DateTime.Now, path);
            }

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
        protected void btnManualClick_Click(object sender, EventArgs e)
        {
            txtmanuPath.Text = "";
            txtManualMailId.Text = "";
            mpeManualBackup.Show();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpeManualBackup.Hide();
        }


    }
}