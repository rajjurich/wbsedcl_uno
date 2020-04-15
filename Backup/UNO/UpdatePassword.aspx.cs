using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;
using CMS.UNO.Login.Handler;

namespace UNO
{
    public partial class UpdatePassword : System.Web.UI.Page
    {

        public bool initialFlag = false;
        public static bool userFlag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"].ToString() == null)
            {
                Response.Redirect("Home.aspx", true);
                return;
            }

            if (!IsPostBack)
            {
                checkUser();
                if (initialFlag == true)
                {
                    btnCancel.Visible = false;
                }
            }
        }
        protected void checkUser()
        {
            clsLogin objLogin = clsLoginHandler.GetLoginDetails("GetLogin", Session["uid"].ToString());
            if (objLogin.IsFirstLogin)
            {
                initialFlag = true;
            }
            if (objLogin.LevelCode.ToLower() == "emp")
            {
                userFlag = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["uid"].ToString() == null)
                {
                    Response.Redirect("Home.aspx", true);
                    return;
                }

                clsLogin objLogin = clsLoginHandler.GetLoginDetails("GetPassword", Session["uid"].ToString());
                if (txtOldPass.Text == Encryption.EncryptDecrypt.Decrypt(objLogin.Password, true))
                {
                    lblError.Text = "";
                    string strPassword = Encryption.EncryptDecrypt.Encrypt(txtNewPass.Text.Trim(), true);
                    clsLogin objSendData = new clsLogin();
                    objSendData.Password = strPassword;
                    objSendData.UserID = Session["uid"].ToString();
                    clsLoginHandler.UpdatePassword("UpdatePass", objSendData);
                    if (objLogin.EmailID.ToString() != "")
                        SendMail(objLogin.EmailID);
                    if (objLogin.LevelCode.ToLower() == "emp")
                    {
                        if (objLogin.IsFirstLogin)
                        {
                            Session.Abandon();
                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Record updated successfully');window.location='Login.aspx';", true);
                        }
                        else
                        {

                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Record updated successfully');window.location='Login.aspx';", true);
                        }
                    }
                    else
                    {
                        if (objLogin.IsFirstLogin)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Record updated successfully');window.location='Login.aspx';", true);
                            Session.Abandon();
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Record updated successfully');window.location='Uno_Dashboard.aspx';", true);
                            Response.Redirect("Uno_Dashboard.aspx", true);
                        }
                    }


                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalid old password.";
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void cstvOldPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                lblError.Visible = false;
                lblError.Text = "";
                if (Session["uid"].ToString() == null)
                {
                    Response.Redirect("Login.aspx", true);
                    return;
                }

                clsLogin objLogin = clsLoginHandler.GetLoginDetails("GetLogin", Session["uid"].ToString());
                if (txtOldPass.Text == Encryption.EncryptDecrypt.Decrypt(objLogin.Password, true))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        public void SendMail(string To)
        {

            try
            {
                string message = "Dear User , " + System.Environment.NewLine + "";
                message = message + "         Your Password has been changed successfully. New Password is : '" + txtNewPass.Text + "'" + System.Environment.NewLine;

                Mail.SendMail("uno@cms.co.in", To, "", "Your UNO-Login password", message.ToString());

            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (userFlag == true)
            {
                Response.Redirect("ESS_Dashboard.aspx");
            }
            else
            {
                Response.Redirect("Uno_Dashboard.aspx");
            }
        }

    }
}