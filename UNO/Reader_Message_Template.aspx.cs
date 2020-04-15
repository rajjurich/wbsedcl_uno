using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UNO.Core.Handler;
using System.Data;

namespace UNO
{
    public partial class Reader_Message_Template : System.Web.UI.Page
    {
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                FillCtrl(ctrllst);
                FillEvnt(ctrllst1);
                FillCtrl(ddlCtrl);
                FillEvnt(ddlEvnt);
                //FillEntity(ddlEntity);
                //btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvCompany.ClientID + "');");
                //BindEntDropDown();
            }
        }

        void bindDataGrid()
        {
            try
            {
                 List<ReaderTemp> lstCommonView = GetcommonList();

                Session["lstCommon"] = lstCommonView;
                gvCompany.DataSource = lstCommonView;
                gvCompany.DataBind();
                DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCompany.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = lstCommonView.Count.ToString() + " Records.";
                if (gvCompany.PageCount == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);

                gvCompany.BottomPagerRow.Visible = true;


                if (lstCommonView.Count != 0)
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

        private void FillCtrl(DropDownList drpEdit)
        {
            try
            {
                DataTable dt = clsCommonHandler.GetControllerList("COMMONMASTERS", "ENT");
                if (dt.Rows.Count > 0)
                {
                    drpEdit.DataValueField = "CTLR_ID";
                    drpEdit.DataTextField = "CTLR_DESCRIPTION";
                    drpEdit.DataSource = dt;
                    drpEdit.DataBind();
                    drpEdit.Items.Insert(0, new ListItem("Select One", "0"));
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        private void FillEvnt(DropDownList drpEdit)
        {
            try
            {
                DataTable dt = clsCommonHandler.GetEntParameterValues("EVENTMASTER", "EVNT");
                if (dt.Rows.Count > 0)
                {
                    drpEdit.DataValueField = "Code";
                    drpEdit.DataTextField = "value";
                    drpEdit.DataSource = dt;
                    drpEdit.DataBind();
                    drpEdit.Items.Insert(0, new ListItem("Select One", "0"));
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        private List<ReaderTemp> GetcommonList()
        {
            List<ReaderTemp> lstCommonView;
            lstCommonView = CMS.UNO.Core.Handler.RederTemplate.GetCommonData("Common");
            Session["lstCommon"] = lstCommonView;
            return lstCommonView;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            ctrllst.SelectedValue = "0";
            ctrllst1.SelectedValue = "0";
            txtDesc.Text = "";
            mpeAddCommon.Show();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ReaderTemp objCommon = new ReaderTemp();
            try
            {
                for (int i = 0; i < gvCompany.Rows.Count; i++)
                {

                    CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked)
                    {
                        objCommon.ReaderID = Convert.ToInt32(gvCompany.Rows[i].Cells[2].Text);
                        objCommon.ControllerID = 0;
                        objCommon.EventID = 0;
                        objCommon.EventMessage = "";

                        RederTemplate.InsertCommonDetails(objCommon, "Delete", ref strErrMsg, ref strSuccMsg);
                    }
                }
                
                if (strSuccMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    bindDataGrid();
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


                List<ReaderTemp> lstCommonView;
                if (Session["lstCommon"] == null)
                {
                    lstCommonView = RederTemplate.GetCommonData("Common");
                    Session["lstCommon"] = lstCommonView;
                }
                else
                    lstCommonView = (List<ReaderTemp>)Session["lstCommon"];


                if (txtControllerName.Text.ToString() == "")
                {
                    bindDataGrid();
                }
                else
                {

                    IEnumerable<ReaderTemp> IEnlstCommonView = null;
                    if (txtControllerName.Text.ToString().Trim() != "")
                        IEnlstCommonView = from item in lstCommonView
                                           where item.ControllerName.ToLower().Contains(txtControllerName.Text.Trim().ToLower()) 
                                           select item;  

                    gvCompany.BottomPagerRow.Visible = true;

                    List<ReaderTemp> lstCommonList = IEnlstCommonView.ToList<ReaderTemp>();
                    gvCompany.DataSource = lstCommonList;
                    gvCompany.DataBind();

                    DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvCompany.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = (lstCommonList.Count().ToString()) + " Records.";
                    if (gvCompany.PageCount == 0)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCompany.PageIndex == 0)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReaderTemp objCommon = new ReaderTemp();
                objCommon.ReaderID = 0;
                objCommon.ControllerID = Convert.ToInt32(ctrllst.SelectedValue);
                objCommon.EventID = Convert.ToInt32(ctrllst1.SelectedValue);
                objCommon.EventMessage = txtDesc.Text.Trim();

                RederTemplate.InsertCommonDetails(objCommon, "Insert", ref strErrMsg, ref strSuccMsg);

                if (strErrMsg.Trim().Length >= 1)
                {
                    lblerror.Visible = true;
                    lblerror.Text = strErrMsg;
                    mpeAddCommon.Show();
                    return;
                }
                else
                {
                    lblerror.Text = strSuccMsg;
                    lblerror.Visible = true;
                    txtDesc.Text = "";
                    ctrllst.SelectedValue = "0";
                    ctrllst1.SelectedValue = "0";
                    ctrllst.Focus();
                    mpeAddCommon.Show();
                    bindDataGrid();
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            mpeAddCommon.Hide();
        }

        protected void btnModifySave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtName.Text;
              
                ReaderTemp objCommon = new ReaderTemp();
                objCommon.ReaderID = Convert.ToInt32(Session["ReaderID"]);
                objCommon.ControllerID = Convert.ToInt32(ddlCtrl.SelectedValue);
                objCommon.EventID =Convert.ToInt32(ddlEvnt.SelectedValue);
                objCommon.EventMessage = txtName.Text;
                RederTemplate.InsertCommonDetails(objCommon, "Update", ref strErrMsg, ref strSuccMsg);

                if (strErrMsg.Trim().Length >= 1)
                {
                    lblEditError.Visible = true;
                    lblEditError.Text = strErrMsg;
                    mpModifyCommon.Show();
                    return;
                }
                else
                {
                    mpModifyCommon.Hide();
                    lblMessages.Text = strSuccMsg;
                    txtName.Text = "";
                    lblEditError.Text = "";
                    ddlEvnt.SelectedValue = "0";
                    ddlCtrl.SelectedValue = "0";
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
            mpModifyCommon.Hide();
        }

        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit3")
            {
                lblMessages.Text = "";
                lblEditError.Text = "";
                int Rowid = Convert.ToInt32(e.CommandArgument);
                txtName.Focus();
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                //int ReaderID = Convert.ToInt32(row.Cells[2].Text);
                FillModifyDetails(Rowid);
                mpModifyCommon.Show();
            }
        }

        public void FillModifyDetails(int ReaderID)
        {
            try
            {
                List<ReaderTemp> lstCommonView;
                if (Session["lstCommon"] == null)
                {
                    lstCommonView = GetcommonList();
                    Session["lstCommon"] = lstCommonView;
                }
                else
                    lstCommonView = (List<ReaderTemp>)Session["lstCommon"];


                IEnumerable<ReaderTemp> IEnlstCommonView = null;
                IEnlstCommonView = from item in lstCommonView
                                   where item.ReaderID == (ReaderID)                                  
                                   select item;

                List<ReaderTemp> lstSearchList = IEnlstCommonView.ToList<ReaderTemp>();

                if (lstSearchList.Count > 0)
                {
                    Session["ReaderID"] = lstSearchList[0].ReaderID;
                    //Added by Pooja Yadav
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:DisplayLabelTextEntity();", true);
                    ddlCtrl.SelectedValue = Convert.ToString(lstSearchList[0].ControllerID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:DisplayLabelTextEntity();", true);
                    ddlEvnt.SelectedValue = 0 + Convert.ToString(lstSearchList[0].EventID);
                    txtName.Text = lstSearchList[0].EventMessage;
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

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = Convert.ToInt32(((DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }

        }
    }
}