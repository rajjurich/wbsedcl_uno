using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CMS.UNO.Core.Handler;

namespace UNO
{
    public partial class LevelAccessPage : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvCustomers.DataSource = clsLevelMasterHandler.GetAllLevels("Modules");
                gvCustomers.DataBind();
                txtLevelCode.Focus();
             
            }
           
        }
        private static DataTable GetData(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlOrders").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";               
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
            }
            else
            {
                row.FindControl("pnlOrders").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
            }
        }
        private void BindOrders(string customerId, GridView gvOrders)
        {
            gvOrders.ToolTip = customerId;           
            gvOrders.DataSource = GetData(string.Format("Select id,name from MenuTable where Modules='" + customerId + "' and ParentMenuId is null "));
            ViewState["Modules"] = customerId;
            gvOrders.DataBind();
        }     
        protected void Show_Hide_ProductsGrid(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlProducts").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";              
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

            }
            else
            {
                row.FindControl("pnlProducts").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
            }
        }
        private void BindProducts(int orderId, GridView gvProducts)
        {
            gvProducts.ToolTip = orderId.ToString();
            string Modules = ViewState["Modules"].ToString();            
            gvProducts.DataSource = GetData(string.Format("Select id,name from MenuTable where Modules = '" + Modules + "' and ParentMenuId = " + orderId + ""));
            gvProducts.DataBind();
        }         
        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                BindOrders(customerId, gvOrders);
            }
        }
        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvOrders = (sender as GridView);              

                string customerId = gvOrders.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvProducts = e.Row.FindControl("gvProducts") as GridView;
                int childid = Convert.ToInt32(customerId);
                BindProducts(childid, gvProducts);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {


                string levelCode = "", levelDescription = "";
                levelCode = txtLevelCode.Text.Trim().ToUpper();
                levelDescription = (txtLevelDescrip.Text.Trim().First().ToString().ToUpper() + String.Join("", txtLevelDescrip.Text.Trim().Skip(1)));
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string chker = "select * from LevelMaster where levelcode = '" + txtLevelCode.Text.Trim() + "' and isnull(LevelIsDeleted,0)=0 ";
                SqlCommand cmdChk = new SqlCommand(chker, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmdChk);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dt.Dispose();
                    lblMessages.Visible = true;
                    lblMessages.Text = "LevelCode Already exists";                    
                }
                else
                {
                    string insertQuery = "insert into LevelMaster(levelcode,levelname)values('" + levelCode + "','" + levelDescription + "')";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();

                    string maxIds = "select max(id) from  LevelMaster";
                    SqlCommand cmd1 = new SqlCommand(maxIds, conn);
                    string lvID = cmd1.ExecuteScalar().ToString();

                    ViewState["levelId"] = lvID;
                    string id = "", parentID = "";

                    for (int i = 0; i < gvCustomers.Rows.Count; i++)
                    {

                        CheckBox chetest1 = gvCustomers.Rows[i].FindControl("CheckBox1") as CheckBox;

                        if (chetest1.Checked == true)
                        {
                            GridViewRow row = (chetest1.NamingContainer as GridViewRow);
                            GridView gridOrders = row.FindControl("gvOrders") as GridView;


                            for (int j = 0; j < gridOrders.Rows.Count; j++)
                            {
                                CheckBox chetest2 = gridOrders.Rows[j].FindControl("CheckBox2") as CheckBox;

                                if (chetest2.Checked == true)
                                {
                                    GridViewRow row1 = (chetest2.NamingContainer as GridViewRow);
                                    GridView gvProducts = row1.FindControl("gvProducts") as GridView;

                                    for (int k = 0; k < gvProducts.Rows.Count; k++)
                                    {
                                       
                                        CheckBox chetest3 = gvProducts.Rows[k].FindControl("CheckBox3") as CheckBox;
                                        if (chetest3.Checked == true)
                                        {
                                            id = gvProducts.DataKeys[k].Value.ToString();
                                            string strchild = "insert into LevelMenuRelation(levelid,menuid)values('" + lvID + "','" + id + "')";

                                            SqlCommand cmd2 = new SqlCommand(strchild, conn);
                                            cmd2.ExecuteNonQuery();
                                        }

                                    }
                                    parentID = gridOrders.DataKeys[j].Value.ToString();
                                    string strParentUpdate = "insert into LevelMenuRelation(levelid,menuid)values('" + lvID + "','" + parentID + "')";

                                    SqlCommand cmdParent = new SqlCommand(strParentUpdate, conn);
                                    cmdParent.ExecuteNonQuery();

                                }


                            }

                        }
                        else
                        {
                            //left for modification this code need a modification bcz its similer to upper code
                            GridViewRow row = (chetest1.NamingContainer as GridViewRow);
                            GridView gridParent = row.FindControl("gvOrders") as GridView;

                            for (int j = 0; j < gridParent.Rows.Count; j++)
                            {
                                CheckBox chektest2 = gridParent.Rows[j].FindControl("CheckBox2") as CheckBox;

                                if (chektest2.Checked == true)
                                {
                                    GridViewRow row1 = (chektest2.NamingContainer as GridViewRow);
                                    GridView gridMenuItemsLastChild = row1.FindControl("gvProducts") as GridView;

                                    for (int k = 0; k < gridMenuItemsLastChild.Rows.Count; k++)
                                    {
                                        CheckBox chetest3 = gridMenuItemsLastChild.Rows[j].FindControl("CheckBox3") as CheckBox;

                                        if (chetest3.Checked == true)
                                        {
                                            id = gridMenuItemsLastChild.DataKeys[k].Value.ToString();
                                            string strchild = "insert into LevelMenuRelation(levelid,menuid)values('" + lvID + "','" + id + "')";

                                            SqlCommand cmd2 = new SqlCommand(strchild, conn);
                                            cmd2.ExecuteNonQuery();

                                        }
                                    }
                                    parentID = gridParent.DataKeys[j].Value.ToString();
                                    string strParentUpdate = "insert into LevelMenuRelation(levelid,menuid)values('" + lvID + "','" + parentID + "')";

                                    SqlCommand cmdParent = new SqlCommand(strParentUpdate, conn);
                                    cmdParent.ExecuteNonQuery();


                                }

                            }


                        }
                      
                        lblMessages.Visible = true;                     
                        lblMessages.Text = "Records saved successfully";
                        txtLevelCode.Text = "";
                        txtLevelDescrip.Text = "";
                        
                    }
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                conn.Close();
            }

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                CheckBox chkModules = (sender as CheckBox);
                if (chkModules.Checked == true)
                {
                    GridViewRow row = (chkModules.NamingContainer as GridViewRow);
                    GridView gridOrders = row.FindControl("gvOrders") as GridView;

                    for (int i = 0; i < gridOrders.Rows.Count; i++)
                    {
                        CheckBox chktest = gridOrders.Rows[i].FindControl("CheckBox2") as CheckBox;
                        chktest.Checked = true;
                        CheckBox2_CheckedChanged(chktest, new EventArgs());

                    }
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                }
                else
                {
                    GridViewRow row = (chkModules.NamingContainer as GridViewRow);
                    GridView gridOrders = row.FindControl("gvOrders") as GridView;

                    for (int i = 0; i < gridOrders.Rows.Count; i++)
                    {
                        CheckBox chktest = gridOrders.Rows[i].FindControl("CheckBox2") as CheckBox;
                        chktest.Checked = false;
                        CheckBox2_CheckedChanged(chktest, new EventArgs());

                    }

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

                }



            }
            catch (Exception ex)
            {

            }


        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                CheckBox chkInnercCek1 = (CheckBox)sender;
                if (chkInnercCek1.Checked == true)
                {
                    GridViewRow row = (chkInnercCek1.NamingContainer as GridViewRow);
                    GridView gridProducts = row.FindControl("gvProducts") as GridView;

                    for (int i = 0; i < gridProducts.Rows.Count; i++)
                    {

                        CheckBox chktest1 = gridProducts.Rows[i].FindControl("CheckBox3") as CheckBox;
                        chktest1.Checked = true;
                        CheckBox3_CheckedChanged(chktest1, new EventArgs());
                    }

                    //modules row
                    GridViewRow modulesRow = (chkInnercCek1.NamingContainer as GridViewRow);
                    GridView gvModules = (modulesRow.NamingContainer as GridView);
                    GridViewRow modulesRowCuurent = (gvModules.NamingContainer as GridViewRow);

                    CheckBox chechked1 = modulesRowCuurent.FindControl("CheckBox1") as CheckBox;
                    chechked1.Checked = true;


                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

                }
                else
                {

                    GridViewRow row = (chkInnercCek1.NamingContainer as GridViewRow);

                    GridView gridProducts = row.FindControl("gvProducts") as GridView;

                    for (int i = 0; i < gridProducts.Rows.Count; i++)
                    {

                        CheckBox chktest1 = gridProducts.Rows[i].FindControl("CheckBox3") as CheckBox;
                        if (chktest1.Checked == true)
                        {
                            chktest1.Checked = false;
                        }

                        // CheckBox3_CheckedChanged(chktest1, new EventArgs());
                    }

                    //parent row
                    GridViewRow gvorderRow = (chkInnercCek1.NamingContainer as GridViewRow);
                    GridView gvOrder = (gvorderRow.NamingContainer as GridView);
                    bool flagParentSelected = false;
                    for (int i = 0; i < gvOrder.Rows.Count; i++)
                    {

                        CheckBox chktest1 = gvOrder.Rows[i].FindControl("CheckBox2") as CheckBox;
                        if (chktest1.Checked == true && flagParentSelected == false)
                        {
                            flagParentSelected = true;

                        }

                    }
                    //modules row
                    GridViewRow modulesRowCuurent = (gvOrder.NamingContainer as GridViewRow);
                    CheckBox chechked1 = modulesRowCuurent.FindControl("CheckBox1") as CheckBox;
                    if (flagParentSelected == true)
                        chechked1.Checked = true;
                    else
                        chechked1.Checked = false;

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                }

            }
            catch (Exception ex)
            {


            }



        }
        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

            try
            {

                CheckBox chkInnercCek3 = (CheckBox)sender;

                if (chkInnercCek3.Checked == true)
                {
                    //parent row
                    GridViewRow row = (chkInnercCek3.NamingContainer as GridViewRow);
                    GridView gvProduct = (row.NamingContainer as GridView);
                    GridViewRow parentRow = (gvProduct.NamingContainer as GridViewRow);

                    CheckBox chechked2 = parentRow.FindControl("CheckBox2") as CheckBox;
                    chechked2.Checked = true;

                    //modules row
                    GridViewRow modulesRow = (chechked2.NamingContainer as GridViewRow);
                    GridView gvModules = (modulesRow.NamingContainer as GridView);
                    GridViewRow modulesRowCuurent = (gvModules.NamingContainer as GridViewRow);

                    CheckBox chechked1 = modulesRowCuurent.FindControl("CheckBox1") as CheckBox;
                    chechked1.Checked = true;

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                }
                else
                {

                    GridViewRow row = (chkInnercCek3.NamingContainer as GridViewRow);
                    GridView gvProduct = (row.NamingContainer as GridView);
                    bool flagChildSelected = false;
                    ///new code added by vaibhav 
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        CheckBox chechked3 = gvProduct.Rows[i].FindControl("CheckBox3") as CheckBox;
                        if (chechked3.Checked == true && flagChildSelected == false)
                        {
                            flagChildSelected = true;
                        }
                    }

                    //parent row
                    GridViewRow gvParentRowCuurent = (gvProduct.NamingContainer as GridViewRow);
                    GridView gvParent = (gvParentRowCuurent.NamingContainer as GridView);
                    bool flagParentSelected = false;

                    for (int i = 0; i < gvParent.Rows.Count; i++)
                    {
                        CheckBox chechked2 = gvParent.Rows[i].FindControl("CheckBox2") as CheckBox;
                        if (chechked2.Checked == true && flagParentSelected == false)
                        {
                            flagParentSelected = true;
                        }

                    }

                    if (flagChildSelected == false && flagParentSelected == false)
                    {



                        // All Child should be Unchecked and Parent also uncheced
                    }
                    if (flagChildSelected == false && flagParentSelected == true)
                    {
                        GridViewRow gvChildRow = (chkInnercCek3.NamingContainer as GridViewRow);
                        GridView gvChild = (gvChildRow.NamingContainer as GridView);
                        GridViewRow gvparentRow = (gvChild.NamingContainer as GridViewRow);
                        CheckBox chechkedParent2 = gvparentRow.FindControl("CheckBox2") as CheckBox;

                        if (chechkedParent2.Checked == true)
                            chechkedParent2.Checked = false;

                        GridViewRow gvorderRow = (chechkedParent2.NamingContainer as GridViewRow);
                        GridView gvOrder = (gvorderRow.NamingContainer as GridView);
                        bool flagISParentSelected = false;
                        for (int i = 0; i < gvOrder.Rows.Count; i++)
                        {

                            CheckBox chktest1 = gvOrder.Rows[i].FindControl("CheckBox2") as CheckBox;
                            if (chktest1.Checked == true && flagISParentSelected == false)
                            {
                                flagISParentSelected = true;

                            }

                        }
                        //modules row
                        GridViewRow modulesRowCuurent = (gvOrder.NamingContainer as GridViewRow);
                        CheckBox chechked1 = modulesRowCuurent.FindControl("CheckBox1") as CheckBox;
                        if (flagISParentSelected == true)
                            chechked1.Checked = true;
                        else
                            chechked1.Checked = false;

                    }

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                }

            }
            catch (Exception ex)
            {


            }




        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("LevelAccessView.aspx");
        }

    }
}