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
    public partial class LocateCardHolder : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.messageDiv.InnerHtml = "No Record Found.";
            this.ImgPhoto.ImageUrl = "~//EmpImage//default.png";
            if (Page.IsPostBack == false)
            {
                this.messageDiv.InnerHtml = "No Record Found.";
                intializeControl();
            }
        }

        public void intializeControl()
        {
            
            cmbCriteria.Items.Clear();
            cmbCriteria.Items.Add(new ListItem("Select Criteria", "-1"));
            cmbCriteria.Items.Add(new ListItem("Employee Code", "Ecode"));
            cmbCriteria.Items.Add(new ListItem("Card Code", "Ccode"));
            cmbCriteria.SelectedValue = null;
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

        protected void cmbCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
            lblEmployeeCode.Text = "";
            lblCardNo.Text = "";
            lblName.Text = "";
            ImgPhoto.ImageUrl = "";
            lblMobileNo.Text = "";
            lblMaritalStatus.Text = "";
            lblLastReaderswiped.Text = "";
        }

        protected void CmdSearch_Click(object sender, EventArgs e)
        {
            if (cmbCriteria.SelectedValue.ToString().Trim() != "-1" && txtCode.Text.Trim() !="")
            {
                string _whereClause="";
                string _strSelect="";
                DataTable _dtRecords=new DataTable();
                if (cmbCriteria.SelectedValue.ToString().Trim() == "Ecode")
                {   _whereClause = "Event_Employee_Code='" + txtCode.Text.Trim() + "'";  }
                else if (cmbCriteria.SelectedValue.ToString().Trim() == "Ccode")
                {   _whereClause = "Event_Card_Code='" + txtCode.Text.Trim() + "'";      }


               _strSelect="select Event_Employee_Code,Event_Card_Code,EPD_FIRST_NAME+' '+ cast(EPD_MIDDLE_NAME as varchar)+' '+EPD_LAST_NAME as Empname,EAD_PHONE_ONE,MAR.MARITALSTATUS," +//,EPD_PHOTOURL, 
                                 "replace(convert(char(28),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as ReaderNCtrl "+
                                 "from ACS_Events ,ENT_EMPLOYEE_PERSONAL_DTLS,ENT_EMPLOYEE_ADDRESS,ACS_READER AR,ACS_CONTROLLER AC, "+
                                 "(SELECT CODE,[VALUE] AS MARITALSTATUS FROM ENT_PARAMS WHERE IDENTIFIER = 'MARITALSTATUS') as MAR "+
                                 "where Event_ID =( "+
                                 "select Event_ID from ACS_Events "+
                                 "where Event_Datetime= "+
                                 "(select MAX(Event_Datetime) from ACS_Events where "+_whereClause +" and Event_Type='01') and "+_whereClause +" and Event_Type='01') " +
                                 "and Event_Employee_Code=EPD_EMPID and Event_Employee_Code=EAD_EMPID  and  EAD_ADDRESS_TYPE='P' and "+
                                 "EPD_MARITAL_STATUS=MAR.CODE and AR.CTLR_ID=AC.CTLR_ID "+
                                 "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar))=cast(Event_Controller_ID as varchar) +'-'+cast(Event_Reader_ID as varchar) ";

                _dtRecords = getDataTable(_strSelect, _sqlConnection);

                if (_dtRecords.Rows.Count != 0)
                {
                    lblEmployeeCode.Text = _dtRecords.Rows[0]["Event_Employee_Code"].ToString();
                    lblCardNo.Text = _dtRecords.Rows[0]["Event_Card_Code"].ToString();
                    lblName.Text = _dtRecords.Rows[0]["Empname"].ToString();
                    //this.ImgPhoto.ImageUrl = "~//EmpPhoto//" + _dtRecords.Rows[0]["EPD_PHOTOURL"].ToString();
                    if (_dtRecords.Rows[0]["EPD_PHOTOURL"].ToString() != "")
                    {
                        this.ImgPhoto.ImageUrl = "~//EmpImage//" + _dtRecords.Rows[0]["EPD_PHOTOURL"].ToString();
                    }
                    else
                    {
                        this.ImgPhoto.ImageUrl = "~//EmpImage//default.png";
                    }
                    //this.ImgPhoto.ImageUrl = "~//EmpImage//cmslogo.gif";
                    lblMobileNo.Text = _dtRecords.Rows[0]["EAD_PHONE_ONE"].ToString();
                    lblMaritalStatus.Text = _dtRecords.Rows[0]["MARITALSTATUS"].ToString();
                    lblLastReaderswiped.Text = _dtRecords.Rows[0]["ReaderNCtrl"].ToString();
                }
                else
                {
                    this.messageDiv.InnerHtml = "No Record Found.";
                    string someScript2 = "<script language='javascript'>setTimeout(\"clearFunctionMessageDiv()\",2000);</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                }
            }
        }
    }
}