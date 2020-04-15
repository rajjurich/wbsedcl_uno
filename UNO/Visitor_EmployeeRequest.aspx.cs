using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
namespace UNO
{
    public partial class Visitor_EmployeeRequest : System.Web.UI.Page
    {
        bool flagVisitorEnabled = false;
     string Url = "http://localhost:62070/ScanDocument.aspx";
        // string Url = "http://10.61.7.204/uno/ScanDocument.aspx";
     // String Url = HttpContext.Current.Request.Url.Authority;
        public static string EmpCode;
        public static int ImageCount = 0;
        string Image64 = "";
        static string path = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        clsEmailTemplate clsTemp = new clsEmailTemplate();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Url = Url + "/uno/ScanDocument.aspx";
            try
            {
                hdConn.Value = ConfigurationManager.ConnectionStrings["connection_string"].ToString().Replace(' ', ',');
                if (!IsPostBack)
                {
                    if (CallManagementHandler.GetLevelCode(Convert.ToInt16(Session["levelId"])).ToLower() != "admin")
                    {
                        btnNew.Attributes.Add("style", "visibility:visible");
                        btnDel.Attributes.Add("style", "visibility:visible");
                    }
                    else
                    {
                        Label1.Text = "View Visitor";
                        rolewiseView();
                        disablecontrol();
                    }
                    if (flagVisitorEnabled != true)
                    {

                    }
                    bindControllerGvAdd();
                    bindControllerGvEdit();
                    binddata();

                    fillddl();
                    Fill_location();
                    // fillVisitor();
                }
                RegisterPostBackControl();
                //ModelPopupFor_Document.Hide();
                btnDel.Attributes.Add("onclick", "javascript:return handleDelete('" + GvVisitor.ClientID + "');");

                CalendarExtender4.StartDate = System.DateTime.Now;
                CalendarExtender5.StartDate = System.DateTime.Now;


                CalendarExtender1.StartDate = System.DateTime.Now;
                CalendarExtender2.StartDate = System.DateTime.Now;
                //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>start();</script>");
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "Script", "validateChosen();", true);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "script", "DisableBtnSave();", true);
        }


        public void startexe(object o, EventArgs e)
        {

        }
        public void disablecontrol()
        {
            txtVisitornameEdit.ReadOnly = true;
            txtMiddleNameEdit.ReadOnly = true;
            txtlastNameEdit.ReadOnly = true;
            txtCompanyNameEdit.ReadOnly = true;
            txtMobileNoEdit.ReadOnly = true;
            txtmobedit.ReadOnly = true;
            ddlnatureOfworkEdit.Enabled = false;
            txtDesignationEdit.ReadOnly = true;

            txtAppoinmentFromDateEdit.ReadOnly = true;
            txtAppoinmentToDateEdit.ReadOnly = true;
            CalendarExtender1.Enabled = false;
            CalendarExtender2.Enabled = false;

            txtAppoinMentFromtimeEdit.ReadOnly = true;
            txtAppointmentToTimeEdit.ReadOnly = true;
            ddApprovalEdit.Enabled = false;
            ddl_location_edit.Enabled = false;
            txtPurposeEdit.ReadOnly = true;

            drpnationalityedit.Enabled = false;
            txt_remarks.ReadOnly = true;
            txtAdditionalInformationEdit.ReadOnly = true;

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

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
            return EMPlist;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetdataByRFID(string prefixText, int count, string contextKey)
        {
            List<String> EMPlist = new List<String>();
            List<String> tempList = new List<String>();
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
                //Make your database connection here
                string strSQL = "select    EPD_FIRST_NAME+ '' + isnull (EPD_MIDDLE_NAME,'') + ''+ isnull(EPD_LAST_NAME,'') +'      |      '+EPD_EMPID as EPD_EMPID from ENT_EMPLOYEE_PERSONAL_DTLS where EPD_FIRST_NAME like '" + prefixText + "%'   order by EPD_EMPID,EPD_FIRST_NAME";
                // string strSQL = "SELECT isnull(rfid,'') as EPD_EMPID FROM Veh_RFIDstock WHERE TagStatus='New' and rfid like '" + prefixText + "%'   order by rfid";

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

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
            return EMPlist;
        }


        private void rolewiseView()
        {
            mpeAddEmployee.Hide();
            btnNew.Attributes.Add("style", "visibility:hidden");
            btnDel.Attributes.Add("style", "visibility:hidden");
            GvVisitor.Columns[0].ItemStyle.CssClass = "Display";
            btnSubmitEdit.Attributes.Add("style", "visibility:hidden");
            GvVisitor.Columns[1].HeaderText = "View";
            //GvVisitor.Columns[1].I = "View";
            //GvVisitor.Columns[1].ItemStyle.CssClass = "Display";
            GvVisitor.Columns[0].HeaderStyle.CssClass = "Display";
            // GvVisitor.Columns[1].HeaderStyle.CssClass = "Display";
        }

        public void RegisterPostBackControl()
        {
            foreach (GridViewRow row in GvVisitor.Rows)
            {
                LinkButton lnkFull = row.FindControl("lnkModify") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
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
                //string strGetData = "select id,vehicleName,entityId,VehicleRegistrationNumber,rfId,CONVERT(varchar(10),enrollmentDate,103) as enrollmentDate,CONVERT(varchar(10),rfidValidityDate,103) as rfidValidityDate  from veh_vehicleEnrollment where isdeleted=0";
                SqlCommand cmd = new SqlCommand("Visitor_sp_fill_Visitor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Requestor", Session["uid"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                GvVisitor.DataSource = dt;
                GvVisitor.DataBind();

                DropDownList ddl = (DropDownList)GvVisitor.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= GvVisitor.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (GvVisitor.PageIndex + 1).ToString();
                Label lblcount = (Label)GvVisitor.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)GvVisitor.DataSource).Rows.Count.ToString() + " Records.";
                if (GvVisitor.PageCount == 0)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (GvVisitor.PageIndex + 1 == GvVisitor.PageCount)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (GvVisitor.PageIndex == 0)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)GvVisitor.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((GvVisitor.PageSize * GvVisitor.PageIndex) + 1) + " to " + (GvVisitor.PageSize * (GvVisitor.PageIndex + 1));

                ((Label)GvVisitor.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((GvVisitor.PageSize * GvVisitor.PageIndex) + 1) + " to " + (((GvVisitor.PageSize * (GvVisitor.PageIndex + 1)) - 10) + GvVisitor.Rows.Count);

                GvVisitor.BottomPagerRow.Visible = true;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int count = 0;
                if (ddlNatureOfVisit.SelectedIndex == -1 || ddlNatureOfVisit.SelectedValue == "0")
                {
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Nature of Visit is a Mandatory Field.');", true);
                    lblSaveMessages.Visible = true;
                    lblSaveMessages.Text = "Nature of Visit is a Mandatory Field.";
                    mpeAddEmployee.Show();
                    return;

                }

                if (ddlApprovalAuthority.SelectedIndex == 0)
                {
                    lblSaveMessages.Visible = true;
                    lblSaveMessages.Text = "Please Select Approval Authority";
                    mpeAddEmployee.Show();
                    return;
                }

                if (ddl_location.SelectedIndex == 0)
                {
                    lblSaveMessages.Visible = true;
                    lblSaveMessages.Text = "Please Select Location";
                    mpeAddEmployee.Show();
                    return;
                }

                if (drpnationality.SelectedIndex == 0)
                {
                    lblSaveMessages.Text = "Please Select Nationality";
                    lblSaveMessages.Visible = true;
                    mpeAddEmployee.Show();
                    return;
                }

                lblSaveMessages.Text = "";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd1 = new SqlCommand("Visitor_sp_insertRequest", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                string VisitorID = "";
                if (ViewState["VisitorID"] != null)
                {
                    VisitorID = ViewState["VisitorID"].ToString();
                }
                cmd1.Parameters.Add("@visitorid", SqlDbType.VarChar).Value = VisitorID;
                cmd1.Parameters.Add("@fromTime", SqlDbType.DateTime).Value = "01/01/1900 " + txtAppoinmentFromTime.Text + ":00";
                cmd1.Parameters.Add("@toTime", SqlDbType.DateTime).Value = "01/01/1900 " + txtAppoinmentToTime.Text + ":00";
                cmd1.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtAppoinmentFromDate.Text, "dd/MM/yyyy", null);
                cmd1.Parameters.Add("@toDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtAppoinmentToDate.Text, "dd/MM/yyyy", null);
                cmd1.Parameters.Add("@NatureOfWork", SqlDbType.VarChar).Value = ddlNatureOfVisit.SelectedValue;
                cmd1.Parameters.Add("@additional_info", SqlDbType.VarChar).Value = txtAdditionalInfoAdd.Text;
                cmd1.Parameters.Add("@visitorName", SqlDbType.VarChar).Value = txtVisitorName.Text;
                cmd1.Parameters.Add("@visitorCompany", SqlDbType.VarChar).Value = txtComapany.Text;
                cmd1.Parameters.Add("@mobileNo", SqlDbType.VarChar).Value = txtmob.Text + "~" + txtMobilleNo.Text;
                cmd1.Parameters.Add("@approvalAuthority", SqlDbType.VarChar).Value = ddlApprovalAuthority.SelectedValue;
                cmd1.Parameters.Add("@Location", SqlDbType.VarChar).Value = ddl_location.SelectedValue;
                cmd1.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Session["uid"].ToString();
                cmd1.Parameters.Add("@Designation", SqlDbType.VarChar).Value = txtDesignation.Text;
                cmd1.Parameters.Add("@PurPoseOfVisit", SqlDbType.VarChar).Value = txtPurposeAdd.Text;
                cmd1.Parameters.Add("@middleName", SqlDbType.VarChar).Value = txtMiddleName.Text;
                cmd1.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                cmd1.Parameters.Add("@nationality", SqlDbType.VarChar).Value = drpnationality.SelectedValue;
                cmd1.Parameters.AddWithValue("@URL", path);
                cmd1.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd1.Parameters.AddWithValue("@RequestID", 0).Direction = ParameterDirection.Output;
                cmd1.Parameters.AddWithValue("@LOGINDEX", 0).Direction = ParameterDirection.Output;
                cmd1.ExecuteNonQuery();
                string RequestID = cmd1.Parameters["@RequestID"].Value.ToString();
                Int32 LogIndex = Convert.ToInt32(cmd1.Parameters["@LOGINDEX"].Value.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("<RFIDInventory>");

                for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
                {
                    CheckBox chkAdd = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                    string alid = gvControllerAdd.Rows[i].Cells[1].Text;

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string str = "SELECT * FROM ACS_ACCESSLEVEL_RELATION WHERE AL_ID='" + alid + "' AND ALR_ISDELETED=0";
                    SqlCommand cmd = new SqlCommand(str, conn);
                    SqlDataAdapter acsdap = new SqlDataAdapter(cmd);
                    DataTable acsdt = new DataTable();
                    acsdap.Fill(acsdt);
                    for (int j = 0; j < acsdt.Rows.Count; j++)
                    {
                        string controllerID, ReaderId;
                        controllerID = acsdt.Rows[j]["CONTROLLER_ID"].ToString();
                        ReaderId = acsdt.Rows[j]["RD_ZN_ID"].ToString();

                        if (chkAdd.Checked == true)
                        {
                            count += 1;
                            sb.Append("<Transaction>");
                            sb.Append("<RequestId>" + RequestID + "</RequestId>");
                            sb.Append("<Controller_ID>" + controllerID + "</Controller_ID>");
                            sb.Append("<Reader_ID>" + ReaderId + "</Reader_ID>");
                            sb.Append("<alId>" + alid + "</alId>");
                            sb.Append("</Transaction>");


                        }

                    }

                }
                sb.Append("</RFIDInventory>");
                string s = sb.ToString();


                SqlCommand cmd2 = new SqlCommand("Visitor_spAssignControllerInsert", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = sb.ToString();
                cmd2.ExecuteNonQuery();
                if (count > 0)
                    UNO.DataTansactionLog.InsertLog(sb.ToString(), "", "Insert", "Visitor_Visitor_Access", path, LogIndex.ToString());

                binddata();

                #region "Mail"
                ///mail 
                ///
                string cc = "";
                string subject = "Visitor Request";
                string UserMailId = "", managerMail_id = "";
                string visitorname = txtVisitorName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;

                SqlCommand cmdManager = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" + ddlApprovalAuthority.SelectedValue + "')", conn);
                SqlDataAdapter dap = new SqlDataAdapter(cmdManager);
                DataTable dtManager = new DataTable();
                dap.Fill(dtManager);

                ////////////
                SqlCommand cmdEmployee = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" + Session["uid"].ToString() + "')", conn);
                SqlDataAdapter dap1 = new SqlDataAdapter(cmdEmployee);
                DataTable dtemploye = new DataTable();
                dap1.Fill(dtemploye);


                //------------arbaaz 20160404------------
                DataTable infoDT = new DataTable();
                SqlCommand cmdInfo = new SqlCommand();
                cmdInfo.CommandType = CommandType.StoredProcedure;
                cmdInfo.CommandText = "Proc_GetVisitorInfoForMail";
                cmdInfo.Connection = conn;
                cmdInfo.Parameters.AddWithValue("@userid", Session["uid"].ToString());
                cmdInfo.Parameters.AddWithValue("@managerid", ddlApprovalAuthority.SelectedValue);
                cmdInfo.Parameters.AddWithValue("@RequestID", RequestID);

                StringBuilder sbaccessdetails = new StringBuilder();
                string accessdetails = "";
                if (gvControllerAdd.Rows.Count > 0)
                {
                    for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
                    {
                        CheckBox chkControllerADD = (CheckBox)gvControllerAdd.Rows[i].FindControl("chkControllerADD");
                        if (chkControllerADD.Checked)
                        {
                            sbaccessdetails.Append(gvControllerAdd.Rows[i].Cells[1].Text + " - " + gvControllerAdd.Rows[i].Cells[2].Text + ", ");
                        }
                    }
                    if (sbaccessdetails != null)
                    {
                        accessdetails = sbaccessdetails.ToString();
                    }
                }


                SqlDataAdapter dap3 = new SqlDataAdapter(cmdInfo);
                dap3.Fill(infoDT);

                managerMail_id = infoDT.Rows[0]["manageremailid"].ToString(); // To manger
                //UserMailId = infoDT.Rows[0]["useremailid"].ToString(); // From emp
                UserMailId = clsTemp.fromMail(); // From Emp
                //UserMailId = "abc@sac.isro.gov.in";

                string employeeidandname, Visitorid, Visitorname, Companyname, MobileNo, Nationality, NatureofWork, AppointmentfromDatetime, AppointmenttoDatetime, PurposeofVisit, VisitorAccessDetail, Additionalinfo = "";
                employeeidandname = infoDT.Rows[0]["employeeidandname"].ToString();
                Visitorid = infoDT.Rows[0]["Visitorid"].ToString();
                Visitorname = infoDT.Rows[0]["Visitorname"].ToString();
                Companyname = infoDT.Rows[0]["Companyname"].ToString();
                MobileNo = infoDT.Rows[0]["MobileNo"].ToString();
                Nationality = infoDT.Rows[0]["Nationality"].ToString();
                NatureofWork = infoDT.Rows[0]["NatureofWork"].ToString();
                AppointmentfromDatetime = infoDT.Rows[0]["AppointmentfromDatetime"].ToString();
                AppointmenttoDatetime = infoDT.Rows[0]["AppointmenttoDatetime"].ToString();
                PurposeofVisit = infoDT.Rows[0]["PurposeofVisit"].ToString();
                VisitorAccessDetail = infoDT.Rows[0]["VisitorAccessDetail"].ToString();
                Additionalinfo = infoDT.Rows[0]["Additionalinfo"].ToString();
                subject = "ACS s/w – " + Session["uid"].ToString() + " visitor request.";

                //string message = "<HTML> <BODY> <span font family='Times New Roman;'>Dear sir/madam,</span> <br/></br> <font family=Times New Roman;>Visitor Request submitted by " + employeeidandname + " needs your approval for visitor. </br> </br>";
                //message = message + "Details of the visitor request: </br></br> Visitor id: " + Visitorid + " </br>";
                //message = message + "Visitor name: " + Visitorname + " </br>";
                //message = message + "Company name: " + Companyname + " </br> Mobile No: " + MobileNo + " </br> Nationality: " + Nationality + "</br> Nature of Work: " + NatureofWork + " </br> Appointment from Date/time: " + AppointmentfromDatetime + " </br>";
                //message = message + "Appointment to Date/time: " + AppointmenttoDatetime + " </br> Purpose of Visit: " + PurposeofVisit + "</br> Visitor Access Detail: " + accessdetails + "</br> Additional info:- " + Additionalinfo + "</br></br> ";
                //message = message + "click here to approve/reject: </br> http://10.61.7.204/uno/home.aspx. </br></br> If you find any difficulty, contact ACS Control room, Ext. :3004/3016. </br> </br>";
                //message = message + "Regards, </br> </br> ACS Team, SAC </font> </br></br> </BODY> </HTML>";

                string message = "Dear sir/madam," + Environment.NewLine + Environment.NewLine + "Visitor Request submitted by " + employeeidandname + " needs your approval for visitor. " + Environment.NewLine + Environment.NewLine;
                message = message + "Details of the visitor request:" + Environment.NewLine + "Visitor id: " + Visitorid + Environment.NewLine;
                message = message + "Visitor name: " + Visitorname + Environment.NewLine;
                message = message + "Company name: " + Companyname + Environment.NewLine + "Mobile No: " + MobileNo + Environment.NewLine + "Nationality: " + Nationality + Environment.NewLine + "Nature of Work: " + NatureofWork + Environment.NewLine + "Appointment from Date/time: " + AppointmentfromDatetime + Environment.NewLine;
                message = message + "Appointment to Date/time: " + AppointmenttoDatetime + Environment.NewLine + "Purpose of Visit: " + PurposeofVisit + Environment.NewLine + "Visitor Access Detail: " + accessdetails + Environment.NewLine + "Additional info:- " + Additionalinfo + Environment.NewLine + Environment.NewLine;
                message = message + "Click here to approve/reject: " + Environment.NewLine + "http://10.61.7.204/uno/home.aspx. " + Environment.NewLine + Environment.NewLine + "If you find any difficulty, contact ACS Control room, Ext. :3004/3016." + Environment.NewLine + Environment.NewLine;
                message = message + "Regards," + Environment.NewLine + "ACS Team, SAC ";


                //------------arbaaz 20160404------------

                //Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));

                //thread.Start();

                //Mail.SendMail(UserMailId, managerMail_id, cc, subject, message);

                string datefield = DateTime.Now.ToString();
                clsTemp.sendMail(UserMailId, managerMail_id, subject, message, datefield);

                //mail end
                #endregion


                txtVisitorName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Enabled = true;
                txtComapany.Enabled = true;
                //txtmob.Enabled = true;
                //txtMobilleNo.Enabled = true;
                //txtDesignation.Enabled = true;

                txtAdditionalInfoAdd.Text = "";
                txtPurposeAdd.Text = "";
                txtVisitorName.Text = "";
                txtComapany.Text = "";
                txtDesignation.Text = "";
                txtAppoinmentFromDate.Text = "";
                txtAppoinmentToDate.Text = "";
                txtAppoinmentToTime.Text = "";
                txtAppoinmentFromTime.Text = "";
                txtLastName.Text = "";
                txtMiddleName.Text = "";
                txtmob.Text = "";
                txtMobilleNo.Text = "";
                ddlNatureOfVisit.SelectedIndex = 0;
                ddlApprovalAuthority.SelectedIndex = 0;
                ddl_location.SelectedIndex = 0;
                ddelSerchVisitor.SelectedIndex = 0;
                drpnationality.SelectedValue = "0";

                CalendarExtender4.StartDate = System.DateTime.Now;
                CalendarExtender5.StartDate = System.DateTime.Now;

                CalendarExtender1.StartDate = System.DateTime.Now;
                CalendarExtender2.StartDate = System.DateTime.Now;

                DateTime today = DateTime.Today;
                txtAppoinmentFromDate.Text = today.ToString("dd/MM/yyyy");
                txtAppoinmentToDate.Text = today.ToString("dd/MM/yyyy");

                //Btnclear1.Enabled = true;
                for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
                {
                    CheckBox chkClear = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                    chkClear.Checked = false;
                }

                lblSaveMessages.Text = "Record Saved Successfully";

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ViewState["VisitorID"] = null;
                //mpeAddEmployee.Show();
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
                if (dt.Rows.Count > 0)
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

        public void fillVisitor()
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("Visitor_FillSerchVisitor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(dt);
            ddelSerchVisitor.Items.Clear();

            if (dt != null)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ddelSerchVisitor.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["Visitor_id"].ToString()));

                    if (dt.Rows[i]["blacklisted"].ToString() == "B")
                    {
                        ddelSerchVisitor.Items[i].Attributes.Add("style", "color:red;font-weight:bold");

                    }

                }
            }


            if (ViewState["VisitorID"] != null)
            {
                ddelSerchVisitor.SelectedValue = ViewState["VisitorID"].ToString();
                ddelSerchVisitor.Items.Insert(0, new ListItem("Select"));
            }
            else
            {
                ddelSerchVisitor.Items.Insert(0, new ListItem("Select"));
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public void Fill_location()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strloc = "select * from ent_params where identifier='location' and module='ACS'";
                SqlCommand cmd = new SqlCommand(strloc, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dap.Fill(dt);
                ddl_location.DataSource = dt;
                ddl_location.DataTextField = "VALUE";
                ddl_location.DataValueField = "CODE";
                ddl_location.DataBind();
                ddl_location.Items.Insert(0, new ListItem("Select", "0"));
                ddl_location.SelectedIndex = 0;

                ddl_location_edit.DataSource = dt;
                ddl_location_edit.DataTextField = "VALUE";
                ddl_location_edit.DataValueField = "CODE";
                ddl_location_edit.DataBind();
                ddl_location_edit.Items.Insert(0, new ListItem("Select", "0"));
                ddl_location_edit.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
 
        }

        public void fillddl()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string employeeid = Session["uid"].ToString();

            SqlCommand cmd = new SqlCommand("Visitor_ApprovalAuthority", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empCode", employeeid.ToLower());
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(dt);
            ddlApprovalAuthority.DataSource = dt;
            ddlApprovalAuthority.DataTextField = "name";
            ddlApprovalAuthority.DataValueField = "approverId";
            ddlApprovalAuthority.DataBind();
            ddlApprovalAuthority.Items.Insert(0, new ListItem("Select", "0"));
            ddlApprovalAuthority.SelectedIndex = 0;

            ddApprovalEdit.DataSource = dt;
            ddApprovalEdit.DataTextField = "name";
            ddApprovalEdit.DataValueField = "approverId";
            ddApprovalEdit.DataBind();
            ddApprovalEdit.Items.Insert(0, new ListItem("Select", "0"));
            ddApprovalEdit.SelectedIndex = 0;

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                GvVisitor.PageIndex = Convert.ToInt32(((DropDownList)GvVisitor.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                btnSearch_Click(sender, e);
                //binddata();
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
                GvVisitor.PageIndex = GvVisitor.PageIndex - 1;
                btnSearch_Click(sender, e);
                //binddata(); ;
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
                GvVisitor.PageIndex = GvVisitor.PageIndex + 1;
                btnSearch_Click(sender, e);
                //binddata(); ;
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
                lblMsg.Text = string.Empty;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Visitor_sp_fill_Visitor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Requestor", Session["uid"].ToString());
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
                    GvVisitor.DataSource = dt;
                    GvVisitor.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"mobileNo~" +txtUserID.Text.Trim(), "S" },
                {"Visitor_Name~" +txtLevelID.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    GvVisitor.DataSource = _tempDT;
                    GvVisitor.DataBind();
                }
                DropDownList ddl = (DropDownList)GvVisitor.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= GvVisitor.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (GvVisitor.PageIndex + 1).ToString();
                Label lblcount = (Label)GvVisitor.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)GvVisitor.DataSource).Rows.Count.ToString() + " Records.";
                if (GvVisitor.PageCount == 0)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (GvVisitor.PageIndex + 1 == GvVisitor.PageCount)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (GvVisitor.PageIndex == 0)
                {
                    ((Button)GvVisitor.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)GvVisitor.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((GvVisitor.PageSize * GvVisitor.PageIndex) + 1) + " to " + (((GvVisitor.PageSize * (GvVisitor.PageIndex + 1)) - 10) + GvVisitor.Rows.Count);

                GvVisitor.BottomPagerRow.Visible = true;

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
            txtVisitorName.Enabled = true;
            txtMiddleName.Enabled = true;
            txtLastName.Enabled = true;
            txtComapany.Enabled = true;
            //txtmob.Enabled = true;
            //txtMobilleNo.Enabled = true;
            //txtDesignation.Enabled = true;

            lblSaveMessages.Text = "";
            txtAdditionalInfoAdd.Text = "";
            txtPurposeAdd.Text = "";
            txtVisitorName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            ddlApprovalAuthority.SelectedIndex = 0;
            ddl_location.SelectedIndex = 0;
            txtComapany.Text = "";
            txtDesignation.Text = "";
            txtmob.Text = "";
            txtMobilleNo.Text = "";
            drpnationality.SelectedValue = "0";
            ddlNatureOfVisit.SelectedValue = "0";

            txtAppoinmentToTime.Text = "";
            txtAppoinmentFromTime.Text = "";

            Btnclear1.Enabled = true;

            DateTime today = DateTime.Today;

            int daysUntilTuesday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextsaturday = today.AddDays(daysUntilTuesday);

            txtAppoinmentFromDate.Text = today.ToString("dd/MM/yyyy");
            txtAppoinmentToDate.Text = today.ToString("dd/MM/yyyy");

            fillVisitor();
            //mpeAddVehicle.Show();
            ddelSerchVisitor.SelectedIndex = 0;
            mpeAddEmployee.Show();
            ViewState["VisitorID"] = null;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "Script", "validateChosen();", true);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gvControllerAdd.Rows.Count; i++)
            {
                CheckBox chkClear = gvControllerAdd.Rows[i].FindControl("chkControllerADD") as CheckBox;
                chkClear.Checked = false;
            }

        }

       

        protected void GvVisitor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "VehHistory")
                {
                    string id = e.CommandArgument.ToString();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_GetVehEntity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dap.Fill(dt);
                    //
                    if (dt.Rows.Count > 0)
                    {

                    }

                }
                if (e.CommandName == "VehData")
                {
                    string id = e.CommandArgument.ToString();
                }

                if (e.CommandName == "Modify")
                {
                    // FillEditedData(e);
                    fillddl();
                    Fill_location();
                    FillEditedData(e);
                    ModalPopupExtender1.Show();

                }

                if (e.CommandName == "ScanDocumnent")
                {
                    string request_id = e.CommandArgument.ToString();
                    ViewState["Rq_id"] = request_id;
                    binddocummentdata();
                    ModelPopupFor_Document.Show();
                }

                if (e.CommandName == "Remove")
                {
                    string id = e.CommandArgument.ToString();
                    ViewState["deleteRowID"] = id;
                    lblMsg.Text = "";
                    // mpeDelVehicle.Show();
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
                    LinkButton lbtnAction, lnkVehRegNo;
                    GridViewRow row;
                    row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    lbtnAction = GvVisitor.Rows[row.RowIndex].FindControl("lnlStatus") as LinkButton;
                    status = lbtnAction.Text;
                    lnkVehRegNo = GvVisitor.Rows[row.RowIndex].FindControl("lnkVehRegNo") as LinkButton;
                    string VehicleNo = lnkVehRegNo.Text, Userid = Session["uid"].ToString();

                    string Rfid = GvVisitor.Rows[row.RowIndex].Cells[6].Text;
                    if (status == "Enabled")
                    {
                        update = "update veh_vehicleEnrollment set isEnabled=1 where id='" + id + "'";
                        lbtnAction.ToolTip = "Click here to disable record";

                        SqlCommand cmd = new SqlCommand("sp_vehEnableDisable", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@vehicleEnrollmentId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@rfId", Rfid);
                        cmd.Parameters.AddWithValue("@user_id", Userid);
                        cmd.Parameters.AddWithValue("@enableDate", null);
                        cmd.Parameters.AddWithValue("@disableDate", DateTime.ParseExact(System.DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null));
                        cmd.Parameters.AddWithValue("@Enabled_Disabled", 1);

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
                        cmd.Parameters.AddWithValue("@disableDate", null);
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

            ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Script", "validatchosen();", true);

        }

        private void FillEditedData(GridViewCommandEventArgs e)
        {
            ddApprovalEdit.Attributes.Add("class", "chosen-select");
            ddl_location_edit.Attributes.Add("class", "chosen-select");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Script", "abc();", true);
            lblerrlMsg.Text = "";

            string vehicleid = e.CommandArgument.ToString();
            ViewState["RowID"] = vehicleid;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string selectData = "SELECT RequestID,Visitor_Name,VisitorCompany," +
            "mobileNo," +
            "nature_of_work," +
            "approval_status," +
            "convert(VARCHAR(5),Visitor_Allowed_From_time,108) AS Visitor_Allowed_From_time,Approver_Remarks ," +
            "convert(VARCHAR(5),visitor_Allowed_To_Time,108) AS visitor_Allowed_To_Time," +
            "convert(VARCHAR(10),appointment_from_date,103) AS appointment_from_date," +
            "convert(VARCHAR(10),appointment_to_date,103) AS appointment_to_date,additional_Info,Designation, " +
            "Approval_Authority_Code,PurposeOfVisit,Visitor_MiddleName,Visitor_LastName,visitor_nationality,visitor_location" +
            "  FROM Visitor_Appointment_Request " +
            "WHERE RequestID=" + vehicleid + "";

            SqlCommand cmdUpdate = new SqlCommand(selectData, conn);

            SqlDataReader dr = cmdUpdate.ExecuteReader();
            while (dr.Read())
            {
                //drvalue not changed
                txtVisitornameEdit.Text = dr["Visitor_Name"].ToString();
                txtMiddleNameEdit.Text = dr["Visitor_MiddleName"].ToString();
                txtlastNameEdit.Text = dr["Visitor_LastName"].ToString();

                txtCompanyNameEdit.Text = dr["VisitorCompany"].ToString();

                string app = dr["approval_status"].ToString();

                if (app.ToString() == "A")
                {
                    btnSubmitEdit.Enabled = false;
                }
                else
                {
                    btnSubmitEdit.Enabled = true;

                }


                if (dr["mobileNo"].ToString().IndexOf("~") > 0)
                {
                    string[] s = dr["mobileNo"].ToString().Split('~');
                    txtMobileNoEdit.Text = s[1].ToString();
                    txtmobedit.Text = s[0].ToString();
                }
                else
                {
                    txtMobileNoEdit.Text = dr["mobileNo"].ToString();
                }



                if (dr["Approval_Authority_Code"].ToString() != "null" && dr["Approval_Authority_Code"].ToString() != "")
                {
                    ddApprovalEdit.SelectedValue = dr["Approval_Authority_Code"].ToString();
                }

                if (dr["visitor_location"].ToString() != "null" && dr["visitor_location"].ToString() != "")
                {
                    ddl_location_edit.SelectedValue = dr["visitor_location"].ToString();
                }
                ddlnatureOfworkEdit.SelectedValue = dr["nature_of_work"].ToString();
                txtAppoinmentFromDateEdit.Text = dr["appointment_from_date"].ToString();
                txtAppoinmentToDateEdit.Text = dr["appointment_to_date"].ToString();
                txtAppointmentToTimeEdit.Text = dr["visitor_Allowed_To_Time"].ToString();
                txtAppoinMentFromtimeEdit.Text = dr["Visitor_Allowed_From_time"].ToString();
                txtAdditionalInformationEdit.Text = dr["additional_Info"].ToString();
                txtDesignationEdit.Text = dr["Designation"].ToString();
                //yet to be add
                txtPurposeEdit.Text = dr["PurposeOfVisit"].ToString();
                txt_remarks.Text = dr["Approver_Remarks"].ToString();

                if (Convert.ToString(dr["visitor_nationality"]).ToLower() == "indian")
                {
                    drpnationalityedit.SelectedValue = "Indian";
                }
                else
                {
                    drpnationalityedit.SelectedValue = "Foreigner";
                }


            }
            dr.Close();

            //////to chek existing chekboxes---start

            for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
            {
                CheckBox chk = gvControllerEdit.Rows[i].FindControl("chkControllerEdit") as CheckBox;
                chk.Checked = false;
            }

            string cntrlID;
            string SelectAssignControllers = "select alid from Visitor_Visitor_Access where RequestId='" + vehicleid + "'";
            SqlCommand cmdChekExistsEntries = new SqlCommand(SelectAssignControllers, conn);
            SqlDataReader Reader = cmdChekExistsEntries.ExecuteReader();
            while (Reader.Read())
            {
                string ExistsControllerID = Reader["alid"].ToString();
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


        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {

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
                // mpeDelVehicle.Hide();
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
        }
        public void bindControllerGvAdd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "SELECT * FROM ACS_ACCESSLEVEL where AL_ISDELETED=0 AND AL_ID <> 255";

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
                string str = "SELECT * FROM ACS_ACCESSLEVEL where  AL_ISDELETED=0 AND AL_ID <> 255";

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

        protected void btnokHistory_Click(object sender, EventArgs e)
        {
            // mpeHistory.Hide();
        }

        protected void btnokVehData_Click(object sender, EventArgs e)
        {
            //  mpeVehData.Hide();
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

        public static void WriteAttachment(string FileName, string FileType, string content)
        {
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.ContentType = FileType;
            Response.Write(content);
            Response.End();
        }

        protected void Btnclear1_Click(object sender, EventArgs e)
        {
            ViewState["VisitorID"] = null;
            mpeAddEmployee.Hide();
           // fillVisitor();

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string visitor = ddelSerchVisitor.SelectedValue;
                if (visitor != "Select")
                {


                    if (visitor != "N")
                    {
                        try
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }


                            SqlCommand cmd = new SqlCommand("Visitor_sp_VisitorDetails", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@visitor_id", visitor);
                            DataTable dt = new DataTable();
                            SqlDataAdapter dap = new SqlDataAdapter(cmd);
                            dap.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                txtVisitorName.Text = dt.Rows[0]["Visitor_Name"].ToString();
                                //txtVisitorName.Enabled = false;
                                txtComapany.Text = dt.Rows[0]["VisitorCompany"].ToString();
                                txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
                                if (dt.Rows[0]["mobileNo"].ToString().IndexOf("~") > 0)
                                {
                                    string[] s = dt.Rows[0]["mobileNo"].ToString().Split('~');
                                    txtMobilleNo.Text = s[1].ToString();
                                    txtmob.Text = s[0].ToString();
                                }
                                else
                                {
                                    txtMobilleNo.Text = dt.Rows[0]["mobileNo"].ToString();
                                }
                                txtMiddleName.Text = dt.Rows[0]["Visitor_MiddleName"].ToString();
                                txtLastName.Text = dt.Rows[0]["Visitor_LastName"].ToString();
                                drpnationality.SelectedValue = dt.Rows[0]["VisitorNationality"].ToString();

                            }
                            ViewState["VisitorID"] = visitor;
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
                    //fillVisitor();
                    mpeAddEmployee.Show();
                    txtVisitorName.Enabled = false;
                    txtMiddleName.Enabled = false;
                    txtLastName.Enabled = false;
                    txtComapany.Enabled = false;
                    //txtmob.Enabled = false;
                    //txtMobilleNo.Enabled = false;
                    //txtDesignation.Enabled = false;
                }
                else
                {
                    txtAdditionalInfoAdd.Text = "";
                    txtPurposeAdd.Text = "";
                    txtAppoinmentFromDate.Text = "";
                    txtAppoinmentToDate.Text = "";
                    txtVisitorName.Text = "";

                    txtMiddleName.Text = "";
                    txtLastName.Text = "";
                    ddlApprovalAuthority.SelectedIndex = 0;
                    ddl_location.SelectedIndex = 0;
                    txtComapany.Text = "";
                    txtDesignation.Text = "";
                    txtmob.Text = "";
                    txtMobilleNo.Text = "";
                    drpnationality.SelectedValue = "0";
                    ddlNatureOfVisit.SelectedValue = "0";
                    mpeAddEmployee.Show();

                }
            }
            catch (Exception ex1)
            {
                UNOException.UNO_DBErrorLog(ex1.Message, ex1.StackTrace, "VehicleErollment");
            }


            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "Script", "validateChosen();", true);
        }

        protected void btnflush_Click(object sender, EventArgs e)
        {
            ViewState["VisitorID"] = null;
            //fillVisitor();
            ModalPopupExtender1.Hide();
        }


        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (ddlnatureOfworkEdit.SelectedIndex == -1 || ddlnatureOfworkEdit.SelectedValue == "0")
                {
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Nature of Visit is a Mandatory Field.');", true);
                    lblerrlMsg.Text = "Nature of Visit is a Mandatory Field.";
                    lblerrlMsg.Visible = true;
                    ModalPopupExtender1.Show();
                    return;

                }
                if (ddApprovalEdit.SelectedIndex == 0)
                {
                    lblerrlMsg.Text = "Please select Approval Authority";
                    lblerrlMsg.Visible = true;
                    ModalPopupExtender1.Show();
                    return;
                }
                if (ddl_location_edit.SelectedIndex == 0)
                {
                    lblerrlMsg.Text = "Please Select Location";
                    lblerrlMsg.Visible = true;
                    ModalPopupExtender1.Show();
                    return;
                }

                if (drpnationalityedit.SelectedIndex == 0)
                {
                    lblerrlMsg.Text = "Please select Nationality";
                    lblerrlMsg.Visible = true;
                    ModalPopupExtender1.Show();
                    return;
                }
                string rowid = "";


                rowid = ViewState["RowID"].ToString();
                int count = 0;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd1 = new SqlCommand("Visitor_sp_UpdateRequest", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@visitorid", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@Requestid", SqlDbType.VarChar).Value = rowid;

                cmd1.Parameters.Add("@fromTime", SqlDbType.DateTime).Value = "01/01/1900 " + txtAppoinMentFromtimeEdit.Text + ":00";
                //cmd1.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime).Value = System.DateTime.Now.ToString("MM/dd/yyyy");

                cmd1.Parameters.Add("@toTime", SqlDbType.DateTime).Value = "01/01/1900 " + txtAppointmentToTimeEdit.Text + ":00";



                cmd1.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtAppoinmentFromDateEdit.Text, "dd/MM/yyyy", null);
                cmd1.Parameters.Add("@toDate", SqlDbType.DateTime).Value = DateTime.ParseExact(txtAppoinmentToDateEdit.Text, "dd/MM/yyyy", null);//.ToString("MM/dd/yyyy");

                cmd1.Parameters.Add("@NatureOfWork", SqlDbType.VarChar).Value = ddlnatureOfworkEdit.SelectedValue;
                ;
                cmd1.Parameters.Add("@additional_info", SqlDbType.VarChar).Value = txtAdditionalInformationEdit.Text;
                cmd1.Parameters.Add("@visitorName", SqlDbType.VarChar).Value = txtVisitornameEdit.Text;

                cmd1.Parameters.Add("@visitorCompany", SqlDbType.VarChar).Value = txtCompanyNameEdit.Text;
                cmd1.Parameters.Add("@mobileNo", SqlDbType.VarChar).Value = txtmobedit.Text + "~" + txtMobileNoEdit.Text;
                cmd1.Parameters.Add("@approvalAuthority", SqlDbType.VarChar).Value = ddApprovalEdit.SelectedValue.ToString();
                cmd1.Parameters.Add("@Location", SqlDbType.VarChar).Value = ddl_location_edit.SelectedValue.ToString();
                cmd1.Parameters.Add("@Designation", SqlDbType.VarChar).Value = txtDesignationEdit.Text;
                ////yet to be add
                cmd1.Parameters.Add("@PurPoseOfVisit", SqlDbType.VarChar).Value = txtPurposeEdit.Text;

                cmd1.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = txtMiddleNameEdit.Text;
                cmd1.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtlastNameEdit.Text;
                cmd1.Parameters.Add("@nationality", SqlDbType.VarChar).Value = drpnationalityedit.SelectedValue;
                cmd1.Parameters.AddWithValue("@URL", path);
                cmd1.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd1.Parameters.AddWithValue("@LOGINDEX", 0).Direction = ParameterDirection.Output;

                cmd1.ExecuteNonQuery();

                Int32 LogIndex = Convert.ToInt32(cmd1.Parameters["@LOGINDEX"].Value.ToString());
                binddata();
                lblMsg.Text = "Record Updated Successfully";
                // ModalPopupExtender1.Hide();

                //------------------------
                //string vehicleID = cmd1.Parameters["@vehicleid"].Value.ToString();
                //string RFID = txtRFID.Text;


                string delEsxisting = "delete  from Visitor_Visitor_Access where RequestId='" + rowid + "'";
                UNO.DataTansactionLog.InsertLog("RequestId=" + rowid, "", "Delete", "Visitor_Visitor_Access", path, LogIndex.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand delCmd = new SqlCommand(delEsxisting, conn);
                delCmd.ExecuteNonQuery();
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("<RFIDInventory>");

                for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
                {
                    CheckBox chkAdd = gvControllerEdit.Rows[i].FindControl("chkControllerEdit") as CheckBox;
                    string alid = gvControllerEdit.Rows[i].Cells[1].Text;

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string str = "SELECT * FROM ACS_ACCESSLEVEL_RELATION WHERE AL_ID='" + alid + "' AND ALR_ISDELETED=0";
                    SqlCommand cmd = new SqlCommand(str, conn);
                    SqlDataAdapter acsdap = new SqlDataAdapter(cmd);
                    DataTable acsdt = new DataTable();
                    acsdap.Fill(acsdt);
                    for (int j = 0; j < acsdt.Rows.Count; j++)
                    {
                        string controllerID, ReaderId;
                        controllerID = acsdt.Rows[j]["CONTROLLER_ID"].ToString();
                        ReaderId = acsdt.Rows[j]["RD_ZN_ID"].ToString();

                        if (chkAdd.Checked == true)
                        {
                            count += 1;
                            sb.Append("<Transaction>");
                            sb.Append("<RequestId>" + rowid + "</RequestId>");
                            sb.Append("<Controller_ID>" + controllerID + "</Controller_ID>");
                            sb.Append("<Reader_ID>" + ReaderId + "</Reader_ID>");
                            sb.Append("<alId>" + alid + "</alId>");
                            sb.Append("</Transaction>");

                        }
                    }

                }
                sb.Append("</RFIDInventory>");
                string s = sb.ToString();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd2 = new SqlCommand("Visitor_spAssignControllerInsert", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = sb.ToString();
                cmd2.ExecuteNonQuery();
                if (count > 0)
                    UNO.DataTansactionLog.InsertLog(sb.ToString(), "", "INSERT", "Visitor_Visitor_Access", path, LogIndex.ToString());

                binddata();
                lblMsg.Text = "Record Updated Successfully";

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ModalPopupExtender1.Hide();

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

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DeleteRequest();
        }

        protected void btn_del_submit_Click1(object sender, EventArgs e)
        {
        }

        private void DeleteRequest()
        {
            try
            {
                bool check = false;
                for (int i = 0; i < GvVisitor.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GvVisitor.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked == true)
                    {
                        check = true;
                        try
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            string code = GvVisitor.Rows[i].Cells[2].Text;
                            SqlCommand cmd = new SqlCommand("Visitor_spDeleteVisitorRequest", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@requestID", code);
                            cmd.Parameters.AddWithValue("@URL", path);
                            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
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
                            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
                        }
                    }
                }
                if (check == false)
                {
                    lblerrlMsg.Text = "Please select record to delete.";
                    lblerrlMsg.Visible = true;
                }
                binddata();
                lblMsg.Text = "Records Deleted Successfully.";
                lblMsg.Visible = true;
                //mpeDelVehicle.Hide();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "Visitor");
            }
        }

        protected void GvVisitor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //  string connection = conn.ConnectionString;
                LinkButton lnkScanDocumnent = (LinkButton)e.Row.FindControl("lnkScanDocumnent");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[12].Text == "Approved")
                    {
                        CheckBox chek = (CheckBox)e.Row.FindControl("DeleteRows");
                        chek.Enabled = false;
                        LinkButton lnk = (LinkButton)e.Row.FindControl("lnkModify");
                        lnk.Enabled = true;

                        lnkScanDocumnent.Enabled = false;

                    }

                    string connection = "\"" + ConfigurationManager.ConnectionStrings["connection_string"].ToString() + "\"";
                    //lnkScanDocumnent.Attributes.Add("onclick", "return CallAutoScan('" + e.Row.Cells[0].Text + "','" + connection + "','" + lnkScanDocumnent.CommandArgument + "');");

                    int document = GetVistorDocumentCount(lnkScanDocumnent.CommandArgument.ToString());

                    if (document == 0)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    }

                    if (CallManagementHandler.GetLevelCode(Convert.ToInt16(Session["levelId"])).ToLower() == "admin")
                    {
                        ((LinkButton)e.Row.FindControl("lnkModify")).Enabled = true;
                        ((LinkButton)e.Row.FindControl("lnkModify")).Text = "View";
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnReset1_Click(object sender, EventArgs e)
        {
            txtVisitorName.Enabled = true;
            txtMiddleName.Enabled = true;
            txtLastName.Enabled = true;
            txtComapany.Enabled = true;
            //txtmob.Enabled = true;
            //txtMobilleNo.Enabled = true;
            //txtDesignation.Enabled = true;

            txtAdditionalInfoAdd.Text = "";
            txtPurposeAdd.Text = "";
            txtVisitorName.Text = "";
            txtVisitorName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            ddlApprovalAuthority.SelectedIndex = 0;
            ddl_location.SelectedIndex = 0;
            txtComapany.Text = "";
            txtDesignation.Text = "";
            txtmob.Text = "";
            txtMobilleNo.Text = "";
            drpnationality.SelectedValue = "0";
            ddlNatureOfVisit.SelectedValue = "0";
            ViewState["VisitorID"] = null;

            txtAppoinmentToTime.Text = "";
            txtAppoinmentFromTime.Text = "";


            DateTime today = DateTime.Today;

            int daysUntilTuesday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextsaturday = today.AddDays(daysUntilTuesday);

            txtAppoinmentFromDate.Text = today.ToString("dd/MM/yyyy");
            txtAppoinmentToDate.Text = today.ToString("dd/MM/yyyy");

            //fillVisitor();

            mpeAddEmployee.Show();
            ddelSerchVisitor.SelectedIndex = 0;

            ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "Script", "validateChosen();", true);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void gvControllerAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvControllerEdit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["uid"].ToString().ToLower() == "admin")
                {
                    e.Row.Enabled = false;
                }
            }
        }

        public int GetVistorDocumentCount(string requestID)
        {
            int count = 0;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("VisitorDocumentUploadChekForForeginoer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@requestID", requestID);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    count = Convert.ToInt32(dt.Rows[0]["DocumentCount"].ToString());

                }
                else
                {
                    count = -1;
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            {

            }
            return count;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            mpeAddEmployee.Hide();
        }

        private int DocumentValidation()
        {
            int docCount = 0;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_VISITOR_UPLOAD_DOCUMENT", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "GetTotalCount_Document";
            //cmd.Parameters.Add("@VisitorId", SqlDbType.NVarChar).Value = EmpCode;

            //cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "GetTotalCount";
            cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "TotalDocument";
            cmd.Parameters.Add("@VisitorId", SqlDbType.NVarChar).Value = EmpCode;
            docCount = Convert.ToInt32(cmd.ExecuteScalar());
            UNOException.WriteLog("------Document Validation after USP_VISITOR_UPLOAD_DOCUMENT procedure-------");
            UNOException.WriteLog("EmpCode: "+EmpCode);
            UNOException.WriteLog("------Document Count after USP_VISITOR_UPLOAD_DOCUMENT procedure-------");
            UNOException.WriteLog("docCount: "+docCount.ToString());
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return docCount;
        }

        private int GetFileCount()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_VISITOR_UPLOAD_DOCUMENT", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "GetCount";
            //cmd.Parameters.Add("@VisitorId", SqlDbType.NVarChar).Value = EmpCode;

            cmd.Parameters.Add("@strCommand", SqlDbType.NVarChar).Value = "GetCount";
            cmd.Parameters.Add("@VisitorId", SqlDbType.NVarChar).Value = EmpCode;
            ImageCount = Convert.ToInt32(cmd.ExecuteScalar())+1;
            UNOException.WriteLog("------Image Count in GetFileCount------");
            UNOException.WriteLog("Image Count"+ImageCount.ToString());
            
         
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ImageCount;
           

        }

        public void uploaddata(string filepath, Stream fs)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    UNOException.WriteLog("------byte in uploaddata()------");
                    UNOException.WriteLog("Byte : " + bytes.ToString());
                    Image64 = Convert.ToBase64String(bytes);
                    UNOException.WriteLog("Image64 : "+Image64);

                    string filename = Path.GetFileName(filepath);
                    string ext = Path.GetExtension(filename);
                    UNOException.WriteLog("------filename & ext in uploaddata-----");
                    UNOException.WriteLog("filename : "+filename);
                    UNOException.WriteLog("ext : "+ext);
                    string contenttype = String.Empty;

                    EmpCode = ViewState["Rq_id"].ToString();
                    UNOException.WriteLog("EmpCode "+EmpCode);
                    DateTime dt = DateTime.Now;
                    GetFileCount();
                    if (DocumentValidation() < 10)
                    {
                        UNOException.WriteLog("-------Inside uploaddata() if condition block-------");
                        string FileName = EmpCode + "Doc" + ImageCount.ToString() + "_" + dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + ".jpg";
                        UNOException.WriteLog("filename : " + FileName);
                        string FinalUrl = Url + "?FName=" + FileName + "&Count=" + ImageCount + "&rId=" + EmpCode + "&VId=";
                        UNOException.WriteLog("FinalUrl : " + FinalUrl);
                        string result = client.UploadString(FinalUrl, "POST", Image64);
                        UNOException.WriteLog("result : " + result);
                        //////
                        //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        //Im.ImageUrl = "data:image/png;base64," + base64String;
                        //Im.Visible = true;
                        ModelPopupFor_Document.Show();
                        binddocummentdata();
                    }
                    else
                    {
                        lbl_document.Text = "You can't upload more then 10 documents.";
                        ModelPopupFor_Document.Show();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

        public void binddocummentdata()
        {
            try
            {
                string rqid=ViewState["Rq_id"].ToString();
                UNOException.WriteLog("-----request Id inside binddocummentdata-----");
                UNOException.WriteLog("rqid : "+ rqid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_fill_visitor_document", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Request_Id", rqid);
                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                //DataColumn dtCol = new DataColumn("FileName");
                //dt.Columns.Add(dtCol);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["FilePath"] = Path.GetFileName(dt.Rows[i][0].ToString());
                   
                }
                //dt.Columns.Remove("FilePath");

                GV_Documment.DataSource = dt;
                GV_Documment.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GV_Documment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GV_Documment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                //string request_id = e.CommandArgument.ToString();
                //ViewState["Rq_id"] = request_id;

                string url_name = e.CommandArgument.ToString();

                RemoveImage(url_name);
                binddocummentdata();
                ModelPopupFor_Document.Show();
            }
            if (e.CommandName == "View")
            {
                UNOException.WriteLog("-----inside GV_Documment_RowCommand ecommandname View-----");
                string imgName = e.CommandArgument.ToString();
                string imgPath = imgName;
                UNOException.WriteLog("Image name : "+imgName);
                //Im.ImageUrl = @"C:\Users\rupali_jadhav\Pictures\doc1.jpg";//imgPath;
                FileStream fsView = new FileStream(imgPath,FileMode.Open);
                BinaryReader br = new BinaryReader(fsView);
                Byte[] bytes = br.ReadBytes((Int32)fsView.Length);
                UNOException.WriteLog("byte : "+bytes);
                Image64 = Convert.ToBase64String(bytes);
                UNOException.WriteLog("Image64 : "+Image64);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                UNOException.WriteLog("base64String : "+ base64String);
                Im.ImageUrl = "data:image/png;base64," + base64String;
                UNOException.WriteLog("Im.ImageUrl : " + Im.ImageUrl);
                Im.Visible = true;
                modalPnlViewImage.Show();
             }

        }

        protected void UploadButton1_Click(object sender, EventArgs e)
        {
            lbl_Message.Text = "";
            string filepath = FileUpload1.PostedFile.FileName;
            Stream fs = FileUpload1.PostedFile.InputStream;
            string ext = Path.GetExtension(filepath);
            UNOException.WriteLog("------Inside UploadButton1_Click----");
            UNOException.WriteLog("filepath : "+filepath);
            UNOException.WriteLog("extention : "+ext);


            if (ext.ToString().Trim().ToLower() == ".jpg" || ext.ToString().Trim().ToLower() == ".png")
            {
                uploaddata(filepath, fs);
            }
            else if (FileUpload1.FileName.ToString() == "")
            {
                lbl_Message.Text = "Please select file";
                ModelPopupFor_Document.Show();
            }
            else
            {
                lbl_Message.Text = "Please select .jpg or .png format file";
                ModelPopupFor_Document.Show();

            }
        }

      

        public void RemoveImage(string ImageName)
        {
            using (WebClient client = new WebClient())
            {
                EmpCode = ViewState["Rq_id"].ToString();
                string FinalUrl = Url + "?FName=" + ImageName + "&Count=" + 0 + "&rId=" + EmpCode + "&VId=";
                string result = client.UploadString(FinalUrl, "POST");
                UNOException.WriteLog("----Inside RemoveImage----");
                UNOException.WriteLog("FinalUrl : " + FinalUrl);
                UNOException.WriteLog("result : " + result);
            }
 
        }

        protected void Btn_Cancel_Doc_Click(object sender, EventArgs e)
        {
            ModelPopupFor_Document.Hide();
        }

        protected void btnCloseImage_Click(object sender, EventArgs e)
        {
            modalPnlViewImage.Hide();
            ModelPopupFor_Document.Show();
        }


        //protected void btnReset1_Click1(object sender, EventArgs e)
        //{
        //    txtAdditionalInfoAdd.Text = "";
        //    txtPurposeAdd.Text = "";
        //    txtVisitorName.Enabled = true;
        //    txtVisitorName.Text = "";
        //    txtVisitorName.Text = "";
        //    txtMiddleName.Text = "";
        //    txtLastName.Text = "";
        //    ddlApprovalAuthority.SelectedIndex = 0;
        //    txtComapany.Text = "";
        //    txtDesignation.Text = "";
        //    txtmob.Text = "";
        //    txtMobilleNo.Text = "";
        //    drpnationality.SelectedValue = "0";
        //    ddlNatureOfVisit.SelectedValue = "0";


        //    txtAppoinmentToTime.Text = "";
        //    txtAppoinmentFromTime.Text = "";


        //    DateTime today = DateTime.Today;

        //    int daysUntilTuesday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
        //    DateTime nextsaturday = today.AddDays(daysUntilTuesday);

        //    txtAppoinmentFromDate.Text = today.ToString("dd/MM/yyyy");
        //    txtAppoinmentToDate.Text = today.ToString("dd/MM/yyyy");

        //    fillVisitor();

        //    mpeAddEmployee.Show();
        //    ddelSerchVisitor.SelectedIndex = 0;

        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel7, UpdatePanel7.GetType(), "Script", "validateChosen();", true);

        //}
    }
}