using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace UNO
{
    public partial class EnrollmentView : System.Web.UI.Page
    {
        public static DataTable _baseDT = new DataTable();
        public static DataTable _tempDT = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            _tempDT.Rows.Clear();
            _baseDT.Rows.Clear();

            try
            {
                string strsql = "";
                if (ddlType.SelectedIndex == 0)
                {
                     strsql = " SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, " +
                              " E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION, CASE  CARDISSUE WHEN '0' THEN 'Enrolled' "+
                              " WHEN '1' THEN 'Card Issue' end as Status,format_type FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1, "+
                              " ENT_EMPLOYEE_PERSONAL_DTLS left outer join finger_template ON ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID= FINGER_TEMPLATE.EMPLOYEECD "+
                              " WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID "+
                              " AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' AND EOD_ISDELETED = '0' AND EOD_ACTIVE = '1'  " +
                              " AND ISDELETED ='0' UNION SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME "+
                              " AS EPD_EMP_NAME, E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION,'Not Enrolled' as status,'' as formattype FROM ENT_EMPLOYEE_OFFICIAL_DTLS , "+
                              " ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS LEFT OUTER JOIN FINGER_TEMPLATE ON EPD_EMPID = EMPLOYEECD "+
                              " WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' "+
                              " AND EPD_EMPID NOT IN (SELECT EMPLOYEECD FROM FINGER_TEMPLATE WHERE ISDELETED = '0') AND EOD_ACTIVE = '1'  ";
                }
                else if (ddlType.SelectedIndex == 1)
                {
                    strsql = " SELECT distinct EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, "+ 
                            " E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION,'Not Enrolled' as status,'' as formattype "+ 
                            " FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS "+
                            " LEFT OUTER JOIN FINGER_TEMPLATE ON EPD_EMPID = EMPLOYEECD WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID "+
                            " AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' and EPD_ISDELETED='0' "+
                            " AND EPD_EMPID NOT IN (SELECT EMPLOYEECD FROM FINGER_TEMPLATE where isdeleted = '0') AND EOD_ACTIVE='1' "; 
                }
                else if (ddlType.SelectedIndex == 2)
                {
                    strsql = " SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, "+
                             "  E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION, "+
                             "  CASE  CARDISSUE WHEN '0' THEN 'Enrolled' END AS STATUS,format_type "+
                             "  FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS "+
                             "  left outer join finger_template ON ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID= FINGER_TEMPLATE.EMPLOYEECD "+
                             "  WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID "+
                             "  AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' and EPD_ISDELETED='0' "+
                             "  AND CARDISSUE = 'FALSE' AND EOD_ISDELETED = 'FALSE' AND EOD_ACTIVE='1'";
                }


                SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(_baseDT);

                if (_baseDT.Rows.Count != 0)
                {
                    _tempDT = _baseDT.Clone();
                    foreach (DataRow row in _baseDT.Rows)
                    {
                        _tempDT.ImportRow(row);
                    }

                    gvEmployee.DataSource = _baseDT;
                    gvEmployee.DataBind();
                    DropDownList ddl = (DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmployee.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmployee.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmployee.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmployee.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmployee.PageCount == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex + 1 == gvEmployee.PageCount)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (gvEmployee.PageSize * (gvEmployee.PageIndex + 1));

                    ((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (((gvEmployee.PageSize * (gvEmployee.PageIndex + 1)) - 10) + gvEmployee.Rows.Count);

                    gvEmployee.BottomPagerRow.Visible = true;
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }
                else
                {
                    gvEmployee.DataSource = null;
                    gvEmployee.DataBind();

                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                 
                    CheckBox chkApprove = (CheckBox)e.Row.FindControl("DeleteRows");

                    if ((e.Row.Cells[7].Text == "Enrolled"))
                    {

                        chkApprove.Enabled = true;
                    }
                    else
                    {
                        chkApprove.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EnrollmentView");
            }


        }
        protected void Grid_DataBound(object sender, EventArgs e)
        {
            string strsql = "";
            //string strsql = "SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME," +
            //                    "E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION, CASE  CARDISSUE WHEN '0' THEN 'Enrolled' " +
            //                    "WHEN '1' THEN 'Card Issue' end as Status FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1," +
            //                    "ENT_EMPLOYEE_PERSONAL_DTLS left outer join finger_template ON ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID= FINGER_TEMPLATE.EMPLOYEECD " +
            //                    "WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID AND EOD_ISDELETED = 'FALSE' " +
            //                    "AND ISDELETED ='FALSE' UNION SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME " +
            //                    "AS EPD_EMP_NAME, E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION,'Not Enrolled' as status FROM ENT_EMPLOYEE_OFFICIAL_DTLS ," +
            //                    "ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS LEFT OUTER JOIN FINGER_TEMPLATE ON EPD_EMPID = EMPLOYEECD " +
            //                    "WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID " +
            //                    "AND EPD_EMPID NOT IN (SELECT EMPLOYEECD FROM FINGER_TEMPLATE WHERE ISDELETED = 'FALSE') AND EOD_ISDELETED = 'FALSE' ";

            if (ddlType.SelectedIndex == 0)
            {
                strsql = " SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, " +
                              " E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION, CASE  CARDISSUE WHEN '0' THEN 'Enrolled' " +
                              " WHEN '1' THEN 'Card Issue' end as Status,format_type FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1, " +
                              " ENT_EMPLOYEE_PERSONAL_DTLS left outer join finger_template ON ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID= FINGER_TEMPLATE.EMPLOYEECD " +
                              " WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID " +
                              " AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' AND EOD_ISDELETED = '0' AND EOD_ACTIVE = '1'  " +
                              " AND ISDELETED ='0' UNION SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME " +
                              " AS EPD_EMP_NAME, E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION,'Not Enrolled' as status,'' as formattype FROM ENT_EMPLOYEE_OFFICIAL_DTLS , " +
                              " ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS LEFT OUTER JOIN FINGER_TEMPLATE ON EPD_EMPID = EMPLOYEECD " +
                              " WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' " +
                              " AND EPD_EMPID NOT IN (SELECT EMPLOYEECD FROM FINGER_TEMPLATE WHERE ISDELETED = '0') AND EOD_ACTIVE = '1'  ";
            }
            else if (ddlType.SelectedIndex == 1)
            {
                strsql = " SELECT distinct EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, " +
                            " E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION,'Not Enrolled' as status,'' as formattype " +
                            " FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS " +
                            " LEFT OUTER JOIN FINGER_TEMPLATE ON EPD_EMPID = EMPLOYEECD WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID " +
                            " AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' and EPD_ISDELETED='0' " +
                            " AND EPD_EMPID NOT IN (SELECT EMPLOYEECD FROM FINGER_TEMPLATE where isdeleted = '0') AND EOD_ACTIVE='1' "; 
            }
            else if (ddlType.SelectedIndex == 2)
            {
                strsql = " SELECT EPD_EMPID,EPD_CARD_ID,EPD_FIRST_NAME + EPD_MIDDLE_NAME + EPD_LAST_NAME AS EPD_EMP_NAME, " +
                             "  E.OCE_DESCRIPTION  as DEPARTMENT,E1.OCE_DESCRIPTION  as DESIGNATION, " +
                             "  CASE  CARDISSUE WHEN '0' THEN 'Enrolled' END AS STATUS,format_type " +
                             "  FROM ENT_EMPLOYEE_OFFICIAL_DTLS ,ENT_ORG_COMMON_ENTITIES E,ENT_ORG_COMMON_ENTITIES E1,ENT_EMPLOYEE_PERSONAL_DTLS " +
                             "  left outer join finger_template ON ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID= FINGER_TEMPLATE.EMPLOYEECD " +
                             "  WHERE EPD_EMPID = EOD_EMPID AND E.OCE_ID = EOD_DEPARTMENT_ID AND E1.OCE_ID = EOD_DESIGNATION_ID " +
                             "  AND E.CEM_ENTITY_ID = 'DEP' AND E1.CEM_ENTITY_ID = 'DES' and EPD_ISDELETED='0' " +
                             "  AND CARDISSUE = 'FALSE' AND EOD_ISDELETED = 'FALSE' AND EOD_ACTIVE='1'";
            }

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;

            if (gvEmployee.Rows.Count > 0)
            {
                //pager.Visible = true;

                //if (ViewState["PageSize"] != null)
                //{
                //    DropDownList ddPagesize = gvEmployee.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                //    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                //}

                //Label lblCount = gvEmployee.BottomPagerRow.FindControl("lblPageCount") as Label;
                //int totRecords = (gvEmployee.PageIndex * gvEmployee.PageSize) + gvEmployee.PageSize;
                ////int totCustomerCount = AdvWorksDB.GetCustomersCount(hfSearchCriteria.Value);
                //int totCustomerCount = recordcnt;
                //totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                //lblCount.Text = ((gvEmployee.PageIndex * gvEmployee.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totCustomerCount.ToString();
                //gvEmployee.BottomPagerRow.Visible = true;

            }
            else
            {
                //pager.Visible = false;
            }
         
        }        

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // handle event          
            //DropDownList ddpagesize = sender as DropDownList;
            //gvEmployee.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            //ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            //BindGrid();
            gvEmployee.PageIndex = e.NewPageIndex;
            gvEmployee.DataSource = _tempDT;
            gvEmployee.DataBind();
        }

        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvEmployee.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            BindGrid();
        }

        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployee.EditIndex = e.NewEditIndex;
            Session["EmpCd"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[2].Text;
            Session["CardCd"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[3].Text.Replace("&nbsp;","");
            Session["EmpName"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[4].Text;
            Session["Department"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[5].Text;
            Session["Designation"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[6].Text;
            Session["Status"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[7].Text;
            Session["FormatType"] = gvEmployee.Rows[gvEmployee.EditIndex].Cells[8].Text;
           
            //comment by kapil for releasing of only time attnd module.
            //clsCardRW.Execute = false;
            //Response.Redirect("EmployeeEnrollment.aspx");
            Response.Redirect("FingerEnrollment.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmployee.Rows.Count; i++)
            {
                SqlConnection conn = new SqlConnection(AccessController.m_connecton);
                conn.Open();
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;               
                try
                {                    
                    CheckBox delrows = (CheckBox)gvEmployee.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        objcmd.CommandText = "Update Finger_Template set ISDELETED = 'true',DELETEDDATE = convert(datetime,GetDate(),103) where EmployeeCD = '" + gvEmployee.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();                     
                        
                        this.messageDiv.InnerHtml = "Record Deleted Successfully.";
                        string someScript1 = "";
                        someScript1 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), "onload", someScript1);
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {                   
                    throw ex;
                }

            }
            BindGrid();         
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtemp_code.Text.ToString() == "" && txtemp_name.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"EPD_EMPID~" +txtemp_code.Text.Trim(), "S" },
				{"EPD_EMP_NAME~" +txtemp_name.Text.Trim(), "S" }			
				 };

                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, _baseDT);
                gvEmployee.DataSource = _tempDT;
                gvEmployee.DataBind();
                if (_tempDT.Rows.Count != 0)
                {
                    DropDownList ddl = (DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmployee.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmployee.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmployee.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmployee.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmployee.PageCount == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex + 1 == gvEmployee.PageCount)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmployee.PageIndex == 0)
                    {
                        ((Button)gvEmployee.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (gvEmployee.PageSize * (gvEmployee.PageIndex + 1));

                    ((Label)gvEmployee.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployee.PageSize * gvEmployee.PageIndex) + 1) + " to " + (((gvEmployee.PageSize * (gvEmployee.PageIndex + 1)) - 10) + gvEmployee.Rows.Count);

                    gvEmployee.BottomPagerRow.Visible = true;
                }
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtemp_code.Text = "";
            txtemp_name.Text = "";
            BindGrid();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = Convert.ToInt32(((DropDownList)gvEmployee.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindGrid();
                //gvEmployee.DataSource = _tempDT;
                //gvEmployee.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex - 1;
                BindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex + 1;
                BindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_LV_View");
            }
        }

       
    }
}