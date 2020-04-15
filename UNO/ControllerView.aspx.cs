using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
namespace UNO
{
    public partial class ControllerView1 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            //getControllerID();
            //txtControllerId.ReadOnly = true;
            ddlCtlrType.Enabled = false;
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                FillContrType();
                FillLocation();
                FillAuthMode();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvController.ClientID + "');");
            }
        }

        public void getControllerID()
        {
            try
            {
                //string strsql = " select IDENT_CURRENT('ACS_CONTROLLER') + 1 ";
                string strsql = "SELECT MAX(CTLR_ID) + 1 FROM ACS_CONTROLLER ";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.Fill(ds);
                txtControllerId.Text = ds.Tables[0].Rows[0][0].ToString().Trim();
                // txt_level_id.ReadOnly = true;
                Reset();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset()
        {
            lblMessages.Text = "";
            lblMsg.Text = "";
            lblMsg.Visible = false;
            lblError.Visible = false;
            lblError.Text = "";
            chkAuthMode.Enabled = false;
        }

        private void FillContrType()
        {
            string strSql = "SELECT CODE,[VALUE] AS CONTROLLERNAME FROM ENT_PARAMS WHERE IDENTIFIER = 'CONTROLLERTYPE'";
            SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlCtlrType.DataValueField = "CODE";
            ddlCtlrType.DataTextField = "CONTROLLERNAME";
            ddlCtlrType.DataSource = dt;
            ddlCtlrType.DataBind();
            ddlCtlrType.Items.Insert(0, "Select One");

            ddlCType.DataValueField = "CODE";
            ddlCType.DataTextField = "CONTROLLERNAME";
            ddlCType.DataSource = dt;
            ddlCType.DataBind();
            ddlCType.Items.Insert(0, "Select One");

            da.Dispose();
            dt.Dispose();

        }
        private void FillLocation()
        {
            try
            {
                DataTable dt = clsCommonHandler.GetCommonEntitiesValues("LOC");
                if (dt.Rows.Count > 0)
                {
                    ddlLocation.DataValueField = "oce_id";
                    ddlLocation.DataTextField = "oce_description";
                    ddlLocation.DataSource = dt;
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, "Select");

                    ddlLocationEdit.DataValueField = "oce_id";
                    ddlLocationEdit.DataTextField = "oce_description";
                    ddlLocationEdit.DataSource = dt;
                    ddlLocationEdit.DataBind();
                    ddlLocationEdit.Items.Insert(0, "Select");

                    //gvHolidayLocationsAdd.DataSource = dt;
                    //gvHolidayLocationsAdd.DataBind();

                }

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }
        private void FillAuthMode()
        {

            string strSql = "SELECT CODE,[VALUE] AS AUTHMODE FROM ENT_PARAMS WHERE IDENTIFIER = 'AUTHENTICATIONMODE'";
            SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (ddlCtlrType.SelectedValue == "DC" || ddlCtlrType.SelectedValue == "MD4" || ddlCtlrType.SelectedValue == "Select One"
                || ddlCtlrType.SelectedValue == "MD8" || ddlCtlrType.SelectedValue == "HHR")
            {
                ddlAuthMode.DataValueField = "CODE";
                ddlAuthMode.DataTextField = "AUTHMODE";
                ddlAuthMode.DataSource = dt;
                //ddlAuthMode.Items.Remove("CARD");
                //ddlAuthMode.Items.Remove("CARD + FINGER");
                ddlAuthMode.DataBind();
                ddlAuthMode.Items.Insert(0, "Select One");

                //ddlAuthMode.Items.FindByText("FINGER").Enabled = false;
                //ddlAuthMode.Items.FindByText("CARD + FINGER").Enabled = false;
            }
            else
            {
                ddlAuthMode.DataValueField = "CODE";
                ddlAuthMode.DataTextField = "AUTHMODE";
                ddlAuthMode.DataSource = dt;
                ddlAuthMode.DataBind();
                ddlAuthMode.Items.Insert(0, "Select One");
            }

            if (ddlCType.SelectedValue == "DC" || ddlCType.SelectedValue == "MD4" || ddlCType.SelectedValue == "Select One"
                || ddlCType.SelectedValue == "MD8" || ddlCType.SelectedValue == "HHR")
            {
                ddlAuthenticationMode.DataValueField = "CODE";
                ddlAuthenticationMode.DataTextField = "AUTHMODE";
                ddlAuthenticationMode.DataSource = dt;
                ddlAuthenticationMode.DataBind();
                ddlAuthenticationMode.Items.Insert(0, "Select One");
                ddlAuthenticationMode.Items.FindByText("FINGER").Enabled = false;
                ddlAuthenticationMode.Items.FindByText("CARD + FINGER").Enabled = false;
                chkAuthMode.Enabled = false;
            }
            else
            {
                ddlAuthenticationMode.DataValueField = "CODE";
                ddlAuthenticationMode.DataTextField = "AUTHMODE";
                ddlAuthenticationMode.DataSource = dt;
                ddlAuthenticationMode.DataBind();
                ddlAuthenticationMode.Items.Insert(0, "Select One");
                chkAuthMode.Enabled = true;
            }

            da.Dispose();
            dt.Dispose();
        }

        void bindDataGrid()
        {
            try
            {
                string strsql = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable dt = new DataTable();
                //if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                //{
                strsql = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT,Case when CTLR_KEY_PAD=1 then 'Enabled' else 'Disabled' end CTLR_KEY_PAD ,CTLR_LOCATION_ID " +
                         " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' order by CTLR_DESCRIPTION ASC  ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);

                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);

                gvController.DataSource = dt;
                gvController.DataBind();
                //}
                //else
                //{
                //    SqlCommand cmd = new SqlCommand("fillController", conn);

                //    cmd.CommandType = CommandType.StoredProcedure;
                //    string levelId = Session["levelId"].ToString();
                //    cmd.Parameters.AddWithValue("@levelid", levelId);
                //    SqlDataAdapter dap = new SqlDataAdapter(cmd);

                //    dap.Fill(dt);

                //    gvController.DataSource = dt;
                //    gvController.DataBind();

                //}




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
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }

                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");

            }

        }

        private void ddlCTypeOnChangeValue()
        {
            if (ddlCType.SelectedValue == "DC")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "DCB")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "MD4")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "MD8")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "HHR")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "BH")
            {
                FillAuthMode();
            }
            else if (ddlCType.SelectedValue == "BIOEDGE+")
            {
                FillAuthMode();
            }
        }

        protected void ddlCType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlCtlrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCtlrType.SelectedItem.Text.Contains("BIO"))
                chkAuthMode.Enabled = true;
            else
            {
                chkAuthMode.Checked = false;
                chkAuthMode.Enabled = false;
            }
            if (txtControllerDesc.Text == "")
            {
                lblError.Text = "Enter controller description";
                lblError.Visible = true;
                PnlInfo.Visible = false;

                //string someScript = "";
                //someScript = "<script language='javascript'>alert('" + Message + "');</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "HideControl();", true);             
                return;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowControl();", true);

            if (ddlCtlrType.SelectedValue == "DC")
            {
                SetReaderData(2);
                SetAccessPointData(2, 1);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "DCB")
            {
                SetReaderData(2);
                SetAccessPointData(2, 1);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "MD4")
            {
                SetReaderData(4);
                SetAccessPointData(4, 4);
                pnlRdr.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "MD8")
            {
                SetReaderData(8);
                SetAccessPointData(8, 8);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "HHR")
            {
                SetReaderData(1);
                SetAccessPointData(1, 1);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "BH")
            {
                SetReaderData(1);
                SetAccessPointData(1, 1);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
            else if (ddlCtlrType.SelectedValue == "BIOEDGE+")
            {
                SetReaderData(1);
                SetAccessPointData(1, 1);
                pnlRdr.Visible = true;
                pnlRdr2.Visible = true;
                PnlInfo.Visible = true;
                FillAuthMode();
            }
        }

        //protected void ddlDoor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    for (int i = 0; i < gvAccesspoint.Rows.Count; i++)
        //    {
        //        if (gvAccesspoint.Rows[0].Cells[3].Text == ddlCType.SelectedItem.Value)
        //        {
        //            count += 1;
        //        }
        //    }

        //    if (count > 2)
        //    {
        //        lblError.Text = "Door " + ddlCType.SelectedItem.Value + " is already attached to two readers";
        //        lblError.Visible = true;
        //    }
        //}

        private void SetReaderData(int ReaderCount)
        {
            DataTable dtReader = new DataTable();
            DataRow drType = null;

            //dtReader.Columns.Add(new DataColumn("DELETE_READER", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_ID", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_DESCRIPTION", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_TYPE", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_MODE", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_PASSES_FROM", typeof(string)));
            dtReader.Columns.Add(new DataColumn("READER_PASSES_TO", typeof(string)));

            for (int count = 0; count < ReaderCount; count++)
            {
                drType = dtReader.NewRow();
                // drType["DELETE_READER"] = string.Empty;
                drType["READER_ID"] = string.Empty;
                drType["READER_DESCRIPTION"] = string.Empty;
                drType["READER_TYPE"] = string.Empty;
                drType["READER_MODE"] = string.Empty;
                drType["READER_PASSES_FROM"] = string.Empty;
                drType["READER_PASSES_TO"] = string.Empty;
                dtReader.Rows.Add(drType);
                //  ViewState["CurrentTable"] = dtReader;
                gvReader.DataSource = dtReader;
                gvReader.DataBind();
            }
            for (int i = 0; i < ReaderCount; i++)
            {
                TextBox ReaderId = (TextBox)gvReader.Rows[i].Cells[0].FindControl("txtReaderId");
                TextBox ReaderDesc = (TextBox)gvReader.Rows[i].Cells[1].FindControl("txtReaderDesc");
                DropDownList ReaderType = (DropDownList)gvReader.Rows[i].Cells[2].FindControl("ddlReaderType");
                DropDownList ReaderMode = (DropDownList)gvReader.Rows[i].Cells[3].FindControl("ddlReaderMode");
                DropDownList PassesFrom = (DropDownList)gvReader.Rows[i].Cells[4].FindControl("ddlPassesFrom");
                DropDownList PassesTo = (DropDownList)gvReader.Rows[i].Cells[5].FindControl("ddlPassesTo");
                CheckBox chDel = (CheckBox)gvReader.Rows[i].FindControl("chkDelete");


                string strSql = "SELECT CODE,[VALUE] AS READERNAME FROM ENT_PARAMS WHERE IDENTIFIER = 'READERTYPE'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ReaderType.DataValueField = "CODE";
                ReaderType.DataTextField = "READERNAME";
                ReaderType.DataSource = dt;
                ReaderType.DataBind();
                ReaderType.SelectedIndex = 1;

                string strSql1 = "SELECT CODE,[VALUE] AS READERMODE FROM ENT_PARAMS WHERE IDENTIFIER = 'READERMODE'";
                SqlDataAdapter da1 = new SqlDataAdapter(strSql1, conn);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                ReaderMode.DataValueField = "CODE";
                ReaderMode.DataTextField = "READERMODE";
                ReaderMode.DataSource = dt1;
                ReaderMode.DataBind();

                string strSql2 = "SELECT ZONE_ID,ZONE_DESCRIPTION FROM ZONE WHERE ZONE_ISDELETED='0' OR ZONE_ISDELETED IS NULL ";
                SqlDataAdapter da2 = new SqlDataAdapter(strSql2, AccessController.m_connecton);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                PassesFrom.DataValueField = "ZONE_ID";
                PassesFrom.DataTextField = "ZONE_DESCRIPTION";
                PassesFrom.DataSource = dt2;
                PassesFrom.DataBind();
                PassesFrom.Items.Insert(0, "Select One");

                string strSql3 = "SELECT ZONE_ID,ZONE_DESCRIPTION FROM ZONE WHERE ZONE_ISDELETED='0' OR ZONE_ISDELETED IS NULL ";
                SqlDataAdapter da3 = new SqlDataAdapter(strSql3, conn);
                DataTable dt3 = new DataTable();
                da2.Fill(dt3);
                PassesTo.DataValueField = "ZONE_ID";
                PassesTo.DataTextField = "ZONE_DESCRIPTION";
                PassesTo.DataSource = dt3;
                PassesTo.DataBind();
                PassesTo.Items.Insert(0, "Select One");
                chDel.Checked = true;

                ReaderId.Text = (i + 1).ToString();
                ReaderDesc.Text = txtControllerDesc.Text + " - " + ReaderId.Text;
            }
        }

        private void SetAccessPointData(int AccessCount, int Door)
        {
            DataTable dtAP = new DataTable();
            DataRow drAP = null;
            // dtAP.Columns.Add(new DataColumn("DELETE_ACCESS", typeof(string)));
            dtAP.Columns.Add(new DataColumn("READER_ID", typeof(string)));
            dtAP.Columns.Add(new DataColumn("READER_DESCRIPTION", typeof(string)));
            dtAP.Columns.Add(new DataColumn("DOOR_ID", typeof(string)));
            dtAP.Columns.Add(new DataColumn("DOOR_TYPE", typeof(string)));
            dtAP.Columns.Add(new DataColumn("DOOR_OPEN_DURATION", typeof(string)));
            dtAP.Columns.Add(new DataColumn("DOOR_FEEDBACK_DURATION", typeof(string)));

            for (int count = 0; count < AccessCount; count++)
            {
                drAP = dtAP.NewRow();
                // drAP["DELETE_ACCESS"] = string.Empty;
                drAP["READER_ID"] = string.Empty;
                drAP["READER_DESCRIPTION"] = string.Empty;
                drAP["DOOR_ID"] = string.Empty;
                drAP["DOOR_TYPE"] = string.Empty;
                drAP["DOOR_OPEN_DURATION"] = string.Empty;
                drAP["DOOR_FEEDBACK_DURATION"] = string.Empty;
                dtAP.Rows.Add(drAP);
                //  ViewState["CurrentTable"] = dtReader;
                gvAccesspoint.DataSource = dtAP;
                gvAccesspoint.DataBind();
            }

            for (int i = 0; i < AccessCount; i++)
            {
                TextBox ReaderId = (TextBox)gvReader.Rows[i].Cells[0].FindControl("txtReaderId");
                TextBox Reader = (TextBox)gvReader.Rows[i].Cells[1].FindControl("txtReaderDesc");
                TextBox ReaderDesc = (TextBox)gvAccesspoint.Rows[i].Cells[1].FindControl("txtReaderdesc");
                DropDownList DoorId = (DropDownList)gvAccesspoint.Rows[i].Cells[2].FindControl("ddlDoor");
                DropDownList LockType = (DropDownList)gvAccesspoint.Rows[i].Cells[3].FindControl("ddlLocktype");
                TextBox OpenDur = (TextBox)gvAccesspoint.Rows[i].Cells[4].FindControl("txtOpenDur");
                TextBox FeedbackDur = (TextBox)gvAccesspoint.Rows[i].Cells[5].FindControl("txtFeedbackDur");
                TextBox APReaderId = (TextBox)gvAccesspoint.Rows[i].Cells[0].FindControl("txtReaderId");

                string strSql = "SELECT CODE,[VALUE] AS DOORNAME FROM ENT_PARAMS WHERE IDENTIFIER = 'DOORTYPE'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                LockType.DataValueField = "CODE";
                LockType.DataTextField = "DOORNAME";
                LockType.DataSource = dt;
                LockType.DataBind();
                LockType.SelectedIndex = 0;

                for (int cnt = 0; cnt < Door; cnt++)
                    DoorId.Items.Add((cnt + 1).ToString());
                DoorId.SelectedIndex = i;

                if (AccessCount > Door)
                    APReaderId.Text = txtControllerId.Text + "." + DoorId.Items[Door - 1].Text;
                else
                    APReaderId.Text = txtControllerId.Text + "." + DoorId.Items[i].Text;

                ReaderDesc.Text = Reader.Text;
                //ReaderId.Text;

                OpenDur.Text = "5";
                FeedbackDur.Text = "5";


            }
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlTransaction trans;
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            trans = conn.BeginTransaction();
            objcmd.Transaction = trans;

            try
            {
                if (txtControllerId.Text == "0" || txtControllerId.Text == "00" || txtControllerId.Text == "000")
                {
                    lblError.Text = "This controller id is not allowed";
                    lblError.Visible = true;
                    return;
                }
                if (txtControllerId.Text == "001" || txtControllerId.Text == "01" || txtControllerId.Text == "1")
                {
                    lblError.Text = "This is a reserved controller id";
                    lblError.Visible = true;
                    return;
                }
                objcmd.CommandText = "SELECT CTLR_ID FROM ACS_CONTROLLER  where CTLR_ISDELETED = 0 and CTLR_ID ='" + Convert.ToInt32(txtControllerId.Text) + "'";
                objcmd.ExecuteNonQuery();
                SqlDataAdapter dachk = new SqlDataAdapter(objcmd);
                DataTable dtchk = new DataTable();
                dachk.Fill(dtchk);

                //SqlCommand cmd = new SqlCommand("SELECT CTLR_ID FROM ACS_CONTROLLER  where CTLR_ISDELETED = 0", conn);
                ////  cmd.CommandType = CommandType.StoredProcedure;
                ////  cmd.Parameters.AddWithValue("@ESS_CO_ROWID", rowid);
                //cmd.ExecuteNonQuery();

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                //for (int i = 0; i < dtchk.Rows.Count - 1; i++)  //Added on 17/Sept/2014 by Shrinith
                //{
                if (dtchk.Rows.Count > 0)
                {
                    lblError.Text = " Controller Id already Exists!";
                    lblError.Visible = true;
                    txtControllerId.Focus();
                    return;
                }
                //}

                //if (rows >= 1)  //commented on 16/Sept/2014 by shrinith
                //{
                //    lblError.Text = " Controller Id already Exists!";
                //    lblError.Visible = true;
                //    txtControllerId.Focus();
                //    return;
                //}

                //objcmd.CommandText = "Select count(*) from ACS_CONTROLLER Where CTLR_IP = '" + txtCtlrIp.Text.Trim() + "' and CTLR_ISDELETED = 0";
                objcmd.CommandText = "select distinct CTLR_IP from ACS_CONTROLLER";
                objcmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(objcmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //int rows1 = Convert.ToInt16(objcmd.ExecuteScalar());
                IPAddress ip1 = IPAddress.Parse(txtCtlrIp.Text.Trim());
                IPAddress ip2;
                foreach (DataRow row in dt.Rows)
                {
                    //row.i
                    ip2 = IPAddress.Parse(row[0].ToString());
                    if (ip1.Equals(ip2))
                    {
                        //...
                        lblError.Text = " Controller IP already Exists!";
                        lblError.Visible = true;
                        txtCtlrIp.Focus();
                        return;
                    }
                }

                //IPAddress ip2 = IPAddress.Parse("01.01.01.01");


                //if (rows1 >= 1)
                //{
                //    lblError.Text = " Controller IP already Exists!";
                //    lblError.Visible = true;
                //    txtCtlrIp.Focus();
                //    return;
                //}
                string TAADD = null;
                if (chkTAADD.Checked)
                {
                    TAADD = "TA";
                }
                objcmd.CommandText = " Insert into ACS_CONTROLLER (CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_MAC_ID," +
                                        " CTLR_INCOMING_PORT,CTLR_OUTGOING_PORT,CTLR_FIRMWARE_VERSION_NO,CTLR_HARDWARE_VERSION_NO, " +
                                        " CTLR_CHK_APB,CTLR_APB_TYPE,CTLR_APB_TIME,CTLR_AUTHENTICATION_MODE,CTLR_CHK_TOC,CTLR_ISDELETED,CLTR_FOR_TA,CTLR_KEY_PAD,CTLR_LOCATION_ID) " +
                                        " Values ('" + txtControllerId.Text.Trim() + "','" + txtControllerDesc.Text.Trim() + "','" + ddlCtlrType.SelectedValue + "', " +
                                        " '" + txtCtlrIp.Text.Trim() + "' ,'" + txtMacId.Text.Trim() + "','" + txtInPort.Text.Trim() + "'," +
                                        " '" + txtOutPort.Text.Trim() + "' , '" + txtFirmwareNo.Text.Trim() + "', '" + txtHardwareNo.Text.Trim() + "', " +
                                        " '" + ddlAntipass.SelectedValue + "','" + ddlAPB.SelectedValue + "','" + txtAPBTimed.Text.Trim() + "','" + ddlAuthMode.SelectedValue + "' ,'" + chkAuthMode.Checked + "','false','" + TAADD + "','" + chkKeypad.Checked + "','" + ddlLocation.SelectedValue + "') ";

                objcmd.ExecuteNonQuery();

                if (gvReader.Rows.Count > 0)
                {
                    for (int i = 0; i < gvReader.Rows.Count; i++)
                    {
                        // CheckBox chkDelete = (CheckBox)gvContrType.Rows[i].Cells[0].FindControl("chkDelete");
                        TextBox ReaderId = (TextBox)gvReader.Rows[i].Cells[0].FindControl("txtReaderId");
                        TextBox ReaderDesc = (TextBox)gvReader.Rows[i].Cells[1].FindControl("txtReaderDesc");
                        DropDownList ReaderType = (DropDownList)gvReader.Rows[i].Cells[2].FindControl("ddlReaderType");
                        DropDownList ReaderMode = (DropDownList)gvReader.Rows[i].Cells[3].FindControl("ddlReaderMode");
                        DropDownList PassesFrom = (DropDownList)gvReader.Rows[i].Cells[4].FindControl("ddlPassesFrom");
                        DropDownList PassesTo = (DropDownList)gvReader.Rows[i].Cells[5].FindControl("ddlPassesTo");
                        CheckBox chkDel = (CheckBox)gvReader.Rows[i].FindControl("chkDelete");

                        int active = 0;
                        if (chkDel.Enabled == true)
                        {
                            active = 1;
                        }

                        string PF = "";
                        string PT = "";
                        if (PassesFrom.SelectedValue == "Select One")
                        {
                            PF = "";
                        }
                        else
                        {
                            PF = PassesFrom.SelectedValue;
                        }
                        if (PassesTo.SelectedValue == "Select One")
                        {
                            PT = "";
                        }
                        else
                        {
                            PT = PassesTo.SelectedValue;
                        }
                        if (ReaderDesc.Text != "")
                        {
                            objcmd.CommandText = " Insert into ACS_READER (READER_ID,READER_DESCRIPTION,CTLR_ID,READER_MODE,READER_TYPE,READER_PASSES_FROM,READER_PASSES_TO,READER_ISDELETED,IsActive,EntryReaderMode) " +
                                                    " values ('" + ReaderId.Text.Trim() + "','" + ReaderDesc.Text.Trim() + "','" + txtControllerId.Text.Trim() + "','" + ReaderMode.SelectedValue + "'," +
                                                    " '" + ReaderType.SelectedValue + "','" + PF + "', '" + PT + "','false'," + active + ",0) ";

                            objcmd.ExecuteNonQuery();
                        }
                        else
                        {
                            lblError.Text = "Please enter Reader Description. ";
                            lblError.Visible = true;
                            ReaderDesc.Focus();
                            return;
                        }
                    }
                }
                objcmd.CommandText = " Select case when max(AP_ID) IS NULL then 1 else max(AP_ID) + 1  End  as APID from ACS_ACCESSPOINT_RELATION ";
                int APId = Convert.ToInt32(objcmd.ExecuteScalar());

                if (gvAccesspoint.Rows.Count > 0)
                {
                    for (int i = 0; i < gvAccesspoint.Rows.Count; i++)
                    {
                        //CheckBox chkDelete = (CheckBox)gvDoor.Rows[i].Cells[0].FindControl("chkDelete");
                        TextBox APReaderId = (TextBox)gvAccesspoint.Rows[i].Cells[0].FindControl("txtReaderId");
                        TextBox ReaderDesc = (TextBox)gvAccesspoint.Rows[i].Cells[1].FindControl("txtReaderDesc");
                        DropDownList DoorId = (DropDownList)gvAccesspoint.Rows[i].Cells[2].FindControl("ddlDoor");
                        DropDownList LockType = (DropDownList)gvAccesspoint.Rows[i].Cells[3].FindControl("ddlLockType");
                        TextBox OpenDur = (TextBox)gvAccesspoint.Rows[i].Cells[4].FindControl("txtOpenDur");
                        TextBox Feedbackdur = (TextBox)gvAccesspoint.Rows[i].Cells[5].FindControl("txtFeedbackDur");
                        TextBox reader = (TextBox)gvReader.Rows[i].FindControl("txtReaderId");

                        //objcmd.CommandText = " Insert into ACS_DOOR (DOOR_ID,CTLR_ID,READER_ID,DOOR_TYPE,DOOR_OPEN_DURATION,DOOR_FEEDBACK_DURATION,DOOR_ISDELETED) " +
                        //                     " values ('" + DoorId.Text.Trim() + "','" + txtControllerId.Text.Trim() + "','" + APReaderId.Text + "','" + LockType.SelectedValue + "', " +
                        //                     " '" + OpenDur.Text.Trim() + "','" + Feedbackdur.Text.Trim() + "','false') ";
                        int door = i + 1;
                        objcmd.CommandText = " Insert into ACS_DOOR (DOOR_ID,CTLR_ID,DOOR_TYPE,DOOR_OPEN_DURATION,DOOR_FEEDBACK_DURATION,DOOR_ISDELETED,READER_ID) " +
                                            " values ('" + door + "','" + txtControllerId.Text.Trim() + "','" + LockType.SelectedValue + "', " +
                                            " '" + OpenDur.Text.Trim() + "','" + Feedbackdur.Text.Trim() + "','false'," + reader.Text + ") ";

                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Insert into ACS_ACCESSPOINT_RELATION(AP_ID,READER_ID,DOOR_ID,AP_CONTROLLER_ID,APR_ISDELETED) " +
                                             " Values(" + APReaderId.Text + ",'" + reader.Text + "'," + DoorId.Text.Trim() + "," + txtControllerId.Text + ",'False')";

                        objcmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < gvAccesspoint.Rows.Count; i++)
                    {
                        TextBox APReaderId = (TextBox)gvAccesspoint.Rows[i].Cells[0].FindControl("txtReaderId");
                        TextBox ReaderDesc = (TextBox)gvAccesspoint.Rows[i].Cells[1].FindControl("txtReaderDesc");
                        DropDownList DoorId = (DropDownList)gvAccesspoint.Rows[i].Cells[2].FindControl("ddlDoor");
                        DropDownList LockType = (DropDownList)gvAccesspoint.Rows[i].Cells[3].FindControl("ddlLockType");
                        TextBox OpenDur = (TextBox)gvAccesspoint.Rows[i].Cells[4].FindControl("txtOpenDur");
                        TextBox Feedbackdur = (TextBox)gvAccesspoint.Rows[i].Cells[5].FindControl("txtFeedbackDur");
                        TextBox reader = (TextBox)gvReader.Rows[i].FindControl("txtReaderId");

                        objcmd.CommandText = " Update ACS_DOOR set DOOR_TYPE = '" + LockType.SelectedValue + "',DOOR_OPEN_DURATION = '" + OpenDur.Text.Trim() + "'," +
                                             " DOOR_FEEDBACK_DURATION = '" + Feedbackdur.Text.Trim() + "' where DOOR_ID = '" + DoorId.Text.Trim() + "' and CTLR_ID = '" + txtControllerId.Text.Trim() + "'";
                        objcmd.ExecuteNonQuery();
                    }

                }

                objcmd.CommandText = "Update ENT_PARAMS set [Value] = '1' where IDENTIFIER = 'CONFIGCHANGE' and CODE = 'CTL' ";
                objcmd.ExecuteNonQuery();

                //objcmd.CommandText = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='APB'";
                //objcmd.ExecuteNonQuery();

                trans.Commit();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                txtControllerId.Text = "";
                txtControllerDesc.Text = "";
                txtCtlrIp.Text = "";
                txtFirmwareNo.Text = "";
                txtHardwareNo.Text = "";
                txtMacId.Text = "";
                txtAPBTimed.Text = "";
                ddlAntipass.SelectedIndex = -1;
                ddlAPB.SelectedIndex = -1;
                ddlAuthMode.SelectedIndex = -1;
                ddlCtlrType.SelectedIndex = -1;
                gvReader.DataSource = null;
                gvAccesspoint.DataSource = null;
                mpeAddCtrl.Hide();
                lblError.Text = ""; // changes on 15/Sept/2014 by shrinith
                lblError.Visible = false;  // changes on 15/Sept/2014 by shrinith
                lblMessages.Text = "Records saved successfully";
                lblMessages.Visible = true;
                pnlRdr.Visible = false;
                pnlRdr2.Visible = false;
                bindDataGrid();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            txtControllerId.Text = ""; //uncommented on 17/Sept/2014 by Shrinith
            txtControllerDesc.Text = "";
            txtCtlrIp.Text = "";
            txtFirmwareNo.Text = "";
            txtHardwareNo.Text = "";
            txtMacId.Text = "";
            txtAPBTimed.Text = "";
            lblMessages.Text = "";
            lblError.Text = "";
            lblError.Visible = false;
            ddlAntipass.SelectedIndex = -1;
            ddlAPB.SelectedIndex = -1;
            ddlAuthMode.SelectedIndex = -1;
            ddlCtlrType.SelectedIndex = -1;
            gvReader.DataSource = null;
            gvAccesspoint.DataSource = null;
            pnlRdr.Visible = false;
            pnlRdr2.Visible = false;
            Reset();
            mpeAddCtrl.Hide();
        }

        ArrayList a = new ArrayList();

        protected void gvController_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    string ControllerID = e.CommandArgument.ToString();
                    string strsql = "";
                    strsql = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_MAC_ID," +
                             " CTLR_FIRMWARE_VERSION_NO,CTLR_HARDWARE_VERSION_NO," +
                             " CTLR_CHK_APB,CTLR_APB_TYPE,CTLR_APB_TIME,CTLR_AUTHENTICATION_MODE,CTLR_CHK_TOC,CLTR_FOR_TA,CTLR_KEY_PAD,CTLR_LOCATION_ID " +
                             " FROM ACS_CONTROLLER where CTLR_ID = '" + ControllerID + "' ";

                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtCID1.Text = ControllerID;
                        txtCDesc.Text = dt.Rows[0]["CTLR_DESCRIPTION"].ToString();
                        ddlCType.SelectedValue = dt.Rows[0]["CTLR_TYPE"].ToString();
                        txtCIP1.Text = dt.Rows[0]["CTLR_IP"].ToString();
                        txtCMacId.Text = dt.Rows[0]["CTLR_MAC_ID"].ToString();
                        txtFirmwareVersionNo.Text = dt.Rows[0]["CTLR_FIRMWARE_VERSION_NO"].ToString();
                        txtHardwareVersionNo.Text = dt.Rows[0]["CTLR_HARDWARE_VERSION_NO"].ToString();
                        if (dt.Rows[0]["CTLR_LOCATION_ID"].ToString() != "" && dt.Rows[0]["CTLR_LOCATION_ID"].ToString() != null)
                        {
                            ddlLocationEdit.SelectedValue = dt.Rows[0]["CTLR_LOCATION_ID"].ToString();
                        }
                        if (dt.Rows[0]["CTLR_KEY_PAD"].ToString() != "" && dt.Rows[0]["CTLR_KEY_PAD"].ToString() != null)
                        {
                            chkKeypadEdit.Checked = Convert.ToBoolean(dt.Rows[0]["CTLR_KEY_PAD"]);
                            Session["KeyPad"] = Convert.ToBoolean(dt.Rows[0]["CTLR_KEY_PAD"]);
                        }
                        if (dt.Rows[0]["CTLR_CHK_APB"].ToString() != "")
                            ddlCAPB.SelectedValue = dt.Rows[0]["CTLR_CHK_APB"].ToString();
                        else
                            ddlCAPB.SelectedIndex = -1;

                        if (dt.Rows[0]["CTLR_APB_TYPE"].ToString() != "")
                            ddlAPBSchedule.SelectedValue = dt.Rows[0]["CTLR_APB_TYPE"].ToString();
                        else
                            ddlAPBSchedule.SelectedIndex = -1;

                        ddlCTypeOnChangeValue();

                        txtABPTime.Text = dt.Rows[0]["CTLR_APB_TIME"].ToString();

                        if (dt.Rows[0]["CTLR_AUTHENTICATION_MODE"].ToString() != "")
                        {
                            ddlAuthenticationMode.SelectedValue = dt.Rows[0]["CTLR_AUTHENTICATION_MODE"].ToString();
                        }

                        if (dt.Rows[0]["CTLR_CHK_TOC"].ToString() != "")
                        {
                            chkTempOnCard.Checked = Convert.ToBoolean(dt.Rows[0]["CTLR_CHK_TOC"]);
                        }
                        if (dt.Rows[0]["CLTR_FOR_TA"].ToString() != "")
                        {
                            if (dt.Rows[0]["CLTR_FOR_TA"].ToString() == "TA")
                            {
                                chkTA.Checked = true;
                            }
                            else
                            {
                                chkTA.Checked = false;
                            }
                        }
                        SetPreviousReaderData(ControllerID);
                        if (dt.Rows[0]["CTLR_TYPE"].ToString() == "DC")
                        {
                            SetPreviousAPData(ControllerID, 1);
                        }
                        else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "DCB")
                        {
                            SetPreviousAPData(ControllerID, 1);
                        }
                        else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "MD4")
                        {
                            SetPreviousAPData(ControllerID, 4);
                        }
                        else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "MD8")
                        {
                            SetPreviousAPData(ControllerID, 8);
                        }
                        else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "HHR")
                        {
                            SetPreviousAPData(ControllerID, 1);
                        }
                        else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "BH")
                        {
                            SetPreviousAPData(ControllerID, 1);
                        }
                        //else if (dt.Rows[0]["CTLR_TYPE"].ToString() == "BIOEDGE+")
                        //{
                        //    SetPreviousAPData(ControllerID, 1);
                        //}
                        ddlCType.Enabled = false;

                        mpeEditCtrl.Show();
                        txtCID1.ReadOnly = false;
                    }
                    else
                    {
                        lblMessages.Text = "Records not found";
                        lblMessages.Visible = true;
                        return;
                    }
                }
                if (e.CommandName == "Reinit")
                {
                    try
                    {
                        string controllerId = e.CommandArgument.ToString();
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("sp_ACS_ReinitializeFlag", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CtrlID", controllerId);
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
                if (e.CommandName == "Empty")
                {
                    try
                    {
                        string controllerId = e.CommandArgument.ToString();
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand cmd = new SqlCommand("update acs_controller set EmptyReaderStatus=1 where CTLR_ID = " + controllerId, conn);
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }

        private void SetPreviousReaderData(string CID)
        {

            string strsql = " Select  READER_ID,READER_DESCRIPTION,CTLR_ID,READER_MODE,READER_TYPE,READER_PASSES_FROM,READER_PASSES_TO, IsActive " +
                            " from ACS_READER where READER_ISDELETED = 'false' and CTLR_ID = '" + CID + "'";

            SqlDataAdapter daReader = new SqlDataAdapter(strsql, conn);
            DataTable dtReader = new DataTable();
            daReader.SelectCommand.CommandTimeout = 0;
            daReader.Fill(dtReader);

            gvEditReader.DataSource = dtReader;
            gvEditReader.DataBind();

            if (dtReader.Rows.Count > 0)
            {
                for (int i = 0; i < dtReader.Rows.Count; i++)
                {
                    CheckBox chkdel = (CheckBox)gvEditReader.Rows[i].FindControl("chkDel");
                    TextBox ReaderId = (TextBox)gvEditReader.Rows[i].Cells[0].FindControl("txtReaderId");
                    TextBox ReaderDesc = (TextBox)gvEditReader.Rows[i].Cells[1].FindControl("txtReaderDesc");
                    DropDownList ReaderType = (DropDownList)gvEditReader.Rows[i].Cells[2].FindControl("ddlReaderType");
                    DropDownList ReaderMode = (DropDownList)gvEditReader.Rows[i].Cells[3].FindControl("ddlReaderMode");
                    DropDownList PassesFrom = (DropDownList)gvEditReader.Rows[i].Cells[4].FindControl("ddlPassesFrom");
                    DropDownList PassesTo = (DropDownList)gvEditReader.Rows[i].Cells[5].FindControl("ddlPassesTo");

                    string strSql = "SELECT CODE,[VALUE] AS READERNAME FROM ENT_PARAMS WHERE IDENTIFIER = 'READERTYPE'";
                    SqlDataAdapter da = new SqlDataAdapter(strSql, AccessController.m_connecton);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ReaderType.DataValueField = "CODE";
                    ReaderType.DataTextField = "READERNAME";
                    ReaderType.DataSource = dt;
                    ReaderType.DataBind();

                    string strSql1 = "SELECT CODE,[VALUE] AS READERMODE FROM ENT_PARAMS WHERE IDENTIFIER = 'READERMODE'";
                    SqlDataAdapter da1 = new SqlDataAdapter(strSql1, AccessController.m_connecton);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    ReaderMode.DataValueField = "CODE";
                    ReaderMode.DataTextField = "READERMODE";
                    ReaderMode.DataSource = dt1;
                    ReaderMode.DataBind();

                    string strSql2 = "SELECT ZONE_ID,ZONE_DESCRIPTION FROM ZONE WHERE ZONE_ISDELETED='0' OR ZONE_ISDELETED IS NULL ";
                    SqlDataAdapter da2 = new SqlDataAdapter(strSql2, AccessController.m_connecton);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    PassesFrom.DataValueField = "ZONE_ID";
                    PassesFrom.DataTextField = "ZONE_DESCRIPTION";
                    PassesFrom.DataSource = dt2;
                    PassesFrom.DataBind();
                    PassesFrom.Items.Insert(0, "Select One");

                    string strSql3 = "SELECT ZONE_ID,ZONE_DESCRIPTION FROM ZONE WHERE ZONE_ISDELETED='0' OR ZONE_ISDELETED IS NULL ";
                    SqlDataAdapter da3 = new SqlDataAdapter(strSql3, AccessController.m_connecton);
                    DataTable dt3 = new DataTable();
                    da2.Fill(dt3);
                    PassesTo.DataValueField = "ZONE_ID";
                    PassesTo.DataTextField = "ZONE_DESCRIPTION";
                    PassesTo.DataSource = dt3;
                    PassesTo.DataBind();
                    PassesTo.Items.Insert(0, "Select One");

                    if (dtReader.Rows[i]["IsActive"].ToString() == "True")
                    {
                        chkdel.Checked = true;
                    }
                    else
                    {
                        chkdel.Checked = false;
                        enable(chkdel, 3);
                    }

                    ReaderId.Text = dtReader.Rows[i]["READER_ID"].ToString();

                    ReaderDesc.Text = dtReader.Rows[i]["READER_DESCRIPTION"].ToString();
                    ReaderType.SelectedValue = dtReader.Rows[i]["READER_TYPE"].ToString(); ;
                    ReaderMode.SelectedValue = dtReader.Rows[i]["READER_MODE"].ToString();
                    PassesFrom.Text = dtReader.Rows[i]["READER_PASSES_FROM"].ToString();
                    PassesTo.Text = dtReader.Rows[i]["READER_PASSES_TO"].ToString();
                }
            }
        }

        private void SetPreviousAPData(string CID, int Door)
        {
            string strsql = " select distinct A.READER_ID as READERID,R.READER_DESCRIPTION AS READERDESC,A.DOOR_ID AS DOORID,D.DOOR_TYPE AS DOORTYPE," +
                           " D.DOOR_OPEN_DURATION AS OPENDURATION,D.DOOR_FEEDBACK_DURATION AS FEEDBACKDURATION,A.AP_ID as APID " +
                           " from ACS_ACCESSPOINT_RELATION A,ACS_READER R,ACS_DOOR D where A.AP_CONTROLLER_ID = R.CTLR_ID AND A.READER_ID = R.READER_ID " +
                           " AND A.AP_CONTROLLER_ID = D.CTLR_ID AND A.DOOR_ID = D.DOOR_ID AND A.AP_CONTROLLER_ID = '" + CID + "'";

            SqlDataAdapter daAP = new SqlDataAdapter(strsql, conn);
            DataTable dtAP = new DataTable();
            daAP.SelectCommand.CommandTimeout = 0;
            daAP.Fill(dtAP);

            gvEditAccessPoint.DataSource = dtAP;
            gvEditAccessPoint.DataBind();

            for (int i = 0; i < dtAP.Rows.Count; i++)
            {
                TextBox AccessId = (TextBox)gvEditAccessPoint.Rows[i].Cells[0].FindControl("txtAPID");
                TextBox ReaderId = (TextBox)gvEditAccessPoint.Rows[i].Cells[1].FindControl("txtReaderId");
                TextBox ReaderDesc = (TextBox)gvEditAccessPoint.Rows[i].Cells[2].FindControl("txtReaderdesc");
                DropDownList DoorId = (DropDownList)gvEditAccessPoint.Rows[i].Cells[3].FindControl("ddlDoor");
                DropDownList LockType = (DropDownList)gvEditAccessPoint.Rows[i].Cells[4].FindControl("ddlLocktype");
                TextBox OpenDur = (TextBox)gvEditAccessPoint.Rows[i].Cells[5].FindControl("txtOpenDur");
                TextBox FeedbackDur = (TextBox)gvEditAccessPoint.Rows[i].Cells[6].FindControl("txtFeedbackDur");
                CheckBox chkDel = (CheckBox)gvEditReader.Rows[i].FindControl("chkDel");

                if (chkDel.Checked == false)
                {
                    GridViewRow gvr = (GridViewRow)chkDel.NamingContainer;
                    int index = gvr.RowIndex;
                    gvEditAccessPoint.Rows[index].Enabled = false;
                }

                string strSql = "SELECT CODE,[VALUE] AS DOORNAME FROM ENT_PARAMS WHERE IDENTIFIER = 'DOORTYPE'";
                SqlDataAdapter da = new SqlDataAdapter(strSql, AccessController.m_connecton);
                DataTable dt = new DataTable();
                da.Fill(dt);
                LockType.DataValueField = "CODE";
                LockType.DataTextField = "DOORNAME";
                LockType.DataSource = dt;
                LockType.DataBind();
                LockType.SelectedIndex = 0;
                for (int cnt = 0; cnt < Door; cnt++)
                    DoorId.Items.Add((cnt + 1).ToString());

                ReaderId.Text = dtAP.Rows[i]["READERID"].ToString();
                ReaderDesc.Text = dtAP.Rows[i]["READERDESC"].ToString();
                DoorId.Text = dtAP.Rows[i]["DOORID"].ToString();
                LockType.SelectedValue = dtAP.Rows[i]["DOORTYPE"].ToString();
                OpenDur.Text = dtAP.Rows[i]["OPENDURATION"].ToString();
                FeedbackDur.Text = dtAP.Rows[i]["FEEDBACKDURATION"].ToString();
                AccessId.Text = dtAP.Rows[i]["APID"].ToString();
                a.Add(AccessId.Text);
                ViewState["previous"] = a;

            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEditCtrl.Hide();
            lblMsg.Text = "";
            lblMsg.Visible = false;
            Reset();
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            ArrayList arrDoor = new ArrayList();
            if (gvEditAccessPoint.Rows.Count > 0)
            {
                for (int i = 0; i < gvEditAccessPoint.Rows.Count; i++)
                {
                    DropDownList DoorId = (DropDownList)gvEditAccessPoint.Rows[i].Cells[3].FindControl("ddlDoor");
                    arrDoor.Add(Convert.ToInt16(DoorId.SelectedItem.Text));
                }
                for (int i = 0; i < arrDoor.Count; i++)
                {
                    int a = Convert.ToInt16(arrDoor[0]);
                    int b = 0;
                    for (int j = 0; j < arrDoor.Count; j++)
                    {
                        if (a == Convert.ToInt16(arrDoor[j]))
                        {
                            b = b + 1;
                        }
                        if (b == 3)
                        {
                            lblMsg.Text = "One door can have maximum two readers";
                            lblMsg.Visible = true;
                            return;
                        }
                    }
                }
            }
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlTransaction trans;
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            trans = conn.BeginTransaction();
            objcmd.Transaction = trans;
            try
            {
                objcmd.CommandText = "Select count(*) from ACS_CONTROLLER Where CTLR_IP = '" + txtCIP1.Text.Trim() + "' and  CTLR_ID not in('" + txtCID1.Text + "') and CTLR_ISDELETED = 0";
                int rows1 = Convert.ToInt16(objcmd.ExecuteScalar());

                if (rows1 >= 1)
                {
                    lblMsg.Text = " Controller IP already Exists!";
                    lblMsg.Visible = true;
                    txtCtlrIp.Focus();
                    return;
                }

                string TA = null;
                if (chkTA.Checked)
                {
                    TA = "TA";
                }

                clsCommonHandler obj = new clsCommonHandler();
                string valueip = obj.GetIpAddress();
                
                string query = " Update ACS_CONTROLLER set CTLR_IP = '" + txtCIP1.Text.Trim() + "',CTLR_DESCRIPTION = '" + txtCDesc.Text + "',CTLR_MAC_ID = '" + txtCMacId.Text.Trim() + "'," +
                                     " CTLR_FIRMWARE_VERSION_NO = '" + txtFirmwareVersionNo.Text.Trim() + "',CTLR_HARDWARE_VERSION_NO = '" + txtHardwareVersionNo.Text.Trim() + "', " +
                                     " CTLR_CHK_APB = '" + ddlCAPB.SelectedValue + "',CTLR_APB_TYPE = '" + ddlAPBSchedule.SelectedValue + "',CTLR_APB_TIME = '" + txtABPTime.Text.Trim() + "'," +
                                     " CTLR_AUTHENTICATION_MODE = '" + ddlAuthenticationMode.SelectedValue + "' ,CTLR_CHK_TOC = '" + chkTempOnCard.Checked + "',CLTR_FOR_TA = '" + TA + "',CTLR_KEY_PAD='" + chkKeypadEdit.Checked + "',CTLR_LOCATION_ID='" + ddlLocationEdit.SelectedValue + "' " +
                                     " where CTLR_ID = '" + txtCID1.Text + "' ;";

                if (chkKeypadEdit.Checked != Convert.ToBoolean(Session["KeyPad"]))
                {
                    query = query + " insert into KeypadAuditLog values ('" + txtCID1.Text + "','" + chkKeypadEdit.Checked + "','" + valueip + "',datediff(mi,(select top 1 dt from KeypadAuditLog where CTLR_ID='" + txtCID1.Text + "' order by dt desc),getdate()),getdate()) ";
                    string statusof = "";
                    if (chkKeypadEdit.Checked == true)
                    {
                        statusof = "Enabled";
                    }
                    else
                    {
                        statusof = "Disabled";
                    }
                    StringBuilder strbody = new StringBuilder();
                    strbody.Append("<HTML><BODY>" + "<font family=Times New Roman;>Hello!</br></br>");
                    strbody.Append("   The Keypard status has changed TO   " + statusof);
                    strbody.Append("   At  " + DateTime.Now);
                    strbody.Append("   From Ip address   =   " + valueip);
                    strbody.Append("</br></br>");
                    strbody.Append("   Regards </br> CMS   ");
                    strbody.Append("</BODY></HTML>");
                    strbody.Append("</br></br>");
                    string emailadresss = ConfigurationManager.AppSettings["email_address_alerts"].ToString();
                    string fromadresss = ConfigurationManager.AppSettings["email_address_from"].ToString();
                    Thread thread = new Thread(() => Mail.SendMail(fromadresss, emailadresss, "", "Keypad Status Changed", strbody.ToString()));
                    thread.Start();
                    
                    SaveIpaddress(statusof, Convert.ToInt32(txtCID1.Text));
                }

                objcmd.CommandText = query;

                objcmd.ExecuteNonQuery();

                if (gvEditReader.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEditReader.Rows.Count; i++)
                    {
                        CheckBox chkDelete = (CheckBox)gvEditReader.Rows[i].FindControl("chkDel");
                        TextBox ReaderId = (TextBox)gvEditReader.Rows[i].Cells[0].FindControl("txtReaderId");
                        TextBox ReaderDesc = (TextBox)gvEditReader.Rows[i].Cells[1].FindControl("txtReaderDesc");
                        DropDownList ReaderType = (DropDownList)gvEditReader.Rows[i].Cells[2].FindControl("ddlReaderType");
                        DropDownList ReaderMode = (DropDownList)gvEditReader.Rows[i].Cells[3].FindControl("ddlReaderMode");
                        DropDownList PassesFrom = (DropDownList)gvEditReader.Rows[i].Cells[4].FindControl("ddlPassesFrom");
                        DropDownList PassesTo = (DropDownList)gvEditReader.Rows[i].Cells[5].FindControl("ddlPassesTo");
                        string PF = "";
                        string PT = "";
                        int chk = 0;
                        if (chkDelete.Checked == true)
                            chk = 1;

                        if (PassesFrom.SelectedValue == "Select One")
                        {
                            PF = "";
                        }
                        else
                        {
                            PF = PassesFrom.SelectedValue;
                        }
                        if (PassesTo.SelectedValue == "Select One")
                        {
                            PT = "";
                        }
                        else
                        {
                            PT = PassesTo.SelectedValue;
                        }

                        objcmd.CommandText = " Update ACS_READER set READER_MODE = '" + ReaderMode.SelectedValue + "',READER_DESCRIPTION = '" + ReaderDesc.Text + "',READER_TYPE = '" + ReaderType.SelectedValue + "'," +
                                                 " READER_PASSES_FROM = '" + PF + "',READER_PASSES_TO = '" + PT + "' , IsActive=" + chk + " where CTLR_ID = '" + txtCID1.Text + "' and READER_ID = '" + ReaderId.Text.Trim() + "' ";

                        objcmd.ExecuteNonQuery();
                    }
                }
                ArrayList accPointId = new ArrayList();
                accPointId = (ArrayList)ViewState["previous"];

                if (gvEditAccessPoint.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEditAccessPoint.Rows.Count; i++)
                    {
                        //CheckBox chkDelete = (CheckBox)gvDoor.Rows[i].Cells[0].FindControl("chkDelete");
                        TextBox AccessId = (TextBox)gvEditAccessPoint.Rows[i].Cells[0].FindControl("txtAPID");
                        TextBox APReaderId = (TextBox)gvEditAccessPoint.Rows[i].Cells[1].FindControl("txtReaderId");
                        TextBox ReaderDesc = (TextBox)gvEditAccessPoint.Rows[i].Cells[2].FindControl("txtReaderDesc");
                        DropDownList DoorId = (DropDownList)gvEditAccessPoint.Rows[i].Cells[3].FindControl("ddlDoor");
                        DropDownList LockType = (DropDownList)gvEditAccessPoint.Rows[i].Cells[4].FindControl("ddlLockType");
                        TextBox OpenDur = (TextBox)gvEditAccessPoint.Rows[i].Cells[5].FindControl("txtOpenDur");
                        TextBox Feedbackdur = (TextBox)gvEditAccessPoint.Rows[i].Cells[6].FindControl("txtFeedbackDur");

                        objcmd.CommandText = " Update ACS_DOOR set DOOR_TYPE = '" + LockType.SelectedValue + "',DOOR_OPEN_DURATION = '" + OpenDur.Text.Trim() + "'," +
                                             " DOOR_FEEDBACK_DURATION = '" + Feedbackdur.Text.Trim() + "' where DOOR_ID = '" + DoorId.Text.Trim() + "' and CTLR_ID = '" + txtCID1.Text.Trim() + "'";

                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = " Update ACS_ACCESSPOINT_RELATION set DOOR_ID = " + DoorId.Text.Trim() + ", AP_ID=" + AccessId.Text + "  where AP_ID= '" + accPointId[i].ToString() + "' and " +
                                             " READER_ID = '" + APReaderId.Text + "' and AP_CONTROLLER_ID = '" + txtCID1.Text + "' ";

                        objcmd.ExecuteNonQuery();
                    }
                }

                objcmd.CommandText = "Update ENT_PARAMS set [Value] = '1' where IDENTIFIER = 'CONFIGCHANGE' and CODE = 'CTL' ";
                objcmd.ExecuteNonQuery();

                //objcmd.CommandText = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='APB'";
                //objcmd.ExecuteNonQuery();

                trans.Commit();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                txtCID1.Text = "";
                txtCDesc.Text = "";
                txtCIP1.Text = "";
                txtFirmwareVersionNo.Text = "";
                txtHardwareVersionNo.Text = "";
                txtCMacId.Text = "";
                txtABPTime.Text = "";
                ddlCAPB.SelectedIndex = -1;
                ddlAPBSchedule.SelectedIndex = -1;
                ddlAuthenticationMode.SelectedIndex = -1;
                ddlCType.SelectedIndex = -1;
                gvEditReader.DataSource = null;
                gvEditAccessPoint.DataSource = null;
                mpeEditCtrl.Hide();
                lblMessages.Text = "Record modified successfully";
                lblMessages.Visible = true;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }


        private void SaveIpaddress(string status,int contoller_id)
        {
            string m_connections = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
            clsCommonHandler obj = new clsCommonHandler();
            string valueip = obj.GetIpAddress();
            var query = "insert into IpaddressLog values(@ipaddress,getdate(),@action,@controller_id)";
            using (SqlConnection con = new SqlConnection(m_connections))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ipaddress", valueip);
                    cmd.Parameters.AddWithValue("@action", status);
                    cmd.Parameters.AddWithValue("@controller_id", contoller_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void delete()
        {
            bool check = false;

            for (int i = 0; i < gvController.Rows.Count; i++)
            {
                try
                {
                    CheckBox delrows = (CheckBox)gvController.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        if (check == false)
                        {
                            check = true;
                        }

                        if (ctlrUsed(gvController.Rows[i].Cells[2].Text) == true)
                        {
                            lblMessages.Text = "Controller can not be deleted since it is already used in Access Level";
                            lblMessages.Visible = true;
                            return;
                        }


                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlCommand objcmd = new SqlCommand();
                        objcmd.Connection = conn;
                        SqlTransaction trans;
                        trans = conn.BeginTransaction();
                        objcmd.Transaction = trans;


                        objcmd.CommandText = " Delete from acs_controller where CTLR_ID = '" + gvController.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "delete from acs_reader where CTLR_ID = '" + gvController.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ACS_DOOR set DOOR_ISDELETED = 'true',DOOR_DELETEDDATE = getdate() where CTLR_ID = '" + gvController.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ACS_ACCESSPOINT_RELATION set APR_ISDELETED = 'true',APR_DELETEDDATE = getdate() where AP_CONTROLLER_ID = '" + gvController.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ACS_ACCESSLEVEL_RELATION set ALR_ISDELETED = 'true',ALR_DELETEDDATE = getdate() where CONTROLLER_ID = '" + gvController.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ENT_PARAMS set Value = 1 where IDENTIFIER = 'CONFIGCHANGE' and CODE = 'CTL' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ENT_PARAMS set Value = 1 where IDENTIFIER = 'CONFIGCHANGE' and CODE = 'ALC' ";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "Update ENT_PARAMS set Value = 1 where IDENTIFIER = 'CONFIGCHANGE' and CODE = 'APC' ";
                        objcmd.ExecuteNonQuery();

                        trans.Commit();

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        lblMessages.Text = "Record(s) Deleted Successfully.";
                        lblMessages.Visible = true;
                    }


                    //conn.Close();
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            bindDataGrid();
        }

        private bool ctlrUsed(string ctlrid)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;
                //objcmd.CommandText = "Select count(*) from Acs_AccessPoint_Relation where AP_CONTROLLER_ID = '" + ctlrid + "' and APR_ISDELETED = 'false' ";
                //int cnt = Convert.ToInt32(objcmd.ExecuteScalar());
                //if (cnt >= 1)
                //{
                //    return true;
                //}

                objcmd.CommandText = "Select COUNT(*) from ACS_ACCESSLEVEL_RELATION where CONTROLLER_ID = '" + ctlrid + "' and ALR_ISDELETED = 'false' ";
                int cnt1 = Convert.ToInt32(objcmd.ExecuteScalar());
                if (cnt1 >= 1)
                {
                    return true;
                }

                //objcmd.CommandText = "Select COUNT(*) from ZONE_READER_REL where CONTROLLER_ID = '" + ctlrid + "' and ZONER_ISDELETED = 'false' ";
                //int cnt2 = Convert.ToInt32(objcmd.ExecuteScalar());
                //if (cnt2 >= 1)
                //{
                //    return true;
                //}

                return false;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
                return false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                string strsql1 = "";
                DataTable dt1;
                SqlDataAdapter da1;
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //strsql = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT " +
                //         " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' ";
                //SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                //DataTable dt = new DataTable();
                //da.SelectCommand.CommandTimeout = 0;
                //da.Fill(dt);
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}

                if (txtCtlrID.Text.ToString() == "" && txtCtlrDesc.Text.ToString() == "")
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    strsql = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT, Case when CTLR_KEY_PAD=1 then 'Enabled' else 'Disabled' end CTLR_KEY_PAD ,CTLR_LOCATION_ID " +
                             " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' order by CTLR_DESCRIPTION ASC ";
                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(dt);
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    gvController.DataSource = dt;
                    gvController.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"CTLR_ID-" +txtCtlrID.Text.Trim(), "S" },
				{"CTLR_DESCRIPTION-" +txtCtlrDesc.Text.Trim(), "S" }			
				 };
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    if (txtCtlrID.Text.ToString() != "" && txtCtlrDesc.Text.ToString() == "")
                    {
                        strsql1 = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT,Case when CTLR_KEY_PAD=1 then 'Enabled' else 'Disabled' end CTLR_KEY_PAD ,CTLR_LOCATION_ID " +
                                 " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_ID Like '%" + txtCtlrID.Text + "%'  order by CTLR_DESCRIPTION ASC";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else if (txtCtlrDesc.Text.ToString() != "" && txtCtlrID.Text.ToString() == "")
                    {
                        strsql1 = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT,Case when CTLR_KEY_PAD=1 then 'Enabled' else 'Disabled' end CTLR_KEY_PAD ,CTLR_LOCATION_ID  " +
                            " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_DESCRIPTION Like '%" + txtCtlrDesc.Text + "%' order by CTLR_DESCRIPTION ASC";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else
                    {
                        strsql1 = " Select CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_EVENTS_STORED,CTLR_CURRENT_USER_CNT " +
                           " from ACS_CONTROLLER where CTLR_ISDELETED = 'false' AND CTLR_ID Like '" + txtCtlrID.Text + "%' AND CTLR_DESCRIPTION Like '%" + txtCtlrDesc.Text + "%' order by CTLR_DESCRIPTION ASC ";
                        da1 = new SqlDataAdapter(strsql1, conn);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    //DataTable _tempDT = new DataTable();
                    //Search _sc = new Search();
                    //if (_tempDT != null)
                    //{ _tempDT.Rows.Clear(); }
                    //_tempDT = _sc.searchTable(values, dt1);
                    gvController.DataSource = dt1;
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

                Reset();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void gvController_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvController.PageIndex = e.NewPageIndex;
                bindDataGrid();
                //btnSearch_Click(sender, e);
                Reset();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = Convert.ToInt32(((DropDownList)gvController.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
                Reset();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }

        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = gvController.PageIndex - 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
                Reset();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvController.PageIndex = gvController.PageIndex + 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
                Reset();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ControllerView");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PnlInfo.Visible = false;
            getControllerID();
            Reset();
            // txtControllerId.ReadOnly = true;
            mpeAddCtrl.Show();
        }

        protected void ddlAntipass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAntipass.SelectedValue == "N")
            {
                ddlAPB.Enabled = false;
                ddlAPB.SelectedIndex = 0;
                txtAPBTimed.Enabled = false;
            }
            else
            {
                ddlAPB.Enabled = true;
                txtAPBTimed.Enabled = true;
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            delete();
            //   mpeConfirmPanel.Hide();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            // mpeConfirmPanel.Hide();
        }

        protected void ddlAPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAPB.SelectedValue == "T")
            {
                txtAPBTimed.Enabled = true;
            }
            else
            {
                txtAPBTimed.Enabled = false;
                txtAPBTimed.Text = "";
            }
        }

        protected void ddlDoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList Door = (DropDownList)gvReader.Rows[0].Cells[2].FindControl("ddlDoor");

            DropDownList Door = (DropDownList)sender;

            GridViewRow gvr = (GridViewRow)Door.NamingContainer;
            int i = gvr.RowIndex;

            TextBox apreaderId = (TextBox)gvAccesspoint.Rows[i].Cells[0].FindControl("txtReaderId");
            apreaderId.Text = txtControllerId.Text + "." + Door.SelectedItem.Text;
        }

        protected void ddlDoorChanged(object sender, EventArgs e)
        {
            DropDownList Door = (DropDownList)sender;

            GridViewRow gvr = (GridViewRow)Door.NamingContainer;
            int i = gvr.RowIndex;

            TextBox apreaderId = (TextBox)gvEditAccessPoint.Rows[i].Cells[0].FindControl("txtAPID");
            apreaderId.Text = txtCID1.Text + "." + Door.SelectedItem.Text;
        }


        protected void enable(CheckBox delete, int flag)
        {
            GridViewRow gvrChk = (GridViewRow)delete.NamingContainer;
            int i = gvrChk.RowIndex;

            if (delete.Checked == false)
            {
                TextBox readertext = (TextBox)gvrChk.FindControl("txtReaderId");
                readertext.Enabled = false;

                TextBox readerDesctext = (TextBox)gvrChk.FindControl("txtReaderDesc");
                readerDesctext.Enabled = false;

                DropDownList ddltype = (DropDownList)gvrChk.FindControl("ddlReaderType");
                ddltype.Enabled = false;

                DropDownList ddlmode = (DropDownList)gvrChk.FindControl("ddlReaderMode");
                ddlmode.Enabled = false;

                DropDownList ddlPass = (DropDownList)gvrChk.FindControl("ddlPassesFrom");
                ddlPass.Enabled = false;

                DropDownList ddlPassesTo = (DropDownList)gvrChk.FindControl("ddlPassesTo");
                ddlPassesTo.Enabled = false;

                if (flag == 1)
                    gvAccesspoint.Rows[i].Enabled = false;
                else if (flag == 0)
                    gvEditAccessPoint.Rows[i].Enabled = false;


            }
            else
            {
                TextBox readertext = (TextBox)gvrChk.FindControl("txtReaderId");
                readertext.Enabled = true;

                TextBox readerDesctext = (TextBox)gvrChk.FindControl("txtReaderDesc");
                readerDesctext.Enabled = true;

                DropDownList ddltype = (DropDownList)gvrChk.FindControl("ddlReaderType");
                ddltype.Enabled = true;

                DropDownList ddlmode = (DropDownList)gvrChk.FindControl("ddlReaderMode");
                ddlmode.Enabled = true;

                DropDownList ddlPass = (DropDownList)gvrChk.FindControl("ddlPassesFrom");
                ddlPass.Enabled = true;

                DropDownList ddlPassesTo = (DropDownList)gvrChk.FindControl("ddlPassesTo");
                ddlPassesTo.Enabled = true;

                if (flag == 1)
                    gvAccesspoint.Rows[i].Enabled = true;
                else if (flag == 0)
                    gvEditAccessPoint.Rows[i].Enabled = true;
            }
        }

        protected void chkDeleteChanged(object sender, EventArgs e)
        {
            CheckBox delete = (CheckBox)sender;
            enable(delete, 1);
        }

        protected void chkEditChanged(object sender, EventArgs e)
        {
            CheckBox del = (CheckBox)sender;
            enable(del, 0);
        }

    }
}