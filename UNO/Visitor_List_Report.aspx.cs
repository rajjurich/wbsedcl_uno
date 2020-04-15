
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
    public partial class Visitor_List_Report : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        DataSet globalds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                string strMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                txtFromDate.Text = "01" + "/" + strMonth + "/" + DateTime.Now.Year;
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MM/yyyy");
                rdbEmployee.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "' );");

                //added by vaibhav

                rdbVisitorCompny.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('VISCOM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "' );");
                //

                rdbComapny.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "' ,'" + rdbVisitorCompny.ClientID + "' );");

                rdbLocation.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "' );");
                rdbDivision.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "' ,'" + rdbVisitorCompny.ClientID + "' );");
                rdbDepartment.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "' ,'" + rdbVisitorCompny.ClientID + "' );");
                rdbShift.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "' ,'" + rdbVisitorCompny.ClientID + "' );");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "' );");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return RptVisitorSpecificDetails('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "' );");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + lstEmployee.ClientID + "','" + lstEmployeeDummy.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + lstCompany.ClientID + "','" + lstCompanyDummy.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + lstLocation.ClientID + "','" + lstLocationDummy.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + lstDivision.ClientID + "','" + lstDivisionDummy.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + lstDepartment.ClientID + "','" + lstDepartmentDummy.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + lstShift.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");

                btnOK.Attributes.Add("onclick", "javascript:return VRptOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "');");

                btnCancel.Attributes.Add("onclick", "javascript:return VRptCloseClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbComapny.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + btnOK.ClientID + "','" + btnCancel.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + VisitorCompanyHdn.ClientID + "','" + lstVisitorCoampany.ClientID + "','" + txtSerchVisitorCompany.ClientID + "','" + rdbVisitorCompny.ClientID + "');");
              
                rdbVisitorEmployee.Attributes.Add("onchange", "javascript:return SwitchVisitorEmployee('" + lstEmployee.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + rdbVisitorEmployee.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtFromDate.ClientID + "','" + txtToDate.ClientID + "' );");
              
                if (Session["uid"].ToString().Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    fillVisitorCompany();
                    fillEmplist();
                }
                else
                {
                    fillVisitorCompany();
                    fillEntities();
                }
            }
            else
            {
                fillEmplist();
                fillVisitorCompany();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            lstEmployee.DataValueField = "EOD_EMPID";
            lstEmployee.DataTextField = "EmployeeName";
            lstEmployee.DataSource = thisDataSet.Tables[0];
            lstEmployee.DataBind();

            lstEmployeeDummy.DataValueField = "EOD_EMPID";
            lstEmployeeDummy.DataTextField = "EmployeeName";
            lstEmployeeDummy.DataSource = thisDataSet.Tables[0];
            lstEmployeeDummy.DataBind();
            return thisDataSet;
        }
        public void fillEmplist()
        {
            string strsql;

            strsql = "SELECT EPD_EMPID,replace(convert(char(40),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as EPD_FIRST_NAME " +
                                       "  FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                                       "  on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' and eod_type = 'E' ";


            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strsql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            lstEmployee.DataValueField = "EPD_EMPID";
            lstEmployee.DataTextField = "EPD_FIRST_NAME";
            lstEmployee.DataSource = thisDataSet.Tables[0];
            lstEmployee.DataBind();

            lstEmployeeDummy.DataValueField = "EPD_EMPID";
            lstEmployeeDummy.DataTextField = "EPD_FIRST_NAME";
            lstEmployeeDummy.DataSource = thisDataSet.Tables[0];
            lstEmployeeDummy.DataBind();

        }

        public void fillVisitorCompany()
        {
            string strsql;

            strsql = "SELECT DISTINCT VisitorCompany FROM Visitor_Master";


            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strsql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            lstVisitorCoampany.DataValueField = "VisitorCompany";
            lstVisitorCoampany.DataTextField = "VisitorCompany";
            lstVisitorCoampany.DataSource = thisDataSet.Tables[0];
            lstVisitorCoampany.DataBind();

            lstVisitorCoampanyDummy.DataValueField = "VisitorCompany";
            lstVisitorCoampanyDummy.DataTextField = "VisitorCompany";
            lstVisitorCoampanyDummy.DataSource = thisDataSet.Tables[0];
            lstVisitorCoampanyDummy.DataBind();

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
                SqlConnection conn = new SqlConnection(m_connectons);
                SqlDataAdapter da;
                DataTable dtCompany, dtData;
                string comp = string.Empty, compAdd = string.Empty;
                ReportViewer1.LocalReport.ReportPath = "RDLC\\VisitorsList.rdlc";
                ReportViewer1.Visible = true;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                string ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "USP_rpt_GetVisitorsDetails";
                cmd.Parameters.AddWithValue("@selEmployee", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@selCompany", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@selLocation", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@selDivision", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@selDepartment", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@selGrade", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@selGroup", GroupHdn.Value);
          
                cmd.Parameters.AddWithValue("@RptType", "Area");
                cmd.Parameters.AddWithValue("@dtToDate", ToDate);
                cmd.Parameters.AddWithValue("@dtFromDate", FromDate);
                cmd.Parameters.AddWithValue("@strEmpVisitor", rdbVisitorEmployee.SelectedValue=="0"?"VIS":"EMP");

                cmd.Parameters.AddWithValue("@selVisitorCompany", VisitorCompanyHdn.Value);
                cmd.Parameters.AddWithValue("@user_id", Session["uid"].ToString());

          
                dtData = new DataTable();

                da = new SqlDataAdapter(cmd);
                da.Fill(dtData);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "dsAreaWiseData";

                da = new SqlDataAdapter("select * from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", m_connectons);
                dtCompany = new DataTable();
                da.Fill(dtCompany);

                if (dtCompany != null && dtCompany.Rows.Count > 0)
                {
                    comp = dtCompany.Rows[0]["value"].ToString();
                    compAdd = dtCompany.Rows[1]["value"].ToString();
                }

                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs from " + txtFromDate.Text + " to " + txtToDate.Text;
                ReportParameter para = new ReportParameter("ReportHeader", strReportHeader);
                ReportParameter header = new ReportParameter("Company", comp);
                ReportParameter address = new ReportParameter("address", compAdd);
                ReportParameter prIsVisitor = new ReportParameter("prIsVisitor", rdbVisitorEmployee.SelectedValue=="0"?"1":"0");
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, header, address, prIsVisitor });

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dtData;
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                HeadPanel.Visible = false;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, new FileInfo(this.Request.Url.LocalPath).Name);
            }
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
            //if (EmployeeHdn.Value == "")
            //    if (rdbEmployee.SelectedIndex == 0)
            //        //EmployeeHdn = a.empAllSelect(lstEmployee);
            //        EmployeeHdn.Value = "";

            if (EmployeeHdn.Value == "")
                EmployeeHdn = a.empAllSelect(lstEmployee);
            

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}