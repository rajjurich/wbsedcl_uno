using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;


namespace UNO
{
    public partial class UserAccessPermissionBrowse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        String _strList = "", _strALList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddlEntityType();
                ddlEntityType.SelectedIndex = 7;
                bindDataGrid(ddlEntityType.SelectedValue);
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvEmployeeAccess.ClientID + "');");
            }
        }
        private void bindddlEntityType()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("select CODE,VALUE from ENT_PARAMS where identifier='COMMONMASTERS' and module='ENT' Order by [value]", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ddlEntityType.DataSource = dt;
                ddlEntityType.DataTextField = "VALUE";
                ddlEntityType.DataValueField = "CODE";
                ddlEntityType.DataBind();
                ddlEntityType.Items.Add(new ListItem("EMPLOYEE", "EMP"));
                //ddlEntityType.Items.Insert(0, new ListItem("Select", "0"));

                ddlEntityAdd.DataSource = dt;
                ddlEntityAdd.DataTextField = "VALUE";
                ddlEntityAdd.DataValueField = "CODE";
                ddlEntityAdd.DataBind();
                ddlEntityAdd.Items.Add(new ListItem("EMPLOYEE", "EMP"));
                ddlEntityAdd.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        private void bindDataGrid(string EntityType)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
               
                string sql = "";

                if (EntityType == "EMP")
                {
                    //sql = "select E.EMPLOYEE_CODE as ID,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME as DES,case E.ENTITY_TYPE when 'EMP' then 'EMPLOYEE' when 'DEP' then 'DEPARTMENT' " +
                    //         "when 'DES' then 'DESIGNATION' when 'DIV' then 'DIVISION' when 'GRD' then 'GRADE' when 'GRP' then 'GROUP' " +
                    //         "when 'LOC' then 'LOCATION'else ''  end as ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                    //         "from EAL_CONFIG E ,ACS_ACCESSLEVEL A ,ENT_EMPLOYEE_PERSONAL_DTLS EP " +
                    //         "where  E.ENTITY_TYPE ='" + EntityType + "' and E.AL_ID=A.AL_ID and isDELETED='0' and EP.EPD_EMPID=E.EMPLOYEE_CODE " +
                    //         "group by E.EMPLOYEE_CODE,E.AL_ID,A.AL_DESCRIPTION,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME,E.ENTITY_TYPE ";

                    sql = "select E.EMPLOYEE_CODE as ID,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME as DES,case E.ENTITY_TYPE when 'EMP' then 'EMPLOYEE' when 'DEP' then 'DEPARTMENT' " +
                            "when 'DES' then 'DESIGNATION' when 'DIV' then 'DIVISION' when 'GRD' then 'GRADE' when 'GRP' then 'GROUP' " +
                            "when 'LOC' then 'LOCATION'else ''  end as ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                            "from EAL_CONFIG E ,ACS_ACCESSLEVEL A ,ENT_EMPLOYEE_PERSONAL_DTLS EP " +
                            "where E.AL_ID=A.AL_ID and isDELETED='0' and EP.EPD_EMPID=E.EMPLOYEE_CODE " +
                            "group by E.EMPLOYEE_CODE,E.AL_ID,A.AL_DESCRIPTION,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME,E.ENTITY_TYPE ";

                }
                else
                {
                    sql = "select E.ENTITY_ID as ID,CE.OCE_DESCRIPTION as DES,E.ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                             "from EAL_CONFIG E,ACS_ACCESSLEVEL A ,ENT_ORG_COMMON_ENTITIES CE " +
                             "where E.ENTITY_TYPE='" + EntityType + "' and E.AL_ID=A.AL_ID and isDELETED='0' " +
                             "and CE.CEM_ENTITY_ID='" + EntityType + "' and CE.OCE_ID=E.ENTITY_ID  " +
                             "group by E.ENTITY_ID,CE.OCE_DESCRIPTION,E.AL_ID ,A.AL_DESCRIPTION,E.ENTITY_TYPE ";
                }


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ///////////Started///////////
                 DataTable thisDataSet = new DataTable(); ;
                 DataTable temp = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    string levelId = Session["levelId"].ToString();
                    
                    SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                    thisDataSet = new DataTable();
                    adpt.Fill(thisDataSet);

                    IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
                                                  join comwise in thisDataSet.AsEnumerable() on acs.Field<string>("ID") equals comwise.Field<string>("EOD_EMPID")
                                                  select acs
                                 );
                    if (Session["uid"].ToString().ToLower() == "admin")
                    {
                        temp = dt;
                    }
                    else
                    {
                        temp = drRow.CopyToDataTable();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    ///////////end////////////
                }
                gvEmployeeAccess.DataSource = temp;
                gvEmployeeAccess.DataBind();

                DropDownList ddl = (DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmployeeAccess.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmployeeAccess.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmployeeAccess.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmployeeAccess.PageCount == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex + 1 == gvEmployeeAccess.PageCount)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1));

                ((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (((gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1)) - 10) + gvEmployeeAccess.Rows.Count);

                gvEmployeeAccess.BottomPagerRow.Visible = true;

                if (dt.Rows.Count != 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = Convert.ToInt32(((DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid(ddlEntityType.SelectedValue);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = gvEmployeeAccess.PageIndex - 1;
                search();
                //bindDataGrid(ddlEntityType.SelectedValue);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = gvEmployeeAccess.PageIndex + 1;
                search();
                //bindDataGrid(ddlEntityType.SelectedValue);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = "";
                if (ddlEntityType.SelectedValue != "0")
                {
                    bindDataGrid(ddlEntityType.SelectedValue);
                }
                else
                {
                    gvEmployeeAccess.DataSource = new DataTable();
                    gvEmployeeAccess.DataBind();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lstAALAdd.Items.Clear();
                lstSALAdd.Items.Clear();
                lstAEntityAdd.Items.Clear();
                ListBox1.Items.Clear();
                lblCount.Text = "0";
                lstSEntityAdd.Items.Clear();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("select ISNULL(max(EAL_ID)+1,1) as id from EAL_CONFIG", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                txtUAPIDAdd.Text = dt.Rows[0]["id"].ToString();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new SqlCommand("select AL_ID,AL_DESCRIPTION from ACS_ACCESSLEVEL where AL_ISDELETED='0'", conn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt1 != null)
                {
                    for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    {
                        lstAALAdd.Items.Add(new ListItem(dt1.Rows[i][1].ToString(), dt1.Rows[i][0].ToString()));
                    }
                }
                mpeAddEmployeeAccess.Show();
                lblMessages.Text = string.Empty;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void ddlEntityAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fillEntityAdd();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        private void fillEntityAdd()
        {
            try
            {
                lstAEntityAdd.Items.Clear();
                ListBox1.Items.Clear();
                lstSEntityAdd.Items.Clear();
                string sql = "";

                if (Session["uid"].ToString().ToLower() == "admin")
                {
                    if (ddlEntityAdd.SelectedValue == "EMP")
                    {
                        //sql = "select EPD_EMPID+'-'+EPD_CARD_ID as ID,replace(convert(char(25),ltrim(SUBSTRING(EPD_FIRST_NAME+ ' '+ EPD_LAST_NAME,1,16)))+(replicate('0', 10 - len(EPD_EMPID)) + EPD_EMPID),' ',' ' ) as EMPNAME "+
                        //      " from ENT_EMPLOYEE_PERSONAL_DTLS emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop on emp.EPD_EMPID=eop.EOD_EMPID "+
                        //      " where EPD_EMPID not in ( select EMPLOYEE_CODE from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityAdd.SelectedValue + "' and ISDELETED='0' )  and  EPD_ISDELETED ='0' and eop.EOD_ACTIVE='1' and EPD_CARD_ID<>'' ";

                        sql = "select EPD_EMPID+'-'+EPD_CARD_ID as ID,replace(convert(char(25),ltrim(SUBSTRING(EPD_FIRST_NAME+ ' '+ EPD_LAST_NAME,1,16)))+(replicate('0', 10 - len(EPD_EMPID)) + EPD_EMPID),' ',' ' ) as EMPNAME " +
                              " from ENT_EMPLOYEE_PERSONAL_DTLS emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop on emp.EPD_EMPID=eop.EOD_EMPID " +
                              " where EPD_EMPID not in ( select EMPLOYEE_CODE from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityAdd.SelectedValue + "' and ISDELETED='0' )  and  EPD_ISDELETED ='0' and eop.EOD_ACTIVE='1' and EPD_CARD_ID<>'' and EPD_EMPID in (select CC_EMP_ID from ACS_CARD_CONFIG) ";

                    }
                    else
                    {
                        sql = "select  OCE_ID ,replace(convert(char(25),ltrim(SUBSTRING(OCE_DESCRIPTION,1,16))) + (replicate('0', 10 - len(OCE_ID)) + OCE_ID),' ',' ' ) as OCE_DESCRIPTION from ENT_ORG_COMMON_ENTITIES where CEM_ENTITY_ID='" + ddlEntityAdd.SelectedValue + "' " +
                                     "and OCE_ID not in ( select distinct(ENTITY_ID) from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityAdd.SelectedValue + "' and ISDELETED='0' ) and OCE_ISDELETED='0'";
                    }



                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    if (dt != null)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            lstAEntityAdd.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            ListBox1.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        }
                    }

                }
                else
                {

                    string levelId = Session["levelId"].ToString();

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlCommand cmd = new SqlCommand("spFillEntities2", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@levelid", levelId);

                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet thisDataSet = new DataSet();
                    adpt.Fill(thisDataSet);


                    sql = "select EOD_EMPID,EPD_EMPID+'-'+EPD_CARD_ID as ID,replace(convert(char(25),ltrim(SUBSTRING(EPD_FIRST_NAME+ ' '+ EPD_LAST_NAME,1,16)))+(replicate('0', 10 - len(EPD_EMPID)) + EPD_EMPID),' ',' ' ) as EmployeeName " +
                              " from ENT_EMPLOYEE_PERSONAL_DTLS emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eop on emp.EPD_EMPID=eop.EOD_EMPID " +
                              " where EPD_EMPID not in ( select EMPLOYEE_CODE from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityAdd.SelectedValue + "' and ISDELETED='0' )  and  EPD_ISDELETED ='0' and eop.EOD_ACTIVE='1' and EPD_CARD_ID<>'' and EPD_EMPID in (select CC_EMP_ID from ACS_CARD_CONFIG) ";
                    SqlCommand cmd1 = new SqlCommand(sql, conn);
                    cmd1.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dtAdmEmp = new DataTable();
                    da.Fill(dtAdmEmp);

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    DataTable dtEmployee = new DataTable();
                    DataTable dtTemp = thisDataSet.Tables[0].DefaultView.ToTable(true, "EOD_EMPID", "EmployeeName");
                    var obj = (from e1 in dtTemp.AsEnumerable()
                               join e2 in dtAdmEmp.AsEnumerable() on e1.Field<string>("EOD_EMPID") equals e2.Field<string>("EOD_EMPID")
                               select new
                               {
                                   EOD_EMPID = e1.Field<string>("EOD_EMPID"),
                                   EmployeeName = e2.Field<string>("EmployeeName"),
                               });

                    dtEmployee.Columns.Add("EOD_EMPID");
                    dtEmployee.Columns.Add("EmployeeName");

                    foreach (var item in obj)
                    {
                        dtEmployee.Rows.Add(item.EOD_EMPID, item.EmployeeName);
                    }

                    DataTable dtDivision = thisDataSet.Tables[3].DefaultView.ToTable(true, "DivisionID", "Division_NAME");
                    DataTable dtLocation = thisDataSet.Tables[1].DefaultView.ToTable(true, "LocationID", "Location_NAME");
                    DataTable dtcom = thisDataSet.Tables[4].DefaultView.ToTable(true, "CompanyID", "COMPANY_NAME");
                    DataTable dtDep = thisDataSet.Tables[2].DefaultView.ToTable(true, "DepartmentID", "Department_NAME");
                    DataTable dtGrade = thisDataSet.Tables[6].DefaultView.ToTable(true, "gradeid", "grade_name");
                    DataTable dtGroup = thisDataSet.Tables[7].DefaultView.ToTable(true, "groupid", "group_name");

                    DataTable dtCategory = thisDataSet.Tables[8].DefaultView.ToTable(true, "CategoryID", "Category_NAME");
                    DataTable dtDESgnation = thisDataSet.Tables[9].DefaultView.ToTable(true, "DesignationID", "Designation_NAME");

                    string str = ddlEntityAdd.SelectedValue;

                    DataTable dt = new DataTable();

                    switch (str)
                    {
                        case "CAT":
                            dt = dtCategory;
                            break;
                        case "DEP":
                            dt = dtDep;
                            break;
                        case "GRD":
                            dt = dtGrade;
                            break;
                        case "DIV":
                            dt = dtDivision;
                            break;
                        case "LOC":
                            dt = dtLocation;
                            break;
                        case "GRP":
                            dt = dtGroup;
                            break;
                        case "DES":
                            dt = dtDESgnation;
                            break;
                        default:
                            dt = dtEmployee;
                            break;
                    }
                    if (dt != null)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            lstAEntityAdd.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            ListBox1.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        }
                    }

                    dt.Clear();
                }


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void cmdEntityAllRightAdd_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerator _ie = lstAEntityAdd.Items.GetEnumerator();
                while (_ie.MoveNext())
                {
                    ListItem _li = (ListItem)_ie.Current;
                    lstSEntityAdd.Items.Add(_li);
                }
                lstAEntityAdd.Items.Clear();
                ListBox1.Items.Clear();
                lstSEntityAdd.SelectedValue = null;

                lblCount.Text = lstSEntityAdd.Items.Count.ToString();

                txtAEntityDescAdd.Text = "";
                txtAEntityCodeAdd.Text = "";
                txtSEntityDescAdd.Text = "";
                txtSEntityCodeAdd.Text = "";
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void cmdEntityRightAdd_Click(object sender, EventArgs e)
        {
            for (int i = lstAEntityAdd.Items.Count - 1; i >= 0; i--)
            {
                if (lstAEntityAdd.Items[i].Selected == true)
                {
                    lstSEntityAdd.Items.Add(lstAEntityAdd.Items[i]);
                    ListItem li = lstAEntityAdd.Items[i];
                    lstAEntityAdd.Items.Remove(li);
                }
            }
            lblCount.Text = lstSEntityAdd.Items.Count.ToString();

            lstSEntityAdd.SelectedValue = null;
            txtAEntityDescAdd.Text = "";
            txtAEntityCodeAdd.Text = "";
            txtSEntityDescAdd.Text = "";
            txtSEntityCodeAdd.Text = "";
        }

        protected void cmdEntityLeftAdd_Click(object sender, EventArgs e)
        {
            for (int i = lstSEntityAdd.Items.Count - 1; i >= 0; i--)
            {
                if (lstSEntityAdd.Items[i].Selected == true)
                {
                    lstAEntityAdd.Items.Add(lstSEntityAdd.Items[i]);
                    ListItem li = lstSEntityAdd.Items[i];
                    lstSEntityAdd.Items.Remove(li);
                }
            }
            lblCount.Text = lstSEntityAdd.Items.Count.ToString();

            lstAEntityAdd.SelectedValue = null;
            txtAEntityDescAdd.Text = "";
            txtAEntityCodeAdd.Text = "";
            txtSEntityDescAdd.Text = "";
            txtSEntityCodeAdd.Text = "";

        }

        protected void cmdEntityAllLeftAdd_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSEntityAdd.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAEntityAdd.Items.Add(_li);
            }

            lstSEntityAdd.Items.Clear();
            lblCount.Text = lstSEntityAdd.Items.Count.ToString();

            lstAEntityAdd.SelectedValue = null;
            txtAEntityDescAdd.Text = "";
            txtAEntityCodeAdd.Text = "";
            txtSEntityDescAdd.Text = "";
            txtSEntityCodeAdd.Text = "";
        }

        protected void cmdALLALRightAdd_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAALAdd.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSALAdd.Items.Add(_li);

            }
            lstAALAdd.Items.Clear();
            lstSALAdd.SelectedValue = null;
        }

        protected void cmdALRightAdd_Click(object sender, EventArgs e)
        {
            for (int i = lstAALAdd.Items.Count - 1; i >= 0; i--)
            {
                if (lstAALAdd.Items[i].Selected == true)
                {
                    lstSALAdd.Items.Add(lstAALAdd.Items[i]);
                    ListItem li = lstAALAdd.Items[i];
                    lstAALAdd.Items.Remove(li);
                }
            }
            lstSALAdd.SelectedValue = null;
        }

        protected void cmdALLeftAdd_Click(object sender, EventArgs e)
        {
            for (int i = lstSALAdd.Items.Count - 1; i >= 0; i--)
            {
                if (lstSALAdd.Items[i].Selected == true)
                {
                    lstAALAdd.Items.Add(lstSALAdd.Items[i]);
                    ListItem li = lstSALAdd.Items[i];
                    lstSALAdd.Items.Remove(li);
                }
            }
            lstAALAdd.SelectedValue = null;
        }

        protected void cmdALLALLeftAdd_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSALAdd.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAALAdd.Items.Add(_li);
            }
            lstSALAdd.Items.Clear();
            lstAALAdd.SelectedValue = null;
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddEmployeeAccess.Hide();
            lblErrorAdd.Visible = false;
            ddlEntityAdd.SelectedValue = "0";
        }

        private void InsertEALConfig(string strMode)
        {
            try
            {
                if (Page.IsValid && validationAdd())
                {

                    string strEmpFilter = "", strCatFilter = "", strDepFilter = "", strDesFilter = "", strDivFiltyer = "", strGRDFilter = "", strGRPFilter = "", strLocFilter = "";


                    _strList = "(" + _strList + ")";

                    if (ddlEntityAdd.SelectedValue == "EMP")
                    {
                        strEmpFilter = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "CAT")
                    {
                        strCatFilter = _strList;

                    }
                    else if (ddlEntityAdd.SelectedValue == "DEP")
                    {
                        strDepFilter = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "DES")
                    {
                        strDesFilter = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "DIV")
                    {
                        strDivFiltyer = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "GRD")
                    {
                        strGRDFilter = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "GRP")
                    {
                        strGRPFilter = _strList;
                    }
                    else if (ddlEntityAdd.SelectedValue == "LOC")
                    {
                        strLocFilter = _strList;
                    }



                    bool _transResult = false;
                    string strSelectedALIDs = "";

                    for (int i = 0; i <= lstSALAdd.Items.Count - 1; i++)
                    {
                        strSelectedALIDs = strSelectedALIDs + (strSelectedALIDs.Length > 0 ? "," : "") + "'" + lstSALAdd.Items[i].Value.ToString() + "'";
                    }
                    strSelectedALIDs = "(" + strSelectedALIDs + ")";


                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "PROC_INSERT_EAL_CONFIG";
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@EMPCODE", strEmpFilter);
                    cmd.Parameters.AddWithValue("@CATCDE", strCatFilter);
                    cmd.Parameters.AddWithValue("@DEPCDE", strDepFilter);
                    cmd.Parameters.AddWithValue("@DESCDE", strDesFilter);
                    cmd.Parameters.AddWithValue("@DIVCDE", strDivFiltyer);
                    cmd.Parameters.AddWithValue("@GRDCDE", strGRDFilter);
                    cmd.Parameters.AddWithValue("@GRPCDE", strGRPFilter);
                    cmd.Parameters.AddWithValue("@LOCCDE", strLocFilter);
                    cmd.Parameters.AddWithValue("@pAL_ID", strSelectedALIDs);
                    cmd.Parameters.AddWithValue("@levelID", Session["levelId"].ToString());



                    if (strMode == "INSERT")
                        cmd.Parameters.AddWithValue("@pMode", "INSERT");
                    else if (strMode == "UPDATE")
                        cmd.Parameters.AddWithValue("@pMode", "UPDATE");



                    int iRowsAffected = cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    // if (iRowsAffected > 0)
                    //{

                    if (strMode == "INSERT")
                    {
                        lblErrorAdd.Visible = true;
                        lblErrorAdd.Text = "Records Saved Successfully";
                        mpeAddEmployeeAccess.Show();
                    }
                    else if (strMode == "UPDATE")
                    {
                        lblMessages.Visible = true;
                        lblMessages.Text = "Records Updated Successfully";
                        mpeAddEmployeeAccess.Hide();
                    }

                    if (ddlEntityType.SelectedValue != "0")
                    {
                        bindDataGrid(ddlEntityType.SelectedValue);
                    }
                    else
                    {
                        gvEmployeeAccess.DataSource = new DataTable();
                        gvEmployeeAccess.DataBind();
                    }

                    // }

                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        private void UpdateEALConfig(string strMode)
        {
            try
            {
                if (Page.IsValid && validationEdit())
                {

                    string strEmpFilter = "", strCatFilter = "", strDepFilter = "", strDesFilter = "", strDivFiltyer = "", strGRDFilter = "", strGRPFilter = "", strLocFilter = "";


                    _strList = "(" + _strList + ")";

                    if (ddlEntityEdit.SelectedValue == "EMP")
                    {
                        strEmpFilter = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "CAT")
                    {
                        strCatFilter = _strList;

                    }
                    else if (ddlEntityEdit.SelectedValue == "DEP")
                    {
                        strDepFilter = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "DES")
                    {
                        strDesFilter = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "DIV")
                    {
                        strDivFiltyer = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "GRD")
                    {
                        strGRDFilter = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "GRP")
                    {
                        strGRPFilter = _strList;
                    }
                    else if (ddlEntityEdit.SelectedValue == "LOC")
                    {
                        strLocFilter = _strList;
                    }



                    bool _transResult = false;
                    string strSelectedALIDs = "";

                    for (int i = 0; i <= lstSALEdit.Items.Count - 1; i++)
                    {
                        strSelectedALIDs = strSelectedALIDs + (strSelectedALIDs.Length > 0 ? "," : "") + "'" + lstSALEdit.Items[i].Value.ToString() + "'";
                    }
                    strSelectedALIDs = "(" + strSelectedALIDs + ")";


                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_INSERT_EAL_CONFIG";
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@EMPCODE", strEmpFilter);
                    cmd.Parameters.AddWithValue("@CATCDE", strCatFilter);
                    cmd.Parameters.AddWithValue("@DEPCDE", strDepFilter);
                    cmd.Parameters.AddWithValue("@DESCDE", strDesFilter);
                    cmd.Parameters.AddWithValue("@DIVCDE", strDivFiltyer);
                    cmd.Parameters.AddWithValue("@GRDCDE", strGRDFilter);
                    cmd.Parameters.AddWithValue("@GRPCDE", strGRPFilter);
                    cmd.Parameters.AddWithValue("@LOCCDE", strLocFilter);
                    cmd.Parameters.AddWithValue("@pAL_ID", strSelectedALIDs);

                    if (strMode == "INSERT")
                        cmd.Parameters.AddWithValue("@pMode", "INSERT");
                    else if (strMode == "UPDATE")
                        cmd.Parameters.AddWithValue("@pMode", "UPDATE");



                    int iRowsAffected = cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    //if (iRowsAffected > 0)
                    //{

                    if (strMode == "INSERT")
                    {
                        lblMessages.Text = "Records Saved Successfully";
                    }

                    if (strMode == "UPDATE")
                    {
                        lblMessages.Text = "Records Updated Successfully";
                    }


                    lblMessages.Visible = true;

                    if (ddlEntityType.SelectedValue != "0")
                    {
                        bindDataGrid(ddlEntityType.SelectedValue);
                    }
                    else
                    {
                        gvEmployeeAccess.DataSource = new DataTable();
                        gvEmployeeAccess.DataBind();
                    }

                    mpeEditEmployeeAccess.Hide();

                    // }

                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        private bool validateBeforeInsert()
        {
            if (ddlEntityAdd.SelectedValue == "EMP")
            {
                for (int i = 0; i <= lstSEntityAdd.Items.Count - 1; i++)
                {
                    string[] empCode;
                    empCode = lstSEntityAdd.Items[i].Value.ToString().Split('-');
                    if (checker(empCode[0]))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checker(string s)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "access_level_validation";

            cmd.Parameters.AddWithValue("@cmd", 1);
            cmd.Parameters.AddWithValue("@employeeId", s);

            //SqlParameter a = new SqlParameter();
            //a.ParameterName = "@output";
            //a.Direction = ParameterDirection.Output;
            // SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@output", '0').Direction = ParameterDirection.Output;
            cmd.Parameters["@output"].Size = 1000;

            cmd.ExecuteNonQuery();

            int output = Convert.ToInt16(cmd.Parameters["@output"].Value);
            conn.Close();
            if (output == 100)
            {
                return false;
            }
            else
            {
                if (output + lstSALAdd.Items.Count > 4)
                    return false;
                else
                    return true;
            }
        }
        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            if (validateBeforeInsert())
            {
                InsertEALConfig("INSERT");
                mpeAddEmployeeAccess.Show();
                bindDataGrid(ddlEntityType.SelectedValue);
                btnAdd_Click(sender, e);
                ddlEntityAdd.SelectedIndex = 0;
            }
            else
            {
                lblErrorAdd.Text = "One Employee cannot have more than 4 Access Level ";
                lblErrorAdd.Visible = true;
            }

            /*
            try
            {
                if (Page.IsValid && validationAdd())
                {
                    String _empSelectQuery = "", _insertQuery = "", _deleteQuery = "";
                    if (ddlEntityAdd.SelectedValue == "EMP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EPD_EMPID as ENTITY_ID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID in " +
                                          "(" + _strList.Trim() + ") and EPD_ISDELETED='0' and EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "CAT")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_CATEGORY_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "DEP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DEPARTMENT_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DEPARTMENT_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "DES")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DESIGNATION_ID as ENTITY_ID  " +
                                         "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                         "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                         "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "DIV")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DIVISION_ID as ENTITY_ID  " +
                                           "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                           "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                           "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "GRD")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GRADE_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "GRP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GROUP_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }
                    else if (ddlEntityAdd.SelectedValue == "LOC")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_LOCATION_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID and EP.EPD_ISDELETED='0' and EP.EPD_CARD_ID<>''";
                    }

                    DataTable _dt = new DataTable();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(_empSelectQuery, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(_dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    //_dt = getDataTable(_empSelectQuery, _sqlConnection);
                    if (_dt.Rows.Count > 0)
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlTransaction _trans;
                        Boolean _result = true, _transResult = true, _deleteResult = true;
                        _trans = conn.BeginTransaction();

                        for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                        {
                            _deleteQuery = "";
                            _deleteQuery = "Delete from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityAdd.SelectedValue + "' and ENTITY_ID='"
                                + _dt.Rows[j]["ENTITY_ID"].ToString() + "' and EMPLOYEE_CODE='" + _dt.Rows[j]["EPD_EMPID"].ToString() + "'";
                            cmd = new SqlCommand(_deleteQuery, conn, _trans);
                            cmd.ExecuteNonQuery();

                            //_deleteResult = RunExecuteNonQueryWithTransaction(_deleteQuery, _sqlConnection, _trans);

                        }


                        for (int i = 0; i <= lstSALAdd.Items.Count - 1; i++)
                        {

                            for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                            {

                                _insertQuery = "";
                                _insertQuery = "insert into EAL_CONFIG(ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE,AL_ID,FLAG,ISDELETED,DELETEDDATE)" +
                                               "values('" + ddlEntityAdd.SelectedValue + "','" + _dt.Rows[j]["ENTITY_ID"].ToString() + "','" + _dt.Rows[j]["EPD_EMPID"].ToString() + "','" + _dt.Rows[j]["EPD_CARD_ID"].ToString() + "','" + lstSALAdd.Items[i].Value.ToString() + "','1','false',null)";
                                cmd = new SqlCommand(_insertQuery, conn, _trans);
                                _result = (cmd.ExecuteNonQuery() == 0) ? false : true;
                                //_result = RunExecuteNonQueryWithTransaction(_insertQuery, _sqlConnection, _trans);

                                if (_result == false)
                                {
                                    _transResult = false;
                                    break;
                                }
                            }
                            if (_transResult == false)
                                break;
                        }
                        if (_transResult == true)
                        {
                            _trans.Commit();
                            lblMessages.Text = "Records Saved Successfully";
                            lblMessages.Visible = true;
                            //this.messageDiv.InnerText = "Records Saved Successfully";
                            String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='UAC'";
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            cmd = new SqlCommand(_updateStr, conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            mpeAddEmployeeAccess.Hide();
                            if (ddlEntityType.SelectedValue != "0")
                            {
                                bindDataGrid(ddlEntityType.SelectedValue);
                            }
                            else
                            {
                                gvEmployeeAccess.DataSource = new DataTable();
                                gvEmployeeAccess.DataBind();
                            }
                            //Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                            //intializeControl();
                            //string someScript2 = "";
                            //someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",5000);</script>";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                        }
                    }
                    else
                    {
                        lblErrorAdd.Text = "This Entity Specification doest not belong to any employee.";
                        lblErrorAdd.Visible = true;
                        //this.messageDiv.InnerText = "This Entity Specification doest not belong to any employee.";
                        //string someScript2 = "";
                        //someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",5000);</script>";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                    }
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }

            */
        }
        public Boolean validationAdd()
        {
            try
            {
                if (checkEntityAccessPermissionAdd() == false)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean checkEntityAccessPermissionAdd()
        {
            try
            {
                _strList = "";
                _strALList = "";
                string[] _empCode;
                if (ddlEntityAdd.SelectedValue == "EMP")
                {
                    for (int i = 0; i <= lstSEntityAdd.Items.Count - 1; i++)
                    {
                        _empCode = lstSEntityAdd.Items[i].Value.ToString().Split('-');
                        _strList = _strList + "'" + _empCode[0].ToString() + "',";
                    }
                    _strList = _strList.Substring(0, (_strList.Length - 1));
                }
                else
                {
                    for (int i = 0; i <= lstSEntityAdd.Items.Count - 1; i++)
                    {
                        _strList = _strList + "'" + lstSEntityAdd.Items[i].Value + "',";
                    }
                    _strList = _strList.Substring(0, (_strList.Length - 1));
                }


                for (int i = 0; i <= lstSALAdd.Items.Count - 1; i++)
                {
                    _strALList = _strALList + "'" + lstSALAdd.Items[i].Value.ToString() + "',";
                }
                _strALList = _strALList.Substring(0, (_strALList.Length - 1));


                String _strSelect = "";
                _strSelect = "select EMPLOYEE_CODE,LevelCount " +
                               "from (" +
                               "select EMPLOYEE_CODE,count(*) LevelCount " +
                               "from ( " +
                               "select AL_ID,EMPLOYEE_CODE ,count(*)  cnt " +
                               "from EAL_CONFIG " +
                               "where EMPLOYEE_CODE in ";

                if (ddlEntityAdd.SelectedValue == "EMP")
                {
                    _strSelect = _strSelect + "(" + _strList.Trim() + ") ";
                }
                else if (ddlEntityAdd.SelectedValue == "CAT")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "DEP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DEPARTMENT_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "DES")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "DIV")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "GRD")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "GRP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityAdd.SelectedValue == "LOC")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                //_strSelect = _strSelect + "and AL_ID not in("+ _strALList +") " +
                _strSelect = _strSelect + "and AL_ID not in( " + _strALList + " ) and entity_type<>'" + ddlEntityAdd.SelectedValue + "' and ISDELETED='0' and FLAG<>'3'" +
                                         "group by AL_ID,EMPLOYEE_CODE " +
                                         ") Sel " +
                                         "group by Sel.EMPLOYEE_CODE " +
                                         ")Wel " +
                    //"where Wel.LevelCount>3 ";
                                          "where Wel.LevelCount + " + (lstSALAdd.Items.Count) + ">4 ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(_strSelect, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable _dtResult = new DataTable();
                da.Fill(_dtResult);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                //DataTable _dtResult = getDataTable(_strSelect, _sqlConnection);
                if (_dtResult.Rows.Count > 0)
                {
                    if (ddlEntityAdd.SelectedValue != "EMP")
                    {
                        lblErrorAdd.Text = "Can not Add records.Because " + _dtResult.Rows.Count.ToString() + " employee(s) will exceed no of maximum level attched to them.";
                        lblErrorAdd.Visible = true;
                        //this.messageDiv.InnerHtml = "Can not Add records.Because " + _dtResult.Rows.Count.ToString() + " employee(s) will exceed no of maximum level attched to them.";
                        return false;
                    }
                    else
                    {
                        lblErrorAdd.Text = "Can not Add records.Because employee will exceed no of maximum level attched to them.";
                        lblErrorAdd.Visible = true;
                        //this.messageDiv.InnerHtml = "Can not Add records.Because employee will exceed no of maximum level attched to them.";
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
                return false;
            }
        }
        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return null;
            }
        }
        public DataTable getDataTablewithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.Transaction = _sqlTrans;
                _sc.CommandText = _strQuery;

                SqlDataAdapter _sqa = new SqlDataAdapter(_sc);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return null;
            }
        }
        public string getValue(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                if (_ds.Tables[0].Rows.Count > 0)

                    return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return "";
            }
            return "";
        }
        public bool RunExecuteNonQuery(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return false;
            }
        }
        public bool RunExecuteNonQueryWithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;

                _sc.Transaction = _sqlTrans;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                if (_result == 0)
                {
                    // _sqlTrans.Rollback();
                    return false;

                }
                else
                {
                    _sqlTrans.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _sqlTrans.Rollback();
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return false;
            }
        }

        protected void gvEmployeeAccess_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    txtUPAIDEdit.Text = e.CommandArgument.ToString();
                    ddlEntityEdit.Items.Clear();
                    ddlEntityEdit.Items.Add(new ListItem("EMPLOYEE", "EMP"));
                    lstAALEdit.Items.Clear();
                    lstSALEdit.Items.Clear();
                    lstAEntityEdit.Items.Clear();
                    lstSEntityEdit.Items.Clear();

                    DataTable _dt = new DataTable();

                    try
                    {
                        string _strsql = "select CODE,VALUE from ENT_PARAMS where identifier='COMMONMASTERS' and module='ENT' Order by [value]";

                        _dt = getDataTable(_strsql, conn);
                        if (_dt.Rows.Count != 0)
                        {
                            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                            {
                                ddlEntityEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    String _entityType = getValue("select distinct(ENTITY_TYPE) from EAL_CONFIG where ENTITY_ID='" + txtUPAIDEdit.Text + "' and FLAG <>'3'", conn);
                    if (_entityType != "")
                    {
                        ddlEntityEdit.SelectedValue = _entityType;
                    }


                    String _strSelect = "";
                    if (ddlEntityEdit.SelectedValue == "EMP")
                    {
                        //_strSelect = "select EPD_EMPID as ID,EPD_FIRST_NAME+ ' ' + EPD_MIDDLE_NAME + ' '+EPD_LAST_NAME + '-' +  EPD_EMPID as EMPNAME from ENT_EMPLOYEE_PERSONAL_DTLS " +
                        //             "where EPD_EMPID ='" + _strId + "'";
                        _strSelect = "select EPD_EMPID as ID,replace(convert(char(25),ltrim(SUBSTRING(EPD_FIRST_NAME+ ' '+ EPD_LAST_NAME ,1,10)))+(replicate('0', 10 - len(EPD_EMPID)) + rtrim(EPD_EMPID)),' ',' ' ) as EMPNAME from ENT_EMPLOYEE_PERSONAL_DTLS " +
                                     "where EPD_EMPID ='" + txtUPAIDEdit.Text + "'";
                    }
                    else
                    {
                        _strSelect = "select  OCE_ID,OCE_DESCRIPTION from ENT_ORG_COMMON_ENTITIES where CEM_ENTITY_ID='" + ddlEntityEdit.SelectedValue + "' " +
                                     "and OCE_ID ='" + txtUPAIDEdit.Text + "'";
                    }

                    _dt = getDataTable(_strSelect, conn);

                    lstAEntityEdit.Items.Add(new ListItem(_dt.Rows[0][1].ToString(), _dt.Rows[0][0].ToString()));


                    ArrayList _level = new ArrayList();
                    try
                    {
                        _strSelect = "";
                        _strSelect = "select distinct (E.AL_ID),AL.AL_DESCRIPTION from EAL_CONFIG E,ACS_ACCESSLEVEL AL where E.ENTITY_TYPE='" + ddlEntityEdit.SelectedValue + "' and E.ENTITY_ID='" + txtUPAIDEdit.Text + "' and E.AL_ID=AL.AL_ID and E.ISDELETED='0'";
                        _dt = getDataTable(_strSelect, conn);
                        if (_dt.Rows.Count != 0)
                        {
                            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                            {
                                lstSALEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                                _level.Add(_dt.Rows[i][0].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Session.Add("LEVEL", _level);

                    //levelCount = lstSAL.Items.Count;

                    try
                    {
                        _strSelect = "select AL_ID,AL_DESCRIPTION from ACS_ACCESSLEVEL where AL_ISDELETED='0' and AL_ID not in " +
                                            "(select distinct (E.AL_ID)from EAL_CONFIG E,ACS_ACCESSLEVEL AL " +
                                            "where E.ENTITY_TYPE='" + _entityType + "' and E.ENTITY_ID='" + txtUPAIDEdit.Text + "' and E.AL_ID=AL.AL_ID and E.ISDELETED='0') ";
                        _dt = getDataTable(_strSelect, conn);
                        if (_dt.Rows.Count != 0)
                        {
                            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                            {
                                lstAALEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    mpeEditEmployeeAccess.Show();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void cmdALLALRightEdit_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAALEdit.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSALEdit.Items.Add(_li);

            }
            lstAALEdit.Items.Clear();
            lstSALEdit.SelectedValue = null;
        }

        protected void cmdALRightEdit_Click(object sender, EventArgs e)
        {
            for (int i = lstAALEdit.Items.Count - 1; i >= 0; i--)
            {
                if (lstAALEdit.Items[i].Selected == true)
                {
                    lstSALEdit.Items.Add(lstAALEdit.Items[i]);
                    ListItem li = lstAALEdit.Items[i];
                    lstAALEdit.Items.Remove(li);
                }
            }
            lstSALEdit.SelectedValue = null;
        }

        protected void cmdALLeftEdit_Click(object sender, EventArgs e)
        {
            for (int i = lstSALEdit.Items.Count - 1; i >= 0; i--)
            {
                if (lstSALEdit.Items[i].Selected == true)
                {
                    lstAALEdit.Items.Add(lstSALEdit.Items[i]);
                    ListItem li = lstSALEdit.Items[i];
                    lstSALEdit.Items.Remove(li);
                }
            }
            lstAALEdit.SelectedValue = null;
        }

        protected void cmdALLALLeftEdit_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSALEdit.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAALEdit.Items.Add(_li);
            }
            lstSALEdit.Items.Clear();
            lstAALEdit.SelectedValue = null;
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            Session.Remove("LEVEL");
            mpeEditEmployeeAccess.Hide();
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            UpdateEALConfig("UPDATE");
            /*
            try
            {
                if (validationEdit())
                {
                    String _empSelectQuery = "", _insertQuery = "", _deleteQuery = "";
                    if (ddlEntityEdit.SelectedValue == "EMP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EPD_EMPID as ENTITY_ID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID in " +
                                          "(" + _strList.Trim() + ") ";
                    }
                    else if (ddlEntityEdit.SelectedValue == "CAT")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_CATEGORY_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "DEP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DESIGNATION_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "DES")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DESIGNATION_ID as ENTITY_ID  " +
                                         "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                         "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                         "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "DIV")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_DIVISION_ID as ENTITY_ID  " +
                                           "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                           "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                           "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "GRD")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GRADE_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "GRP")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_GROUP_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }
                    else if (ddlEntityEdit.SelectedValue == "LOC")
                    {
                        _empSelectQuery = "select EPD_EMPID,EPD_CARD_ID,EO.EOD_LOCATION_ID as ENTITY_ID  " +
                                          "from ENT_EMPLOYEE_PERSONAL_DTLS EP,ENT_EMPLOYEE_OFFICIAL_DTLS EO " +
                                          "where EPD_EMPID in ( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList + ") and  EOD_ISDELETED='0' ) " +
                                          "and EP.EPD_EMPID=EO.EOD_EMPID";
                    }



                    DataTable _dt = new DataTable();
                    _dt = getDataTable(_empSelectQuery, conn);
                    if (_dt.Rows.Count > 0)
                    {
                        SqlTransaction _trans;
                        Boolean _result = true, _transResult = true, _deleteResult = true;
                        _trans = conn.BeginTransaction();

                        for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                        {
                            _deleteQuery = "";
                            _deleteQuery = "Delete from EAL_CONFIG where ENTITY_TYPE='" + ddlEntityEdit.SelectedValue + "' and ENTITY_ID='"
                                + _dt.Rows[j]["ENTITY_ID"].ToString() + "' and EMPLOYEE_CODE='" + _dt.Rows[j]["EPD_EMPID"].ToString()
                                + "' and CARD_CODE= '" + _dt.Rows[j]["EPD_CARD_ID"].ToString() + "'";
                            _deleteResult = RunExecuteNonQueryWithTransaction(_deleteQuery, conn, _trans);

                        }
                        for (int i = 0; i <= lstSALEdit.Items.Count - 1; i++)
                        {
                            for (int j = 0; j <= _dt.Rows.Count - 1; j++)
                            {
                                _insertQuery = "";
                                _insertQuery = "insert into EAL_CONFIG(ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE,AL_ID,FLAG,ISDELETED,DELETEDDATE)" +
                                               "values('" + ddlEntityEdit.SelectedValue + "','" + _dt.Rows[j]["ENTITY_ID"].ToString() + "','" + _dt.Rows[j]["EPD_EMPID"].ToString() + "','" + _dt.Rows[j]["EPD_CARD_ID"].ToString() + "','" + lstSALEdit.Items[i].Value.ToString() + "','2','false',null)";
                                _result = RunExecuteNonQueryWithTransaction(_insertQuery, conn, _trans);
                                if (_result == false)
                                {
                                    _transResult = false;
                                    break;
                                }
                            }
                            if (_transResult == false)
                                break;
                        }
                        if (_transResult == true)
                        {
                            _trans.Commit();
                            lblMessages.Text = "Records Saved Successfully";
                            lblMessages.Visible = true;

                            //this.messageDiv.InnerText = "Records Saved Successfully";
                            String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='UAC'";
                            Boolean _bl = RunExecuteNonQuery(_updateStr, conn);

                            //string someScript = "";
                            //someScript = "<script language='javascript'>setTimeout(\"returnviewmode()\",2000);</script>";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                            mpeEditEmployeeAccess.Hide();
                        }
                    }
                    else
                    {
                        lblErrorEdit.Text = "This Entity Specification doest not belong to any employee.";
                        lblErrorEdit.Visible = true;
                        //this.messageDiv.InnerText = "This Entity Specification doest not belong to any employee.";

                    }

                }

                //string someScript2 = "";
                //someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",5000);</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }

            */
        }

        public String getALString()
        {
            try
            {
                String _str = "";

                ArrayList _AL = new ArrayList();
                _AL = (ArrayList)Session["LEVEL"];

                //for (int i = 0; i <= lstAEntity.Items.Count - 1; i++)
                //{
                //    _strList = _strList + "'" + lstSEntity.Items[i].Value + "',";
                //}
                //_strList = _strList.Substring(0, (_strList.Length - 1));


                for (int i = 0; i <= _AL.Count - 1; i++)
                {
                    _str = _str + "'" + _AL[i].ToString() + "',";
                }
                _str = _str.Length > 0 ? _str.Substring(0, (_str.Length - 1)) : _str;
                return _str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public Boolean checkEntityAccessPermissionEdit(String _strAL)
        {
            try
            {

                _strList = "";

                _strList = "'" + lstAEntityEdit.Items[0].Value.ToString() + "'";

                String _strSelect = "";
                _strSelect = "select EMPLOYEE_CODE,LevelCount " +
                               "from (" +
                               "select EMPLOYEE_CODE,count(*) LevelCount " +
                               "from ( " +
                               "select AL_ID,EMPLOYEE_CODE ,count(*)  cnt " +
                               "from EAL_CONFIG " +
                               "where EMPLOYEE_CODE in ";

                if (ddlEntityEdit.SelectedValue == "EMP")
                {
                    _strSelect = _strSelect + "(" + _strList.Trim() + ") ";
                }
                else if (ddlEntityEdit.SelectedValue == "CAT")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_CATEGORY_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "DEP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DEPARTMENT_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "DES")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DESIGNATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "DIV")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_DIVISION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "GRD")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GRADE_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "GRP")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_GROUP_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }
                else if (ddlEntityEdit.SelectedValue == "LOC")
                {
                    _strSelect = _strSelect + "( select EOD_EMPID from ENT_EMPLOYEE_OFFICIAL_DTLS where EOD_LOCATION_ID in(" + _strList.Trim() + ") and  EOD_ISDELETED='0' ) ";
                }

                if (_strAL.Length > 1) { _strSelect = _strSelect + "and AL_ID not in( " + _strAL + " )"; }
                _strSelect = _strSelect + " and entity_type<>'" + ddlEntityEdit.SelectedValue + "' and ISDELETED='0' and FLAG<>'3'" +
                                        " group by AL_ID,EMPLOYEE_CODE " +
                                        ") Sel " +
                                        "group by Sel.EMPLOYEE_CODE" +
                                        ")Wel " +
                                        "where Wel.LevelCount + " + (lstSALEdit.Items.Count) + ">4 ";


                String _strDelete = "";

                DataTable _dtResult = getDataTable(_strSelect, conn);

                if (_dtResult.Rows.Count > 0)
                {
                    lblErrorEdit.Text = "Can not Add records.Because " + _dtResult.Rows.Count.ToString() + " employee(s) will exceed no of maximum level attched to them.";
                    lblErrorEdit.Visible = true;
                    //this.messageDiv.InnerHtml = "Can not Add records.Because " + _dtResult.Rows.Count.ToString() + " employee(s) will exceed no of maximum level attched to them.";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean validationEdit()
        {
            try
            {
                //if (lstSAL.Items.Count == 0)
                //{ this.messageDiv.InnerHtml = "Please select Entity specification."; return false; }
                //else if (lstSAL.Items.Count > 4)
                //{ this.messageDiv.InnerHtml = "Maximum 4 access level can be selected."; return false; }
                //else 
                if (checkEntityAccessPermissionEdit(getALString()) == false)
                { return false; }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringCollection idCollection = new StringCollection();
            String index = "";
            bool Check = false;
            for (int i = 0; i < gvEmployeeAccess.Rows.Count; i++)
            {
                CheckBox delrows = (CheckBox)gvEmployeeAccess.Rows[i].FindControl("DeleteRows");
                if (delrows.Checked == true)
                {
                    index = (String)gvEmployeeAccess.DataKeys[i].Values[0].ToString();
                    index = index + "-" + (String)gvEmployeeAccess.DataKeys[i].Values[1].ToString();
                    index = index + "-" + (String)gvEmployeeAccess.DataKeys[i].Values[2].ToString();
                    idCollection.Add(index.ToString());
                }
            }

            if (idCollection.Count != 0)
            {
                Boolean _result = DeleteRecords(idCollection);
                if (_result == true)
                {
                    lblMessages.Text = "Record(s) Deleted Successfully";
                    lblMessages.Visible = true;
                    //this.messageDiv.InnerHtml = "Record Deleted Successfully";
                }
                bindDataGrid(ddlEntityType.SelectedValue);
                Session["CHECKED_ITEMS"] = null;
            }
            else
            {
                lblMessages.Text = "Please select record to delete";
                lblMessages.Visible = true;
            }

        }

        public Boolean DeleteRecords(StringCollection _idCollection)
        {
            try
            {
                Boolean _resultAP = true;
                String _deleteQuery = "";
                _strList = "";
                string[] _ID;
                String _ENTITY_TYPE = "";
                for (int i = 0; i <= _idCollection.Count - 1; i++)
                {
                    _ID = _idCollection[i].ToString().Trim().Split('-');
                    if (_strList.Contains("'" + _ID[0].ToString() + "'"))
                    { }
                    else
                    { _strList = _strList + "'" + _ID[0].ToString() + "',"; }
                }
                _strList = _strList.Substring(0, (_strList.Length - 1));


                string _strsql = "select CODE,VALUE from ENT_PARAMS where identifier='COMMONMASTERS' and module='ENT' Order by [value]";
                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, conn);



                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlTransaction _trans;
                Boolean _result = true, _transResult = true;
                //  _trans = conn.BeginTransaction();

                for (int i = 0; i <= _idCollection.Count - 1; i++)
                {
                    _deleteQuery = "";
                    _ID = _idCollection[i].ToString().Trim().Split('-');
                    if (ddlEntityType.SelectedValue == "EMP")
                    //_deleteQuery = "update EAL_CONFIG set FLAG='3', ISDELETED='1',DELETEDDATE =getdate() where ENTITY_TYPE='" + cmbEntity.SelectedValue + "' and EMPLOYEE_CODE ='" + _ID[0].ToString() + "' and AL_ID='" + _ID[1].ToString() + "'";
                    {
                        if (_ID[2].ToString().Trim() != "EMPLOYEE")
                        {
                            DataRow[] _resultR = _dt.Select("VALUE = '" + _ID[2].ToString().Trim() + "'");
                            _ENTITY_TYPE = _resultR[0][0].ToString().Trim();
                        }
                        else
                        { _ENTITY_TYPE = "EMP"; }

                        //_deleteQuery = "update EAL_CONFIG set FLAG='3', ISDELETED='1',DELETEDDATE =getdate() where ENTITY_TYPE='" + _ENTITY_TYPE + "' and EMPLOYEE_CODE ='" + _ID[0].ToString() + "' and AL_ID='" + _ID[1].ToString() + "'";
                        _deleteQuery = " exec PROC_DELETE_EALCONFIG @cmd ='0',@entityType ='" + _ENTITY_TYPE + "',@empCode='" + _ID[0].ToString() + "',@alId='" + _ID[1].ToString() + "' ";

                    }
                    else
                    {
                        _deleteQuery = " exec PROC_DELETE_EALCONFIG @cmd ='1',@entityType ='" + ddlEntityType.SelectedValue + "',@entityId='" + _ID[0].ToString() + "',@alId='" + _ID[1].ToString() + "' ";
                    }
                    // _result = RunExecuteNonQueryWithTransaction(_deleteQuery, conn, _trans);
                    SqlCommand cmd = new SqlCommand(_deleteQuery, conn);

                    int test = cmd.ExecuteNonQuery();
                    if (test == 0)
                    {
                        _result = false;
                    }


                    if (_result == false)
                    {
                        _transResult = false;
                        break;
                    }
                }
                if (_transResult == true)
                {
                    // _trans.Commit();
                    String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='UAC'";
                    Boolean _bl = RunExecuteNonQuery(_updateStr, conn);
                    lblMessages.Text = "Records Deleted Successfully";
                    lblMessages.Visible = true;
                    //this.messageDiv.InnerText = "Records Deleted Successfully";
                }
                else
                {
                    // _trans.Rollback();
                    //String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='UAC'";
                    //Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                    //_updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='CC'";
                    //_bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                    lblMessages.Text = "You have selected some access levels which has been attached to employee by other entity specification.";
                    lblMessages.Visible = true;
                    //this.messageDiv.InnerText = "You have selected some access levels which has been attached to employee by other entity specification.";
                    return false;
                }
                //}
                //else
                //{
                //    return false;
                //}


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private void search()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sql = "";
                string empcode = "";
                if (txt_empcode.Text.Length > 0) { empcode = "and EP.EPD_EMPID ='" + txt_empcode.Text.Trim() + "'"; }
                if (ddlEntityType.SelectedValue == "EMP")
                {
                    sql = "select E.EMPLOYEE_CODE as ID,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME as DES,case E.ENTITY_TYPE when 'EMP' then 'EMPLOYEE' when 'DEP' then 'DEPARTMENT' " +
                             "when 'DES' then 'DESIGNATION' when 'DIV' then 'DIVISION' when 'GRD' then 'GRADE' when 'GRP' then 'GROUP' " +
                             "when 'LOC' then 'LOCATION'else ''  end as ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                             "from EAL_CONFIG E ,ACS_ACCESSLEVEL A ,ENT_EMPLOYEE_PERSONAL_DTLS EP " +
                             "where  E.ENTITY_TYPE ='" + ddlEntityType.SelectedValue + "' and E.AL_ID=A.AL_ID and isDELETED='0' and EP.EPD_EMPID=E.EMPLOYEE_CODE " + empcode +
                             "group by E.EMPLOYEE_CODE,E.AL_ID,A.AL_DESCRIPTION,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME,E.ENTITY_TYPE ";
                }
                else
                {
                    sql = "select E.ENTITY_ID as ID,CE.OCE_DESCRIPTION as DES,E.ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                             "from EAL_CONFIG E,ACS_ACCESSLEVEL A ,ENT_ORG_COMMON_ENTITIES CE " +
                             "where E.ENTITY_TYPE='" + ddlEntityType.SelectedValue + "' and E.AL_ID=A.AL_ID and isDELETED='0' " +
                             "and CE.CEM_ENTITY_ID='" + ddlEntityType.SelectedValue + "' and CE.OCE_ID=E.ENTITY_ID  " + empcode +
                             "group by E.ENTITY_ID,CE.OCE_DESCRIPTION,E.AL_ID ,A.AL_DESCRIPTION,E.ENTITY_TYPE ";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ///////////Started///////////
                DataTable thisDataSet = new DataTable(); ;
                DataTable temp = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    string levelId = Session["levelId"].ToString();

                    SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                    thisDataSet = new DataTable();
                    adpt.Fill(thisDataSet);

                    IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
                                                  join comwise in thisDataSet.AsEnumerable() on acs.Field<string>("ID") equals comwise.Field<string>("EOD_EMPID")
                                                  select acs
                                 );
                    if (Session["uid"].ToString().ToLower() == "admin")
                    {
                        temp = dt;
                    }
                    else
                    {
                        temp = drRow.CopyToDataTable();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    
                }
                ///////////end////////////
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtEntityName.Text.ToString() == "" && txtLevelDescription.Text.ToString() == "")
                {
                    gvEmployeeAccess.DataSource = temp;
                    gvEmployeeAccess.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"DES~" +txtEntityName.Text.Trim(), "S" },
				{"AL_DESCRIPTION~" +txtLevelDescription.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, temp);
                    gvEmployeeAccess.DataSource = _tempDT;
                    gvEmployeeAccess.DataBind();
                }

                DropDownList ddl = (DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmployeeAccess.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmployeeAccess.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmployeeAccess.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmployeeAccess.PageCount == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex + 1 == gvEmployeeAccess.PageCount)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1));

                ((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (((gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1)) - 10) + gvEmployeeAccess.Rows.Count);

                gvEmployeeAccess.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = string.Empty;
            search();

        }

    }
}
