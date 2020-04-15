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
    
    public partial class UserAccessPermissionEdit : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public String _strId = "";
        public String _strList = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";
            if (Page.IsPostBack == false)
            {
                _strId = Request.QueryString["id"].ToString();
                Session.Add("UAP_ID", _strId);
                intializeControl();
            }
        }
        public void intializeControl()
        {
            _strId = Session["UAP_ID"].ToString();
            cmbEntity.Items.Clear();
            //cmbEntity.Items.Add(new ListItem("Select Type", "-1"));
            //cmbEntity.SelectedValue = "-1";
            cmbEntity.Items.Add(new ListItem("EMPLOYEE", "EMP"));
            lstAAL.Items.Clear();
            lstSAL.Items.Clear();
            lstAEntity.Items.Clear();
            lstSEntity.Items.Clear();

            //cmbEntity.SelectedValue = "EMP";
            DataTable _dt = new DataTable();

            try
            {
                string _strsql = "select CODE,VALUE from ENT_PARAMS where identifier='COMMONMASTERS' and module='ENT' Order by [value]";
                
                _dt = getDataTable(_strsql, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        cmbEntity.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            String _entityType = getValue("select distinct(ENTITY_TYPE) from EAL_CONFIG where ENTITY_ID='" + _strId + "' and FLAG <>'3'", _sqlConnection);
            cmbEntity.SelectedValue = _entityType;



            String _strSelect = "";
            if (cmbEntity.SelectedValue == "EMP")
            {
                //_strSelect = "select EPD_EMPID as ID,EPD_FIRST_NAME+ ' ' + EPD_MIDDLE_NAME + ' '+EPD_LAST_NAME + '-' +  EPD_EMPID as EMPNAME from ENT_EMPLOYEE_PERSONAL_DTLS " +
                //             "where EPD_EMPID ='" + _strId + "'";
                _strSelect = "select EPD_EMPID as ID,replace(convert(char(28),ltrim(SUBSTRING(EPD_FIRST_NAME+ ' '+ EPD_LAST_NAME ,1,10)))+(replicate('0', 10 - len(EPD_EMPID)) + rtrim(EPD_EMPID)),' ',' ' ) as EMPNAME from ENT_EMPLOYEE_PERSONAL_DTLS " +
                             "where EPD_EMPID ='" + _strId + "'";
            }
            else
            {
                _strSelect = "select  OCE_ID,OCE_DESCRIPTION from ENT_ORG_COMMON_ENTITIES where CEM_ENTITY_ID='" + cmbEntity.SelectedValue + "' " +
                             "and OCE_ID ='" + _strId +"'" ;
            }

            _dt = getDataTable(_strSelect, _sqlConnection);

            lstAEntity.Items.Add(new ListItem(_dt.Rows[0][1].ToString(),_dt.Rows[0][0].ToString()));
           
            
            ArrayList _level = new ArrayList();
            try
            {
                _strSelect="";
                _strSelect = "select distinct (E.AL_ID),AL.AL_DESCRIPTION from EAL_CONFIG E,ACS_ACCESSLEVEL AL where E.ENTITY_TYPE='" + _entityType + "' and E.ENTITY_ID='" + _strId + "' and E.AL_ID=AL.AL_ID and E.ISDELETED='0'";
                _dt = getDataTable(_strSelect, _sqlConnection);                
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstSAL.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));                        
                        _level.Add(_dt.Rows[i][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Session.Add("LEVEL", _level);

            //levelCount = lstSAL.Items.Count;

            try
            {
                _strSelect = "select AL_ID,AL_DESCRIPTION from ACS_ACCESSLEVEL where AL_ISDELETED='0' and AL_ID not in " +
                                    "(select distinct (E.AL_ID)from EAL_CONFIG E,ACS_ACCESSLEVEL AL " +
                                    "where E.ENTITY_TYPE='" + _entityType +"' and E.ENTITY_ID='" + _strId +"' and E.AL_ID=AL.AL_ID and E.ISDELETED='0') ";
                _dt = getDataTable(_strSelect, _sqlConnection);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAAL.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
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
        public DataTable getDataTablewithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.Transaction = _sqlTrans;
                _sc.CommandText = _strQuery;

                SqlDataAdapter _sqa = new SqlDataAdapter(_sc);
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
        public bool RunExecuteNonQueryWithTransaction(string _strQuery, SqlConnection _sqlconn,SqlTransaction _sqlTrans)
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
        protected void cmdALLALRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAAL.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSAL.Items.Add(_li);

            }
            lstAAL.Items.Clear();
            lstSAL.SelectedValue = null;
        }
        protected void cmdALRight_Click(object sender, EventArgs e)
        {
            for (int i = lstAAL.Items.Count - 1; i >= 0; i--)
            {
                if (lstAAL.Items[i].Selected == true)
                {
                    lstSAL.Items.Add(lstAAL.Items[i]);
                    ListItem li = lstAAL.Items[i];
                    lstAAL.Items.Remove(li);
                }
            }
            lstSAL.SelectedValue = null;
        }
        protected void cmdALLeft_Click(object sender, EventArgs e)
        {
            for (int i = lstSAL.Items.Count - 1; i >= 0; i--)
            {
                if (lstSAL.Items[i].Selected == true)
                {
                    lstAAL.Items.Add(lstSAL.Items[i]);
                    ListItem li = lstSAL.Items[i];
                    lstSAL.Items.Remove(li);
                }
            }
            lstAAL.SelectedValue = null;
        }
        protected void cmdALLALLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSAL.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAAL.Items.Add(_li);
            }
            lstSAL.Items.Clear();
            lstAAL.SelectedValue = null;
        }
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            Session.Remove("LEVEL");
            intializeControl();
        }
        public Boolean checkEntityAccessPermission( String _strAL)
        {
            try
            {

                _strList = "";
                // string[] _empCode;
                //if (cmbEntity.SelectedValue == "EMP")
                //{
                //    for (int i = 0; i <= lstAEntity.Items.Count - 1; i++)
                //    {
                //        _empCode = lstAEntity.Items[i].Value.ToString().Split('-');
                //        _strList = _strList + "'" + _empCode[0].ToString() + "',";
                //    }
                //    _strList = _strList.Substring(0, (_strList.Length - 1));
                //}
                //else
                //{
                //    for (int i = 0; i <= lstAEntity.Items.Count - 1; i++)
                //    {
                //        _strList = _strList + "'" + lstSEntity.Items[i].Value + "',";
                //    }
                //    _strList = _strList.Substring(0, (_strList.Length - 1));
                //}

                _strList = "'" + lstAEntity.Items[0].Value.ToString() + "'";

                String _strSelect = "";
                _strSelect =   "select EMPLOYEE_CODE,LevelCount " +
                               "from (" +
                               "select EMPLOYEE_CODE,count(*) LevelCount " +
                               "from ( " +
                               "select AL_ID,EMPLOYEE_CODE ,count(*)  cnt " +
                               "from EAL_CONFIG " +
                               "where EMPLOYEE_CODE in ";

                if (cmbEntity.SelectedValue == "EMP")
                {
                    _strSelect = _strSelect + "(" + _strList.Trim() + ") ";
                }
                else if (cmbEntity.SelectedValue == "CAT")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "DEP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DEPARTMENT_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "DES")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "DIV")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "GRD")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "GRP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (cmbEntity.SelectedValue == "LOC")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }

                _strSelect = _strSelect + "and AL_ID not in( " + _strAL + " ) and entity_type<>'" + cmbEntity.SelectedValue + "' and ISDELETED='0' and FLAG<>'3'" +
                                        " group by AL_ID,EMPLOYEE_CODE " +
                                        ") Sel " +
                                        "group by Sel.EMPLOYEE_CODE" +
                                        ")Wel " +
                                        "where Wel.LevelCount + "+ (lstSAL.Items.Count) + ">4 ";


                String _strDelete = "";
                
                  DataTable _dtResult = getDataTable(_strSelect, _sqlConnection);
                 
                    if (_dtResult.Rows.Count > 0)
                    {                 
                        this.messageDiv.InnerHtml = "Can not Add records.Because " + _dtResult.Rows.Count.ToString() + " employee(s) will exceed no of maximum level attched to them.";
                        return false;
                    }
                    else
                    {                     
                        return true;
                    }               
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void CmdOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && validation() )
            {
                String _empSelectQuery = "", _insertQuery = "", _deleteQuery="";
                if (cmbEntity.SelectedValue == "EMP")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EPD_EMPID as ENTITY_ID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID in " +
                                      "(" + _strList.Trim() + ") ";
                }
                else if (cmbEntity.SelectedValue == "CAT")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_CATEGORY_ID as ENTITY_ID  " +
                                      "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                      "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                      "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "DEP")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DESIGNATION_ID as ENTITY_ID  " +
                                      "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                      "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                      "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "DES")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DESIGNATION_ID as ENTITY_ID  " +
                                     "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                     "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                     "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "DIV")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DIVISION_ID as ENTITY_ID  " +
                                       "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                       "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                       "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "GRD")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GRADE_ID as ENTITY_ID  " +
                                      "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                      "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                      "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "GRP")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GROUP_ID as ENTITY_ID  " +
                                      "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                      "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                      "and EP.EPD_EMPID=EO.EOD_EMPID";
                }
                else if (cmbEntity.SelectedValue == "LOC")
                {
                    _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_LOCATION_ID as ENTITY_ID  " +
                                      "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                      "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                      "and EP.EPD_EMPID=EO.EOD_EMPID";
                }



                DataTable _dt = new DataTable();
                _dt = getDataTable(_empSelectQuery, _sqlConnection);
                if (_dt.Rows.Count > 0)
                {
                    SqlTransaction _trans;
                    Boolean _result = true, _transResult = true ,_deleteResult=true;
                    _trans = _sqlConnection.BeginTransaction();
                            
                    for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                    {
                            _deleteQuery = "";
                            _deleteQuery = "Delete from EAL_CONFIG where ENTITY_TYPE='" + cmbEntity.SelectedValue + "' and ENTITY_ID='"
                                + _dt.Rows[j]["ENTITY_ID"].ToString() + "' and EMPLOYEE_CODE='" + _dt.Rows[j]["EPD_EMPID"].ToString()
                                + "' and CARD_CODE= '" + _dt.Rows[j]["EPD_CARD_ID"].ToString() + "'";
                            _deleteResult = RunExecuteNonQueryWithTransaction(_deleteQuery, _sqlConnection, _trans);

                     }
                    for (int i = 0; i <= lstSAL.Items.Count - 1; i++)
                    {                       
                        for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                        {
                            _insertQuery = ""; 
                            _insertQuery = "insert into EAL_CONFIG(ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE,AL_ID,FLAG,ISDELETED,DELETEDDATE)" +
                                           "values('" + cmbEntity.SelectedValue + "','" + _dt.Rows[j]["ENTITY_ID"].ToString() + "','" + _dt.Rows[j]["EPD_EMPID"].ToString() + "','" + _dt.Rows[j]["EPD_CARD_ID"].ToString() + "','" + lstSAL.Items[i].Value.ToString() + "','2','false',null)";
                            _result = RunExecuteNonQueryWithTransaction(_insertQuery, _sqlConnection, _trans);
                            if (_result == false)
                            {
                                _transResult = false;
                                break;
                            }
                        }
                        if (_transResult == false)
                            break;
                    }
                    if (_transResult == true)
                    {
                        _trans.Commit();
                        this.messageDiv.InnerText = "Records Saved Successfully";
                        String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='UAC'";
                        Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);                      

                        string someScript = "";
                        someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                    }
                }
                else
                {
                    this.messageDiv.InnerText = "This Entity Specification doest not belong to any employee.";

                }

            }

            string someScript2 = "";
            someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",5000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

        }
        public int checkALCount()
        {

            ArrayList _AL = new ArrayList();
            _AL = (ArrayList)Session["LEVEL"];

            int _diff=0;
            for (int i=0; i <= lstSAL.Items.Count - 1; i++)
            {
                if (!_AL.Contains(lstSAL.Items[i].Value.ToString()))
                {                  
                    _diff = _diff + 1;
                }               
            }
            return _diff;
        }
        public String getALString()
        {
            try
            {
                String _str = "";

                ArrayList _AL = new ArrayList();
                _AL = (ArrayList)Session["LEVEL"];

                //for (int i = 0; i <= lstAEntity.Items.Count - 1; i++)
                //{
                //    _strList = _strList + "'" + lstSEntity.Items[i].Value + "',";
                //}
                //_strList = _strList.Substring(0, (_strList.Length - 1));


                for (int i = 0; i <= _AL.Count - 1; i++)
                {
                    _str = _str + "'" + _AL[i].ToString() + "',";
                }
                _str = _str.Substring(0, (_str.Length - 1));
                return _str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public Boolean validation()
        {
            try
            {
                //if (lstSAL.Items.Count == 0)
                //{ this.messageDiv.InnerHtml = "Please select Entity specification."; return false; }
                //else if (lstSAL.Items.Count > 4)
                //{ this.messageDiv.InnerHtml = "Maximum 4 access level can be selected."; return false; }
                //else 
                if (checkEntityAccessPermission(getALString()) == false)
                { return false; }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("UAP_ID");
            Session.Remove("LEVEL");
            Response.Redirect("~/UserAccessPermissionBrowse.aspx");
        }

        protected void CVAL_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lstSAL.Items.Count == 0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CVAL2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lstSAL.Items.Count > 4)
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