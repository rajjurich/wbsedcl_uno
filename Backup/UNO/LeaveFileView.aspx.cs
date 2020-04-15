using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using CMS.UNO.Tempus.Handler;
using System.Text;

namespace UNO
{
    public partial class LeaveFileView : System.Web.UI.Page
    {       
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLeave.ClientID + "');");
                bindDataGrid();
                intializeControl();
            }
        }

        public void intializeControl()
        {
            cmbPaidLeave.Items.Clear();
            cmbPaidLeave.Items.Add(new ListItem("Select One", "-1"));
            cmbPaidLeave.Items.Add(new ListItem("Yes", "True"));
            cmbPaidLeave.Items.Add(new ListItem("No", "False"));
            cmbPaidLeave.SelectedValue = null;

            cmbModifyPaidLeave.Items.Clear();
            cmbModifyPaidLeave.Items.Add(new ListItem("Select One", "-1"));
            cmbModifyPaidLeave.Items.Add(new ListItem("Yes", "True"));
            cmbModifyPaidLeave.Items.Add(new ListItem("No", "False"));
            cmbModifyPaidLeave.SelectedValue = null;

            try
            {


                DataTable dt = clsCommonHandler.GetEntParameterValues("LEAVE_TYPE", "TA");

                cmbLeaveGroup.DataValueField = "CODE";
                cmbLeaveGroup.DataTextField = "VALUE";
                cmbLeaveGroup.DataSource = dt;
                cmbLeaveGroup.DataBind();
                cmbLeaveGroup.Items.Insert(0, "Select One");

                cmbModifyLeaveGroup.DataValueField = "CODE";
                cmbModifyLeaveGroup.DataTextField = "VALUE";
                cmbModifyLeaveGroup.DataSource = dt;
                cmbModifyLeaveGroup.DataBind();
                cmbModifyLeaveGroup.Items.Insert(0, "Select One");

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }



        }

        void bindDataGrid()
        {
            try
            {

                DataTable dt = clsLeaveFileViewHandler.GetLeaveDetails("All");

                gvLeave.DataSource = dt;
                gvLeave.DataBind();

                DropDownList ddl = (DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeave.PageCount == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex + 1 == gvLeave.PageCount)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeave.PageSize * gvLeave.PageIndex) + 1) + " to " + (((gvLeave.PageSize * (gvLeave.PageIndex + 1)) - 10) + gvLeave.Rows.Count);

                gvLeave.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLeave.PageIndex = e.NewPageIndex;
                bindDataGrid();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = Convert.ToInt32(((DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = gvLeave.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLeave.PageIndex = gvLeave.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        public void ModifyLeaveMaster(string strRowid)
        {
            try
            {
                clsLeaveFileView objData = clsLeaveFileViewHandler.GetLeaveDetails("FillEditData", Convert.ToInt32(strRowid));
             
                if (objData!=null)
                {
                    txtModifyLeaveID.Text = objData.LeaveID;
                    txtModifyLeaveDesc.Text = objData.LeaveDescr;
                    cmbModifyPaidLeave.SelectedValue = Convert.ToString(objData.IsPaid);
                    cmbModifyLeaveGroup.SelectedValue = objData.LeaveGroup;
                }
                else
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = "Records not found";
                }

            }
            catch (Exception ex)
            {             
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                lblEditError.Text = "";
                string Rowid = e.CommandArgument.ToString();
                ViewState["Rowid"] = Rowid;
                ModifyLeaveMaster(Rowid);
                mpeModifyLeave.Show();
            }

        }

        protected void BtnAddSave_Click(object sender, EventArgs e)
        {
            try
            {
                clsLeaveFileView objData = new clsLeaveFileView();
                objData.LeaveID = txtleaveID.Text.Trim();
                objData.LeaveDescr = txtleaveDescription.Text;
                objData.CreatedBy = Session["uid"].ToString();
                objData.IsPaid = Convert.ToBoolean(cmbPaidLeave.SelectedValue);
                objData.LeaveGroup = cmbLeaveGroup.SelectedValue;
                clsLeaveFileViewHandler.UpdateLeaveDetails(objData, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblErrorAdd.Visible = true;
                    lblErrorAdd.Text = strErrMsg;
                    return;
                }
                else
                {
                    mpeAddLeave.Show();
                    lblErrorAdd.Visible = true;
                    lblErrorAdd.Text = strSuccMsg;
                    txtleaveID.Text = "";
                    txtleaveDescription.Text = "";
                    cmbPaidLeave.SelectedIndex = 0;
                    cmbLeaveGroup.SelectedIndex = 0;
                    bindDataGrid();
                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void BtnAddCancel_Click(object sender, EventArgs e)
        {
            mpeAddLeave.Hide();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            mpeAddLeave.Show();
            txtleaveID.Text = "";
            txtleaveDescription.Text = "";
            cmbPaidLeave.SelectedIndex = 0;
            cmbLeaveGroup.SelectedIndex = 0;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            StringBuilder strXML = new StringBuilder();
            bool check = false;
            int count = 0;
            try
            {
                strXML.Append("<TA_Leave_File>");
                for (int i = 0; i < gvLeave.Rows.Count; i++)
                {
                    
                    CheckBox chk = (CheckBox)gvLeave.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        count++;
                        check = true;
                        strXML.Append("<Leave>");
                        strXML.Append("<Leave_ID>" + gvLeave.Rows[i].Cells[2].Text + "</Leave_ID>");
                        strXML.Append("</Leave>");
                    }
                }
                strXML.Append("</TA_Leave_File>");

                if (count > 0)
                {
                    clsLeaveFileView objData = new clsLeaveFileView();
                    objData.LeaveID = txtModifyLeaveID.Text.Trim();
                    objData.LeaveDescr = txtModifyLeaveDesc.Text;
                    objData.CreatedBy = Session["uid"].ToString();
                    objData.IsPaid = Convert.ToBoolean(cmbModifyPaidLeave.SelectedValue);
                    objData.LeaveGroup = cmbModifyLeaveGroup.SelectedValue;
                    clsLeaveFileViewHandler.UpdateLeaveDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblMessages.Visible = true;
                        lblMessages.Text = strErrMsg;
                        return;
                    }
                    else
                    {
                        lblMessages.Visible = true;
                        lblMessages.Text = strSuccMsg;
                        bindDataGrid();
                        return;
                    }
                }
                if (check == false)
                {
                    lblMessages.Text = "Please select record to delete.";
                    lblMessages.Visible = true;
                }
            }
            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsLeaveFileViewHandler.GetLeaveDetails("All");
                if (textLeaveid.Text.ToString() == "" && textLeavename.Text.ToString() == "")
                {
                    gvLeave.DataSource = dt;
                    gvLeave.DataBind();

                }
                else
                {
                    String[,] values = { 
                {"Leave_ID~" +textLeaveid.Text.Trim(), "S" },
                {"Leave_Description~" +textLeavename.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvLeave.DataSource = _tempDT;
                    gvLeave.DataBind();

                }
                DropDownList ddl = (DropDownList)gvLeave.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLeave.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLeave.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLeave.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLeave.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLeave.PageCount == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex + 1 == gvLeave.PageCount)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLeave.PageIndex == 0)
                {
                    ((Button)gvLeave.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvLeave.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLeave.PageSize * gvLeave.PageIndex) + 1) + " to " + (((gvLeave.PageSize * (gvLeave.PageIndex + 1)) - 10) + gvLeave.Rows.Count);
                gvLeave.BottomPagerRow.Visible = true;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }

        protected void btnModifySave_Click(object sender, EventArgs e)
        {

            try
            {
                clsLeaveFileView objData = new clsLeaveFileView();
                objData.LeaveID = txtModifyLeaveID.Text.Trim();
                objData.LeaveDescr = txtModifyLeaveDesc.Text;
                objData.CreatedBy = Session["uid"].ToString();
                objData.IsPaid = Convert.ToBoolean(cmbModifyPaidLeave.SelectedValue);
                objData.LeaveGroup = cmbModifyLeaveGroup.SelectedValue;
                clsLeaveFileViewHandler.UpdateLeaveDetails(objData, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblEditError.Visible = true;
                    lblEditError.Text = strErrMsg;
                    return;
                }
                else
                {
                    mpeModifyLeave.Hide();
                    lblMessages.Visible = true;
                    lblMessages.Text = strSuccMsg;
                    bindDataGrid();
                }

            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnModifyCancel_Click(object sender, EventArgs e)
        {
            mpeModifyLeave.Hide();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (string.Equals(hdnFlag.Value, "A", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChkDelete.Enabled = true;               
                }
                else
                {
                    ChkDelete.Enabled = false;                   
                }
            }
        }

    }
}