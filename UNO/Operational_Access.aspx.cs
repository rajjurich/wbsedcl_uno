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
namespace UNO
{
    public partial class Operational_Access : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string strlist = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //this.ddlMngrcode.Focus();
                if (!Page.IsPostBack)
                {
                    FillManagerEntity();
                    //FillEmployeeEntity();
                    FillCompanyEntity();
                    FillLocationEntity();
                    FillDivisionEntity();
                    FillDepartmentEntity();
                    FillCategoryEntity();
                    Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>CacheValues();</script>");
                    //FillEntitydrp();
                    //FillManagerdrp();
                }
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



                if (EmployeeHdn.Value != "")
                {
                    string strEmployeeSql = "select EOD_EMPID,epd_first_name + ' ' + epd_last_name as Empname, ";
                    strEmployeeSql += " ent_employee_official_dtls.EOD_DIVISION_ID from ";
                    strEmployeeSql += " ENT_EMPLOYEE_OFFICIAL_DTLS,ent_employee_personal_dtls ";
                    strEmployeeSql += " where ent_employee_personal_dtls.epd_empid=ENT_EMPLOYEE_OFFICIAL_DTLS.eod_empid";
                    strEmployeeSql += " and ent_employee_official_dtls.EOD_EMPID in " + EmployeeHdn.Value + "";
                    strEmployeeSql += " order by EOD_EMPID asc";

                    Executestring(strEmployeeSql);
                    showEmpLoyee();
                }
                else if (ComapnyHdn.Value != "")
                {
                    // strSql += " and ent_employee_official_dtls.EOD_COMPANY_ID in " + ComapnyHdn.Value + "";

                    string strCompanySql = "select eod.EOD_EMPID,epd_first_name + ' ' + epd.epd_last_name as Empname," +
                                             "eod.EOD_DIVISION_ID from " +
                                              "ENT_EMPLOYEE_OFFICIAL_DTLS eod  inner join ent_employee_personal_dtls epd " +
                                              "on eod.EOD_EMPID=epd.EPD_EMPID " +
                                              "inner join (select distinct CostCenterCode from  CostCenterMapping where Company in  " + ComapnyHdn.Value + ") company " +
                                              " on company.CostCenterCode=eod.EOD_LOCATION_ID";
                    Executestring(strCompanySql);
                    showCompany();
                }
                else if (LocationHdn.Value != "")
                {
                    //strSql += " and ent_employee_official_dtls.EOD_LOCATION_ID in " + LocationHdn.Value + "";
                    string strLocationSql = "select eod.EOD_EMPID,epd_first_name + ' ' + epd.epd_last_name as Empname," +
                                             "eod.EOD_DIVISION_ID from " +
                                              "ENT_EMPLOYEE_OFFICIAL_DTLS eod  inner join ent_employee_personal_dtls epd " +
                                              "on eod.EOD_EMPID=epd.EPD_EMPID " +
                                              "inner join (select distinct CostCenterCode from  CostCenterMapping where CostCenterCode in  " + LocationHdn.Value + ") Location " +
                                              " on Location.CostCenterCode=eod.EOD_LOCATION_ID";
                    Executestring(strLocationSql);
                    showLocation();
                }
                else if (DivisionHdn.Value != "")
                {
                    string strDivisionSql = " IF OBJECT_ID('tempdb..#tmptbl') IS NOT NULL " +
                            " DROP TABLE #tmptbl " +
                        " CREATE TABLE #tmptbl " +
                                       "            ( " +
                         " costcenter varchar(max) " +
                         " ) " +
                         " INSERT INTO #tmptbl " +
                         " Exec GetCostCenter '" + DivisionHdn.Value.Replace("'", "''") + "'" +
                        " select eod.EOD_EMPID,epd_first_name + ' ' + epd.epd_last_name as Empname," +
                                             "eod.EOD_DIVISION_ID from " +
                                              "ENT_EMPLOYEE_OFFICIAL_DTLS eod  inner join ent_employee_personal_dtls epd " +
                                              "on eod.EOD_EMPID=epd.EPD_EMPID " +
                                              "inner join (select distinct CostCenterCode from  CostCenterMapping where CostCenterCode in  (select * from #tmptbl)) Location " +
                                              " on Location.CostCenterCode=eod.EOD_LOCATION_ID";



                    //if (conn.State == ConnectionState.Closed)
                    //{
                    //    conn.Open();
                    //}

                    //SqlCommand cmd = new SqlCommand("USP_GET_EMPLOYEEDIVISION", conn);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@SelDivisionLocationList", DivisionHdn.Value);

                    //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    //DataSet thisDataSet = new DataSet();
                    //adpt.Fill(thisDataSet);
                    //gvEmpMgrAdd.DataSource = thisDataSet;
                    //gvEmpMgrAdd.DataBind();


                    //DropDownList ddl = (DropDownList)gvEmpMgrAdd.BottomPagerRow.FindControl("ddlPageNo");
                    //for (int i = 1; i <= gvEmpMgrAdd.PageCount; i++)
                    //{
                    //    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    //}
                    //ddl.SelectedValue = (gvEmpMgrAdd.PageIndex + 1).ToString();
                    //Label lblcount = (Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblTotal");
                    //lblcount.Text = ((DataTable)gvEmpMgrAdd.DataSource).Rows.Count.ToString() + " Records.";
                    //if (gvEmpMgrAdd.PageCount == 0)
                    //{
                    //    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    //    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    //}
                    //if (gvEmpMgrAdd.PageIndex + 1 == gvEmpMgrAdd.PageCount)
                    //{
                    //    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    //}
                    //if (gvEmpMgrAdd.PageIndex == 0)
                    //{
                    //    ((Button)gvEmpMgrAdd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    //}
                    //((Label)gvEmpMgrAdd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpMgrAdd.PageSize * gvEmpMgrAdd.PageIndex) + 1) + " to " + (gvEmpMgrAdd.PageSize * (gvEmpMgrAdd.PageIndex + 1));

                    //gvEmpMgrAdd.BottomPagerRow.Visible = true;

                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}
                    Executestring(strDivisionSql);
                    showDivision();
                }
                else if (DepartmentHdn.Value != "")
                {
                    // strSql += " and ent_employee_official_dtls.EOD_DEPARTMENT_ID in " + DepartmentHdn.Value + "";
                    showDepartment();
                }
                else
                    showCategory();

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


        public void Executestring(string strSql)
        {
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
            LstEntitySelected.Attributes.Add("style", "display:none");
        }


        private void FillManagerEntity()
        {
            try
            {
                string strSql = "";

                strSql = "select * from LevelMaster order by LevelName";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                lstManager.DataValueField = "ID";
                lstManager.DataTextField = "LevelName";

                lstManager.DataSource = thisDataSet.Tables[0];

                lstManager.DataBind();
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

        private void FillEmployeeEntity(string empId)
        {
            try
            {
                string strSql = "";

                strSql = "SELECT  epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+' | '+epd_empid,' ',' ' ) as NAME " +

                         "FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +

                          "on emp.EPD_EMPID=eod.EOD_EMPID where isnull(emp.EPD_ISDELETED,0)='0' and isnull(eod.EOD_ACTIVE,0)='1' and emp.EPD_EMPID !='" + empId + "' order by ID ";



                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstEmployee.DataValueField = "ID";
                LstEmployee.DataTextField = "NAME";

                LstEmployee.DataSource = thisDataSet.Tables[0];

                LstEmployee.DataBind();
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

        private void FillCompanyEntity()
        {
            try
            {
                string strSql = "";

                strSql = " select distinct Company  as ID,replace(convert(char(23),ltrim(Company  ))+' | '+ Company,' ',' ' ) as NAME " +
                         "from CostCenterMapping ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstCompany.DataValueField = "ID";
                LstCompany.DataTextField = "NAME";

                LstCompany.DataSource = thisDataSet.Tables[0];

                LstCompany.DataBind();
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

        private void FillLocationEntity()
        {
            try
            {

                string strSql = "";

                strSql = " select distinct CostCenterCode 'ID',isnull(replace(convert(char(25),ltrim(CostCenter))+' | '+CostCenterCode,' ',' '),0) 'NAME' " +

                          "from CostCenterMapping ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstLocation.DataValueField = "ID";
                LstLocation.DataTextField = "NAME";

                LstLocation.DataSource = thisDataSet.Tables[0];

                LstLocation.DataBind();
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

        private void FillDivisionEntity()
        {
            try
            {

                string strSql = "";

                strSql = " select distinct ProfitCenterCode 'ID',isnull(replace(convert(char(25),ltrim(ProfitCenter))+' | '+ProfitCenterCode,' ',' '),0) 'NAME' " +

                         " from  CostCenterMapping";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstDivision.DataValueField = "ID";
                LstDivision.DataTextField = "NAME";

                LstDivision.DataSource = thisDataSet.Tables[0];

                LstDivision.DataBind();
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

        private void FillDepartmentEntity()
        {
            try
            {
                string strSql = "";

                strSql = "SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DEP' and  oce_isdeleted='0' order by ID";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstDepartment.DataValueField = "ID";
                LstDepartment.DataTextField = "NAME";

                LstDepartment.DataSource = thisDataSet.Tables[0];

                LstDepartment.DataBind();
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

        private void FillCategoryEntity()
        {

            string strSql = "";
            strSql = " SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='CAT' and  oce_isdeleted='0' order by ID";
            // strSql = "SELECT cat_category_id as ID,replace(convert(char(20),ltrim(cat_category_description  ))+cat_category_id,' ',' ' ) as NAME FROM ENT_CATEGORY where   cat_isdeleted='0'";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            // conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstCategory.DataValueField = "ID";
            LstCategory.DataTextField = "NAME";

            LstCategory.DataSource = thisDataSet.Tables[0];

            LstCategory.DataBind();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        public void showEmpLoyee()
        {
            RbdLocation.Enabled = false;
            RbdDivision.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdCompany.Enabled = false;
            RbdCategory.Enabled = false;
        }

        public void showLocation()
        {
            RbdEmp.Enabled = false;
            RbdDivision.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdCompany.Enabled = false;
            RbdCategory.Enabled = false;
        }

        public void showDivision()
        {
            RbdLocation.Enabled = false;
            RbdEmp.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdCompany.Enabled = false;
            RbdCategory.Enabled = false;
        }

        public void showDepartment()
        {
            RbdLocation.Enabled = false;
            RbdDivision.Enabled = false;
            RbdEmp.Enabled = false;
            RbdCompany.Enabled = false;
            RbdCategory.Enabled = false;
        }

        public void showCompany()
        {
            RbdLocation.Enabled = false;
            RbdDivision.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdEmp.Enabled = false;
            RbdCategory.Enabled = false;
        }

        public void showCategory()
        {
            RbdLocation.Enabled = false;
            RbdDivision.Enabled = false;
            RbdDepartment.Enabled = false;
            RbdCompany.Enabled = false;
            RbdEmp.Enabled = false;
        }

        protected void CmdSave_Click(object sender, EventArgs e)
        {
            //strlist=  GetSelectedList();

            //SqlConnection conn = new SqlConnection(m_connectons);
            //conn.Open();
            //SqlCommand objcmd = new SqlCommand();
            //objcmd.Connection = conn;
            try
            {
                //SqlCommand CmdProc = new SqlCommand("PROC_Insert_Emp_Mgr", conn);
                //CmdProc.CommandType = CommandType.StoredProcedure;
                //CmdProc.Parameters.Add("@MGR_ID", SqlDbType.VarChar).Value =  ddlMngrcode.SelectedValue.ToString() ;
                //CmdProc.Parameters.Add("@entity_name", SqlDbType.VarChar).Value =  cmbEntity.SelectedValue.ToString() ;
                //CmdProc.Parameters.Add("@strList", SqlDbType.VarChar).Value = "'" + strlist + "'";

                ////CmdProc.Parameters.AddWithValue("@strList", strlist);

                //int iRowCount=  CmdProc.ExecuteNonQuery();

                //if (iRowCount > 0)
                //{

                //    this.messageDiv.InnerHtml = "Record Saved Successfully";
                //}
                //else
                //{
                //    this.messageDiv.InnerHtml = "No Employee Exist for the given entity";
                //}
                // string someScript3 = "";
                //someScript3 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript3);

                //FillManagerdrp();
            }

            catch (Exception ex)
            {
                // this.messageDiv.InnerHtml = ex.Message;
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

        protected void btnCancelAdd_Click(object sender, EventArgs e) //Changes by Shrinith 23/Sept/2014
        {
            RbdEmp.SelectedValue = "0";
            RbdLocation.SelectedValue = "0";
            RbdDivision.SelectedValue = "0";
            RbdDepartment.SelectedValue = "0";
            RbdCompany.SelectedValue = "0";
            RbdCategory.SelectedValue = "0";
            Response.Redirect("Operational_Access.aspx");
        }


        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            if (EmployeeHdn.Value != "")
            {
                SqlDataAdapter dachk = new SqlDataAdapter(" select * from ENT_HierarchyDef where Hier_Emp_ID in " + EmployeeHdn.Value + " and Hier_Mgr_ID='" + lstManager.SelectedValue + "'", conn);
                DataTable dt = new DataTable();
                dachk.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMessages.Text = "This Record Already Exists";
                    lblMessages.Visible = true;
                    return;
                }
            }
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (lstManager.SelectedValue != "")
                {
                    SqlCommand cmd = new SqlCommand("Sp_Insert_OperationalAccess", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LevelID", lstManager.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmpCode", EmployeeHdn.Value);
                    cmd.Parameters.AddWithValue("@Cmpcde", ComapnyHdn.Value);
                    cmd.Parameters.AddWithValue("@Loccde ", LocationHdn.Value);
                    cmd.Parameters.AddWithValue("@divcde", DivisionHdn.Value);
                    cmd.Parameters.AddWithValue("@depcde", DepartmentHdn.Value);
                    cmd.Parameters.AddWithValue("@category", ShiftHdn.Value);

                    cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    lblMessages.Text = "Records Saved Successfully";
                    lblMessages.Visible = true;
                }
                else
                {
                    lblMessages.Text = "Please Select Level to Save";
                    lblMessages.Visible = true;
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

        protected void lstManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = lstManager.SelectedItem.Text;
            FillEmployeeEntity(lstManager.SelectedItem.Value.ToString());
            LstEntitySelected.Items.Clear();
            try
            {
                string strSql = "";

                strSql = "select * From Operational_Access where IsDeleted=0 and levelcode in (select id from LevelMaster Where LevelName='" + TextBox1.Text + "');";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);
                DataTable dt = thisDataSet.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["employee"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showEmpLoyee()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdEmp.SelectedIndex = 1;
                            EmployeeHdn.Value = "";
                            ComapnyHdn.Value = "";
                            LocationHdn.Value = "";
                            DepartmentHdn.Value = "";
                            DivisionHdn.Value = "";
                            ShiftHdn.Value = "";
                            EmployeeHdn.Value = item["employee"].ToString();
                            FillEmployeeEntityEdit(lstManager.SelectedItem.Value.ToString());
                            bindedit();
                        }
                        else if (item["company"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showCompany()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdCompany.SelectedIndex = 1;
                            EmployeeHdn.Value = "";
                            ComapnyHdn.Value = "";
                            LocationHdn.Value = "";
                            DepartmentHdn.Value = "";
                            DivisionHdn.Value = "";
                            ShiftHdn.Value = "";
                            ComapnyHdn.Value = item["company"].ToString();
                            FillCompanyEntityEdit();
                            bindedit();
                        }
                        else if (item["location"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showLocation()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdLocation.SelectedIndex = 1;
                            EmployeeHdn.Value = "";
                            ComapnyHdn.Value = "";
                            LocationHdn.Value = "";
                            DepartmentHdn.Value = "";
                            DivisionHdn.Value = "";
                            ShiftHdn.Value = "";
                            LocationHdn.Value = item["location"].ToString();
                            FillLocationEntityEdit();
                            bindedit();
                        }
                        else if (item["division"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showDivision()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdDivision.SelectedIndex = 1;
                            DivisionHdn.Value = item["division"].ToString();
                            FillDivisionEntityEdit();
                            bindedit();
                        }
                        else if (item["department"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showDepartment()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdDepartment.SelectedIndex = 1;
                            EmployeeHdn.Value = "";
                            ComapnyHdn.Value = "";
                            LocationHdn.Value = "";
                            DepartmentHdn.Value = "";
                            DivisionHdn.Value = "";
                            ShiftHdn.Value = "";
                            DepartmentHdn.Value = item["department"].ToString();
                            FillDepartmentEntityEdit();
                            bindedit();
                        }
                        else if (item["category"].ToString() != "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "showCategory()", true);
                            LstEntitySelected.Attributes.Add("style", "display:inline");
                            RbdCategory.SelectedIndex = 1;
                            EmployeeHdn.Value = "";
                            ComapnyHdn.Value = "";
                            LocationHdn.Value = "";
                            DepartmentHdn.Value = "";
                            DivisionHdn.Value = "";
                            ShiftHdn.Value = "";
                            ShiftHdn.Value = item["category"].ToString();
                            FillCategoryEntityEdit();
                            bindedit();
                        }
                    }
                }
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


        public void bindedit()
        {
            try
            {
                string strSql = " ";

                if (EmployeeHdn.Value != "")
                {
                    strSql += "SELECT  epd_empid as ID,replace(convert(char(40),ltrim(EPD_FIRST_NAME )) + '  '+epd_empid,' ',' ')   as NAME FROM ENT_employee_personal_dtls emp " +
                             " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop on emp.EPD_EMPID=eop.EOD_EMPID WHERE emp.EPD_ISDELETED='0' and eop.EOD_ACTIVE='1'  and emp.EPD_EMPID in " + EmployeeHdn.Value + " order by ID ";
                    showEmpLoyee();
                }
                else if (ComapnyHdn.Value != "")
                {

                    //strSql += " SELECT COMPANY_ID as ID,replace(convert(char(30),ltrim(COMPANY_NAME  ))+ '  '+COMPANY_ID,' ',' ' ) as NAME FROM ENT_COMPANY where COMPANY_ISDELETED='0' and COMPANY_ID in " + ComapnyHdn.Value + " order by ID";

                    strSql += " select distinct Company  as ID,replace(convert(char(23),ltrim(Company  ))+' | '+ Company,' ',' ' ) as NAME " +
                         "from CostCenterMapping where Company in "  + ComapnyHdn.Value + "";
                    showCompany();
                }
                else if (LocationHdn.Value != "")
                {
                    //strSql += " SELECT oce_id as ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  ))+ '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0' and oce_id in " + LocationHdn.Value + " order by ID";
                    strSql += " select distinct CostCenterCode 'ID',isnull(replace(convert(char(25),ltrim(CostCenter))+' | '+CostCenterCode,' ',' '),0) 'NAME' " +

                         "from CostCenterMapping where CostCenterCode in "+ LocationHdn.Value +"";
                    
                    showLocation();
                }
                else if (DivisionHdn.Value != "")
                {
                    //strSql += " SELECT oce_id as ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DIV' and  oce_isdeleted='0' and oce_id in " + DivisionHdn.Value + " order by ID ";
                    strSql += "select distinct ProfitCenterCode 'ID',isnull(replace(convert(char(25),ltrim(ProfitCenter))+' | '+ProfitCenterCode,' ',' '),0) 'NAME' from costcentermapping where profitcentercode in " + DivisionHdn.Value + "";
                    showDivision();
                }
                else if (DepartmentHdn.Value != "")
                {
                    strSql += "SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DEP' and  oce_isdeleted='0' and oce_id in " + DepartmentHdn.Value + " order by ID";
                    showDepartment();
                }
                else if (ShiftHdn.Value != "")
                {
                    strSql += "SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='CAT' and  oce_isdeleted='0' and oce_id in " + ShiftHdn.Value + "order by ID";
                    showCategory();
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);
                DataTable dt = thisDataSet.Tables[0];
                LstEntitySelected.Attributes.Add("style", "display:inline");
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        LstEntitySelected.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                    }
                }
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

        private void FillEmployeeEntityEdit(string empId)
        {
            try
            {
                string strSql = "";

                strSql = " select ID, Name from ( SELECT  epd_empid as ID,replace(convert(char(40),ltrim(EPD_FIRST_NAME )) + '  '+epd_empid,' ',' ')   as NAME FROM ENT_employee_personal_dtls emp " +
                         " inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop on emp.EPD_EMPID=eop.EOD_EMPID WHERE emp.EPD_ISDELETED='0' and eop.EOD_ACTIVE='1'  and emp.EPD_EMPID !='" + empId + "' ) t Where t.ID not in " + EmployeeHdn.Value + " order by ID ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstEmployee.DataValueField = "ID";
                LstEmployee.DataTextField = "NAME";

                LstEmployee.DataSource = thisDataSet.Tables[0];

                LstEmployee.DataBind();
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

        private void FillCompanyEntityEdit()
        {
            try
            {
                string strSql = "";

               // strSql = " select ID, Name from ( SELECT COMPANY_ID as ID,replace(convert(char(30),ltrim(COMPANY_NAME  ))+ '  '+COMPANY_ID,' ',' ' ) as NAME FROM ENT_COMPANY where COMPANY_ISDELETED='0' ) t Where t.ID not in " + ComapnyHdn.Value + " order by ID";
                strSql = " select distinct Company  as ID,replace(convert(char(23),ltrim(Company  ))+' | '+ Company,' ',' ' ) as NAME " +
                       "from CostCenterMapping where Company not in " + ComapnyHdn.Value + "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstCompany.DataValueField = "ID";
                LstCompany.DataTextField = "NAME";

                LstCompany.DataSource = thisDataSet.Tables[0];

                LstCompany.DataBind();
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

        private void FillLocationEntityEdit()
        {
            try
            {

                string strSql = "";

                //strSql = " select ID, Name from ( SELECT oce_id as ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  ))+ '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='LOC' and  oce_isdeleted='0' ) t Where t.ID not in " + LocationHdn.Value + " order by ID";
                strSql = " select distinct CostCenterCode 'ID',isnull(replace(convert(char(25),ltrim(CostCenter))+' | '+CostCenterCode,' ',' '),0) 'NAME' " +

                         "from CostCenterMapping where CostCenterCode not in " + LocationHdn.Value + "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstLocation.DataValueField = "ID";
                LstLocation.DataTextField = "NAME";

                LstLocation.DataSource = thisDataSet.Tables[0];

                LstLocation.DataBind();
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

        private void FillDivisionEntityEdit()
        {
            try
            {

                string strSql = "";

                strSql = " select distinct ProfitCenterCode 'ID',isnull(replace(convert(char(25),ltrim(ProfitCenter))+' | '+ProfitCenterCode,' ',' '),0) 'NAME' " +

                      " from  CostCenterMapping where ProfitCenterCode not in " + DivisionHdn.Value + "";

                //strSql = " select ID, Name from ( SELECT oce_id as ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DIV' and  oce_isdeleted='0' ) t Where t.ID not in " + DivisionHdn.Value + " order by ID";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstDivision.DataValueField = "ID";
                LstDivision.DataTextField = "NAME";

                LstDivision.DataSource = thisDataSet.Tables[0];

                LstDivision.DataBind();
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

        private void FillDepartmentEntityEdit()
        {
            try
            {
                string strSql = "";

                strSql = " select ID, Name from ( SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='DEP' and  oce_isdeleted='0' ) t Where t.ID not in " + DepartmentHdn.Value + " order by ID";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet thisDataSet = new DataSet();
                adpt.Fill(thisDataSet);

                LstDepartment.DataValueField = "ID";
                LstDepartment.DataTextField = "NAME";

                LstDepartment.DataSource = thisDataSet.Tables[0];

                LstDepartment.DataBind();
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

        private void FillCategoryEntityEdit()
        {

            string strSql = "";
            strSql = " select ID, Name from ( SELECT oce_id AS ID,replace(convert(char(40),ltrim(OCE_DESCRIPTION  )) + '  '+oce_id,' ',' ' ) as NAME FROM ent_org_common_entities where cem_entity_id='CAT' and  oce_isdeleted='0' ) t Where t.ID not in " + ShiftHdn.Value + " order by ID";
            // strSql = "SELECT cat_category_id as ID,replace(convert(char(20),ltrim(cat_category_description  ))+cat_category_id,' ',' ' ) as NAME FROM ENT_CATEGORY where   cat_isdeleted='0'";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            // conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet thisDataSet = new DataSet();
            adpt.Fill(thisDataSet);

            LstCategory.DataValueField = "ID";
            LstCategory.DataTextField = "NAME";

            LstCategory.DataSource = thisDataSet.Tables[0];

            LstCategory.DataBind();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }


    }
}