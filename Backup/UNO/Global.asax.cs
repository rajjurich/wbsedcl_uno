using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace UNO
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            clsCommonEntity objData = new clsCommonEntity();
            objData = clsCommonHandler.GetClientName();
            Application["ClientName"] = objData.CompanyName;
            Application["ClientAdd"] = objData.CompanyAdd;

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            clsCommonEntity objData = new clsCommonEntity();
            objData = clsCommonHandler.GetClientName();
            Application["ClientName"] = objData.CompanyName;
            Application["ClientAdd"] = objData.CompanyAdd;
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
