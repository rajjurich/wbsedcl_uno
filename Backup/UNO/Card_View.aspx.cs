using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

namespace UNO
{
    public partial class Card_View : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string EmpId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null || Convert.ToString(Session["uid"]) == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            EmpId = Convert.ToString(Session["uid"]);
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                BindDropDown();
            }
            hfEmpCode.Value = EmpId;
            hfconnection.Value = ConfigurationManager.ConnectionStrings["connection_string"].ToString().Replace(' ',',');
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //mpeNew.Show();
            //  Response.Redirect("Card_Design.aspx",true);
            bindDataGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindDataGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool Check = false;
            for (int i = 0; i < gvCardView.Rows.Count; i++)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                try
                {

                    CheckBox delrows = (CheckBox)gvCardView.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        Label lblId = (Label)gvCardView.Rows[i].FindControl("lblId");
                        string swipedt = gvCardView.Rows[i].Cells[2].Text;
                        String SelStr = "EXEC USP_CardTemplate @strCommand='Delete', @intId='" + lblId.Text.Trim() + "' ";
                        SqlCommand cmd = new SqlCommand(SelStr, conn);
                        cmd.ExecuteNonQuery();

                        Check = true;
                    }

                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
                }

            }
            conn.Close();
            if (Check == true)
            {
                lblMessages.Visible = true;
                lblMessages.Text = "Records Deleted Successfully.";

            }

            bindDataGrid();
        }

        protected void gvCardView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCardView.PageIndex = e.NewPageIndex;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
            }
        }

        protected void gvCardView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    if (e.CommandName == "Modify")
            //    {
            //        string cardId = e.CommandArgument.ToString();
            //        Response.Redirect("Card_Design.aspx?id=" + cardId + "", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
            //}
            bindDataGrid();
        }
        protected void bindDataGrid()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_CARD_VIEW",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@strCommand", "SelectAll");
                cmd.Parameters.Add("@nvarUserId", EmpId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsTemplate = new DataSet();
                da.Fill(dsTemplate);
                conn.Close();
            

                //drpCategory.DataSource = dsTemplate.Tables[1];
                //drpCategory.DataTextField = "OCE_DESCRIPTION";
                //drpCategory.DataValueField = "OCE_ID";
                //drpCategory.DataBind();
                if (txtCtlrID.Text.ToString() == "" && txtCtlrDesc.Text.ToString() == "")
                {
                    gvCardView.DataSource = dsTemplate.Tables[0];
                    gvCardView.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"OCE_DESCRIPTION~" +txtCtlrID.Text.Trim(), "S" },
				{"nvarTemplateName~" +txtCtlrDesc.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dsTemplate.Tables[0]);
                    gvCardView.DataSource = _tempDT;
                    gvCardView.DataBind();
                }
                if (gvCardView.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)gvCardView.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvCardView.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvCardView.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvCardView.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvCardView.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvCardView.PageCount == 0)
                    {
                        ((Button)gvCardView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvCardView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCardView.PageIndex + 1 == gvCardView.PageCount)
                    {
                        ((Button)gvCardView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCardView.PageIndex == 0)
                    {
                        ((Button)gvCardView.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvCardView.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCardView.PageSize * gvCardView.PageIndex) + 1) + " to " + (gvCardView.PageSize * (gvCardView.PageIndex + 1));

                    gvCardView.BottomPagerRow.Visible = true;
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");

            }

        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCardView.PageIndex = gvCardView.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCardView.PageIndex = gvCardView.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCardView.PageIndex = Convert.ToInt32(((DropDownList)gvCardView.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Card_View");
            }
        }
     

        protected void gvCardView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblId = (Label)e.Row.FindControl("lblId");
              
                Label lblTempName = (Label)e.Row.FindControl("lblTemplate");
                Label lblCatId = (Label)e.Row.FindControl("lblCategoryId");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkEdit.Attributes.Add("onclick", "return CallExe('" + lblId.Text.Trim() + "','" + lblTempName.Text.Trim().Replace(' ',',') + "','" + lblCatId.Text.Trim() + "');");
                lblId.Visible = false;
                lblCatId.Visible = false;

            }
        }

        protected void ddlIssuingAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (ddlIssuingAuth.SelectedValue != "0")
            {
                SqlCommand cmd = new SqlCommand("EXEC USP_CardTemplate @strCommand ='InsertIssuingAuth', @nvarUserId='" + ddlIssuingAuth.SelectedValue + "'", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT CODE,VALUE FROM ENT_PARAMS WHERE MODULE='Perso' AND IDENTIFIER='IssuingAuthority'", conn);
                DataSet dsData1 = new DataSet();
                da1.Fill(dsData1);
                if (dsData1.Tables[0].Rows.Count > 0)
                {
                    lblCode.Text = Convert.ToString(dsData1.Tables[0].Rows[0]["CODE"]);
                    lblName.Text = Convert.ToString(dsData1.Tables[0].Rows[0]["VALUE"]);
                }
            }
            conn.Close();
        }
        protected void BindDropDown()
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter("EXEC USP_IssuingAuth @strCommand='getEmp'", conn);
            DataSet dsData = new DataSet();
            da.Fill(dsData);
            DataRow dr = dsData.Tables[0].NewRow();
            dr["EPD_EMPID"] = "0";
            dr["EMPNAME"] = "--Select Issuing Authority--";
            dsData.Tables[0].Rows.InsertAt(dr, 0);
            ddlIssuingAuth.DataSource = dsData.Tables[0];
            ddlIssuingAuth.DataValueField = "EPD_EMPID";
            ddlIssuingAuth.DataTextField = "EMPNAME";
            ddlIssuingAuth.DataBind();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT CODE,VALUE FROM ENT_PARAMS WHERE MODULE='Perso' AND IDENTIFIER='IssuingAuthority'", conn);
            DataSet dsData1 = new DataSet();
            da1.Fill(dsData1);
            if (dsData1.Tables[0].Rows.Count > 0)
            {
                lblCode.Text = Convert.ToString(dsData1.Tables[0].Rows[0]["CODE"]);
                lblName.Text = Convert.ToString(dsData1.Tables[0].Rows[0]["VALUE"]);
                ddlIssuingAuth.SelectedValue = lblCode.Text;
            }
        
            conn.Close();
        }


    }
}