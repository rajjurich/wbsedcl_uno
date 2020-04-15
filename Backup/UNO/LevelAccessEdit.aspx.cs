using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Configuration;

namespace UNO
{
    public partial class LevelAccessEdit : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string levelID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                levelID = Request.QueryString["levelID"];
                if (!IsPostBack)
                {

                    setLevelTextBox(levelID);
                    gvCustomers.DataSource = GetData("select distinct modules from MenuTable");
                    gvCustomers.DataBind();


                }
                //setLevelTextBox(levelID);
            }
            catch (Exception ex)
            {

            }
            
        }
        public void setLevelTextBox(string levelID)
        {
            try
            {

                levelID = Request.QueryString["levelID"];
                string str = "select levelCode,levelName from levelmaster where id='" + levelID + "'";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(str, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtLevelCode.Text = dr["levelCode"].ToString();
                    txtLevelDescrip.Text = dr["levelName"].ToString();
                }
                dr.Close();
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
                //string customerId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
                //GridView gvOrders = row.FindControl("gvOrders") as GridView;
                //BindOrders(customerId, gvOrders);
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
            //gvOrders.DataSource = GetData(string.Format("select * from TE_Milestone where ProjectID='{0}'", customerId));
            gvOrders.DataSource = GetData(string.Format("Select id,name from MenuTable where Modules='" + customerId + "' and ParentMenuId is null "));
            ViewState["Modules"] = customerId;
            gvOrders.DataBind();
        }

        protected void OnOrdersGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView gvOrders = (sender as GridView);
            //gvOrders.PageIndex = e.NewPageIndex;
            //BindOrders(gvOrders.ToolTip, gvOrders);
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
                //int orderId = Convert.ToInt32((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
                //GridView gvProducts = row.FindControl("gvProducts") as GridView;
                //BindProducts(orderId, gvProducts);
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
            //gvProducts.DataSource = GetData(string.Format("select * from TE_WBS where MilestoneID='{0}'", orderId));
            gvProducts.DataSource = GetData(string.Format("Select id,name,url,ParentMenuId from MenuTable where Modules = '" + Modules + "' and ParentMenuId = " + orderId + ""));
            gvProducts.DataBind();
        }
        protected void OnProductsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView gvProducts = (sender as GridView);
            //gvProducts.PageIndex = e.NewPageIndex;
            //BindProducts(int.Parse(gvProducts.ToolTip), gvProducts);
        }
        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //GridView gvord = (sender as GridView);
                    //GridViewRow row = (gvord.NamingContainer as GridViewRow);
                    CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
                    CheckBox1.Attributes.Add("onclick", "javascript:MasterCheckChanged('" + CheckBox1.ClientID + "')");
                    string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                    BindOrders(customerId, gvOrders);
                    if ((txtLevelCode.Text.Trim().Equals("admin", StringComparison.CurrentCultureIgnoreCase)) || (txtLevelCode.Text.Trim().Equals("emp", StringComparison.CurrentCultureIgnoreCase)))
                        if (CheckBox1.Checked)
                            CheckBox1.Enabled = false;

                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox2 = (CheckBox)e.Row.FindControl("CheckBox2");
                    //CheckBox2.Attributes.Add("onclick", "javascript:ParentCheckChanged('" + CheckBox2.ClientID + "')");

                    GridView gvOrders = (sender as GridView);
                    GridViewRow row = (gvOrders.NamingContainer as GridViewRow);

                    string customerId = gvOrders.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvProducts = e.Row.FindControl("gvProducts") as GridView;
                    int childid = Convert.ToInt32(customerId);
                    BindProducts(childid, gvProducts);

                    //vaibhav edit code
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string strBindChekBoxEntry = "select a.MenuID from LevelMenuRelation a,MenuTable b where a.MenuID=b.Id and a.MenuID=" + customerId + " and a.LevelID='" + levelID + "'";
                    SqlCommand cmd = new SqlCommand(strBindChekBoxEntry, conn);
                    SqlDataReader drChkParentReader = cmd.ExecuteReader();


                    if (drChkParentReader.Read())
                    {
                        CheckBox bindChekcbox = e.Row.FindControl("CheckBox2") as CheckBox;
                        bindChekcbox.Checked = true;
                        CheckBox bindModuleChekcbox = row.FindControl("CheckBox1") as CheckBox;
                        bindModuleChekcbox.Checked = true;
                    }

                    drChkParentReader.Close();
                    //vaibhav end edit

                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                CheckBox bindChekcbox;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox3 = (CheckBox)e.Row.FindControl("CheckBox3");
                    //CheckBox3.Attributes.Add("onclick", "javascript:ChildCheckChanged('" + CheckBox3.ClientID + "')");

                    GridView gvChild = (sender as GridView);
                    //GridViewRow row = (gvord.NamingContainer as GridViewRow);

                    string childID = gvChild.DataKeys[e.Row.RowIndex].Value.ToString();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string strBindChekBoxEntry = "select a.MenuID from LevelMenuRelation a,MenuTable b where a.MenuID=b.Id and a.MenuID=" + childID + " and a.LevelID='" + levelID + "'";
                    SqlCommand cmd = new SqlCommand(strBindChekBoxEntry, conn);
                    SqlDataReader drChkParentReader = cmd.ExecuteReader();
                    if (drChkParentReader.Read())
                    {
                        bindChekcbox = e.Row.FindControl("CheckBox3") as CheckBox;
                        bindChekcbox.Checked = true;


                    }

                    drChkParentReader.Close();
                    // vaibhav end edit

                    //Added by Pooja Yadav 
                    if (e.Row.Cells[0].Text.ToString().Equals("Level", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bindChekcbox = e.Row.FindControl("CheckBox3") as CheckBox;
                        bindChekcbox.Enabled = false;
                    }




                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string levelCode = "", levelDescription = "";
                levelCode = txtLevelCode.Text;
                levelDescription = txtLevelDescrip.Text;

                // vaibhav delete code
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                // string insertQuery = "insert into LevelMaster(levelcode,levelname)values('" + levelCode + "','" + levelDescription + "')";

                string deleteQuery = "delete from LevelMenuRelation where LevelID='" + levelID + "'";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.ExecuteNonQuery();
                ///// end delete code

                //start update code vaibhav
                string updateQuery = "update levelMaster set levelName='" + (txtLevelDescrip.Text.Trim().First().ToString().ToUpper() + String.Join("", txtLevelDescrip.Text.Trim().Skip(1))) + "' where id='" + levelID + "' ";
                SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
                cmdUpdate.ExecuteNonQuery();
                //end update code vaibhav


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
                                        string strchild = "insert into LevelMenuRelation(levelid,menuid)values('" + levelID + "','" + id + "')";

                                        SqlCommand cmd2 = new SqlCommand(strchild, conn);
                                        cmd2.ExecuteNonQuery();

                                    }


                                }

                                parentID = gridOrders.DataKeys[j].Value.ToString();
                                string strParentUpdate = "insert into LevelMenuRelation(levelid,menuid)values('" + levelID + "','" + parentID + "')";

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
                                        string strchild = "insert into LevelMenuRelation(levelid,menuid)values('" + levelID + "','" + id + "')";

                                        SqlCommand cmd2 = new SqlCommand(strchild, conn);
                                        cmd2.ExecuteNonQuery();

                                    }
                                }
                                parentID = gridParent.DataKeys[j].Value.ToString();
                                string strParentUpdate = "insert into LevelMenuRelation(levelid,menuid)values('" + levelID + "','" + parentID + "')";

                                SqlCommand cmdParent = new SqlCommand(strParentUpdate, conn);
                                cmdParent.ExecuteNonQuery();


                            }

                        }


                    }
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Records modified successfully');</script>");


                    //ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Records modified successfully');window.location='LevelAccessView.aspx';", true);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record updated successfully";
                    
                  //  Response.Redirect("LevelAccessView.aspx?Message=" + lblMessage.Text + "");

                }
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
            }
            catch (Exception ex)
            {

            }

            //Response.Redirect("LevelAccessView.aspx");
        }

        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkModules = (sender as CheckBox);
        //    if (chkModules.Checked == true)
        //    {
        //        GridViewRow row = (chkModules.NamingContainer as GridViewRow);
        //        GridView gridOrders = row.FindControl("gvOrders") as GridView;

        //        for (int i = 0; i < gridOrders.Rows.Count; i++)
        //        {
        //            CheckBox chktest = gridOrders.Rows[i].FindControl("CheckBox2") as CheckBox;
        //            chktest.Checked = true;
        //            CheckBox2_CheckedChanged(chktest, new EventArgs());

        //        }

        //    }
        //    else
        //    {




        //    }

        //}
        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkInnercCek1 = (CheckBox)sender;
        //    if (chkInnercCek1.Checked == true)
        //    {
        //        GridViewRow row = (chkInnercCek1.NamingContainer as GridViewRow);
        //        GridView gridProducts = row.FindControl("gvProducts") as GridView;

        //        for (int i = 0; i < gridProducts.Rows.Count; i++)
        //        {

        //            CheckBox chktest1 = gridProducts.Rows[i].FindControl("CheckBox3") as CheckBox;
        //            chktest1.Checked = true;
        //            CheckBox3_CheckedChanged(chktest1, new EventArgs());
        //        }
        //    }


        //}
        //protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkInnercCek2 = (CheckBox)sender;

        //    if (chkInnercCek2.Checked == true)
        //    {
        //        GridViewRow row = (chkInnercCek2.NamingContainer as GridViewRow);
        //        GridView gridProducts = row.FindControl("gvOrders") as GridView;




        //    }




        //}

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                CheckBox chkModules = (sender as CheckBox);
                if (chkModules.Checked == true)
                {
                    GridViewRow row = (chkModules.NamingContainer as GridViewRow);
                    GridView gridOrders = row.FindControl("gvOrders") as GridView;
                    
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

                    for (int i = 0; i < gridOrders.Rows.Count; i++)
                    {
                        CheckBox chktest = gridOrders.Rows[i].FindControl("CheckBox2") as CheckBox;
                        chktest.Checked = true;
                        CheckBox2_CheckedChanged(chktest, new EventArgs());

                    }
                   
                }
                else
                {
                    GridViewRow row = (chkModules.NamingContainer as GridViewRow);
                    GridView gridOrders = row.FindControl("gvOrders") as GridView;

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                    
                    for (int i = 0; i < gridOrders.Rows.Count; i++)
                    {
                        CheckBox chktest = gridOrders.Rows[i].FindControl("CheckBox2") as CheckBox;
                        chktest.Checked = false;
                        CheckBox2_CheckedChanged(chktest, new EventArgs());

                    }
                   
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

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

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
                    
                }
                else
                {

                    GridViewRow row = (chkInnercCek1.NamingContainer as GridViewRow);

                    GridView gridProducts = row.FindControl("gvProducts") as GridView;

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
                    
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

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);

                    CheckBox chechked2 = parentRow.FindControl("CheckBox2") as CheckBox;
                    chechked2.Checked = true;

                    //modules row
                    GridViewRow modulesRow = (chechked2.NamingContainer as GridViewRow);
                    GridView gvModules = (modulesRow.NamingContainer as GridView);
                    GridViewRow modulesRowCuurent = (gvModules.NamingContainer as GridViewRow);

                    CheckBox chechked1 = modulesRowCuurent.FindControl("CheckBox1") as CheckBox;
                    chechked1.Checked = true;

                }
                else
                {

                    GridViewRow row = (chkInnercCek3.NamingContainer as GridViewRow);
                    GridView gvProduct = (row.NamingContainer as GridView);
                    bool flagChildSelected = false;
                    //new code added by vaibhav 
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Script", "xyz();", true);
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