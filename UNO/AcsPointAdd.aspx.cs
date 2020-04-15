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
using System.Collections;


namespace UNO
{
    public partial class AcsPointAdd : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";            
            if (Page.IsPostBack == false)
            {                
                intializeControl();
            }
        }
        void handleAdd()
        {
            try
            {

                //string acsid = this.Request.Params["txtaccesspointid"];
                //this.messageDiv.InnerHtml = "Done successfully";
                //string m_messageString = "Done successfully";
                //Response.CacheControl = "no-cache";
                //Response.ContentType = "text/xml";
                //Response.StatusDescription = "Done successfully";
                //Response.StatusCode = 200;
                //ReportWriter w = new ReportWriter();
                //w.AddXMLTag("messageDiv", m_messageString);
                //string strReport = w.Close();
                //Response.Write(strReport);  
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {

                Response.CacheControl = "no-cache";
                Response.ContentType = "text/xml";
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public Int16 getId()
        {
            Int16 _apID;
            _apID = Convert.ToInt16(getValue("select ISNULL(min(AP_ID),0) from ACS_ACCESSPOINT where AP_ISDELETED='1'", _sqlConnection));
            if (_apID == 0)
            {
                _apID = Convert.ToInt16(getValue("select ISNULL(max(AP_ID)+1,1) from ACS_ACCESSPOINT where AP_ISDELETED='0'", _sqlConnection));                
            }
            else
            {
                Session.Add("_IDExists", "TRUE");              
            }
            return _apID;
        }
        public void intializeControl()
        {
           // txtaccesspointid.Text = getValue("select ISNULL(max(AP_ID)+1,1) as id from ACS_ACCESSPOINT", _sqlConnection);
            Session.Remove("_IDExists");
            txtaccesspointid.Text=Convert.ToString(getId());
            txtdescription.Text = "";
            cmbAccessPointType.Items.Clear();
            cmbAccessPointType.Items.Add(new ListItem("Select Type", "-1"));
            cmbAccessPointType.SelectedValue = null;

            cmbContrller.Items.Clear();
            cmbContrller.Items.Add(new ListItem("Select One", "-1"));
            cmbContrller.SelectedValue = null;

            try
            {
                //string _strsql = "select CTLR_ID,CTLR_DESCRIPTION from ACS_CONTROLLER where CTLR_ID not in (select AP_CONTROLLER_ID from ACS_ACCESSPOINT where AP_CONTROLLER_ID is not null) ";
                string _strsql = "select CTLR_ID,CTLR_DESCRIPTION from ACS_CONTROLLER A " +
                                "where A.CTLR_ISDELETED='0' and CTLR_ID in " +
                                "(select distinct(CTLR_ID) from ACS_READER where READER_ISDELETED='0' and READER_ID not in  " +
                                "(select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where APR_ISDELETED='0' AND AP_CONTROLLER_ID=A.CTLR_ID)" +
                                " union " +
                                " select distinct(CTLR_ID) from ACS_DOOR  " +
                                " where DOOR_ISDELETED='0' and DOOR_ID not in (select distinct(DOOR_ID) from ACS_ACCESSPOINT_RELATION where APR_ISDELETED='0' AND AP_CONTROLLER_ID=A.CTLR_ID))";
     
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbContrller.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (cmbContrller.Items.Count != 0)
            {
               // cmbContrller.Items[0].Selected = true;
                fillList();
            }

            try
            {
                string _strsql = "select CODE,VALUE from ENT_PARAMS where MODULE='ACS' and IDENTIFIER='ACCESSPOINTTYPE'";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbAccessPointType.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        public void Display(string Message)
        {
            CVLSTReader.IsValid = false;
            CVLSTReader.ErrorMessage = Message;
            //CVLSTReader.ValidationGroup = "grp1";
            //CVLSTReader.Text = "&nbsp;";
	 
	        //Page currentPage = HttpContext.Current.Handler as Page;
            //currentPage.Validators.Add(CVLSTReader);
        }
        public void DisplayDoor(string Message)
        {
            CVDoor.IsValid = false;
            CVDoor.ErrorMessage = Message;
            //CVLSTReader.ValidationGroup = "grp1";
            //CVLSTReader.Text = "&nbsp;";

            //Page currentPage = HttpContext.Current.Handler as Page;
            //currentPage.Validators.Add(CVLSTReader);
        }
        public Boolean checkR()
        {
            String _value = cmbAccessPointType.SelectedValue;
            if (_value == "OROD" && lstSReader.Items.Count == 1)
            {
                //this.messageDiv.InnerHtml = "You can select only one reader";
                Display("You can select only one reader");
                lstAReader.SelectedValue = null;          
                return false;
            }
            else if (_value == "TROD" && lstSReader.Items.Count == 2)
            {
                //this.messageDiv.InnerHtml = "You can select only two reader";                
                Display("You can select only two readers");
                lstAReader.SelectedValue = null;
                return false;
            }
            else if (_value == "ROND" && lstSReader.Items.Count == 1)
            {
                //this.messageDiv.InnerHtml = "You can select only one reader";
                Display("You can select only one reader");
                lstAReader.SelectedValue = null;
                return false;
            }
            return true;
        }
        public Boolean checkD()
        {
            String _value = cmbAccessPointType.SelectedValue;
            if (_value == "OROD" && lstSDoor.Items.Count == 1)
            {
                //this.messageDiv.InnerHtml = "You can select only one door";
                DisplayDoor("You can select only one door");
                lstADoor.SelectedValue = null;
                return false;
            }
            else if (_value == "TROD" && lstSDoor.Items.Count == 1)
            {
                //this.messageDiv.InnerHtml = "You can select only one door";
                DisplayDoor("You can select only one door");
                lstADoor.SelectedValue = null;
                return false;
            }
            else if (_value == "ROND" && lstSDoor.Items.Count == 0)
            {
                //this.messageDiv.InnerHtml = "You can not select any door";
                DisplayDoor("You can not select any door");
                lstADoor.SelectedValue = null;
                return false;
            }
            return true;
        }
        public Boolean checkRValidation()
        {
            String _value = cmbAccessPointType.SelectedValue;

            if (_value == "OROD" && lstSReader.Items.Count < 1)
            {
                //this.messageDiv.InnerHtml = "Please select one reader";
                Display("Please select one reader"); 
                return false;
            }
            else if (_value == "OROD" && lstSReader.Items.Count > 1)
            {
                //this.messageDiv.InnerHtml = "You can select only one reader";
                Display("You can select only one reader");
                return false;
            }

            else if (_value == "TROD" && lstSReader.Items.Count < 2)
            {
                //this.messageDiv.InnerHtml = "Please select two reader";
                Display("Please select two reader");
                return false;
            }
            else if (_value == "TROD" && lstSReader.Items.Count > 2)
            {
               //this.messageDiv.InnerHtml = "Please select only two reader";
                Display("Please select only two reader");
                return false;
            }
            else if (_value == "ROND" && lstSReader.Items.Count < 1)
            {
                //this.messageDiv.InnerHtml = "Please select one reader";
                Display("Please select one reader");
                return false;
            }
            else if (_value == "ROND" && lstSReader.Items.Count > 1)
            {
                //this.messageDiv.InnerHtml = "Please select only one reader";
                Display("Please select only one reader");
                return false;
            }

            return true;
        }
        public Boolean checkDValidation()
        {

            String _value = cmbAccessPointType.SelectedValue;
            if (_value == "OROD" && lstSDoor.Items.Count < 1)
            {
                //this.messageDiv.InnerHtml = "Please select one door";
                DisplayDoor("Please select one door");
                return false;
            }
            else if (_value == "OROD" && lstSDoor.Items.Count > 1)
            {
                
                //this.messageDiv.InnerHtml = "Please select only one door";
                DisplayDoor("Please select only one door");
                return false;
            }
            else if (_value == "TROD" && lstSDoor.Items.Count < 1)
            {
                //this.messageDiv.InnerHtml = "Please select one door";
                DisplayDoor("Please select one door");
                return false;
            }
            else if (_value == "TROD" && lstSDoor.Items.Count > 1)
            {
                //this.messageDiv.InnerHtml = "Please select only one door";
                DisplayDoor("Please select only one door");
                return false;
            }
            else if (_value == "ROND" && lstSDoor.Items.Count > 0)
            {
                //this.messageDiv.InnerHtml = "Please do not select any door";
                DisplayDoor("Please do not select any door");
                return false;
            }
            return true;
        }
        protected void cmdReaderRight_Click(object sender, EventArgs e)
        {            
                if (lstAReader.SelectedItem != null)
                {
                    if (checkR())
                    {
                        lstSReader.Items.Add(lstAReader.SelectedItem);
                        lstAReader.Items.Remove(lstAReader.SelectedItem);
                        lstSReader.SelectedValue = null;
                    }
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
        protected void CmdDoorRight_Click(object sender, EventArgs e)
        {
            if (lstADoor.SelectedItem != null)
            {
                if (checkD())
                {
                    lstSDoor.Items.Add(lstADoor.SelectedItem);
                    lstADoor.Items.Remove(lstADoor.SelectedItem);
                    lstSDoor.SelectedValue = null;
                }
            }
        }
        protected void CmdDoorLeft_Click(object sender, EventArgs e)
        {
            if (lstSDoor.SelectedItem != null)
            {
                lstADoor.Items.Add(lstSDoor.SelectedItem);
                lstSDoor.Items.Remove(lstSDoor.SelectedItem);
                lstADoor.SelectedValue = null;
            }
        }
        public Boolean checkId(String _strId)
        {
            try
            {
                String _result = getValue("Select AP_ID from ACS_ACCESSPOINT where AP_ID='" + _strId + "'", _sqlConnection);
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
                //if (txtdescription.Text.Trim() == "")
                //{ this.messageDiv.InnerHtml = "Please enter Description"; return false; }
                //else if (cmbContrller.SelectedValue == "-1")
                //{ this.messageDiv.InnerHtml = "Please select Controller"; return false; }
                //else if (cmbAccessPointType.SelectedValue == "-1")
                //{ this.messageDiv.InnerHtml = "Please select Access Point Type"; return false; }
                //else 
                    
                if (!checkRValidation())
                    return false;
                else if (!checkDValidation())
                    return false;
                //else
                //{
                //    string someScript = "";
                //    someScript = "<script language='javascript'>Confirm();</script>";
                //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //    //Page.ClientScript.RegisterStartupScript(this.GetType(),
                //    String confirmValue = Request.Form["confirm_value"];
                //    if (confirmValue == "Yes")
                //    { this.messageDiv.InnerHtml = "OK"; }
                //    else
                //    { this.messageDiv.InnerHtml = "Cancel"; }                   
                //}
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void CmdOk_Click(object sender, EventArgs e)
        {

            ////validation();            

         


            if (Page.IsValid && validation())
            {

                string _strInsertQuery = "";
                if (Session["_IDExists"] != null)
                {
                    _strInsertQuery = "Update ACS_ACCESSPOINT set AP_DESCRIPTION ='" + txtdescription.Text +
                                      "',AP_TYPE='" + cmbAccessPointType.SelectedValue + "',AP_ISDELETED='False',AP_DELETEDDATE=null where AP_ID='" + txtaccesspointid.Text + "'";
                }
                else
                {
                    _strInsertQuery = "Insert into ACS_ACCESSPOINT(AP_ID,AP_DESCRIPTION,AP_TYPE,AP_ISDELETED,AP_DELETEDDATE)" +
                                      " values('" + txtaccesspointid.Text + "','" + txtdescription.Text + "','" + cmbAccessPointType.SelectedValue + "','False',null)";
                }

                Boolean _result = RunExecuteNonQuery(_strInsertQuery, _sqlConnection);
                //String _strAPID = _result == true ? getValue("select SCOPE_IDENTITY()", _sqlConnection) : "";
                if (_result == true)
                {

                    String _doorId = "";
                    if (lstSDoor.Items.Count > 0)
                        _doorId = lstSDoor.Items[0].Value;
                    if (_doorId == "")
                        _doorId = "null";

                    for (int i = 0; lstSReader.Items.Count > i; i++)
                    {
                        _strInsertQuery = "Insert into ACS_ACCESSPOINT_RELATION(AP_ID,READER_ID,DOOR_ID,AP_CONTROLLER_ID,APR_ISDELETED,APR_DELETEDDATE) " +
                            " values(" + txtaccesspointid.Text + "," + lstSReader.Items[i].Value + "," + _doorId + "," + cmbContrller.SelectedValue + ",'False',null)";
                        _result = RunExecuteNonQuery(_strInsertQuery, _sqlConnection);
                    }
                    this.messageDiv.InnerHtml = "Record Saved Successfully";


                    String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='APC'";
                    Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);


                    //string someScript = "";
                    //someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",2000);</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                    //this.messageDiv.InnerHtml = "Record Saved Successfully";
                    cmbAccessPointType.Items.Clear();
                    cmbContrller.Items.Clear();
                    lstADoor.Items.Clear();
                    lstAReader.Items.Clear();
                    lstSDoor.Items.Clear();
                    lstSReader.Items.Clear();
                    intializeControl();

                }
            }
            else
            {

                //string someScript2 = "";
                //someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
            }

            string someScript2 = "";
            someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
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
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
             
            intializeControl(); 
        }
        protected void cmbContrller_SelectedIndexChanged(object sender, EventArgs e)
        {

            fillList();
        }
        public void fillList()
        {
            lstADoor.Items.Clear();
            lstAReader.Items.Clear();
            lstSDoor.Items.Clear();
            lstSReader.Items.Clear();

            try
            {

                string _strsql = "select READER_ID,READER_DESCRIPTION from ACS_READER where READER_ID  " +
                                 "not in (select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where AP_CONTROLLER_ID='" + cmbContrller.SelectedValue +"' " +
                                 "and APR_ISDELETED='0') and CTLR_ID='" + cmbContrller.SelectedValue + "' and READER_ISDELETED='0'";
                                  //" READER_ID in (select READER_ID from ACS_READER where CTLR_ID='" + cmbContrller.SelectedValue + "')";
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

            try
            {

                string _strsql = "select DOOR_ID,DOOR_DESCRIPTION from ACS_DOOR where  DOOR_ID " +
                                  "not in (select distinct(DOOR_ID)  from ACS_ACCESSPOINT_RELATION  where AP_CONTROLLER_ID='" + cmbContrller.SelectedValue + "'" +
                                  " and APR_ISDELETED='0') and CTLR_ID='" + cmbContrller.SelectedValue + "' and DOOR_ISDELETED='0'";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstADoor.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("_IDExists");            
            Response.Redirect("~/AcsPointBrowse.aspx");  
        }
        protected void CVLSTReader_ServerValidate(object source, ServerValidateEventArgs args)
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
        protected void cmdAllReaderRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAReader.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSReader.Items.Add(_li);

            }
            lstAReader.Items.Clear();
        }
        protected void cmdAllReaderLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSReader.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAReader.Items.Add(_li);
            }
            lstSReader.Items.Clear();
        }
        protected void CmdAllDoorRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstADoor.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSDoor.Items.Add(_li);

            }
            lstADoor.Items.Clear();
        }
        protected void CmdAllDoorLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSDoor.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstADoor.Items.Add(_li);
            }
            lstSDoor.Items.Clear();
        }
        protected void cmbAccessPointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccessPointType.SelectedValue == "ROND")
            {
                lstADoor.Enabled = false;
                lstSDoor.Enabled = false;
                CmdDoorLeft.Enabled = false;
                CmdDoorRight.Enabled = false;
                IEnumerator _ie = lstSDoor.Items.GetEnumerator();
                while (_ie.MoveNext())
                {
                    ListItem _li = (ListItem)_ie.Current;
                    lstADoor.Items.Add(_li);
                }
                lstSDoor.Items.Clear();

            }
            else
            {
                lstADoor.Enabled = true;
                lstSDoor.Enabled = true;
                CmdDoorLeft.Enabled = true;
                CmdDoorRight.Enabled = true;
            }
        }

       
       
    }
}