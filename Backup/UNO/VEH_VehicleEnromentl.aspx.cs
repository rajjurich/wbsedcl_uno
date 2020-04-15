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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using System.Drawing;
namespace UNO
{
    public partial class VehicleEnromentl : System.Web.UI.Page
    {
        bool flagVisitorEnabled = false;
        static string employeeVisitorFlag;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (ddlType0.SelectedItem.Text == "Employee")
                {
                    Label9.Text = "Employee";
                    employeeVisitorFlag = "Employee";
                }
                else
                {
                    Label9.Text = "Department";
                    employeeVisitorFlag = "Visitor";
                }

                if (!IsPostBack)
                {
                    flagVisitorEnabled = GetVisitorStatus();
                    if (flagVisitorEnabled != true)
                    {
                        ddlType0.SelectedIndex = 0;
                        ddlType0.Enabled = false;
                        ddlEditType.SelectedIndex = 0;
                        ddlEditType.Enabled = false;
                    }
                    bindControllerGvAdd();
                    bindControllerGvEdit();
                    binddata();
                    SetTagInventory();
                    txtEditRFID.Enabled = false;
                    fillDllEntity("EMP");
                }
                //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>start();</script>");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetdataByName(string prefixText, int count, string contextKey)
        {
            List<String> EMPlist = new List<String>();
            List<String> tempList = new List<String>();
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
                //Make your database connection here
                string strSQL = "select    EPD_FIRST_NAME+ '' + isnull (EPD_MIDDLE_NAME,'') + ''+ isnull(EPD_LAST_NAME,'') +'      |      '+EPD_EMPID as EPD_EMPID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_FIRST_NAME like '" + prefixText + "%'   order by EPD_EMPID,EPD_FIRST_NAME";
                //Get data in datatable 
                if (dt != null)
                {
                    cn1.Open();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, cn1);
                    da.Fill(dt);
                    cn1.Close();
                }
               
                foreach (DataRow dr in dt.Rows)
                {
                    EMPlist.Add(dr["EPD_EMPID"].ToString());
                }


                //List<String> Visitorlist = new List<String>();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    EMPlist.Add(dr["EPD_EMPID"].ToString());
                //}
                //if (employeeVisitorFlag == "Employee")
                //{
                //    return EMPlist;
                //}
                //else
                //{
                //    return Visitorlist;
                //}

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
            return EMPlist;
        }
        public bool GetVisitorStatus()
        {
            bool flag = false;
            try
            {
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //string strGetData = "select value from dbo.ent_params where identifier='VistorEnabled' and MODULE='VEH'";
                //SqlCommand cmd6 = new SqlCommand(strGetData, conn);
                //SqlDataReader dr = cmd6.ExecuteReader();
                //if (dr.Read())
                //{
                //    flag = Convert.ToBoolean(dr["value"]);
                //}
                //dr.Close();
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                return true;
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
        public void binddata()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //string strGetData = "select id,vehicleName,entityId,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";
                SqlCommand cmd = new SqlCommand("sp_fill_vehicleEnrollment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvVehicleEntries.DataSource = dt;
                gvVehicleEntries.DataBind();

                DropDownList ddl = (DropDownList)gvVehicleEntries.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvVehicleEntries.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvVehicleEntries.PageIndex + 1).ToString();
                Label lblcount = (Label)gvVehicleEntries.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvVehicleEntries.DataSource).Rows.Count.ToString() + " Records.";
                if (gvVehicleEntries.PageCount == 0)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVehicleEntries.PageIndex + 1 == gvVehicleEntries.PageCount)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVehicleEntries.PageIndex == 0)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvVehicleEntries.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVehicleEntries.PageSize * gvVehicleEntries.PageIndex) + 1) + " to " + (gvVehicleEntries.PageSize * (gvVehicleEntries.PageIndex + 1));

                ((Label)gvVehicleEntries.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVehicleEntries.PageSize * gvVehicleEntries.PageIndex) + 1) + " to " + (((gvVehicleEntries.PageSize * (gvVehicleEntries.PageIndex + 1)) - 10) + gvVehicleEntries.Rows.Count);

                gvVehicleEntries.BottomPagerRow.Visible = true;

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
        public void fillDepDll()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
             SqlCommand cmd = new SqlCommand("SELECT OCE_DESCRIPTION,OCE_ID FROM  ENT_ORG_COMMON_ENTITIES WHERE CEM_ENTITY_ID='DEP' AND OCE_ISDELETED=0", conn);
          
                    DataTable dt = new DataTable();
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(dt);
                    ddlSelectedDep.DataSource = dt;
                    ddlSelectedDep.DataTextField = "OCE_DESCRIPTION";
                    ddlSelectedDep.DataValueField = "OCE_ID";
                    ddlSelectedDep.DataBind();
                    ddlSelectedDep.Items.Add(new ListItem("Select"));
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
        
        
        }

        public void fillDllEntity(string type)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (type == "EMP")
                {
                    SqlCommand cmd = new SqlCommand("SELECT EPD_FIRST_NAME +'  '+EPD_LAST_NAME as name,EPD_EMPID  FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EPD_ISDELETED=0", conn);
            
                    DataTable dt = new DataTable();
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(dt);
                    txtempVistorId.DataSource = dt;
                    txtempVistorId.DataTextField = "name";
                    txtempVistorId.DataValueField = "EPD_EMPID";
                    txtempVistorId.DataBind();

                    //DropDownList1.DataSource = dt;
                    //DropDownList1.DataTextField = "name";
                    //DropDownList1.DataValueField = "EPD_EMPID";
                    //DropDownList1.DataBind();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT OCE_DESCRIPTION,OCE_ID FROM  ENT_ORG_COMMON_ENTITIES WHERE CEM_ENTITY_ID='DEP' AND OCE_ISDELETED=0", conn);
          
                    DataTable dt = new DataTable();
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(dt);
                    ddlSelectedDep.DataSource = dt;
                    ddlSelectedDep.DataTextField = "OCE_DESCRIPTION";
                    ddlSelectedDep.DataValueField = "OCE_ID";
                    ddlSelectedDep.DataBind();
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
            }
        
        
        
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                SqlCommand cmd1 = new SqlCommand("spInsertVehicleEntry", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@vehicleName", SqlDbType.VarChar).Value = txtVehicleName.Text;
                cmd1.Parameters.Add("@model", SqlDbType.VarChar).Value = txtModelName.Text;
                cmd1.Parameters.Add("@make", SqlDbType.VarChar).Value = txtMake.Text;
                cmd1.Parameters.Add("@usertype", SqlDbType.VarChar).Value = ddlType0.SelectedItem.Text;
                cmd1.Parameters.Add("@entityID", SqlDbType.VarChar).Value = txtempVistorId.Text;
              

                cmd1.Parameters.Add("@RFID", SqlDbType.VarChar).Value = txtRFID.Text;
              //  cmd1.Parameters.Add("@RFIDvalidityDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtRFID_ValidityDate.Text).ToString("MM/dd/yyyy");
              //  cmd1.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime).Value = System.DateTime.Now.ToString("MM/dd/yyyy");
                cmd1.Parameters.Add("@RFIDvalidityDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtRFID_ValidityDate.Text, "dd/MM/yyyy", null);
                //cmd1.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime).Value = System.DateTime.Now.ToString("MM/dd/yyyy");
                cmd1.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime).Value = DateTime.ParseExact(System.DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy",null);//.ToString("MM/dd/yyyy");
                cmd1.Parameters.Add("@VehRegNumber", SqlDbType.VarChar).Value = txtRegNumber.Text;
                cmd1.Parameters.Add("@purpose", SqlDbType.VarChar).Value = txtxPurpose.Text;
                cmd1.Parameters.Add("@IShhue_Date", SqlDbType.DateTime).Value = DateTime.ParseExact(txtRFID_IssueDate.Text, "dd/MM/yyyy", null);//.ToString("MM/dd/yyyy");
                cmd1.Parameters.AddWithValue("@vehicleid", 0).Direction = ParameterDirection.Output;


                cmd1.ExecuteNonQuery();
                string vehicleID = cmd1.Parameters["@vehicleid"].Value.ToString();
                string RFID = txtRFID.Text;

                StringBuilder sb = new StringBuilder();
                string cntrID, cntrlIP;
                sb.Clear();
                sb.Append("<RFIDInventory>");

                for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
                {
                    CheckBox chkAdd = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                    string controllerID = gvControllerAdd.Rows[i].Cells[1].Text;
                    cntrID = gvControllerAdd.Rows[i].Cells[2].Text;
                    cntrlIP = gvControllerAdd.Rows[i].Cells[3].Text;
                    if (chkAdd.Checked == true)
                    {
                        //string insertAssignControllers = "insert into Veh_assignController(RFID,ControllerID,vehicleEnrollementID)values('" + RFID + "','" + controllerID + "','" + vehicleID + "')";
                        //SqlCommand cmd2 = new SqlCommand(insertAssignControllers, conn);
                        //cmd2.ExecuteNonQuery();

                        sb.Append("<Transaction>");
                        sb.Append("<RFID>" + RFID + "</RFID>");
                        sb.Append("<ControllerID>" + controllerID + "</ControllerID>");
                        sb.Append("<vehicleEnrollementID>" + vehicleID + "</vehicleEnrollementID>");
                        sb.Append("<cntrID>" + cntrID + "</cntrID>");
                        sb.Append("<ControllerIP>" + cntrlIP + "</ControllerIP>");

                        sb.Append("</Transaction>");


                    }
                }
                sb.Append("</RFIDInventory>");
                string s = sb.ToString();


                SqlCommand cmd = new SqlCommand("veh_spAssignControllerInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = sb.ToString();
                cmd.ExecuteNonQuery();
                
                binddata();
                //txtempVistorId.Text = "";
                txtMake.Text = "";
                txtModelName.Text = "";
                txtRegNumber.Text = "";
                txtRFID.Text = "";
                txtxPurpose.Text = "";
                txtRFID_ValidityDate.Text = "";
                txtVehicleName.Text = "";
                for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
                {
                    CheckBox chkClear = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                    chkClear.Checked = false;
                }
                lblMsg.Text = "Record Saved Successfully";
                mpeAddVehicle.Hide();
              
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Saved Successfully');</script>");
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");

            }
        }
        public void getReaderData()
        {
            try
            {




            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");

            }


        }
        public void SetTagInventory()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_GetTagInventory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    lblIsshued.Text = dt.Rows[0]["Issued"].ToString();
                    lblInventory.Text = dt.Rows[0]["Inventory"].ToString();
                    lblTotalTags.Text = dt.Rows[0]["totalTags"].ToString();
                    lblEnabled.Text = dt.Rows[0]["EnabledTags"].ToString();
                    lblDisabled.Text = dt.Rows[0]["DisabledTags"].ToString();
                }
                else
                {
                   lblIsshued.Text = "0";
                    lblInventory.Text = "0";
                    lblTotalTags.Text = "0";
                    lblEnabled.Text = "0";
                    lblDisabled.Text = "0";
                
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
            }
           
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType0.SelectedItem.Text == "Employee")
            {
                employeeVisitorFlag = "Employee";
                txtempVistorId.Focus();
            }
            else
            {
                employeeVisitorFlag = "Visitor";
                txtempVistorId.Focus();
            }


            if (ddlType0.SelectedValue == "0")
            {
                //trDep.Style.Add("display", "none");
                //ddlDep.Visible = false;
                ddlSelectedDep.Style.Add("visibility", "hidden");
                lblDept.Style.Add("visibility", "hidden");
                txtempVistorId.Enabled = true;
                fillDllEntity("EMP");
            }
            else
            {
                txtempVistorId.Enabled = false;
                //trDep.Style.Add("display", "inline");
                //trDep.Style.Add("visibility", "visible");
                ddlSelectedDep.Style.Add("visibility", "visible");
                lblDept.Style.Add("visibility", "visible");
             //ddlDep.Visible = true;
                //fillDllEntity("DEP");
                fillDepDll();
            }
         }
        public void fillddl(string deptId)
        { 
        if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            SqlCommand cmd = new SqlCommand("SELECT EPD_FIRST_NAME +'  '+EPD_LAST_NAME as name,EPD_EMPID  FROM ENT_EMPLOYEE_PERSONAL_DTLS epd INNER JOIN  ENT_EMPLOYEE_OFFICIAL_DTLS eod ON epd.EPD_EMPID=eod.EoD_EMPID WHERE eod.EOD_DEPARTMENT_ID='" + deptId + "' and EPD_ISDELETED=0", conn);
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(dt);
            txtempVistorId.DataSource = dt;
            txtempVistorId.DataTextField = "name";
            txtempVistorId.DataValueField = "EPD_EMPID";
            txtempVistorId.DataBind();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvVehicleEntries.PageIndex = Convert.ToInt32(((DropDownList)gvVehicleEntries.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                binddata();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvVehicleEntries.PageIndex = gvVehicleEntries.PageIndex - 1;
                binddata(); ;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvVehicleEntries.PageIndex = gvVehicleEntries.PageIndex + 1;
                binddata(); ;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("sp_fill_vehicleEnrollment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvVehicleEntries.DataSource = dt;
                    gvVehicleEntries.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"entityId~" +txtUserID.Text.Trim(), "I" },
                {"VehicleRegistrationNumber~" +txtLevelID.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvVehicleEntries.DataSource = _tempDT;
                    gvVehicleEntries.DataBind();
                }
                DropDownList ddl = (DropDownList)gvVehicleEntries.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvVehicleEntries.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvVehicleEntries.PageIndex + 1).ToString();
                Label lblcount = (Label)gvVehicleEntries.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvVehicleEntries.DataSource).Rows.Count.ToString() + " Records.";
                if (gvVehicleEntries.PageCount == 0)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVehicleEntries.PageIndex + 1 == gvVehicleEntries.PageCount)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVehicleEntries.PageIndex == 0)
                {
                    ((Button)gvVehicleEntries.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvVehicleEntries.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVehicleEntries.PageSize * gvVehicleEntries.PageIndex) + 1) + " to " + (gvVehicleEntries.PageSize * (gvVehicleEntries.PageIndex + 1));

                ((Label)gvVehicleEntries.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVehicleEntries.PageSize * gvVehicleEntries.PageIndex) + 1) + " to " + (((gvVehicleEntries.PageSize * (gvVehicleEntries.PageIndex + 1)) - 10) + gvVehicleEntries.Rows.Count);

                gvVehicleEntries.BottomPagerRow.Visible = true;

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
         
            ddlSelectedDep.Style.Add("visibility", "hidden");
            lblDept.Style.Add("visibility", "hidden");
            mpeAddVehicle.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
            txtempVistorId.SelectedIndex = 0;
            txtMake.Text = "";
            txtModelName.Text = "";
            txtRegNumber.Text = "";
            txtRFID.Text = "";
            txtxPurpose.Text = "";
            txtRFID_ValidityDate.Text = "";
            txtVehicleName.Text = "";
            lblMsg.Text = "";
            txtempVistorId.SelectedIndex = 0;
            for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
            {
                CheckBox chkClear = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                chkClear.Checked = false;
            }
            mpeAddVehicle.Hide();

        }

        protected void gvVehicleEntries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try 
            {
                if (e.CommandName == "VehHistory")
                {
                    string id = e.CommandArgument.ToString();
                    getVehicleData(id);
                    //to fill the headers
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_GetVehEntity", conn);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    //cmd.Parameters.AddWithValue("@EntityId", id);
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dap.Fill(dt);
                    //
                    if(dt.Rows.Count >0)
                    {
                        lblVehicleDescription.Text = dt.Rows[0]["vehicleName"].ToString();
                        lblVehId.Text = dt.Rows[0]["VehicleRegistrationNumber"].ToString();
                        VehHolder.Text = dt.Rows[0]["Empname"].ToString();
                    }
                    //
                    mpeHistory.Show();
                    //mpeVehHistory.Show();
                }
                if (e.CommandName == "VehData")
                {
                    string id = e.CommandArgument.ToString();
                  
                    getVehicleSpecifficData(id);

                    //string str = "select * from veh_vehicleEnrollment where id=" + id + "";
                    
                    //if (conn.State == ConnectionState.Closed)
                    //{
                    //    conn.Open();
                    //}
                    //SqlCommand cmd = new SqlCommand(str,conn);
                    //SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //dap.Fill(dt);
                   

                    mpeVehData.Show();
                }

                if (e.CommandName == "Modify")
                {
                    string vehicleid = e.CommandArgument.ToString();
                    ViewState["RowID"] = vehicleid;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string selectData = "select userType,VehicleRegistrationNumber,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate ,CONVERT(varchar(10),isshue_date,103) as rfidIssueDate ,rfId,make,model,vehicleName,purpose,entityId from veh_vehicleEnrollment where id=" + vehicleid + "";
                    SqlCommand cmdUpdate = new SqlCommand(selectData, conn);
                    SqlDataReader dr = cmdUpdate.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["userType"].ToString() == "Employee")
                            ddlEditType.SelectedValue = (0).ToString();
                        else
                            ddlEditType.SelectedValue = (1).ToString();


                        txtEditRegNumber.Text = dr["VehicleRegistrationNumber"].ToString();
                        txtEditRFID_IssueDate.Text = dr["rfidIssueDate"].ToString();

                        txtEditRFID_ValidityDate.Text = dr["rfidValidityDate"].ToString();
                        txtEditRFID.Text = dr["rfId"].ToString();
                        txtEditMake.Text = dr["make"].ToString();
                        txtEditModelName.Text = dr["model"].ToString();
                        txtEditVehicleName.Text = dr["vehicleName"].ToString();
                        txtEditPurpose.Text = dr["purpose"].ToString();
                        txtEditempVistorId.Text = dr["entityId"].ToString();
                    }
                    dr.Close();
                    //////to chek existing chekboxes---start
                    string cntrlID;
                    string SelectAssignControllers = "select controllerid from Veh_assignController where vehicleEnrollementID='" + vehicleid + "'";
                    SqlCommand cmdChekExistsEntries = new SqlCommand(SelectAssignControllers, conn);
                    SqlDataReader Reader = cmdChekExistsEntries.ExecuteReader();
                    while (Reader.Read())
                    {
                        string ExistsControllerID = Reader["controllerid"].ToString();
                        for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
                        {
                            cntrlID = gvControllerEdit.DataKeys[i].Value.ToString();
                            CheckBox chk = gvControllerEdit.Rows[i].FindControl("chkControllerEdit") as CheckBox;
                            if (cntrlID == ExistsControllerID)
                            {
                                chk.Checked = true;
                            }

                        }
                    }
                    Reader.Close();
                    //////to chek existing chekboxes---End
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    lblMsg.Text = "";
                    mpeEditVehicle.Show();

                }
                if (e.CommandName == "Remove")
                {
                    string id = e.CommandArgument.ToString();
                    ViewState["deleteRowID"] = id;
                    lblMsg.Text = "";
                    mpeDelVehicle.Show();
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
                    LinkButton lbtnAction,lnkVehRegNo;
                    GridViewRow row;
                    row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    lbtnAction = gvVehicleEntries.Rows[row.RowIndex].FindControl("lnlStatus") as LinkButton;
                    status = lbtnAction.Text;
                    lnkVehRegNo = gvVehicleEntries.Rows[row.RowIndex].FindControl("lnkVehRegNo") as LinkButton;
                    string VehicleNo=lnkVehRegNo.Text,Userid=Session["uid"].ToString();
                   
                    string Rfid= gvVehicleEntries.Rows[row.RowIndex].Cells[6].Text;
                    if (status == "Enabled")
                    {
                        update = "update veh_vehicleEnrollment set isEnabled=1 where id='" + id + "'";
                        lbtnAction.ToolTip = "Click here to disable record";

                        SqlCommand cmd = new SqlCommand("sp_vehEnableDisable", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@vehicleEnrollmentId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@rfId",Rfid);
                        cmd.Parameters.AddWithValue("@user_id",Userid);
                        cmd.Parameters.AddWithValue("@enableDate",null);
                        cmd.Parameters.AddWithValue("@disableDate", DateTime.ParseExact(System.DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy",null));
                        cmd.Parameters.AddWithValue("@Enabled_Disabled",1);

                        cmd.ExecuteNonQuery();
               
                    }
                    else
                    {
                        lbtnAction.ToolTip = "Click here to Enable record";
                        update = "update veh_vehicleEnrollment set isEnabled=0 where id='" + id + "'";

                        SqlCommand cmd = new SqlCommand("sp_vehEnableDisable", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@vehicleEnrollmentId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@rfId", Rfid);
                        cmd.Parameters.AddWithValue("@user_id", Userid);
                        cmd.Parameters.AddWithValue("@enableDate", DateTime.ParseExact(System.DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null));
                        cmd.Parameters.AddWithValue("@disableDate",null);
                        cmd.Parameters.AddWithValue("@Enabled_Disabled", 0);

                        cmd.ExecuteNonQuery();
                    
                    }
                    SqlCommand cmdUpdate = new SqlCommand(update, conn);
                    cmdUpdate.ExecuteNonQuery();
                    binddata();
                    SetTagInventory();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
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

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            txtEditempVistorId.Text = "";
            txtEditMake.Text = "";
            txtEditModelName.Text = "";
            txtEditRegNumber.Text = "";
            txtEditRFID.Text = "";
            txtEditPurpose.Text = "";
            txtEditRFID_ValidityDate.Text = "";
            txtEditVehicleName.Text = "";
            lblMsg.Text = "";
            for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
            {
                CheckBox chkClear = gvControllerEdit.Rows[i].FindControl("chkControllerEdit") as CheckBox;
                chkClear.Checked = false;
            }
            mpeEditVehicle.Hide();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string rowid = ViewState["RowID"].ToString();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd1 = new SqlCommand("spUpdateVehicleEntry", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@RowID", SqlDbType.BigInt).Value = rowid;
                cmd1.Parameters.Add("@userType", SqlDbType.VarChar).Value = ddlEditType.SelectedItem.Text;
                cmd1.Parameters.Add("@entityId", SqlDbType.VarChar).Value = txtEditempVistorId.Text;
                cmd1.Parameters.Add("@make", SqlDbType.VarChar).Value = txtEditMake.Text;
                cmd1.Parameters.Add("@model", SqlDbType.VarChar).Value = txtEditModelName.Text;
                cmd1.Parameters.Add("@VehicleRegistrationNumber", SqlDbType.VarChar).Value = txtEditRegNumber.Text;
                cmd1.Parameters.Add("@vehicleName", SqlDbType.VarChar).Value = txtEditVehicleName.Text;
                //cmd1.Parameters.Add("@rfidValidityDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEditRFID_ValidityDate.Text).ToString("MM/dd/yyyy");
                cmd1.Parameters.Add("@rfidValidityDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtEditRFID_ValidityDate.Text, "dd/MM/yyyy", null);//.ToString("MM/dd/yyyy");
                //cmd1.Parameters.Add("@rfidValidityDat", SqlDbType.DateTime).Value = "07/23/1990";
                cmd1.Parameters.Add("@purpose", SqlDbType.VarChar).Value = txtEditPurpose.Text;

                cmd1.Parameters.Add("@IShhue_Date", SqlDbType.DateTime).Value = DateTime.ParseExact(txtEditRFID_IssueDate.Text, "dd/MM/yyyy", null);//.ToString("MM/dd/yyyy");

                cmd1.Parameters.AddWithValue("@vehicleid", 0).Direction = ParameterDirection.Output;
                cmd1.ExecuteNonQuery();
                string vehicleID = cmd1.Parameters["@vehicleid"].Value.ToString();
                string RFID = txtRFID.Text;

                string delEsxisting = "delete  from Veh_assignController where vehicleEnrollementID='" + vehicleID + "'";
                SqlCommand delCmd = new SqlCommand(delEsxisting, conn);
                delCmd.ExecuteNonQuery();
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                string cntrID, cntrlIP;
                sb.Append("<RFIDInventory>");


              
                for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
                {
                    CheckBox chkAdd = gvControllerEdit.Rows[i].FindControl("chkControllerEdit") as CheckBox;
                    string controllerID = gvControllerEdit.Rows[i].Cells[1].Text;
                    cntrID = gvControllerEdit.Rows[i].Cells[2].Text;
                    cntrlIP = gvControllerEdit.Rows[i].Cells[3].Text;
                    if (chkAdd.Checked == true)
                    {
                        //string updateAssignControllers = "insert into Veh_assignController(RFID,ControllerID,vehicleEnrollementID)values('" + txtEditRFID.Text + "','" + controllerID + "','" + vehicleID + "')";
                        //SqlCommand cmd2 = new SqlCommand(updateAssignControllers, conn);
                        //cmd2.ExecuteNonQuery();




                        sb.Append("<Transaction>");
                            sb.Append("<RFID>" + txtEditRFID.Text + "</RFID>");
                            sb.Append("<ControllerID>" + controllerID + "</ControllerID>");
                            sb.Append("<vehicleEnrollementID>" + vehicleID + "</vehicleEnrollementID>");
                            sb.Append("<cntrID>" + cntrID + "</cntrID>");
                            sb.Append("<ControllerIP>" + cntrlIP + "</ControllerIP>");

                            sb.Append("</Transaction>");

                    }
                 
                 
                }
              


                sb.Append("</RFIDInventory>");
                string s = sb.ToString();


                SqlCommand cmd = new SqlCommand("veh_spAssignControllerUpdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = sb.ToString();
                cmd.ExecuteNonQuery();
                
                binddata();
                lblMsg.Text = "Record Successfully Saved";
                mpeEditVehicle.Hide();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Saved Successfully');</script>");
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

        protected void btn_del_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string rowid = ViewState["deleteRowID"].ToString();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string delRecord = "update veh_vehicleEnrollment set isDeleted=1,isDeletedDate=getdate() where ID =" + rowid + "";

                SqlCommand del_cmd = new SqlCommand(delRecord, conn);
                del_cmd.ExecuteNonQuery();
                binddata();
                lblMsg.Text = "Record Successfully Deleted";
                mpeDelVehicle.Hide();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }

        protected void btn_cl_Click(object sender, EventArgs e)
        {

            mpeDelVehicle.Hide();
        }
        public void bindControllerGvAdd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from veh_controller";

                SqlCommand cmd = new SqlCommand(str, conn);


                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvControllerAdd.DataSource = dt;
                gvControllerAdd.DataBind();
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        public void bindControllerGvEdit()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from veh_controller";

                SqlCommand cmd = new SqlCommand(str, conn);


                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvControllerEdit.DataSource = dt;
                gvControllerEdit.DataBind();



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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
      
        [WebMethod()]
        public static bool SaveDate(string data)
        {
            bool flag = false;
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn1.State == ConnectionState.Closed)
                {
                    conn1.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_VerifyNewRFIDInventory", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@rfid", SqlDbType.VarChar).Value = data;
                cmd.Parameters.AddWithValue("@counter1", 0).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.Parameters["@counter1"].Value);
                if (count > 0)
                {
                    flag = true;
                 
                }
                    if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return flag;
        }
        public void getVehicleData(string VehicleId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_get_vehHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvVehHistory.DataSource = dt;
                gvVehHistory.DataBind();
               
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
        public void getVehicleSpecifficData(string VehicleId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_get_vehSpecifficData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvVehicledata.DataSource = dt;
                gvVehicledata.DataBind();

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

        protected void btnokHistory_Click(object sender, EventArgs e)
        {
            mpeHistory.Hide();
        }

        protected void btnokVehData_Click(object sender, EventArgs e)
        {
            mpeVehData.Hide();
        }


        public void getRFIDCount()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_get_vehHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvVehHistory.DataSource = dt;
                gvVehHistory.DataBind();

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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlSelectedDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillddl(ddlSelectedDep.SelectedValue);
            txtempVistorId.Enabled = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void imgExpotBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (gvVehHistory.Rows.Count > 0)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //To Export all pages
                        gvVehHistory.AllowPaging = false;
                       // this.BindGrid();

                        gvVehHistory.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in gvVehHistory.HeaderRow.Cells)
                        {
                            cell.BackColor = gvVehHistory.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in gvVehHistory.Rows)
                        {
                            row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                if (row.RowIndex % 2 == 0)
                                {
                                    cell.BackColor = gvVehHistory.AlternatingRowStyle.BackColor;
                                }
                                else
                                {
                                    cell.BackColor = gvVehHistory.RowStyle.BackColor;
                                }
                                cell.CssClass = "textmode";
                            }
                        }

                        gvVehHistory.RenderControl(hw);

                        //style to format numbers to string
                        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }

                    //Response.ClearContent();
                    //Response.Buffer = true;
                    //Response.AddHeader("content-disposition", string.Format("attachment; FileName=Employee.xls"));
                    //Response.ContentType = "application/ms-excel";
                    //System.IO.StringWriter sw = new StringWriter();
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //gvVehHistory.AllowPaging = false;
                    //binddata();
                    ////Change the Header Row back to white color
                    //gvVehHistory.HeaderRow.Style.Add("background-color", "#FFFFFF");
                    ////Applying stlye to gridview header cells
                    //for (int i = 0; i < gvVehHistory.HeaderRow.Cells.Count; i++)
                    //{
                    //    gvVehHistory.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                    //}
                    //gvVehHistory.RenderControl(htw);
                    //Response.Write(sw.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.ContentType = "TEXT/CSV";
                    //Response.End();
                    ///////////




                    ////Response.ContentType = ContentType;
                    ////Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv");
                    //// Response.AddHeader("Content-Length", newFile.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.ContentType = "TEXT/CSV";

                    //newFile.Close();



                    //Response.WriteFile(fpath);

                    ////new exmort toexcel by vaibhav 


                 //   Response.Clear();
                 //   Response.Buffer = true;

                 //   Response.AddHeader("content-disposition",
                 //    "attachment;filename=GridViewExport.xls");
                 //   Response.Charset = "";
                 //   Response.ContentType = "application/vnd.ms-excel";
                 //   StringWriter sw = new StringWriter();
                 //   HtmlTextWriter hw = new HtmlTextWriter(sw);

                 //   PrepareForExport(gvVehHistory);
                 //   //PrepareForExport(GridView2);

                 //   Table tb = new Table();
                 //   TableRow tr1 = new TableRow();
                 //   TableCell cell1 = new TableCell();
                 //   cell1.Controls.Add(gvVehHistory);
                 //   tr1.Cells.Add(cell1);
                 //   //TableCell cell3 = new TableCell();
                 //   //cell3.Controls.Add(GridView2);
                 //   TableCell cell2 = new TableCell();
                 //   cell2.Text = "&nbsp;";
                 //   //if (rbPreference.SelectedValue == "2")
                 //   //{
                 //   //    tr1.Cells.Add(cell2);
                 //   //    tr1.Cells.Add(cell3);
                 //   //    tb.Rows.Add(tr1);
                 //   //}
                 //   //else
                 //   //{
                 //       TableRow tr2 = new TableRow();
                 //       tr2.Cells.Add(cell2);
                 //       //TableRow tr3 = new TableRow();
                 //     //  tr3.Cells.Add(cell3);
                 //       tb.Rows.Add(tr1);
                 //       tb.Rows.Add(tr2);
                 //     //  tb.Rows.Add(tr3);
                 ////   }
                 //   tb.RenderControl(hw);

                 //   //style to format numbers to string
                 //   string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                 //   Response.Write(style);
                 //   Response.Output.Write(sw.ToString());
                 //   Response.Flush();
                 //   Response.End();



                }
            }
            catch (Exception ex)
            { }
        }

        protected void gvVehHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehHistory.PageIndex = e.NewPageIndex;
        }

        protected void gvVehicledata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehicledata.PageIndex = e.NewPageIndex;
        }
        protected void PrepareForExport(GridView Gridview)
        {
           // Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);
            //Gridview.AllowPaging = false;
            //Gridview.DataBind();

            //Change the Header Row back to white color
            Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
            {
                Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
            }

            for (int i = 0; i < Gridview.Rows.Count; i++)
            {
                GridViewRow row = Gridview.Rows[i];

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                    {
                        row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }
            }
        }
        //protected void txtempVistorId_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}