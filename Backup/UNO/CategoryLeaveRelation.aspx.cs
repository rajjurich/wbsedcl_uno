using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace UNO
{
    public partial class CategoryLeaveRelation : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDataGrid();
            }
        }

        private void bindDataGrid()
        {
            try
            {
                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                strsql = "SELECT ELR_REC_ID,ELR_LEAVE_ID,ELR_ENTITY_ID FROM dbo.TA_Entity_Leave_Rule where ELR_ISDELETED=0";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                gvCategoryLeave.DataSource = dt;
                gvCategoryLeave.DataBind();

                DropDownList ddl = (DropDownList)gvCategoryLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCategoryLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCategoryLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCategoryLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvCategoryLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvCategoryLeave.PageCount == 0)
                {
                    ((Button)gvCategoryLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCategoryLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategoryLeave.PageIndex + 1 == gvCategoryLeave.PageCount)
                {
                    ((Button)gvCategoryLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCategoryLeave.PageIndex == 0)
                {
                    ((Button)gvCategoryLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvCategoryLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCategoryLeave.PageSize * gvCategoryLeave.PageIndex) + 1) + " to " + (gvCategoryLeave.PageSize * (gvCategoryLeave.PageIndex + 1));

                gvCategoryLeave.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CategoryLeaveRelation");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCategoryLeave.PageIndex = Convert.ToInt32(((DropDownList)gvCategoryLeave.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCategoryLeave.PageIndex = gvCategoryLeave.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCategoryLeave.PageIndex = gvCategoryLeave.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CompanyMasterView");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

    }
}