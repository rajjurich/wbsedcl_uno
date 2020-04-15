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
    public partial class Current_Emp_Strength1 : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string strDate = "";
        DataSet globalds;  
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
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
            catch
            {
            }


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
            ListBox1.DataValueField = "EOD_EMPID";
            ListBox1.DataTextField = "EmployeeName";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();
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
                SqlDataAdapter da;
                DataTable  dtData;
                SqlConnection conn = new SqlConnection(m_connectons);
                ReportViewer1.LocalReport.ReportPath = "RDLC\\CurrentEmpStrength.rdlc";
           
                viewer.Visible = true;
                ReportViewer1.Visible = true;
               
                string locAsUnit = LocationHdn.Value;
                string DivAsEntity = DivisionHdn.Value;
                string EmpCode = EmployeeHdn.Value;
                string companyAsCenter = ComapnyHdn.Value;
                string DeptAsGroup = DepartmentHdn.Value;
                string GradeAsDiv = GradeHdn.Value;
                string GroupAsSection = GroupHdn.Value;
          
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "SAC_Current_Emp_Strength_Report";
                cmd.Parameters.AddWithValue("@EMP_ID", EmpCode);
                cmd.Parameters.AddWithValue("@Center", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@Unit", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@ENTITY", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@GROUP", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@Division", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@Section", GroupHdn.Value);
                cmd.Parameters.AddWithValue("@TODATE", txtToDate.Text);
                cmd.Parameters.AddWithValue("@FROM_DATE", txtCalendarFrom.Text);
                cmd.Parameters.AddWithValue("@EmpType", ddlPersonnelType.SelectedValue);
                dtData = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtData);
                String dataSetName = "Emp_Strength";           
                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs" ;
                string Header = "Current Employees & Non-Employees Strength Report";
                if (ddlPersonnelType.SelectedValue == "E")
                {
                    Header = "Current Employees Strength Report";
                }
                else
                {
                    Header = "Current Non-Employees Strength Report";
                }
                lblHeader.Text = Header;
                ReportParameter para = new ReportParameter("myParameter1", strReportHeader);
                ReportParameter rpHeader = new ReportParameter("Header", Header);
                DataTable dtReport = clsCommonHandler.GetReportHeader();
                ReportParameter header = new ReportParameter("heading", dtReport.Rows[0]["value"].ToString());
                ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rpHeader,para,address,header });
              

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dtData;
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                HeadPnl.Visible = false;               
                HeadPanel.Visible = false;
            }

            catch(Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private DataSet ExecuteQuery(SqlCommand cmd, string dataSetName, string tableName)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandType = CommandType.Text;
                DataSet dataSet = new DataSet(dataSetName);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
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
            Response.Redirect("Current_Emp_Strength.aspx");
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
            
        }

    }
}