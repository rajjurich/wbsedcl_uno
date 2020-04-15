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
    public partial class AccessLevelReport_SecondLevel : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "";
        DataSet globalds;     
            
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {

                RadioButtonList1.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                RadioButtonList2.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                RadioButtonList3.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                RadioButtonList4.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                RadioButtonList5.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                RadioButtonList6.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                rblReader.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('RDR','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");
                rblZone.Attributes.Add("onclick", "javascript:return AccessLevelEntityDetails('ZNE','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','" + txtReader.ClientID + "','" + ddlPersonnelType.ClientID + "','" + lstReader.ClientID + "','" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + rblReader.ClientID + "','" + rblZone.ClientID + "','" + ddlTA.ClientID + "','" + DropDownList1.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + ListBox1.ClientID + "','" + ListBox7.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + ListBox2.ClientID + "','" + ListBox8.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + ListBox3.ClientID + "','" + ListBox9.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + ListBox4.ClientID + "','" + ListBox10.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + ListBox5.ClientID + "','" + ListBox11.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + ListBox6.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");
                txtReader.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtReader.ClientID + "','" + lstReader.ClientID + "','" + lstReaderDummy.ClientID + "' );");
                txtZone.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + lstZoneDummy.ClientID + "' );");


                Button1.Attributes.Add("onclick", "javascript:return AccessLevelOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + ReaderHdn.ClientID + "','" + txtReader.ClientID + "','" + lstZone.ClientID + "','" + ZoneHdn.ClientID + "','" + txtZone.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return AccessLevelCancelClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + rblReader.ClientID + "','" + ReaderHdn.ClientID + "','" + lstZone.ClientID + "','" + ZoneHdn.ClientID + "','" + txtZone.ClientID + "','" + rblZone.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtCalendarFrom.ClientID + "','" + txtToDate.ClientID + "' );");
                GetDate();

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                  
                    fillEmplist();
                    hdnUser.Value = "admin";
                }
                else
                {
                    fillEntities();
                    hdnUser.Value = "notAdmin";
                }
                
              
                FillReader();
                FillZone();

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

            //SqlCommand cmd = new SqlCommand("USP_GET_EMPDETAILS_ROLEWISE", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmployeeCode", mgrId);

            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet thisDataSet = new DataSet();
            //adpt.Fill(thisDataSet);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}

            SqlCommand cmd = new SqlCommand("spFillEntities2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelid", levelId);

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



            DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            ListBox4.DataValueField = "DivisionID";
            ListBox4.DataTextField = "Division_NAME";

            ListBox4.DataSource = dtDivision;

            ListBox4.DataBind();

            ListBox10.DataValueField = "DivisionID";
            ListBox10.DataTextField = "Division_NAME";
            ListBox10.DataSource = thisDataSet.Tables[3];
            ListBox10.DataBind();



            DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            ListBox3.DataValueField = "LocationID";
            ListBox3.DataTextField = "Location_NAME";

            ListBox3.DataSource = dtLocation;

            ListBox3.DataBind();

            ListBox9.DataValueField = "LocationID";
            ListBox9.DataTextField = "Location_NAME";
            ListBox9.DataSource = thisDataSet.Tables[1];
            ListBox9.DataBind();

            ///////////////////////////////////////////////////////////////////////
            DataTable dtcom = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

            ListBox2.DataValueField = "CompanyID";
            ListBox2.DataTextField = "COMPANY_NAME";

            ListBox2.DataSource = dtcom;

            ListBox2.DataBind();

            ListBox8.DataValueField = "CompanyID";
            ListBox8.DataTextField = "COMPANY_NAME";
            ListBox8.DataSource = thisDataSet.Tables[4];
            ListBox8.DataBind();



            DataTable dtDep = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            ListBox5.DataValueField = "DepartmentID";
            ListBox5.DataTextField = "Department_NAME";

            ListBox5.DataSource = dtDep;

            ListBox5.DataBind();

            ListBox11.DataValueField = "DepartmentID";
            ListBox11.DataTextField = "Department_NAME";
            ListBox11.DataSource = thisDataSet.Tables[2];
            ListBox11.DataBind();



            DataTable dtGrade = thisDataSet.Tables[6].DefaultView.ToTable(true, "gradeid", "grade_name");
            lstGrade.DataValueField = "gradeid";
            lstGrade.DataTextField = "grade_name";
            lstGrade.DataSource = dtGrade;
            lstGrade.DataBind();

            lstGradeDummy.DataValueField = "gradeid";
            lstGradeDummy.DataTextField = "grade_name";
            lstGradeDummy.DataSource = dtGrade;
            lstGradeDummy.DataBind();

            DataTable dtGroup = thisDataSet.Tables[7].DefaultView.ToTable(true, "groupid", "group_name");
            lstGroup.DataValueField = "groupid";
            lstGroup.DataTextField = "group_name";
            lstGroup.DataSource = dtGroup;
            lstGroup.DataBind();

            lstGroupDummy.DataValueField = "groupid";
            lstGroupDummy.DataTextField = "group_name";
            lstGroupDummy.DataSource = dtGroup;
            lstGroupDummy.DataBind();

            return thisDataSet;


        }
        //private DataSet fillEntities()
        //{

        //    string mgrId = Session["uid"].ToString();

        //    string levelId = Session["levelId"].ToString();
        //    SqlConnection con = new SqlConnection(m_connectons);

        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    //SqlCommand cmd = new SqlCommand("USP_GET_EMPDETAILS_ROLEWISE", con);
        //    //cmd.CommandType = CommandType.StoredProcedure;
        //    //cmd.Parameters.AddWithValue("@EmployeeCode", mgrId);

        //    //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    //DataSet thisDataSet = new DataSet();
        //    //adpt.Fill(thisDataSet);

        //    SqlCommand cmd = new SqlCommand("spFillEntities2", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@levelid", levelId);

        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    DataSet thisDataSet = new DataSet();
        //    adpt.Fill(thisDataSet);

        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }


        //    //Employee
        //    ListBox1.DataValueField = "EOD_EMPID";
        //    ListBox1.DataTextField = "EmployeeName";
        //    ListBox1.DataSource = thisDataSet.Tables[0];
        //    ListBox1.DataBind();


        //    ListBox7.DataValueField = "EOD_EMPID";
        //    ListBox7.DataTextField = "EmployeeName";
        //    ListBox7.DataSource = thisDataSet.Tables[0];
        //    ListBox7.DataBind();



        //    DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

        //    ListBox4.DataValueField = "DivisionID";
        //    ListBox4.DataTextField = "Division_NAME";

        //    ListBox4.DataSource = dtDivision;

        //    ListBox4.DataBind();

        //    ListBox10.DataValueField = "DivisionID";
        //    ListBox10.DataTextField = "Division_NAME";
        //    ListBox10.DataSource = thisDataSet.Tables[3];
        //    ListBox10.DataBind();



        //    DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

        //    ListBox3.DataValueField = "LocationID";
        //    ListBox3.DataTextField = "Location_NAME";

        //    ListBox3.DataSource = dtLocation;

        //    ListBox3.DataBind();

        //    ListBox9.DataValueField = "LocationID";
        //    ListBox9.DataTextField = "Location_NAME";
        //    ListBox9.DataSource = thisDataSet.Tables[1];
        //    ListBox9.DataBind();

        //    ///////////////////////////////////////////////////////////////////////
        //    DataTable dtcom = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

        //    ListBox2.DataValueField = "CompanyID";
        //    ListBox2.DataTextField = "COMPANY_NAME";

        //    ListBox2.DataSource = dtcom;

        //    ListBox2.DataBind();

        //    ListBox8.DataValueField = "CompanyID";
        //    ListBox8.DataTextField = "COMPANY_NAME";
        //    ListBox8.DataSource = thisDataSet.Tables[4];
        //    ListBox8.DataBind();

           

        //    DataTable dtDep = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

        //    ListBox5.DataValueField = "DepartmentID";
        //    ListBox5.DataTextField = "Department_NAME";

        //    ListBox5.DataSource = dtDep;

        //    ListBox5.DataBind();

        //    ListBox11.DataValueField = "DepartmentID";
        //    ListBox11.DataTextField = "Department_NAME";
        //    ListBox11.DataSource = thisDataSet.Tables[2];
        //    ListBox11.DataBind();

        //    return thisDataSet;

        //}
        public void fillEmplist()
        {
            if (ddlPersonnelType.SelectedIndex == 0)
            {
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEntity(ddlPersonnelType.SelectedValue);
                    hdnUser.Value = "admin";
                }
                else
                {
                    fillEntities();
                    hdnUser.Value = "notAdmin";
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
        private void FillReader()
        {

           // if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            //{
                DataSet thisDataSet = clsCommonHandler.GetReaderZoneDetails("RDR");
                lstReader.DataValueField = "READER_ID";
                lstReader.DataTextField = "Reader_Description";
                lstReader.DataSource = thisDataSet.Tables[0];
                lstReader.DataBind();

                lstReaderDummy.DataValueField = "READER_ID";
                lstReaderDummy.DataTextField = "Reader_Description";
                lstReaderDummy.DataSource = thisDataSet.Tables[0];
                lstReaderDummy.DataBind();
           // }
            //else
            //{
            //    DataSet ds = fillReaderAndZone();

            //    if (ds.Tables.Count > 0)
            //    {
            //        lstReader.DataValueField = "ID";
            //        lstReader.DataTextField = "DESCRIPTION";
            //        lstReader.DataSource = ds.Tables[0];
            //        lstReader.DataBind();

            //        lstReaderDummy.DataValueField = "ID";
            //        lstReaderDummy.DataTextField = "DESCRIPTION";
            //        lstReaderDummy.DataSource = ds.Tables[0];
            //        lstReaderDummy.DataBind();
            //    }

            //}
        }
        private void FillZone()
        {
            DataSet thisDataSet = clsCommonHandler.GetReaderZoneDetails("ZNE");
            lstZone.DataValueField = "ZONE_ID";
            lstZone.DataTextField = "ZONE_DESCRIPTION";
            lstZone.DataSource = thisDataSet.Tables[0];
            lstZone.DataBind();

            lstZoneDummy.DataValueField = "ZONE_ID";
            lstZoneDummy.DataTextField = "ZONE_DESCRIPTION";
            lstZoneDummy.DataSource = thisDataSet.Tables[0];
            lstZoneDummy.DataBind();

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
            //if (ReaderHdn.Value == "")
                //    ReaderHdn = a.empAllSelect(lstReader);
            
            if (ZoneHdn.Value == "")
                ZoneHdn = a.empAllSelect(lstZone);

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
                
                ReportViewer1.LocalReport.ReportPath = "RDLC\\Second_Level_Access_Report.rdlc";

                viewer.Visible = true;
                ReportViewer1.Visible = true;

                SqlConnection conn = new SqlConnection(m_connectons);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("usp_getAccessControlDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@strPersonnelType", ddlPersonnelType.SelectedValue);
                cmd.Parameters.AddWithValue("@strTA", ddlTA.SelectedValue);
                cmd.Parameters.AddWithValue("@strUID", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@strEventStatus", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@strEmployeeHdn", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@strGroupHdn", GroupHdn.Value);
                cmd.Parameters.AddWithValue("@strReaderHdn", ReaderHdn.Value);
               // cmd.Parameters.AddWithValue("@strZoneHdn", ZoneHdn.Value);
                cmd.Parameters.AddWithValue("@strCalendarFrom", txtCalendarFrom.Text);
                cmd.Parameters.AddWithValue("@strToDate", txtToDate.Text);

                DataSet ds = new DataSet();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "DataSet1";
              

                //DataSet thisDataSet = ExecuteQuery(strQuery, dataSetName, tableName);


                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
               

                string strReportHeader2 = "";
                if (txtCalendarFrom.Text == txtToDate.Text)
                {
                    strReportHeader = strReportHeader + strDate;
                }
                else
                {
                    strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
                }
                ReportParameter para = new ReportParameter("Parameter1", strReportHeader);
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });
                
                if (ddlPersonnelType.SelectedIndex == 0)
                {
                    strReportHeader2 = "Employees";
                }
                else
                {
                    strReportHeader2 = "Non-Employees";
                }
                ReportParameter para2 = new ReportParameter("Parameter2", strReportHeader2);
                ReportParameter par3 = new ReportParameter("count", ds.Tables[0].Rows.Count.ToString());
                ReportParameter para5 = new ReportParameter("userName", Session["uid"].ToString() + " " + Session["loginName"].ToString());

                DataTable dtReport = clsCommonHandler.GetReportHeader();
                ReportParameter header = new ReportParameter("header", dtReport.Rows[0]["value"].ToString());
                ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
               
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para2, par3, para5, header, address });
               
                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = ds.Tables[0];               
                ReportViewer1.LocalReport.DataSources.Add(datasource);               
                ReportViewer1.LocalReport.Refresh();
               
                HeadPnl.Visible = false;
                HeadPanel.Visible = false;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
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
            Response.Redirect("accesslevelreport_secondlevel.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("TADashboard.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
            GetDate();
        }
        protected void Close_Click1(object sender, EventArgs e)
        {

        }

    }
}