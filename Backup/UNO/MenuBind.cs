using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using System.Text;

namespace UNO
{
    public class MenuBind
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
       // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public string BindMenu(string empId)
        {

            string strMenuList = "";
            DataTable dtMenusdetails = new DataTable();
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string ModuleName = "";
                string[] Module = null;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmdLicense = new SqlCommand("select LicenseKey from Client_License", conn);
                string ClientLicenseKey = Convert.ToString(cmdLicense.ExecuteScalar());
                if (ClientLicenseKey != "")
                {
                    string[] strKey = ClientLicenseKey.Split('µ');
                    Module = strKey[1].Split('¶');
                }
                string fetchMenuDetails = "";
                if (Module != null)
                {
                    for (int i = 0; i < Module.Length; i++)
                    {
                        if (Module[i] != "")
                        {
                            ModuleName = Encryption.EncryptDecrypt.DecryptModule(Module[i]);
                            if (i == 0)
                            {
                                fetchMenuDetails += "  and ( menutable.Modules='" + ModuleName + "'";
                            }
                            else
                            {
                                fetchMenuDetails += "  OR  menutable.Modules='" + ModuleName + "'";
                            }
                        }
                        if (Module.Length == i + 1)
                        {
                            fetchMenuDetails += ")";
                        }


                    }
                }
                //string fetchMenuDetails = " SELECT ";
                //fetchMenuDetails += " menutable.Id,ENT_User.EmployeeID,MenuTable.Name as MenuName,MenuTable.Modules as ModuleName,MenuTable.url as PageUrl,MenuTable.ParentMenuId as ParentMenu ";
                //fetchMenuDetails += "   FROM ENT_User, ";
                //fetchMenuDetails += "  LevelMenuRelation, ";
                //fetchMenuDetails += "  menutable ";
                //fetchMenuDetails += "   where ";
                //fetchMenuDetails += "  ENT_User.LevelID=LevelMenuRelation.LevelID and ";
                //fetchMenuDetails += "  MenuTable.Id=LevelMenuRelation.MenuID ";
                //fetchMenuDetails += "  and ENT_User.UserID='" + empId + "'";




                SqlCommand cmdMenuDetails = new SqlCommand("spGetMenuItem", conn);
                cmdMenuDetails.CommandType = CommandType.StoredProcedure;
                cmdMenuDetails.Parameters.AddWithValue("@userid", empId);
                cmdMenuDetails.Parameters.AddWithValue("@LicenseMenu", fetchMenuDetails);

                SqlDataAdapter dap = new SqlDataAdapter(cmdMenuDetails);

                dap.Fill(dtMenusdetails);
                bool flag = false;
                try
                {
                    string str = "select essenabled from ent_user where userid='" + empId + "' ";
                    SqlCommand cmdFlagRet = new SqlCommand(str, conn);
                    flag = (bool)cmdFlagRet.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    flag = false;
                }
                strMenuList = ctreateMenus(dtMenusdetails, flag);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strMenuList;
        }
        public int EssModuleEnableFromParams()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int enableFlag = 0;
            SqlCommand CheckESS = new SqlCommand("select value from ent_params where identifier='ESS_ENABLE_FLAG'", conn);
            SqlDataAdapter da = new SqlDataAdapter(CheckESS);
            DataTable dta = new DataTable();
            da.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                enableFlag = Convert.ToInt32(dta.Rows[0][0].ToString());
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return enableFlag;
        }
        public string ctreateMenus(DataTable menuData, bool essEnabledFlag)
        {
            StringBuilder menuItems = new StringBuilder();
            try
            {
                DataTable dtDistinctModules = menuData.DefaultView.ToTable(true, "ModuleName");
                menuItems.Append("<ul>");
                for (int i = 0; i < dtDistinctModules.Rows.Count; i++)
                {
                    menuItems.Append("<li><a href=\"#\">");

                    menuItems.Append(dtDistinctModules.Rows[i]["ModuleName"].ToString());
                    menuItems.Append("</a><div class=\"cbp-hrsub\">");
                    menuItems.Append("<div class=\"cbp-hrsub-inner\">");

                    DataTable dtparent = new DataTable();
                    DataRow[] drParant = menuData.Select("ModuleName = '" + dtDistinctModules.Rows[i]["ModuleName"].ToString() + "' and ParentMenu IS NULL");

                    for (int j = 0; j < drParant.Length; j++)
                    {
                        menuItems.Append("<div>");
                        DataRow parentMenu = drParant[j];


                        menuItems.Append("<h4>"); menuItems.Append(parentMenu["MenuName"].ToString()); menuItems.Append("</h4>");
                        DataRow[] drChild = menuData.Select("ModuleName = '" + dtDistinctModules.Rows[i]["ModuleName"].ToString() + "' and ParentMenu='" + parentMenu["id"].ToString() + "'");
                        menuItems.Append("<ul>");
                        for (int k = 0; k < drChild.Length; k++)
                        {
                            DataRow ChildRow = drChild[k];
                            menuItems.Append("<li><a href=\"" + ChildRow["PageUrl"].ToString() + "\">");
                            menuItems.Append(ChildRow["MenuName"].ToString());
                            menuItems.Append("</a></li>");
                        }

                        menuItems.Append("</ul>");
                        menuItems.Append("</div>");

                    }

                    menuItems.Append("</li>");



                }


                int EnableFlag = EssModuleEnableFromParams();

                if (essEnabledFlag == true && EnableFlag == 1)
                {
                    menuItems.Append("<li><a href=\"#\">");
                    //module
                    menuItems.Append("Employee Self-Service");
                    menuItems.Append("</a><div class=\"cbp-hrsub\">");
                    menuItems.Append("<div class=\"cbp-hrsub-inner\">");
                    //parent
                    menuItems.Append("<div>");
                    menuItems.Append("<h4>"); menuItems.Append("Dashboard"); menuItems.Append("</h4>");
                    menuItems.Append("<ul>");

                    menuItems.Append("<li><a href=\"" + "ESS_Dashboard.aspx" + "\">");
                    menuItems.Append("Dashboard");
                    menuItems.Append("</a></li>");

                    menuItems.Append("</ul>");
                    menuItems.Append("</div>");
                    //end parent

                    //parent 1
                    menuItems.Append("<div>");
                    menuItems.Append("<h4>"); menuItems.Append("Transactions"); menuItems.Append("</h4>");
                    menuItems.Append("<ul>");

                    menuItems.Append("<li><a href=\"" + "ESS_TA_LV_View.aspx" + "\">");
                    menuItems.Append("Leave");
                    menuItems.Append("</a></li>");

                    menuItems.Append("<li><a href=\"" + "ESS_TA_od_View.aspx" + "\">");
                    menuItems.Append("Out Door");
                    menuItems.Append("</a></li>");

                    menuItems.Append("<li><a href=\"" + "ESS_TA_ma_View.aspx" + "\">");
                    menuItems.Append("Manual Attendance");
                    menuItems.Append("</a></li>");

                    menuItems.Append("<li><a href=\"" + "ESS_TA_gp_View.aspx" + "\">");
                    menuItems.Append("Outpass");
                    menuItems.Append("</a></li>");

                    menuItems.Append("<li><a href=\"" + "UpdatePassword.aspx" + "\">");
                    menuItems.Append("Change Password");
                    menuItems.Append("</a></li>");


                    ////vms link added by vaibhav start
                    //menuItems.Append("<li><a href=\"" + "VMSPriorAppShow.aspx" + "\">");
                    //menuItems.Append("Prior Appoinment");
                    //menuItems.Append("</a></li>");

                    //menuItems.Append("<li><a href=\"" + "ESS_CallLogManagement.aspx" + "\">");
                    //menuItems.Append("Call Logs");
                    //menuItems.Append("</a></li>");




                    ////vms link added by vaibhav end

                    menuItems.Append("</ul>");
                    menuItems.Append("</div>");
                    //end parent 2


                    //parent 3
                    menuItems.Append("<div>");
                    menuItems.Append("<h4>"); menuItems.Append("Reports"); menuItems.Append("</h4>");
                    menuItems.Append("<ul>");

                    menuItems.Append("<li><a href=\"" + "ESS_PendingRequest.aspx?Att=0" + "\">");
                    menuItems.Append("Attendance");
                    menuItems.Append("</a></li>");

                    menuItems.Append("</ul>");
                    menuItems.Append("</div>");
                    //end parent 3





                    menuItems.Append("</div>");
                    //end module
                    menuItems.Append("</li>");
                }
                menuItems.Append("</ul>");




            }
            catch (Exception ex)
            {





            }
            return menuItems.ToString();
        }
    }
}