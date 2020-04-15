using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

using System.Net;
using System.Net.Mail;
namespace UNO
{
    public partial class ManualAttendView : System.Web.UI.Page
    {
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        string manualattnddeletedate;
        string userid;


        public string strMailOption;
        public string strMailServer;
        public string strMailUserName;
        public string strMailPassword;
        public int strMailPort;
        public string strEmpFromAddress;
        public string strEmpToAddress;

        public string strdbFrmdt;
        public string strdbToDt;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }


            if (!Page.IsPostBack)
            {


                bindDataGrid();


                gvManualAttnd.Columns[2].Visible = false;
            }
            manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
            btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvManualAttnd.ClientID + "');");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Session.Remove("AppMode");




            Response.Redirect("ManualADD.aspx");




        }

        void bindDataGrid()
        {
            try
            {

                string strsql = " select MA_RECID, MA_EMPID , ent_employee_personal_dtls.Epd_First_Name + ' ' +ent_employee_personal_dtls.EPD_LAST_NAME as Name,convert(VARCHAR(20),MA_SWIPEFROMDATE,103) as FromDate, " +
                            " case " +
                            " when convert(VARCHAR(20),MA_SWIPETODATE,103)= convert(VARCHAR(20), " +
                            " MA_SWIPEFROMDATE,103) " +
                            " then '' else convert(VARCHAR(20),MA_SWIPETODATE,103)  end as " +
                            " ToDate ,convert(char(5),MA_SWIPEFROMTIME,8) " +
                            " as [In_Time],convert(char(5),MA_SWIPETOTIME,8) as [Out_Time], " +
                            " MA_STATUS from TA_Manual_Att,ent_employee_personal_dtls " +
                            " inner join ENT_EMPLOYEE_OFFICIAL_DTLS " +
                            " on EPD_EMPID=EOD_EMPID " +
                            " where  TA_Manual_Att.MA_EMPID=ent_employee_personal_dtls.epd_empid  " +
                            " and MA_ISDELETED='0' and EPD_ISDELETED='0' and EOD_ACTIVE='1' ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvManualAttnd.DataSource = dt;
                gvManualAttnd.DataBind();
                if (gvManualAttnd.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                    DropDownList ddl = (DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvManualAttnd.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvManualAttnd.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvManualAttnd.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvManualAttnd.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvManualAttnd.PageCount == 0)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvManualAttnd.PageIndex + 1 == gvManualAttnd.PageCount)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvManualAttnd.PageIndex == 0)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1));

                    ((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (((gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1)) - 10) + gvManualAttnd.Rows.Count);

                    gvManualAttnd.BottomPagerRow.Visible = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvManualAttnd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManualAttnd.EditIndex = e.NewEditIndex;
            Label lblId = (Label)gvManualAttnd.Rows[gvManualAttnd.EditIndex].FindControl("lblId");
            string ID = lblId.Text;
            // string ID = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[2].Text;
            string EmpID = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[3].Text;
            string strFromDt = gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[4].Text;
            //string strSwipeMode=gvManualAttnd.Rows[gvManualAttnd.EditIndex].Cells[4].Text;
            //Session["AppMode"] = "EDIT";
            // string AppMode="ADD";

            Session["EmpID"] = EmpID;
            Session["RquestDT"] = strFromDt;
            Response.Redirect("ManualAttendEdit.aspx?id=" + ID);
            //  Response.Redirect("ManualAttendEdit.aspx?id=" + EmpID);
        }

        protected void gvManualAttnd_sorting(object sender, GridViewSortEventArgs e)
        {
            bindDataGrid();
        }

        protected void gvManualAttnd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManualAttnd.PageIndex = e.NewPageIndex;
            bindDataGrid();

        }

        protected void gvManualAttnd_DataBound(object sender, EventArgs e)
        {
            string strsql = " select MA_RECID, MA_EMPID , ent_employee_personal_dtls.Epd_First_Name + ' ' +ent_employee_personal_dtls.EPD_LAST_NAME as Name,convert(VARCHAR(20),MA_SWIPEFROMDATE,103) as FromDate, " +
                                " case " +
                                " when convert(VARCHAR(20),MA_SWIPETODATE,103)= convert(VARCHAR(20), " +
                                " MA_SWIPEFROMDATE,103) " +
                                " then '' else convert(VARCHAR(20),MA_SWIPETODATE,103)  end as " +
                                " ToDate ,convert(char(5),MA_SWIPEFROMTIME,8) " +
                                " as [In_Time],convert(char(5),MA_SWIPETOTIME,8) as [Out_Time], " +
                                " MA_STATUS from TA_Manual_Att,ent_employee_personal_dtls " +
                                " inner join ENT_EMPLOYEE_OFFICIAL_DTLS " +
                                " on EPD_EMPID=EOD_EMPID " +
                                " where  TA_Manual_Att.MA_EMPID=ent_employee_personal_dtls.epd_empid  " +
                                " and MA_ISDELETED='0' and EPD_ISDELETED='0' and EOD_ACTIVE='1' ";

            //   "  FROM ENT_HOLIDAY Where HOLIDAY_ISDELETED = '0'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;


        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            bool Check = false;
            bool marked = false;
            for (int i = 0; i < gvManualAttnd.Rows.Count; i++)
            {
                SqlConnection conn = new SqlConnection(m_connectons);
                conn.Open();
                SqlCommand objcmd = new SqlCommand();
                objcmd.Connection = conn;

                try
                {

                    CheckBox delrows = (CheckBox)gvManualAttnd.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        if (marked == false)
                        {
                            marked = true;
                        }
                        Label lblId = (Label)gvManualAttnd.Rows[i].FindControl("lblId");

                        string rowid = lblId.Text;


                        objcmd.CommandType = CommandType.StoredProcedure;
                        objcmd.CommandText = "Proc_Delete_HRENTRY";
                        objcmd.Parameters.AddWithValue("@pREQ_RECID", rowid);
                        objcmd.Parameters.AddWithValue("@EntityFlag", "MA");
                        objcmd.ExecuteNonQuery();

                        Check = true;

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    // trans.Rollback();
                    // throw ex;
                }

            }
            if (Check == true)
            {
                lblMessages.Text = "Record Deleted Successfully";
                lblMessages.Visible = true;
            }
            else if (marked == false)
            {
                lblMessages.Text = "Please select record to Delete";
                lblMessages.Visible = true;
            }
            bindDataGrid();
            string someScript2 = "";
            someScript2 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);

        }


        public void chkMailConfiguration()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            String strSql = "select Identifier,value FROM ENT_PARAMS WHERE  MODULE='ESS'";
            SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["Identifier"].ToString() == "WITHMAIL")
                {
                    strMailOption = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVER")
                {
                    strMailServer = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERUSERNAME")
                {
                    strMailUserName = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILSERVERPASSWORD")
                {
                    strMailPassword = dt.Rows[i]["Value"].ToString();
                }
                if (dt.Rows[i]["Identifier"].ToString() == "MAILPORT")
                {
                    strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);
                }
                //strMailServer=dt.Rows[i]["Value"].ToString();
                //strMailUserName=dt.Rows[i]["Value"].ToString();
                //strMailPassword=dt.Rows[i]["Value"].ToString();
                // strMailPort = Convert.ToInt32(dt.Rows[i]["Value"]);

            }

        }

        public void FindEmailID()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;


            objcmd.CommandText = "select epd_email from ENT_EMPLOYEE_PERSONAL_DTLS where epd_empid= '" + userid + "'";
            //int rows = Convert.ToInt16(objcmd.ExecuteScalar());
            SqlDataReader dr = objcmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["epd_email"] != null)
                {
                    //string usname = dr["UserID'].ToString();
                    strEmpFromAddress = dr["epd_email"].ToString();
                }

            }
            objcmd.Dispose();
        }


        public void FindMgrMailid()
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();
            SqlCommand objMgrcmd = new SqlCommand();
            objMgrcmd.Connection = conn;
            objMgrcmd.CommandText = "select ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMAIL from ENT_HierarchyDef,ENT_EMPLOYEE_PERSONAL_DTLS " +
                          " where ENT_HierarchyDef.Hier_Mgr_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and ENT_HierarchyDef.Hier_Emp_ID='" + userid + "'";
            //int rows = Convert.ToInt16(objcmd.ExecuteScalar());
            SqlDataReader drmgr = objMgrcmd.ExecuteReader();
            if (drmgr.Read())
            {
                if (drmgr["epd_email"] != null)
                {
                    //string usname = dr["UserID'].ToString();
                    strEmpToAddress = drmgr["epd_email"].ToString();
                }

            }

        }

        public void SendMail()
        {
            FindEmailID();
            FindMgrMailid();

            try
            {
                SmtpClient objSMTPCLIENT = new SmtpClient();
                MailMessage objMailMessage = new MailMessage();
                string message = "I would like to cancel Manual Attendance from '" + strdbFrmdt + "' to '" + strdbToDt + "'";
                message = message + "Kindly do the needful.";
                objMailMessage.From = new MailAddress(strEmpFromAddress);
                objMailMessage.To.Add(strEmpToAddress.Trim());
                // p_Mailobj.CC.Add(_mailCCAddress.Trim());
                objMailMessage.Subject = "OD Application";
                objMailMessage.Body = message.Trim();
                objMailMessage.Priority = MailPriority.High;

                objSMTPCLIENT.Port = strMailPort;
                objSMTPCLIENT.Host = strMailServer;

                if (strMailUserName != "")
                {
                    CredentialCache.DefaultNetworkCredentials.UserName = strMailUserName;
                    CredentialCache.DefaultNetworkCredentials.Password = strMailPassword;

                    objSMTPCLIENT.Credentials = CredentialCache.DefaultNetworkCredentials;

                }
                objSMTPCLIENT.Send(objMailMessage);


            }

            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = "";

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            
            string strsql = " select MA_RECID, MA_EMPID , ent_employee_personal_dtls.Epd_First_Name + ' ' +ent_employee_personal_dtls.EPD_LAST_NAME as Name,convert(VARCHAR(20),MA_SWIPEFROMDATE,103) as FromDate, " +
                                " case " +
                                " when convert(VARCHAR(20),MA_SWIPETODATE,103)= convert(VARCHAR(20), " +
                                " MA_SWIPEFROMDATE,103) " +
                                " then '' else convert(VARCHAR(20),MA_SWIPETODATE,103)  end as " +
                                " ToDate ,convert(char(5),MA_SWIPEFROMTIME,8) " +
                                " as [In_Time],convert(char(5),MA_SWIPETOTIME,8) as [Out_Time], " +
                                " MA_STATUS from TA_Manual_Att,ent_employee_personal_dtls " +
                                " inner join ENT_EMPLOYEE_OFFICIAL_DTLS " +
                                " on EPD_EMPID=EOD_EMPID " +
                                " where  TA_Manual_Att.MA_EMPID=ent_employee_personal_dtls.epd_empid  " +
                                " and MA_ISDELETED='0' and EPD_ISDELETED='0' and EOD_ACTIVE='1'  ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"MA_EMPID~" +txtCompanyId.Text.Trim(), "S" },
				{"name~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                DataTable _tempDT = new DataTable();
                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, dt);
                gvManualAttnd.DataSource = _tempDT;
                gvManualAttnd.DataBind();
                if (_tempDT.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                    DropDownList ddl = (DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvManualAttnd.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvManualAttnd.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvManualAttnd.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvManualAttnd.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvManualAttnd.PageCount == 0)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvManualAttnd.PageIndex + 1 == gvManualAttnd.PageCount)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvManualAttnd.PageIndex == 0)
                    {
                        ((Button)gvManualAttnd.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1));

                    ((Label)gvManualAttnd.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvManualAttnd.PageSize * gvManualAttnd.PageIndex) + 1) + " to " + (((gvManualAttnd.PageSize * (gvManualAttnd.PageIndex + 1)) - 10) + gvManualAttnd.Rows.Count);

                    gvManualAttnd.BottomPagerRow.Visible = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }

        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Text = "";
            txtCompanyName.Text = "";
            lblMessages.Text = "";
            bindDataGrid();

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = Convert.ToInt32(((DropDownList)gvManualAttnd.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = gvManualAttnd.PageIndex - 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvManualAttnd.PageIndex = gvManualAttnd.PageIndex + 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

    

    }
}