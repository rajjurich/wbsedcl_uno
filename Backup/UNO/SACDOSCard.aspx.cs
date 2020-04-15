using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace UNO
{
    public partial class SACDOSCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetMasterKey()
        {
            try
            {
                string Masterkey = HttpContext.Current.Session["MasterKey"].ToString();
                return Masterkey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        public static string SetDosKey(string DosKey)
        {
            try
            {
                HttpContext.Current.Session["DosKey"] = DosKey;
                return "True";
            }
            catch (Exception ex)
            {
                return "False";
            }
        }
    }
}