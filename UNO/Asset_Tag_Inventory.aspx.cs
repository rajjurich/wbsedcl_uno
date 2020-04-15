using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Web.Services;
using System.Data;

namespace UNO
{
    public partial class Asset_Tag_Inventory : System.Web.UI.Page
    {
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }
            if (!IsPostBack)
            {
                bindDataGrid();
            }
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + grdAsset.ClientID + "');");
            }
        }
        public void bindDataGrid()
        {
            try
            {
                DataSet ds;
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "0");
                ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                if (ds.Tables.Count > 0)
                {
                    if (txtTagNo.Text.ToString() == "" && txtTagStatus.Text.ToString() == "")
                    {
                        grdAsset.DataSource = ds.Tables[0];
                        grdAsset.DataBind();
                    }
                    else
                    {
                        DataTable _tempDT = new DataTable();
                        String[,] values = { 
				                                {"TagEPCID~" +txtTagNo.Text.Trim(), "S" },
				                                {"CurStatus~" +txtTagStatus.Text.Trim(), "S" }			
				                           };
                        Search _sc = new Search();
                        if (_tempDT != null)
                        { _tempDT.Rows.Clear(); }
                        _tempDT = _sc.searchTable(values, ds.Tables[0]);
                        grdAsset.DataSource = _tempDT;
                        grdAsset.DataBind();
                    }
                    if (grdAsset.Rows.Count > 0)
                    {
                        DropDownList ddl = (DropDownList)grdAsset.BottomPagerRow.FindControl("ddlPageNo");
                        for (int i = 1; i <= grdAsset.PageCount; i++)
                        {
                            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                        ddl.SelectedValue = (grdAsset.PageIndex + 1).ToString();
                        Label lblcount = (Label)grdAsset.BottomPagerRow.FindControl("lblTotal");
                        lblcount.Text = ((DataTable)grdAsset.DataSource).Rows.Count.ToString() + " Records.";
                        if (grdAsset.PageCount == 0)
                        {
                            ((Button)grdAsset.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                            ((Button)grdAsset.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (grdAsset.PageIndex + 1 == grdAsset.PageCount)
                        {
                            ((Button)grdAsset.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (grdAsset.PageIndex == 0)
                        {
                            ((Button)grdAsset.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        }
                        ((Label)grdAsset.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((grdAsset.PageSize * grdAsset.PageIndex) + 1) + " to " + (grdAsset.PageSize * (grdAsset.PageIndex + 1));

                        grdAsset.BottomPagerRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        [WebMethod]
        public static string SaveData(string Tags)
        {
            string userid = Convert.ToString(HttpContext.Current.Session["uid"]);
            string Result = "";
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "1");
            sd.Add("@TagEPCID", Tags);
            sd.Add("@userId", userid);
            DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
            if (ds.Tables.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Result = Convert.ToString(ds.Tables[0].Rows[0][0]) + " already exists";
                }
                else
                {
                    Result = "Record Saved Successfully";
                }
            }
            return Result;
        }
        protected void btnSaveTags_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == false)
                return;
            lblPnlNew.Text = "";
            try
            {
                if (txtToTag.Text == "")
                {
                    txtToTag.Text = txtFromTag.Text;
                }

                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "4");
                sd.Add("@FromTag", txtFromTag.Text);
                sd.Add("@ToTag", txtToTag.Text);
                sd.Add("@userId", userid);
                DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0][0].ToString()!="")
                    {
                        lblPnlNew.Text = ds.Tables[1].Rows[0][0].ToString();
                        txtFromTag.Text = "";
                        txtToTag.Text = "";
                    }
                    else
                    {
                        lblPnlNew.Text = "Already Exists.";
                    }
                }
                else
                {
                    lblPnlNew.Text = "Record Saved Successfully";
                    txtFromTag.Text = "";
                    txtToTag.Text = "";
                }
                bindDataGrid();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "buttonClick();", true);
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEdit.Hide();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            bindDataGrid();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            lblPnlNew.Text = "";
            lblMessages.Text = "";
            txtFromTag.Text = "";
            txtToTag.Text = "";
            LstRFIDList.Items.Clear();
            mpeAddCall.Show();
        }
        protected void btnCancelNew_Click(object sender, EventArgs e)
        {
          
            mpeAddCall.Hide();
        }
        protected void btnHistoryCancel_Click(object sender, EventArgs e)
        {
            mpeHistory.Hide();    
        }
        protected void grdAsset_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    ViewState["RowId"] = e.CommandArgument.ToString();
                    ResetEditControl();
                    fillModifydata(e.CommandArgument.ToString());
                    lblMessages.Text = "";
                    lblPnlNew.Text = "";
                    mpeEdit.Show();
                }
                else if (e.CommandName == "History")
                {
                    ViewState["TagId"] = e.CommandArgument.ToString();
                    grdHistory.PageIndex = 0;
                    GetHistory(e.CommandArgument.ToString());
                    mpeHistory.Show();
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        private void GetHistory(string tagId)
        {
            try
            {
                DataSet ds = null;
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "8");
                sd.Add("@TagEPCID", tagId);
                ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                grdHistory.DataSource = ds.Tables[0];
                grdHistory.DataBind();
                if (grdHistory.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)grdHistory.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= grdHistory.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (grdHistory.PageIndex + 1).ToString();
                    Label lblcount = (Label)grdHistory.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)grdHistory.DataSource).Rows.Count.ToString() + " Records.";
                    if (grdHistory.PageCount == 0)
                    {
                        ((Button)grdHistory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)grdHistory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (grdHistory.PageIndex + 1 == grdHistory.PageCount)
                    {
                        ((Button)grdHistory.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (grdHistory.PageIndex == 0)
                    {
                        ((Button)grdHistory.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)grdHistory.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((grdHistory.PageSize * grdHistory.PageIndex) + 1) + " to " + (grdHistory.PageSize * (grdHistory.PageIndex + 1));

                    grdHistory.BottomPagerRow.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        private void ResetEditControl()
        {
            txtEditEpcid.Text = "";
            lblDate.Text = "";
        }
        private void fillModifydata(string Rowid)
        {
            try
            {
                DataSet ds = null;
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "5");
                sd.Add("@Tag_Id", Rowid);
                ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtEditEpcid.Text = Convert.ToString(ds.Tables[0].Rows[0]["TagEPCID"]);
                        lblDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        protected void grdAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblOldStatus = (Label)e.Row.FindControl("lblOldStatus");
                    Label lblCurStatus = (Label)e.Row.FindControl("lblCurStatus");
                    CheckBox chkDel = (CheckBox)e.Row.FindControl("DeleteRows");
                    LinkButton lnkHistory = (LinkButton)e.Row.FindControl("lnkHistory");
                    if (String.IsNullOrEmpty(lblOldStatus.Text))
                        lnkHistory.Enabled = false;
                    else
                        lnkHistory.Enabled = true;
                    
                    if (lblCurStatus.Text.ToLower() == "in use")
                        chkDel.Enabled = false;
                    else
                        chkDel.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "6");
                sd.Add("@TagEPCID", txtEditEpcid.Text.Trim());
                sd.Add("@userID", userid);
                sd.Add("@Tag_Id", ViewState["RowId"].ToString());
                DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                if (IsEmpty(ds))
                {
                    lblMessages.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessages.Text = "Record Already Exists";
                }
                ResetEditControl();
                bindDataGrid();
                mpeEdit.Hide();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                mpeEdit.Hide();
            }
        }
        bool IsEmpty(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
                if (table.Rows.Count != 0) return false;
            return true;
        }
        protected void gvNext(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdAsset.PageIndex = grdAsset.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdAsset.PageIndex = grdAsset.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdAsset.PageIndex = Convert.ToInt32(((DropDownList)grdAsset.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvNext_History(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdHistory.PageIndex = grdHistory.PageIndex + 1;
                GetHistory(Convert.ToString(ViewState["TagId"]));
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious_History(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdHistory.PageIndex = grdHistory.PageIndex - 1;
                GetHistory(Convert.ToString(ViewState["TagId"]));
            }
            catch (Exception ex)
            {

            }
        }
        protected void ChangePage_History(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                grdHistory.PageIndex = Convert.ToInt32(((DropDownList)grdHistory.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                GetHistory(Convert.ToString(ViewState["TagId"]));
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < grdAsset.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox delrows = (CheckBox)grdAsset.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)grdAsset.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }
                            StringDictionary sd = new StringDictionary();
                            sd.Add("@cmd", "3");
                            sd.Add("@TagEPCID", lblRowId.Text);
                            sd.Add("@userID", userid);
                            ExecuteSQL.ExecuteDataSet("PROC_ASSET_TAG_MASTER", sd);
                            Check = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessages.Text = ex.Message;
                    }
                }
                if (Check == true)
                {
                    lblMessages.Text = "Record Deleted Successfully";

                }
                else if (marked == false)
                {
                    lblMessages.Text = "Please select record to Delete";
                }
                bindDataGrid();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
    }
}