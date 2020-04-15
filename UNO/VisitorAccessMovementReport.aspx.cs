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
    public partial class VisitorAccessMovementReport : System.Web.UI.Page
    {
        public string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        DataSet globalds;
        DataTable dtData;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                string strMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                txtFromDate.Text = "01" + "/" + strMonth + "/" + DateTime.Now.Year;
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MM/yyyy");

                rdbEmployee.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbCompany.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbLocation.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbDivision.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbDepartment.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbShift.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rblReader.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('RDR','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");
                rblZone.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('ZNE','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstEmployeeDummy.ClientID + "','" + lstCompanyDummy.ClientID + "','" + lstLocationDummy.ClientID + "','" + lstDivisionDummy.ClientID + "','" + lstDepartmentDummy.ClientID + "','" + lstShiftDummy.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlLevel.ClientID + "','" + ddlStatus.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + lstEmployee.ClientID + "','" + lstEmployeeDummy.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + lstCompany.ClientID + "','" + lstCompanyDummy.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + lstLocation.ClientID + "','" + lstLocationDummy.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + lstDivision.ClientID + "','" + lstDivisionDummy.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + lstDepartment.ClientID + "','" + lstDepartmentDummy.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + lstShift.ClientID + "','" + lstShiftDummy.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");
                txtReader.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtReader.ClientID + "','" + lstReader.ClientID + "','" + lstReaderDummy.ClientID + "' );");
                txtZone.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + lstZoneDummy.ClientID + "' );");


                Button1.Attributes.Add("onclick", "javascript:return AccessLevelOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + ReaderHdn.ClientID + "','" + txtReader.ClientID + "','" + lstZone.ClientID + "','" + ZoneHdn.ClientID + "','" + txtZone.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return AccessLevelCancelClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + lstEmployee.ClientID + "','" + lstCompany.ClientID + "','" + lstLocation.ClientID + "','" + lstDivision.ClientID + "','" + lstDepartment.ClientID + "','" + lstShift.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + rdbEmployee.ClientID + "','" + rdbCompany.ClientID + "','" + rdbLocation.ClientID + "','" + rdbDivision.ClientID + "','" + rdbDepartment.ClientID + "','" + rdbShift.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtFromDate.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + rblReader.ClientID + "','" + ReaderHdn.ClientID + "','" + lstZone.ClientID + "','" + ZoneHdn.ClientID + "','" + txtZone.ClientID + "','" + rblZone.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtFromDate.ClientID + "','" + txtToDate.ClientID + "' );");

                if (Session["uid"].ToString().Equals("admin", StringComparison.CurrentCultureIgnoreCase))

                    fillEmplist();
                else
                    fillEntities();

                FillReader();
                FillZone();

            }

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
                DataTable dtCompany;
                string comp = string.Empty, compAdd = string.Empty;
                ReportViewer1.LocalReport.ReportPath = "RDLC\\VisitorAccessMovement.rdlc";
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
                    cmd.Parameters.AddWithValue("@selReader", ReaderHdn.Value);
                    cmd.Parameters.AddWithValue("@selZone", ZoneHdn.Value);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@strLevel", ddlLevel.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RptType", "Access");
                    cmd.Parameters.AddWithValue("@dtToDate", ToDate);
                    cmd.Parameters.AddWithValue("@dtFromDate", FromDate);
                    dtData = new DataTable();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(dtData);
                
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "dsAccessMvmt";

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

                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, header, address });
                
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


        public void fillEmplist()
        {
            string strsql;

            if (ddlPersonnelType.SelectedIndex == 0)
            {
                strsql = " SELECT '00'+ SUBSTRING(EOD_EMPID,1,2) + '0' +SUBSTRING(EOD_EMPID,3,5) EPD_EMPID ,replace(convert(char(40),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as EPD_FIRST_NAME  " +
                    " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  " +
                   " on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1'  ";
            }
            else
            {
                strsql = " SELECT '00'+ SUBSTRING(EOD_EMPID,1,2) + '0' +SUBSTRING(EOD_EMPID,3,5) EPD_EMPID ,replace(convert(char(40),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as EPD_FIRST_NAME " +
                         "  FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                         "  on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' AND " +
                         "  EOD_TYPE =  '" + ddlPersonnelType.SelectedValue + "'";
            }

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


        private void FillReader()
        {

            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            {

                string strSql = "select Convert(varchar,READER_ID)+'-'+convert(varchar,CTLR_ID) as READER_ID,replace(convert(char(25),ltrim(READER_ID))+READER_DESCRIPTION,' ',' ' ) as  Reader_Description  from ACS_READER  where READER_ISDELETED='0' ORDER BY CTLR_ID";

                SqlConnection con = new SqlConnection(m_connectons);
                con.Open();
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);
                lstReader.DataValueField = "READER_ID";
                lstReader.DataTextField = "Reader_Description";
                lstReader.DataSource = thisDataSet.Tables[0];
                lstReader.DataBind();

                lstReaderDummy.DataValueField = "READER_ID";
                lstReaderDummy.DataTextField = "Reader_Description";
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


        public DataSet fillReaderAndZone()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string levelId = Session["uid"].ToString();
            SqlCommand cmd = new SqlCommand("fillAccess_Settings2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@levelId", levelId);
            DataSet vai = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(vai);

            return vai;
        }


        private void FillZone()
        {
            string strSql = "select ZONE_ID,replace(convert(char(25),ltrim(ZONE_ID))+ZONE_DESCRIPTION,' ',' ' ) as  ZONE_DESCRIPTION from zone  where ZONE_ISDELETED='0' ORDER BY ZONE_ID";
            SqlConnection con = new SqlConnection(m_connectons);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            lstZone.DataValueField = "ZONE_ID";
            lstZone.DataTextField = "ZONE_DESCRIPTION";
            lstZone.DataSource = thisDataSet.Tables[0];
            lstZone.DataBind();

            lstZoneDummy.DataValueField = "ZONE_ID";
            lstZoneDummy.DataTextField = "ZONE_DESCRIPTION";
            lstZoneDummy.DataSource = thisDataSet.Tables[0];
            lstZoneDummy.DataBind();

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("VisitorAccessMovementReport.aspx");
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
                //if (rdbEmployee.SelectedIndex == 0)
                    EmployeeHdn = a.empAllSelect(lstEmployee);
                    //EmployeeHdn.Value = "";
            if (ReaderHdn.Value == "")
                ReaderHdn = a.empAllSelect(lstReader);
            if (ZoneHdn.Value == "")
                ZoneHdn = a.empAllSelect(lstZone);

        }
    }
}