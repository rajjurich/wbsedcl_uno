using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
namespace UNO
{
    public partial class VEH_Controller : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    binddata();
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }

        }
        public void binddata()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strGetData = "select * from veh_controller where isdeleted=0";
                SqlCommand cmd = new SqlCommand(strGetData, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvController.DataSource = dt;
                gvController.DataBind();

                DropDownList ddl = (DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvController.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvController.PageIndex + 1).ToString();
                Label lblcount = (Label)gvController.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvController.DataSource).Rows.Count.ToString() + " Records.";
                if (gvController.PageCount == 0)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvController.PageIndex + 1 == gvController.PageCount)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvController.PageIndex == 0)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvController.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvController.PageSize * gvController.PageIndex) + 1) + " to " + (gvController.PageSize * (gvController.PageIndex + 1));

                ((Label)gvController.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvController.PageSize * gvController.PageIndex) + 1) + " to " + (((gvController.PageSize * (gvController.PageIndex + 1)) - 10) + gvController.Rows.Count);

                gvController.BottomPagerRow.Visible = true;

                if (dt.Rows.Count != 0)
                {

                    btnSearch.Enabled = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //string strsql = "select id,vehicleName,entityId,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";
                string strsql = "select * from veh_controller where isdeleted=0";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtControllerIDSerch.Text.ToString() == "" && txtControllerName.Text.ToString() == "")
                {
                    gvController.DataSource = dt;
                    gvController.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"controllerDescription~" +txtControllerName.Text.Trim(), "S" },
                {"controllerID~" +txtControllerIDSerch.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvController.DataSource = _tempDT;
                    gvController.DataBind();
                }
                DropDownList ddl = (DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvController.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvController.PageIndex + 1).ToString();
                Label lblcount = (Label)gvController.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvController.DataSource).Rows.Count.ToString() + " Records.";
                if (gvController.PageCount == 0)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvController.PageIndex + 1 == gvController.PageCount)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvController.PageIndex == 0)
                {
                    ((Button)gvController.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvController.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvController.PageSize * gvController.PageIndex) + 1) + " to " + (gvController.PageSize * (gvController.PageIndex + 1));

                ((Label)gvController.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvController.PageSize * gvController.PageIndex) + 1) + " to " + (((gvController.PageSize * (gvController.PageIndex + 1)) - 10) + gvController.Rows.Count);

                gvController.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            mpeAddController.Show();
        }


        protected void btnAddController_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                SqlCommand cmd1 = new SqlCommand("sp_InsertController", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@location", SqlDbType.VarChar).Value = txtAddControllerLoc.Text;
                cmd1.Parameters.Add("@controllerIp", SqlDbType.VarChar).Value = txtAddControllerIp.Text;
                cmd1.Parameters.Add("@controllerID", SqlDbType.VarChar).Value = txtAddControllerID.Text;
                cmd1.Parameters.Add("@controllerDescription", SqlDbType.VarChar).Value = txtControllerDescriptionAdd.Text;
                cmd1.Parameters.Add("@ControllerStatus", SqlDbType.VarChar).Value = ddlControllerStatusAdd.SelectedValue;

            


                cmd1.ExecuteNonQuery();
                binddata();
                txtAddControllerLoc.Text = "";
                txtAddControllerIp.Text = "";
                txtAddControllerID.Text = "";
                txtControllerDescriptionAdd.Text = "";
                ddlControllerStatusAdd.SelectedValue = (0).ToString();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblMsg.Text = "Record Saved Successfully";
                mpeAddController.Hide();
            }
            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");

            }

        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            mpeAddController.Hide();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string cntrlId = ViewState["RowID"].ToString();

                SqlCommand cmd1 = new SqlCommand("spUpdateController", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@ID", SqlDbType.BigInt).Value = cntrlId;
                cmd1.Parameters.Add("@ControllerStatus", SqlDbType.VarChar).Value = ddlEditStatus.SelectedItem.Text;
                cmd1.Parameters.Add("@ControllerIP", SqlDbType.VarChar).Value = txtEdiControllerIp.Text;
                cmd1.Parameters.Add("@ControllerLocation", SqlDbType.VarChar).Value = txtEditControllerLoc.Text;
                cmd1.Parameters.Add("@ControllerID", SqlDbType.VarChar).Value = txtEditControllerID.Text;
                cmd1.Parameters.Add("@ControllerDesscription", SqlDbType.VarChar).Value = txtEditControllerDescrption.Text;
                
                cmd1.ExecuteNonQuery();
                binddata();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblMsg.Text = "Record Successfully Saved";
                mpeEditController.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            mpeEditController.Hide();
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = Convert.ToInt32(((DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                binddata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = gvController.PageIndex - 1;
                binddata(); ;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = gvController.PageIndex + 1;
                binddata(); ;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }
        }

        protected void gvController_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "Modify")
                {
                    string id = e.CommandArgument.ToString();
                    ViewState["RowID"] = id;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string selectData = "select * from veh_controller where id=" + id + "";
                    SqlCommand cmdUpdate = new SqlCommand(selectData, conn);
                    SqlDataReader dr = cmdUpdate.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["ControllerStatus"].ToString() == "Enabled")
                            ddlEditStatus.SelectedValue = "Enabled";
                        else
                            ddlEditStatus.SelectedValue = "Disabled";


                        txtEditControllerLoc.Text = dr["location"].ToString();
                        txtEditControllerID.Text = dr["controllerID"].ToString();
                        txtEdiControllerIp.Text = dr["controllerIp"].ToString();
                        txtEditControllerDescrption.Text = dr["controllerDescription"].ToString();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    lblMsg.Text = "";
                    mpeEditController.Show();

                }
                if (e.CommandName == "Remove")
                {
                    string id = e.CommandArgument.ToString();
                    ViewState["deleteRowID"] = id;
                    lblMsg.Text = "";
                    mpeDelController.Show();
                }
                if (e.CommandName == "ChangeStatus")
                {
                    lblMsg.Text = "";
                    string status = "", update = "";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string id = e.CommandArgument.ToString();
                    LinkButton lbtnAction;
                    GridViewRow row;
                    row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    lbtnAction = gvController.Rows[row.RowIndex].FindControl("lnlStatus") as LinkButton;
                    status = lbtnAction.Text;

                    if (status == "Enabled")
                    {
                        update = "update veh_controller set ControllerStatus='Disabled' where id='" + id + "'";
                        lbtnAction.ToolTip = "Click here to disable record";
                    }
                    else
                    {
                        lbtnAction.ToolTip = "Click here to Enable record";
                        update = "update veh_controller set ControllerStatus='Enabled' where id='" + id + "'";

                    }
                    SqlCommand cmdUpdate = new SqlCommand(update, conn);
                    cmdUpdate.ExecuteNonQuery();
                    binddata();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                if (e.CommandName == "Reinit")
                {
                    try
                    {
                        string controllerId=e.CommandArgument.ToString();
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("sp_ReinitializeFlag", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CtrlID",controllerId);
                        cmd.ExecuteNonQuery();

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

            }

            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");

            }


        }

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
            mpeDelController.Hide();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                string delid = ViewState["deleteRowID"].ToString();

                string strDelQuery = "update veh_controller set isDeleted='1',  isDeletedDate=getdate() where id=" + delid + "";
                SqlCommand delcmd = new SqlCommand(strDelQuery, conn);
                delcmd.ExecuteNonQuery();
                binddata();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblMsg.Text = "Record Successfully Deleted";
                mpeDelController.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AddController");
            }
        }
    }
}