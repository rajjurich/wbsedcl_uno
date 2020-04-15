using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Sockets;
using CMS.UNO.Core.Handler;
namespace UNO
{
    public partial class HierarchyManagerLinkAdd : System.Web.UI.Page
    {
        public string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uid"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                if (!Page.IsPostBack)
                {
                    RbdEmp.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('EMP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdCompany.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('COM','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdLocation.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('LOC', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdDivision.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DIV', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");
                    RbdDepartment.Attributes.Add("onclick", "javascript:return EntitySpecificDetails('DEP', '" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstLocationDummy.ClientID + "','" + LstDivisionDummy.ClientID + "','" + LstDepartmentDummy.ClientID + "','','" + btnSubmitAdd.ClientID + "','" + lstEmployDummy.ClientID + "');");


                    txtSearchEmp.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtSearchEmp.ClientID + "','" + LstEmployee.ClientID + "','" + lstEmployDummy.ClientID + "','"+LstEntitySelected.ClientID+"');");
                    txtCompany.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtCompany.ClientID + "','" + LstCompany.ClientID + "','" + LstCompanyDummy.ClientID + "','" + LstEntitySelected.ClientID + "' );");
                    txtLocation.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtLocation.ClientID + "','" + LstLocation.ClientID + "','" + LstLocationDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    txtDivision.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDivision.ClientID + "','" + LstDivision.ClientID + "','" + LstDivisionDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    txtDepartment.Attributes.Add("onkeyup", "javascript:return FilterListBoxS('" + txtDepartment.ClientID + "','" + LstDepartment.ClientID + "','" + LstDepartmentDummy.ClientID + "' ,'" + LstEntitySelected.ClientID + "');");
                    

                    btnOK.Attributes.Add("onclick", "javascript:return okclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','" + txtShift.ClientID + "','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSubmitAdd.ClientID + "');");
                    //Close
                    Button4.Attributes.Add("onclick", "javascript:return closeclick('" + EmployeeHdn.ClientID + "','" + ComapnyHdn.ClientID + "', '" + LocationHdn.ClientID + "','" + DivisionHdn.ClientID + "','" + DepartmentHdn.ClientID + "', '" + ShiftHdn.ClientID + "','" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "','" + txtSearchEmp.ClientID + "','" + txtCompany.ClientID + "','" + txtDivision.ClientID + "','" + txtLocation.ClientID + "','" + txtDepartment.ClientID + "','','" + lblAvailableData.ClientID + "','" + lblSeletcedData.ClientID + "','" + btnSubmitAdd.ClientID + "');");

                    Button2.Attributes.Add("onclick", "javascript:return removeEntitySelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    cmdEntityAllRight.Attributes.Add("onclick", "javascript:return AllSelectedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    Button1.Attributes.Add("onclick", "javascript:return FillEntitySeletedListBox('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                    Button3.Attributes.Add("onclick", "javascript:return ReturnFillEntityAvailable('" + LstCompany.ClientID + "','" + LstLocation.ClientID + "','" + LstDivision.ClientID + "','" + LstDepartment.ClientID + "','" + LstCategory.ClientID + "','" + RbdEmp.ClientID + "','" + RbdCompany.ClientID + "','" + RbdLocation.ClientID + "','" + RbdDivision.ClientID + "','" + RbdDepartment.ClientID + "','" + RbdCategory.ClientID + "','" + btnOK.ClientID + "','" + Btntd.ClientID + "','" + Button4.ClientID + "','" + LstEntitySelected.ClientID + "','" + LstEmployee.ClientID + "');");
                   // FillManagerEntity();
                    //FillEmployeeEntity();
                    //FillCompanyEntity();
                    //FillLocationEntity();
                    //FillDivisionEntity();
                    //FillDepartmentEntity();
                    FillManagerEntity();
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
                    //FillEntitydrp();
                    //FillManagerdrp();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }
        private void FillEmployeeEntity()
        {

            string strSql = "";

            string levelId = Session["levelId"].ToString();

            SqlConnection con = new SqlConnection(m_connections);
            con.Open();
            SqlCommand cmd;
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
            {
                strSql = "SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
          " FROM ENT_employee_personal_dtls  emp with(nolock) inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod with(nolock) on emp.EPD_EMPID=eod.EOD_EMPID where " +
          "  epd_isdeleted='0' and eod_active = '1'  AND ISNULL(EPD_ISDELETED,0)=0 ";
                cmd = new SqlCommand(strSql, con);
            }
            else
            {
                cmd = new SqlCommand("spFillEntities2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@levelid", levelId);
            }
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
        public void bindData()
        {
            try
            {

                clsEmployeeHierarchy objData = new clsEmployeeHierarchy();
                objData.EmpID = EmployeeHdn.Value;
                objData.Company = ComapnyHdn.Value;
                objData.Location = LocationHdn.Value;
                objData.Division = DivisionHdn.Value;
                objData.Department = DepartmentHdn.Value;

                DataTable dt = clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("EMPFilter", objData);              
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

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }
        private void FillManagerEntity()
        {
            try
            {               
                string levelId = Session["levelId"].ToString();
                SqlCommand cmd ;
                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                    DataSet ds = clsCommonHandler.GetEmployeesDetails("AEMP");
                    lstManager.DataValueField = "ID";
                    lstManager.DataTextField = "NAME";
                    lstManager.DataSource = ds.Tables[0];
                    lstManager.DataBind();
                }
                else
                {
                    SqlConnection con = new SqlConnection(m_connections);
                    if (ConnectionState.Closed == con.State)
                    {
                        con.Open();
                    }
                    cmd = new SqlCommand("spFillEntities2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet thisDataSet = new DataSet();
                    adpt.Fill(thisDataSet);

                    lstManager.DataValueField = "EOD_EMPID";
                    lstManager.DataTextField = "EmployeeName";
                    lstManager.DataSource = thisDataSet.Tables[0];
                    lstManager.DataBind();

                    if (ConnectionState.Open == con.State)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private void FillEmployeeEntity(string empId)
        {
            try
            {
                clsEmployeeHierarchy objHierarchy=new clsEmployeeHierarchy();
                objHierarchy.EmpID=empId;
                DataTable dt=clsEmployeeHierarchyHandler.GetEmployeeManagerDetails("EMP", objHierarchy);
              
                LstEmployee.DataValueField = "ID";
                LstEmployee.DataTextField = "NAME";
                LstEmployee.DataSource = dt;
                LstEmployee.DataBind();

                lstEmployDummy.DataValueField = "ID";
                lstEmployDummy.DataTextField = "NAME";
                lstEmployDummy.DataSource = dt;
                lstEmployDummy.DataBind();

               
            }
            catch (Exception ex)
            {                
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EmployeeManagerHierarchyAdd");
            }
        }           
        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            //this.messageDiv.InnerHtml = "";
            //this.ddlMngrcode.SelectedIndex = 0;
            //this.cmbEntity.SelectedIndex = 0;
            ////this.ddlMngrcode.SelectedItem.Text = "Select One";
            ////this.cmbEntity.SelectedItem.Text = "Select One";
            //this.lstAEntity.Items.Clear();
            //this.lstSEntity.Items.Clear();
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
        protected void btnOK_Click(object sender, EventArgs e)
        {
            bindData();
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            
            try
            {           
                clsEmployeeHierarchy objHier = new clsEmployeeHierarchy();
                objHier.EmpID = EmployeeHdn.Value;
                objHier.MngrID = lstManager.SelectedValue;
                objHier.Company = ComapnyHdn.Value;
                objHier.Location = LocationHdn.Value;
                objHier.Division = DivisionHdn.Value;
                objHier.Department = DepartmentHdn.Value;
                objHier.CreatedBy = Session["uid"].ToString();
                clsEmployeeHierarchyHandler.InsertEmployeeMgr(objHier,ref  strErrMsg, ref strSuccMsg,"HierarchyManagerView.aspx");
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strErrMsg;
                    lblMessages.Visible = true;
                    return;
                }
                else
                {
                    reset();
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                }
            }
            catch (Exception ex)
            {                
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            
            }
        }
        protected void lstManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = lstManager.SelectedItem.Text;
            if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin" || Session["uid"].ToString() == "Admin")
            {
                //FillEmployeeEntity();

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
        protected void btnCancelAdd_Click(object sender, EventArgs e) //Changes by Shrinith 23/Sept/2014
        {

            Response.Redirect("HierarchyManagerView.aspx");
        }
        protected void reset()
        {
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            lstManager.SelectedIndex = -1;
            RbdEmp.Enabled = true;
            RbdCompany.Enabled = true;
            RbdLocation.Enabled = true;
            RbdDivision.Enabled = true;
            RbdDepartment.Enabled = true;
            RbdEmp.SelectedIndex = 0;
            RbdCompany.SelectedIndex = 0;
            RbdLocation.SelectedIndex = 0;
            RbdDivision.SelectedIndex = 0;
            RbdDepartment.SelectedIndex = 0;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();
        }

        protected void ResetALL_Click(object sender, EventArgs e)
        {
            EmployeeHdn.Value = "";
            ComapnyHdn.Value = "";
            LocationHdn.Value = "";
            DivisionHdn.Value = "";
            DepartmentHdn.Value = "";
            lstManager.SelectedIndex = -1;
            RbdEmp.Enabled = true;
            RbdCompany.Enabled = true;
            RbdLocation.Enabled = true;
            RbdDivision.Enabled = true;
            RbdDepartment.Enabled = true;
            RbdEmp.SelectedIndex = 0;
            RbdCompany.SelectedIndex = 0;
            RbdLocation.SelectedIndex = 0;
            RbdDivision.SelectedIndex = 0;
            RbdDepartment.SelectedIndex = 0;
            gvEmpMgrAdd.DataSource = null;
            gvEmpMgrAdd.DataBind();
        }
    }
}