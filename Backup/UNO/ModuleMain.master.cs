using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UNO
{
    public partial class ModuleMain : System.Web.UI.MasterPage
    {
        MenuBind mb = new MenuBind();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Request.FilePath.Contains("Login"))
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {

                    Session.Abandon();
                    Response.Redirect("Login.aspx");


                }
            }
            try
            {
                string empID = Session["uid"].ToString();



                string url = HttpContext.Current.Request.Url.AbsolutePath;



                string[] pageName = url.Split('/');


                int a = pageName.Length;

                string b = pageName[a - 1].ToString();

                if (b.ToLower() == "updatepassword.aspx")
                { }
                else
                {

                    menuList.Text = mb.BindMenu(empID);
                }

            
                
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ModuleMainMaster");
            }
        }
    }
}