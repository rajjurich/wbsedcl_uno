using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace UNO
{
    public partial class iFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string fileName = "";
            if (Request.Browser.Type.ToUpper().Contains("IE")) // replace with your check
            {
                fileName = System.IO.Path.GetFileName(e.FileName);
            }
            else
            {
                fileName = e.FileName;
            }
            string path = Server.MapPath("~/uploadImg/") + fileName;
            AjaxFileUpload1.SaveAs(path);
            Session["imageName"] = fileName;

        }
        [WebMethod]
        public static string setSession()
        {
            HttpContext.Current.Session["imageName"] = "add_image.png";
            return HttpContext.Current.Session["imageName"].ToString();
        }
    }
}