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
using AjaxControlToolkit;
using System.Globalization;
using System.Text;
using System.Threading;

namespace UNO
{
    public partial class ESS_Dashboard : System.Web.UI.Page
    {
        string empid;
        ArrayList arr = new ArrayList();
        DataTable MonthlyAttendance = new DataTable();
        public string strRequestDt;
        public string strFrmDt;
        string UserMailId = "";
        string managerMail_id = "";
        public string strToDt;
        //int btnclick = 0;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["uid"] == null || Session["uid"].ToString() == "")
                {
                    Response.Redirect("Login.aspx", true);
                    empid = Session["uid"].ToString();
                }
                if (!IsPostBack)
                {
                    empid = Session["uid"].ToString();
                    GetMonthlyAttendance(DateTime.Now.Month, DateTime.Now.Year);
                    //FillReasonMA();
                    //FillReasonOD();
                    //FillReasonLV();
                    //FillLeaveTypes();
                    //Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID();
                    //FillReasonTypeCO();
                    //FillReasonOutPass();
                    fillMonthyAttendenceAndPendingRequests(DateTime.Now.Month, DateTime.Now.Year);
                    getFillProfile();
                    fillAw8ingRequest();
                    fillLeaveBalance();
                    //BindBirthDays();
                    chagePhoto();
                    // GetAnnouncement();
                    txtFrmDate.Attributes.Add("readonly", "true");

                }


                empid = Session["uid"].ToString();
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
        private void chagePhoto()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select epd_photourl,epd_gender from dbo.ENT_EMPLOYEE_PERSONAL_DTLS where EPD_EMPID='" + empid + "'";
                SqlDataAdapter daType = new SqlDataAdapter(strSql, conn);
                DataTable dtType = new DataTable();
                daType.Fill(dtType);
                if (dtType.Rows.Count > 0)
                {
                    string imgUrl = dtType.Rows[0]["epd_photourl"].ToString();

                    if (imgUrl != "")
                    {

                        imgEmployeeImage.ImageUrl = "";
                        imgEmployeeImage.ImageUrl = "~/Handler1.ashx?ImagePath=" + imgUrl;
                    }
                    else
                    {
                        if (dtType.Rows[0]["epd_gender"].ToString() == "M")
                            imgEmployeeImage.ImageUrl = "~/EmpImage/M2.jpg";
                        else
                            imgEmployeeImage.ImageUrl = "~/EmpImage/M1.jpg";


                    }
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
        private void FillLeaveTypes()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //string strSql = " select distinct Leave_ID,Leave_Description,EOD_CATEGORY_ID  from TA_Leave_File , TA_LEAVE_RULE_NEW t inner join ENT_EMPLOYEE_OFFICIAL_DTLS e " +
                //              " on e.EOD_CATEGORY_ID=t.LR_CATEGORYID where EOD_EMPID='" + empid + "' and Leave_ISDELETED = 'FALSE'  ";
                //string strSql = "select Leave_ID,Leave_Description  from TA_Leave_File where  Leave_ISDELETED = 'FALSE'";
                string strSql = " select distinct Leave_ID,Leave_Description from TA_Leave_File  where Leave_ISDELETED = 'FALSE' ";

                SqlDataAdapter daType = new SqlDataAdapter(strSql, conn);
                DataTable dtType = new DataTable();
                daType.Fill(dtType);
                ddleaveType.DataValueField = "Leave_ID";
                ddleaveType.DataTextField = "Leave_Description";
                ddleaveType.DataSource = dtType;
                ddleaveType.DataBind();
                ddleaveType.Items.Insert(0, "Select One");
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
        #region vaibhav
        private void FillReasonMA()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE'   and  Reason_Type = 'MA'";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReason.DataValueField = "Reason_ID";
                ddlReason.DataTextField = "Reason_Description";
                ddlReason.DataSource = dtReason;
                ddlReason.DataBind();
                ddlReason.Items.Insert(0, "Select One");

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
        private void FillReasonLV()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'LA' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReasonTypeLVReq.DataValueField = "Reason_ID";
                ddlReasonTypeLVReq.DataTextField = "Reason_Description";
                ddlReasonTypeLVReq.DataSource = dtReason;
                ddlReasonTypeLVReq.DataBind();
                ddlReasonTypeLVReq.Items.Insert(0, "Select One");
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
        private void FillReasonOD()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                               " and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'OD' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReasonOD.DataValueField = "Reason_ID";
                ddlReasonOD.DataTextField = "Reason_Description";
                ddlReasonOD.DataSource = dtReason;
                ddlReasonOD.DataBind();
                ddlReasonOD.Items.Insert(0, "Select One");
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
        private void FillReasonTypeCO()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("PROC_GET_CO_REASONTYPE", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter daReason = new SqlDataAdapter(cmd);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlReasonComOff.DataValueField = "Reason_ID";
                ddlReasonComOff.DataTextField = "Reason_Description";
                ddlReasonComOff.DataSource = dtReason;
                ddlReasonComOff.DataBind();
                ddlReasonComOff.Items.Insert(0, "Select One");
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
        private void FillReasonOutPass()
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                                "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'GP' ";
                SqlDataAdapter daReason = new SqlDataAdapter(strSql, conn);
                DataTable dtReason = new DataTable();
                daReason.Fill(dtReason);
                ddlResonOutPaas.DataValueField = "Reason_ID";
                ddlResonOutPaas.DataTextField = "Reason_Description";
                ddlResonOutPaas.DataSource = dtReason;
                ddlResonOutPaas.DataBind();
                ddlResonOutPaas.Items.Insert(0, "Select One");

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
        
        //public void fillAbsentStatusOutPass()
        //{
        //    try
        //    {

        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }

        //        string strsql;
        //        string bde;

        //        strsql = "SELECT CONVERT(VARCHAR(10),TDAY_DATE,103) as [AbsentDT],TDAY_STATUS FROM TDAY WHERE TDAY_STATUS in ('AB','PRAB') " +
        //                 " And TDay_Date Between  Convert(Datetime, DATEADD(YEAR,-3 ,(GETDATE()-1)),103)  And Convert(DateTime, getdate()-1, 103) " +
        //                 " and tday_empcde= '" + empid + "' Order By TDay_Date ";

        //        SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        if (dt.Rows.Count != 0)
        //        {
        //            gvStatus.DataSource = dt;
        //            gvStatus.DataBind();
        //            gvStatus.Enabled = true;
        //            gvStatus.Visible = true;

        //        }
        //        else
        //        {
        //            gvStatus.Visible = false;

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        //public void bindBarChart()
        //{
        //    DataTable dt = GetData();

        //    //string[] x = new string[dt.Rows.Count];
        //    //decimal[] y = new decimal[dt.Rows.Count];
        //    //for (int i = 0; i < dt.Rows.Count; i++)
        //    //{
        //    //    x[i] = dt.Rows[i][0].ToString();
        //    //    y[i] = Convert.ToInt32(dt.Rows[i][1]);
        //    //}
        //    // BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y });

        //    decimal[] PL = new decimal[dt.Rows.Count];
        //    decimal[] CL = new decimal[dt.Rows.Count];
        //    decimal[] SL = new decimal[dt.Rows.Count];
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        PL[i] = Convert.ToDecimal(dt.Rows[0][0]);
        //        CL[i] = Convert.ToDecimal(dt.Rows[0][1]);
        //        SL[i] = Convert.ToDecimal(dt.Rows[0][2]);
        //    }
        //    BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = PL, BarColor = "#2fd1f9", Name = "PL" });
        //    BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = CL, BarColor = "#2fd1f9", Name = "SL" });
        //    BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = SL, BarColor = "#2fd1f9", Name = "CL" });

        //    // BarChart1.CategoriesAxis = string.Join(",", values,CL,SL);
        //    //BarChart1.ChartTitle = "Levae Details";
        //    //if (x.Length > 3)
        //    //{
        //    //    BarChart1.ChartWidth = (x.Length * 75).ToString();
        //    //}

        //    if (PL.Length > 3)
        //    {
        //        BarChart1.ChartWidth = (PL.Length * 75).ToString();
        //    }

        //    //BarChartSeries sc=new BarChartSeries();
        //    //sc.Data=Convert.to(dt.Rows[0][0]);
        //    //BarChart1.Series.Add(


        //    BarChart1.Visible = true;



        //}
        private DataTable GetData()
        {



            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd1 = new SqlCommand("sp_Bind_LeaveChart", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@empid", "00019064");
            SqlDataAdapter dap = new SqlDataAdapter(cmd1);
            dap.Fill(dt);

            return dt;
        }
        private int GetDateForWeekDay(DayOfWeek DesiredDay, int Occurrence, int Month, int Year)
        {
            DateTime dtSat = new DateTime(Year, Month, 1);
            int j = 0;
            if (Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) >= 0)
            {
                j = Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) + 1;
            }
            else
            {
                j = (7 - Convert.ToInt32(dtSat.DayOfWeek)) + (Convert.ToInt32(DesiredDay) + 1);
            }
            return j + (Occurrence - 1) * 7;
        }
        private void GetMonthlyAttendance(int month, int year)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT case when convert(char(5), tday_intime, 108)>(select top 1 MaxLateTime from biometricStatus where SHIFT_ID = T.TDAY_SFTREPO) then 'AB' when TDAY_STATUS like 'PR%' then 'PR' else TDAY_STATUS end as TDAY_STATUS,Tday_date,left(CONVERT(VARCHAR(8),TDAY_INTIME,108),5) AS intime,left(CONVERT(VARCHAR(8),TDAY_OUTIME,108),5) AS OutTime FROM TDAY T WHERE DATEPART(MM,TDAY_DATE)='"
                    + month.ToString() + "' AND DATEPART(YY,TDAY_DATE)='" + year.ToString() + "' and TDAY_EMPCDE ='"
                    + Session["uid"].ToString() + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                MonthlyAttendance.Rows.Clear();
                da.Fill(MonthlyAttendance);
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
        //added by shraddha
        private void GetAnnouncement()
        {
            //try
            //{
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //    }
            //    SqlCommand cmd = new SqlCommand("EXEC USP_AdminAnnouncement @strCommand='SelectByDate' ", conn);
            //    lblAnnouncement.Text = Convert.ToString(cmd.ExecuteScalar());
            //    if (lblAnnouncement.Text == "")
            //    {
            //        lblAnnouncement.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
        private void BindBirthDays()
        {

            StringBuilder sb = new StringBuilder();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Select a.EPD_Salutation + ' ' + a.EPD_FIRST_Name + ' ' + a.EPD_Last_Name as Name, b.EOD_DIVISION_ID from ENT_EMPLOYEE_PERSONAL_DTLS a, ENT_EMPLOYEE_OFFICIAL_DTLS b where a.EPD_EMPID = b.EOD_EMPID and DATEPART(MM,a.EPD_DOB)= DATEPART(MM,getdate()) and DATEPART(DD,a.EPD_DOB)= DATEPART(DD,getdate()) ", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (sb.ToString() == "")
                        {
                            sb.Append(dt.Rows[i]["Name"].ToString() + "  [" + dt.Rows[i]["EOD_DIVISION_ID"].ToString() + "]");
                        }
                        else
                        {
                            sb.Append("  -  " + dt.Rows[i]["Name"].ToString() + "  [" + dt.Rows[i]["EOD_DIVISION_ID"].ToString() + "]");
                        }


                    }

                }
                if (sb.ToString() == "")
                {
                    //  lblBirdays.Text = "";
                }
                else
                {
                    //lblBirdays.Text = sb.ToString().TrimStart('-');
                }

            }
            catch (Exception ex)
            {
            }
        }
        public string getLvApplied_Status(string date)
        {
            string status = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("select * from ESS_TA_LA where '" + date + "' between ESS_LA_FROMDT and ESS_LA_TODT  and ESS_LA_STATUS='N' and ess_la_empid='" + empid + "' and ESS_LA_ISDELETED=0", conn);
                SqlCommand cmd1 = new SqlCommand("select * from ESS_TA_MA where '" + date + "' between ESS_ma_FROMDT and ESS_ma_TODT  and ESS_ma_STATUS='N' and ess_ma_empid='" + empid + "' and ESS_MA_ISDELETED=0", conn);
                SqlCommand cmd2 = new SqlCommand("select * from ESS_TA_OD where '" + date + "' between ESS_od_FROMDT and ESS_od_TODT  and ESS_od_STATUS='N' and ess_od_empid='" + empid + "' and ESS_OD_ISDELETED=0", conn);
                SqlCommand cmd3 = new SqlCommand("select * from ESS_TA_CO where '" + date + "' between ESS_co_FROMDT and ESS_co_FROMDT  and ESS_co_STATUS='N' and ess_co_empid='" + empid + "' and ESS_CO_ISDELETED=0", conn);
                SqlCommand cmd4 = new SqlCommand("select * from ESS_TA_gp where '" + date + "' between ESS_gp_FROMDT and ESS_gp_TODT  and ESS_gp_STATUS='N' and ess_gp_empid='" + empid + "' and ESS_GP_ISDELETED=0", conn);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    status = dt.Rows[0]["ESS_LA_LVCD"].ToString() + " Req.";
                    return status;
                }
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    status = "MA Req.";
                    return status;
                }
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    status = "OD Req.";
                    return status;
                }
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    status = "CO Req.";
                    return status;
                }
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                if (dt4.Rows.Count > 0)
                {
                    status = "OP Req.";
                    return status;
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {

            if (DropDownList1.SelectedIndex == 1 || DropDownList1.SelectedIndex == 2 || DropDownList1.SelectedIndex == 3 || DropDownList1.SelectedIndex == 4 || DropDownList1.SelectedIndex == 5)
                return;

            if (hdnClick.Value == "1")
                return;

            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.Controls.Clear();
                e.Cell.Text = "";
                e.Cell.CssClass = "otherMonth";
                //vaibhav
            }
            if (!e.Day.IsOtherMonth)
            {
                // e.Cell.Attributes.Add("ONCLICK",e.SelectUrl);
                e.Cell.Attributes.Add("style", "cursor:pointer");
                Table table = new Table();
                //table.CssClass = "Width100";
                // table.Width = "100%";
                TableRow row = new TableRow();
                TableRow rowin = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell celltemp = new TableCell();
                TableCell cell = new TableCell();
                Label Attandance = new Label();
                Label inTime = new Label();
                Label outTime = new Label();

                Attandance.ID = "Attend" + e.Day.Date.ToShortDateString();
                inTime.ID = "inTime" + e.Day.Date.ToShortDateString();
                outTime.ID = "outTime" + e.Day.Date.ToShortDateString();

                if (MonthlyAttendance.Rows.Count != 0)
                {
                    //DataRow[] foundDate = MonthlyAttendance.Select("tday_date = '" + e.Day.Date + "'");
                    DataRow[] foundDate = MonthlyAttendance.Select("tday_date = '" + CMSDateTime.CMSDateTime.ConvertToDateTime(e.Day.Date.Month.ToString().PadLeft(2, '0') + "/" + e.Day.Date.Day.ToString().PadLeft(2, '0') + "/" + e.Day.Date.Year.ToString(), "MM/dd/yyyy") + "'");
                    if (foundDate.Length != 0)
                    {
                        inTime.Text = foundDate[0]["intime"].ToString();
                        outTime.Text = foundDate[0]["OutTime"].ToString();

                        //change status according to applied leaves
                        string status = "";
                        //status = getLvApplied_Status(e.Day.Date.ToString());
                        status = getLvApplied_Status(CMSDateTime.CMSDateTime.ConvertToDateTime(e.Day.Date.Month.ToString().PadLeft(2, '0') + "/" + e.Day.Date.Day.ToString().PadLeft(2, '0') + "/" + e.Day.Date.Year.ToString(), "MM/dd/yyyy").ToString("MM/dd/yyyy"));
                        Attandance.Text = ReturnStatus(foundDate[0]["tday_status"].ToString(), e.Day.Date, status);

                        if (inTime.Text == "")
                        {
                            inTime.Text = "09:30";
                            inTime.CssClass = "BlnakValue";
                        }
                        if (outTime.Text == "")
                        {
                            outTime.Text = "18:00";
                            outTime.CssClass = "BlnakValue";
                        }
                        if (!e.Day.IsToday)
                        {
                            e.Cell.CssClass = ReturnCSS(foundDate[0]["tday_status"].ToString());
                        }
                        // if (Attandance.Text == "Work Day<br/><br/>" && e.Day.Date > DateTime.Now)
                        //  e.Cell.BackColor = System.Drawing.Color.Gray;
                    }
                    else
                    {
                        Attandance.Text = "NA";
                    }
                }
                else
                {
                    Attandance.Text = "NA";
                }

                cell.Controls.Add(Attandance);
                cell.ColumnSpan = 2;

                //cell.Style.Add("padding-left", "15%");
                //cell.Style.Add("padding-right", "15%");
                cell.Style.Add("text-align", "center");
                //cell.Style.Add("padding-botton", "20%");
                // inTime.CssClass = "inTime";
                celltemp.Controls.Add(inTime);
                //  celltemp.Style.Add("padding-right", "15px");
                celltemp.Style.Add("text-align", "left");
                celltemp.Style.Add("width", "100%");
                cell1.Controls.Add(outTime);
                // celltemp.Style.Add("padding-right", "15px");
                //outTime.CssClass = "outTime";
                rowin.Cells.Add(celltemp);
                rowin.Cells.Add(cell1);
                row.Cells.Add(cell);
                table.Rows.Add(row);
                table.Rows.Add(rowin);
                e.Cell.Controls.Add(table);

                if ((int)e.Day.Date.DayOfWeek == 0)
                {
                    // e.Cell.CssClass = "calWeeklyOff";

                }
                else if ((int)e.Day.Date.DayOfWeek == 6)
                {
                    if (e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 2, e.Day.Date.Month, e.Day.Date.Year)
                        || e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 4, e.Day.Date.Month, e.Day.Date.Year)
                        || e.Day.Date.Day == GetDateForWeekDay(DayOfWeek.Saturday, 5, e.Day.Date.Month, e.Day.Date.Year))
                    {
                    }
                }
                else
                {

                }
                if (e.Day.IsSelected)
                {
                    e.Cell.CssClass = "SelectedDate";

                }
            }
        }
        private string ReturnCSS(string StatusCode)
        {
            switch (StatusCode)
            {
                case "PR":
                    {
                        return "calPresent";
                    }
                case "AB":
                    {

                        return "calAbsent";
                    }
                case "CL":
                    {
                        return "calAbsent";
                    }
                case "ABW2":
                    {
                        return "calWeeklyOff";
                    }
                case "ABWO":
                    {
                        return "calWeeklyOff";
                    }
                case "SL":
                    {
                        return "calAbsent";
                    }
                case "OD":
                    {
                        return "calPresent";
                    }
                case "PRWO":
                    {
                        return "calWeeklyOff";
                    }
                case "PRW2":
                    {
                        return "calWeeklyOff";
                    }
                case "ABHO":
                    {
                        return "calWeeklyOff";
                    }
                case "PRHO":
                    {
                        return "calWeeklyOff";
                    }
                case "ABHW":
                    {
                        return "calPresent";
                    }
                default:
                    {
                        return "calGeneral";
                    }
            }
        }
        private string ReturnStatus(string StatusCode, DateTime tdayDate, string changeStatus)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            switch (StatusCode)
            {
                case "PR":
                    {
                        if (changeStatus != "")
                        {
                            changeStatus = "";
                            if (tdayDate > DateTime.Now)
                            {
                                return "<b>Work Day</b><br/>" + changeStatus + "";
                            }
                            else
                            {
                                return "<b>Work Day</b><br>&nbsp;&nbsp;&nbsp;<font color='red'><b>" + changeStatus + "</b></font>";
                            }
                        }
                        else
                        {
                            //return "<b>Work Day</b><br>&nbsp;<font color=''>Present</font>";
                            return "<b>Work Day</b><br>&nbsp;<font color=''>&nbsp;</font>";
                        }
                    }
                case "AB":
                    {
                        if (changeStatus != "")
                        {
                            changeStatus = "";
                            if (tdayDate > DateTime.Now)
                            {
                                return "<b>Work Day</b><br/>" + changeStatus + "";
                            }
                            else
                            {
                                return "<b>Work Day</b><br>&nbsp;&nbsp;&nbsp;<font color='red'><b>" + changeStatus + "</b></font>";
                            }
                        }
                        else
                        {
                            if (tdayDate > DateTime.Now)
                            {
                                return "<b>Work Day</b><br/><br/>";
                            }
                            else
                            {
                               // return "<b>Work Day</b><br>&nbsp;<font color='red'><b>Absent</b></font>";
                                return "<b>Work Day</b><br>&nbsp;<font color='red'><b>&nbsp;</b></font>";
                            }
                        }
                    }
                //case "CL":
                //    {
                //        return "Leave<br/><br/>";
                //    }
                case "ABW2":
                    {
                        return "<font color='#151B8D'><b>Week End<b></font><br/><br/>";
                    }
                case "ABWO":
                    {
                        return "<font color='#151B8D'><b>Weekly Off<b></font><br/><br/>";
                    }
                //case "SL":
                //    {
                //        return "Leave<br/><br/>";
                //    }
                case "OD":
                    {
                        return "<b>Outdoor Duty</b><br/><br/>";
                    }
                case "PRWO":
                    {
                        return "Weekly Off<BR>&nbsp;<font><b>Present</b></font>";
                    }
                case "PRW2":
                    {
                        return "Weekly Off<BR>&nbsp;<font><b>Present</b></font>";
                    }
                case "ABHO":
                    {
                        return "<b>Holiday</b><br/><br/>";
                    }
                case "PRHO":
                    {
                        //return "Working on Holiday";
                        return "Holiday<BR><font color='Black'><b>Present</b></font>";

                    }
                case "PRAB":
                    {
                        return "<b>Half Day</b><br/>" + changeStatus + "";
                    }




                //new case started by vaibhav

                case "PRPL": { return " <b>Workday</b>Hf + PL<br/><br/>"; }
                // case "ABW2": { return " <b>Weekend Off</b><br/><br/>"; }
                case "ABCL": { return " <b>0.5AB & 0.5CL</b><br/><br/>"; }
                //  case "ABWO": { return " <b>Weekly Off</b><br/><br/>"; }
                case "WPW2": { return " <b>WP</b><br/><br/>"; }
                case "CO": { return " <br/><b>Workday</b><br/>&nbsp;&nbspComp.Off<br/><br/>"; }
                case "ML": { return "  <br/><b>Workday</b><br/>&nbsp;&nbspML<br/><br/>"; }
                //  case "ABHO": { return " <b>Holiday</b><br/><br/>"; }
                case "SL": { return " <br/><b>Workday</b><br/>&nbsp;&nbspSL<br/><br/>"; }
                case "PRWP": { return " <br/><b>Workday</b><br/> 0.5PR & 0.5WP<br/><br/>"; }
                //  case "PRHO": { return " <b>Holiday Present</b><br/><br/>"; }
                case "ODW2": { return " <br/><b>Weekend Off</b><br/>&nbsp;&nbspOutdoor<br/><br/>"; }
                case "PRSL": { return " <br/><b>Workday</b><br/>0.5PR & 0.5SL <br/><br/>"; }
                //  case "OD": { return " <b>Outdoor</b><br/><br/>"; }
                case "ODHO": { return " <br/><b>Holiday</b><br/>&nbsp;&nbspOutDoor<br/><br/>"; }
                case "WP": { return " <br/><b>Workday</b><br/>&nbsp;&nbsp WP<br/><br/>"; }
                // case "PR": { return " <b> Present</b><br/><br/>"; }
                case "MLW2": { return " <br/><b>Weekend Off</b><br/>&nbsp;&nbsp ML<br/><br/>"; }
                case "PLWO": { return " <br/><b>Weekly Off</b><br/>&nbsp;&nbspPL<br/><br/>"; }
                case "ABSL": { return " <br/><b>Workday</b><br/>0.5AB & 0.5SL<br/><br/>"; }
                // case "PRW2": { return " <b> Present</b><br/><br/>"; }
                case "MLWO": { return " <br/><b>Weekly Off</b><br/>&nbsp;&nbspML<br/><br/>"; }
                case "SLWO": { return " <br/><b>Weekly Off</b><br/>&nbsp;&nbspSL<br/><br/>"; }
                case "SLW2": { return " <br/><b>Weekend Off</b><br/>&nbsp;&nbspSL<br/><br/>"; }
                // case "PRAB": { return " <b>Half day</b><br/><br/>"; }
                case "ABWP": { return " <br/><b>Workday</b><br/>Hf.AB & Hf.WP<br/><br/>"; }
                case "PRPR": { return " <br/><b>Workday</b><br/>&nbsp;&nbspPresent<br/><br/>"; }
                // case "PRWO": { return " <b>Present</b><br/><br/>"; }
                case "PL": { return " <br/><b>Workday</b><br/>&nbsp;&nbspPL<br/><br/>"; }
                case "CLCL": { return " <br/><b>Workday</b><br/>&nbsp;&nbspCL<br/><br/>"; }
                case "SLHO": { return " <br/><b>Holiday</b><br/>&nbsp;&nbspSL<br/><br/>"; }
                //  case "AB": { return " <b>Absent</b><br/><br/>"; }
                case "PLW2": { return " <br/><b>Weekend Off</b><br/>&nbsp;&nbspPL<br/><br/>"; }
                case "WPWO": { return " <br/><b>Weekly Off</b><br/>&nbsp;&nbspWP<br/><br/>"; }
                case "ODWO": { return " <br/><b>Weekly Off</b><br/>&nbsp;&nbspOutdoor<br/><br/>"; }


                //END case started by vaibhav
                default:
                    {
                        SqlCommand cmd = new SqlCommand("USP_DashBoard @strCommand='getLeaveCode',@Leave_ID='" + StatusCode + "'", conn);
                        string Value = Convert.ToString(cmd.ExecuteScalar());
                        if (Value != "")
                        {
                            return "<b>Leave</b><br/><br/>";
                        }
                        else
                        {
                            return StatusCode;
                        }
                    }
            }
        }
        public void fillAw8ingRequest()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_getAwa8ingReq", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@managerID", empid);
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblW8ReqLv.Text = Convert.ToString(dt.Rows[0]["lvReqCount"]);
                lblW8ReqMa.Text = Convert.ToString(dt.Rows[0]["MAReqCount"]);
                lblW8ReqOD.Text = Convert.ToString(dt.Rows[0]["ODReqCount"]);
                lblW8ReqOP.Text = Convert.ToString(dt.Rows[0]["GPReqCount"]);
                lblW8ReqCO.Text = Convert.ToString(dt.Rows[0]["COReqCount"]);
                if (Convert.ToInt32(dt.Rows[0]["lvReqCount"]) < 1) { lblW8ReqLvtxt.Text = "Leave"; }
                if (Convert.ToInt32(dt.Rows[0]["MAReqCount"]) < 1) { lblW8ReqMatxt.Text = "Manual"; }
                if (Convert.ToInt32(dt.Rows[0]["ODReqCount"]) < 1) { lblW8ReqODtxt.Text = "Outdoor"; }
                if (Convert.ToInt32(dt.Rows[0]["GPReqCount"]) < 1) { lblW8ReqOPtxt.Text = "Outpass"; }
                if (Convert.ToInt32(dt.Rows[0]["COReqCount"]) < 1) { lblW8ReqCOtxt.Text = "Com-Off"; }

            }

        }
       
        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{

        //string fromDate = "", toDate = "";
        //if (ViewState["fromdate"] == null)
        //{
        //    fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        //    ViewState["fromdate"] = fromDate;
        //    ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
        //}
        //else
        //{

        //    if (ViewState["toDate"] == null)
        //    {
        //        toDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        //        ViewState["toDate"] = toDate;
        //        ViewState["toDate1"] = Calendar1.SelectedDate.DayOfYear;

        //    }
        //    else
        //    {
        //        fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        //        ViewState["fromdate"] = fromDate;
        //        ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
        //        ViewState["toDate"] = null;
        //        ViewState["toDate1"] = null;

        //    }

        //}

        //if (Convert.ToString(ViewState["fromdate1"]) != null)
        //{
        //    if (Convert.ToString(ViewState["toDate1"]) != null)
        //    {
        //        DateTime dt = DateTime.ParseExact(Convert.ToString(ViewState["fromdate"]), "dd/MM/yyyy", null);
        //      //  DateTime dt = Convert.ToDateTime(ViewState["fromdate"]);

        //        int Fdate = Convert.ToInt32(ViewState["fromdate1"]);
        //        int Tdate = Convert.ToInt32(ViewState["toDate1"]);
        //        if (Tdate > Fdate)
        //        {

        //            int datediff = (Tdate - Fdate);
        //            for (int i = 0; i < datediff; i++)
        //            {
        //                Calendar1.SelectedDates.Add(dt.AddDays(i));
        //                Calendar1.SelectedDayStyle.CssClass = "SelectedDate";
        //            }
        //        }
        //        else
        //        {
        //            fromDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        //            ViewState["fromdate"] = fromDate;
        //            ViewState["fromdate1"] = Calendar1.SelectedDate.DayOfYear;
        //            ViewState["toDate"] = null;
        //            ViewState["toDate1"] = null;
        //        }

        //    }
        //}

        //GetMonthlyAttendance(Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Year);


        //}
       
        protected void btnNewReq_Click(object sender, EventArgs e)
        {

            // btnclick = 1;
            hdnClick.Value = "1";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("USP_DashBoard @strCommand='getReportingMgr' , @userId='" + empid + "'", conn);
            string ReportingMgr = Convert.ToString(cmd.ExecuteScalar());
            if (ReportingMgr != "")
            {
                //if (Calendar1.SelectedDate.Month == 1 && Calendar1.SelectedDate.Year == 1)
                //{
                //    lblMsg.Text = "Please select date from Calendar";
                //    mpeReportingMgr.Show();
                //}
                //else
                //{
                //    DateTime todate = DateTime.MinValue, fromDate = DateTime.MinValue;

                //    if (ViewState["fromdate"] != null)
                //    {
                //        fromDate = DateTime.ParseExact(ViewState["fromdate"].ToString(), "dd/MM/yyyy", null);
                //        txtFrmDate.Text = ViewState["fromdate"].ToString();
                //    }
                //    if (ViewState["toDate"] != null)
                //    {
                //        todate = DateTime.ParseExact(ViewState["toDate"].ToString(), "dd/MM/yyyy", null);
                //        txtToDate.Text = ViewState["toDate"].ToString();
                //        if (todate.CompareTo(fromDate) < 0)
                //        {
                //            txtFrmDate.Text = todate.ToString("dd/MM/yyyy");
                //            txtToDate.Text = fromDate.ToString("dd/MM/yyyy");
                //        }
                //        else
                //        {
                //            txtFrmDate.Text = fromDate.ToString("dd/MM/yyyy");
                //            txtToDate.Text = todate.ToString("dd/MM/yyyy");
                //        }
                //    }
                //    else
                //    {
                //        txtToDate.Text = txtFrmDate.Text;
                //    }
                //   mpeNewReq.Show();
                //}

                FillReasonMA();
                FillReasonOD();
                FillReasonLV();
                FillLeaveTypes();
                Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID();
                FillReasonTypeCO();
                FillReasonOutPass();
                fillgridHoliday();
                GetDate();
                mpeNewReq.Show();
            }
            else
            {
                lblMsg.Text = "Reporting Manager is not set.";
                mpeReportingMgr.Show();
            }






        }
        private void GetDate()
        {
            txtFrmDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void btnSaveManualAtt_Click(object sender, EventArgs e)
        {
            InserManualAttendence();
            Panel1.Attributes.Add("style", "display:block");

        }
        protected void btnSaveLvReq_Click(object sender, EventArgs e)
        {
            InsertLeaveApplication();
            PnlLvReq.Attributes.Add("style", "display:block");
        }
        protected void btnSaveOdReq_Click(object sender, EventArgs e)
        {
            InsertODApplication();
            pnlODreq.Attributes.Add("style", "display:block");
        }
        public void InserManualAttendence()
        {

            DateTime dtFromtime, dtTotime;
            int intHr = 0, intMi = 0;
            if (frm_time.Text.Trim() != "" && To_Time.Text.Trim() != "")
            {
                dtFromtime = new DateTime(1990, 01, 01, Convert.ToInt16(frm_time.Text.Trim().Substring(0, 2)), Convert.ToInt16(frm_time.Text.Trim().Substring(3, 2)), 00);
                dtTotime = new DateTime(1990, 01, 01, Convert.ToInt16(To_Time.Text.Trim().Substring(0, 2)), Convert.ToInt16(To_Time.Text.Trim().Substring(3, 2)), 00);
                intHr = Convert.ToInt16(dtTotime.Subtract(dtFromtime).TotalHours);
                intMi = Convert.ToInt16(dtTotime.Subtract(dtFromtime).TotalMinutes);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            if (frm_time.Text == "" && To_Time.Text == "")
            {
                eoorLabelManulAtt.Visible = true;
                eoorLabelManulAtt.Text = "Enter  in time or out time";
                Panel1.Attributes.Add("style", "display:block");
                return;
            }
            strFrmDt = txtFrmDate.Text;
            strToDt = txtToDate.Text;

            WriteLog("Initial State :- " + strFrmDt);
            WriteLog("Initial State :- " + strToDt);


            if (strToDt == "")
            {
                strToDt = strFrmDt;
            }
            //DateTime chkdate = DateTime.ParseExact(strToDt, "dd/MM/yyyy", null);
            DateTime chkdate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDt, "dd/MM/YY");
           // DateTime chkdate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text,"dd/MM/yyyy");

            WriteLog("Initial State DateTime :- " + chkdate);

            if (chkdate > DateTime.Now)
            {
                eoorLabelManulAtt.Visible = true;
                eoorLabelManulAtt.Text = "Manual Attendance cannot be applied for future date";
                Panel1.Attributes.Add("style", "display:block");
                return;
            }
            if (frm_time.Text.Trim() != "" && To_Time.Text.Trim() != "")
            {
                if (intHr < 0 || intMi < 0)
                {
                    eoorLabelManulAtt.Visible = true;
                    eoorLabelManulAtt.Text = "Out Time should be greater than In Time";
                    Panel1.Attributes.Add("style", "display:block");
                    return;

                }
            }


            try
            {
                //Added by Pooja Yadav
                clsESSDashboardObjects objDashboard = null;
                new clsESSDashboardObjects().getAllRequest(ref objDashboard, strFrmDt, strToDt, empid);

                //if (objDashboard.IsManualAtt)
                //{
                //    eoorLabelManulAtt.Text = "Manual Attendance already applied for the date range";
                //    eoorLabelManulAtt.Visible = true;
                //    return;
                //}
                if (objDashboard.@IsShiftCreated)
                {
                    eoorLabelManulAtt.Text = "Shift not Created";
                    eoorLabelManulAtt.Visible = true;
                    Panel1.Attributes.Add("style", "display:block");
                    return;
                }
                if (objDashboard.IsLeaveApplied)
                {
                    eoorLabelManulAtt.Text = "Leave already applied for the date range";
                    eoorLabelManulAtt.Visible = true;
                    Panel1.Attributes.Add("style", "display:block");
                    return;
                }

                if (objDashboard.IsOutdoor)
                {
                    eoorLabelManulAtt.Text = "OD already applied for the date range";
                    eoorLabelManulAtt.Visible = true;
                    Panel1.Attributes.Add("style", "display:block");
                    return;
                }

                if (objDashboard.IsCompoff)
                {
                    eoorLabelManulAtt.Text = "Comp-off already applied for the date range";
                    eoorLabelManulAtt.Visible = true;
                    Panel1.Attributes.Add("style", "display:block");
                    return;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                objcmd.CommandText = "Insert into ESS_TA_MA(ESS_MA_EMPID,ESS_MA_REQUESTDT,ESS_MA_FROMDT,ESS_MA_FROMTM,ESS_MA_TODT,ESS_MA_TOTM, " +
                        "  ESS_MA_RSNID, ESS_MA_REMARK,ESS_MA_ORDER,ESS_MA_STATUS,ESS_MA_ISDELETED) " +
                          " values ('" + empid + "',Convert(datetime,'" + strRequestDt + "',103) " +
                          ",convert(datetime,'" + strFrmDt + "',103) " +
                          ",Convert(datetime,'" + strFrmDt + " " + frm_time.Text + ":00',103) " +
                          ",convert(datetime,'" + strToDt + "',103)" +
                          ",Convert(datetime,'" + strToDt + " " + To_Time.Text + ":00',103)" +
                          ",'" + ddlReason.SelectedValue + "', " +
                           "'" + txt_Remarks.Text + "','1','N','0')";


                objcmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                eoorLabelManulAtt.Text = "Record Saved Successfully";
                WriteLog("Record Saved Successfully");
                mpeNewReq.Hide();
                DropDownList1.SelectedIndex = 0;
                Panel1.Visible = false;

                //send mail to r1
                SetMailId();
                string cc = "";
                string subject = "Manual Attendance Application";

                string message = "I am applying Manual Attendance from " + strFrmDt + " to " + strToDt + System.Environment.NewLine + "";
                message = message + "Reason: " + txt_Remarks.Text + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";

                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";
                Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                thread.Start();
                lblMsg.Text = "Request applied successfully.";
                mpeReportingMgr.Show();
            }
            catch (Exception ex)
            {

            }
        }
        public void InsertLeaveApplication()
        {

            strFrmDt = txtFrmDate.Text;
            strToDt = txtToDate.Text;
            strRequestDt = DateTime.Now.ToString("dd/MM/yyyy");
            if (strToDt == "")
            {
                strToDt = strFrmDt;
            }
            //DateTime dtfromdt = DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null);
            //DateTime dtttodt = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);

            DateTime dtfromdt = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy");
            DateTime dtttodt = CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy");


            TimeSpan difference = dtttodt - dtfromdt;
            float LvDays = difference.Days + 1;

            if (rbtLeaveType1.SelectedValue == "H")
            {
                strToDt = strFrmDt;
                txtToDate.Text = txtFrmDate.Text;
                LvDays = 0.50f;
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;

            try
            {
                clsESSDashboardObjects objDashboard = null;
                new clsESSDashboardObjects().getAllRequest(ref objDashboard, strFrmDt, strToDt, empid);

                if (objDashboard.IsShiftCreated)
                {
                    errorLabelLV.Text = "Shift not created";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (objDashboard.IsLeaveApplied)
                {
                    errorLabelLV.Text = "Leave already applied for the date range";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (objDashboard.IsManualAtt)
                {
                    errorLabelLV.Text = "Manual Attendance already applied for the date range";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (objDashboard.IsOutdoor)
                {
                    errorLabelLV.Text = "OD already applied for the date range";
                    errorLabelLV.Visible = true;
                    return;
                }

                if (objDashboard.IsOutPass)
                {
                    errorLabelLV.Text = "Outpass already applied for the date range";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (objDashboard.IsCompoff)
                {
                    errorLabelLV.Text = "Comp-off already applied for the date range";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                objcmd.CommandText = "select ISNULL(tday_status,'') as tday_status from TDAY where TDAY_DATE  between convert(datetime,'" + strFrmDt + "',103) and Convert(datetime,'" + strToDt + "',103)  AND tday_empcde= '" + empid + "'";
                SqlDataAdapter da = new SqlDataAdapter(objcmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                String strtdaystatus = "PR";

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //if (strtdaystatus == dt.Rows[i]["tday_status"].ToString())
                    //{
                    //    errorLabelLV.Text = "Cannot apply for Leave as Employee is already present for the day";
                    //    errorLabelLV.Visible = true;
                    //    return;
                    //}

                    if (strFrmDt == strToDt)
                    {
                        if (dt.Rows[i]["tday_status"].ToString() == "ABW2" || dt.Rows[i]["tday_status"].ToString() == "ABWO")
                        {
                            errorLabelLV.Text = "Cannot apply Leave on weekly off";
                            errorLabelLV.Visible = true;
                            return;
                        }
                        else if (dt.Rows[i]["tday_status"].ToString() == "ABHO")
                        {
                            errorLabelLV.Text = "Cannot apply Leave on holiday";
                            errorLabelLV.Visible = true;
                            return;
                        }
                    }
                }

                if (ddleaveType.SelectedValue.ToString() != "WP")
                {
                    objcmd.CommandText = "Select LV_AVAILABLE from ta_leave_summary where Lv_Emp_Id='" + empid + "' and Lv_Leave_ID='" + ddleaveType.SelectedValue + "' and Lv_Available >= " + LvDays + " ";
                    int LeaveBal;
                    LeaveBal = Convert.ToInt32(objcmd.ExecuteScalar());
                    if (Convert.ToInt32(LeaveBal) < 1)
                    {
                        errorLabelLV.Text = " Insufficient Leave Balance ";
                        errorLabelLV.Visible = true;
                        return;
                    }

                    //added by vaibhav
                    objcmd.CommandText = "SELECT isnull(sum(ESS_LA_LVDAYS),0) FROM ESS_TA_LA WHERE ESS_LA_STATUS='N' AND ESS_LA_EMPID='" + empid + "' and  ESS_LA_ISDELETED=0 and ESS_LA_LVCD='" + ddleaveType.SelectedValue + "' ";
                    decimal LeaveBal2;
                    LeaveBal2 = Convert.ToDecimal(objcmd.ExecuteScalar());

                    LeaveBal2 = LeaveBal2 + Convert.ToDecimal(LvDays);

                    if (Convert.ToDecimal(LeaveBal2) > LeaveBal)
                    {
                        errorLabelLV.Text = " Insufficient Leave Balance, already Leave Request Pending ";
                        errorLabelLV.Visible = true;
                        return;
                    }
                }

                SqlCommand cmdLvRule = new SqlCommand("PROC_LEAVE_RULE_VALIDATION", conn);
                cmdLvRule.CommandType = CommandType.StoredProcedure;
                cmdLvRule.Parameters.AddWithValue("@pEmpCode", empid);
                cmdLvRule.Parameters.AddWithValue("@pLeaveCode", ddleaveType.SelectedValue);
                //cmdLvRule.Parameters.AddWithValue("@pLeaveFromDate", DateTime.ParseExact(strFrmDt, "dd/MM/yyyy", null));
                //cmdLvRule.Parameters.AddWithValue("@pLeaveToDate", DateTime.ParseExact(strToDt, "dd/MM/yyyy", null));

                cmdLvRule.Parameters.AddWithValue("@pLeaveFromDate", CMSDateTime.CMSDateTime.ConvertToDateTime(strFrmDt, "dd/MM/yyyy"));
                cmdLvRule.Parameters.AddWithValue("@pLeaveToDate", CMSDateTime.CMSDateTime.ConvertToDateTime(strToDt, "dd/MM/yyyy"));

                SqlParameter min = new SqlParameter();
                min.ParameterName = "@pMinDaysLeave";
                min.Direction = ParameterDirection.Output;
                min.Size = 10;
                cmdLvRule.Parameters.Add(min);

                SqlParameter max = new SqlParameter();
                max.ParameterName = "@pMaxDaysLeave";
                max.Direction = ParameterDirection.Output;
                max.Size = 10;
                cmdLvRule.Parameters.Add(max);

                cmdLvRule.CommandTimeout = 0;

                SqlDataAdapter daLvRule = new SqlDataAdapter(cmdLvRule);
                DataTable dtLvRule = new DataTable();
                daLvRule.Fill(dtLvRule);

                string updatedfromDate = dtLvRule.Rows[0]["pleaveDate"].ToString();
                string updatedToDate = dtLvRule.Rows[dtLvRule.Rows.Count - 1]["pleaveDate"].ToString();

                string outMinDays = cmdLvRule.Parameters["@pMinDaysLeave"].Value.ToString();

                string outMaxDays = cmdLvRule.Parameters["@pMaxDaysLeave"].Value.ToString();

                float lvCount = 0;
                if (dtLvRule.Rows.Count > 0)
                {
                    lvCount = dtLvRule.Rows.Count;

                    if (rbtLeaveType1.SelectedValue == "H")
                    {
                        lvCount = lvCount / 2;
                    }

                }
                if (outMinDays == "")
                {
                    outMinDays = "0";
                }
                if (outMaxDays == "")
                {
                    outMaxDays = "0";
                }



                if (Convert.ToDouble(outMinDays) > LvDays)
                {

                    errorLabelLV.Text = "Please apply minimum " + outMinDays.ToString() + " as per HR policies";
                    errorLabelLV.Visible = true;
                    return;
                }
                if (Convert.ToDouble(outMaxDays) != 0)
                {
                    if (Convert.ToDouble(outMaxDays) < LvDays)
                    {

                        errorLabelLV.Text = "Leave days requested should be less than or equal to " + outMaxDays.ToString() + " as per HR policies";
                        errorLabelLV.Visible = true;
                        return;
                    }
                }
                if (LvDays < lvCount)
                {
                    txtFrmDate.Text = Convert.ToDateTime(updatedfromDate.Remove(10)).ToString("dd/MM/yyyy");
                    txtToDate.Text = Convert.ToDateTime(updatedToDate.Remove(10)).ToString("dd/MM/yyyy");
                    //adeed on 2-7-2015 by vaibhav
                    strFrmDt = Convert.ToDateTime(updatedfromDate.Remove(10)).ToString("dd/MM/yyyy");
                    strToDt = Convert.ToDateTime(updatedToDate.Remove(10)).ToString("dd/MM/yyyy");

                    errorLabelLV.Text = "The leave dates and count has been changed as per HR policies";
                    errorLabelLV.Visible = true;
                    return;
                }


                float FLeaveDays = rbtLeaveType1.SelectedValue == "F" ? LvDays : 0.5f;
                objcmd = new SqlCommand("Sp_Insert_Leave_application", conn);
                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.Parameters.AddWithValue("@flag", 'F');
                objcmd.Parameters.AddWithValue("@empid", empid);
                //objcmd.Parameters.AddWithValue("@requestDate", strRequestDt);
                //objcmd.Parameters.AddWithValue("@fromDate", strFrmDt);
                //objcmd.Parameters.AddWithValue("@toDate", strToDt);

                objcmd.Parameters.AddWithValue("@fromDate",  CMSDateTime.CMSDateTime.ConvertToDateTime(strFrmDt,"dd/MM/yyyy").ToString("MM/dd/yyyy"));
                objcmd.Parameters.AddWithValue("@toDate", CMSDateTime.CMSDateTime.ConvertToDateTime(strToDt, "dd/MM/yyyy").ToString("MM/dd/yyyy"));

                objcmd.Parameters.AddWithValue("@leaveType", ddleaveType.SelectedValue);
                objcmd.Parameters.AddWithValue("@ReasonType", ddlReasonTypeLVReq.SelectedValue);
                objcmd.Parameters.AddWithValue("@remarks", TextBox3.Text);
                objcmd.Parameters.AddWithValue("@lvdays", FLeaveDays);
                objcmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                errorLabelLV.Visible = true;
                errorLabelLV.Text = "Record Saved Successfully";
                SetMailId();
                string cc = "";
                string subject = "Leave Application";

                string message = "I am applying " + ddleaveType.SelectedValue + " leave from " + strFrmDt + " to " + strToDt + System.Environment.NewLine + "";
                message = message + "Reason: " + TextBox3.Text + System.Environment.NewLine + "";
                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";

                Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                thread.Start();

                mpeNewReq.Hide();
                PnlLvReq.Visible = false;
                lblMsg.Text = "Request applied successfully.";
                mpeReportingMgr.Show();

            }

            catch (Exception ex)
            {
                Response.Redirect("ESS_Dashboard.aspx");
            }

        }
        public void InsertODApplication()
        {
            strRequestDt = DateTime.Now.ToString("dd/MM/yyyy");
            strFrmDt = txtFrmDate.Text;
            strToDt = txtToDate.Text;

            if (strToDt == "")
            {
                strToDt = strFrmDt;
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            try
            {

                //Added by Pooja Yadav
                clsESSDashboardObjects objDashboard = null;
                new clsESSDashboardObjects().getAllRequest(ref objDashboard, strFrmDt, strToDt, empid);
                if (objDashboard.IsShiftCreated)
                {
                    erroLabelOutDoor.Text = "Shift not created";
                    erroLabelOutDoor.Visible = true;
                    return;
                }

                if (objDashboard.IsOutdoor)
                {
                    erroLabelOutDoor.Text = "OD already applied for the date range";
                    erroLabelOutDoor.Visible = true;
                    return;
                }

                if (objDashboard.IsLeaveApplied)
                {
                    erroLabelOutDoor.Text = "Leave already applied for the date range";
                    erroLabelOutDoor.Visible = true;
                    return;
                }


                if (objDashboard.IsOutPass)
                {
                    erroLabelOutDoor.Text = "Outpass already applied for the date range";
                    erroLabelOutDoor.Visible = true;
                    return;
                }

                if (objDashboard.IsCompoff)
                {
                    erroLabelOutDoor.Text = "Comp-off already applied for the date range";
                    erroLabelOutDoor.Visible = true;
                    return;
                }

                if (objDashboard.IsManualAtt)
                {
                    erroLabelOutDoor.Text = "Manual Attendance already applied for the date range";
                    erroLabelOutDoor.Visible = true;
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                objcmd.CommandText = "Insert into ESS_TA_OD(ESS_OD_EMPID,ESS_OD_REQUESTDT,ESS_OD_FROMDT,ESS_OD_TODT,ESS_OD_ODCD, " +
                             "  ESS_OD_RSNID, ESS_OD_REMARK,ESS_OD_ORDER,ESS_OD_STATUS,ESS_OD_ISDELETED) " +
                               " values ('" + empid + "',Convert(datetime,'" + strRequestDt + "',103), convert(datetime,'" + strFrmDt + "',103) ,Convert(datetime,'" +
                               strToDt + "',103), 'OD' " +
                               " ,'" + ddlReasonOD.SelectedValue + "', " +
                                "'" + txtAdditionalInfoOD.Text + "','1','N','0')";

                int i = objcmd.ExecuteNonQuery();

                if (i > 0)
                {
                    erroLabelOutDoor.Text = "Record Saved Successfully";

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    SetMailId();
                    string cc = "";
                    string subject = "Outdoor Application";

                    string message = "I am applying OD from " + strFrmDt + " to " + strToDt + System.Environment.NewLine + "";
                    message = message + "Reason: " + txt_Remarks.Text + System.Environment.NewLine + "";


                    message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                    message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + Session["loginName"].ToString() + "";
                    Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                    thread.Start();

                    mpeNewReq.Hide();
                    DropDownList1.SelectedIndex = 0;
                    pnlODreq.Visible = false;
                    lblMsg.Text = "Request applied successfully.";
                    mpeReportingMgr.Show();
                }
                else
                {
                    erroLabelOutDoor.Text = "Record not saved";
                }






            }

            catch (Exception ex)
            {
                //  this.messageDiv.InnerHtml = ex.Message;
            }

        }
        public void InsertGatePass()
        {
            string strFromTime = string.Empty, strTotime = string.Empty;
            strFromTime = txtInOuP.Text.Trim() == "" ? "" : "(" + txtInOuP.Text.Trim() + ")";
            strTotime = txtInTimeOutP.Text.Trim() == "" ? "" : "(" + txtInTimeOutP.Text.Trim() + ")";
            strRequestDt = DateTime.Now.ToString("dd/MM/yyyy");

            strFrmDt = txtFrmDate.Text;
            strToDt = txtToDate.Text;
            if (strToDt == "")
            {
                strToDt = strFrmDt;
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;



            try
            {
                //Added by Pooja Yadav               

                SqlCommand cmd = new SqlCommand("exec USP_DashBoard @strCommand='CheckOutPass',@FromDate='" + strFrmDt + "',@Todate='" + strToDt + "',@FromTime='" + txtInOuP.Text + "',@ToTime='" + txtInTimeOutP.Text + "',@userId='" + empid + "'", conn);
                string strResult = Convert.ToString(cmd.ExecuteScalar());

                if (strResult != "" && strResult != null)
                {
                    errorLabelOutPass.Text = "Outpass already applied for the date and time";
                    return;
                }

                clsESSDashboardObjects objDashboard = null;
                new clsESSDashboardObjects().getAllRequest(ref objDashboard, strFrmDt, strToDt, empid);

                if (objDashboard.IsLeaveApplied)
                {
                    errorLabelOutPass.Text = "Leave already applied for the date range";
                    errorLabelOutPass.Visible = true;
                    return;
                }

                if (objDashboard.IsOutdoor)
                {
                    errorLabelOutPass.Text = "OD already applied for the date range";
                    errorLabelOutPass.Visible = true;
                    return;
                }

                if (objDashboard.IsCompoff)
                {
                    errorLabelOutPass.Text = "Comp-off already applied for the date range";
                    errorLabelOutPass.Visible = true;
                    return;
                }

                objcmd.CommandText = "Insert into ESS_TA_GP(ESS_GP_EMPID,ESS_GP_REQUESTDT,ESS_GP_FROMDT,ESS_GP_FROMTM,ESS_GP_TODT,ESS_GP_TOTM, " +
                             "  ESS_GP_RSNID, ESS_GP_REMARK,ESS_GP_ORDER,ESS_GP_STATUS,ESS_GP_ISDELETED) " +
                               " values ('" + empid + "',Convert(datetime,'" + strRequestDt + "',103), convert(datetime,'" + strFrmDt + "',103) ,Convert(varchar(8),'" +
                               txtInOuP.Text + "',103), " +
                               " convert(datetime,'" + strToDt + "',103), Convert(varchar(8),'" + txtInTimeOutP.Text + "',103),'" + ddlResonOutPaas.SelectedValue + "', " +
                                "'" + txtAdditionInfoOup.Text + "','1','N','0')";

                int i = objcmd.ExecuteNonQuery();
                if (i > 0)
                {
                    btnSaveOutpass.Enabled = false;
                    errorLabelOutPass.Text = "Record Saved Successfully";
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    SetMailId();
                    string cc = "";
                    string subject = "Official Out-Pass Application";
                    string message = "I am applying Official Out-Pass for " + strFrmDt + strFromTime + " to " + strToDt + strTotime + System.Environment.NewLine + "";
                    message = message + "Reason: " + txt_Remarks.Text + System.Environment.NewLine + "";

                    message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                    message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + Session["loginName"].ToString() + "";
                    Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                    thread.Start();
                    mpeNewReq.Hide();
                    DropDownList1.SelectedIndex = 0;
                    pnlOutPass.Visible = false;
                    // Response.Redirect("ESS_Dashboard.aspx");
                    lblMsg.Visible = true;
                    btnSaveOutpass.Enabled = true;
                    lblMsg.Text = "Request applied successfully.";
                    mpeReportingMgr.Show();
                }
                else
                {
                    errorLabelOutPass.Text = "Record Not Saved";
                }


            }



            catch (Exception ex)
            {
                // this.messageDiv.InnerHtml = ex.Message;
            }

        }
        private void Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID()
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int _result = 0;

            SqlCommand cmd = new SqlCommand("sp_GetComOffDateList_BYEMPID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", empid);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            gvPopUp.DataSource = dt1;
            gvPopUp.DataBind();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }


        }
        public string FindEmailID()
        {
            string strEmpFromAddress = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;


            objcmd.CommandText = "select epd_email from ENT_EMPLOYEE_PERSONAL_DTLS where epd_empid= '" + empid + "'";
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
            return strEmpFromAddress;
        }
        public string FindMgrMailid()
        {
            string strEmpToAddress = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objMgrcmd = new SqlCommand();
            objMgrcmd.Connection = conn;
            objMgrcmd.CommandText = "select ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMAIL from ENT_HierarchyDef,ENT_EMPLOYEE_PERSONAL_DTLS " +
                          " where ENT_HierarchyDef.Hier_Mgr_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and ENT_HierarchyDef.Hier_Emp_ID='" + empid + "'";
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
            return strEmpToAddress;
        }
        protected void gvPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
        protected void btnComOffReq_Click(object sender, EventArgs e)
        {
            InsertCompoff();
            pnlComOffReq.Attributes.Add("style", "display:block");
        }

        private void InsertCompoff()
        {
            try
            {
                WriteLog("1 txtFrmDate.Text:-" + txtFrmDate.Text);
                WriteLog("2 txtToDate.Text:-" + txtToDate.Text);   
                bool isSaved = false;
                //int fromdate = DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null).DayOfYear;
                //int toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).DayOfYear;
                //string from = DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null).ToString();
                //string to = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString();

                int fromdate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy").DayOfYear;
                int toDate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy").DayOfYear;

                string from = txtFrmDate.Text;
                string to = txtToDate.Text;
                WriteLog("3 From Date:-" + from);
                WriteLog("4 to Date:-" + to);

                DateTime frDt, Todt;

                //frDt = Convert.ToDateTime(from);
                //Todt = Convert.ToDateTime(to);

                frDt = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy");
                Todt = CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy");

                int a = (int)frDt.DayOfWeek;

                DataRow[] daystatus = MonthlyAttendance.Select("Tday_Date = '" + frDt + "'");
                WriteLog(" After Filter ");

                string status = "";

                if (daystatus.Length > 0)
                {
                    status = daystatus[0]["TDAY_STATUS"].ToString();
                }

                //Added by Pooja Yadav
                clsESSDashboardObjects objDashboard = null;
                new clsESSDashboardObjects().getAllRequest(ref objDashboard, strFrmDt, strToDt, empid);

                if (objDashboard.IsCompoff)
                {
                    errorLableCompOff.Text = "Comp-off already applied for the date range";
                    errorLableCompOff.Visible = true;
                    return;
                }

                if (objDashboard.IsManualAtt)
                {
                    errorLableCompOff.Text = "Manual Attendance already applied for the date range";
                    errorLableCompOff.Visible = true;
                    return;
                }

                if (objDashboard.IsLeaveApplied)
                {
                    errorLableCompOff.Text = "Leave already applied for the date range";
                    errorLableCompOff.Visible = true;
                    return;
                }

                if (objDashboard.IsOutdoor)
                {
                    errorLableCompOff.Text = "OD already applied for the date range";
                    errorLableCompOff.Visible = true;
                    return;
                }
                if (objDashboard.IsOutPass)
                {
                    errorLableCompOff.Text = "Outpass already applied for the date range";
                    errorLableCompOff.Visible = true;
                    return;
                }


                WriteLog("Before Validation ");
                if (ValidateOnSubmit())
                {
                   // ArrayList arrlist = new ArrayList();

                    int difference = 0;
                    int j = 0;
                    // difference = fromdate == toDate ? 1 : (toDate - fromdate) + 1;

                    //WriteLog(fromdate + "|" + toDate);
                    if (fromdate == toDate)
                    {
                        difference = 1;
                    }
                    else
                    {
                        difference = (toDate - fromdate) + 1;
                    }

                    DateTime[] arrlistDate = new DateTime[difference];

                    for (int i = 0; i < difference; i++)
                    {
                        // arrlist.Add(DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null).Date.AddDays(i).ToString("dd/MM/yyyy"));
                       // arrlist.Add(CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy").Date.AddDays(i));
                        arrlistDate[i] = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy").Date.AddDays(i);
                    }

                    WriteLog("Before Foreach");
                    foreach (GridViewRow gr in gvPopUp.Rows)
                    {
                        int rowIndex = gr.RowIndex;
                        CheckBox SaveRows = (CheckBox)gvPopUp.Rows[rowIndex].FindControl("SaveRows");
                        Label lblRowID = (Label)gvPopUp.Rows[rowIndex].FindControl("lblRowID");
                        Label lblFromDT = (Label)gvPopUp.Rows[rowIndex].FindControl("lblFromDT");

                        if (SaveRows.Checked == true)
                        {
                            WriteLog("Came in  At Point 1");
                            WriteLog(" arrlist[j].ToString():-" + arrlistDate[j].ToString());
                            //DateTime CO_fromdate = DateTime.ParseExact(arrlist[j].ToString(), "dd/MM/yyyy", null);
                            //string leaveDate = DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null).ToString();

                            //DateTime CO_fromdate = CMSDateTime.CMSDateTime.ConvertToDateTime(arrlist[j].ToString(), "dd-MM-yyyy");
                            DateTime CO_fromdate = arrlistDate[j];                    
                            string leaveDate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy").ToString("dd/MM/yyyy");

                            

                            WriteLog("lblFromDT.Text:-" + lblFromDT.Text);
                            leaveDate = lblFromDT.Text;
                            WriteLog("leaveDate:-" + leaveDate);

                            //DateTime LeaveAgainstDate = DateTime.ParseExact(leaveDate, "dd/MM/yyyy", null);
                            DateTime LeaveAgainstDate = CMSDateTime.CMSDateTime.ConvertToDateTime(leaveDate, "dd/MM/yyyy");

                            try
                            {
                                int rowID = 0;
                                WriteLog(" Goint to SAVE_ESS_TA_CO()");    
                                SAVE_ESS_TA_CO(CO_fromdate, LeaveAgainstDate, rowID);
                                   
                                j = j + 1;
                                isSaved = true;
                                WriteLog("CO Saved Successfully."); 
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("UK_ESS_TA_CO_ESS_CO_FROMDT"))
                                {
                                    errorLableCompOff.Text = "Already applied on " + "  -  " + CO_fromdate.ToString("dd/MM/yyyy");
                                    return;
                                }
                                else
                                {
                                    UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
                                }

                            }

                        }


                    }

                }


                if (isSaved)
                {
                    //ResetAll();

                    WriteLog("Final Pre Output FRom Date :-" + from);
                    WriteLog("Final Pre Output To Date :-" + to);

                    //errorLableCompOff.Text = "Comp-Off Saved Successfully from" + "    " + from + "  to  " + to; ;


                    WriteLog("Final Post Output FRom Date :-" + from);
                    WriteLog("Final Post Output To Date :-" + to);

                    WriteLog("Final Output");
                    SetMailId();
                    string cc = "";
                    string subject = " COMP-OFF Application";
                    string message = "I had taken COMP - OFF on " + txtFrmDate.Text + System.Environment.NewLine + "";
                    message = message + "Reason: " + txtRemarkCoOff.Text + System.Environment.NewLine + "";

                    message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                    message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                    message = message + Session["loginName"].ToString() + "";
                    Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                    thread.Start();

                    lblMsg.Text = "Comp-Off Saved Successfully";
                    //lblMsg.Text = errorLableCompOff.Text;
                    mpeReportingMgr.Show();
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "HidePopUp", "Showalert()", true);
                    //Response.Redirect("ESS_Dashboard.aspx");

                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CO");
            }
        }

        private bool ValidateOnSubmit()
        {


            //int fromdate = DateTime.ParseExact(txtFrmDate.Text, "dd/MM/yyyy", null).DayOfYear;
            //int todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).DayOfYear;

            int fromdate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtFrmDate.Text, "dd/MM/yyyy").DayOfYear;
            int todate = CMSDateTime.CMSDateTime.ConvertToDateTime(txtToDate.Text, "dd/MM/yyyy").DayOfYear;

            int DaysSelected = 0;
            int chkcount = 0;

            if (fromdate == todate)
            {
                DaysSelected = 1;
            }
            else
            {
                DaysSelected = (todate - fromdate) + 1;
            }
            if (gvPopUp.Rows.Count > 0)
            {
                foreach (GridViewRow dgvRow in gvPopUp.Rows)
                {
                    CheckBox SaveRows = (CheckBox)dgvRow.FindControl("SaveRows");

                    if (SaveRows.Checked == true)
                    {
                        chkcount = chkcount + 1;
                    }
                }

                if (chkcount > DaysSelected)
                {
                    errorLableCompOff.Visible = true;
                    errorLableCompOff.Text = "Days checked is more than the days selected";
                    return false;
                }

                if (chkcount < DaysSelected)
                {
                    errorLableCompOff.Visible = true;
                    errorLableCompOff.Text = "Days checked is less than the days selected";
                    return false;
                }
            }
            else
            {
                errorLableCompOff.Text = "Com-Off Not Found";

            }

            return true;
        }
        private void SAVE_ESS_TA_CO(DateTime fromdate, DateTime LeaveAganistDate, int rowID)
        {

            try 
            {
            
           

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }


            Decimal NoOfdays = 1;

            string Reason;
            string Remark;


            Reason = ddlReasonComOff.SelectedItem.Value;
            Remark = txtRemarkCoOff.Text;

           // string fm_date = fromdate.ToString();

            SqlCommand cmd = new SqlCommand("PROC_SAVE_ESS_TA_CO_ESS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ESS_CO_RowID", rowID);
            cmd.Parameters.AddWithValue("@ESS_CO_EMPID", empid);
            WriteLog("Proc Pre Save :-" + fromdate);
            cmd.Parameters.AddWithValue("@ESS_CO_FROMDT", fromdate);

            //cmd.Parameters.AddWithValue("@ESS_CO_FROMDT", CMSDateTime.CMSDateTime.ConvertToDateTime(fm_date, "dd/MM/yyyy"));
            WriteLog("Proc Post Save :-" + fromdate);

            // cmd.Parameters.AddWithValue("@ESS_CO_TODT", fromdate);
            cmd.Parameters.AddWithValue("@ESS_CO_CD", "CO");
            cmd.Parameters.AddWithValue("@ESS_CO_RSNID", Reason);
            cmd.Parameters.AddWithValue("@ESS_CO_REMARK", Remark);
            cmd.Parameters.AddWithValue("@ESS_CO_SANCID", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_SANCDT", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_SANC_REMARK", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_ORDER", DBNull.Value);
            cmd.Parameters.AddWithValue("@ESS_CO_STATUS", "N");
            cmd.Parameters.AddWithValue("@ESS_CO_OLDSTATUS", string.Empty);
            cmd.Parameters.AddWithValue("@ESS_CO_ISDELETED", 0);
            cmd.Parameters.AddWithValue("@ESS_CO_DELETEDDATE", DBNull.Value);
            cmd.Parameters.AddWithValue("@ESS_CO_DAYS", NoOfdays);
            cmd.Parameters.AddWithValue("@ESS_CO_REASON", Reason);
            cmd.Parameters.AddWithValue("@ESS_CO_LEAVEAGANISTDATE", LeaveAganistDate);

            cmd.ExecuteNonQuery();
         

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                WriteLog("Proc Save Procedure");
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
            }

        }
        
        
        protected void btnSaveOutpass_Click(object sender, EventArgs e)
        {
            InsertGatePass();
            pnlOutPass.Attributes.Add("style", "display:block");
        }
        public void fillMonthyAttendenceAndPendingRequests(int month, int year)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }
                string MinInTime = System.Configuration.ConfigurationManager.AppSettings["MinInTime"].ToString();
                string MaxInTime = System.Configuration.ConfigurationManager.AppSettings["MaxInTime"].ToString();
                string MinOutTime = System.Configuration.ConfigurationManager.AppSettings["MinOutTime"].ToString();
                string MaxOutTime = System.Configuration.ConfigurationManager.AppSettings["MaxOutTime"].ToString();
                SqlDataAdapter da = new SqlDataAdapter("USP_DashBoard @strCommand='GetMonthyAttendence', @userId='" + Convert.ToString(Session["uid"]) + "',@MinInTime='" + MinInTime + "',@MaxInTime='" + MaxInTime + "',@MinOutTime='" + MinOutTime + "',@MaxOutTime='" + MaxOutTime + "',@Month='" + month + "',@Year='" + year + "'", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {

                    lblPrDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["PresentDays"]);
                    lblAbDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthAbsentDays"]);
                    lblOutDoorDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthOutDoorDays"]);
                    lblOutPassDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthOutPassDays"]);
                    lblExtraHours.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthExtraHDays"]);
                    lblLWP.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthLWPDays"]);
                    lblLateComing.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthLateComingDays"]);

                    int lv, ma, od, op, co;
                    lv = Convert.ToInt32(ds.Tables[1].Rows[0]["LeaveCount"]);
                    ma = Convert.ToInt32(ds.Tables[1].Rows[0]["ManualCount"]);
                    od = Convert.ToInt32(ds.Tables[1].Rows[0]["OdCount"]);
                    op = Convert.ToInt32(ds.Tables[1].Rows[0]["GpCount"]);
                    co = Convert.ToInt32(ds.Tables[1].Rows[0]["ComOffCount"]);
                    lblLVReq.Text = Convert.ToString(lv);
                    lblMAReq.Text = Convert.ToString(ma);
                    lblODreq.Text = Convert.ToString(od);
                    lblOPReq.Text = Convert.ToString(op);
                    lblCoReq.Text = Convert.ToString(co);
                    if (lv < 1) { lblLVReqtxt.Text = "Leave"; }
                    if (ma < 1) { lblMAReqtxt.Text = "Manual"; }
                    if (od < 1) { lblODreqtxt.Text = "Outdoor"; }
                    if (op < 1) { lblOPReqtxt.Text = "Outpass"; }
                    if (co < 1) { lblCoReqtxt.Text = "Com-Off"; }

                    int ait = 0, agt = 0, al = 0, ab = 0, dbt = 0;
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        ait = Convert.ToInt32(ds.Tables[2].Rows[0]["ait"]);
                        agt = Convert.ToInt32(ds.Tables[2].Rows[0]["agt"]);
                        al = Convert.ToInt32(ds.Tables[2].Rows[0]["al"]);
                        ab = Convert.ToInt32(ds.Tables[2].Rows[0]["ab"]);
                        dbt = Convert.ToInt32(ds.Tables[2].Rows[0]["dbt"]);

                        lblArrivalInTime.Text = Convert.ToString(ait);
                        lblArrivalInGraceTime.Text = Convert.ToString(agt);
                        lblArrivalLate.Text = Convert.ToString(al);
                        lblAbsent.Text = Convert.ToString(ab);
                        lblDepartureBeforeTime.Text = Convert.ToString(dbt);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void fillLeaveBalance()
        {
            SqlCommand cmd = new SqlCommand("ESS_filllvBalance", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public void getFillProfile()
        {
            SqlCommand cmdGetMyProfileData = new SqlCommand("GedtMyProfileDetails", conn);
            cmdGetMyProfileData.CommandType = CommandType.StoredProcedure;
            cmdGetMyProfileData.Parameters.AddWithValue("@empcode", empid);
            DataTable dtEmpInfo = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmdGetMyProfileData);
            dap.Fill(dtEmpInfo);

            if (dtEmpInfo.Rows.Count > 0)
            {
                lblEmpID.Text = dtEmpInfo.Rows[0]["employee_id"].ToString();
                lblEmpName.Text = dtEmpInfo.Rows[0]["EmployeeName"].ToString();
                lblDesignation.Text = dtEmpInfo.Rows[0]["EmpDesignation"].ToString();
                lblLocation.Text = dtEmpInfo.Rows[0]["EmployeeLocation"].ToString();
                lblMailID.Text = dtEmpInfo.Rows[0]["empMailID"].ToString();
                lblR1.Text = dtEmpInfo.Rows[0]["ManagerName"].ToString();
                lblDept.Text = dtEmpInfo.Rows[0]["EmployeeDept"].ToString();
                //lblManager.Text = dtEmpInfo.Rows[0]["ManagerName"].ToString();
            }
        }

        protected void Calendar1_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Calendar1.SelectedDate.Month != 1 && Calendar1.SelectedDate.Year != 1)
                {
                    GetMonthlyAttendance(Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Year);
                }
                else
                {
                    GetMonthlyAttendance(DateTime.Now.Month, DateTime.Now.Year);
                }
            }
        }

        private void BindLeaveInfo()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT LV_LEAVE_ID as Leave, LV_AVAILABLE as Available FROM TA_LEAVE_SUMMARY WHERE LV_EMP_ID = '" + Session["uid"].ToString() + "'", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //gvLeaveInfo.DataSource = dt;
                //gvLeaveInfo.DataBind();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {

            //vaibhav 
            DateTime start = e.NewDate;
            GetMonthlyAttendance(e.NewDate.Month, e.NewDate.Year);
            fillMonthyAttendenceAndPendingRequests(e.NewDate.Month, e.NewDate.Year);
            //try
            //{
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();

            //    }
            //    string MinInTime = System.Configuration.ConfigurationManager.AppSettings["MinInTime"].ToString();
            //    string MaxInTime = System.Configuration.ConfigurationManager.AppSettings["MaxInTime"].ToString();
            //    string MinOutTime = System.Configuration.ConfigurationManager.AppSettings["MinOutTime"].ToString();
            //    string MaxOutTime = System.Configuration.ConfigurationManager.AppSettings["MaxOutTime"].ToString();
            //    SqlDataAdapter da = new SqlDataAdapter("USP_DashBoard @strCommand='GetMonthyAttendence', @userId='" + Convert.ToString(Session["uid"]) + "',@MinInTime='" + MinInTime + "',@MaxInTime='" + MaxInTime + "',@MinOutTime='" + MinOutTime + "',@MaxOutTime='" + MaxOutTime + "',@cuurentMonth='" + e.NewDate + "'", conn);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    if (ds.Tables.Count > 0)
            //    {

            //        lblPrDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["PresentDays"]);
            //        lblAbDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthAbsentDays"]);
            //        lblOutDoorDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthOutDoorDays"]);
            //        lblOutPassDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthOutPassDays"]);
            //        lblExtraHours.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthExtraHDays"]);
            //        lblLWP.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthLWPDays"]);
            //        lblLateComing.Text = Convert.ToString(ds.Tables[0].Rows[0]["MonthLateComingDays"]);

            //        int lv, ma, od, op, co;
            //        lv = Convert.ToInt32(ds.Tables[1].Rows[0]["LeaveCount"]);
            //        ma = Convert.ToInt32(ds.Tables[1].Rows[0]["ManualCount"]);
            //        od = Convert.ToInt32(ds.Tables[1].Rows[0]["OdCount"]);
            //        op = Convert.ToInt32(ds.Tables[1].Rows[0]["GpCount"]);
            //        co = Convert.ToInt32(ds.Tables[1].Rows[0]["ComOffCount"]);
            //        lblLVReq.Text = Convert.ToString(lv);
            //        lblMAReq.Text = Convert.ToString(ma);
            //        lblODreq.Text = Convert.ToString(od);
            //        lblOPReq.Text = Convert.ToString(op);
            //        lblCoReq.Text = Convert.ToString(co);
            //        if (lv < 1) { lblLVReqtxt.Text = "Leave Request"; }
            //        if (ma < 1) { lblMAReqtxt.Text = "Manual Request"; }
            //        if (od < 1) { lblODreqtxt.Text = "Outdoor Request"; }
            //        if (op < 1) { lblOPReqtxt.Text = "Outpass Request"; }
            //        if (co < 1) { lblCoReqtxt.Text = "Com-Off Request"; }

            //    }

            //}
            //catch (Exception ex)
            //{


            //}
            //vaibhav end
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ESS_PendingRequest.aspx");
        }

        protected void btnreqAwat_Click(object sender, EventArgs e)
        {
            Response.Redirect("ESS_Sanc_View.aspx");
        }

        #endregion vaibhav
        //Added by shraddha
        #region Mail
        private void SetMailId()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("USP_DashBoard @strCommand='getEmailId', @userId='" + Convert.ToString(Session["uid"]) + "'", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    UserMailId = Convert.ToString(ds.Tables[0].Rows[0]["epd_email"]);
                    managerMail_id = Convert.ToString(ds.Tables[1].Rows[0]["EPD_EMAIL"]);
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
        #endregion

        protected void gvPopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPopUp.PageIndex = e.NewPageIndex;
                Get_PROC_GETLVDETAILS_ELIGIBLE_FOR_CO_BYEMPID();

            }
            catch (Exception ex)
            {

            }
        }

        public void fillgridHoliday()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PROC_GET_OPTIONAL_HOLIDAY";
            cmd.Parameters.AddWithValue("@cmd", 1);
            cmd.Parameters.AddWithValue("@empid", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvHolidayList.DataSource = ds.Tables[0];
                gvHolidayList.DataBind();
                btnSAveHoliday.Visible = true;
            }
            else
                btnSAveHoliday.Visible = false;
        }

        public void insertOptHoliday()
        {
            int ctr = 0;
            CheckBox selectChk;
            string date = "";
            string cc = "";
            string subject = "";
            string message = "";
            HiddenField hdnHolidayId;
            try
            {
                foreach (GridViewRow gr in gvHolidayList.Rows)
                {
                    selectChk = (CheckBox)gr.FindControl("selectChk");
                    hdnHolidayId = (HiddenField)gr.FindControl("hdnHolidayId");
                    if (selectChk.Checked == true)
                    {
                        ctr = ctr + 1;
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn;
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "PROC_GET_OPTIONAL_HOLIDAY";
                        cmd1.Parameters.AddWithValue("@cmd", 2);
                        cmd1.Parameters.AddWithValue("@empid", Session["uid"].ToString());
                        cmd1.Parameters.AddWithValue("@holidayid", hdnHolidayId.Value);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            lblHolidaymsg.Text = "You have already applied for this holiday ";
                            return;
                        }
                    }
                    if (ctr > 2)
                    {
                        lblHolidaymsg.Text = "You can apply for only two optional holidays";
                        return;
                    }
                }
                if (ctr == 0)
                {
                    lblHolidaymsg.Text = "Please select holiday.";
                    return;
                }

                SqlDataAdapter da = new SqlDataAdapter(" exec PROC_INSERT_HOLIDAY @cmd=4, @empid='" + Session["uid"].ToString() + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ctr = ctr + Convert.ToInt16(dt.Rows[0][0]);
                    if (ctr > 2)
                    {
                        lblHolidaymsg.Text = "You have applied for " + dt.Rows[0][0].ToString() + " optional holiday.";
                        return;
                    }
                }



                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                foreach (GridViewRow gr in gvHolidayList.Rows)
                {
                    selectChk = (CheckBox)gr.FindControl("selectChk");
                    hdnHolidayId = (HiddenField)gr.FindControl("hdnHolidayId");

                    if (selectChk.Checked == true)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PROC_INSERT_HOLIDAY";
                        cmd.Parameters.AddWithValue("@cmd", 1);
                        cmd.Parameters.AddWithValue("@empid", Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@holidayid", hdnHolidayId.Value);
                        cmd.Parameters.AddWithValue("@holidayDate", gr.Cells[2].Text.ToString());
                        cmd.ExecuteNonQuery();
                        date = gr.Cells[2].Text.ToString();
                    }
                }

                conn.Close();

                SetMailId();

                subject = "Optional Holiday Application";

                message = "I am applying for holiday  on " + date + ". " + System.Environment.NewLine + "";

                message = message + " I would request you to kindly approve the same." + System.Environment.NewLine + "";
                message = message + "UNO Login: http://uno.cms.co.in/uno/login.aspx " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + "Regards, " + System.Environment.NewLine + System.Environment.NewLine + "";
                message = message + Session["loginName"].ToString() + "";
                Thread thread = new Thread(() => Mail.SendMail(UserMailId, managerMail_id, cc, subject, message));
                thread.Start();
                mpeNewReq.Hide();
                lblHolidaymsg.Text = "Request saved successfully";
                lblMsg.Text = lblHolidaymsg.Text;
                mpeReportingMgr.Show();
            }
            catch (Exception e)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                lblHolidaymsg.Text = "Exception Raised";
            }
        }

        protected void btnSAveHoliday_Click(object sender, EventArgs e)
        {
            insertOptHoliday();           
            pnlOptHoliday.Attributes.Add("style", "display:block");
        }

        private void WriteLog(string strMessage)
        {
            string strPath = "C:\\logs"; //@"D:\cms\UNO-LIVE\LOG\LeaveLog.txt";
            bool exists = System.IO.Directory.Exists(strPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(strPath);
            System.IO.StreamWriter SW = null;
            try
            {
                SW = new System.IO.StreamWriter(strPath + "\\LogFile.txt", true);
                SW.WriteLine(DateTime.Now.ToString("ddMMyyyy HHmmss") + " " + strMessage);

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
            finally
            {
                SW.Close();
            }
        }

    }
    public class clsESSDashboardObjects
    {
        public bool IsManualAtt = false;
        public bool IsLeaveApplied = false;
        public bool IsOutPass = false;
        public bool IsCompoff = false;
        public bool IsOutdoor = false;
        public bool IsShiftCreated = false;

        public void getAllRequest(ref clsESSDashboardObjects obj, string strFrmDt, string strToDt, string empid)
        {
            SqlConnection conn;
            DataTable dtStatus;
            SqlDataAdapter da;
            obj = new clsESSDashboardObjects();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                dtStatus = new DataTable();
                da = new SqlDataAdapter("USP_DashBoard @strCommand='chkForAllRequest',@FromDate='" + strFrmDt + "',@Todate='" + strToDt + "',@userId='" + empid + "'", conn);
                da.Fill(dtStatus);
                obj.IsManualAtt = Convert.ToBoolean(dtStatus.Rows[0]["IsManualAtt"]);
                obj.IsLeaveApplied = Convert.ToBoolean(dtStatus.Rows[0]["IsLeave"]);
                obj.IsOutPass = Convert.ToBoolean(dtStatus.Rows[0]["IsOutPass"]);
                obj.IsCompoff = Convert.ToBoolean(dtStatus.Rows[0]["IsCompoff"]);
                obj.IsOutdoor = Convert.ToBoolean(dtStatus.Rows[0]["IsOutdoor"]);
                obj.IsShiftCreated = Convert.ToBoolean(dtStatus.Rows[0]["IsShiftCreated"]);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESSTACO");
            }
        }
    }
}
