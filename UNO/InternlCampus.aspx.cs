using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

using Microsoft.Reporting.WebForms;


namespace UNO
{
    public partial class InternlCampus : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "";
     
        String m_messageString = "";
        String m_role = "";
        DataSet globalds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                GetDate();
                RadioButtonList1.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList2.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList3.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList4.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList5.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList6.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + ListBox1.ClientID + "','" + ListBox7.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + ListBox2.ClientID + "','" + ListBox8.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + ListBox3.ClientID + "','" + ListBox9.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + ListBox4.ClientID + "','" + ListBox10.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + ListBox5.ClientID + "','" + ListBox11.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + ListBox6.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");
                txtReader.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtReader.ClientID + "','" + lstReader.ClientID + "','" + lstReaderDummy.ClientID + "' );");
                // txtZone.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + lstZoneDummy.ClientID + "' );");

                Button1.Attributes.Add("onclick", "javascript:return ReaderOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + ReaderHdn.ClientID + "','" + txtReader.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return ReaderCancelClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + RadioButtonList7.ClientID + "','" + ReaderHdn.ClientID + "');");

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                 
                    fillEmplist();                   
                    GetDate();
                }
                else
                {
                    fillEntities();
                }

              
                fillReader();

            }
            else
            {
                fillEmplist();
                strDate = txtCalendarFrom.Text;
            }
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            if (userAgent.Contains("MSIE 7.0"))
                DateHdn.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void GetDate()
        {
            DateTime today = DateTime.Now; //current date

            string firstDay = today.AddDays(-(today.Day - 1)).ToString("dd/MM/yyyy"); //first day

            today = today.AddMonths(1);
            DateTime lastDay = today.AddDays(-(today.Day)); //last day

            txtCalendarFrom.Text = firstDay;
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public DataSet fillReaderAndZone()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string levelId = Session["levelId"].ToString();
            SqlCommand cmd = new SqlCommand("fillAccess_Settings2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@levelId", levelId);
            DataSet vai = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(vai);

            return vai;
        }
        private DataSet fillEntities()
        {           

            string mgrId = Session["uid"].ToString();

            string levelId = Session["levelId"].ToString();
            SqlConnection con = new SqlConnection(m_connectons);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand cmd = new SqlCommand("USP_GET_EMPDETAILS_ROLEWISE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeCode", mgrId);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


            //Employee
            ListBox1.DataValueField = "EOD_EMPID";
            ListBox1.DataTextField = "EmployeeName";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();

            ListBox7.DataValueField = "EOD_EMPID";
            ListBox7.DataTextField = "EmployeeName";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();

            return thisDataSet;


         
        }
        public void fillEmplist()
        {
            if (ddlPersonnelType.SelectedIndex == 0)
            {

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEntity(ddlPersonnelType.SelectedValue);
                }
                else
                {
                    fillEntities();
                }
            }
            else
            {
                fillEntity(ddlPersonnelType.SelectedValue);
            }

        }      
        private void Fillshiftdrp()
        {
            string strSql = "select SHIFT_ID,replace(convert(char(40),ltrim(SHIFT_ID))+SHIFT_DESCRIPTION,' ',' ' ) as  SHIFTNAME from ta_shift  where SHIFT_ISDELETED='0'";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ListBox6.DataValueField = "SHIFT_ID";
            ListBox6.DataTextField = "SHIFTNAME";
            ListBox6.DataSource = thisDataSet.Tables[0];
            ListBox6.DataBind();

            ListBox12.DataValueField = "SHIFT_ID";
            ListBox12.DataTextField = "SHIFTNAME";
            ListBox12.DataSource = thisDataSet.Tables[0];
            ListBox12.DataBind();



        }
        private void fillReader()
        {
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            {

                DataSet thisDataSet = clsCommonHandler.GetReaderZoneDetails("RDR");
                lstReader.DataValueField = "READER_ID";
                lstReader.DataTextField = "ReaderNAME";
                lstReader.DataSource = thisDataSet.Tables[0];
                lstReader.DataBind();

                lstReaderDummy.DataValueField = "READER_ID";
                lstReaderDummy.DataTextField = "ReaderNAME";
                lstReaderDummy.DataSource = thisDataSet.Tables[0];
                lstReaderDummy.DataBind();

            }
            else
            {
                DataSet ds = fillReaderAndZone();

                if (ds.Tables.Count > 0)
                {
                    lstReader.DataValueField = "ID";
                    lstReader.DataTextField = "DESCRIPTION";
                    lstReader.DataSource = ds.Tables[0];
                    lstReader.DataBind();

                    lstReaderDummy.DataValueField = "ID";
                    lstReaderDummy.DataTextField = "DESCRIPTION";
                    lstReaderDummy.DataSource = ds.Tables[0];
                    lstReaderDummy.DataBind();
                }

            }
        }
        void initializeControls()
        {
            fillEmplist();
        }
        public void fillHiddenVal()
        {
            allSelectRoleWise a = new allSelectRoleWise();

            if (ComapnyHdn.Value == "")
                ComapnyHdn = a.allSelect(globalds, "COM");
            if (DivisionHdn.Value == "")
                DivisionHdn = a.allSelect(globalds, "DIV");
            if (DepartmentHdn.Value == "")
                DepartmentHdn = a.allSelect(globalds, "DEPT");
            if (LocationHdn.Value == "")
                LocationHdn = a.allSelect(globalds, "LOC");
            if (GradeHdn.Value == "")
                GradeHdn = a.allSelect(globalds, "GRD");
            if (GroupHdn.Value == "")
                GroupHdn = a.allSelect(globalds, "GRP");
            if (EmployeeHdn.Value == "")
                EmployeeHdn = a.empAllSelect(ListBox1);
            if (ReaderHdn.Value == "")
                ReaderHdn = a.empAllSelect(lstReader);

        }
        private void fillEntity(string Type)
        {
            DataSet thisDataSet = clsCommonHandler.GetEmployeesDetails("NEMP", Type);

            ListBox1.DataValueField = "EPD_EMPID";
            ListBox1.DataTextField = "EPD_FIRST_NAME";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();

            ListBox7.DataValueField = "EPD_EMPID";
            ListBox7.DataTextField = "EPD_FIRST_NAME";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();
        }
        protected void View_Click(object sender, EventArgs e)
        {
            if (Session["uid"].ToString().ToLower() != "admin")
            {
                globalds = fillEntities();
                fillHiddenVal();
            }
            btnClose.Visible = true;
            viewer.Visible = true;
            ReportViewer1.Visible = true;
            ShowReport();
        }
        private void ShowReport()
        {

            ReportViewer1.LocalReport.ReportPath = "RDLC\\IntnlCmpsReport.rdlc";

            String commandText = "";

            commandText = commandText + " select e.EPD_EMPID,e.EPD_FIRST_NAME+' '+ e.EPD_LAST_NAME as Name,convert(varchar,ev.Event_Datetime,103) as date,convert(varchar,ev.Event_Datetime,108) as time ,r.READER_DESCRIPTION,  case when r.reader_mode='I' then 'In' else 'Out' end as Mode,r.READER_ID,ev.Event_Controller_ID,c.CTLR_DESCRIPTION ,";
            commandText = commandText + " ev.Event_Datetime,ENT_COMPANY.COMPANY_NAME ,CAT.OCE_DESCRIPTION AS Category  ,LOC.OCE_DESCRIPTION AS Location ";
            commandText = commandText + " ,DIV.OCE_DESCRIPTION AS Division  ,DEP.OCE_DESCRIPTION AS Department , ";
            commandText = commandText + " DESG.OCE_DESCRIPTION AS Designation  ,GRP.OCE_DESCRIPTION AS Groups ,GRD.OCE_DESCRIPTION AS Grade ";
            commandText = commandText + " from ENT_EMPLOYEE_PERSONAL_DTLS e  with(nolock)";
            commandText = commandText + " inner join acs_events ev with(nolock) on ('00'+ SUBSTRING(e.EPD_EMPID,1,2) + '0' +SUBSTRING(e.EPD_EMPID,3,5)) = ev.Event_Employee_Code ";
            commandText = commandText + " left join ACS_READER r  with(nolock) on ev.Event_Reader_ID=r.READER_ID ";
            commandText = commandText + " and  ev.Event_Controller_ID =r.CTLR_ID	";
            commandText = commandText + " left join acs_controller c  with(nolock) on c.CTLR_ID=ev.Event_Controller_ID ";
            commandText = commandText + " INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS eo ON eo.EOD_EMPID =e.EPD_EMPID ";
            commandText = commandText + " INNER JOIN ENT_COMPANY ON eo.EOD_COMPANY_ID=ENT_COMPANY.COMPANY_ID  ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS CAT ON eo.EOD_CATEGORY_ID=CAT.OCE_ID AND CAT.CEM_ENTITY_ID='CAT'  ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS LOC ON eo.EOD_LOCATION_ID=LOC.OCE_ID AND LOC.CEM_ENTITY_ID='LOC'  ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DIV ON  eo.EOD_DIVISION_ID=DIV.OCE_ID AND DIV.CEM_ENTITY_ID='DIV' ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DEP ON eo.EOD_DEPARTMENT_ID=DEP.OCE_ID AND DEP.CEM_ENTITY_ID='DEP' ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS DESG ON eo.EOD_DESIGNATION_ID=DESG.OCE_ID AND DESG.CEM_ENTITY_ID='DES' ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRP ON eo.EOD_GROUP_ID=GRP.OCE_ID AND GRP.CEM_ENTITY_ID='GRP'  ";
            commandText = commandText + " INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRD ON eo.EOD_GRADE_ID=GRD.OCE_ID AND GRD.CEM_ENTITY_ID='GRD'  ";

            string strCondition = " coalesCE( c.CLTR_FOR_TA,'') <> 'TA' and isnull(eo.EOD_ACTIVE,0)=1 and isnull(eo.EOD_ISDELETED,0)=0 and e.EPD_ISDELETED=0 and EOD_TYPE='" + ddlPersonnelType.SelectedValue.ToString() + "'", strSort = "";
            if (Session["uid"].ToString().ToLower() != "admin")
            {
                strCondition += this.EmployeeHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " e.EPD_EMPID in " + this.EmployeeHdn.Value);

                strCondition += this.ComapnyHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " COMPANY_ID in " + this.ComapnyHdn.Value);

                strCondition += this.LocationHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " LOC.OCE_ID in " + this.LocationHdn.Value);

                strCondition += this.DivisionHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " DIV.OCE_ID in " + this.DivisionHdn.Value);

                strCondition += this.DepartmentHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " DEP.OCE_ID in " + this.DepartmentHdn.Value);

                strCondition += this.GradeHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " And " : "") + "GRD.OCE_ID in " + this.GradeHdn.Value);

                strCondition += this.GroupHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " And " : "") + "GRP.OCE_ID in " + this.GroupHdn.Value);

                strCondition += ReaderHdn.Value.Trim().Length == 0 ? "" : ((strCondition.Length > 0 ? " AND " : "") + " (SELECT Convert(varchar(30), r.READER_ID)+'-'+convert(varchar(30),r.CTLR_ID)) IN " + ReaderHdn.Value);
            }
            else
            {
                if (EmployeeHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " e.EPD_EMPID in " + this.EmployeeHdn.Value;
                }

                if (ComapnyHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " COMPANY_ID in " + this.ComapnyHdn.Value;
                }

                if (LocationHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " LOC.OCE_ID in " + this.LocationHdn.Value;
                }

                if (DivisionHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " DIV.OCE_ID in " + this.DivisionHdn.Value;
                }

                if (DepartmentHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " DEP.OCE_ID in " + this.DepartmentHdn.Value;
                }

                if (GradeHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " And " : "") + "GRD.OCE_ID in " + this.GradeHdn.Value;
                }

                if (GroupHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " And " : "") + "GRP.OCE_ID in " + this.GroupHdn.Value;
                }

                if (ReaderHdn.Value.Trim() != "")
                {
                    strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " (SELECT Convert(varchar(30), r.READER_ID)+'-'+convert(varchar(30),r.CTLR_ID)) IN " + ReaderHdn.Value;
                }
            }



            if (txtCalendarFrom.Text != "")
            {
                strCondition = strCondition + (strCondition.Length > 0 ? " AND " : "") + " CONVERT(datetime,Event_Datetime,103) between convert(datetime,'" + txtCalendarFrom.Text + "',103) and convert(datetime,'" + txtToDate.Text + "',103)+1 ";
            }

            if (strCondition.Length > 0)
            {
                strCondition = " WHERE " + strCondition + "And ev.Event_Controller_ID in (select ctlr_id from acs_controller)  order by ev.Event_Employee_Code,Event_Datetime";
            }

            commandText = commandText + strCondition;

            String dataSetName = "DataSet1";
            String tableName = "DataTable1";//USER DEFINED NAME

            DataSet thisDataSet = ExecuteQuery(commandText, dataSetName, tableName);


            string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
            if (txtCalendarFrom.Text == txtToDate.Text)
            {
                strReportHeader = strReportHeader + strDate;
            }
            else
            {
                strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
            }
            // ReportParameter para3 = new ReportParameter("count", thisDataSet.Tables[0].Rows.Count.ToString());

            ReportParameter para = new ReportParameter("myparameter1", strReportHeader);

            ReportParameter para5 = new ReportParameter("userName", Session["uid"].ToString() + " " + Session["loginName"].ToString());
            DataTable dtReport = clsCommonHandler.GetReportHeader();
            ReportParameter header = new ReportParameter("header", dtReport.Rows[0]["value"].ToString());
            ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
            ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para5,address,header });

            ReportDataSource datasource = new ReportDataSource();
            datasource.Name = dataSetName;
            datasource.Value = thisDataSet.Tables[tableName];
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();

            HeadPnl.Visible = false;
            //Panel1.Visible = false;
            HeadPanel.Visible = false;

        }
        private DataSet ExecuteQuery(string strQuery, string dataSetName, string tableName)
        {

            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = m_connectons;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = strQuery;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dataSet, tableName);
                return dataSet;
            }

            catch (Exception ex)
            {
                return null;
            }

        }
        protected void Close_Click(object sender, EventArgs e)
        {
            Response.Redirect("InternlCampus.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("TADashboard.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

            RadioButtonList1.SelectedIndex = 0;
            RadioButtonList2.SelectedIndex = 0;
            RadioButtonList3.SelectedIndex = 0;
            RadioButtonList4.SelectedIndex = 0;
            RadioButtonList5.SelectedIndex = 0;
            RadioButtonList6.SelectedIndex = 0;
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            GradeHdn.Value = "";
            GroupHdn.Value = "";
            GetDate();
        }


    }
}