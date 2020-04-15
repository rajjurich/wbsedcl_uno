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
    public partial class ZoneEdit : System.Web.UI.Page
    {
        public String _strId = "";
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());    
        protected void Page_Load(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";
            if (Page.IsPostBack == false)
            {
                _strId = Request.QueryString["id"].ToString();
                Session.Add("ZONE_ID", _strId);
                intializeControl();
            }
        }
        public void intializeControl()
        {
            _strId = Session["ZONE_ID"].ToString();
            DataTable _dt = new DataTable();
            _dt = getDataTable("select ZONE_ID,ZONE_DESCRIPTION from ZONE where ZONE_ID='" + _strId + "'", _sqlConnection);
            
            lstAReader.Items.Clear();
            lstSReader.Items.Clear();

            txtzoneid.Text = _dt.Rows[0]["ZONE_ID"].ToString();
            txtdescription.Text = _dt.Rows[0]["ZONE_DESCRIPTION"].ToString();

            // available reader
            try
            {
                //string _strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER where  " +
                //                 "READER_ID in (select distinct(READER_ID) from  ACS_ACCESSPOINT_RELATION where APR_ISDELETED='0') " +
                //                 "and READER_ID not in (select distinct(READER_ID) from ZONE_READER_REL where ZONE_ID='" + _strId + "' and ZONER_ISDELETED='0') and READER_ISDELETED='0' ";
                //string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                //                 "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //                 "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                //                 "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                //                 "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0' " +
                //                 ") " +
                //                 "and  (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) " +
                //                 "not in( " +
                //                 "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) " +
                //                 "from ZONE_READER_REL ZR where ZONE_ID='"+ _strId+"' and ZONER_ISDELETED='0') ";

                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                                 "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                                 "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                                 "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                                 "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0' " +
                                 ") " +
                                 "and  (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) " +
                                 "not in( " +
                                 "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) " +
                                 "from ZONE_READER_REL ZR where ZONE_ID='" + _strId + "' and ZONER_ISDELETED='0') ";


                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReader.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //selected reader
            try
            {

                //string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, (AR.READER_DESCRIPTION+'-'+AC.CTLR_DESCRIPTION) as DESCRIPTION " +
                //                   "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                //                   "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                //                   "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar))  " +
                //                   "from ZONE_READER_REL ZR where ZONE_ID='" + _strId + "' and ZONER_ISDELETED='0' " +
                //                   ") ";
                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                                  "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                                  "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                                  "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar))  " +
                                  "from ZONE_READER_REL ZR where ZONE_ID='" + _strId + "' and ZONER_ISDELETED='0' " +
                                  ") ";


                                   
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstSReader.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void cmdReaderRight_Click(object sender, EventArgs e)
        {
            if (lstAReader.SelectedItem != null)
            {                
                    lstSReader.Items.Add(lstAReader.SelectedItem);
                    lstAReader.Items.Remove(lstAReader.SelectedItem);
                    lstSReader.SelectedValue = null;
            }
        }
        protected void cmdReaderLeft_Click(object sender, EventArgs e)
        {
            if (lstSReader.SelectedItem != null)
            {
                lstAReader.Items.Add(lstSReader.SelectedItem);
                lstSReader.Items.Remove(lstSReader.SelectedItem);
                lstAReader.SelectedValue = null;
            }
        }
        public Boolean validation()
        {
            try
            {
                if (txtzoneid.Text.Trim() == "")
                { this.messageDiv.InnerHtml = "Zone ID already exist"; return false; }
                else if (txtdescription.Text.Trim() == "")
                { this.messageDiv.InnerHtml = "Please enter Description"; return false; }
                else if (lstSReader.Items.Count == 0)
                { this.messageDiv.InnerHtml = "Please select reader"; return false; }
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
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();
                SqlTransaction _trans = _sqlConnection.BeginTransaction();
                string _strInsertQuery = "";
                Boolean _result;
                try
                {
                    string[] _ReaderController;
                    _strInsertQuery = "";

                    _strInsertQuery = "update ZONE set ZONE_DESCRIPTION='" + txtdescription.Text.Trim() + "' where ZONE_ID='" + Session["ZONE_ID"].ToString() + "'"; 
                    _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                    
                    if (_result == true)
                    {
                        _result = RunExecuteNonQueryWithTransaction("delete from ZONE_READER_REL where ZONE_ID='" + Session["ZONE_ID"].ToString() + "'", _sqlConnection, _trans);
                                        
                        for (int i = 0; lstSReader.Items.Count > i; i++)
                        {
                            _ReaderController = lstSReader.Items[i].Value.ToString().Split('-');

                            //_strInsertQuery = "insert into ZONE_READER_REL(ZONE_ID,READER_ID,ZONER_ISDELETED,ZONER_DELETEDDATE) " +
                            //    " values('" + txtzoneid.Text.Trim() + "','" + lstSReader.Items[i].Value + "','False',null)";
                            _strInsertQuery = "Insert into ZONE_READER_REL(ZONE_ID,CONTROLLER_ID,READER_ID,ZONER_ISDELETED,ZONER_DELETEDDATE) " +
                              "values('" + txtzoneid.Text.Trim() + "','" + _ReaderController[0].ToString() + "','" + _ReaderController[1].ToString() + "','False',null)";
                       
                            _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, _sqlConnection, _trans);
                        }

                        _trans.Commit();
                        _trans.Dispose();
                        this.messageDiv.InnerHtml = "Record Saved Successfully";
                        string someScript = "";
                        someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                        //cmbAccessPointType.Items.Clear();
                        //lstADoor.Items.Clear();
                        //lstAReader.Items.Clear();
                        //lstSDoor.Items.Clear();
                        //lstSReader.Items.Clear();
                        // intializeControl();
                    }
                    //String _strId = Session["AP_ID"].ToString();

                }
                catch (Exception ex)
                {
                    //_trans.Rollback();
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
            Session.Remove("ZONE_ID");
            Response.Redirect("~/ZoneBrowse.aspx");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lstSReader.Items.Count == 0)
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