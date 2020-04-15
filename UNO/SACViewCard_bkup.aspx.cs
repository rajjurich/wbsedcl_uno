using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace UNO
{
    public partial class SACViewCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetMasterKey()
        {
            try
            {
                string MasterKey = HttpContext.Current.Session["MasterKey"].ToString();
                return MasterKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        public static string GetDOSKey()
        {
            try
            {
                string DosKey = HttpContext.Current.Session["DosKey"].ToString();
                return DosKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}