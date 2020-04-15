using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using CMS.UNO.Core.Handler;
using System.Text;
namespace UNO
{
    public partial class UserView : System.Web.UI.Page
    {
      
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPasswordEdit.Attributes["type"] = "password";
            txtConfirmPasswordEdit.Attributes["type"] = "password";
            if (!IsPostBack)
            {
                bindDataGrid();
                bindddlLevelType();
                FillEmployeeEntity();
                FillEditEmployeeEntity();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvUsers.ClientID + "');");
            }
        }

        private void bindDataGrid()
        {
            try
            {
                DataTable dt = clsUserViewHandler.GetUserEmployeeDetails("All");              
                gvUsers.DataSource = dt;
                gvUsers.DataBind();


                if (dt.Rows.Count != 0)
                {
                    DropDownList ddl = (DropDownList)gvUsers.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvUsers.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvUsers.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvUsers.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvUsers.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvUsers.PageCount == 0)
                    {
                        ((Button)gvUsers.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvUsers.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvUsers.PageIndex + 1 == gvUsers.PageCount)
                    {
                        ((Button)gvUsers.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvUsers.PageIndex == 0)
                    {
                        ((Button)gvUsers.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvUsers.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvUsers.PageSize * gvUsers.PageIndex) + 1) + " to " + (((gvUsers.PageSize * (gvUsers.PageIndex + 1)) - 10) + gvUsers.Rows.Count);

                    gvUsers.BottomPagerRow.Visible = true;
                }
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

        private void bindddlLevelType()
        {
            try
            {
                DataTable dt = clsUserViewHandler.GetUserEmployeeDetails("Level");
                ddlLevelTypeAdd.DataValueField = "Id";
                ddlLevelTypeAdd.DataTextField = "LevelName";
                ddlLevelTypeAdd.DataSource = dt;
                ddlLevelTypeAdd.DataBind();
                ddlLevelTypeAdd.Items.Insert(0, new ListItem("Select One", "0"));

                ddlLevelTypeEdit.DataValueField = "Id";
                ddlLevelTypeEdit.DataTextField = "LevelName";
                ddlLevelTypeEdit.DataSource = dt;
                ddlLevelTypeEdit.DataBind();
                ddlLevelTypeEdit.Items.Insert(0, new ListItem("Select One", "0"));

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
                gvUsers.PageIndex = Convert.ToInt32(((DropDownList)gvUsers.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
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
                gvUsers.PageIndex = gvUsers.PageIndex - 1;
                bindDataGrid();
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
                gvUsers.PageIndex = gvUsers.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
            }
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLevelTypeAdd.SelectedValue.ToString().ToLower() == "2")
                {
                    if (ddlEmpCodeAdd.SelectedValue.ToString() == "0")
                    {
                        lblErrorAdd.Text = "Please Select an Employee";
                        lblErrorAdd.Visible = true;
                        mpeAddUser.Show();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                        return;
                    }
                }

                clsUserView objUser = new clsUserView();
                objUser.CreatedBy = Session["uid"].ToString();
                objUser.EmpID = ddlEmpCodeAdd.SelectedValue.ToString();
                objUser.UserID = txtUserNameAdd.Text.Trim();
                objUser.Password = Encryption.EncryptDecrypt.Encrypt(txtPasswordAdd.Text.Trim(), true);
                objUser.LevelID = ddlLevelTypeAdd.SelectedValue;

                clsUserViewHandler.UpdateUserDetails(objUser, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblErrorAdd.Text = strErrMsg.Trim();
                    lblErrorAdd.Visible = true;
                    mpeAddUser.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                    return;
                }
                else
                {

                    lblErrorAdd.Text = strSuccMsg;
                    lblErrorAdd.Visible = true;
                    bindDataGrid();
                    FillEmployeeEntity();
                    txtUserNameAdd.Text = "";
                    txtPasswordAdd.Text = "";
                    txtConfirmPasswordAdd.Text = "";
                    ddlEmpCodeAdd.SelectedIndex = 0;
                    ddlLevelTypeAdd.SelectedIndex = 0;
                    mpeAddUser.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddUser.Hide();

            clearAdd();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mpeAddUser.Show();
            clearAdd();
            txtUserNameAdd.Focus();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel3, UpdatePanel3.GetType(), "Script", "validateChosen();", true);
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    txtPasswordEdit.Focus();
                    string UserID = e.CommandArgument.ToString();
                    clsUserView objUser = clsUserViewHandler.GetUserDetails("User", UserID);
                    txtUserNameEdit.Text = objUser.UserID;
                 
                    if (objUser.EmpID.ToString() != "")
                        ddlEmployeeIDEdit.SelectedValue = objUser.EmpID.ToString();
                    else
                        ddlEmployeeIDEdit.SelectedIndex = -1;
                    ddlLevelTypeEdit.SelectedValue = objUser.LevelID;
                    txtUserNameEdit.ReadOnly = true;
                    txtPasswordEdit.Text = Encryption.EncryptDecrypt.Decrypt(objUser.Password.ToString(), true);
                    txtConfirmPasswordEdit.Text = Encryption.EncryptDecrypt.Decrypt(objUser.Password.ToString(), true);
                    lblErrorEdit.Text = string.Empty;
                    lblErrorEdit.Visible = true;
                    mpeEditUser.Show();

                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLevelTypeEdit.SelectedValue.ToString().ToLower() == "2")
                {
                    if (ddlEmployeeIDEdit.SelectedValue.ToString() == "0")
                    {
                        lblErrorAdd.Text = "Please Select an Employee";
                        lblErrorAdd.Visible = true;
                        mpeEditUser.Show();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
                        return;
                    }
                }

                clsUserView objUser = new clsUserView();
                objUser.CreatedBy = Session["uid"].ToString();
                objUser.EmpID = ddlEmployeeIDEdit.SelectedValue.ToString();
                objUser.UserID = txtUserNameEdit.Text.Trim();
                objUser.Password = Encryption.EncryptDecrypt.Encrypt(txtPasswordEdit.Text.Trim(), true);
                objUser.LevelID = ddlLevelTypeEdit.SelectedValue;

                clsUserViewHandler.UpdateUserDetails(objUser, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblErrorEdit.Text = strErrMsg.Trim();
                    lblErrorEdit.Visible = true;
                    mpeEditUser.Show();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
                    return;
                }
                else
                {
                    mpeEditUser.Hide();
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                }



            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel4, UpdatePanel4.GetType(), "Script", "validateChosen();", true);
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEditUser.Hide();
            clearEdit();
        }

        private void clearAdd()
        {
            txtUserNameAdd.Text = "";
            txtPasswordAdd.Text = "";
            txtConfirmPasswordAdd.Text = "";
            //txtEmpCodeAdd.Text = "";
            ddlEmpCodeAdd.SelectedIndex = 0;
            ddlLevelTypeAdd.SelectedIndex = 0;
            lblErrorAdd.Text = "";
            lblErrorAdd.Visible = false;
            lblMessages.Text = "";

        }

        private void clearEdit()
        {
            txtUserNameEdit.Text = "";
            txtPasswordEdit.Text = "";
            txtConfirmPasswordEdit.Text = "";
            //txtEmpCodeEdit.Text = "";
            ddlEmployeeIDEdit.SelectedIndex = 0;
            ddlLevelTypeEdit.SelectedIndex = 0;
            lblErrorEdit.Text = "";
            lblErrorEdit.Visible = false;
            lblMessages.Text = "";

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder strXML = new StringBuilder();
            clsUserView objUser=new clsUserView();
            objUser.CreatedBy = Session["uid"].ToString();
            int cnt = 0;
            try
            {
               
                strXML.Append("<ent_user>");
                for (int i = 0; i < gvUsers.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvUsers.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {                       
                        try
                        {
                            cnt++;
                            strXML.Append("<User>");
                            strXML.Append("<UserID>" + gvUsers.Rows[i].Cells[2].Text + "</UserID>"); 
                            strXML.Append("</User>");
                        }
                        catch (Exception ex)
                        {                           
                            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserView");
                        }
                    }
                }
                strXML.Append("</ent_user>");
                if (cnt > 0)
                {
                    clsUserViewHandler.UpdateUserDetails(objUser,"Delete",strXML.ToString(),ref strErrMsg,ref strSuccMsg,clsCommonHandler.PageName());
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblMessages.Text = strErrMsg.Trim();
                        lblMessages.Visible = true;
                        bindDataGrid();
                    }
                    else
                    {
                        lblMessages.Text = strSuccMsg.Trim();
                        lblMessages.Visible = true;
                        bindDataGrid();
                        FillEmployeeEntity();
                    }
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
                DataTable dt = clsUserViewHandler.GetUserEmployeeDetails("All");


                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvUsers.DataSource = dt;
                    gvUsers.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"UserID~" +txtUserID.Text.Trim(), "S" },
				{"LevelName~" +txtLevelID.Text.Trim(), "I" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvUsers.DataSource = _tempDT;
                    gvUsers.DataBind();
                }

                DropDownList ddl = (DropDownList)gvUsers.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvUsers.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvUsers.PageIndex + 1).ToString();
                Label lblcount = (Label)gvUsers.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvUsers.DataSource).Rows.Count.ToString() + " Records.";
                if (gvUsers.PageCount == 0)
                {
                    ((Button)gvUsers.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvUsers.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvUsers.PageIndex + 1 == gvUsers.PageCount)
                {
                    ((Button)gvUsers.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvUsers.PageIndex == 0)
                {
                    ((Button)gvUsers.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvUsers.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvUsers.PageSize * gvUsers.PageIndex) + 1) + " to " + (gvUsers.PageSize * (gvUsers.PageIndex + 1));

                ((Label)gvUsers.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvUsers.PageSize * gvUsers.PageIndex) + 1) + " to " + (((gvUsers.PageSize * (gvUsers.PageIndex + 1)) - 10) + gvUsers.Rows.Count);

                gvUsers.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Added by Pooja Yadav
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnLevelCode = (HiddenField)e.Row.FindControl("hdnLevelCode");
                CheckBox chkDeleteRows = (CheckBox)e.Row.FindControl("DeleteRows");
                if ((hdnLevelCode.Value.Trim().Equals("admin", StringComparison.CurrentCultureIgnoreCase)))
                    chkDeleteRows.Enabled = false;

            }
        }
        private void FillEmployeeEntity()
        {
            try
            {
                DataTable dt = clsUserViewHandler.GetUserEmployeeDetails("NEMP");

                ddlEmpCodeAdd.DataValueField = "ID";
                ddlEmpCodeAdd.DataTextField = "ID";
                ddlEmpCodeAdd.DataSource = dt;
                ddlEmpCodeAdd.DataBind();
                ddlEmpCodeAdd.Items.Insert(0, new ListItem("Select One", "0"));


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
        private void FillEditEmployeeEntity()
        {
            try
            {
                DataTable dt = clsUserViewHandler.GetUserEmployeeDetails("EEMP");

                ddlEmployeeIDEdit.DataValueField = "ID";
                ddlEmployeeIDEdit.DataTextField = "ID";
                ddlEmployeeIDEdit.DataSource = dt;
                ddlEmployeeIDEdit.DataBind();
                ddlEmployeeIDEdit.Items.Insert(0, new ListItem("Select One", "0"));

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

    }
}