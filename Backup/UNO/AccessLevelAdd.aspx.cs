using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace UNO
{
	public partial class AccessLevelAdd : System.Web.UI.Page
	{
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

		protected void Page_Load(object sender, EventArgs e)
		{
            this.messageDiv.InnerHtml = "";
            if (Page.IsPostBack == false)
            {
                PnlZone.Visible = false;
                intializeControl();
            }
		}
        public void intializeControl()
        {
            Session.Remove("_IDExists");
           // txtalid.Text = getValue("select ISNULL(max(AL_ID)+1,1) as id from ACS_ACCESSLEVEL", _sqlConnection);
            txtalid.Text = Convert.ToString(getId());
            txtdescription.Text = "";
            
            cmbZone.Items.Clear();
            cmbZone.Items.Add(new ListItem("Select Zone", "-1"));
            cmbZone.SelectedValue = "-1";

            cmbTimeZone.Items.Clear();
            cmbTimeZone.Items.Add(new ListItem("Select Time Zone", "-1"));
            cmbTimeZone.SelectedValue = "-1";
            try
            {
                string _strsql = "select ZONE_ID,ZONE_DESCRIPTION from ZONE where ZONE_ISDELETED='0'";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbZone.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                string _strsql = "select Distinct(TZ_CODE),TZ_DESCRIPTION from ACS_TIMEZONE where TZ_ISDELETED='0'";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbTimeZone.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            RBLZone.SelectedValue = null;
            lstAReader.Items.Clear();
            lstSReader.Items.Clear();
        }
        public void fillReaders()
        {
            lstAReader.Items.Clear();            
            lstSReader.Items.Clear();
            try
            {

                //string _strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER where READER_ID  " +
                //                 "in (select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where APR_ISDELETED='0' ) and READER_ISDELETED='0'";

                //string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                //                "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //                "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                //                "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                //                "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                //                ")";

                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                                "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                                "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                                "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                                "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                                ")";

                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReader.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void fillZoneReaders()
        {
            lstAReader.Items.Clear();
            lstSReader.Items.Clear();
            try
            {

//select READER_ID,READER_DESCRIPTION from ACS_READER where READER_ID 
//in (select READER_ID from ZONE_READER_REL where ZONE_ID='z006' and ZONER_ISDELETED='0' ) 
//and READER_ISDELETED='0'

                //string _strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER where READER_ID " +
                //                 "in (select READER_ID from ZONE_READER_REL where ZONE_ID='"+ cmbZone.SelectedValue + "' and ZONER_ISDELETED='0') and READER_ISDELETED='0'";

                //string _strsql ="select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                //                "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //                "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
                //                "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
                //                "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0'" +
                //                ")";

                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                               "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                               "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
                               "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
                               "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0'" +
                               ")";

                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReader.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void RBLZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBLZone.SelectedValue == "Z")
            {
                cmbZone.SelectedValue = "-1";
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
                lblSelected.Visible = false;
                lstSReader.Visible = false;
                cmdReaderLeft.Visible = false;
                cmdReaderRight.Visible = false;
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
                lblAvailable.Text = "Readers Included in Zone";
                PnlZone.Visible = true;
            }
            else
            {
                lblSelected.Visible = true;
                lstSReader.Visible = true;
                cmdReaderLeft.Visible = true;
                cmdReaderRight.Visible = true;
                lblAvailable.Text = "Available";
                PnlZone.Visible = false;
                fillReaders();
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
                return _ds.Tables[0];
            }
            catch (Exception ex)
            { return null; }
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
        protected void cmbZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZone.SelectedValue == "-1")
            {
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
            }
            else
            { 
            fillZoneReaders();
            }
        }
        protected void cmdReaderRight_Click(object sender, EventArgs e)
        {
            if (lstAReader.SelectedItem != null)
            {
                    //lstSReader.Items.Add(lstAReader.SelectedItem);
                    //lstAReader.Items.Remove(lstAReader.SelectedItem);  
                for (int i = lstAReader.Items.Count - 1; i >= 0; i--)
                {
                    if (lstAReader.Items[i].Selected == true)
                    {
                        lstSReader.Items.Add(lstAReader.Items[i]);
                        ListItem li = lstAReader.Items[i];
                        lstAReader.Items.Remove(li);
                    }
                }
                lstSReader.SelectedValue = null;
            }
        }
        protected void cmdReaderLeft_Click(object sender, EventArgs e)
        {
            if (lstSReader.SelectedItem != null)
            {
                //lstAReader.Items.Add(lstSReader.SelectedItem);
                //lstSReader.Items.Remove(lstSReader.SelectedItem);
                for (int i = lstSReader.Items.Count - 1; i >= 0; i--)
                {
                    if (lstSReader.Items[i].Selected == true)
                    {
                        lstAReader.Items.Add(lstSReader.Items[i]);
                        ListItem li = lstSReader.Items[i];
                        lstSReader.Items.Remove(li);
                    }
                }
                lstAReader.SelectedValue = null;
            }
        }
        public Boolean checkId(String _strId)
        {
            try
            {
                String _result = getValue("Select AL_ID from ACS_ACCESSLEVEL where AL_ID='" + _strId + "'", _sqlConnection);
                if (_result != "")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            { return false; }
        }
        public Boolean validation()
        {
            try
            {
                string script = "";
                
                if (txtdescription.Text.Trim() == "")
                { this.messageDiv.InnerHtml = "Please enter Description"; return false; }
                else if (RBLZone.SelectedValue == "Z" && cmbZone.SelectedValue == "-1")
                { //this.messageDiv.InnerHtml = "Please select Zone."; return false; 
                    script = "<script language='javascript'>PushAlert('Please select Zone');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (RBLZone.SelectedValue == "R" && lstSReader.Items.Count == 0)
                { //this.messageDiv.InnerHtml = "Please select Reader(s)."; return false; 
                    script = "<script language='javascript'>PushAlert('Please select Reader(s).');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (RBLZone.SelectedValue == "Z" && lstAReader.Items.Count == 0)
                { //this.messageDiv.InnerHtml = "Please select valid Zone"; return false; 
                    script = "<script language='javascript'>PushAlert('Please select valid Zone.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                else if (cmbTimeZone.SelectedValue == "-1")
                { //this.messageDiv.InnerHtml = "Please select Time Zone."; return false;
                    script = "<script language='javascript'>PushAlert('Please select Time Zone.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void CmdOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && validation())
            {
                string _strInsertQuery = "";
                if (Session["_IDExists"] != null)
                {
                    _strInsertQuery = "Update ACS_ACCESSLEVEL set AL_DESCRIPTION='" + txtdescription.Text + "',AL_TIMEZONE_ID='" + cmbTimeZone.SelectedValue + "',AL_ISDELETED='False',AL_DELETEDDATE=null " +
                        "where AL_ID='" + txtalid.Text.Trim()+"'";
                                    
                }
                else
                { 
                        _strInsertQuery = "insert into ACS_ACCESSLEVEL(AL_ID,AL_DESCRIPTION,AL_TIMEZONE_ID,AL_ISDELETED,AL_DELETEDDATE) " +
                        " values(" + txtalid.Text.Trim() + ",'" + txtdescription.Text + "','" + cmbTimeZone.SelectedValue + "','False',null)";
             
                }
                Boolean _result = RunExecuteNonQuery(_strInsertQuery, _sqlConnection);
                string[] _ReaderController;
                //String _strALID = _result == true ? getValue("select SCOPE_IDENTITY()", _sqlConnection) : "";
                if (_result == true)
                {
                    if (RBLZone.SelectedValue == "Z")
                    {
                        _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,ALR_ISDELETED,ALR_DELETEDDATE) " +
                         " values('" + txtalid.Text.Trim() + "','" + cmbZone.SelectedValue + "','Z','False',null)";
                        _result = RunExecuteNonQuery(_strInsertQuery, _sqlConnection);
                    }
                    else
                    {
                        for (int i = 0; lstSReader.Items.Count > i; i++)
                        {
                            _ReaderController = lstSReader.Items[i].Value.ToString().Split('-');
                            _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,CONTROLLER_ID,ALR_ISDELETED,ALR_DELETEDDATE) " +
                                " values('" + txtalid.Text.Trim() + "','" + _ReaderController[1].ToString() + "','R'," + _ReaderController[0].ToString() + ",'False',null)";
                            _result = RunExecuteNonQuery(_strInsertQuery, _sqlConnection);
                        }
                    }
                    String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                    Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                    


                    this.messageDiv.InnerHtml = "Record Saved Successfully";
                    cmbZone.Items.Clear();
                    cmbTimeZone.Items.Clear();                   
                    lstAReader.Items.Clear();                    
                    lstSReader.Items.Clear();
                    PnlZone.Visible = false;
                    RBLZone.SelectedValue = null;
                    intializeControl();
                }
            }
            string someScript2 = "";
            someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);


        }
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            intializeControl();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessLevelBrowse.aspx");
        }
        //public Int16 getId()
        //{
        //    Int16 _apID;
        //    _apID = Convert.ToInt16(getValue("select ISNULL(min(AP_ID),0) from ACS_ACCESSPOINT where AP_ISDELETED='1'", _sqlConnection));
        //    if (_apID == 0)
        //    {
        //        _apID = Convert.ToInt16(getValue("select ISNULL(max(AP_ID)+1,1) from ACS_ACCESSPOINT where AP_ISDELETED='0'", _sqlConnection));
        //    }
        //    return _apID;
        //}
        public Int16 getId()
        {
            Int16 _apID;
            _apID = Convert.ToInt16(getValue("select ISNULL(min(AL_ID),0) from ACS_ACCESSLEVEL where AL_ISDELETED='1'", _sqlConnection));
            if (_apID == 0)
            {
                _apID = Convert.ToInt16(getValue("select ISNULL(max(AL_ID)+1,1) from ACS_ACCESSLEVEL where AL_ISDELETED='0'", _sqlConnection));
            }
            else
            {
                Session.Add("_IDExists", "TRUE");
            }
            return _apID;
        }

        protected void CVReaders_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RBLZone.SelectedValue == "R" && lstSReader.Items.Count == 0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
	}
}