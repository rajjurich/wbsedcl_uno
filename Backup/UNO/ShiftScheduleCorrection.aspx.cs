
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
using System.Globalization;

namespace UNO
{
    public partial class ShiftScheduleCorrection : System.Web.UI.Page
    {
        string empid;
        ArrayList arr = new ArrayList();
        DataTable MonthlyAttendance = new DataTable();
        DateTime[] dt;
        public string emloyee;
        public string strRequestDt;
        public string strFrmDt;
        public string strToDt;
        string selDates;
        int count = 0;
        string selDateCount;

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["uid"] == null || Session["uid"].ToString() == "")
            {
                Response.Redirect("Login.aspx", true);
                empid = Session["uid"].ToString();
            }

            if (ViewState["seleteddates"] != null)
            {

                selDates = ViewState["seleteddates"].ToString();

            }


            if (ViewState["count"] != null)
            {

                selDateCount = ViewState["count"].ToString();

            }


            if (!IsPostBack)
            {
                rblCorrectionType.SelectedIndex = 0;
                txtToDateSC.Enabled = false;
                RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCategory.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('CAT', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + btnSave.ClientID + "','" + lstEmployDummy.ClientID + "');");

                txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstEntitySelected.ClientID + "'  );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "'+'" + LstEntitySelected.ClientID + "'  );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstEntitySelected.ClientID + "'  );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtShift.ClientID + "','" + LstCategory.ClientID + "','" + LstCategoryDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");


                btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSave.ClientID + "');");
                //Close
                btnClose.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSave.ClientID + "');");
                /*<< */
                cmdEntityAllLeft.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*>> */
                cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*> */
                cmdEntityRight.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                /*< */
                cmdEntityLeft.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + btnClose.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");

                if (Session["uid"].ToString().ToLower() == "admin")
                {
                    FillEmployeeEntity();
                    //FillCompanyEntity();
                    //FillLocationEntity();
                    // FillDivisionEntity();
                    //FillDepartmentEntity();
                    //FillCategoryEntity();
                }
                else
                {
                    fillEntities();
                }
                //FillShiftMaster();

                empid = Session["uid"].ToString();
                //ShowLoginName();

                // GetMonthlyAttendance(DateTime.Now.Month, DateTime.Now.Year);

                //  fillAbsentStatusOutPass();
                Fillshift();
                FillshiftPattern();

            }
            empid = Session["uid"].ToString();


            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>pageloadShift();</script>");
            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>pageloadShfitType();</script>");


        }
        protected void onclick(object sender, EventArgs e)
        {
        }
        private void fillEntities()
        {
            string mgrId = Session["uid"].ToString();
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


        }
        private void FillEmployeeEntity()
        {

            string strSql = "";

            strSql = "SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
           " FROM ENT_employee_personal_dtls  emp with(nolock) inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod with(nolock) on emp.EPD_EMPID=eod.EOD_EMPID where EPD_EMPID  in  (select etc_emp_id from TNA_EMPLOYEE_TA_CONFIG with(nolock)) " +
           " and epd_isdeleted='0' and eod_active = '1'  AND ISNULL(EPD_ISDELETED,0)=0 ";

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


        }
        private DataTable GetData()
        {



            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd1 = new SqlCommand("sp_Bind_LeaveChart", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter dap = new SqlDataAdapter(cmd1);
            dap.Fill(dt);

            return dt;
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
            //lblShiftType.Text = "Select Shift";
            pnlshifttype.Visible = true;
        }
        private void FillshiftPattern()
        {
            string strSql = "";

            strSql = "SELECT SHIFT_PATTERN_ID,SHIFT_PATTERN_DESCRIPTION FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0'";


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

            //lblShiftType.Text = "Select Shift Pattern";
            pnlshifttype.Visible = true;
        }
        private void GetMonthlyAttendance(int month, int year)
        {

            string[] emp = EmployeeHdn.Value.Split(',');

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT TDAY_SFTREPO FROM TDAY WHERE DATEPART(MM,TDAY_DATE)='"
                    + month.ToString() + "' AND DATEPART(YY,TDAY_DATE)='" + year.ToString() + "' and TDAY_EMPCDE ='"
                    + emp[0].ToString() + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                MonthlyAttendance.Rows.Clear();
                da.Fill(MonthlyAttendance);
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
            }

        }
        protected void OnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                string _strErrMsg = string.Empty;
                SqlConnection conn = new SqlConnection(m_connections);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ShiftScheduleCorrection";
                cmd.CommandTimeout = 0;
                DateTime todate = DateTime.MinValue, fromDate = DateTime.MinValue;


                cmd.Parameters.AddWithValue("@pFromDate", txtFromDateSC.Text);
                cmd.Parameters.AddWithValue("@pToDate", txtToDateSC.Text);
                cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@Loccde", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@catcde", ShiftHdn.Value);
                cmd.Parameters.AddWithValue("@scheduleType", rblScheduleType.SelectedValue);
                //if (rblScheduleType.SelectedIndex == 1)
                //{
                cmd.Parameters.AddWithValue("@shiftType", ddlShift.SelectedValue);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@shiftType", ddlShiftPattern.SelectedValue);
                //}
                if (rblCorrectionType.SelectedIndex == 2)
                {
                    cmd.Parameters.AddWithValue("@pIsOnWordType", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pIsOnWordType", "0");
                }
                cmd.Parameters.AddWithValue("@strErrorMsg", '0').Direction = ParameterDirection.Output;
                cmd.Parameters["@strErrorMsg"].Size = 1000;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int j = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                _strErrMsg = cmd.Parameters["@strErrorMsg"].Value.ToString();
                if (_strErrMsg.Trim().Length > 1)
                {
                    lblErrorMsg.Text = _strErrMsg;
                    lblErrorMsg.Visible = true;
                    btnSave.Enabled = true;
                    if (rblCorrectionType.SelectedValue == "1")
                    {
                        txtToDateSC.Text = txtFromDateSC.Text;
                        txtToDateSC.Enabled = false;
                        txtFromDateSC.Enabled = true;
                    }
                    else if (rblCorrectionType.SelectedValue == "2")
                    {
                        txtToDateSC.Enabled = true;
                        txtFromDateSC.Enabled = true;
                    }
                    else if (rblCorrectionType.SelectedValue == "3")
                    {
                        txtToDateSC.Text = "";
                        txtToDateSC.Enabled = false;
                        txtFromDateSC.Enabled = true;
                    }
                    return;
                }
                else
                {
                    Clear();
                    btnSave.Enabled = true;
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Registering", "RecSave(" + (j / 2).ToString() + ")", true);
                    lblErrorMsg.Text = "Record Saved Successfully";
                    lblErrorMsg.Visible = true;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ShiftSceduleCreation");
               
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            lblErrorMsg.Text = "";
            // lblErrorMsg.Visible = false;
            Response.Redirect("Uno_Dashboard.aspx");
        }
        private void Clear()
        {
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            RbdEmp.SelectedIndex = 0;
            RbdCompany.SelectedIndex = 0;
            RbdLocation.SelectedIndex = 0;
            RbdDepartment.SelectedIndex = 0;
            RbdCategory.SelectedIndex = 0;
            RbdEmp.Enabled = true;
            RbdCompany.Enabled = true;
            RbdLocation.Enabled = true;
            RbdDivision.Enabled = true;
            RbdDepartment.Enabled = true;
            RbdCategory.Enabled = true;
            ddlShift.SelectedIndex = 0;
            ddlShiftPattern.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            ddlWeeklyOffview.SelectedIndex = 0;
            rblCorrectionType.SelectedIndex = 0;
            ShiftHdn.Value = "";
            txtToDateSC.Text = "";
            txtFromDateSC.Text = "";
            txtFrmDate.Text = "";
            txtToDate.Text = "";
            ddlShift.Enabled = true;
            ddlShiftPattern.Enabled = true;
            DropDownList1.Enabled = true;
            ddlWeeklyOffview.Enabled = true;
            rblCorrectionType.Enabled = true;
            txtToDateSC.Enabled = false;
            txtFromDateSC.Enabled = true;
            txtFrmDate.Enabled = true;
            txtToDate.Enabled = true;
            ViewState["seleteddates"] = null;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();
            resetEntity();

        }
        private void Disable()
        {

            RbdEmp.Enabled = false;
            RbdCompany.Enabled = false;
            RbdLocation.Enabled = false;
            RbdDivision.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdCategory.Enabled = false;
            ddlShift.Enabled = false;
            ddlShiftPattern.Enabled = false;
            DropDownList1.Enabled = false;
            ddlWeeklyOffview.Enabled = false;
            rblCorrectionType.Enabled = false;
            txtToDateSC.Enabled = false;
            txtFromDateSC.Enabled = false;
            txtFrmDate.Enabled = false;
            txtToDate.Enabled = false;


        }
        protected void resetEntity()
        {
            if (Session["uid"].ToString().ToLower() == "admin")
            {
                FillEmployeeEntity();
                //FillCompanyEntity();
                // FillLocationEntity();
                // FillDivisionEntity();
                // FillDepartmentEntity();
                // FillCategoryEntity();
            }
            else
            {
                fillEntities();
            }
        }
        private int GetDateForWeekDay(DayOfWeek DesiredDay, int Occurrence, int Month, int Year)
        {
            DateTime dtSat = new DateTime(Year, Month, 1);
            int j = 0;
            if (Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) >= 0)
            {
                j = Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) + 1;
            }
            else
            {
                j = (7 - Convert.ToInt32(dtSat.DayOfWeek)) + (Convert.ToInt32(DesiredDay) + 1);
            }
            return j + (Occurrence - 1) * 7;
        }
        protected void btnNewReq_Click(object sender, EventArgs e)
        {

            if (ViewState["fromdate"] != null)
            {
                txtFrmDate.Text = ViewState["fromdate"].ToString().Remove(10);
            }

            if (ViewState["toDate"] != null)
            {
                txtToDate.Text = ViewState["toDate"].ToString().Remove(10);
            }
            else
            {
                txtToDate.Text = ViewState["fromdate"].ToString().Remove(10);
            }

            //mpeNewReq.Show();

            ViewState["fromdate"] = null;
            ViewState["toDate"] = null;


        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {


            e.Cell.Attributes.Add("ONCLICK", e.SelectUrl);
            if (e.Day.IsOtherMonth)
            {
                e.Cell.Text = "";


            }

            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.Controls.Clear();
                e.Cell.Text = "";
                e.Cell.CssClass = "calWeeklyOff";
            }

            if (!e.Day.IsOtherMonth)
            {
                Table table = new Table();
                table.CssClass = "Width100";

                TableRow row = new TableRow();
                TableRow rowin = new TableRow();
                TableRow rowOUt = new TableRow();

                TableCell cellForInOUt = new TableCell();
                TableCell cell1 = new TableCell();
                TableCell celltemp = new TableCell();

                TableCell cellout = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();

                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell = new TableCell();


                cell.CssClass = "Width100";
                Label Attandance = new Label();
                Label inTime = new Label();
                Label outTime = new Label();

                Attandance.ID = "Attend" + e.Day.Date.ToShortDateString();
                //inTime.ID = "inTime" + e.Day.Date.ToShortDateString();
                //outTime.ID = "outTime" + e.Day.Date.ToShortDateString();

                if (MonthlyAttendance.Rows.Count != 0)
                {
                    DataRow[] foundDate = MonthlyAttendance.Select("tday_date = '" + e.Day.Date + "'");
                    if (foundDate.Length != 0)
                    {
                        Attandance.Text = ReturnStatus(foundDate[0]["TDAY_SFTREPO"].ToString(), e.Day.Date);
                        e.Cell.CssClass = "attendLabel";
                        //ReturnCSS(foundDate[0]["TDAY_SFTREPO"].ToString());
                    }
                    //adil
                    else
                    {
                        //Attandance.Text = "NA";
                        e.Cell.CssClass = "calGeneral";
                    }
                }
                else
                {
                    //Attandance.Text = "NA";
                    e.Cell.CssClass = "calGeneral";
                }

                cell.Controls.Add(Attandance);


                rowin.Cells.Add(cell1);
                rowin.Cells.Add(celltemp);
                rowin.Cells.Add(cellForInOUt);

                rowOUt.Cells.Add(cell2);
                rowOUt.Cells.Add(cell3);
                rowOUt.Cells.Add(cellout);

                row.Cells.Add(cell);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);


                table.Rows.Add(row);
                table.Rows.Add(new TableRow());
                table.Rows.Add(rowin);
                table.Rows.Add(rowOUt);


                e.Cell.Controls.Add(table);

                if ((int)e.Day.Date.DayOfWeek == 0)
                {
                    e.Cell.CssClass = "calWeeklyOff";

                }
                else if ((int)e.Day.Date.DayOfWeek == 6)
                {
                    if (e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 2, e.Day.Date.Month, e.Day.Date.Year)
                        || e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 4, e.Day.Date.Month, e.Day.Date.Year)
                        || e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 5, e.Day.Date.Month, e.Day.Date.Year))
                    {
                        e.Cell.CssClass = "calWeeklyOff";
                    }
                }
                else
                {

                }
                e.Cell.CssClass = "attendLabel";
            }
        }
        private string ReturnCSS(string StatusCode)
        {
            switch (StatusCode)
            {

                default:
                    {
                        return "calWeeklyOff";
                    }
            }
        }
        protected void Calendar1_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //GetMonthlyAttendance(Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Year);
            }
        }
        private string ReturnStatus(string StatusCode, DateTime tdayDate)
        {
            switch (StatusCode)
            {

                default:
                    {
                        return "<p style = \"padding-left:50%\">" + StatusCode + "</p>";
                    }
            }
        }
        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DateTime start = e.NewDate;
            getDetails();
            //GetMonthlyAttendance(e.NewDate.Month, e.NewDate.Year);
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            string fromDate = "", toDate = "";
            if (ViewState["fromdate"] == null)
            {
                fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");


                ViewState["fromdate"] = fromDate;

                ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
            }
            else
            {
                toDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                ViewState["toDate"] = toDate;
                if (Convert.ToInt32(ViewState["toDate1"]) != Calendar1.SelectedDate.DayOfYear)
                {
                    ViewState["toDate1"] = Calendar1.SelectedDate.DayOfYear;

                }
                else
                {
                    fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                    ViewState["fromdate"] = fromDate;
                    ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
                    ViewState["toDate"] = null;
                    ViewState["toDate1"] = null;

                }


            }

            if (Convert.ToString(ViewState["fromdate1"]) != null)
            {
                if (Convert.ToString(ViewState["toDate1"]) != null)
                {


                    DateTime dt = DateTime.ParseExact(Convert.ToString(ViewState["fromdate"]), "dd/MM/yyyy", null);

                    int Fdate = Convert.ToInt32(ViewState["fromdate1"]);
                    int Tdate = Convert.ToInt32(ViewState["toDate1"]);
                    if (Tdate > Fdate)
                    {

                        int datediff = (Tdate - Fdate);
                        for (int i = 0; i < datediff; i++)
                        {
                            Calendar1.SelectedDates.Add(dt.AddDays(i));
                        }
                    }
                    else
                    {
                        fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                        ViewState["fromdate"] = fromDate;
                        ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
                        ViewState["toDate"] = null;
                        ViewState["toDate1"] = null;
                    }

                }

            }

            getDetails();
            //GetMonthlyAttendance(Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Year);


        }
        protected void RbdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["uid"].ToString().ToLower() == "admin")
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
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string[] empArray = EmployeeHdn.Value.Split(',');
            if (empArray.Length == 1 && EmployeeHdn.Value.Trim() != "")
            {
                emloyee = empArray[0].Substring(2, empArray[0].Length - 4);
                ViewState["employee"] = emloyee;
                getDetails();
                bindData();
            }
            else
            {
                bindData();
            }


        }
        public void getDetails()
        {

            if (ViewState["employee"] != null)
            {
                string query = " SELECT TDAY_SFTREPO,Tday_date FROM TDAY with(nolock) WHERE DATEPART(MM,TDAY_DATE)='" + DateTime.Now.Month.ToString() + "' " +
                              " AND DATEPART(YY,TDAY_DATE)='" + DateTime.Now.Year.ToString() + "' and  TDAY_EMPCDE ='" + ViewState["employee"].ToString() + "'";


                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                //DataTable dt = new DataTable();
                da.Fill(MonthlyAttendance);
                conn.Close();
            }
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
            try
            {

                //string strSql = "select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where eod_isdeleted= 0";

                string strSql = "select EOD_EMPID,epd_first_name + ' ' + epd_last_name as Empname, ";
                strSql += " convert(varchar,EOD_JOINING_DATE,103) as EOD_JOINING_DATE from ";
                strSql += " ENT_EMPLOYEE_OFFICIAL_DTLS,ent_employee_personal_dtls ";
                strSql += " where ent_employee_personal_dtls.epd_empid=ENT_EMPLOYEE_OFFICIAL_DTLS.eod_empid and ENT_EMPLOYEE_OFFICIAL_DTLS.eod_active=1 ";
                strSql += " and EOD_EMPID in (select ETC_EMP_ID from TNA_EMPLOYEE_TA_CONFIG) ";
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
        protected void btnReset_Click(object sender, EventArgs e)
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
            txtFromDateSC.Text = "";
            txtToDateSC.Text = "";
            rblCorrectionType.SelectedIndex = 0;
            ddlShift.SelectedIndex = 0;
            ddlShiftPattern.SelectedIndex = 0;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();
        }

    }
}