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
using System.Linq;
using System.Linq.Expressions;

using Microsoft.Reporting.WebForms;


namespace UNO
{
    public partial class CardholderActivationdetails : System.Web.UI.Page
    {

        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        string strDate = "";
     
    
        DataSet globalds=null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
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
                txtZone.Attributes.Add("onkeyup", "javascript:return FilterListBox('" + txtZone.ClientID + "','" + lstZone.ClientID + "','" + lstZoneDummy.ClientID + "' );");

                Button1.Attributes.Add("onclick", "javascript:return ReaderOkClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + ReaderHdn.ClientID + "','" + txtReader.ClientID + "');");
                Button2.Attributes.Add("onclick", "javascript:return ReaderCancelClick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "','" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "','" + ShiftHdn.ClientID + "','" + GroupHdn.ClientID + "','" + GradeHdn.ClientID + "','" + ListBox1.ClientID + "','" + ListBox2.ClientID + "','" + ListBox3.ClientID + "','" + ListBox4.ClientID + "','" + ListBox5.ClientID + "','" + ListBox6.ClientID + "','" + lstGroup.ClientID + "','" + lstGrade.ClientID + "','" + RadioButtonList1.ClientID + "','" + RadioButtonList2.ClientID + "','" + RadioButtonList3.ClientID + "','" + RadioButtonList4.ClientID + "','" + RadioButtonList5.ClientID + "','" + RadioButtonList6.ClientID + "','" + rdbtnGroup.ClientID + "','" + rdbtnGrade.ClientID + "','" + Button1.ClientID + "','" + Button2.ClientID + "','" + txtEmployeeSearch.ClientID + "','" + txtCompany.ClientID + "','" + txtLocation.ClientID + "','" + txtDivision.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + txtGroup.ClientID + "','" + txtGrade.ClientID + "','" + Panel1.ClientID + "','" + txtCalendarFrom.ClientID + "','" + View.ClientID + "','" + Button4.ClientID + "','" + lstReader.ClientID + "','" + rblReader.ClientID + "','" + ReaderHdn.ClientID + "');");
                View.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtCalendarFrom.ClientID + "','" + txtToDate.ClientID + "' );");

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    fillEmplist();                

                }
                else
                {

                    fillEntities();

                }
              
                FillReader();
                FillZone();
                strDate = txtCalendarFrom.Text;
            }

            else
            {

                fillEmplist();
            }


            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            if (userAgent.Contains("MSIE 7.0"))

                DateHdn.Value = DateTime.Now.ToString("dd/MM/yyyy");


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
        private void FillReader()
        {
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            {

                DataSet thisDataSet = clsCommonHandler.GetReaderZoneDetails("RDR");
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
            if (ReaderHdn.Value == "")
                ReaderHdn = a.empAllSelect(lstReader);
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
                ReportViewer1.LocalReport.ReportPath = "RDLC\\CardholderActivendetails.rdlc";

                String dataSetName = "DataSet1";
                SqlConnection conn = new SqlConnection(m_connectons);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "usp_getCardHolderDetails";
                cmd.Parameters.AddWithValue("@strPersonnelType", ddlPersonnelType.SelectedValue);
                cmd.Parameters.AddWithValue("@strEmployeeHdn", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@strComapnyHdn", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@strLocationHdn", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@strDivisionHdn", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@strDepartmentHdn", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@strGradeHdn", GradeHdn.Value);
                cmd.Parameters.AddWithValue("@strGroupHdn", GroupHdn.Value);
                cmd.Parameters.AddWithValue("@strReaderHdn", ReaderHdn.Value);

                DataTable dtData = new DataTable();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dtData);
                string param2;
                param2 = "Card Holder Detail Report";
                string strReportHeader = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs for ";
                if (txtCalendarFrom.Text == txtToDate.Text)
                {
                    strReportHeader = strReportHeader + strDate;
                }
                else
                {
                    strReportHeader = strReportHeader + strDate + " to " + txtToDate.Text;
                }

                if (dtData.Rows.Count > 65000)
                {
                    MessageLabel.Text = "The data for the date range selected by you exceeds the admissible limit to be displayed in a web-report. Please change the filter criteria to reduce the number of records.";
                    MessageLabel.Visible = true;
                    return;
                }
                MessageLabel.Visible = true;
                int distinctNames = dtData.AsEnumerable().Select(row => new
                                    {
                                        Name = row.Field<string>("Name")
                                    }).Distinct().Count();


                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = m_connectons;

                string comp = "", compAdd = "";
                DataTable dtComp = clsCommonHandler.GetReportHeader();

                if (dtComp.Rows.Count > 0)
                {
                    comp = dtComp.Rows[0]["value"].ToString();
                    compAdd = dtComp.Rows[1]["value"].ToString();
                }

                ReportParameter para = new ReportParameter("myparameter1", strReportHeader);
                ReportParameter para2 = new ReportParameter("header", param2);
                ReportParameter para3 = new ReportParameter("count", distinctNames.ToString());
                ReportParameter para5 = new ReportParameter("userName", Session["uid"].ToString() + " " + Session["loginName"].ToString());
                ReportParameter header = new ReportParameter("compHeader", comp);
                ReportParameter address = new ReportParameter("address", compAdd);
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para2, para3, para5, header, address });

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dtData;
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                HeadPnl.Visible = false;
                HeadPanel.Visible = false;

            }
            catch (Exception ex) 
            {  UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
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
                command.CommandTimeout = 0;
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
            Response.Redirect("CardholderActivationdetails.aspx");
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