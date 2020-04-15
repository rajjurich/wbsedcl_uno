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
using UNO;

namespace UNO
{
    public partial class rptManagerWiseReport : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "";

        void Page_PreInit(Object sender, EventArgs e)
        {
            //this.Title = "Late Comers/Early Goers Report";
        }
        DataSet globalds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Home.aspx");

            if (!IsPostBack)
            {
                
                RadioButtonList1.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('EMP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList2.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('COM','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList3.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('LOC','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList4.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('DIV','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList5.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('DEP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                RadioButtonList6.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('SFT','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGroup.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('GRP','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnGrade.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('GRD','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");
                rdbtnDesignation.Attributes.Add("onclick", "javascript:return RptEntitySpecificDetails2('DES','" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + ListBox7.ClientID + "','" + ListBox8.ClientID + "','" + ListBox9.ClientID + "','" + ListBox10.ClientID + "','" + ListBox11.ClientID + "','" + ListBox12.ClientID + "','" + lstGroupDummy.ClientID + "','" + lstGradeDummy.ClientID + "','" + lstDesignationDummy.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + rdbtnDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + HeadLbl.ClientID + "','','" + ddlPersonnelType.ClientID + "');");

                txtEmployeeSearch.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtEmployeeSearch.ClientID + "','" + ListBox1.ClientID + "','" + ListBox7.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtCompany.ClientID + "','" + ListBox2.ClientID + "','" + ListBox8.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtLocation.ClientID + "','" + ListBox3.ClientID + "','" + ListBox9.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDivision.ClientID + "','" + ListBox4.ClientID + "','" + ListBox10.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDepartment.ClientID + "','" + ListBox5.ClientID + "','" + ListBox11.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtShift.ClientID + "','" + ListBox6.ClientID + "','" + ListBox12.ClientID + "' );");
                txtGroup.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGroup.ClientID + "','" + lstGroup.ClientID + "','" + lstGroupDummy.ClientID + "' );");
                txtGrade.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtGrade.ClientID + "','" + lstGrade.ClientID + "','" + lstGradeDummy.ClientID + "' );");
                txtDesignation.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtDesignation.ClientID + "','" + lstDesignation.ClientID + "','" + lstDesignationDummy.ClientID + "' );");        

                //Button1.Attributes.Add("onclick", "javascript:return RptOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                 Button1.Attributes.Add("onclick", "javascript:return RptOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + DesignationHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + lstDesignation.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + txtDesignation.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                
                Button2.Attributes.Add("onclick", "javascript:return RptCloseClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtCalendarFrom.ClientID + "','" + txtToDate.ClientID + "' );");
                GetDate();


                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEmplist();
                    fillManagerlist();
                    fillControllerlist();
                    fillDesignationlist();

                }

                Fillshiftdrp();
            }

            else
            {

                fillEmplist();
                fillManagerlist();
                fillControllerlist();
                fillDesignationlist();
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

        //    SqlCommand cmd = new SqlCommand("USP_GET_EMPDETAILS_ROLEWISE", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@EmployeeCode", mgrId);

        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    DataSet thisDataSet = new DataSet();
        //    adpt.Fill(thisDataSet);
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    ListBox1.DataValueField = "EOD_EMPID";
        //    ListBox1.DataTextField = "EmployeeName";

        //    ListBox1.DataSource = thisDataSet.Tables[0];

        //    ListBox1.DataBind();



        //    ListBox7.DataValueField = "EOD_EMPID";
        //    ListBox7.DataTextField = "EmployeeName";
        //    ListBox7.DataSource = thisDataSet.Tables[0];
        //    ListBox7.DataBind();

        //    return thisDataSet;



        //    /*

        //         DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

        //         ListBox2.DataValueField = "CompanyID";
        //         ListBox2.DataTextField = "COMPANY_NAME";

        //         ListBox2.DataSource = dtCompany;

        //         ListBox2.DataBind();


        //         ListBox8.DataValueField = "CompanyID";
        //         ListBox8.DataTextField = "COMPANY_NAME";
        //         ListBox8.DataSource = dtCompany;

        //         ListBox8.DataBind();

        //         //DataTable dtCategory = thisDataSet.Tables[0].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

        //         //LstCategory.DataValueField = "CategoryID";
        //         //LstCategory.DataTextField = "Category_NAME";

        //         //LstCategory.DataSource = dtCategory;

        //         //LstCategory.DataBind();


        //         DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

        //         ListBox5.DataValueField = "DepartmentID";
        //         ListBox5.DataTextField = "Department_NAME";

        //         ListBox5.DataSource = dtDepartment;

        //         ListBox5.DataBind();

        //         ListBox11.DataValueField = "DepartmentID";
        //         ListBox11.DataTextField = "Department_NAME";
        //         ListBox11.DataSource = dtDepartment;
        //         ListBox11.DataBind();



        //         DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

        //         ListBox4.DataValueField = "DivisionID";
        //         ListBox4.DataTextField = "Division_NAME";

        //         ListBox4.DataSource = dtDivision;

        //         ListBox4.DataBind();

        //         ListBox10.DataValueField = "DivisionID";
        //         ListBox10.DataTextField = "Division_NAME";
        //         ListBox10.DataSource = dtDivision;
        //         ListBox10.DataBind();



        //         DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

        //         ListBox3.DataValueField = "LocationID";
        //         ListBox3.DataTextField = "Location_NAME";

        //         ListBox3.DataSource = dtLocation;

        //         ListBox3.DataBind();

        //         ListBox9.DataValueField = "LocationID";
        //         ListBox9.DataTextField = "Location_NAME";
        //         ListBox9.DataSource = thisDataSet.Tables[1];
        //         ListBox9.DataBind();

        //         DataTable dtGrade = thisDataSet.Tables[6].DefaultView.ToTable(true, "GrdId", "GrdName");
        //         lstGrade.DataValueField = "GrdId";
        //         lstGrade.DataTextField = "GrdName";
        //         lstGrade.DataSource = dtGrade;
        //         lstGrade.DataBind();

        //         lstGradeDummy.DataValueField = "GrdId";
        //         lstGradeDummy.DataTextField = "GrdName";
        //         lstGradeDummy.DataSource = dtGrade;
        //         lstGradeDummy.DataBind();

        //         DataTable dtGroup = thisDataSet.Tables[7].DefaultView.ToTable(true, "GrpId", "GrpName");
        //         lstGroup.DataValueField = "GrpId";
        //         lstGroup.DataTextField = "GrpName";
        //         lstGroup.DataSource = dtGroup;
        //         lstGroup.DataBind();

        //         lstGroupDummy.DataValueField = "GrpId";
        //         lstGroupDummy.DataTextField = "GrpName";
        //         lstGroupDummy.DataSource = dtGroup;
        //         lstGroupDummy.DataBind();

        //     */
        //}
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

            DataSet thisDataSet = clsCommonHandler.GetEmployeesDetails("NEPM", Type);

            ListBox1.DataValueField = "EPD_EMPID";
            ListBox1.DataTextField = "EPD_FIRST_NAME";
            ListBox1.DataSource = thisDataSet.Tables[0];
            ListBox1.DataBind();

            ListBox7.DataValueField = "EPD_EMPID";
            ListBox7.DataTextField = "EPD_FIRST_NAME";
            ListBox7.DataSource = thisDataSet.Tables[0];
            ListBox7.DataBind();

        }


        private void fillManagerlist()
        {
            DataSet thisDataSet = clsCommonHandler.GetManagerDetails();
            ListBox2.DataValueField = "EPD_EMPID";
            ListBox2.DataTextField = "EPD_FIRST_NAME";
            ListBox2.DataSource = thisDataSet.Tables[0];
            ListBox2.DataBind();

            ListBox8.DataValueField = "EPD_EMPID";
            ListBox8.DataTextField = "EPD_FIRST_NAME";
            ListBox8.DataSource = thisDataSet.Tables[0];
            ListBox8.DataBind();
        }


        private void fillDesignationlist()
        {
            DataSet thisDataSet = clsCommonHandler.GetDesignationDetails();
            lstDesignation.DataValueField = "EOD_DESIGNATION_ID";
            lstDesignation.DataTextField = "DESIGNATION_NAME";
            lstDesignation.DataSource = thisDataSet.Tables[0];
            lstDesignation.DataBind();

            lstDesignationDummy.DataValueField = "EOD_DESIGNATION_ID";
            lstDesignationDummy.DataTextField = "DESIGNATION_NAME";
            lstDesignationDummy.DataSource = thisDataSet.Tables[0];
            lstDesignationDummy.DataBind();
        }




        private void fillControllerlist()
        {
            DataSet thisDataSet = clsCommonHandler.GetControllerDetails();
            ListBox3.DataValueField = "CTLR_ID";
            ListBox3.DataTextField = "CTLR_DESCRIPTION";
            ListBox3.DataSource = thisDataSet.Tables[0];
            ListBox3.DataBind();

            ListBox9.DataValueField = "CTLR_ID";
            ListBox9.DataTextField = "CTLR_DESCRIPTION";
            ListBox9.DataSource = thisDataSet.Tables[0];
            ListBox9.DataBind();
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
        void initializeControls()
        {
            fillEmplist();
        }
        protected void View_Click(object sender, EventArgs e)
        {
            if (Session["uid"].ToString().ToLower() != "admin")
            {
                //globalds = fillEntities();
               // fillHiddenVal();
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
                //if (rdbtnLate.Checked == true)
                //{
                //    ReportViewer1.LocalReport.ReportPath = "RDLC\\Late_Report.rdlc";
                //}
                //else
                //{
                //    ReportViewer1.LocalReport.ReportPath = "RDLC\\Early.rdlc";
                //}
                ReportViewer1.LocalReport.ReportPath = "RDLC\\GetManagerAudit.rdlc";
                viewer.Visible = true;
                ReportViewer1.Visible = true;

                String dataSetName = "DataSet1";
                SqlConnection conn = new SqlConnection(m_connectons);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "usp_rpt_getManagerAuditDetails";
                cmd.Parameters.AddWithValue("@strEmployeeHdn", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);//Manager ID
                cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);//ControllerID
                cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@strGoupHdn", GroupHdn.Value);
                cmd.Parameters.AddWithValue("@strFromDate", txtCalendarFrom.Text);
                cmd.Parameters.AddWithValue("@strToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@strCheck", ddlReportType.SelectedValue == "1" ? "1" : ddlReportType.SelectedValue == "2" ? "2" : ddlReportType.SelectedValue == "0" ? "3" : "0");
                cmd.Parameters.AddWithValue("@strPersonnelType", ddlPersonnelType.SelectedValue);
                var checkvalue = txtCalendarFrom.Text;
                var checkvalue2 = txtToDate.Text;
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
                    param2 = "Late Report";
                else if (ddlReportType.SelectedValue == "2")
                    param2 = "Early Going Report";
                else
                    param2 = "Manager Audit Report";

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
            Response.Redirect("rptManagerWiseReport.aspx");
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
        }     
    }
}