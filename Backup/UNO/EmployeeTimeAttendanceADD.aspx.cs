using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace UNO
{
    public partial class EmployeeTimeAttendanceADD : System.Web.UI.Page
    {
        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','"+lstEmployDummy.ClientID+"');");
                RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCategory.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('CAT', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");

                txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtShift.ClientID + "','" + LstCategory.ClientID + "','" + LstCategoryDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");


                btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");
                //Close
                btnClose.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");

                /*<< */
                cmdEntityAllLeft.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*>> */
                cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*> */
                cmdEntityRight.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*< */
                cmdEntityLeft.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    FillEmployeeEntity();
                    //FillCompanyEntity();
                    //FillLocationEntity();
                    //FillDivisionEntity();
                    //FillDepartmentEntity();
                    //FillCategoryEntity();

                }
                else
                {
                    fillEntities();
                }

                
                fillweeklyOff();
                fillweeklyEnd();

                FillShiftMaster();

                Fillshift();
                FillshiftPattern();

                //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");

            }


            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>pageloadShift();</script>");

            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");
        }
        private void fillEntities()
        {
            string mgrId = Session["uid"].ToString();

            string levelId = Session["levelId"].ToString();
            SqlConnection con = new SqlConnection(m_connections);

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
            LstEmployee.DataValueField = "EOD_EMPID";
            LstEmployee.DataTextField = "EmployeeName";

            LstEmployee.DataSource = thisDataSet.Tables[0];

            LstEmployee.DataBind();


            lstEmployDummy.DataValueField = "EOD_EMPID";
            lstEmployDummy.DataTextField = "EmployeeName";
            lstEmployDummy.DataSource = thisDataSet.Tables[0];
            lstEmployDummy.DataBind();

            //DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

            //LstCompany.DataValueField = "CompanyID";
            //LstCompany.DataTextField = "COMPANY_NAME";

            //LstCompany.DataSource = dtCompany;

            //LstCompany.DataBind();

            //ListBox8.DataValueField = "CompanyID";
            //ListBox8.DataTextField = "COMPANY_NAME";
            //ListBox8.DataSource = dtCompany;

            //ListBox8.DataBind();
            ///*
            //DataTable dtCategory = thisDataSet.Tables[0].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "Category_NAME";

            //LstCategory.DataSource = dtCategory;

            //LstCategory.DataBind();
            // */


            //DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            //LstDepartment.DataValueField = "DepartmentID";
            //LstDepartment.DataTextField = "Department_NAME";

            //LstDepartment.DataSource = dtDepartment;

            //LstDepartment.DataBind();

            //ListBox11.DataValueField = "DepartmentID";
            //ListBox11.DataTextField = "Department_NAME";
            //ListBox11.DataSource = dtDepartment;
            //ListBox11.DataBind();



            //DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            //LstDivision.DataValueField = "DivisionID";
            //LstDivision.DataTextField = "Division_NAME";

            //LstDivision.DataSource = dtDivision;

            //LstDivision.DataBind();


            //ListBox10.DataValueField = "DivisionID";
            //ListBox10.DataTextField = "Division_NAME";
            //ListBox10.DataSource = dtDivision;
            //ListBox10.DataBind();

            //DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            //LstLocation.DataValueField = "LocationID";
            //LstLocation.DataTextField = "Location_NAME";

            //LstLocation.DataSource = dtLocation;

            //LstLocation.DataBind();

            //ListBox9.DataValueField = "LocationID";
            //ListBox9.DataTextField = "Location_NAME";
            //ListBox9.DataSource = dtLocation;
            //ListBox9.DataBind();

        }
        private void FillEmployeeEntity()
        {
            /*
            string strSql = "";
            strSql = "SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME )) + epd_empid,' ',' ')   as NAME FROM ENT_employee_personal_dtls  where epd_isdeleted='0'";
            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstEmployee.DataValueField = "ID";
            LstEmployee.DataTextField = "NAME";
            LstEmployee.DataSource = thisDataSet.Tables[0];
            LstEmployee.DataBind();
            */


            string strSql = "";

            //strSql = "SELECT EPD_EMPID as ID ,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
            //         " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  " +
            //         " on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' ";

            strSql = "SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME "+
            " FROM ENT_employee_personal_dtls  emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  on emp.EPD_EMPID=eod.EOD_EMPID "+
            " where EPD_EMPID not in  (select etc_emp_id from TNA_EMPLOYEE_TA_CONFIG) " +
            " and epd_isdeleted='0' and eod_active = '1' ";




            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstEmployee.DataValueField = "ID";
            LstEmployee.DataTextField = "NAME";
            LstEmployee.DataSource = thisDataSet.Tables[0];
            LstEmployee.DataBind();

            lstEmployDummy.DataValueField = "ID";
            lstEmployDummy.DataTextField = "NAME";
            lstEmployDummy.DataSource = thisDataSet.Tables[0];
            lstEmployDummy.DataBind();


        }
        public void fillweeklyOff()
        {
            string strSql = "select mwk_cd from dbo.TA_WKLYOFF where MWK_OFF=1";



            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ddweekoff.DataSource = thisDataSet;
            ddweekoff.DataValueField = "mwk_cd";
            ddweekoff.DataTextField = "mwk_cd";
            ddweekoff.DataBind();


        }
        public void fillweeklyEnd()
        {
            string strSql = "select mwk_cd from dbo.TA_WKLYOFF where MWK_OFF=0";
            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);
            ddWeekend.DataSource = thisDataSet;
            ddWeekend.DataValueField = "mwk_cd";
            ddWeekend.DataTextField = "mwk_cd";
            ddWeekend.DataBind();


        }
        private void FillShiftMaster()
        {

            string strSql = "";

            //strSql = "select '-1' as SHIFT_ID,'Select One' as SHIFT union  select SHIFT_ID,SHIFT_ID + '-' +SHIFT_DESCRIPTION as SHIFT from TA_SHIFT where SHIFT_ISDELETED='0'";

            strSql = " select '-1' as SHIFT_ID,'Select One' as SHIFT union ";
            strSql = strSql + " select   SHIFT_ID as ID,replace(convert(char(20),ltrim(SHIFT_DESCRIPTION  ))+ SHIFT_ID,' ',' ' ) as SHIFT ";
            strSql = strSql + " from TA_SHIFT where SHIFT_ISDELETED='0'";



            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            txtshiftcode.DataValueField = "SHIFT_ID";
            txtshiftcode.DataTextField = "SHIFT";
            txtshiftcode.DataSource = thisDataSet.Tables[0];
            txtshiftcode.DataBind();




            txtshiftcode.SelectedValue = "-1";

        }
        private void FillCompanyEntity()
        {

            string strSql = "";

            strSql = "SELECT COMPANY_ID as ID,replace(convert(char(20),ltrim(COMPANY_NAME  ))+ COMPANY_ID,' ',' ' ) as NAME FROM ENT_COMPANY where COMPANY_ISDELETED='0'";


            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstCompany.DataValueField = "ID";
            LstCompany.DataTextField = "NAME";

            LstCompany.DataSource = thisDataSet.Tables[0];

            LstCompany.DataBind();


        }
        private void FillLocationEntity()
        {

            string strSql = "";

            strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstLocation.DataValueField = "ID";
            LstLocation.DataTextField = "NAME";

            LstLocation.DataSource = thisDataSet.Tables[0];

            LstLocation.DataBind();


        }
        private void FillDivisionEntity()
        {

            string strSql = "";

            strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  )) + oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DIV' and  oce_isdeleted='0'";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstDivision.DataValueField = "ID";
            LstDivision.DataTextField = "NAME";

            LstDivision.DataSource = thisDataSet.Tables[0];

            LstDivision.DataBind();


        }
        private void FillDepartmentEntity()
        {

            string strSql = "";

            strSql = "SELECT oce_id AS ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  )) + oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DEP' and  oce_isdeleted='0'";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstDepartment.DataValueField = "ID";
            LstDepartment.DataTextField = "NAME";

            LstDepartment.DataSource = thisDataSet.Tables[0];

            LstDepartment.DataBind();


        }
        private void FillCategoryEntity()
        {

            string strSql = "";

            strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='CAT' and  oce_isdeleted='0'";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstCategory.DataValueField = "ID";
            LstCategory.DataTextField = "NAME";

            LstCategory.DataSource = thisDataSet.Tables[0];

            LstCategory.DataBind();


            LstCategoryDummy.DataValueField = "ID";
            LstCategoryDummy.DataTextField = "NAME";
            LstCategoryDummy.DataSource = thisDataSet.Tables[0];
            LstCategoryDummy.DataBind();



        }
        protected void CmdOk_Click(object sender, EventArgs e)
        {
            try
            {
             
                SqlConnection conn = new SqlConnection(m_connections);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_Insert_TimeAttendanceConfiguration_New]";
                cmd.CommandTimeout = 0;
                string str_Final_shft_dt = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                if (rblScheduleType.SelectedIndex == 1)
                {
                    cmd.Parameters.AddWithValue("@shiftCode", ddlShift.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@shiftCode", ddlShiftPattern.SelectedValue);
                }

                cmd.Parameters.AddWithValue("@MinSwipe", txtMinimumSwipe.Text);
                cmd.Parameters.AddWithValue("@Weekendoff", ddWeekend.SelectedValue);
                cmd.Parameters.AddWithValue("@Weeklyoff", ddweekoff.SelectedValue);
                cmd.Parameters.AddWithValue("@shift_start_date", str_Final_shft_dt);
                cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@Loccde", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@catcde", ShiftHdn.Value);
                cmd.Parameters.AddWithValue("@scheduleType", rblScheduleType.SelectedValue);
                if (rblScheduleType.SelectedIndex == 1)
                {
                    cmd.Parameters.AddWithValue("@shiftType", ddlShift.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@shiftType", ddlShiftPattern.SelectedValue);
                }
                int j = cmd.ExecuteNonQuery();
                Reset();
                resetEntity();
                lblMsg.Text = "Record Saved Succesfully";
              


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeTimeAttendance");
            }
        }
        private void Fillshift()
        {
            string strSql = "";

            strSql = "SELECT SHIFT_ID,SHIFT_DESCRIPTION FROM dbo.TA_SHIFT WHERE SHIFT_ISDELETED = '0'";


            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable thisDataSet = new DataTable();
            adpt.Fill(thisDataSet);

            ddlShift.DataValueField = "SHIFT_ID";
            ddlShift.DataTextField = "SHIFT_DESCRIPTION";
            ddlShift.DataSource = thisDataSet;
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("Select One", "0"));
            //lblShiftType.Text = "Select Shift";
            pnlshifttype.Visible = true;
        }
        private void FillshiftPattern()
        {
            string strSql = "";

            //  strSql = "SELECT SHIFT_PATTERN_ID,SHIFT_PATTERN_DESCRIPTION FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0' ";


            strSql = "SELECT SHIFT_PATTERN_ID,SHIFT_PATTERN_DESCRIPTION + ' - ' + CASE WHEN SHIFT_PATTERN_TYPE = 'WK' THEN 'WEEKLY'  WHEN SHIFT_PATTERN_TYPE = 'DL' THEN 'DAILY'" +
                      "WHEN  SHIFT_PATTERN_TYPE = 'MN' THEN 'MONTHLY' " +
                      "WHEN SHIFT_PATTERN_TYPE = 'BW' THEN 'BI-WEEKLY' END AS SHIFT_PATTERN_DESCRIPTION " +
                      "FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0'";

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable thisDataSet = new DataTable();
            adpt.Fill(thisDataSet);


            ddlShiftPattern.DataValueField = "SHIFT_PATTERN_ID";
            ddlShiftPattern.DataTextField = "SHIFT_PATTERN_DESCRIPTION";
            ddlShiftPattern.DataSource = thisDataSet;
            ddlShiftPattern.DataBind();

            ddlShiftPattern.Items.Insert(0, new ListItem("Select One", "0"));

            //lblShiftType.Text = "Select Shift Pattern";
            pnlshifttype.Visible = true;
        }     
        protected void resetEntity()
        {
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            {
                FillEmployeeEntity();
                //FillCompanyEntity();
                //FillLocationEntity();
                //FillDivisionEntity();
                //FillDepartmentEntity();
                //FillCategoryEntity();
            }
            else
            {
                fillEntities();
            }
        }
        protected void RbdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillEntityList();
        }

        protected void ddweekoff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddWeekend_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtshiftcode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMinimumSwipe_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMinimumSwipe_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void txtshiftStartDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtshiftStartDate_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeTimeAttendanceView.aspx");
        }

        protected void btnbak_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeTimeAttendanceView.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //FillEmployeeEntity();
            //FillCompanyEntity();
            //FillLocationEntity();
            //FillDivisionEntity();
            //FillDepartmentEntity();
            //FillCategoryEntity();

        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlShiftPattern_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click1(object sender, EventArgs e)
        {
            //FillEmployeeEntity();
            //FillCompanyEntity();
            //FillLocationEntity();
            //FillDivisionEntity();
            //FillDepartmentEntity();
            //FillCategoryEntity();
            txtSearchEmp.Text = "";
        }


        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = Convert.ToInt32(((DropDownList)gvEmpMgrAdd.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = gvEmpMgrAdd.PageIndex - 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmpMgrAdd.PageIndex = gvEmpMgrAdd.PageIndex + 1;
                bindData();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        public void bindData()
        {
            SqlConnection conn = new SqlConnection(m_connections);
            conn.Open();
            try
            {              
               
                string strSql = "select EOD_EMPID,epd_first_name + ' ' + epd_last_name as Empname, ";
                strSql += " ent_employee_official_dtls.EOD_DIVISION_ID from ";
                strSql += " ENT_EMPLOYEE_OFFICIAL_DTLS,ent_employee_personal_dtls ";
                strSql += " where ent_employee_personal_dtls.epd_empid=ENT_EMPLOYEE_OFFICIAL_DTLS.eod_empid";

                if (EmployeeHdn.Value != "")
                {
                    strSql += " and ent_employee_official_dtls.EOD_EMPID in " + EmployeeHdn.Value + "";
                }
                if (ComapnyHdn.Value != "")
                {
                    strSql += " and ent_employee_official_dtls.EOD_COMPANY_ID in " + ComapnyHdn.Value + "";
                }
                if (LocationHdn.Value != "")
                {
                    strSql += " and ent_employee_official_dtls.EOD_LOCATION_ID in " + LocationHdn.Value + "";
                }
                if (DivisionHdn.Value != "")
                {
                    strSql += " and ent_employee_official_dtls.EOD_DIVISION_ID in " + DivisionHdn.Value + "";
                }
                if (DepartmentHdn.Value != "")
                {
                    strSql += " and ent_employee_official_dtls.EOD_DEPARTMENT_ID in " + DepartmentHdn.Value + "";
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmdBindgv = new SqlCommand(strSql, conn);
                SqlDataAdapter dapBndgv = new SqlDataAdapter(cmdBindgv);
                DataTable dt = new DataTable();
                dapBndgv.Fill(dt);
                gvEmpMgrAdd.DataSource = dt;
                gvEmpMgrAdd.DataBind();


                DropDownList ddl = (DropDownList)gvEmpMgrAdd.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmpMgrAdd.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmpMgrAdd.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmpMgrAdd.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmpMgrAdd.PageCount == 0)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrAdd.PageIndex + 1 == gvEmpMgrAdd.PageCount)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmpMgrAdd.PageIndex == 0)
                {
                    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpMgrAdd.PageSize * gvEmpMgrAdd.PageIndex) + 1) + " to " + (gvEmpMgrAdd.PageSize * (gvEmpMgrAdd.PageIndex + 1));

                gvEmpMgrAdd.BottomPagerRow.Visible = true;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }


        }
        protected void okclick1(object sender, EventArgs e)
        {
            bindData();
        }

        private void Reset()
        {
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            ShiftHdn.Value = "";
            RbdEmp.SelectedIndex = 0;
            RbdEmp.Enabled = true;
            RbdCompany.Enabled = true;
            RbdLocation.Enabled = true;
            RbdDivision.Enabled = true;
            RbdDepartment.Enabled = true;
            RbdCategory.Enabled = true;
            RbdCompany.SelectedIndex = 0;
            RbdLocation.SelectedIndex = 0;
            RbdDivision.SelectedIndex = 0;
            RbdDepartment.SelectedIndex = 0;
            RbdCategory.SelectedIndex = 0;            
            txtshiftStartDate.Text = "";
            txtMinimumSwipe.Text = "";
            ddweekoff.SelectedIndex = 0;
            txtshiftcode.Enabled = true;
            txtshiftStartDate.Enabled = true;
            ddlShift.SelectedIndex = 0;
            ddlShiftPattern.SelectedIndex = 0;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();

        }

        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}