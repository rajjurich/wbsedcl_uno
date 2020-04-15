using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CMS.UNO.Core.Handler;
using System.Text;
namespace UNO
{
    public partial class LevelAccessView : System.Web.UI.Page
    {
      
        static string strSuccMsg, strErrMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                string msg = string.Empty;
                if (Convert.ToString(Request.QueryString["Message"])!= null)
                {
                    msg = Request.QueryString["Message"].ToString();
                    lblMessages.Text = msg;
                    lblMessages.Visible = true;
                }
                bindLevels();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLevels.ClientID + "');");
            }
        }
        public void bindLevels()
        {
           
            try
            {

             
                DataTable dt = clsLevelMasterHandler.GetAllLevels("AllLevel");              
                gvLevels.DataSource = dt;
                gvLevels.DataBind();

                DropDownList ddl = (DropDownList)gvLevels.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLevels.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLevels.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLevels.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLevels.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLevels.PageCount == 0)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLevels.PageIndex + 1 == gvLevels.PageCount)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLevels.PageIndex == 0)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }                

                ((Label)gvLevels.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLevels.PageSize * gvLevels.PageIndex) + 1) + " to " + (((gvLevels.PageSize * (gvLevels.PageIndex + 1)) - 10) + gvLevels.Rows.Count);

                gvLevels.BottomPagerRow.Visible = true;

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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {

            Response.Redirect("LevelAccessPage.aspx");

        }
        protected void gvLevels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    string levelID = e.CommandArgument.ToString();
                    Response.Redirect("LevelAccessEdit.aspx?levelID=" + levelID, true);


                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = "";
                DataTable dt = clsLevelMasterHandler.GetAllLevels("AllLevel");     
              

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvLevels.DataSource = dt;
                    gvLevels.DataBind();
                }
                else
                {
                    //Resolved Bug 374 - Swapnil Start
                    String[,] values = { 
				{"LevelCode~" +txtUserID.Text.Trim(), "S" },
				{"LevelName~" +txtLevelID.Text.Trim(), "S" }			
				 };
                    //Resolved Bug 374 - Swapnil End
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvLevels.DataSource = _tempDT;
                    gvLevels.DataBind();
                }

                DropDownList ddl = (DropDownList)gvLevels.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvLevels.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvLevels.PageIndex + 1).ToString();
                Label lblcount = (Label)gvLevels.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvLevels.DataSource).Rows.Count.ToString() + " Records.";
                if (gvLevels.PageCount == 0)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLevels.PageIndex + 1 == gvLevels.PageCount)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvLevels.PageIndex == 0)
                {
                    ((Button)gvLevels.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }                

                ((Label)gvLevels.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLevels.PageSize * gvLevels.PageIndex) + 1) + " to " + (((gvLevels.PageSize * (gvLevels.PageIndex + 1)) - 10) + gvLevels.Rows.Count);

                gvLevels.BottomPagerRow.Visible = true;

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
                gvLevels.PageIndex = Convert.ToInt32(((DropDownList)gvLevels.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindLevels();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLevels.PageIndex = gvLevels.PageIndex - 1;
                bindLevels();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLevels.PageIndex = gvLevels.PageIndex + 1;
                bindLevels();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
            }
        }        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = string.Empty;
                CheckBox DeleteRows;
                int intCount = 0;
                foreach (GridViewRow gr in gvLevels.Rows)
                {
                    
                    
                    DeleteRows = (CheckBox)gr.FindControl("DeleteRows");
                    if (DeleteRows.Checked)
                    {                        
                        intCount++;
                    }
                }
                if (intCount > 0)
                {

                    StringBuilder strXML = new StringBuilder();
                    strXML.Append("<LevelMenuRelation>");
                    
                        for (int i = 0; i < gvLevels.Rows.Count; i++)
                        {

                            CheckBox chk = (CheckBox)gvLevels.Rows[i].FindControl("DeleteRows");
                            if (chk.Checked == true)
                            {
                                
                                try
                                {
                                    string ID = ((GridViewRow)chk.NamingContainer).Cells[2].Text;
                                    strXML.Append("<Level>");
                                    strXML.Append("<LevelID>" + ID+"</LevelID>");
                                    strXML.Append("</Level>");
                                    
                                }
                                catch (Exception ex)
                                {
                                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                                }
                            }
                        }
                        strXML.Append("</LevelMenuRelation>");
                        clsLevel objLevel = new clsLevel();
                        objLevel.CreatedBy = Session["uid"].ToString();
                        clsLevelMasterHandler.UpdateLevelDetails(objLevel, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                        if (strErrMsg.Trim().Length >= 1)
                        {
                            lblMessages.Text = strErrMsg;
                            lblMessages.Visible = true;
                        }
                        else
                        {
                            lblMessages.Text = strSuccMsg;
                            lblMessages.Visible = true;
                            bindLevels();
                        }
                  
                }
                else
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
        protected void gvLevels_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Added by Pooja Yadav
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblLableCode = (Label)e.Row.FindControl("lblLableCode");
                Label lblFlag = (Label)e.Row.FindControl("lblFlag");
                CheckBox chkDeleteRows = (CheckBox)e.Row.FindControl("DeleteRows");
                if ((lblLableCode.Text.Trim().Equals("admin", StringComparison.CurrentCultureIgnoreCase)) || (lblLableCode.Text.Trim().Equals("emp", StringComparison.CurrentCultureIgnoreCase)) || (lblLableCode.Text.Trim().Equals("ENGINEER", StringComparison.CurrentCultureIgnoreCase)) || (lblFlag.Text.Trim().Equals("1",StringComparison.CurrentCultureIgnoreCase)))
                    chkDeleteRows.Enabled = false;     
               
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    
    }
}