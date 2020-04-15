using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CMS.UNO.Tempus.Handler;
using System.Text;

namespace UNO
{

    public partial class ShiftMasterView : System.Web.UI.Page
    {
        public static clsShift _oldshift = new clsShift();
        public static clsShift _newshift = new clsShift();
        static string strSuccMsg, strErrMsg = string.Empty;
        string Entity_Mode = "";
        string ShiftID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvShift();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvShift.ClientID + "');");
            }
        }
        private void BindgvShift()
        {
            try
            {
                DataTable dt = clsShiftViewHandler.GetShiftDetails("All");

                gvShift.DataSource = dt;
                gvShift.DataBind();

                DropDownList ddl = (DropDownList)gvShift.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvShift.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvShift.PageIndex + 1).ToString();
                Label lblcount = (Label)gvShift.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvShift.DataSource).Rows.Count.ToString() + " Records.";
                if (gvShift.PageCount == 0)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvShift.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvShift.PageIndex + 1 == gvShift.PageCount)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvShift.PageIndex == 0)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvShift.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvShift.PageSize * gvShift.PageIndex) + 1) + " to " + (((gvShift.PageSize * (gvShift.PageIndex + 1)) - 10) + gvShift.Rows.Count);
                gvShift.BottomPagerRow.Visible = true;

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
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvShift.PageIndex = Convert.ToInt32(((DropDownList)gvShift.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindgvShift();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvShift.PageIndex = gvShift.PageIndex - 1;
                BindgvShift();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvShift.PageIndex = gvShift.PageIndex + 1;
                BindgvShift();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            flush();
        }
        public void flush()
        {
            try
            {
                Shft_Type.Items.Clear();
                Shft_Type.Items.Add(new ListItem("Select One", "-1"));
                Shft_Type.Items.Add(new ListItem("General", "General"));
                Shft_Type.Items.Add(new ListItem("Morning", "Morning"));
                Shft_Type.Items.Add(new ListItem("Afternoon", "Afternoon"));
                Shft_Type.Items.Add(new ListItem("Night", "Night"));
                Shft_Type.SelectedValue = "-1";
                Shft_id.Enabled = true;
                Shft_Type.Enabled = true;
                lblError.Text = "";
                intializeControl();
                mpeAddEditShift.Show();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        public void intializeControl()
        {
            try
            {
                WShft_Frm.Text = "";
                WShft_To.Text = "";
                WLnch_Frm.Text = "";
                WLnch_To.Text = "";
                WTot_WrKHr.Text = "";
                WChkShft.Checked = false;
                WTot_WrKHr.Text = "";
                WShft_Frm.Enabled = false;
                WShft_To.Enabled = false;
                WLnch_Frm.Enabled = false;
                WLnch_To.Enabled = false;
                WTot_WrKHr.Enabled = false;
                WChkShft.Enabled = false;
                RBShiftALType.SelectedIndex = 0;
                ASStartTime.Enabled = false;
                ASEndTime.Enabled = false;
                Shft_id.Text = "";
                Shft_desc.Text = "";
                RBShiftALType.SelectedIndex = 0;
                ASStartTime.Text = "";
                ASEndTime.Text = "";
                Shft_Type.SelectedIndex = 0;
                Shft_Frm.Text = "";
                Shft_To.Text = "";
                Lnch_Frm.Text = "";
                Lnch_To.Text = "";
                Tot_WrKHr.Text = "";
                ChkShft.Checked = false;
                WChkShft1.Checked = false;
                WShft_Frm.Text = "";
                WShft_To.Text = "";
                WLnch_Frm.Text = "";
                WLnch_To.Text = "";
                EarlySearchHours.Text = "";
                LateSearchHours.Text = "";
                Session.Remove("Mode");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void gvShift_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    lblError.Text = string.Empty;
                    ViewState["PageMode"] = "Modify";
                    ShiftID = e.CommandArgument.ToString();
                    Shft_Type.Items.Clear();
                    Shft_Type.Items.Add(new ListItem("Select One", "-1"));
                    Shft_Type.Items.Add(new ListItem("General", "General"));
                    Shft_Type.Items.Add(new ListItem("Morning", "Morning"));
                    Shft_Type.Items.Add(new ListItem("Afternoon", "Afternoon"));
                    Shft_Type.Items.Add(new ListItem("Night", "Night"));
                    Shft_Type.SelectedValue = "-1";
                    Modify_Data(ShiftID);
                    mpeAddEditShift.Show();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void Modify_Data(string ShiftID)
        {
            try
            {
                Shft_id.Enabled = false;
                _oldshift = clsShiftViewHandler.GetShiftDetails("EditFill", ShiftID);
                if (_oldshift != null)
                {

                    Shft_id.Text = _oldshift.SHIFT_ID;
                    Shft_desc.Text = _oldshift.SHIFT_DESCRIPTION;
                    RBShiftALType.SelectedValue = _oldshift.SHIFT_ALLOCATION_TYPE;
                    if (RBShiftALType.SelectedValue == "AUTO") { ASStartTime.Enabled = true; ASEndTime.Enabled = true; } else { ASStartTime.Enabled = false; ASEndTime.Enabled = false; }
                    ASStartTime.Text = _oldshift.SHIFT_AUTO_SEARCH_START;
                    ASEndTime.Text = _oldshift.SHIFT_AUTO_SEARCH_END;

                    Shft_Type.SelectedValue = _oldshift.SHIFT_TYPE;
                    Shft_Type.Enabled = false;
                    Shft_Frm.Text = _oldshift.SHIFT_START;
                    Shft_To.Text = _oldshift.SHIFT_END;
                    Lnch_Frm.Text = _oldshift.SHIFT_BREAK_START;
                    Lnch_To.Text = _oldshift.SHIFT_BREAK_END;

                    Tot_BrHr.Text = _oldshift.SHIFT_BREAK_HRS;
                    hdnTot_BrHr.Value = _oldshift.SHIFT_BREAK_HRS;

                    Tot_WrKHr.Text = _oldshift.SHIFT_WORKHRS;
                    hdnTot_WrKHr.Value = _oldshift.SHIFT_WORKHRS;

                    if (_oldshift.SHIFT_FLAG_ADD_BREAK == true)
                    { ChkShft.Checked = true; }
                    else
                    {
                        ChkShft.Checked = false;
                    }
                    if (_oldshift.SHIFT_WEEKEND_DIFF_TIME == true)
                    {
                        WChkShft1.Checked = true;
                        WShft_Frm.Enabled = true; WShft_To.Enabled = true; WLnch_Frm.Enabled = true; WLnch_To.Enabled = true;
                    }
                    else
                    {
                        WChkShft1.Checked = false; WShft_Frm.Enabled = false; WShft_To.Enabled = false; WLnch_Frm.Enabled = false; WLnch_To.Enabled = false;
                    }
                    WShft_Frm.Text = _oldshift.SHIFT_WEEKEND_START;
                    WShft_To.Text = _oldshift.SHIFT_WEEKEND_END;
                    WLnch_Frm.Text = _oldshift.SHIFT_WEEKEND_BREAK_START;
                    WLnch_To.Text = _oldshift.SHIFT_WEEKEND_BREAK_END;
                    EarlySearchHours.Text = _oldshift.SHIFT_EARLY_SEARCH_HRS;
                    LateSearchHours.Text = _oldshift.SHIFT_LATE_SEARCH_HRS;
                }
            }
            catch (Exception ex)
            {
             
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        public String getDiffInBreakTime()
        {
            try
            {
                DateTime _dtBrkSTime, _dtBrkETime;
                _dtBrkSTime = new DateTime(1990, 1, 1, Convert.ToInt32(Lnch_Frm.Text.Trim().Substring(0, 2).Trim()), Convert.ToInt32(Lnch_Frm.Text.Trim().Substring(3, 2).Trim()), 0);
                _dtBrkETime = new DateTime(1990, 1, 1, Convert.ToInt32(Lnch_To.Text.Trim().Substring(0, 2).Trim()), Convert.ToInt32(Lnch_To.Text.Trim().Substring(3, 2).Trim()), 0);
                TimeSpan _ts = _dtBrkETime - _dtBrkSTime;
                String _strBrTime = "";
                if (_ts.Hours < 9)
                {
                    _strBrTime = "0" + _ts.Hours;
                    if (_ts.Minutes < 9)
                    { _strBrTime = _strBrTime + ":0" + _ts.Minutes; }
                    else
                    { _strBrTime = _strBrTime + ":" + _ts.Minutes; }
                }
                else
                {
                    _strBrTime = _ts.Hours.ToString();
                    if (_ts.Minutes < 9)
                    { _strBrTime = _strBrTime + ":0" + _ts.Minutes; }
                    else
                    { _strBrTime = _strBrTime + ":" + _ts.Minutes; }
                }
                return _strBrTime;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
                return "";
            }

        }
        public void setNewDataForShift()
        {
            try
            {
                _newshift.SHIFT_ID = Shft_id.Text.Trim().ToUpper();
                _newshift.SHIFT_DESCRIPTION = Shft_desc.Text.Trim();
                _newshift.SHIFT_ALLOCATION_TYPE = RBShiftALType.SelectedValue.ToString().Trim();
                _newshift.SHIFT_AUTO_SEARCH_START = ASStartTime.Text.Trim();
                _newshift.SHIFT_AUTO_SEARCH_END = ASEndTime.Text.Trim();
                _newshift.SHIFT_TYPE = Shft_Type.SelectedValue.ToString();
                _newshift.SHIFT_START = Shft_Frm.Text.Trim();
                _newshift.SHIFT_END = Shft_To.Text.Trim();
                _newshift.SHIFT_BREAK_START = Lnch_Frm.Text.Trim();
                _newshift.SHIFT_BREAK_END = Lnch_To.Text.Trim();
                _newshift.SHIFT_BREAK_HRS = hdnTot_BrHr.Value.ToString(); //Tot_BrHr.Text.Trim();
                _newshift.SHIFT_WORKHRS = hdnTot_WrKHr.Value.ToString(); //Tot_WrKHr.Text.Trim();
                _newshift.SHIFT_FLAG_ADD_BREAK = Convert.ToBoolean(ChkShft.Checked.ToString());
                _newshift.SHIFT_WEEKEND_DIFF_TIME = Convert.ToBoolean(WChkShft1.Checked.ToString());
                _newshift.SHIFT_WEEKEND_START = WShft_Frm.Text.Trim();
                _newshift.SHIFT_WEEKEND_END = WShft_To.Text.Trim();
                _newshift.SHIFT_WEEKEND_BREAK_START = WLnch_Frm.Text.Trim();
                _newshift.SHIFT_WEEKEND_BREAK_END = WLnch_To.Text.Trim();
                _newshift.SHIFT_EARLY_SEARCH_HRS = EarlySearchHours.Text.Trim();
                _newshift.SHIFT_LATE_SEARCH_HRS = LateSearchHours.Text.Trim();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }

        }
        protected void BtnSave_Cilck(object sender, EventArgs e)
        {
            try
            {

                Entity_Mode = (ViewState["PageMode"] == null ? "" : ViewState["PageMode"].ToString());

                try
                {

                    setNewDataForShift();
                    _newshift.CreatedBy = Session["uid"].ToString();
                    if (Entity_Mode == "")
                    {
                        clsShiftViewHandler.UpdateShiftDetails(_newshift, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                        if (strErrMsg.Trim().Length >= 1)
                        {
                            lblError.Text = strErrMsg;
                            lblError.Visible = true;
                            return;
                        }
                        else
                        {

                            Shft_id.Text = "";
                            Shft_desc.Text = "";
                            RBShiftALType.SelectedIndex = 0;
                            ASStartTime.Text = "";
                            ASEndTime.Text = "";
                            Shft_Type.SelectedIndex = 0;
                            Shft_Frm.Text = "";
                            Shft_To.Text = "";
                            Lnch_Frm.Text = "";
                            Lnch_To.Text = "";
                            Tot_BrHr.Text = "";
                            Tot_WrKHr.Text = "";
                            ChkShft.Checked = false;
                            WChkShft1.Checked = false;
                            WShft_Frm.Text = "";
                            WShft_To.Text = "";
                            WLnch_Frm.Text = "";
                            WLnch_To.Text = "";
                            EarlySearchHours.Text = "";
                            LateSearchHours.Text = "";
                            lblError.Text = strSuccMsg;
                            ViewState["PageMode"] = null;
                            mpeAddEditShift.Show();
                        }

                    }
                    else
                    {

                        clsShiftViewHandler.UpdateShiftDetails(_newshift, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                        if (strErrMsg.Trim().Length >= 1)
                        {
                            lblError.Text = strErrMsg;
                            lblError.Visible = true;
                            return;
                        }
                        else
                        {

                            lblMessages.Text = strSuccMsg;
                            lblMessages.Visible = true;
                            ViewState["PageMode"] = null;
                            lblMessages.Visible = true;
                            Shft_id.Text = "";
                            Shft_desc.Text = "";
                            RBShiftALType.SelectedIndex = 0;
                            ASStartTime.Text = "";
                            ASEndTime.Text = "";
                            Shft_Type.SelectedIndex = 0;
                            Shft_Frm.Text = "";
                            Shft_To.Text = "";
                            Lnch_Frm.Text = "";
                            Lnch_To.Text = "";
                            Tot_BrHr.Text = "";
                            Tot_WrKHr.Text = "";
                            ChkShft.Checked = false;
                            WChkShft1.Checked = false;
                            WShft_Frm.Text = "";
                            WShft_To.Text = "";
                            WLnch_Frm.Text = "";
                            WLnch_To.Text = "";
                            EarlySearchHours.Text = "";
                            LateSearchHours.Text = "";
                            mpeAddEditShift.Hide();
                        }

                    }


                }
                catch (Exception ex)
                {
                  
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
                }
            }
            catch (Exception ex)
            {               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
            BindgvShift();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Entity_Mode = (ViewState["PageMode"] == null ? "" : ViewState["PageMode"].ToString());

                if (Entity_Mode == "Modify")
                {
                    Modify_Data(ShiftID);
                }
                else
                {
                    Shft_id.Text = "";
                    Shft_desc.Text = "";
                    RBShiftALType.SelectedIndex = 0;
                    ASStartTime.Text = "";
                    ASEndTime.Text = "";
                    Shft_Type.SelectedIndex = 0;
                    Shft_Frm.Text = "";
                    Shft_To.Text = "";
                    Lnch_Frm.Text = "";
                    Lnch_To.Text = "";
                    Tot_WrKHr.Text = "";
                    ChkShft.Checked = false;
                    WChkShft1.Checked = false;
                    WShft_Frm.Text = "";
                    WShft_To.Text = "";
                    WLnch_Frm.Text = "";
                    WLnch_To.Text = "";
                    EarlySearchHours.Text = "";
                    LateSearchHours.Text = "";

                    Session.Remove("Mode");
                    //    return;
                }
                mpeAddEditShift.Hide();
            }
            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void WChkShft1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (WChkShft1.Checked == true)
                {
                    WShft_Frm.Text = ""; WShft_To.Text = ""; WLnch_Frm.Text = ""; WLnch_To.Text = ""; WTot_WrKHr.Text = ""; WTot_WrKHr.Text = "";// WChkShft.Checked = false; 
                    WShft_Frm.Enabled = true; WShft_To.Enabled = true; WLnch_Frm.Enabled = true; WLnch_To.Enabled = true;
                    WTot_WrKHr.Enabled = true; WChkShft.Enabled = true;

                }
                else
                {
                    WShft_Frm.Text = ""; WShft_To.Text = ""; WLnch_Frm.Text = ""; WLnch_To.Text = ""; WTot_WrKHr.Text = ""; WTot_WrKHr.Text = "";
                    WShft_Frm.Enabled = false; WShft_To.Enabled = false; WLnch_Frm.Enabled = false; WLnch_To.Enabled = false;
                    WTot_WrKHr.Enabled = false; WChkShft.Enabled = false; WTot_WrKHr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void RBShiftALType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBShiftALType.SelectedIndex == 1)
                {
                    ASStartTime.Enabled = true;
                    ASEndTime.Enabled = true;
                    ASStartTime.Text = "";
                    ASEndTime.Text = "";
                }
                else
                {
                    ASStartTime.Enabled = false;
                    ASEndTime.Enabled = false;
                    ASStartTime.Text = "";
                    ASEndTime.Text = "";
                }
                mpeAddEditShift.Show();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName()); ;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ShiftDetails>");
                int count = 0;
                for (int i = 0; i < gvShift.Rows.Count; i++)
                {                 
                    try
                    {
                   
                        CheckBox delrows = (CheckBox)gvShift.Rows[i].FindControl("DeleteRows");
                        HiddenField hdnRowID = (HiddenField)gvShift.Rows[i].FindControl("hdnRowID");
                        if (delrows.Checked == true)
                        {
                            count++;
                            strXML.Append("<Shift>");
                            strXML.Append("<RowID>" + hdnRowID.Value + "</RowID>");
                            strXML.Append("</Shift>");
                        }

                    }
                    catch (Exception ex)
                    {
                        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                    }
                }
                strXML.Append("</ShiftDetails>");
                clsShift objShift = new clsShift();
                objShift.CreatedBy = Session["uid"].ToString();
                if (count > 0)
                {
                    clsShiftViewHandler.UpdateShiftDetails(objShift, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblMessages.Text = strErrMsg;
                        lblMessages.Visible = true;
                        return;
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg;
                        lblMessages.Visible = true;
                        BindgvShift();
                        return;
                    }
                }
                else
                {
                    lblMessages.Text = "Please Select Record";
                    lblMessages.Visible = true;
                    return;
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
                DataTable dt = clsShiftViewHandler.GetShiftDetails("All");
                if (txtID.Text.ToString() == "" && txtDescription.Text.ToString() == "")
                {
                    gvShift.DataSource = dt;
                    gvShift.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"SHIFT_ID~" +txtID.Text.Trim(), "S" },
				{"SHIFT_DESCRIPTION~" +txtDescription.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvShift.DataSource = _tempDT;
                    gvShift.DataBind();
                }

                DropDownList ddl = (DropDownList)gvShift.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvShift.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvShift.PageIndex + 1).ToString();
                Label lblcount = (Label)gvShift.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvShift.DataSource).Rows.Count.ToString() + " Records.";
                if (gvShift.PageCount == 0)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvShift.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvShift.PageIndex + 1 == gvShift.PageCount)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvShift.PageIndex == 0)
                {
                    ((Button)gvShift.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvShift.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvShift.PageSize * gvShift.PageIndex) + 1) + " to " + (((gvShift.PageSize * (gvShift.PageIndex + 1)) - 10) + gvShift.Rows.Count);
                gvShift.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void gvShift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (string.Equals(hdnFlag.Value, "A", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChkDelete.Enabled = true;
                     lnkEdit.Enabled = true;
                }
                else
                {
                    ChkDelete.Enabled = false;
                    lnkEdit.Enabled = false;
                }
            }
        }

    }
}
