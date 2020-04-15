using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace UNO
{
    public partial class ModuleMain : System.Web.UI.MasterPage
    {
        MenuBind mb = new MenuBind();
        public static string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
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

                //uid.Value = empID;
                Uname.Value = empID;
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

                if (empID != "admin")
                {
                    getOperationalAccess();
                }
                else
                {                    
                    lblOperationalAccess.Visible = false;
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ModuleMainMaster");
            }
        }

        private void getOperationalAccess()
        {
            try
            {
                DataTable dtData = new DataTable();
                using (SqlConnection con = new SqlConnection(m_connections))
                {
                    SqlCommand cmd = new SqlCommand("getOperationalAccess", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramDisplayLength = new SqlParameter() { ParameterName = "@EmployeeCode", Value = Session["uid"].ToString() };
                    cmd.Parameters.Add(paramDisplayLength);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtData);
                }
                if (dtData.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    int i = 0;
                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (i == 0)
                        {
                            str.Append(dr["columnValue"].ToString());
                        }
                        else
                        {
                            str.Append(", " + dr["columnValue"].ToString());
                        }
                        i++;
                    }
                    lblOperationalAccess.Text = string.Format("{0} : {1}", "Access provided", str.ToString());
                }
                else
                {
                    lblOperationalAccess.Visible = false;
                }



            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ModuleMainMaster");
            }
        }
    }
}