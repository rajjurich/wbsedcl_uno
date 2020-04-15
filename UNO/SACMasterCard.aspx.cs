using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace UNO
{
    public partial class SACMasterCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string SetMasterKey(string MasterKey, string CompanyCode)
        {
            try
            {
                HttpContext.Current.Session["MasterKey"] = MasterKey;
                HttpContext.Current.Session["CompanyCode"] = CompanyCode;
                return "True";
            }
            catch (Exception ex)
            {
                return "False";
            }
        }
    }
}