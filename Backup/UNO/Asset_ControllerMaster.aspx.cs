using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;

namespace UNO
{
    public partial class Asset_ControllerMaster : System.Web.UI.Page
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
            
        }
        private void bindDataGrid()
        {
            try
            {
                DataSet ds = null;
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "0");
                ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);
                if (ds != null)
                {
                    ddlLocation.DataSource = ds.Tables[1];
                    ddlLocation.DataTextField = "OCE_DESCRIPTION";
                    ddlLocation.DataValueField = "OCE_ID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, "Select Location");
                    ddlEditLocation.DataSource = ds.Tables[1];
                    ddlEditLocation.DataTextField = "OCE_DESCRIPTION";
                    ddlEditLocation.DataValueField = "OCE_ID";
                    ddlEditLocation.DataBind();
                    ddlEditLocation.Items.Insert(0, "Select Location");

                    if (txtControllerCode.Text.ToString() == "" && txtControllerDescription.Text.ToString() == "")
                    {
                        gvController.DataSource = ds.Tables[0];
                        gvController.DataBind();

                    }
                    else
                    {
                        DataTable _tempDT = new DataTable();
                        String[,] values = { 
				                                {"CtlrCode~" +txtControllerCode.Text.Trim(), "S" },
				                                {"CtlrDesc~" +txtControllerDescription.Text.Trim(), "S" }			
				                           };
                        Search _sc = new Search();
                        if (_tempDT != null)
                        { _tempDT.Rows.Clear(); }
                        _tempDT = _sc.searchTable(values, ds.Tables[0]);
                        gvController.DataSource = _tempDT;
                        gvController.DataBind();
                    }
                    if (gvController.Rows.Count > 0)
                    {
                        DropDownList ddl = (DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo");
                        for (int i = 1; i <= gvController.PageCount; i++)
                        {
                            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                        ddl.SelectedValue = (gvController.PageIndex + 1).ToString();
                        Label lblcount = (Label)gvController.BottomPagerRow.FindControl("lblTotal");
                        lblcount.Text = ((DataTable)gvController.DataSource).Rows.Count.ToString() + " Records.";
                        if (gvController.PageCount == 0)
                        {
                            ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                            ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvController.PageIndex + 1 == gvController.PageCount)
                        {
                            ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                        }
                        if (gvController.PageIndex == 0)
                        {
                            ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        }
                        ((Label)gvController.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvController.PageSize * gvController.PageIndex) + 1) + " to " + (gvController.PageSize * (gvController.PageIndex + 1));

                        gvController.BottomPagerRow.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }

        }
        protected void gvController_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                ViewState["RowId"] = e.CommandArgument.ToString();
                fillModifydata(e.CommandArgument.ToString());
                lblMessages.Text = "";
                lblPnlNew.Text = "";
                //ResetEditControl();
                lblMessages.Text = "";
                Label2.Text = "";
                mpeEdit.Show();
            }

        }
        private void fillModifydata(string Rowid)
        {
            DataSet ds = null;
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "4");
            sd.Add("@CtlrID", Rowid);
            ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblEditControllerCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["CtlrCode"]);
                    txtEditControllerDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["CtlrDesc"]);
                    txtEditControllerIP.Text = Convert.ToString(ds.Tables[0].Rows[0]["CtlrIP"]);
                    ddlEditLocation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LocationID"]);
                }

            }
        }
        protected void gvController_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdn = (HiddenField)e.Row.FindControl("hdnIsDelete");
                    CheckBox chk = (CheckBox)e.Row.FindControl("DeleteRows");

                    if (hdn.Value == "1")
                    {
                        chk.Enabled = false;
                    }
                    else
                    {
                        chk.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetNewControl();
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
                sd.Add("@CtlrCode", txtNewControllerCode.Text.Trim());
                sd.Add("@CtlrDesc", txtNewControllerDescription.Text.Trim());
                sd.Add("@CtlrIP", txtNewControllerIP.Text.Trim());
                sd.Add("@LocationID", ddlLocation.SelectedValue.ToString());
                sd.Add("@userId", userid);
                DataSet ds= ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);
                if (IsEmpty(ds))
                {
                    lblPnlNew.Text = "Record Saved Successfully";
                    ResetNewControl();
                    bindDataGrid();
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    lblPnlNew.Text = ds.Tables[0].Rows[0][0].ToString() ;
                }
                else
                {
                    lblPnlNew.Text = "Server Error";
                    ResetNewControl();
                    bindDataGrid();
                }
                mpeAddCall.Show();
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
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StringDictionary sd = new StringDictionary();
                sd.Add("@cmd", "2");
                sd.Add("@CtlrID", ViewState["RowId"].ToString());
                sd.Add("@CtlrDesc", txtEditControllerDescription.Text.Trim());
                sd.Add("@CtlrIP", txtEditControllerIP.Text.Trim());
                sd.Add("@LocationID", ddlEditLocation.SelectedValue.ToString());
                sd.Add("@userId", userid);
                DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string msg=ds.Tables[0].Rows[0][0].ToString();

                    if (msg == "Record Updated Successfully")
                    {
                        lblMessages.Text = msg;
                        ResetEditControl();
                        bindDataGrid();
                        mpeEdit.Hide();
                    }
                    else
                    {
                        Label2.Text = msg;
                        mpeEdit.Show();
                    }
                }
                else
                    lblMessages.Text = "Server Error";

                
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                mpeEdit.Hide();
            }
        }
        private void ResetEditControl()
        {
            txtEditControllerDescription.Text = "";
            txtEditControllerIP.Text = "";
            ddlEditLocation.ClearSelection();
        }
        protected void btnCancelNew_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEdit.Hide();
        }
        private void ResetNewControl()
        {
            txtNewControllerCode.Text = "";
            txtNewControllerDescription.Text = "";
            txtNewControllerIP.Text = "";
            ddlLocation.ClearSelection();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < gvController.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox delrows = (CheckBox)gvController.Rows[i].FindControl("DeleteRows");
                        Label lblRowId = (Label)gvController.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }
                            StringDictionary sd = new StringDictionary();
                            sd.Add("@cmd", "3");
                            sd.Add("@CtlrID", lblRowId.Text);
                            ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);
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
        protected void gvNext(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                gvController.PageIndex = gvController.PageIndex + 1;
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
                gvController.PageIndex = gvController.PageIndex - 1;
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
                gvController.PageIndex = Convert.ToInt32(((DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
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