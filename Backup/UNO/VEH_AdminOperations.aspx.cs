using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;


namespace UNO
{
    public partial class VEH_AdminOperations : System.Web.UI.Page
    {
        bool flagVisitorEnabled = false;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    flagVisitorEnabled = GetVisitorStatus();
                    if (flagVisitorEnabled != true)
                    {
                        txtVisitorIDDisabled.Visible = false;
                        txtVisitorIDEnabled.Visible = false;
                        gvEnabled.Columns[1].Visible = false;
                        gvDisabled.Columns[1].Visible = false;
                    }
                    bindEnabledata();
                    bindDisabledata();
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }
        public bool GetVisitorStatus()
        {
            bool flag = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strGetData = "select value from dbo.ent_params where identifier='VistorEnabled' and MODULE='VEH'";
                SqlCommand cmd6 = new SqlCommand(strGetData, conn);
                SqlDataReader dr = cmd6.ExecuteReader();
                if (dr.Read())
                {
                    flag = Convert.ToBoolean(dr["value"]);
                }
                dr.Close();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return flag;
            }
            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");

            }

            return flag;
        }
        public void bindEnabledata()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd1 = new SqlCommand("spGetDisableRecords", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //string strGetData = "select id,vehicleName,entityId,userType,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";
                //SqlCommand cmd = new SqlCommand(strGetData, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                gvEnabled.DataSource = dt;
                gvEnabled.DataBind();

                DropDownList ddl = (DropDownList)gvEnabled.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEnabled.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEnabled.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEnabled.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEnabled.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEnabled.PageCount == 0)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEnabled.PageIndex + 1 == gvEnabled.PageCount)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEnabled.PageIndex == 0)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEnabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEnabled.PageSize * gvEnabled.PageIndex) + 1) + " to " + (gvEnabled.PageSize * (gvEnabled.PageIndex + 1));

                ((Label)gvEnabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEnabled.PageSize * gvEnabled.PageIndex) + 1) + " to " + (((gvEnabled.PageSize * (gvEnabled.PageIndex + 1)) - 10) + gvEnabled.Rows.Count);

                gvEnabled.BottomPagerRow.Visible = true;

                if (dt.Rows.Count != 0)
                {

                    btnSearch.Enabled = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                }


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");

            }



        }
        public void bindDisabledata()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd1 = new SqlCommand("spGetEnableRecords", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                //string strGetData = "select id,vehicleName,entityId,userType,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";
                //SqlCommand cmd = new SqlCommand(strGetData, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                gvDisabled.DataSource = dt;
                gvDisabled.DataBind();

                DropDownList ddl = (DropDownList)gvDisabled.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvDisabled.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvDisabled.PageIndex + 1).ToString();
                Label lblcount = (Label)gvDisabled.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvDisabled.DataSource).Rows.Count.ToString() + " Records.";
                if (gvDisabled.PageCount == 0)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvDisabled.PageIndex + 1 == gvDisabled.PageCount)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvDisabled.PageIndex == 0)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvDisabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvDisabled.PageSize * gvDisabled.PageIndex) + 1) + " to " + (gvDisabled.PageSize * (gvDisabled.PageIndex + 1));

                ((Label)gvDisabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvDisabled.PageSize * gvDisabled.PageIndex) + 1) + " to " + (((gvDisabled.PageSize * (gvDisabled.PageIndex + 1)) - 10) + gvDisabled.Rows.Count);

                gvDisabled.BottomPagerRow.Visible = true;

                if (dt.Rows.Count != 0)
                {

                    btnSearch.Enabled = true;
                }
                else
                {

                    btnSearch.Enabled = false;
                }

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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");

            }



        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEnabled.PageIndex = Convert.ToInt32(((DropDownList)gvEnabled.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindEnabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEnabled.PageIndex = gvEnabled.PageIndex - 1;
                bindEnabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEnabled.PageIndex = gvEnabled.PageIndex + 1;
                bindEnabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

               // string strsql = "select id,vehicleName,entityId,userType,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";

                SqlCommand cmd = new SqlCommand("spGetDisableRecords", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtRFIDEnabled.Text.ToString() == "" && txtemployeeIDEnabled.Text.ToString() == "" && txtVehregNoEnabled.Text == "" && txtVisitorIDEnabled.Text.ToString() == "")
                {
                    gvEnabled.DataSource = dt;
                    gvEnabled.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"rfid~" +txtRFIDEnabled.Text.Trim(), "S" },
               {"entityId~" +txtemployeeIDEnabled.Text.Trim(), "I" },
                {"entityId~" +txtVisitorIDEnabled.Text.Trim(), "I" },
                {"VehicleRegistrationNumber~" +txtVehregNoEnabled.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEnabled.DataSource = _tempDT;
                    gvEnabled.DataBind();
                }
                DropDownList ddl = (DropDownList)gvEnabled.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEnabled.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEnabled.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEnabled.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEnabled.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEnabled.PageCount == 0)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEnabled.PageIndex + 1 == gvEnabled.PageCount)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEnabled.PageIndex == 0)
                {
                    ((Button)gvEnabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEnabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEnabled.PageSize * gvEnabled.PageIndex) + 1) + " to " + (gvEnabled.PageSize * (gvEnabled.PageIndex + 1));

                ((Label)gvEnabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEnabled.PageSize * gvEnabled.PageIndex) + 1) + " to " + (((gvEnabled.PageSize * (gvEnabled.PageIndex + 1)) - 10) + gvEnabled.Rows.Count);

                gvEnabled.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void ChangePageDisbled(object sender, EventArgs e)
        {
            try
            {
                gvDisabled.PageIndex = Convert.ToInt32(((DropDownList)gvDisabled.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
               
                bindDisabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void gvPreviousDisbled(object sender, EventArgs e)
        {
            try
            {
                gvDisabled.PageIndex = gvDisabled.PageIndex - 1;
            
                bindDisabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void gvNextDisbled(object sender, EventArgs e)
        {
            try
            {
                gvDisabled.PageIndex = gvDisabled.PageIndex + 1;
           
                bindDisabledata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }
        protected void btnSearchDisbled_Click(object sender, EventArgs e)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetEnableRecords", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtRFIDDisabled.Text.ToString() == "" && txtemployeeIDDisabled.Text.ToString() == "" && txtVehregNoDisabled.Text == "" && txtVisitorIDDisabled.Text == "")
                {
                    gvDisabled.DataSource = dt;
                    gvDisabled.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"rfid~" +txtRFIDDisabled.Text.Trim(), "S" },
                {"entityId~" +txtemployeeIDDisabled.Text.Trim(), "I" },
                {"entityId~" +txtVisitorIDDisabled.Text.Trim(), "I" },
                {"VehicleRegistrationNumber~" +txtVehregNoDisabled.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvDisabled.DataSource = _tempDT;
                    gvDisabled.DataBind();
                }
                DropDownList ddl = (DropDownList)gvDisabled.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvDisabled.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvDisabled.PageIndex + 1).ToString();
                Label lblcount = (Label)gvDisabled.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvDisabled.DataSource).Rows.Count.ToString() + " Records.";
                if (gvDisabled.PageCount == 0)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvDisabled.PageIndex + 1 == gvDisabled.PageCount)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvDisabled.PageIndex == 0)
                {
                    ((Button)gvDisabled.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvDisabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvDisabled.PageSize * gvDisabled.PageIndex) + 1) + " to " + (gvDisabled.PageSize * (gvDisabled.PageIndex + 1));

                ((Label)gvDisabled.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvDisabled.PageSize * gvDisabled.PageIndex) + 1) + " to " + (((gvDisabled.PageSize * (gvDisabled.PageIndex + 1)) - 10) + gvDisabled.Rows.Count);

                gvDisabled.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void btnDisabled_Click(object sender, EventArgs e)
        {
            try
            {

                bool flacgCheked = false;
                for (int i = 0; i < gvDisabled.Rows.Count; i++)
                {
                    CheckBox chkDisabledTemp = gvDisabled.Rows[i].FindControl("chkDisabled") as CheckBox;
                    if (chkDisabledTemp.Checked == true)
                    {
                        flacgCheked = true;
                    }
                }
                if (flacgCheked == true)
                    mpeDisabled.Show();
                else
                    mpeNonChekedMsg.Show();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }

        }

        protected void btnEnabled_Click(object sender, EventArgs e)
        {
            try
            {

                bool flacgCheked = false;
                for (int i = 0; i < gvEnabled.Rows.Count; i++)
                {
                    CheckBox chkEnabledTemp = gvEnabled.Rows[i].FindControl("chkEnabled") as CheckBox;
                    if (chkEnabledTemp.Checked == true)
                    {
                        flacgCheked = true;
                    }
                }
                if (flacgCheked == true)
                    mpeEnabled.Show(); 
                else
                    mpeNonChekedMsg.Show();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void btnEnabledPnl_Click(object sender, EventArgs e)
        {
        try
            {
             
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                for (int i = 0; i < gvEnabled.Rows.Count; i++)
                {
                    CheckBox chkEnabledTemp = gvEnabled.Rows[i].FindControl("chkEnabled") as CheckBox;
                    if (chkEnabledTemp.Checked == true)
                    {
                     
                        string RFID = gvEnabled.Rows[i].Cells[4].Text;
                        string entityID = gvEnabled.Rows[i].Cells[3].Text;
                        string VehicleRegNumber = gvEnabled.Rows[i].Cells[6].Text;

                        string UpdateQuery = "update veh_vehicleEnrollment set isEnabled=0 where rfId='" + RFID + "' and entityId='" + entityID + "' and VehicleRegistrationNumber='" + VehicleRegNumber + "'";
                        SqlCommand cmd = new SqlCommand(UpdateQuery, conn);
                        cmd.ExecuteNonQuery();
                    
                    }
                }
                bindEnabledata();
                bindDisabledata();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                mpeEnabled.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void btnDisabledPnl_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                for (int i = 0; i < gvDisabled.Rows.Count; i++)
                {
                    CheckBox chkDisabledTemp = gvDisabled.Rows[i].FindControl("chkDisabled") as CheckBox;
                    if (chkDisabledTemp.Checked == true)
                    {
                        string RFID = gvDisabled.Rows[i].Cells[4].Text;
                        string entityID = gvDisabled.Rows[i].Cells[3].Text;
                        string VehicleRegNumber = gvDisabled.Rows[i].Cells[6].Text;

                        string UpdateQuery = "update veh_vehicleEnrollment set isEnabled=1 where rfId='" + RFID + "' and entityId='" + entityID + "' and VehicleRegistrationNumber='" + VehicleRegNumber + "'";
                        SqlCommand cmd = new SqlCommand(UpdateQuery, conn);
                        cmd.ExecuteNonQuery();
                     
                    }
                }
                bindEnabledata();
                bindDisabledata();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                mpeDisabled.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleAdministration");
            }
        }

        protected void btn_cl_Click(object sender, EventArgs e)
        {
            mpeEnabled.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpeDisabled.Hide();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            mpeNonChekedMsg.Hide();
        }

    }
}