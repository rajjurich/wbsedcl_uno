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
    public partial class EmployeePinConfigureADD : System.Web.UI.Page
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


                RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','" + LstCategoryDummy.ClientID + "','" + CmdOk.ClientID + "','" + lstEmployDummy.ClientID + "');");
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
                txtShift.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtShift.ClientID + "','" + LstCategory.ClientID + "','" + LstCategoryDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");


                btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");
                //Close
                btnClose.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + CmdOk.ClientID + "');");

                Button2.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                Button1.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                Button3.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + btnClose.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin" || Session["uid"].ToString() == "Admin")
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
                //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");

            }
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


            string strSql = "";

            strSql = "SELECT  epd_empid as EOD_EMPID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+' | '+epd_empid,' ',' ' ) as EmployeeName " +                
                     "FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +                
                     "on emp.EPD_EMPID=eod.EOD_EMPID "+
                     "where isnull(emp.EPD_ISDELETED,0)='0' and isnull(eod.EOD_ACTIVE,0)='1' "+
                     "and eod.EOD_EMPID  in (select employee from operational_access where LevelCode=(select LevelID from ent_user where active=1 and UserID='" + HttpContext.Current.Session["uid"].ToString() + "')) ";
            SqlCommand cmdChekCOnfig = new SqlCommand(strSql, con);
            SqlDataAdapter adptChekCOnfig = new SqlDataAdapter(cmdChekCOnfig);
            DataTable dtChekCOnfig = new DataTable();
            adptChekCOnfig.Fill(dtChekCOnfig);

            DataTable dtEmployee = thisDataSet.Tables[0].DefaultView.ToTable(true, "EOD_EMPID", "EmployeeName");
            DataTable dtemp = new DataTable();
            if (dtChekCOnfig.Rows.Count > 0)
            {
                var query = (from configEMp in dtChekCOnfig.AsEnumerable()
                             join empHie in dtEmployee.AsEnumerable()
                             on configEMp.Field<string>("EOD_EMPID") equals empHie.Field<string>("EOD_EMPID")
                             select new
                             {
                                 EOD_EMPID = configEMp.Field<string>("EOD_EMPID"),
                                 EmployeeName = configEMp.Field<string>("EmployeeName")

                             });

                dtemp.Columns.Add("EOD_EMPID");
                dtemp.Columns.Add("EmployeeName");

                foreach (var item in query)
                {
                    dtemp.Rows.Add(item.EOD_EMPID, item.EmployeeName);
                }
            }
            else
            {
                dtEmployee.Rows.Clear();
            }



            LstEmployee.DataValueField = "EOD_EMPID";
            LstEmployee.DataTextField = "EmployeeName";
            LstEmployee.DataSource = dtemp;
            LstEmployee.DataBind();

            lstEmployDummy.DataValueField = "EOD_EMPID";
            lstEmployDummy.DataTextField = "EmployeeName";
            lstEmployDummy.DataSource = dtemp;
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

            LstCategoryDummy.DataValueField = "CategoryID";
            LstCategoryDummy.DataTextField = "Category_NAME";
            LstCategoryDummy.DataSource = dtCategory;
            LstCategoryDummy.DataBind();

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
        private void FillEmployeeEntity()
        {

            string strSql = "";

            strSql = "SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+' | '+epd_empid,' ',' ' ) as NAME " +
                     "FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
                     "on emp.EPD_EMPID=eod.EOD_EMPID " +
                     "where isnull(emp.EPD_ISDELETED,0)='0' and isnull(eod.EOD_ACTIVE,0)='1' AND emp.EPD_EMPID NOT IN (SELECT CC_EMP_ID FROM ACS_CARD_CONFIG WHERE ACE_isdeleted = '0') and ISNULL(LTRIM(RTRIM(eod.eod_location_id)),'')<>'' ";
                    

            //strSql = "SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+' | '+epd_empid,' ',' ' ) as NAME " +
            //         "FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
            //         "on emp.EPD_EMPID=eod.EOD_EMPID " +
            //         "where isnull(emp.EPD_ISDELETED,0)='0' and isnull(eod.EOD_ACTIVE,0)='1' " +
            //         "and eod.EOD_EMPID  in (select employee from operational_access where LevelCode=(select LevelID from ent_user where active=1 and UserID='" + HttpContext.Current.Session["uid"].ToString() + "')) ";



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

            strSql = "select OCE_ID AS ID,OCE_DESCRIPTION  as NAME from ENT_ORG_COMMON_ENTITIES where OCE_ISDELETED='0' AND CEM_ENTITY_ID='CAT'";

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
                SqlConnection conn = new SqlConnection(m_connections);
                conn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_Insert_ACS_PinConfig_New]";
                cmd.CommandTimeout = 0;

                //string str_Final_shft_dt = DateTime.ParseExact(Act_Date.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                string str_Final_shft_dt = CMSDateTime.CMSDateTime.ConvertToDateTime(Act_Date.Text, "dd/MM/yyyy").ToString("MM/dd/yyyy");
                //WriteLog("Final ACtivation Date : -" + str_Final_shft_dt);

                string chkflag, chkflag2;

                //if (Chk_SetAPB.Checked)
                //    chkflag = "1";
                //else
                //    chkflag = "0";


                if (Chk_Sts.Checked)
                    chkflag2 = "1";
                else
                    chkflag2 = "0";


                //modified by vaibhav
                cmd.Parameters.AddWithValue("@userCount", txtMinimumSwipe.Text);
                cmd.Parameters.AddWithValue("@activationDate", Act_Date.Text);
                cmd.Parameters.AddWithValue("@Status", chkflag2);
                cmd.Parameters.AddWithValue("@PIN", Pin.Text);
                cmd.Parameters.AddWithValue("@ExpiryDate", Exp_Date1.Text);

                cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                cmd.Parameters.AddWithValue("@Loccde", LocationHdn.Value);
                cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                cmd.Parameters.AddWithValue("@catcde", ShiftHdn.Value);
                cmd.Parameters.AddWithValue("@Command", "Insert");

                //WriteLog("Procedure ACtivation Date : -" + Act_Date);
                //WriteLog("Procedure ACtivation Date : -" + Exp_Date1);

                int j = cmd.ExecuteNonQuery();
                lblMsg.Text = "Record saved successfully";

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeCardConfig");
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:resetAll();", true);
            resetEntity();
            // Response.Redirect("EmployeeCardConfigView.aspx");

        }
        protected void resetEntity()
        {
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin" || Session["uid"].ToString() == "Admin")
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
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeePinConfigView.aspx");
        }

        private void WriteLog(string strMessage)
        {
            string strPath = "C:\\logs"; //@"D:\cms\UNO-LIVE\LOG\LeaveLog.txt";
            bool exists = System.IO.Directory.Exists(strPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(strPath);
            System.IO.StreamWriter SW = null;
            try
            {
                SW = new System.IO.StreamWriter(strPath + "\\LogFile.txt", true);
                SW.WriteLine(DateTime.Now.ToString("ddMMyyyy HHmmss") + " " + strMessage);

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
            finally
            {
                SW.Close();
            }
        }
    }
}