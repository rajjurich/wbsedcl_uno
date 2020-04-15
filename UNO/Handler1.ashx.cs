using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.SessionState;
namespace UNO
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {
       
        public void ProcessRequest(HttpContext context)
        {
          
          
            //string imgEmpImage = "Hello World";
            //context.Response.BinaryWrite((byte[])imgEmpImage);
          
            //string path = @"D:\AppIcon_128.png";

            //if (path= @"D:\AppIcon_128.png)


            if (context.Request.QueryString["ImagePath"] != null &&  context.Request.QueryString["ImagePath"] != "")
            {
                string path = context.Request.QueryString["ImagePath"];
                // path = HttpContext.Current.Session["PhotoPath"].ToString();
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(path);
                System.IO.MemoryStream m = new System.IO.MemoryStream();
                bmp.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bPicture = m.ToArray();

                context.Response.BinaryWrite((byte[])bPicture);

            }
            
            //context.Response.BinaryWrite((byte[])dReader["EMP_IMAGES"]);
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}