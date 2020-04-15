using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Management;
using System.Net;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
namespace UNO
{
    public partial class GatepassRegistration : System.Web.UI.Page
    {
        public string ImageUrl;
        string default_path = null;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        static string strpath = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                bindDataGrid();
                default_path = Server.MapPath(@"~/Captures/");
                hdfEmpcode.Value = Convert.ToString(Session["uid"]);
            }

            default_path = Server.MapPath(@"~/Captures/");
            hdConn.Value = ConfigurationManager.ConnectionStrings["connection_string"].ToString().Replace(' ', ',');
            hdnPageName.Value = strpath;
            if (Session["uid"] != null)
            {
                hdnUserId.Value = Session["uid"].ToString();
            }
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


                int _result = 0;

                SqlCommand cmd = new SqlCommand("PROC_VMS_GET_GATEPASSREGISTRATTIONDETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvGatePass.DataSource = dt;
                gvGatePass.DataBind();
                DropDownList ddl = (DropDownList)gvGatePass.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvGatePass.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvGatePass.PageIndex + 1).ToString();
                Label lblcount = (Label)gvGatePass.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvGatePass.DataSource).Rows.Count.ToString() + " Records.";
                if (gvGatePass.PageCount == 0)
                {
                    ((Button)gvGatePass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvGatePass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvGatePass.PageIndex + 1 == gvGatePass.PageCount)
                {
                    ((Button)gvGatePass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvGatePass.PageIndex == 0)
                {
                    ((Button)gvGatePass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }



                ((Label)gvGatePass.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGatePass.PageSize * gvGatePass.PageIndex) + 1) + " to " + (((gvGatePass.PageSize * (gvGatePass.PageIndex + 1)) - 10) + gvGatePass.Rows.Count);

                gvGatePass.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }

        }
        protected void gvGatePass_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvGatePass.PageIndex = e.NewPageIndex;
                //bindDataGrid();

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvGatePass.PageIndex = Convert.ToInt32(((DropDownList)gvGatePass.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvGatePass.PageIndex = gvGatePass.PageIndex - 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }

        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvGatePass.PageIndex = gvGatePass.PageIndex + 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }

        }
        public void FillPersonalDetails(int request)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("PROC_Vistor_GET_GATEPASSREGISTRATTIONDETAILS_BYREQUSETID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", request);
                cmd.ExecuteNonQuery();
                lblRequestID.Text = request.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count > 0)
                {
                    string visitor_id = null;

                    txtVisitorName.Text = dt.Rows[0]["Visitor_Name"].ToString();
                    txtVisitor.Text = dt.Rows[0]["Visitor_ID"].ToString();
                    txtStartDate.Text = dt.Rows[0]["appointment_from_date"].ToString();
                    txtToDate.Text = dt.Rows[0]["appointment_to_date"].ToString();
                    txtStartTime.Text = dt.Rows[0]["Visitor_Allowed_From_time"].ToString();
                    txtToTime.Text = dt.Rows[0]["visitor_Allowed_To_Time"].ToString();
                    txtNationality.Text = dt.Rows[0]["VisitorNationality"].ToString();
                    txtCompany.Text = dt.Rows[0]["VisitorCompany"].ToString();
                    txtCity.Text = dt.Rows[0]["VisitorCity"].ToString();
                    txtPassport.Text = dt.Rows[0]["passport"].ToString();
                    
                   // WtiteLog("Before Visitor Address Assignment" + txtVisitor.Text);
                  //  WtiteLog(" Value:-"+dt.Rows[0]["VisitorCompAddress"].ToString());
                    txtVisitorCompAddress.Text = dt.Rows[0]["VisitorCompAddress"].ToString();
                  //  WtiteLog("After Visitor Address Assignment" + txtVisitor.Text);

                    //(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;

                    hdnFinger1.Value = ByteArrayToString((byte[])dt.Rows[0]["VisitorFingerImageWarWick1"]);
                    hdFinger2.Value = ByteArrayToString((byte[])dt.Rows[0]["VisitorFingerImageWarWick2"]);
                    hdnactivationdate.Value = dt.Rows[0]["ActivationDate"].ToString();
                    hdnexpirydate.Value = dt.Rows[0]["ExpiryDate"].ToString();
                    DisplayEmployeeImage(dt.Rows[0]["Visitor_Photopath"].ToString());

                  //  WtiteLog("Visitor Photopath" + txtVisitor.Text);


                    if (dt.Rows[0]["VisitorSign"].ToString() != string.Empty || dt.Rows[0]["VisitorSign"].ToString() != "")
                    {
                        byte[] bytes = (byte[])dt.Rows[0]["VisitorSign"];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ImageSignPerso.ImageUrl = "";
                        ImageSignPerso.ImageUrl = "data:image/png;base64," + base64String;
                        //imgSign.Visible = true;
                    }

                 //   WtiteLog("Visitor Sign" + txtVisitor.Text);
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
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        private void DisplayEmployeeImage(string visitor_path)
        {

            string fileName = Path.GetFileName(visitor_path);
            imgEmployeeImage.ImageUrl = "~/Handler1.ashx?ImagePath=" + default_path + fileName;
        }
        protected void gvGatePass_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Edit3")
            {

                int Rowid = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                string entityid = row.Cells[2].Text;

                FillPersonalDetails(Rowid);

                mpWriteOnCard.Show();
            }


            else if (e.CommandName == "ScanDocumnent")
            {
                //int Rowid = Convert.ToInt32(e.CommandArgument.ToString());
                //GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            }

            else if (e.CommandName == "Photo")
            {
                string Rowid = e.CommandArgument.ToString();
                //GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Session["VisitorID"] = Rowid;
                //  Response.Redirect("WebCamVisitor.aspx", true);



                // mpeCamera.Show();
            }
            else if (e.CommandName == "Finger")
            {
                int Rowid = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            }

            else if (e.CommandName == "WriteOnCard")
            {
                int Rowid = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                FillPersonalDetails(Rowid);
                mpWriteOnCard.Show();

            }
            else if (e.CommandName == "PaperPass")
            {
                GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                LinkButton lnkScanDocumnent = (LinkButton)gRow.FindControl("lnkScanDocumnent");
                LinkButton lnkFinger = (LinkButton)gRow.FindControl("lnkFinger");
                LinkButton lnkPhoto = (LinkButton)gRow.FindControl("lnkPhoto");
                LinkButton lnkSignCapture = (LinkButton)gRow.FindControl("lnkSignCapture");
                LinkButton lnkPaperPass = (LinkButton)gRow.FindControl("lnkPaperPass");

                if (lnkFinger.Text == "Modify" && lnkPhoto.Text == "Modify" && lnkSignCapture.Text == "Modify" && lnkScanDocumnent.Text == "Modify")
                {
                    lnkPaperPass.Enabled = true;
                    string strReqstId = e.CommandArgument.ToString();
                   // InsertGatepassReg(strReqstId);
                    fillPaperPassDetails(strReqstId);
                    mpePrintgp.Show();
                }
                else
                {
                    return;
                }

              

            }

          

        }
        public void fillPaperPassDetails(string id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "fillGatePass";

            cmd.Parameters.AddWithValue("@requestId", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                hdnRequestID.Value = id;
                lblVisitorCardNo.Text = "";
                lblVisitorIDPass.Text = dt.Rows[0]["Visitor_id"].ToString().ToUpper().Trim();
                lblVisitorType.Text = dt.Rows[0]["visitor_type"].ToString().ToUpper().Trim();
                lblGatePassNo.Text = dt.Rows[0]["Gatepass_id"].ToString().ToUpper().Trim();
                lblValidFrom.Text = dt.Rows[0]["visitor_Entry_Time"].ToString().ToUpper().Trim();
                lblName.Text = dt.Rows[0]["Visitor_Name"].ToString().ToUpper().Trim();
                lblDesg.Text = dt.Rows[0]["Designation"].ToString().ToUpper().Trim();
                lblNation.Text = dt.Rows[0]["VisitorNationality"].ToString().ToUpper().Trim();
                lblcomapny.Text = dt.Rows[0]["VisitorCompany"].ToString().ToUpper().Trim();
                lblAdd.Text = dt.Rows[0]["VisitorCompAddress"].ToString().ToUpper().Trim() + "/" + dt.Rows[0]["mobileNo"].ToString().ToUpper().Trim();
                lblOfficial.Text = dt.Rows[0]["Name"].ToString().ToUpper().Trim();
                lblExtn.Text = dt.Rows[0]["EAD_PHONE_ONE"].ToString().ToUpper().Trim();
                lblPurposeOfVisit.Text = dt.Rows[0]["PurposeOfVisit"].ToString().ToUpper().Trim();
                lblValidFrom.Text = dt.Rows[0]["ValidFrom"].ToString().ToUpper().Trim();
                lblValidTo.Text = dt.Rows[0]["ValidTo"].ToString().ToUpper().Trim();
                myBarCode.ImageUrl = "BarCode.aspx?code=" + dt.Rows[0]["Gatepass_id"].ToString();
                lblVisitorCardNo.Text = dt.Rows[0]["cardID"].ToString().ToUpper().Trim();
                lblAddInfo.Text = dt.Rows[0]["additional_Info"].ToString().ToUpper().Trim();
                lblApprovalId.Text = dt.Rows[0]["Approval_Name"].ToString().ToUpper().Trim() + " (" + dt.Rows[0]["DESIGNATION_Dept"].ToString().ToUpper().Trim() + ")";
                
                
                ////PtxtMat.Text = dt.Rows[0]["additional_Info"].ToString();

                //Image4.ImageUrl = "";
                //string fileName = Path.GetFileName(dt.Rows[0]["visitor_photopath"].ToString());
                //imgVisitor.ImageUrl = "~/Handler1.ashx?ImagePath=" + default_path + fileName;

                if (dt.Rows[0]["VisitorImage"].ToString() != string.Empty || dt.Rows[0]["VisitorImage"].ToString() != "")
                {
                    byte[] bytes = (byte[])dt.Rows[0]["VisitorImage"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgVisitor.ImageUrl = "";
                    imgVisitor.ImageUrl = "data:image/jpeg;base64," + base64String;
                    //imgSign.Visible = true;
                }


                if (dt.Rows[0]["VisitorSign"].ToString() != string.Empty || dt.Rows[0]["VisitorSign"].ToString() != "")
                {
                    byte[] bytes = (byte[])dt.Rows[0]["VisitorSign"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgSign.ImageUrl = "";
                    imgSign.ImageUrl = "data:image/png;base64," + base64String;
                    //imgSign.Visible = true;
                }


                //  imgVisitor.ImageUrl = Server.MapPath("~\\Captures\\") + fileName;

            }

        }
        protected void btnPersonalDetaiCancel_Click(object sender, EventArgs e)
        {
            mpWriteOnCard.Hide();
            btnSearch_Click(sender, e);
            //bindDataGrid();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_VMS_GET_GATEPASSREGISTRATTIONDETAILS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (txtVisitorSearch.Text.ToString() == "" && txtName.Text.ToString() == "" && txtMobile.Text == "")
                {
                    bindDataGrid();
                    
                    //gvGatePass.DataSource = dt;
                    //gvGatePass.DataBind();
                    //cmdReset_Click(sender, e);

                }
                else
                {
                    String[,]  values = { 
                                            {"Visitor_id~" +txtVisitorSearch.Text.Trim(), "S" },
                                            {"Visitor_Name~" +txtName.Text.Trim(), "S" },
                                            {"mobileNo~" +txtMobile.Text.Trim(), "S" }	
                                        };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvGatePass.DataSource = _tempDT;
                    gvGatePass.DataBind();

                    DropDownList ddl = (DropDownList)gvGatePass.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvGatePass.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvGatePass.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvGatePass.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvGatePass.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvGatePass.PageCount == 0)
                    {
                        ((Button)gvGatePass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvGatePass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGatePass.PageIndex + 1 == gvGatePass.PageCount)
                    {
                        ((Button)gvGatePass.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvGatePass.PageIndex == 0)
                    {
                        ((Button)gvGatePass.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvGatePass.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGatePass.PageSize * gvGatePass.PageIndex) + 1) + " to " + (gvGatePass.PageSize * (gvGatePass.PageIndex + 1));

                    ((Label)gvGatePass.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvGatePass.PageSize * gvGatePass.PageIndex) + 1) + " to " + (((gvGatePass.PageSize * (gvGatePass.PageIndex + 1)) - 10) + gvGatePass.Rows.Count);

                    gvGatePass.BottomPagerRow.Visible = true;
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
        protected void gvGatePass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblVisitorID = (Label)e.Row.FindControl("lblVisitorID");

                    Label lblReq = (Label)e.Row.FindControl("lblReq");
                    LinkButton lnkScanDocumnent = (LinkButton)e.Row.FindControl("lnkScanDocumnent");
                    //Label lblRequestID = (Label)e.Row.FindControl("lblRequestID");

                    LinkButton lnkFinger = (LinkButton)e.Row.FindControl("lnkFinger");

                    LinkButton lnkWriteOnCard = (LinkButton)e.Row.FindControl("lnkWriteOnCard");

                    LinkButton lnkPhoto = (LinkButton)e.Row.FindControl("lnkPhoto");
                    Label lblVisitorCardNO = (Label)e.Row.FindControl("lblVisitorCardNO");

                    LinkButton lnkSignCapture = (LinkButton)e.Row.FindControl("lnkSignCapture");
                    Label lblSign = (Label)e.Row.FindControl("lblSign");
                    LinkButton lnkSignEdit = (LinkButton)e.Row.FindControl("lnkSignEdit");

                    string connection = conn.ConnectionString;
                    LinkButton lnkPaperPass = (LinkButton)e.Row.FindControl("lnkPaperPass");

                    //if (lblVisitorID.Text.Trim() == "")
                    //{
                    //    lnkPhoto.Enabled = false;
                    //    lnkScanDocumnent.Enabled = false;
                    //    lnkWriteOnCard.Enabled = false;
                    //    lnkPaperPass.Enabled = false;
                    //    lnkSignCapture.Enabled = false;
                    //    lnkFinger.Text = "Capture"; 
                    //}
                    //else
                    //{
                        lnkPhoto.Enabled = true;
                        lnkScanDocumnent.Enabled = true;
                        //lnkWriteOnCard.Enabled = true;
                        lnkPaperPass.Enabled = true;
                        lnkSignCapture.Enabled = true;
                        lnkFinger.Text = "Modify";
                    //}


                    IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                    //lnkFinger.Attributes.Add("onclick", "return FingerTemplate('" + lnkFinger.CommandArgument + "','" + connection + "');");


                    //if (lblVisitorID.Text.Trim() != "")
                    //{
                        lnkPhoto.Attributes.Add("onclick", "return WebCam('" + lnkPhoto.CommandArgument + "','" + connection + "','" + localIPs[0].ToString() + "');");

                        lnkScanDocumnent.Attributes.Add("onclick", "return CallAutoScan('" + lnkScanDocumnent.CommandArgument + "','" + connection + "');");

                        lnkSignCapture.Attributes.Add("onclick", string.Format("return Sign('{0}','New')", lblVisitorID.Text));

                        lnkSignEdit.Attributes.Add("onclick", string.Format("return Sign('{0}','New')", lblVisitorID.Text));
                   // }
                    DataTable dt = null;

                    dt = GetVisitorDetailsByVisitiorID(lblReq.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[0]["Visitor_Photopath"].ToString()))
                        {
                            lnkPhoto.Text = "Capture";
                            lnkPhoto.Enabled = true;
                            lnkScanDocumnent.Enabled = false;
                            //lnkWriteOnCard.Enabled = false;
                            lnkPaperPass.Enabled = false;
                            
                        }
                        else
                        {
                            lnkPhoto.Text = "Modify";
                            lnkPhoto.Enabled = true;
                            lnkScanDocumnent.Enabled = true;
                            //lnkWriteOnCard.Enabled = true;
                            lnkPaperPass.Enabled = true;
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["VisitorFingerImageWarWick1"].ToString()))
                        {
                            lnkFinger.Text = "Capture";
                        }
                        else
                        {
                            lnkFinger.Text = "Modify";
                        }
                        
                        if (string.IsNullOrEmpty(dt.Rows[0]["VisitorCardNo"].ToString()))
                        {
                            lnkWriteOnCard.ForeColor = System.Drawing.Color.Red;
                            lnkPaperPass.Enabled = true;

                        }
                        else
                        {
                            lnkWriteOnCard.ForeColor = System.Drawing.Color.Green;
                            lnkWriteOnCard.Enabled = false;
                            lnkPaperPass.Enabled = true;

                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["VisitorSign"].ToString()))
                        {
                            lnkSignCapture.Enabled = true;
                            lnkSignCapture.Text = "Capture";
                            lnkSignCapture.Visible = true;
                            lnkSignEdit.Visible = false;

                        }
                        else
                        {
                            lnkSignCapture.Enabled = true;
                            lnkSignCapture.Text = "Modify";
                            lnkSignCapture.Visible = false;
                            lnkSignEdit.Visible = true;
                            
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["ImageCount"].ToString()))
                        {
                            lnkScanDocumnent.Text="Capture";
                            lnkScanDocumnent.Enabled = true;

                        }
                        else
                        {
                            lnkScanDocumnent.Text = "Modify";
                            lnkScanDocumnent.Enabled = true;
                        }
                    }
                    else
                    {
                        lnkWriteOnCard.ForeColor = System.Drawing.Color.Red;
                        lnkPaperPass.Enabled = false;
                        lnkSignCapture.Visible = true;
                    }


                  

                }
            }
            catch (Exception ex)
            {
            }
        }
        private DataTable GetVisitorDetailsByVisitiorID(string requestid)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("Proc_VisitorDetails_ByVisitiorID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Requestid", SqlDbType.VarChar).Value = requestid;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return dt;


        }
        protected void btnCaptureClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("GatepassRegistration.aspx", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpePrintgp.Hide();

        }
        protected void UpdateGrid(object sender, EventArgs e)
        {
            bindDataGrid();

        }
        //[WebMethod]
        //public static string CompletePersonalisation(string EmpCode, string CSNR, int RequestID)
        [WebMethod]
        public static string CompletePersonalisation(string EmpCode, string CSNR, string RequestID)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("PROC_Vistor_Save_CardPersonalisationComplete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitorID", SqlDbType.VarChar).Value = EmpCode;
                cmd.Parameters.Add("@VisitorCardNo ", SqlDbType.VarChar).Value = CSNR;
                cmd.Parameters.Add("@RequestID", SqlDbType.VarChar).Value = RequestID;



                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)             
                {
                    conn.Close();
                }
                return "True";
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }
        [WebMethod]
        public static string GetMasterKey()
        {
            try
            {
                string MasterKey = HttpContext.Current.Session["MasterKey"].ToString();
                return MasterKey;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        public static string GenerateKeyWithoutEmployeeType(string CSNR)
        {
            string KeyA = "121212121212";
            string KeyB = "343434343434";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spGetISROKeysWithoutType", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                KeyA = dt.Rows[0][0].ToString();
                KeyB = dt.Rows[1][0].ToString();
                KeyA = KeyA.PadLeft(12, '0');
                KeyB = KeyB.PadLeft(12, '0');
                CSNR = CSNR.PadLeft(0x10, '0');
                //this.txtKeyA1.Text = TripleDESImplementation.ReverseKey(this.txtKeyA.Text, this.txtCSN.Text);
                //byte[] buffer = TripleDESImplementation.HEXToByteArray(this.txtCSN.Text);
                return TripleDESImplementation.EncryptKey(KeyA, CSNR) + "," + TripleDESImplementation.EncryptKey(KeyB, CSNR);
                //}
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "";
            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        [WebMethod]
        public static string CheckCSNR(string CSNR)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("spValidateVisitorCSNR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CSNR", SqlDbType.VarChar).Value = CSNR;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count == 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }
        public void InsertGatepassReg(string RequestID)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("PROC_Vistor_Save_CardPersonalisationComplete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitorID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@VisitorCardNo ", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@RequestID", SqlDbType.VarChar).Value = RequestID;



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
        [WebMethod]
        public static string InsertVisitorAccessData(string strRequestID)
        {


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Sp_Insert_Visitor_Access", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pRequestId", strRequestID);
                cmd.CommandTimeout = 0;
                int iCount = cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (iCount > 0)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return "False";
            }
        }

        private void WtiteLog(string strMessage)
        {
            StreamWriter SW=null;
            try
            {
                SW=new StreamWriter("D:\\Logs\\UnoLog\\Log"+DateTime.Now.ToString("ddMMyyyy")+".txt",true);
                SW.WriteLine(strMessage);
                SW.Close();
            }
            catch(Exception ex)
            {
                SW.Close();
            }
        }

    }
}

//Date Modified         Reason
//20-Jun-2016 18:25     Made Changes for Visitor_Access