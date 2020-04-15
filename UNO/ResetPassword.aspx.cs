using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UNO.Login.Handler;
using System.Text;
using System.Web.Script.Serialization;

namespace UNO
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                try
                {

                    //dataobj
                    string strQuerystring = clsLoginHandler.Decryptdata((Convert.ToString(Request.QueryString["dataobj"])));
                    clsLoginView objDeserialize = new JavaScriptSerializer().Deserialize<clsLoginView>(strQuerystring);
                    clsLogin objData = new clsLogin();

                    //objData.UniqueCode = Convert.ToString(Request.QueryString["uCode"]);
                    //objData.UserID = Convert.ToString(Request.QueryString["uName"]);
                    objData.UniqueCode = objDeserialize.UniqueCode;
                    objData.UserID = objDeserialize.UserID;
                    clsLogin objRecData = clsLoginHandler.GetPassword("GetUniqueCode", objData);

                    if (objRecData != null)
                    {
                        if (objRecData.UniqueCode.Trim() == "" || objRecData.Hours >= 24)
                        {
                            ResetPwdPanel.Visible = false;
                            lblExpired.Text = "Reset password link has been expired.It was for 24 hours only.";
                        }

                        else
                        {
                            ResetPwdPanel.Visible = true;
                        }
                    }
                    else
                    {
                        ResetPwdPanel.Visible = false;
                        lblExpired.Text = "Reset password link has been expired.It was for 24 hours only.";
                        return;
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = "Error Occured: " + ex.Message.ToString();
                }

            }
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuerystring = clsLoginHandler.Decryptdata((Convert.ToString(Request.QueryString["dataobj"])));
                clsLoginView objDeserialize = new JavaScriptSerializer().Deserialize<clsLoginView>(strQuerystring);
                clsLogin objData = new clsLogin();
                objData.UniqueCode = objDeserialize.UniqueCode;
                objData.UserID = objDeserialize.UserID;
                objData.EmailID = objDeserialize.EmailID;
                objData.Password = Encryption.EncryptDecrypt.Encrypt(txtNewPwd.Text.Trim(), true);
                clsLoginHandler.UpdateUniqueID("ResetPassword", objData);
                lblError.Text = "Your password has been updated successfully.";
                txtNewPwd.Text = string.Empty;
                txtConfirmPwd.Text = string.Empty;
                StringBuilder strBody = new StringBuilder();

                strBody.Append("<HTML><BODY>" + "<font family=Times New Roman;>Hello!</br></br>");
                strBody.Append("Your password to access http://uno.cms.co.in/uno has been successfully changed. Please keep it in a secure place.");
                strBody.Append("</br></br>");
                strBody.Append("If you have not changed your password, then someone must have done it on your behalf. ");
                strBody.Append("</br></br>");
                strBody.Append("Regards,</br>" + Application["ClientName"].ToString() + "</font>");
                strBody.Append("</BODY></HTML>");
                strBody.Append("</br></br>");
                Mail.SendMail("pooja_yadav@cms.co.in", objData.EmailID, "", "ResetPassword", strBody.ToString());
            }
            catch (Exception ex)
            {
                lblError.Text = "Error Occured : " + ex.Message.ToString();
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("Home.aspx");

        }
    }
}