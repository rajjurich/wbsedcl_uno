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
    public partial class ChronicLateEarly : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "";

        String m_messageString = "";
        String m_role = "";
        DataSet globalds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                RadioButtonList1.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList2.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList3.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList4.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList5.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList6.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + ListBox1.ClientID + "','" + ListBox7.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + ListBox2.ClientID + "','" + ListBox8.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + ListBox3.ClientID + "','" + ListBox9.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + ListBox4.ClientID + "','" + ListBox10.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + ListBox5.ClientID + "','" + ListBox11.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + ListBox6.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");

                Button1.Attributes.Add("onclick", "javascript:return RptOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return RptCloseClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtCalendarFrom.ClientID + "','" + txtToDate.ClientID + "' );");
                GetDate();
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEmplist();
                }
                else
                {
                    fillEntities();
                }

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

                    fillEntity(ddlPersonnelType.SelectedValue);
                else
                    fillEntities();

            }
            else
                fillEntity(ddlPersonnelType.SelectedValue);


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
            try
            {
                ReportViewer1.LocalReport.ReportPath = "RDLC\\EarlyOrLate.rdlc";
                viewer.Visible = true;
                ReportViewer1.Visible = true;


                String dataSetName = "DataSet1";
                SqlConnection conn = new SqlConnection(m_connectons);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "usp_rpt_getChronicLateEarlyDetails";
                cmd.Parameters.AddWithValue("@strEmployeeHdn", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@strGoupHdn", GroupHdn.Value);
                cmd.Parameters.AddWithValue("@strFromDate", txtCalendarFrom.Text);
                cmd.Parameters.AddWithValue("@strToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@strCheck", ddlReportType.SelectedValue == "1" ? "1" : ddlReportType.SelectedValue == "2" ? "2" : ddlReportType.SelectedValue == "0" ? "3" : "0");
                cmd.Parameters.AddWithValue("@Count", txtcount.Text.Trim() == "" ? 0 : Convert.ToInt16(txtcount.Text));
                cmd.Parameters.AddWithValue("@strPersonnelType", ddlPersonnelType.SelectedValue);

                DataTable dtData = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtData);

                string param2, strLateEarly = "", strInOut = "", strAll = string.Empty;
                ReportParameter para3;
                if (dtData.Rows.Count > 0)
                {
                    para3 = new ReportParameter("count", dtData.Rows.Count.ToString());

                    ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para3 });
                }
                else
                {
                    para3 = new ReportParameter("count", "0");

                    ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para3 });
                }


                if (ddlReportType.SelectedValue == "1")
                    param2 = "Chronic Late Report";
                else if (ddlReportType.SelectedValue == "2")
                    param2 = " Chronic Early Going Report";
                else
                    param2 = " Chronic Late/Early Going Report";

                if (ddlReportType.SelectedValue == "1")
                {
                    strLateEarly = "Late By";
                    strInOut = "In Time";
                    strAll = "P";
                }
                else if (ddlReportType.SelectedValue == "2")
                {
                    strLateEarly = "Early Going";
                    strInOut = "Out Time";
                    strAll = "P";
                }
                else
                {
                    strLateEarly = "and";
                    strInOut = "and";
                    strAll = "A";
                }

                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
                if (txtCalendarFrom.Text == txtToDate.Text)
                {
                    strReportHeader = strReportHeader + strDate;
                }
                else
                {
                    strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
                }
                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                ReportParameter para2 = new ReportParameter("myparameter2", param2);
                DataTable dtReport = clsCommonHandler.GetReportHeader();
                ReportParameter header = new ReportParameter("header", dtReport.Rows[0]["value"].ToString());
                ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
                ReportParameter parLateEarly = new ReportParameter("parLateEarly", strLateEarly);
                ReportParameter parInOut = new ReportParameter("parInOut", strInOut);
                ReportParameter parAll = new ReportParameter("parAll", strAll);
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para2, header, address, parAll, parInOut, parLateEarly });
                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dtData;
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();

                HeadPnl.Visible = false;
                //Panel1.Visible = false;
                HeadPanel.Visible = false;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private DataSet ExecuteQuery(string strQuery, string dataSetName, string tableName)
        {

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();

                connection.ConnectionString = m_connectons;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = strQuery;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dataSet, tableName);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return dataSet;

            }

            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                return null;
            }

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            RadioButtonList1.SelectedIndex = 0;
            RadioButtonList2.SelectedIndex = 0;
            RadioButtonList3.SelectedIndex = 0;
            RadioButtonList4.SelectedIndex = 0;
            RadioButtonList5.SelectedIndex = 0;
            RadioButtonList6.SelectedIndex = 0;
            GetDate();
        }
        protected void Close_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChronicLateEarly.aspx");
        }

    }
}