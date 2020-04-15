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

namespace UNO
{
    public partial class AccessLevelEdit : System.Web.UI.Page
    {
        public String _strId = "";
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";
            if (Page.IsPostBack == false)
            {
                PnlZone.Visible = false;
                _strId = Request.QueryString["id"].ToString();
                Session.Add("AL_ID", _strId);
                intializeControl();
            }
        }
        public void intializeControl()
        {
            _strId = Session["AL_ID"].ToString();
            DataTable _dt = new DataTable();
            DataTable _dtALRelation = new DataTable();

            _dt = getDataTable("select * from ACS_ACCESSLEVEL where AL_ID='" + _strId + "'", _sqlConnection);

            txtalid.Text = _dt.Rows[0]["AL_ID"].ToString();
            txtdescription.Text = _dt.Rows[0]["AL_DESCRIPTION"].ToString();

            //String _entityType = _dt.Rows[0]["AL_ENTITY_TYPE"].ToString();
            String _entityType = getValue("select distinct(AL_ENTITY_TYPE) from ACS_ACCESSLEVEL_RELATION where AL_ID='" + _strId + "'", _sqlConnection);
            


            String _timeZoneID = _dt.Rows[0]["AL_TIMEZONE_ID"].ToString();

            FillList(_entityType);

           //Filling Time Zone
            cmbTimeZone.Items.Clear();
            cmbTimeZone.Items.Add(new ListItem("Select Time Zone", "-1"));
            try
            {
                String _strsql = "select Distinct(TZ_CODE),TZ_DESCRIPTION from ACS_TIMEZONE where TZ_ISDELETED='0'";
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
            cmbTimeZone.SelectedValue = _timeZoneID;

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
                //                 "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //                 "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                //                 "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                //                 "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                //                 ")";

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
                //                 "in (select READER_ID from ZONE_READER_REL where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0') and READER_ISDELETED='0'";

                //string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                //               "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //               "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
                //               "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
                //               "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0'" +
                //               ")";
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
        public void FillList(String _entityType)
        {
            DataTable _dt = new DataTable();
            DataTable _dtALRelation = new DataTable();
            string _strsql ="";
            _strsql="select * from ACS_ACCESSLEVEL_RELATION where AL_ID='" + _strId +"'";
            _dtALRelation = getDataTable(_strsql, _sqlConnection);
            cmbZone.Items.Clear();
                try
                {
                    cmbZone.Items.Add(new ListItem("Select Zone", "-1"));
                    _strsql = "select ZONE_ID,ZONE_DESCRIPTION from ZONE where ZONE_ISDELETED='0'";
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

            if (_entityType == "Z")
            {
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
                lblSelected.Visible = false;    
                lstSReader.Visible = false;
                cmdReaderLeft.Visible = false;
                cmdReaderRight.Visible = false;                
                lblAvailable.Text = "Readers Included in Zone";
                lstAReader.Visible = true;
                PnlZone.Visible = true;
                RBLZone.SelectedValue = "Z";
                
                cmbZone.SelectedValue = _dtALRelation.Rows[0]["RD_ZN_ID"].ToString();
                try
                {

                    //_strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER   " +
                    //                 "where READER_ID in (select READER_ID from ZONE_READER_REL where ZONE_ID='" + cmbZone.SelectedValue.ToString() + "' )";


                    //_strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                    //           "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                    //           "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
                    //           "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
                    //           "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0'" +
                    //           ")";

                    _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                              "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
                              "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
                              "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZone.SelectedValue + "' and ZONER_ISDELETED='0'" +
                              ")";

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
            else
            {
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
                lblSelected.Visible = true;
                lstSReader.Visible = true;
                cmdReaderLeft.Visible = true;
                cmdReaderRight.Visible = true;
                lblAvailable.Text = "Available";
                lstAReader.Visible = true;
                PnlZone.Visible = false;               
                RBLZone.SelectedValue = "R";
                try
                {

                    //_strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER where READER_ID in (select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where APR_ISDELETED='0' )   " +
                    //                 "and  READER_ID not in (select  RD_ZN_ID from ACS_ACCESSLEVEL_RELATION where AL_ID='" + txtalid.Text.Trim() + "' and ALR_ISDELETED='0') and READER_ISDELETED='0'";

                    //_strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                    //          "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                    //          "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( "+
                    //          "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID "+
                    //          "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0' "+
                    //          ") "+
                    //          "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) not in(  "+
                    //          "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID "+
                    //          "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='"+ _strId+"' and ALR_ISDELETED='0' "+
                    //          ")";

                    _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                              "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                              "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                              "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0' " +
                              ") " +
                              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) not in(  " +
                              "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID " +
                              "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='" + _strId + "' and ALR_ISDELETED='0' " +
                              ")";

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

                try
                {

                    //_strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                    //          "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +                              
                    //          "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(  " +
                    //          "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID " +
                    //          "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='" + _strId + "' and ALR_ISDELETED='0' " +
                    //          ")";


                    _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                              "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(  " +
                              "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID " +
                              "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='" + _strId + "' and ALR_ISDELETED='0' " +
                              ")";
                    _dt = getDataTable(_strsql, _sqlConnection);
                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            lstSReader.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }


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
        public bool RunExecuteNonQueryWithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.Transaction = _sqlTrans;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                _sqlTrans.Rollback();
                return false;
            }
        }
        protected void RBLZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBLZone.SelectedValue == "Z")
            {
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
                fillZoneReaders();
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
        protected void cmbZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZone.SelectedValue != "-1")
            {
                fillZoneReaders();
            }
            else
            {
                lstAReader.Items.Clear();
                lstSReader.Items.Clear();
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
        public Boolean validation()
        {
            try
            {
                if (txtalid.Text.Trim() == "")
                { this.messageDiv.InnerHtml = "Please enter Access point ID"; return false; }                
                else if (txtdescription.Text.Trim() == "")
                { this.messageDiv.InnerHtml = "Please enter Description"; return false; }
                else if (RBLZone.SelectedValue == "R" && lstSReader.Items.Count == 0)
                { this.messageDiv.InnerHtml = "Please select Reader(s)."; return false; }
                else if (RBLZone.SelectedValue == "Z" && lstAReader.Items.Count == 0)
                { this.messageDiv.InnerHtml = "Please select valid Zone"; return false; }
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
                this.messageDiv.InnerHtml = "Validation Done";
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();
                SqlTransaction _trans = _sqlConnection.BeginTransaction();
                string _strInsertQuery = "";
                Boolean _result;
                try
                {

                    _strInsertQuery = "";
                    _strInsertQuery = "Update ACS_ACCESSLEVEL set AL_DESCRIPTION='" + txtdescription.Text.Trim() + "'," +
                                      " AL_TIMEZONE_ID='" + cmbTimeZone.SelectedValue + "' where AL_ID='" + Session["AL_ID"].ToString() + "'";
                    _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                    string[] _ReaderController;

                    if (_result == true)
                    {
                        _result = RunExecuteNonQueryWithTransaction("delete from ACS_ACCESSLEVEL_RELATION where AL_ID='" + Session["AL_ID"].ToString() + "'", _sqlConnection, _trans);


                        if (_result == true)
                        {
                            if (RBLZone.SelectedValue == "Z")
                            {
                                _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,ALR_ISDELETED,ALR_DELETEDDATE) " +
                                                  " values('" + txtalid.Text.Trim() + "','" + cmbZone.SelectedValue + "','Z','False',null)";
                                _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                            }
                            else
                            {
                                for (int i = 0; lstSReader.Items.Count > i; i++)
                                {
                                    _ReaderController = lstSReader.Items[i].Value.ToString().Split('-');
                                    _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,CONTROLLER_ID,ALR_ISDELETED,ALR_DELETEDDATE) " +
                                                        " values('" + txtalid.Text.Trim() + "','" + _ReaderController[1].ToString() + "','R'," + _ReaderController[0].ToString() + ",'False',null)";
                                    _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                                }
                            }
                        }




                        //for (int i = 0; lstSReader.Items.Count > i; i++)
                        //{
                        //    _strInsertQuery = "Insert into ACS_ACCESSPOINT_RELATION(AP_ID,READER_ID,DOOR_ID,APR_ISDELETED,APR_DELETEDDATE) " +
                        //        " values('" + txtaccesspointid.Text.Trim() + "','" + lstSReader.Items[i].Value + "','" + _doorId + "','False',null)";
                        //    _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                        //}

                        _trans.Commit();
                        _trans.Dispose();

                        String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                        Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                       


                        this.messageDiv.InnerHtml = "Record Saved Successfully";
                        PnlZone.Visible = false;
                        RBLZone.SelectedValue = null;
                        string someScript = "";
                        someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                    }
                    
                }
                catch (Exception ex)
                {
                    _trans.Rollback();
                }

            }
            else
            {
                string someScript2 = "";
                someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
            }
        }
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            intializeControl();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("AL_ID");
            Response.Redirect("~/AccessLevelBrowse.aspx");
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