using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;

namespace UNO
{
    public partial class OutPassADD : System.Web.UI.Page
    {
        public static string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        static string mgrId, levelId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            mgrId = Session["uid"].ToString();
            levelId = Session["levelId"].ToString();

            if (!Page.IsPostBack)
            {
                RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCategory.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('CAT', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");


                txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtShift.ClientID + "','" + LstCategory.ClientID + "','" + LstCategoryDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");

                btnReset.Attributes.Add("onclick", "javascript:return Reset('" + lblmsg.ClientID + "');");

                btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");
                //Close
                Button4.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");

                Button2.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                Button1.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                Button3.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");

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

                calFrom.EndDate = DateTime.Now.Date;
                txtfromDate_CalendarExtender.EndDate = DateTime.Now.Date;
                FillReason();
               

            }
            txtfromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
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

            LstEmployee.DataValueField = "EOD_EMPID";
            LstEmployee.DataTextField = "EmployeeName";
            LstEmployee.DataSource = thisDataSet.Tables[0];
            LstEmployee.DataBind();

            lstEmployDummy.DataValueField = "EOD_EMPID";
            lstEmployDummy.DataTextField = "EmployeeName";
            lstEmployDummy.DataSource = thisDataSet.Tables[0];
            lstEmployDummy.DataBind();


            DataTable dtCompany = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");

            LstCompany.DataValueField = "CompanyID";
            LstCompany.DataTextField = "COMPANY_NAME";

            LstCompany.DataSource = dtCompany;

            LstCompany.DataBind();

            LstCompanyDummy.DataValueField = "CompanyID";
            LstCompanyDummy.DataTextField = "COMPANY_NAME";
            LstCompanyDummy.DataSource = dtCompany;
            LstCompanyDummy.DataBind();

            DataTable dtCategory = thisDataSet.Tables[5].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

            LstCategory.DataValueField = "CategoryID";
            LstCategory.DataTextField = "Category_NAME";
            LstCategory.DataSource = dtCategory;
            LstCategory.DataBind();

            //LstCategoryDummy.DataValueField = "CategoryID";
            //LstCategoryDummy.DataTextField = "Category_NAME";
            //LstCategoryDummy.DataSource = dtCategory;
            //LstCategoryDummy.DataBind();

            DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            LstDepartment.DataValueField = "DepartmentID";
            LstDepartment.DataTextField = "Department_NAME";
            LstDepartment.DataSource = dtDepartment;
            LstDepartment.DataBind();

            LstDepartmentDummy.DataValueField = "DepartmentID";
            LstDepartmentDummy.DataTextField = "Department_NAME";
            LstDepartmentDummy.DataSource = dtDepartment;
            LstDepartmentDummy.DataBind();

            DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            LstDivision.DataValueField = "DivisionID";
            LstDivision.DataTextField = "Division_NAME";
            LstDivision.DataSource = dtDivision;
            LstDivision.DataBind();

            LstDivisionDummy.DataValueField = "DivisionID";
            LstDivisionDummy.DataTextField = "Division_NAME";
            LstDivisionDummy.DataSource = dtDivision;
            LstDivisionDummy.DataBind();


            DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            LstLocation.DataValueField = "LocationID";
            LstLocation.DataTextField = "Location_NAME";
            LstLocation.DataSource = dtLocation;
            LstLocation.DataBind();

            LstLocationDummy.DataValueField = "LocationID";
            LstLocationDummy.DataTextField = "Location_NAME";
            LstLocationDummy.DataSource = dtLocation;
            LstLocationDummy.DataBind();


        }

        //private void fillEntities()
        //{
        //    mgrId = Session["uid"].ToString();
        //    levelId = Session["levelId"].ToString();
        //    SqlConnection con = new SqlConnection(m_connections);

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
        //    LstEmployee.DataValueField = "EOD_EMPID";
        //    LstEmployee.DataTextField = "EmployeeName";
        //    LstEmployee.DataSource = thisDataSet.Tables[0];
        //    LstEmployee.DataBind();

        //    lstEmployDummy.DataValueField = "EOD_EMPID";
        //    lstEmployDummy.DataTextField = "EmployeeName";
        //    lstEmployDummy.DataSource = thisDataSet.Tables[0];
        //    lstEmployDummy.DataBind();

        

        //}
        private void FillReason()
        {

            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                            "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE'   and  Reason_Type = 'GP'";
            SqlDataAdapter daReason = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);
            ddlReason.DataValueField = "Reason_ID";
            ddlReason.DataTextField = "Reason_Description";
            ddlReason.DataSource = dtReason;
            ddlReason.DataBind();
            ddlReason.Items.Insert(0, "Select One");

        }      
        private void FillEmployeeEntity()
        {

            string strSql = "";

            strSql = " SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
                     "  FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                     "  on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1'";


            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            cmd.CommandTimeout = 0;
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

            strSql = "SELECT COMPANY_ID as ID,replace(convert(char(23),ltrim(COMPANY_NAME  ))+ COMPANY_ID,' ',' ' ) as NAME FROM ENT_COMPANY where COMPANY_ISDELETED='0'";


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

            strSql = "SELECT oce_id AS ID,replace(convert(char(24),ltrim(OCE_DESCRIPTION  )) + oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DEP' and  oce_isdeleted='0'";

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

            strSql = "SELECT oce_id AS ID,replace(convert(char(24),ltrim(OCE_DESCRIPTION  )) + oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='CAT' and  oce_isdeleted='0'";
            // strSql = "SELECT cat_category_id as ID,replace(convert(char(20),ltrim(cat_category_description  ))+cat_category_id,' ',' ' ) as NAME FROM ENT_CATEGORY where   cat_isdeleted='0'";

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
        protected void CmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm_time.Text == "" && To_Time.Text == "")
                {
                    lblmsg.Text = "Please Enter In Time or Out Time";
                    return;
                }
                lblmsg.Text = "";
                SqlConnection conn = new SqlConnection(m_connections);
                conn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_Insert_HRENTRY_Outpass";
                cmd.CommandTimeout = 0;

                //string str_from_date = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                //string str_To_Date = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                string str_from_date = CMSDateTime.CMSDateTime.ConvertToDateTime(txtfromDate.Text, "dd/MM/yyyy").ToString("MM/dd/yyyy");
                string str_To_Date = CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy").ToString("MM/dd/yyyy");

                string fromTime = "01/01/1900 " + frm_time.Text;
                string ToTime = "01/01/1900 " + To_Time.Text;
                string txtEntity = "OP";
                cmd.Parameters.AddWithValue("@EntityFlag", txtEntity);
                cmd.Parameters.AddWithValue("@ReasonCode", ddlReason.SelectedValue);
                cmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                cmd.Parameters.AddWithValue("@LeaveCode", "");
                cmd.Parameters.AddWithValue("@leaveFromdate", str_from_date);
                cmd.Parameters.AddWithValue("@leaveTodate", str_To_Date);
                cmd.Parameters.AddWithValue("@from_time", fromTime);
                cmd.Parameters.AddWithValue("@to_time", ToTime);
                cmd.Parameters.AddWithValue("@OP_Mode", "N");
                cmd.Parameters.AddWithValue("@strCommand", "Insert");
                cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@Loccde", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@catcde", ShiftHdn.Value);
                cmd.Parameters.AddWithValue("@output", '0').Direction = ParameterDirection.Output;
                cmd.Parameters["@output"].Size = 1000;

                cmd.ExecuteNonQuery();
                lblmsg.Text = cmd.Parameters["@output"].Value.ToString();
                Reset();
                
                
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ManualAdd");
            }
        }
        protected void RbdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillEntityList();
        }
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("OutpassView.aspx");
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
            ddlReason.SelectedIndex = 0;
            txtfromDate.Text = "";
            txtToDate.Text = "";
            frm_time.Text = "";
            To_Time.Text = "";
            txt_Remarks.Text = "";
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

    }

}