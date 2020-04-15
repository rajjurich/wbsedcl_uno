using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Text;
using System.IO;

namespace UNO
{
    public partial class Visitor_VMSApproveRequest : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        static string path = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];

        clsEmailTemplate clsTemp = new clsEmailTemplate();
      string Image64 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatus.SelectedValue = "N";
                bindGrid();
              //  bindControllerGvEdit();
                GetReqCount();
            
            }
        }

        public void GetReqCount()
        { 
             try
                {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Visitor_sp_RequestCount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Aprroverid", Session["uid"].ToString());
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dap.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblPending.Text = dt.Rows[0]["Pending"].ToString();
                    lblApprove.Text = dt.Rows[0]["Approve"].ToString();
                    lblrejected.Text = dt.Rows[0]["Reject"].ToString();
                    lbltotalreq.Text = dt.Rows[0]["TotalRequest"].ToString();



                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
          
            }

            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        public void bindGrid()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PROC_VMS_REQUEST_APPROVAL";

                cmd.Parameters.AddWithValue("@cmd", 0);
                cmd.Parameters.AddWithValue("@authorityid", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.CommandTimeout = 100;
                da.Fill(dt);
                gvVisitorApproval.DataSource = dt;
                gvVisitorApproval.DataBind();
                if (dt.Rows.Count > 0)
                {
              

                    DropDownList ddl = (DropDownList)gvVisitorApproval.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvVisitorApproval.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvVisitorApproval.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvVisitorApproval.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvVisitorApproval.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvVisitorApproval.PageCount == 0)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvVisitorApproval.PageIndex + 1 == gvVisitorApproval.PageCount)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvVisitorApproval.PageIndex == 0)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1));

                    gvVisitorApproval.BottomPagerRow.Visible = true;


                }
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            { 
            
            }

        }
        public void fillNatureOfVisit()
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from ent_params where ", con);
          
                cmd.ExecuteNonQuery();
                bindGrid();
                mpeAddEmployee.Hide();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PROC_VMS_REQUEST_APPROVAL";

                cmd.Parameters.AddWithValue("@cmd", 0);
                cmd.Parameters.AddWithValue("@authorityid", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue );

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.CommandTimeout = 100;
                da.Fill(dt);
                gvVisitorApproval.DataSource = dt;
                gvVisitorApproval.DataBind();
                lblMsg.Text = "";
                if (dt.Rows.Count > 0)
                {


                    DropDownList ddl = (DropDownList)gvVisitorApproval.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvVisitorApproval.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvVisitorApproval.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvVisitorApproval.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvVisitorApproval.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvVisitorApproval.PageCount == 0)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvVisitorApproval.PageIndex + 1 == gvVisitorApproval.PageCount)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvVisitorApproval.PageIndex == 0)
                    {
                        ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1));

                    gvVisitorApproval.BottomPagerRow.Visible = true;


                }
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_VMS_REQUEST_APPROVAL", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmd", 0);
                cmd.Parameters.AddWithValue("@authorityid", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (txtVisitorName.Text.ToString() == "")
                {
                    gvVisitorApproval.DataSource = dt;
                    gvVisitorApproval.DataBind();
                }
                else
                {
                    String[,] values = { 
                
                {"VisitorName~" +txtVisitorName.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvVisitorApproval.DataSource = _tempDT;
                    gvVisitorApproval.DataBind();
                }
                DropDownList ddl = (DropDownList)gvVisitorApproval.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvVisitorApproval.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvVisitorApproval.PageIndex + 1).ToString();
                Label lblcount = (Label)gvVisitorApproval.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvVisitorApproval.DataSource).Rows.Count.ToString() + " Records.";
                if (gvVisitorApproval.PageCount == 0)
                {
                    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVisitorApproval.PageIndex + 1 == gvVisitorApproval.PageCount)
                {
                    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvVisitorApproval.PageIndex == 0)
                {
                    ((Button)gvVisitorApproval.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1));

                ((Label)gvVisitorApproval.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvVisitorApproval.PageSize * gvVisitorApproval.PageIndex) + 1) + " to " + (((gvVisitorApproval.PageSize * (gvVisitorApproval.PageIndex + 1)) - 10) + gvVisitorApproval.Rows.Count);

                gvVisitorApproval.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }

        }


        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvVisitorApproval.PageIndex = Convert.ToInt32(((DropDownList)gvVisitorApproval.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                btnSearch_Click(sender, e);
                //bindGrid();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvVisitorApproval.PageIndex = gvVisitorApproval.PageIndex - 1;
                btnSearch_Click(sender, e);
                //bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_Sanc_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvVisitorApproval.PageIndex = gvVisitorApproval.PageIndex + 1;
                btnSearch_Click(sender, e);
                //bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_Sanc_View");
            }
        }


        public void bindControllerGvEdit()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string str = "SELECT * FROM ACS_ACCESSLEVEL where AL_ISDELETED=0";

                SqlCommand cmd = new SqlCommand(str, con);


                DataTable dt = new DataTable();
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                gvControllerEdit.DataSource = dt;
                gvControllerEdit.DataBind();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }
        protected void gvVisitorApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                try
                {
                    string requestId = e.CommandArgument.ToString();
                  
                   // GridViewRow row = gvVisitorApproval.Rows[index];
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    int RowIndex = gvr.RowIndex;
                    string status = gvVisitorApproval.Rows[RowIndex].Cells[6].Text;
                    if (status.ToLower() == "rejected")
                    {
                        btnReject.Enabled = false;
                    }
                    else
                    {
                        btnReject.Enabled = true;
                    }
                    ViewState["requestId"] = requestId;
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Visitor_GetApprovalRecord";

                    cmd.Parameters.AddWithValue("@requestId", requestId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.CommandTimeout = 100;
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblVisitorName.Text = dt.Rows[0]["Visitor_Name"].ToString();

                        lblCompanyName.Text = dt.Rows[0]["VisitorCompany"].ToString();
                        lblDesignation.Text = dt.Rows[0]["Designation"].ToString();

                        lblMobileNo.Text = dt.Rows[0]["mobileNo"].ToString();
                        lblNaturOfWork.Text = dt.Rows[0]["nature_of_work"].ToString();

                        lblfromDate.Text = dt.Rows[0]["appointment_from_date"].ToString();
                        lblToDate.Text = dt.Rows[0]["appointment_to_date"].ToString();
                        lblFromTime.Text = dt.Rows[0]["Visitor_Allowed_From_time"].ToString();
                        lblToTime.Text = dt.Rows[0]["visitor_Allowed_To_Time"].ToString();
                        txtAdditionalInfoAdd.Text = dt.Rows[0]["Approver_Remarks"].ToString();
                        lbl_Nationality.Text = dt.Rows[0]["visitor_nationality"].ToString();
                        lblPurposeOfVisit.Text = dt.Rows[0]["PurposeOfVisit"].ToString();

                        lblRemarks.Text = dt.Rows[0]["additional_Info"].ToString();
                        hdnIsblacklisted.Value = dt.Rows[0]["IsBlackllisted"].ToString();


                        if (Convert.ToString(dt.Rows[0]["approval_status"])=="A")
                        {
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                        }
                        //////to chek existing chekboxes---start
                        string cntrlID;
                        string SelectAssignControllers = "select alid from Visitor_Visitor_Access where RequestId='" + requestId + "'";
                       
                        SqlCommand cmd1 = new SqlCommand("Visitor_getAccesLevel", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@RequestId", requestId);

                        DataTable dt1 = new DataTable();
                        SqlDataAdapter dap = new SqlDataAdapter(cmd1);
                        dap.Fill(dt1);
                        gvControllerEdit.DataSource = dt1;
                        gvControllerEdit.DataBind();


                        SqlCommand cmdChekExistsEntries = new SqlCommand(SelectAssignControllers, con);
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
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        //lblMsg.Text = "";

                  
                    }
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                catch (Exception ex)
                {

                }
                mpeAddEmployee.Show();
            }

            if (e.CommandName == "View_Document")
            {
                string Id = e.CommandArgument.ToString();
                UNOException.WriteLog("Id in View_document: " + Id);
                fill_documents(Id);
                ModelPopupFor_Document.Show();
                //ModelPopupFor_Document_View.Show();
            }
        }

        private void fill_documents(string Id)
        {
            try
            {
                GV_Documment.DataSource = null;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_fill_visitor_document", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Request_Id", Id);
                UNOException.WriteLog("Id in fill_documents() : " + Id);
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dap.Fill(dt);

              //  if (dt.Rows.Count > 0)
               // {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       dt.Rows[i]["FilePath"] = Path.GetFileName(dt.Rows[i][0].ToString());
                   
                    }
               // }
                GV_Documment.DataSource = dt;
                GV_Documment.DataBind();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;

            }
        }

        protected void Btnclear1_Click(object sender, EventArgs e)
        {
            txtAdditionalInfoAdd.Text = "";
            mpeAddEmployee.Hide();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string requestid=ViewState["requestId"].ToString();
                SqlCommand cmd = new SqlCommand("Vsisitor_ApproVeRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", requestid);
                cmd.Parameters.AddWithValue("@approverRemarks", txtAdditionalInfoAdd.Text);
                cmd.Parameters.AddWithValue("@status","A");
                cmd.Parameters.AddWithValue("@URL", path);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.ExecuteNonQuery();

                ///mail 
                ///
                string cc = "";
                string subject = "Visitor Request";
                string UserMailId = "", managerMail_id = "";
                string visitorname = lblVisitorName.Text;



                SqlCommand cmdManager = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" + Session["uid"].ToString() + "')", con);
                SqlDataAdapter dap = new SqlDataAdapter(cmdManager);
                DataTable dtManager = new DataTable();
                dap.Fill(dtManager);

                ////////////

                string strempMailid = "select CreatedBy from Visitor_Appointment_Request where RequestID='" + requestid + "'";
                SqlCommand cmdEmployee1 = new SqlCommand(strempMailid, con);
                SqlDataAdapter dap2 = new SqlDataAdapter(cmdEmployee1);
                DataTable dtemploye2 = new DataTable();
                dap2.Fill(dtemploye2);

               string RequestorID=dtemploye2.Rows[0]["CreatedBy"].ToString(); 

                SqlCommand cmdEmployee = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" +RequestorID + "')", con);
                SqlDataAdapter dap1 = new SqlDataAdapter(cmdEmployee);
                DataTable dtemploye = new DataTable();
                dap1.Fill(dtemploye);

                managerMail_id = dtManager.Rows[0]["EPD_EMAIL"].ToString();
                UserMailId = dtemploye.Rows[0]["EPD_EMAIL"].ToString();


                //-----------------------------------

                DataTable infoDT = new DataTable();
                SqlCommand cmdInfo = new SqlCommand();
                cmdInfo.CommandType = CommandType.StoredProcedure;
                cmdInfo.CommandText = "Proc_GetVisitorInfoForMail";
                cmdInfo.Connection = con;
                cmdInfo.Parameters.AddWithValue("@userid", RequestorID);
                cmdInfo.Parameters.AddWithValue("@managerid", Session["uid"].ToString());//ddlApprovalAuthority.SelectedValue
                cmdInfo.Parameters.AddWithValue("@RequestID", requestid); //RequestID

                StringBuilder sbaccessdetails = new StringBuilder();
                string accessdetails = "";
                if (gvControllerEdit.Rows.Count > 0)
                {
                    for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
                    {
                        
                            sbaccessdetails.Append(gvControllerEdit.Rows[i].Cells[1].Text + " - " + gvControllerEdit.Rows[i].Cells[2].Text + ", ");
                        
                    }
                    if (sbaccessdetails != null)
                    {
                        accessdetails = sbaccessdetails.ToString();
                    }
                }


                SqlDataAdapter dap3 = new SqlDataAdapter(cmdInfo);
                dap3.Fill(infoDT);

               // managerMail_id = infoDT.Rows[0]["manageremailid"].ToString();
               // managerMail_id = "supportacs@sac.isro.gov.in";
                UserMailId = infoDT.Rows[0]["useremailid"].ToString(); // To Emp
                managerMail_id = clsTemp.fromMail(); // From Manger

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

                string message = "Your following visitor request has been approved." + Environment.NewLine + Environment.NewLine;
                message = message + "Please See Details of Approved Visitor Request given below. " + Environment.NewLine + Environment.NewLine + "Visitor id: " + Visitorid + Environment.NewLine;
                message = message + "Visitor name: " + Visitorname + Environment.NewLine;
                message = message + "Company name: " + Companyname + Environment.NewLine + "Mobile No: " + MobileNo + Environment.NewLine + "Nationality: " + @Nationality + Environment.NewLine + "Nature of Work: " + NatureofWork + Environment.NewLine + "Appointment from Date/time: " + AppointmentfromDatetime + Environment.NewLine;
                message = message + "Appointment to Date/time: " + AppointmenttoDatetime + Environment.NewLine + "Purpose of Visit: " + PurposeofVisit + Environment.NewLine + "Visitor Access Detail: " + accessdetails + Environment.NewLine + "Additional info:- " + Additionalinfo + Environment.NewLine + Environment.NewLine;
                message = message + "Click here to view status of request: " + Environment.NewLine + "http://10.61.7.204/uno/home.aspx. " + Environment.NewLine + Environment.NewLine + "If you find any difficulty, contact ACS Control room, Ext. :3004/3016." + Environment.NewLine + Environment.NewLine;
                message = message + "Regards, " + Environment.NewLine + Environment.NewLine + "ACS Team, SAC ";

                //subject = "Your Visitor Request for " + Visitorname + " is Approved.";  
                //Thread thread = new Thread(() => Mail.SendMail(managerMail_id,UserMailId,cc, subject, message));
                //thread.Start();

                string datefield = DateTime.Now.ToString();
                clsTemp.sendMail(managerMail_id, UserMailId, subject, message, datefield);

                //mail end


                bindGrid();
                GetReqCount();
                mpeAddEmployee.Hide();
                lblMsg.Text = "";
                lblMsg.Visible = true;
                lblMsg.Text = "Record Approved Successfully";

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }



        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string requestid = ViewState["requestId"].ToString();
                SqlCommand cmd = new SqlCommand("Vsisitor_ApproVeRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", requestid);
                cmd.Parameters.AddWithValue("@approverRemarks", txtAdditionalInfoAdd.Text);
                cmd.Parameters.AddWithValue("@status", "R");
                cmd.Parameters.AddWithValue("@URL", path);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.ExecuteNonQuery();
                bindGrid();
                GetReqCount();

                ///mail 
                ///
                string cc = "";
                string subject = "Visitor Request";
                string UserMailId = "", managerMail_id = "";

                string visitorname = lblVisitorName.Text;

                SqlCommand cmdManager = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" + Session["uid"].ToString() + "')", con);
                SqlDataAdapter dap = new SqlDataAdapter(cmdManager);
                DataTable dtManager = new DataTable();
                dap.Fill(dtManager);

                ////////////

                string strempMailid = "select CreatedBy from Visitor_Appointment_Request where RequestID='" + requestid + "'";
                SqlCommand cmdEmployee1 = new SqlCommand(strempMailid, con);
                SqlDataAdapter dap2 = new SqlDataAdapter(cmdEmployee1);
                DataTable dtemploye2 = new DataTable();
                dap2.Fill(dtemploye2);

                string RequestorID = dtemploye2.Rows[0]["CreatedBy"].ToString();

                SqlCommand cmdEmployee = new SqlCommand("SELECT EPD_EMAIL FROM ENT_EMPLOYEE_PERSONAL_DTLS WHERE EpD_EMPID in('" + RequestorID + "')", con);
                SqlDataAdapter dap1 = new SqlDataAdapter(cmdEmployee);
                DataTable dtemploye = new DataTable();
                dap1.Fill(dtemploye);

                managerMail_id = dtManager.Rows[0]["EPD_EMAIL"].ToString();
                UserMailId = dtemploye.Rows[0]["EPD_EMAIL"].ToString();

                //-----------------------------------

                DataTable infoDT = new DataTable();
                SqlCommand cmdInfo = new SqlCommand();
                cmdInfo.CommandType = CommandType.StoredProcedure;
                cmdInfo.CommandText = "Proc_GetVisitorInfoForMail";
                cmdInfo.Connection = con;
                cmdInfo.Parameters.AddWithValue("@userid", RequestorID);
                cmdInfo.Parameters.AddWithValue("@managerid", Session["uid"].ToString());//ddlApprovalAuthority.SelectedValue
                cmdInfo.Parameters.AddWithValue("@RequestID", requestid); //RequestID

                StringBuilder sbaccessdetails = new StringBuilder();
                string accessdetails = "";
                if (gvControllerEdit.Rows.Count > 0)
                {
                    for (int i = 0; i < gvControllerEdit.Rows.Count; i++)
                    {

                        sbaccessdetails.Append(gvControllerEdit.Rows[i].Cells[1].Text + " - " + gvControllerEdit.Rows[i].Cells[2].Text + ", ");

                    }
                    if (sbaccessdetails != null)
                    {
                        accessdetails = sbaccessdetails.ToString();
                    }
                }


                SqlDataAdapter dap3 = new SqlDataAdapter(cmdInfo);
                dap3.Fill(infoDT);

                managerMail_id = clsTemp.fromMail(); // From Manger
                UserMailId = infoDT.Rows[0]["useremailid"].ToString(); // To Emp
               

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

                string message = "Your following visitor request has been rejected." + Environment.NewLine + Environment.NewLine;
                message = message + "Please See Details of Rejected Visitor Request given below. " + Environment.NewLine + Environment.NewLine + "Visitor id: " + Visitorid + Environment.NewLine;
                message = message + "Visitor name: " + Visitorname + Environment.NewLine;
                message = message + "Company name: " + Companyname + Environment.NewLine + "Mobile No: " + MobileNo + Environment.NewLine + "Nationality: " + @Nationality + Environment.NewLine + "Nature of Work: " + NatureofWork + Environment.NewLine + "Appointment from Date/time: " + AppointmentfromDatetime + Environment.NewLine;
                message = message + "Appointment to Date/time: " + AppointmenttoDatetime + Environment.NewLine + "Purpose of Visit: " + PurposeofVisit + Environment.NewLine + "Visitor Access Detail: " + accessdetails + Environment.NewLine + "Additional info:- " + Additionalinfo + Environment.NewLine + Environment.NewLine;
                message = message + "Click here to view status of request: " + Environment.NewLine + "http://10.61.7.204/uno/home.aspx. " + Environment.NewLine + Environment.NewLine + "If you find any difficulty, contact ACS Control room, Ext. :3004/3016." + Environment.NewLine + Environment.NewLine;
                message = message + "Regards, " + Environment.NewLine + Environment.NewLine + "ACS Team, SAC ";

                subject = "Your Visitor Request for " + Visitorname + " is Rejected.";   
                //Thread thread = new Thread(() => Mail.SendMail(managerMail_id, UserMailId, cc, subject, message));
                //thread.Start();

                string datefield = DateTime.Now.ToString();
                clsTemp.sendMail(managerMail_id, UserMailId, subject, message, datefield);

                //mail end


                bindGrid();
                GetReqCount();
                mpeAddEmployee.Hide();
                lblMsg.Text = "";
                lblMsg.Visible = true;
                lblMsg.Text = "Record Rejected Successfully";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "VehicleErollment");
            }
        }

        protected void gvVisitorApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvVisitorApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[6].Text == "Pending")
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    
                    }
                    //LinkButton lk = (LinkButton)e.Row.FindControl("lblVisitorID");
                    //string Namecolumnvalue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "status"));
                    //if (Namecolumnvalue == "Approved")
                    //{
                    //    lk.Enabled = false;

                    //}

                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void gvControllerEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GV_Documment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GV_Documment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string url_name = e.CommandArgument.ToString();
                string FilePath = url_name;
               string request_id = e.CommandArgument.ToString();
                
                //View_file(url_name,request_id);
               // ModelPopupFor_Document.Hide();
                FileStream fsView = new FileStream(FilePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fsView);
                Byte[] bytes = br.ReadBytes((Int32)fsView.Length);
                Image64 = Convert.ToBase64String(bytes);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                document_Image.ImageUrl = "data:image/png;base64," + base64String;
                document_Image.Visible = true;
             // string FilePath = Server.MapPath("~\\VisitorDocuments\\") + url_name;
              // string FilePath = "~/VisitorDocuments/" + url_name;
                
                ModelPopupFor_Document_View.Show();
                //ModelPopupFor_Document.Hide();
         

            }
        }

        //public void View_file(string ImageName, string Id)
        //{
        //    string FilePath = Server.MapPath("~\\VisitorDocuments\\") + ImageName;
        //    document_Image.ImageUrl = FilePath;
        //}

        protected void Btn_Cancel_Doc_Click(object sender, EventArgs e)
        {
            ModelPopupFor_Document.Hide();
        }

        protected void btn_cancel_view_doc_Click(object sender, EventArgs e)
        {
            ModelPopupFor_Document_View.Hide();
            ModelPopupFor_Document.Show();
        }


    }
}