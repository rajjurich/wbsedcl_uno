using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;

namespace UNO
{
    public partial class NetwrokUpdate : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                { intilizeControl(); }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");

            }
        }

        public void intilizeControl()
        {

            try
            {
                String _strSelect = "select * from ENT_PARAMS where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE'";
                DataTable _dt = getDataTable(_strSelect, conn);
                divEraseUpdate.Disabled = true;
                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    if (_dt.Rows[i]["CODE"].ToString().Trim() == "ALC" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkAccessLevel.Checked = true; }
                    else if (_dt.Rows[i]["CODE"].ToString().Trim() == "APC" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkAccessPoint.Checked = true; }
                    else if (_dt.Rows[i]["CODE"].ToString().Trim() == "CTL" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkController.Checked = true; }
                    else if (_dt.Rows[i]["CODE"].ToString().Trim() == "TZN" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkTimeZone.Checked = true; }
                    else if (_dt.Rows[i]["CODE"].ToString().Trim() == "UAC" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkUAC.Checked = true; divEraseUpdate.Disabled = false; }
                    //added by jasmin
                    else if (_dt.Rows[i]["CODE"].ToString().Trim() == "APB" && _dt.Rows[i]["VALUE"].ToString().Trim() == "1")
                    { chkABP.Checked = true; }
                    //end
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
            }
        }

        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();

                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
                return null;
            }
        }


        protected void chkResetAllABP_CheckedChanged(object sender, EventArgs e)
        {
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
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
                return "";
            }
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
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                    _sqlconn.Close();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
                return false;
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked == true)
                {
                    chkAccessLevel.Checked = true;
                    chkAccessPoint.Checked = true;
                    chkController.Checked = true;
                    chkTimeZone.Checked = true;
                    chkUAC.Checked = true;
                    chkABP.Checked = true; //added by jasmin
                }
                else
                {
                    chkAccessLevel.Checked = false;
                    chkAccessPoint.Checked = false;
                    chkController.Checked = false;
                    chkTimeZone.Checked = false;
                    chkUAC.Checked = false;
                    chkABP.Checked = false; //added by jasmin
                    intilizeControl();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
            }
        }

        public String getString()
        {
            String _strStatus = "";

            try
            {

                if (chkAll.Checked == true)
                {
                    _strStatus = "111111";
                }
                else
                {
                    if (chkAccessLevel.Checked == true)
                    { _strStatus = "1"; }
                    else
                    { _strStatus = "0"; }

                    if (chkAccessPoint.Checked == true)
                    { _strStatus = _strStatus + "1"; }
                    else
                    { _strStatus = _strStatus + "0"; }

                    if (chkController.Checked == true)
                    { _strStatus = _strStatus + "1"; }
                    else
                    { _strStatus = _strStatus + "0"; }


                    if (chkTimeZone.Checked == true)
                    { _strStatus = _strStatus + "1"; }
                    else
                    { _strStatus = _strStatus + "0"; }

                    if (chkUAC.Checked == true)
                    { _strStatus = _strStatus + "1"; }
                    else
                    { _strStatus = _strStatus + "0"; }

                    _strStatus = _strStatus + "0";
                }
                return _strStatus;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
                return "";
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                saveRecords("0");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
            }
        }

        protected void cmdEraseUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                saveRecords("1");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
            }
        }

        public void saveRecords(String _EraseUpdateProfiles)
        {
            try
            {

                String _status = getString();

                if (_status != "000000" || chkABP.Checked == true)
                {
                    try
                    {
                        _status = _status + _EraseUpdateProfiles; // Added 0 for no Erase and Update Profiles
                        //added by jasmin APB status
                        if (chkABP.Checked == true)
                        {
                            _status += "1";
                        }
                        else
                        {
                            _status += "0";
                        }
                        if (chkResetAllABP.Checked == true)
                        {
                            _status += "1";
                        }
                        else
                        {
                            _status += "0";
                        }
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        String _strupdate = "update ENT_PARAMS set [VALUE]='" + _status + "' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='CC'";
                        Boolean _result = RunExecuteNonQuery(_strupdate, conn);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    { UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork"); }

                    this.messageDiv.InnerHtml = "Updation Intiated";
                    string someScript = "";
                    someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",5000);</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                }
                else if (_status == "000000")
                {

                    this.messageDiv.InnerHtml = "Please Select.";
                    string someScript2 = "";
                    someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",4000);</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UpdateNetwork");
            }

        }

    }
}