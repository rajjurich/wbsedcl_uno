using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace UNO
{
    public partial class Asset_Master_Entry : System.Web.UI.Page
    {
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + grdAsset.ClientID + "');");
            }
            if (!IsPostBack)
            {
                bindDataGrid();
            }
        }
        public void bindDataGrid()
        {
            DataSet ds;
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "0");
            ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_MASTER", sd);
            if (ds.Tables.Count > 0)
            {
                if (txtAssetCode.Text.ToString() == "" && txtAssetDescription.Text.ToString() == "")
                {
                    grdAsset.DataSource = ds.Tables[0];
                    grdAsset.DataBind();
                }
                else
                {
                    DataTable _tempDT = new DataTable();
                    String[,] values = { 
				                                {"Asset_Code~" +txtAssetCode.Text.Trim(), "S" },
				                                {"Asset_Desc~" +txtAssetDescription.Text.Trim(), "S" }			
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

                ddlAstType.DataSource = ds.Tables[1];
                ddlAstType.DataValueField = "CODE";
                ddlAstType.DataTextField = "VALUE";
                ddlAstType.DataBind();
                ddlAstType.Items.Insert(0, new ListItem("Select Type", "0"));
                ddlAstType.SelectedIndex = 0;
                ddlEditType.DataSource = ds.Tables[1];
                ddlEditType.DataValueField = "CODE";
                ddlEditType.DataTextField = "VALUE";
                ddlEditType.DataBind();
                ddlEditType.Items.Insert(0, new ListItem("Select Type", "0"));
            }
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
                            sd.Add("@Asset_Id", lblRowId.Text);
                            ExecuteSQL.ExecuteDataSet("PROC_ASSET_MASTER", sd);
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            bindDataGrid();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            lblPnlNew.Text = "";
            lblMessages.Text = "";
            mpeAddCall.Show();
        }
        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "1");
                sd.Add("@Asset_Code", txtAstCode.Text.Trim());
                sd.Add("@Asset_Desc", txtAstDescription.Text.Trim());
                sd.Add("@Asset_Type", ddlAstType.SelectedValue);
                sd.Add("@Asset_SrNo", txtAstSrNo.Text);
                sd.Add("@Asset_Make", txtAstMake.Text);
                sd.Add("@Asset_Model", txtAstModel.Text);
                sd.Add("@userId", userid);
                DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_MASTER", sd);
                if (IsEmpty(ds))
                {
                    lblPnlNew.Text = "Record Saved Successfully";
                    ResetNewControl();
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0][0].ToString()=="0")
                        lblPnlNew.Text = "Sr No. Already Exists";
                    else
                        lblPnlNew.Text = "Asset Code Already Exists";
                }
                else
                {
                    lblPnlNew.Text = "Server Error";
                }
                bindDataGrid();
            }
            catch (Exception ex)
            {
                lblPnlNew.Text = ex.Message;
            }
        }
        bool IsEmpty(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
                if (table.Rows.Count != 0) return false;

            return true;
        }
        protected void btnCancelNew_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "2");
                sd.Add("@Asset_Id", ViewState["RowId"].ToString());
                sd.Add("@Asset_Desc", txtEditDescription.Text.Trim());
                sd.Add("@Asset_Type", ddlEditType.SelectedValue);
                sd.Add("@Asset_SrNo", txtEditSerialNo .Text);
                sd.Add("@Asset_Make", txtEditMake.Text);
                sd.Add("@Asset_Model", txtEditModel.Text);
                sd.Add("@userId", userid);
                ExecuteSQL.ExecuteDataSet("PROC_ASSET_MASTER", sd);
                lblMessages.Text = "Record Updated Successfully";

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
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEdit.Hide();
        }
        protected void grdAsset_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                ViewState["RowId"] = e.CommandArgument.ToString();
                ResetEditControl();
                fillModifydata(e.CommandArgument.ToString());
                lblMessages.Text = "";
                lblPnlNew.Text = "";
                lblMessages.Text = "";
                mpeEdit.Show();
            }
        }
        private void fillModifydata(string Rowid)
        {
            DataSet ds = null;
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "4");
            sd.Add("@Asset_Id", Rowid);
            ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_MASTER", sd);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblAssetCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Asset_Code"]);
                    txtEditDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Asset_Desc"]);
                    txtEditSerialNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Asset_SrNo"]);
                    ddlEditType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Asset_Type"]);
                    txtEditMake.Text = Convert.ToString(ds.Tables[0].Rows[0]["Asset_Make"]);
                    txtEditModel.Text = Convert.ToString(ds.Tables[0].Rows[0]["Asset_Model"]);
                }
            }
        }
        private void ResetEditControl()
        {
            txtEditDescription.Text = "";
            txtEditSerialNo.Text = "";
            ddlEditType.ClearSelection();
            txtEditMake.Text = "";
            txtEditModel.Text = "";
        }
        private void ResetNewControl()
        {
            txtAstCode.Text = "";
            txtAstDescription.Text = "";
            txtAstSrNo.Text = "";
            txtAstMake.Text = "";
            txtAstModel.Text = "";
            ddlAstType.ClearSelection();
        }
        protected void grdAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdn = (HiddenField)e.Row.FindControl("hdnIsDelete");
                    CheckBox lnkedit = (CheckBox)e.Row.FindControl("DeleteRows");

                    if (hdn.Value=="1")
                    {
                        lnkedit.Enabled = false;
                    }
                    else
                    {
                        lnkedit.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}