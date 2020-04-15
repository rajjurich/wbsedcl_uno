using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace UNO
{
    public partial class ODADD : System.Web.UI.Page
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
                RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
                RbdCategory.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('CAT', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");


                txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtShift.ClientID + "','" + LstCategory.ClientID + "','" + LstCategoryDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");


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

                FillReason();
                FindManagerID();

            }
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
            //cmd.Parameters.AddWithValue("@mgeid", mgrId);
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

            //DataTable dtCategory = thisDataSet.Tables[5].DefaultView.ToTable(true, "CategoryID", "Category_NAME");

            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "Category_NAME";

            //LstCategory.DataSource = dtCategory;

            //LstCategory.DataBind();


            //DataTable dtDepartment = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");

            //LstDepartment.DataValueField = "DepartmentID";
            //LstDepartment.DataTextField = "Department_NAME";

            //LstDepartment.DataSource = dtDepartment;

            //LstDepartment.DataBind();


            //DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");

            //LstDivision.DataValueField = "DivisionID";
            //LstDivision.DataTextField = "Division_NAME";

            //LstDivision.DataSource = dtDivision;

            //LstDivision.DataBind();




            //DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");

            //LstLocation.DataValueField = "LocationID";
            //LstLocation.DataTextField = "Location_NAME";

            //LstLocation.DataSource = dtLocation;

            //LstLocation.DataBind();

        }

        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                            "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and Reason_Type='OD'";
            SqlDataAdapter daReason = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);
            ddlReason.DataValueField = "Reason_ID";
            ddlReason.DataTextField = "Reason_Description";
            ddlReason.DataSource = dtReason;
            ddlReason.DataBind();
            ddlReason.Items.Insert(0, "Select One");

        }


        private void FindManagerID()
        {
            string strSql = "select distinct hier_mgr_id as Mgrid,replace(convert(char(20),ltrim(EPD_FIRST_NAME+ ' ' + EPD_LAST_NAME))+ hier_mgr_id,' ',' ' ) as MgrName from ENT_HierarchyDef,ENT_EMPLOYEE_PERSONAL_DTLS " +
                         " where  ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID=ENT_HierarchyDef.Hier_Mgr_ID ";
            SqlDataAdapter daSanctn = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtSanctn = new DataTable();
            daSanctn.Fill(dtSanctn);
            ddSanctionedID.DataValueField = "Mgrid";
            ddSanctionedID.DataTextField = "MgrName";
            ddSanctionedID.DataSource = dtSanctn;
            ddSanctionedID.DataBind();
            ddSanctionedID.Items.Insert(0, "Select One");

        }

        private void FillEmployeeEntity()
        {

            string strSql = "";

            strSql = " SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
                     " FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  " +
                     " on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1' ";
            //strSql = " select Left(EPD_FIRST_NAME + space(28),30) + epd_empid as Name ,epd_empid as ID from ENT_EMPLOYEE_PERSONAL_DTLS where epd_isdeleted='0' ";


            //strSql = "SELECT oce_id as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+ oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0'";

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

            //strSql = "SELECT cat_category_id as ID,replace(convert(char(20),ltrim(cat_category_description  ))+cat_category_id,' ',' ' ) as NAME FROM ENT_CATEGORY where   cat_isdeleted='0'";
            strSql = " SELECT OCE_ID as ID,replace(convert(char(20),ltrim(OCE_DESCRIPTION  ))+OCE_ID,' ',' ' ) as NAME FROM ENT_ORG_COMMON_ENTITIES WHERE CEM_ENTITY_ID='CAT' AND  OCE_ISDELETED='0' ";
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
            if (Page.IsValid)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(m_connections);
                    conn.Open();


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Proc_Insert_HRENTRY]";
                    cmd.CommandTimeout = 0;

                    //string txts = "'00000015','00000020','00001990'";

                    //string txts = "";
                    //Int64 i=0;
                    //for (i = 0; i <= 10000 ; i++)
                    //{
                    //    txts = txts+(txts.Length > 0 ? ",":"")  +"'"+ Convert.ToString(i).PadLeft(5,'0')+"'"; 
                    //}


                    // DateTime str_Final_shft_dt = DateTime.ParseExact(txtshiftStartDate.Text, "MM/dd/yyyy", null);

                    string str_from_date = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                    string str_To_Date = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                    string txtEntity = "OD";
                    cmd.Parameters.AddWithValue("@EntityFlag", txtEntity);
                    cmd.Parameters.AddWithValue("@ReasonCode", ddlReason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                    cmd.Parameters.AddWithValue("@LeaveCode", "");
                    cmd.Parameters.AddWithValue("@leaveFromdate", str_from_date);
                    cmd.Parameters.AddWithValue("@leaveTodate", str_To_Date);
                    cmd.Parameters.AddWithValue("@from_time", "");
                    cmd.Parameters.AddWithValue("@to_time", "");
                    cmd.Parameters.AddWithValue("@MA_Mode", "");
                    cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                    cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                    cmd.Parameters.AddWithValue("@Loccde", LocationHdn.Value);
                    cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                    cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                    cmd.Parameters.AddWithValue("@catcde", ShiftHdn.Value);                

                    int j = cmd.ExecuteNonQuery();
                    lblMessages.Text = "Record(s) Saved Successfully";
                    lblMessages.Visible = true;
                    Reset();
                }
                catch (Exception ex)
                {
                    lblMessages.Text = ex.Message;
                    lblMessages.Visible = true;
                }
            }
        }

        protected void RbdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillEntityList();
        }

        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ODAppView.aspx", true);
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
            txtfromDate.Text = "";
            txtToDate.Text = "";
            ddlReason.SelectedIndex = 0;
            txt_Remarks.Text = "";
        }
    }
}