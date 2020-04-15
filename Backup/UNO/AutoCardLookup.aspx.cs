using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UNO
{
    public partial class AutoCardLookup : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static DataTable _dtRecords = new DataTable();
        public static int _recordCounter = 0;
        public String _dtFromTime = "";
        public static String _evenType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.ImgPhoto.ImageUrl = "~//EmpImage//default.png";
            if (Page.IsPostBack == false)
            {
                intializeControl();
                this.ImgPhoto.ImageUrl = "~//EmpImage//default.png";
            }
        }

        public void intializeControl()
        {
            ctlTimer.Enabled = false;

            cmbReaders.Items.Clear();
            cmbReaders.Items.Add(new ListItem("Select Criteria", "-1"));
            cmbReaders.Items.Add(new ListItem("All", "All"));
            cmbReaders.SelectedValue = null;

            _evenType = getValue("select CODE from ENT_PARAMS where MODULE='ACS' and IDENTIFIER='EVENTTYPE' and [VALUE]='CARD'", _sqlConnection);
             
            try
            {
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
                        cmbReaders.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }

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

        public void showRecords()
        {
            try
            {
                if (_dtRecords != null && (_recordCounter <= _dtRecords.Rows.Count - 1))
                {
                    lblDateTime.Text = _dtRecords.Rows[_recordCounter]["Event_Datetime"].ToString();
                    lblCardNo.Text = _dtRecords.Rows[_recordCounter]["Event_Card_Code"].ToString();
                    lblName.Text = _dtRecords.Rows[_recordCounter]["Empname"].ToString();
                    if (_dtRecords.Rows[_recordCounter]["STATUS"].ToString() == "True") { lblCardStatus.ForeColor = System.Drawing.Color.Green; lblCardStatus.Text = "Valid Card"; }
                    else if (_dtRecords.Rows[_recordCounter]["STATUS"].ToString() == "False") { lblCardStatus.ForeColor = System.Drawing.Color.Red; lblCardStatus.Text = "Invalid Card"; }

                    if (_dtRecords.Rows[_recordCounter]["EPD_PHOTOURL"].ToString() != "")
                    {
                        this.ImgPhoto.ImageUrl = "~//EmpImage//" + _dtRecords.Rows[_recordCounter]["EPD_PHOTOURL"].ToString();
                    }
                    else
                    {
                        this.ImgPhoto.ImageUrl = "~//EmpImage//default.png";
                    }







                   // this.ImgPhoto.ImageUrl = "~//EmpImage//" + _dtRecords.Rows[_recordCounter]["EPD_PHOTOURL"].ToString();
                    
                    
                    
                    //if (_recordCounter % 2 == 0)
                    //    this.ImgPhoto.ImageUrl = "~//EmpPhoto//cmslogo.gif";
                    //else
                    //    this.ImgPhoto.ImageUrl = "~//EmpPhoto//Core.gif";
                    _recordCounter++;
                }
                else
                {
                    if (_dtRecords.Rows.Count != 0 || _dtRecords != null)
                    { 
                        _dtFromTime = Convert.ToDateTime(_dtRecords.Rows[_dtRecords.Rows.Count - 1]["Event_Datetime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        _recordCounter = 0;
                        if (ctlTimer.Enabled == true) { ctlTimer.Enabled = false; getRecords(_dtFromTime); ctlTimer.Enabled = true; }
                        else { getRecords(_dtFromTime); }
                    }
                }
            }
            catch (Exception ex)
            { }

        }

        protected void CmdNext_Click(object sender, EventArgs e)
        {
            showRecords();
        }

        protected void OnTimerIntervalElapse(object sender, EventArgs e)
        {           
            showRecords();  
        }

        protected void cmbReaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            _dtRecords = null;
            _recordCounter = 0;
            ctlTimer.Enabled = false;
            if (cmbReaders.SelectedValue.ToString().Trim() != "-1")
            {
              //getRecords("03/01/2013 09:52:11");
              getRecords( DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            }
            ctlTimer.Enabled = true;  
        }

        public void getRecords(String _strTime)
        {
            try
            {                           
                _dtFromTime = _strTime;
                String _whereClause = "";
                string[] _ReaderController;
                if (cmbReaders.SelectedValue.ToString().Trim() != "All")
                {
                    _ReaderController = cmbReaders.SelectedValue.ToString().Split('-');
                    _whereClause = "Event_Reader_ID='" + _ReaderController[1].ToString() + "' and Event_Controller_ID='" + _ReaderController[0].ToString() + "' and ";
                }

                String _strSelect = "";
                _strSelect = "select Event_Datetime,Event_Card_Code," +
                           "EPD_FIRST_NAME+' '+ cast(EPD_MIDDLE_NAME as varchar)+' '+EPD_LAST_NAME as Empname,[STATUS],EPD_PHOTOURL " + //,EPD_PHOTOURL
                           "from ACS_Events ,ENT_EMPLOYEE_PERSONAL_DTLS,ACS_CARD_CONFIG " +
                           "where " +
                           "EPD_CARD_ID =Event_Card_Code and CARD_CODE=Event_Card_Code and Event_Type='"+_evenType+"' and  " + _whereClause +
                           " Event_Datetime between '" + _strTime + "' and getdate() order by Event_Datetime asc";
                
                _dtRecords = getDataTable(_strSelect, _sqlConnection);               
            } 
            catch (Exception ex)
            { }
        }

        //protected void CmdPause_Click(object sender, EventArgs e)
        //{
        //    if (CmdPause.Text == "Release")
        //    {
        //        ctlTimer.Enabled = false;
        //        CmdPause.Text = "Start";
        //    }
        //    else
        //    {
        //        ctlTimer.Enabled = true;
        //        CmdPause.Text = "Release";
        //    } 
        //}

        protected void ChkBuffer_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBuffer.Checked==true)
            {
                ctlTimer.Enabled = false;               
            }
            else
            {
                if (cmbReaders.SelectedValue.ToString().Trim() != "-1")
                {
                    ctlTimer.Enabled = true;
                }
            }
        }

        
    }
}

