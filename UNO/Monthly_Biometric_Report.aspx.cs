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
using System.Globalization;

namespace UNO
{
    public partial class Monthly_Biometric_Report : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string strDate = "";
        DataSet globalds;
        String m_messageString = "";
        String m_role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                //RadioButtonList1.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //RadioButtonList2.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //RadioButtonList3.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //RadioButtonList4.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //RadioButtonList5.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //RadioButtonList6.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');"); 
                //rdbtnGroup.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //rdbtnGrade.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");
                //rdbtnDesignation.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DES','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','');");

                RadioButtonList1.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList2.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList3.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList4.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList5.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList6.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnDesignation.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails1('DES','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + ListBox1.ClientID + "','" + ListBox7.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + ListBox2.ClientID + "','" + ListBox8.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + ListBox3.ClientID + "','" + ListBox9.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + ListBox4.ClientID + "','" + ListBox10.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + ListBox5.ClientID + "','" + ListBox11.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + ListBox6.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");
                txtDesignation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDesignation.ClientID + "','" + lstDesignation.ClientID + "','" + lstDesignationDummy.ClientID + "' );");     

               // Button1.Attributes.Add("onclick", "javascript:return RptOkClick1('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lblCompanyList.ClientID + "','" + lblProfitCenter.ClientID + "','" + lblCostCenter.ClientID + "','" + lblGrade.ClientID + "','" + lblEmployee.ClientID + "','" + lblCompanyName.ClientID + "','" + lblProfitCenterName.ClientID + "','" + lblCostCenterName.ClientID + "','" + lblGradeName.ClientID + "','" + lblEmployeeName.ClientID + "');");
                Button1.Attributes.Add("onclick", "javascript:return RptOkClick1('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lblCompanyList.ClientID + "','" + lblProfitCenter.ClientID + "','" + lblCostCenter.ClientID + "','" + lblGrade.ClientID + "','" + lblEmployee.ClientID + "','" + lblDesignation.ClientID + "','" + lblCompanyName.ClientID + "','" + lblProfitCenterName.ClientID + "','" + lblCostCenterName.ClientID + "','" + lblGradeName.ClientID + "','" + lblEmployeeName.ClientID + "','" + lblDesignationName.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return RptCloseClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtCalendarFrom.ClientID + "','" + txtToDate.ClientID + "' );");
                GetDate();
                BindMonth();
                BindYear();
               // FillEmpType();
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    //fillEmplist();
                }
                else
                {
                    fillEntities();
                }
            }

            else
            {
                //fillEmplist();
                strDate = txtCalendarFrom.Text;
            }

            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            if (userAgent.Contains("MSIE 7.0"))
                DateHdn.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void BindMonth()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            //ddlFromMonth.Items.Add(new ListItem("-Select Month-", "0"));

            for (int i = 1; i < 13; i++)
            {
                ddlFromMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
            ddlFromMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
        private void BindYear()
        {
            //ddlFromYear.Items.Add(new ListItem("-Select Year-", "0"));

            int year = DateTime.Now.Year;

            for (int i = 2012; i <= year; i++)
            {
                ddlFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlFromYear.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void GetDate()
        {
            DateTime today = DateTime.Now; //current date

            string firstDay = today.AddDays(-(today.Day - 1)).ToString("dd/MM/yyyy"); //first day

            today = today.AddMonths(1);
            DateTime lastDay = today.AddDays(-(today.Day)); //last day

            txtCalendarFrom.Text = firstDay;
            txtToDate.Text = txtCalendarFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

            DataTable dtDesignation = thisDataSet.Tables[9].DefaultView.ToTable(true, "DesignationID", "Designation_NAME");
            lstDesignation.DataValueField = "DesignationID";
            lstDesignation.DataTextField = "Designation_NAME";
            lstDesignation.DataSource = dtDesignation;
            lstDesignation.DataBind();

            lstDesignationDummy.DataValueField = "DesignationID";
            lstDesignationDummy.DataTextField = "Designation_NAME";
            lstDesignationDummy.DataSource = dtDesignation;
            lstDesignationDummy.DataBind();


            return thisDataSet;
            //DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

            //ListBox2.DataValueField = "CompanyID";
            //ListBox2.DataTextField = "COMPANY_NAME";

            //ListBox2.DataSource = dtCompany;

            //ListBox2.DataBind();



            //ListBox8.DataValueField = "CompanyID";
            //ListBox8.DataTextField = "COMPANY_NAME";
            //ListBox8.DataSource = dtCompany;
            //ListBox8.DataBind();

            //DataTable dtCategory = thisDataSet.Tables[0].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "Category_NAME";

            //LstCategory.DataSource = dtCategory;

            //LstCategory.DataBind();

            /*
            DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            ListBox5.DataValueField = "DepartmentID";
            ListBox5.DataTextField = "Department_NAME";

            ListBox5.DataSource = dtDepartment;

            ListBox5.DataBind();



            ListBox11.DataValueField = "DepartmentID";
            ListBox11.DataTextField = "Department_NAME";
            ListBox11.DataSource = dtDepartment;
            ListBox11.DataBind();



            DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            ListBox4.DataValueField = "DivisionID";
            ListBox4.DataTextField = "Division_NAME";

            ListBox4.DataSource = dtDivision;

            ListBox4.DataBind();

            ListBox10.DataValueField = "DivisionID";
            ListBox10.DataTextField = "Division_NAME";
            ListBox10.DataSource = dtDivision;
            ListBox10.DataBind();



            DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            ListBox3.DataValueField = "LocationID";
            ListBox3.DataTextField = "Location_NAME";

            ListBox3.DataSource = dtLocation;

            ListBox3.DataBind();

            ListBox9.DataValueField = "LocationID";
            ListBox9.DataTextField = "Location_NAME";
            ListBox9.DataSource = dtLocation;
            ListBox9.DataBind();

            DataTable dtGrade = thisDataSet.Tables[6].DefaultView.ToTable(true, "GrdId", "GrdName");
            lstGrade.DataValueField = "GrdId";
            lstGrade.DataTextField = "GrdName";
            lstGrade.DataSource = dtGrade;
            lstGrade.DataBind();

            lstGradeDummy.DataValueField = "GrdId";
            lstGradeDummy.DataTextField = "GrdName";
            lstGradeDummy.DataSource = dtGrade;
            lstGradeDummy.DataBind();

            DataTable dtGroup = thisDataSet.Tables[7].DefaultView.ToTable(true, "GrpId", "GrpName");
            lstGroup.DataValueField = "GrpId";
            lstGroup.DataTextField = "GrpName";
            lstGroup.DataSource = dtGroup;
            lstGroup.DataBind();

            lstGroupDummy.DataValueField = "GrpId";
            lstGroupDummy.DataTextField = "GrpName";
            lstGroupDummy.DataSource = dtGroup;
            lstGroupDummy.DataBind();*/

        }
        public void fillEmplist()
        {
            if (ddlPersonnelType.SelectedIndex == 0)
            {
                //if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEntity(ddlPersonnelType.SelectedValue);
                }
               // else
                {
                 //   fillEntities();
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
            //fillEmplist();
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

        private void ShowReport()
        {
            try
            {                
                if (check_Monthly.Checked == true)
                {
                    SqlConnection conn = new SqlConnection(m_connectons);
                    SqlDataAdapter da;
                    DataTable dtData;
                    //ReportViewer1.LocalReport.ReportPath = "RDLC\\TOUR_DETAIL_REPORT.rdlc";


                    viewer.Visible = true;
                    ReportViewer1.Visible = true;
                    ReportViewer1.ShowRefreshButton = false;
                    string locAsUnit = LocationHdn.Value;
                    string DivAsEntity = DivisionHdn.Value;
                    string EmpCode = EmployeeHdn.Value;
                    string companyAsCenter = ComapnyHdn.Value;
                    string DeptAsGroup = DepartmentHdn.Value;
                    string GradeAsDiv = GradeHdn.Value;
                    string GroupAsSection = GroupHdn.Value;

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string strcomp = "";
                    //if (companyAsCenter != "")
                    //{
                    //    string str = companyAsCenter.Replace("'", "").Replace("(", "").Replace(")", "").Replace("''", "");
                    //    string[] cid = str.Split(',');
                    //    SqlCommand cmdcompname = new SqlCommand();
                    //    cmdcompname.CommandType = CommandType.StoredProcedure;
                    //    cmdcompname.Connection = conn;
                    //    cmdcompname.CommandTimeout = 0;
                    //    cmdcompname.CommandText = "usp_getCompanyName";
                    //    cmdcompname.Parameters.AddWithValue("@companyid", cid[0].ToString());
                    //    DataTable dtDatacomp = new DataTable();
                    //    SqlDataAdapter dacomp = new SqlDataAdapter(cmdcompname);
                    //    dacomp.Fill(dtDatacomp);
                    //    strcomp = dtDatacomp.Rows[0][0].ToString();
                    //}
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    //cmd.CommandText = "SAC_TOUR_DETAILS_REPORT";



                    if (txtCalendarFrom.Text == txtToDate.Text)
                        ReportViewer1.LocalReport.ReportPath = "RDLC\\rptMonthlyBiometric.rdlc";
                    else
                        ReportViewer1.LocalReport.ReportPath = "RDLC\\rptMonthlyBiometric.rdlc";

                    

                    cmd.CommandText = "uspBiometricAttendanceReport";
                    cmd.Parameters.AddWithValue("@strEmployeeHdn", EmpCode);
                    cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);
                    cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);
                    cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                    cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                    cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                    cmd.Parameters.AddWithValue("@strDesignationHdn", DesignationHdn.Value);
                    cmd.Parameters.AddWithValue("@strGoupHdn", GroupHdn.Value);

                    //cmd.Parameters.AddWithValue("@TODATE", txtToDate.Text);
                    //cmd.Parameters.AddWithValue("@FROM_DATE", txtCalendarFrom.Text);
                    //cmd.Parameters.AddWithValue("@EmpType", ddlPersonnelType.SelectedValue);
                    cmd.Parameters.AddWithValue("@month", ddlFromMonth.SelectedValue);
                    cmd.Parameters.AddWithValue("@year", ddlFromYear.SelectedValue);
                    //if (ddlRepotType.SelectedValue == "Daily")
                    //    cmd.Parameters.AddWithValue("@date", txtCalendarFrom.Text);
                    //cmd.Parameters.AddWithValue("@empType", ddlEmpType.SelectedValue);


                    dtData = new DataTable();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(dtData);
                    dtData = BindDataTable(dtData);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    // SqlCommand cmd = new SqlCommand("EXEC SAC_TOUR_DETAILS_REPORT @EmpType='" + ddlPersonnelType.SelectedValue + "' ,  @EMP_ID='" + EmpCode + "',@FROM_DATE='" + txtCalendarFrom.Text + "',@TODATE='" + txtToDate.Text + "',@ENTITY='" + DivAsEntity + "',@GROUP='" + DeptAsGroup + "',@Division='" + GradeAsDiv + "',@Center='" + companyAsCenter + "',@Unit='" + locAsUnit + "',@Section='" + GroupAsSection + "'", conn);


                    String dataSetName = "DataSet1";
                    //String tableName = "Tour_Report";//USER DEFINED NAME
                    //DataSet thisDataSet = ExecuteQuery(cmd, dataSetName, tableName);



                    string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
                    strReportHeader = "";
                    if (txtCalendarFrom.Text == txtToDate.Text)
                    {
                        strReportHeader = strReportHeader + strDate;
                    }
                    else
                    {
                        strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
                    }
                    string strhead = ddlFromMonth.SelectedItem.Text + " " + ddlFromYear.SelectedValue;

                    strcomp = strcomp == "" ? " of All Employees" : " of " + strcomp;
                    string Header = "Check Biometric Report" + strcomp + " for the month of " + strhead;
                    if (ddlPersonnelType.SelectedValue == "E")
                    {
                        Header = "Check Biometric Report" + strcomp + " for the month of " + strhead;
                    }
                    else
                    {
                        Header = "Check Biometric Report" + strcomp + " for the month of " + strhead;
                    }
                    lblHeader.Text = Header;
                    ReportParameter para = new ReportParameter("myParameter1", strReportHeader);
                    ReportParameter rpHeader = new ReportParameter("Header", Header);
                    DataTable dtReport = clsCommonHandler.GetReportHeader();
                    ReportParameter header = new ReportParameter("heading", dtReport.Rows[0]["value"].ToString());
                    ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
                    ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, rpHeader, header, address });


                    ReportDataSource datasource = new ReportDataSource();
                    datasource.Name = dataSetName;
                    datasource.Value = dtData;// thisDataSet.Tables[tableName];
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.Refresh();

                    HeadPnl.Visible = false;
                    //Panel1.Visible = false;
                    HeadPanel.Visible = false;
                }
                else
                {
                    SqlConnection conn = new SqlConnection(m_connectons);
                    SqlDataAdapter da;
                    DataTable dtData;
                    //ReportViewer1.LocalReport.ReportPath = "RDLC\\TOUR_DETAIL_REPORT.rdlc";


                    viewer.Visible = true;
                    ReportViewer1.Visible = true;
                    ReportViewer1.ShowRefreshButton = false;
                    string locAsUnit = LocationHdn.Value;
                    string DivAsEntity = DivisionHdn.Value;
                    string EmpCode = EmployeeHdn.Value;
                    string companyAsCenter = ComapnyHdn.Value;
                    string DeptAsGroup = DepartmentHdn.Value;
                    string GradeAsDiv = GradeHdn.Value;
                    string GroupAsSection = GroupHdn.Value;

                    string P_Last_Month="";
                    string P_Last_Year = "";
                    if(Convert.ToInt32(ddlFromMonth.SelectedValue) == 1)
                    {
                        P_Last_Month="12";
                        P_Last_Year=Convert.ToString(Convert.ToInt32(ddlFromYear.SelectedValue) -1);
                    }
                    else
                    {
                        P_Last_Month=Convert.ToString(Convert.ToInt32(ddlFromMonth.SelectedValue) -1);
                        P_Last_Year=ddlFromYear.SelectedValue;
                    }

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string strcomp = "";

                    //if (companyAsCenter != "")
                    //{
                    //    string str = companyAsCenter.Replace("'", "").Replace("(", "").Replace(")", "").Replace("''", "");
                    //    string[] cid = str.Split(',');
                    //    SqlCommand cmdcompname = new SqlCommand();
                    //    cmdcompname.CommandType = CommandType.StoredProcedure;
                    //    cmdcompname.Connection = conn;
                    //    cmdcompname.CommandTimeout = 0;
                    //    cmdcompname.CommandText = "usp_getCompanyName";
                    //    cmdcompname.Parameters.AddWithValue("@companyid", cid[0].ToString());
                    //    DataTable dtDatacomp = new DataTable();
                    //    SqlDataAdapter dacomp = new SqlDataAdapter(cmdcompname);
                    //    dacomp.Fill(dtDatacomp);
                    //    strcomp = dtDatacomp.Rows[0][0].ToString();
                    //}

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    //cmd.CommandText = "SAC_TOUR_DETAILS_REPORT";

                    if (txtCalendarFrom.Text == txtToDate.Text)
                        ReportViewer1.LocalReport.ReportPath = "RDLC\\rptMonthlyBiometric1.rdlc";
                    else
                        ReportViewer1.LocalReport.ReportPath = "RDLC\\rptMonthlyBiometric1.rdlc";


                    cmd.CommandText = "uspBiometricAttendanceReport1";
                    cmd.Parameters.AddWithValue("@strEmployeeHdn", EmpCode);
                    cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);
                    cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);
                    cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                    cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                    cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                    cmd.Parameters.AddWithValue("@strDesignationHdn", DesignationHdn.Value);
                    cmd.Parameters.AddWithValue("@strGoupHdn", GroupHdn.Value);
                    //cmd.Parameters.AddWithValue("@TODATE", txtToDate.Text);
                    //cmd.Parameters.AddWithValue("@FROM_DATE", txtCalendarFrom.Text);
                    //cmd.Parameters.AddWithValue("@EmpType", ddlPersonnelType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Lastmonth", P_Last_Month);
                    cmd.Parameters.AddWithValue("@month", ddlFromMonth.SelectedValue);
                    cmd.Parameters.AddWithValue("@year", ddlFromYear.SelectedValue);
                    //if (ddlRepotType.SelectedValue == "Daily")
                    //    cmd.Parameters.AddWithValue("@date", txtCalendarFrom.Text);
                    //cmd.Parameters.AddWithValue("@empType", ddlEmpType.SelectedValue);



                    dtData = new DataTable();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(dtData);
                    //dtData = BindDataTable(dtData);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    // SqlCommand cmd = new SqlCommand("EXEC SAC_TOUR_DETAILS_REPORT @EmpType='" + ddlPersonnelType.SelectedValue + "' ,  @EMP_ID='" + EmpCode + "',@FROM_DATE='" + txtCalendarFrom.Text + "',@TODATE='" + txtToDate.Text + "',@ENTITY='" + DivAsEntity + "',@GROUP='" + DeptAsGroup + "',@Division='" + GradeAsDiv + "',@Center='" + companyAsCenter + "',@Unit='" + locAsUnit + "',@Section='" + GroupAsSection + "'", conn);


                    String dataSetName = "DataSet1";
                    //String tableName = "Tour_Report";//USER DEFINED NAME
                    //DataSet thisDataSet = ExecuteQuery(cmd, dataSetName, tableName);



                    string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
                    strReportHeader = "";
                    if (txtCalendarFrom.Text == txtToDate.Text)
                    {
                        strReportHeader = strReportHeader + strDate;
                    }
                    else
                    {
                        strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
                    }
                    //string strhead =  ddlFromMonth.SelectedItem.Text + " " + ddlFromYear.SelectedValue;
                    string strhead = " 16/"+ddlFromMonth.Items.FindByValue(P_Last_Month)+"/"+ddlFromYear.Items.FindByValue(P_Last_Year) + " to  15/" + ddlFromMonth.SelectedItem +"/"+ ddlFromYear.SelectedValue;
                    strcomp = strcomp == "" ? " of All Employees" : " of " + strcomp;
                    string Header = "Biometric Report" + strcomp + " for the Duration of " + strhead;
                    if (ddlPersonnelType.SelectedValue == "E")
                    {
                        Header = "Biometric Report" + strcomp + " for the Duration of " + strhead;
                    }
                    else
                    {
                        Header = "Biometric Report" + strcomp + " for the Duration of " + strhead;
                    }
                    lblHeader.Text = Header;
                    ReportParameter para = new ReportParameter("myParameter1", strReportHeader);
                    ReportParameter rpHeader = new ReportParameter("Header", Header);
                    DataTable dtReport = clsCommonHandler.GetReportHeader();
                    ReportParameter header = new ReportParameter("heading", dtReport.Rows[0]["value"].ToString());
                    ReportParameter address = new ReportParameter("address", dtReport.Rows[1]["value"].ToString());
                    ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, rpHeader, header, address });


                    ReportDataSource datasource = new ReportDataSource();
                    datasource.Name = dataSetName;
                    datasource.Value = dtData;// thisDataSet.Tables[tableName];
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.Refresh();

                    HeadPnl.Visible = false;
                    //Panel1.Visible = false;
                    HeadPanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private DataTable BindDataTable(DataTable dataTable)
        {
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    int countArrivalInTime = 0;
                    int countArrivalGraceTime = 0;
                    int countArrivalLate = 0;
                    int countAbsent = 0;
                    int countDepartureBeforeTime = 0;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (col.ColumnName.Contains("d_"))
                        {
                            var cellArray = row[col].ToString().Split('_');

                            if (cellArray[0].ToString() == "Ab" || cellArray[0].ToString() == "Pr" || cellArray[0].ToString() == "WO" || cellArray[0].ToString() == "HD" || cellArray[0].ToString() == "")
                            {
                                if (cellArray[0].ToString() == "Ab")
                                {
                                    countAbsent += 1;
                                }
                            }
                            else
                            {
                                if (row[col].ToString() == "0_0")
                                {
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    var inTime = DateTime.Parse(cellArray[0].ToString());

                                    var MaxArrivalTime = DateTime.Parse(row["MaxArrivalTime"].ToString());

                                    var MaxGraceTime = DateTime.Parse(row["MaxGraceTime"].ToString());

                                    var MaxLateTime = DateTime.Parse(row["MaxLateTime"].ToString());

                                    if (inTime < MaxArrivalTime)
                                    {
                                        countArrivalInTime += 1;
                                    }

                                    if (inTime >= MaxArrivalTime && inTime < MaxGraceTime)
                                    {
                                        countArrivalGraceTime += 1;
                                    }

                                    if (inTime >= MaxGraceTime && inTime < MaxLateTime)
                                    {
                                        countArrivalLate += 1;
                                    }

                                    if (inTime >= MaxLateTime)
                                    {
                                        countAbsent += 1;
                                    }
                                }
                            }
                            if (cellArray[1].ToString() == "Ab" || cellArray[1].ToString() == "Pr" || cellArray[1].ToString() == "WO" || cellArray[1].ToString() == "HD" || cellArray[1].ToString() == "")
                            {
                                Console.WriteLine("");
                            }
                            else
                            {
                                if (row[col].ToString() == "0_0")
                                {
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    var outTime = DateTime.Parse(cellArray[1].ToString());
                                    DateTime dtDepartureBeforeTime;
                                    if (cellArray[2].ToString() == "1")
                                    {
                                        dtDepartureBeforeTime = DateTime.Parse(row["MaxDepartureTimeOnWorkend"].ToString());
                                    }
                                    else
                                    {
                                        dtDepartureBeforeTime = DateTime.Parse(row["MaxDepartureTimeOnWorkday"].ToString());
                                    }
                                    if (outTime < dtDepartureBeforeTime)
                                    {
                                        countDepartureBeforeTime += 1;
                                    }
                                }
                            }

                        }
                    }
                    row["AIT"] = countArrivalInTime;
                    row["AGT"] = countArrivalGraceTime;
                    row["AL"] = countArrivalLate;
                    row["AB"] = countAbsent;
                    row["DBT"] = countDepartureBeforeTime;
                }
            }
            catch (Exception ex)
            {

            }

            return dataTable;
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
            Response.Redirect("Monthly_Biometric_Report.aspx");
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
            GetDate();
            lblCompanyList.Attributes.Add("style", "display:none;");
            lblCompanyName.Attributes.Add("style", "display:none;");
            lblProfitCenterName.Attributes.Add("style", "display:none;");
            lblProfitCenter.Attributes.Add("style", "display:none;");
            lblCostCenterName.Attributes.Add("style", "display:none;");

            lblCostCenter.Attributes.Add("style", "display:none;");
            lblGradeName.Attributes.Add("style", "display:none;");
            lblGrade.Attributes.Add("style", "display:none;");
            lblEmployeeName.Attributes.Add("style", "display:none;");
            lblEmployee.Attributes.Add("style", "display:none;");
        }

    }
}