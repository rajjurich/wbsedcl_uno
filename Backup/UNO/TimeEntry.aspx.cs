using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UNO
{
    public partial class TimeEntry : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        DataTable TotalActivities = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null || Session["uid"].ToString() == "")
            {
                Response.Redirect("Login.aspx", true);
            }
            if (!IsPostBack)
            {


                DateTime dt = DateTime.Today;
                DateTime StartDate = dt.AddDays(-((int)dt.DayOfWeek));
                DateTime EndDate = dt.AddDays(6 - (int)dt.DayOfWeek);

                for (int i = 0; i < 7; i++)
                {
                    calOverview.SelectedDates.Add(StartDate.AddDays(i));

                }
                GetTimeGridData();
                //GetEntireGridData();
                GetActivityEntries();
                GetTotalActivities();
                //GetTotalActivities();
                //gvEntry.DataSource = TotalActivities;

                //GetTimeSheetEntriesData();
                gvEntry.DataBind();

                //DataTable gvdata = (DataTable)gvEntry.DataSource;
                //ViewState["testData"] = gvdata;
            }
        }
        protected void calOverview_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dt = calOverview.SelectedDate;
            DateTime StartDate = dt.AddDays(-((int)dt.DayOfWeek));
            DateTime EndDate = dt.AddDays(6 - (int)dt.DayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                calOverview.SelectedDates.Add(StartDate.AddDays(i));
            }
            // GetTotalActivities();
            GetTimeGridData();

            GetActivityEntries();
            GetTotalActivities();
            //DataTable dataa = TotalActivities.DefaultView.ToTable(true, "ProjectId", "MilestoneId", "wbsid", "activity_id", "description");

            //gvEntry.DataSource = dataa;
            gvEntry.DataBind();
        }

        protected void gvEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //  DataTable currActivites = (DataTable)ViewState["dtactivities"];
                    DataTable currenTable = (DataTable)ViewState["masterTable"];
                    DataTable totalActivities = (DataTable)ViewState["TotalActivities"];


                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlActivites");
                    ddl.DataSource = totalActivities;
                    ddl.DataTextField = "description";
                    ddl.DataValueField = "id";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));

                    ddl.SelectedValue = currenTable.Rows[e.Row.RowIndex]["Activites"].ToString();
                    //ddl.SelectedItem.Text = currenTable.Rows[e.Row.RowIndex]["Activites"].ToString();
                    //if (ddl.SelectedItem.Text == "")
                    //{
                    //    ddl.SelectedItem.Text = "Select..";
                    //}

                    string sqlflagquery = "select a.id from TE_Activity a join TE_RESOURCE_TIMESHEET_REL b on  a.ID=b.ACTIVITY_ID where a.ID='" + ddl.SelectedValue + "' and a.Progress='COMPLETED' and b.TIMESHEET_REQ_STATUS ='SUBMITTED'";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(sqlflagquery, conn);
                    SqlDataReader flagRead = cmd.ExecuteReader();


                    if (flagRead.Read())
                    {
                        ddl.Enabled = false;
                    }
                    flagRead.Close();

                    string sqlflagquery1 = "select a.id from TE_Activity a join TE_RESOURCE_TIMESHEET_REL b on  a.ID=b.ACTIVITY_ID where a.ID='" + ddl.SelectedValue + "' and a.Progress='INCOMPLETE' and b.TIMESHEET_REQ_STATUS ='SUBMITTED'";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd1 = new SqlCommand(sqlflagquery, conn);
                    SqlDataReader flagRead1 = cmd.ExecuteReader();


                    if (flagRead1.Read())
                    {
                        ddl.Enabled = false;
                    }
                    flagRead1.Close();

                    string sqlflagquery2 = "select a.id from TE_Activity a join TE_RESOURCE_TIMESHEET_REL b on  a.ID=b.ACTIVITY_ID where a.ID='" + ddl.SelectedValue + "' and a.Progress='COMPLETED' and b.TIMESHEET_REQ_STATUS ='SAVED'";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd2 = new SqlCommand(sqlflagquery, conn);
                    SqlDataReader flagRead2 = cmd.ExecuteReader();


                    if (flagRead2.Read())
                    {
                        ddl.Enabled = true;
                    }
                    flagRead2.Close();

                    string sqlflagquery3 = "select a.id from TE_Activity a join TE_RESOURCE_TIMESHEET_REL b on  a.ID=b.ACTIVITY_ID where a.ID='" + ddl.SelectedValue + "' and a.Progress='INCOMPLETE' and b.TIMESHEET_REQ_STATUS ='SAVED'";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd3 = new SqlCommand(sqlflagquery, conn);
                    SqlDataReader flagRead3 = cmd.ExecuteReader();


                    if (flagRead3.Read())
                    {
                        ddl.Enabled = true;
                    }
                    flagRead3.Close();

                    ddlActivites_SelectedIndexChanged(ddl, new EventArgs());


                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void GetActivityEntries()
        {
            DateTime dtt = calOverview.SelectedDate;
            DateTime StartDate = dtt.AddDays(-((int)dtt.DayOfWeek));
            DateTime EndDate = dtt.AddDays(6 - (int)dtt.DayOfWeek);

            DataTable cuurentTable = (DataTable)ViewState["masterTable"];
            DataTable tempData = new DataTable();

            string impquery = " Select distinct b.ProjectId, b.MilestoneId, b.WBSID, b.ActivityId, c.[Description] ";
            impquery += "  from TE_RESOURCE_TIMESHEET_REL a, TE_AssignedActivity b, TE_Activity c ";
            impquery += " where a.ACTIVITY_ID = b.ActivityId ";
            impquery += " and b.ResourceID = a.RESOURCE_ID ";
            impquery += " and b.ActivityId = c.ID";
            impquery += " and a.TIMESHEET_ISDELETED = 0";
            impquery += " and b.DeleteFlag is null ";
            impquery += " and c.DeleteFlag is null ";
            impquery += " and c.PlannedStartDate < '" + EndDate + "'";
            impquery += " and b.ResourceID = " + Session["uid"].ToString() + " ";

            //string impquery = "select a.projectid as ProjectId,a.MilestoneId,a.wbsid,a.ID as activity_id,";
            //impquery += "a.description,a.Progress,a.PlannedStartDate,a.PlannedEndDate,b.TIMESHEET_NOOFHRS ";
            //impquery += "from TE_Activity a,TE_RESOURCE_TIMESHEET_REL b ";
            //impquery += "where a.ProjectId=b.PROJECT_CODE and ";
            //impquery += "a.WBSID=b.WBS_ID and ";
            //impquery += "a.MilestoneId=b.MILESTONE_ID and ";
            //impquery += "a.ID=b.ACTIVITY_ID ";
            //impquery += " and b.RESOURCE_ID='" + Session["uid"].ToString() + "' ";
            //impquery += " and a.plannedstartdate <= '" + EndDate + "'";

            SqlCommand cmd = new SqlCommand(impquery, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(tempData);

            for (int i = 0; i < tempData.Rows.Count; i++)
            {
                cuurentTable.Rows[i]["ProjectId"] = tempData.Rows[i]["ProjectId"];
                cuurentTable.Rows[i]["MilestoneId"] = tempData.Rows[i]["MilestoneId"];
                cuurentTable.Rows[i]["wbsid"] = tempData.Rows[i]["WBSID"];
                cuurentTable.Rows[i]["activity_id"] = tempData.Rows[i]["ActivityId"];
                cuurentTable.Rows[i]["Activites"] = tempData.Rows[i]["ActivityId"];

                //DataRow drt = cuurentTable.NewRow();
                //cuurentTable.Rows.Add(drt);


            }

            ViewState["masterTable"] = cuurentTable;
            gvEntry.DataSource = cuurentTable;
            //DataTable dat = TotalActivities.DefaultView.ToTable(true, "ProjectId", "MilestoneId", "wbsid", "activity_id", "description");


            //DataTable dtactivities = TotalActivities.DefaultView.ToTable(true, "activity_id", "description");

            //ViewState["dtactivities"] = dtactivities;

            //ViewState["ActivityData"] = dat;

            // gvEntry.DataSource = tempData;

            // gvEntry.DataSource = TotalActivities;




        }
        //public void GetTimeSheetEntriesData()
        //{
        //    try
        //    {
        //        DataTable dtTemp = new DataTable();
        //        DataRow dr = null;

        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntrySunday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntryMonday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntryTuesday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntryWednesday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntryThursday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntryFriday", typeof(System.String)));
        //        dtTemp.Columns.Add(new DataColumn("txtTimeEntrySaturday", typeof(System.String)));

        //        dr = dtTemp.NewRow();
        //        dr["SrNo"] = "1";
        //        dr["txtTimeEntrySunday"] = String.Empty;
        //        dr["txtTimeEntryMonday"] = String.Empty;
        //        dr["txtTimeEntryTuesday"] = String.Empty;
        //        dr["txtTimeEntryWednesday"] = String.Empty;
        //        dr["txtTimeEntryThursday"] = String.Empty;
        //        dr["txtTimeEntryFriday"] = String.Empty;
        //        dr["txtTimeEntrySaturday"] = String.Empty;
        //        dtTemp.Rows.Add(dr);
        //        ViewState["TimeSheetEntriesData"] = dtTemp;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }


        //}
        public void GetTimeGridData()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                DataRow dr = null;

                dtTemp.Columns.Add(new DataColumn("ProjectId", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("MilestoneId", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("wbsid", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("activity_id", typeof(System.String)));

                dtTemp.Columns.Add(new DataColumn("Activites", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntrySunday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntryMonday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntryTuesday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntryWednesday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntryThursday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntryFriday", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("txtTimeEntrySaturday", typeof(System.String)));

                dtTemp.Columns.Add(new DataColumn("Total", typeof(System.String)));
                dtTemp.Columns.Add(new DataColumn("Completed", typeof(System.String)));
                dr = dtTemp.NewRow();
                // dr["SrNo"] = "1";
                dr["ProjectId"] = String.Empty;
                dr["MilestoneId"] = String.Empty;
                dr["wbsid"] = String.Empty;
                dr["activity_id"] = String.Empty;

                dr["Activites"] = String.Empty;
                dr["txtTimeEntrySunday"] = String.Empty;
                dr["txtTimeEntryMonday"] = String.Empty;
                dr["txtTimeEntryTuesday"] = String.Empty;
                dr["txtTimeEntryWednesday"] = String.Empty;
                dr["txtTimeEntryThursday"] = String.Empty;
                dr["txtTimeEntryFriday"] = String.Empty;
                dr["txtTimeEntrySaturday"] = String.Empty;

                dr["Total"] = String.Empty;
                dr["Completed"] = String.Empty;
                dtTemp.Rows.Add(dr);
                ViewState["masterTable"] = dtTemp;

            }
            catch (Exception ex)
            {
                //  return null;
            }


        }
        private void GetTotalActivities()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DateTime dt = calOverview.SelectedDate;
                DateTime StartDate = dt.AddDays(-((int)dt.DayOfWeek));
                DateTime EndDate = dt.AddDays(6 - (int)dt.DayOfWeek);

                SqlCommand cmd = new SqlCommand("Select * from TE_Activity a inner join TE_AssignedActivity b on a.ID = b.ActivityId where (b.Progess <> 'COMPLETED' or b.Progess is null) and b.DeleteFlag is null and b.ResourceID = '" + Session["uid"].ToString() + "'  and a.PlannedStartDate < '" + EndDate + "'", conn);
                //SqlCommand cmd = new SqlCommand("Select * from TE_Activity a inner join TE_AssignedActivity b on a.ID = b.ActivityId where (b.Progess <> 'COMPLETED' or b.Progess is null) and b.DeleteFlag is null", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(TotalActivities);
                ViewState["TotalActivities"] = TotalActivities;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (TotalActivities.Rows.Count == 0)
                {
                    btnSubmitTimeEntry.Enabled = false;
                }
                else
                {
                    btnSubmitTimeEntry.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSubmitTimeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvEntry.Rows.Count; i++)
                {
                    try
                    {
                        string ProjectCode="", MilestoneID="", WBSID="";
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        //string ProjectCode = gvEntry.Rows[i].Cells[0].Text;
                        //string MilestoneID = gvEntry.Rows[i].Cells[1].Text;
                        //string WBSID = gvEntry.Rows[i].Cells[2].Text;
                        //string ActivityID = gvEntry.Rows[i].Cells[3].Text;
                        string ActivityID = ((DropDownList)gvEntry.Rows[i].Cells[13].FindControl("ddlActivites")).SelectedValue;

                        string StrDetails = "select projectid,wbsid,milestoneid from TE_Activity where id='" + ActivityID + "'";
                        SqlCommand cmdDetails = new SqlCommand(StrDetails, conn);
                        SqlDataReader rd = cmdDetails.ExecuteReader();

                        if (rd.Read())
                        {

                             ProjectCode = rd["projectid"].ToString();
                            MilestoneID = rd["wbsid"].ToString();
                          WBSID =rd["milestoneid"].ToString();

                        }

                        rd.Close();
                        
                        string ResourceID = Session["uid"].ToString();
                        string Completed = ((DropDownList)gvEntry.Rows[i].Cells[13].FindControl("ddlCompleted")).SelectedValue;
                        foreach (DateTime date in calOverview.SelectedDates)
                        {
                            Label lblFlag = (Label)gvEntry.Rows[i].FindControl("lblFlag" + date.DayOfWeek);
                            TextBox txtTimeEntry = (TextBox)gvEntry.Rows[i].FindControl("txtTimeEntry" + date.DayOfWeek);

                            if (lblFlag.Text == "NEW")
                            {
                                if (txtTimeEntry.Text != "")
                                {
                                    if (conn.State == ConnectionState.Closed)
                                    {
                                        conn.Open();
                                    }
                                    SqlCommand cmd = new SqlCommand("Insert into TE_RESOURCE_TIMESHEET_REL (PROJECT_CODE, MILESTONE_ID, WBS_ID, ACTIVITY_ID, RESOURCE_ID, TIMESHEET_CREATEDT, TIMESHEET_NOOFHRS, TIMESHEET_ISDELETED, TIMESHEET_REQ_STATUS) VALUES ('" + ProjectCode + "', '" + MilestoneID + "', '" + WBSID + "', '" + ActivityID + "', '" + ResourceID + "', convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103), convert(datetime, '" + txtTimeEntry.Text + "', 103), '0', 'SUBMITTED')", conn);
                                    lblError.Text += cmd.CommandText;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    if (conn.State == ConnectionState.Open)
                                    {
                                        conn.Close();
                                    }
                                }
                            }
                            else if (lblFlag.Text == "EDIT")
                            {
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }
                                if (txtTimeEntry.Text != "")
                                {
                                    SqlCommand cmd = new SqlCommand("update TE_RESOURCE_TIMESHEET_REL set TIMESHEET_NOOFHRS = convert(datetime, '" + txtTimeEntry.Text + "', 103), TIMESHEET_REQ_STATUS = 'SUBMITTED' WHERE PROJECT_CODE = '" + ProjectCode + "' AND MILESTONE_ID = '" + MilestoneID + "' AND WBS_ID = '" + WBSID + "' AND ACTIVITY_ID = '" + ActivityID + "' AND RESOURCE_ID = '" + ResourceID + "' AND TIMESHEET_CREATEDT = convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103)", conn);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                    //vaibhav addition
                                    SqlCommand cmdUpdate2 = new SqlCommand("update TE_Activity set  Progress = 'COMPLETED' WHERE PROJECTid = '" + ProjectCode + "' AND MILESTONEID = '" + MilestoneID + "' AND WBSID = '" + WBSID + "' AND ACTIVITYID = '" + ActivityID + "'", conn);
                                    cmdUpdate2.CommandType = CommandType.Text;
                                    cmdUpdate2.ExecuteNonQuery();

                                    //vaibhav end
                                }
                                else if (txtTimeEntry.Text == "")
                                {
                                    SqlCommand cmd = new SqlCommand("delete from TE_RESOURCE_TIMESHEET_REL WHERE PROJECT_CODE = '" + ProjectCode + "' AND MILESTONE_ID = '" + MilestoneID + "' AND WBS_ID = '" + WBSID + "' AND ACTIVITY_ID = '" + ActivityID + "' AND RESOURCE_ID = '" + ResourceID + "' AND TIMESHEET_CREATEDT = convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103)", conn);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                }
                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }

                            }
                        }

                        if (Completed == "Yes")
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            //SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_ACTIVITY_DEF set ACTIVITY_STATUS = 'C', SUBMITTION = 'SUBMITTED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                            //cmd.CommandType = CommandType.Text;
                            //cmd.ExecuteNonQuery();

                            
                            //        //vaibhav addition
                            //        SqlCommand cmdUpdate2 = new SqlCommand("update TE_Activity set  Progress = 'COMPLETED' WHERE PROJECTid = '" + ProjectCode + "' AND MILESTONEID = '" + MilestoneID + "' AND WBSID = '" + WBSID + "' AND ACTIVITYID = '" + ActivityID + "'", conn);
                            //        cmdUpdate2.CommandType = CommandType.Text;
                            //        cmdUpdate2.ExecuteNonQuery();

                            SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_TIMESHEET_REL set  TIMESHEET_REQ_status = 'SUBMITTED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            SqlCommand cmd1 = new SqlCommand("Update TE_Activity set  PROGRESS = 'COMPLETED' where PROJECTID = '" + ProjectCode + "' and MILESTONEID = '" + MilestoneID + "' and WBSID = '" + WBSID + "' and RESOURCEID = '" + Session["uid"].ToString() + "' and ID = '" + ActivityID + "'", conn);
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();




                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                        else
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            //SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_ACTIVITY_DEF set ACTIVITY_STATUS = 'I', SUBMITTION = 'SUBMITTED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                            //cmd.CommandType = CommandType.Text;
                            //cmd.ExecuteNonQuery();
                            SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_TIMESHEET_REL set  TIMESHEET_REQ_status = 'SUBMITTED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            SqlCommand cmd1 = new SqlCommand("Update TE_Activity set  PROGRESS = 'INCOMPLETE' where PROJECTID = '" + ProjectCode + "' and MILESTONEID = '" + MilestoneID + "' and WBSID = '" + WBSID + "' and RESOURCEID = '" + Session["uid"].ToString() + "' and ID = '" + ActivityID + "'", conn);
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();


                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text += ex.Message;
                        lblError.Visible = true;
                    }
                }



                //GetTotalActivities();
                //gvEntry.DataSource = TotalActivities;

                GetTimeGridData();

                GetActivityEntries();
                GetTotalActivities();


                DateTime dt = DateTime.Today;
                DateTime StartDate = dt.AddDays(-((int)dt.DayOfWeek));
                DateTime EndDate = dt.AddDays(6 - (int)dt.DayOfWeek);

                for (int i = 0; i < 7; i++)
                {
                    calOverview.SelectedDates.Add(StartDate.AddDays(i));

                }
                gvEntry.DataBind();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Time Entries Submitted Successfully');</script>");
                //Response.Write("<script>alert('Time Entries Submitted Successfully');</script>");
            }
            catch (Exception ex)
            {
                lblError.Text += ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSaveTimeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvEntry.Rows.Count; i++)
                {
                    string ProjectCode = "", MilestoneID = "", WBSID = "";
                    //string ProjectCode = gvEntry.Rows[i].Cells[0].Text;
                    //string MilestoneID = gvEntry.Rows[i].Cells[1].Text;
                    //string WBSID = gvEntry.Rows[i].Cells[2].Text;
                    //string ActivityID = gvEntry.Rows[i].Cells[3].Text;
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string ActivityID = ((DropDownList)gvEntry.Rows[i].Cells[13].FindControl("ddlActivites")).SelectedValue;
                    string StrDetails = "select projectid,wbsid,milestoneid from TE_Activity where id='" + ActivityID + "'";
                    SqlCommand cmdDetails = new SqlCommand(StrDetails, conn);
                    SqlDataReader rd = cmdDetails.ExecuteReader();

                    if (rd.Read())
                    {

                        ProjectCode = rd["projectid"].ToString();
                        MilestoneID = rd["wbsid"].ToString();
                        WBSID = rd["milestoneid"].ToString();

                    }
                    rd.Close();

                    string ResourceID = Session["uid"].ToString();
                    string Completed = ((DropDownList)gvEntry.Rows[i].Cells[13].FindControl("ddlCompleted")).SelectedValue;

                    foreach (DateTime date in calOverview.SelectedDates)
                    {
                        Label lblFlag = (Label)gvEntry.Rows[i].FindControl("lblFlag" + date.DayOfWeek);
                        TextBox txtTimeEntry = (TextBox)gvEntry.Rows[i].FindControl("txtTimeEntry" + date.DayOfWeek);

                        if (lblFlag.Text == "NEW")
                        {
                            if (txtTimeEntry.Text != "")
                            {
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }
                                SqlCommand cmd = new SqlCommand("Insert into TE_RESOURCE_TIMESHEET_REL (PROJECT_CODE, MILESTONE_ID, WBS_ID, ACTIVITY_ID, RESOURCE_ID, TIMESHEET_CREATEDT, TIMESHEET_NOOFHRS, TIMESHEET_ISDELETED, TIMESHEET_REQ_STATUS) VALUES ('" + ProjectCode + "', '" + MilestoneID + "', '" + WBSID + "', '" + ActivityID + "', '" + ResourceID + "', convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103), convert(datetime, '" + txtTimeEntry.Text + "', 103), '0', 'SAVED')", conn);
                                lblError.Text += cmd.CommandText;
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }
                            }
                        }
                        else if (lblFlag.Text == "EDIT")
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            if (txtTimeEntry.Text != "")
                            {
                                SqlCommand cmd = new SqlCommand("update TE_RESOURCE_TIMESHEET_REL set TIMESHEET_NOOFHRS = convert(datetime, '" + txtTimeEntry.Text + "', 103), TIMESHEET_REQ_STATUS = 'SAVED' WHERE PROJECT_CODE = '" + ProjectCode + "' AND MILESTONE_ID = '" + MilestoneID + "' AND WBS_ID = '" + WBSID + "' AND ACTIVITY_ID = '" + ActivityID + "' AND RESOURCE_ID = '" + ResourceID + "' AND TIMESHEET_CREATEDT = convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103)", conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                            }
                            else if (txtTimeEntry.Text == "")
                            {
                                SqlCommand cmd = new SqlCommand("delete from TE_RESOURCE_TIMESHEET_REL WHERE PROJECT_CODE = '" + ProjectCode + "' AND MILESTONE_ID = '" + MilestoneID + "' AND WBS_ID = '" + WBSID + "' AND ACTIVITY_ID = '" + ActivityID + "' AND RESOURCE_ID = '" + ResourceID + "' AND TIMESHEET_CREATEDT = convert(datetime, '" + (date.Day + "/" + date.Month + "/" + date.Year) + "', 103)", conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();

                            }
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }

                        }
                    }
                    if (Completed == "Yes")
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        //SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_ACTIVITY_DEF set ACTIVITY_STATUS = 'C', SUBMITTION = 'SAVED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                        SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_TIMESHEET_REL set  TIMESHEET_REQ_status = 'SAVED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd1 = new SqlCommand("Update TE_Activity set  PROGRESS = 'COMPLETED' where PROJECTID = '" + ProjectCode + "' and MILESTONEID = '" + MilestoneID + "' and WBSID = '" + WBSID + "' and RESOURCEID = '" + Session["uid"].ToString() + "' and ID = '" + ActivityID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        //SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_ACTIVITY_DEF set ACTIVITY_STATUS = 'I', SUBMITTION = 'SAVED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                        //cmd.CommandType = CommandType.Text;
                        //cmd.ExecuteNonQuery();

                        SqlCommand cmd = new SqlCommand("Update TE_RESOURCE_TIMESHEET_REL set  TIMESHEET_REQ_status = 'SAVED' where PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and RESOURCE_ID = '" + Session["uid"].ToString() + "' and ACTIVITY_ID = '" + ActivityID + "'", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd1 = new SqlCommand("Update TE_Activity set  PROGRESS = 'INCOMPLETE' where PROJECTID = '" + ProjectCode + "' and MILESTONEID = '" + MilestoneID + "' and WBSID = '" + WBSID + "' and RESOURCEID = '" + Session["uid"].ToString() + "' and ID = '" + ActivityID + "'", conn);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                //Response.Redirect("TimeEntry.aspx", false);
                GetTimeGridData();

                GetActivityEntries();
                GetTotalActivities();



                //GetTotalActivities();

                //gvEntry.DataSource = TotalActivities;

                DateTime dt = DateTime.Today;
                DateTime StartDate = dt.AddDays(-((int)dt.DayOfWeek));
                DateTime EndDate = dt.AddDays(6 - (int)dt.DayOfWeek);

                for (int i = 0; i < 7; i++)
                {
                    calOverview.SelectedDates.Add(StartDate.AddDays(i));

                }
                gvEntry.DataBind();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Time Entries Saved Successfully');</script>");
                //Response.Write("<script>alert('Time Entries Saved Successfully');</script>");
            }
            catch (Exception ex)
            {
                lblError.Text += ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlActivites_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable timeentries = ViewState[""];
            DataTable currentTable = (DataTable)ViewState["masterTable"];
            DropDownList ddlMode = (DropDownList)sender;
            string activityid = ddlMode.SelectedValue;
            GridViewRow gvRow = (GridViewRow)ddlMode.NamingContainer;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            int Hours = 0;
            int mins = 0;
            //DataTable tableTimeEntries = (DataTable)ViewState["TimeSheetEntriesData"];
            foreach (DateTime date in calOverview.SelectedDates)
            {
                try
                {
                    string ProjectCodes = gvRow.Cells[0].Text;
                    string ProjectCode = gvRow.Cells[0].Text;
                    string MilestoneID = gvRow.Cells[1].Text;
                    string WBSID = gvRow.Cells[2].Text;
                    string ActivityID = gvRow.Cells[3].Text;
                    DataTable dt1 = (DataTable)gvEntry.DataSource;
                    SqlCommand cmd = new SqlCommand("SELECT CASE WHEN MINS > 60 THEN (MINS / 60)+HRS	ELSE HRS END AS HRS,CASE WHEN MINS > 60 THEN (MINS % 60) ELSE MINS END AS MINS FROM(Select sum(datepart(hour,timesheet_noofhrs)) as hrs, sum(datepart(minute,timesheet_noofhrs)) as mins  from TE_RESOURCE_TIMESHEET_REL where TIMESHEET_STATUS is null and ACTIVITY_ID = '" + activityid + "' and TIMESHEET_CREATEDT = convert(datetime,'" + date.ToString("dd/MM/yyyy") + "',103) and RESOURCE_ID = '" + Session["uid"].ToString() + "' )TAB", conn);
                    // SqlCommand cmd = new SqlCommand("SELECT CASE WHEN MINS > 60 THEN (MINS / 60)+HRS	ELSE HRS END AS HRS,CASE WHEN MINS > 60 THEN (MINS % 60) ELSE MINS END AS MINS FROM(Select sum(datepart(hour,timesheet_noofhrs)) as hrs, sum(datepart(minute,timesheet_noofhrs)) as mins  from TE_RESOURCE_TIMESHEET_REL where TIMESHEET_STATUS is null and PROJECT_CODE = '" + ProjectCode + "' and MILESTONE_ID = '" + MilestoneID + "' and WBS_ID = '" + WBSID + "' and ACTIVITY_ID = '" + ActivityID + "' and TIMESHEET_CREATEDT = convert(datetime,'" + date.ToString("dd/MM/yyyy") + "',103))TAB", conn);

                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    string TotalTime = dt.Rows[0][0].ToString() + ":" + dt.Rows[0][1].ToString();
                    HyperLink lnkExpenseEntry = (HyperLink)gvRow.FindControl("lnkExpenseEntry" + date.DayOfWeek);
                    lnkExpenseEntry.NavigateUrl = "ExpenseEntry.aspx?ProjectCode=" + ProjectCode + "&MilestoneId=" + MilestoneID + "&WBSID=" + WBSID + "&ActivityID=" + ActivityID + "&Date=" + date.Date.ToString("dd/MM/yyyy") + "&iframe=true&width=480&height=510";
                    if (TotalTime != ":")
                    {
                        TextBox txtTimeEntry = (TextBox)gvRow.FindControl("txtTimeEntry" + date.DayOfWeek);
                        txtTimeEntry.Text = TotalTime;
                        Label lblFlag = (Label)gvRow.FindControl("lblFlag" + date.DayOfWeek);
                        lblFlag.Text = "EDIT";
                        Hours = Hours + Convert.ToInt32(dt.Rows[0][0].ToString());
                        mins = mins + Convert.ToInt32(dt.Rows[0][1].ToString());

                        currentTable.Rows[gvRow.RowIndex]["txtTimeEntry" + date.DayOfWeek] = txtTimeEntry.Text;
                        if (ddlMode.Enabled == false)
                        {

                            txtTimeEntry.Enabled = false;

                        }
                    }
                    else
                    {
                        TextBox txtTimeEntry = (TextBox)gvRow.FindControl("txtTimeEntry" + date.DayOfWeek);
                        txtTimeEntry.Text = "";
                        Label lblFlag = (Label)gvRow.FindControl("lblFlag" + date.DayOfWeek);
                        lblFlag.Text = "NEW";


                    }

                    //if (dt1.Rows[gvRow.RowIndex]["Progess"].ToString() != "COMPLETED")
                    //{
                    //    ((DropDownList)gvRow.FindControl("ddlCompleted")).SelectedValue = "No";
                    //}
                    //else
                    //{
                    //    ((DropDownList)gvRow.FindControl("ddlCompleted")).SelectedValue = "Yes";
                    //}
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                //DataRow dtdr = tableTimeEntries.NewRow();
                //tableTimeEntries.Rows.Add(dtdr);
            }
            ViewState["masterTable"] = currentTable;
            Label lblTotal = (Label)gvRow.FindControl("lblTotal");
            if (mins >= 60)
            {
                Hours = Hours + (mins / 60);
                mins = mins % 60;
            }
        }
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["masterTable"] != null)
            {
                //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataTable dtCurrentTable = (DataTable)ViewState["masterTable"];
                DataTable tempTable = dtCurrentTable.Clone();
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEntry.Rows.Count; i++)
                    {

                        dtCurrentTable.Rows[i]["ProjectId"] = gvEntry.Rows[rowIndex].Cells[0].Text;
                        dtCurrentTable.Rows[i]["MilestoneId"] = gvEntry.Rows[rowIndex].Cells[1].Text;
                        dtCurrentTable.Rows[i]["WBSID"] = gvEntry.Rows[rowIndex].Cells[2].Text;
                        dtCurrentTable.Rows[i]["activity_id"] = gvEntry.Rows[rowIndex].Cells[3].Text;
                        DropDownList ddlActivites = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlActivites");
                        TextBox txtTimeEntrySunday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySunday");
                        TextBox txtTimeEntryMonday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryMonday");
                        TextBox txtTimeEntryTuesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryTuesday");
                        TextBox txtTimeEntryWednesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryWednesday");
                        TextBox txtTimeEntryThursday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryThursday");
                        TextBox txtTimeEntryFriday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryFriday");
                        TextBox txtTimeEntrySaturday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySaturday");
                        Label lblTotal = (Label)gvEntry.Rows[rowIndex].FindControl("lblTotal");
                        DropDownList ddlCompleted = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlCompleted");
                        //drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;
                        //dtCurrentTable.Rows[i]["Activites"] = ddlActivites.SelectedItem;

                        dtCurrentTable.Rows[i]["Activites"] = ddlActivites.SelectedValue;

                        dtCurrentTable.Rows[i]["txtTimeEntrySunday"] = txtTimeEntrySunday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntryMonday"] = txtTimeEntryMonday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntryTuesday"] = txtTimeEntryTuesday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntryWednesday"] = txtTimeEntryWednesday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntryThursday"] = txtTimeEntryThursday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntryFriday"] = txtTimeEntryFriday.Text;
                        dtCurrentTable.Rows[i]["txtTimeEntrySaturday"] = txtTimeEntrySaturday.Text;
                        dtCurrentTable.Rows[i]["Total"] = lblTotal.Text;
                        dtCurrentTable.Rows[i]["Completed"] = ddlCompleted.Text;

                        //drCurrentRow = dtCurrentTable.NewRow();
                        ////dtCurrentTable.Rows.Add(drCurrentRow);
                        //}
                        rowIndex++;
                        //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        //{


                    }
                    DataRow dr = dtCurrentTable.NewRow();
                    dr["ProjectId"] = 0;
                    dr["MilestoneId"] = 0;
                    dr["WBSID"] = 0;
                    dr["activity_id"] = 0;


                    dtCurrentTable.Rows.Add(dr);

                    //drCurrentRow = dtCurrentTable.NewRow();
                    //dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["masterTable"] = dtCurrentTable;
                    gvEntry.DataSource = dtCurrentTable;
                    gvEntry.DataBind();
                }


                //dtCurrentTable.Rows.RemoveAt(0);
                // DataTable acticityData = (DataTable)ViewState["ActivityData"];
                //DataRow drCurrentRow = null;
                ////if (dtCurrentTable.Rows.Count > 0)
                ////{
                //    //for (int i = 1; i < acticityData.Rows.Count; i++)
                //    //{
                //for (int i = 1; i < dtCurrentTable.Rows.Count; i++)
                //{ 

                //        dtCurrentTable.Rows[i - 1]["ProjectId"] = gvEntry.Rows[rowIndex].Cells[0].Text;
                //        dtCurrentTable.Rows[i - 1]["MilestoneId"] = gvEntry.Rows[rowIndex].Cells[1].Text;
                //        dtCurrentTable.Rows[i - 1]["WBSID"] = gvEntry.Rows[rowIndex].Cells[2].Text;
                //        dtCurrentTable.Rows[i - 1]["activity_id"] = gvEntry.Rows[rowIndex].Cells[3].Text;


                //        DropDownList ddlActivites = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlActivites");
                //        dtCurrentTable.Rows[i - 1]["Activites"] = ddlActivites.SelectedValue;


                //        TextBox txtTimeEntrySunday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySunday");
                //        TextBox txtTimeEntryMonday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryMonday");
                //        TextBox txtTimeEntryTuesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryTuesday");
                //        TextBox txtTimeEntryWednesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryWednesday");
                //        TextBox txtTimeEntryThursday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryThursday");
                //        TextBox txtTimeEntryFriday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryFriday");
                //        TextBox txtTimeEntrySaturday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySaturday");
                //        Label lblTotal = (Label)gvEntry.Rows[rowIndex].FindControl("lblTotal");
                //        DropDownList ddlCompleted = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlCompleted");
                //        //drCurrentRow = dtCurrentTable.NewRow();
                //        //drCurrentRow["RowNumber"] = i + 1;

                //        dtCurrentTable.Rows[i - 1]["EntrySunday"] = txtTimeEntrySunday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntryMonday"] = txtTimeEntryMonday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntryTuesday"] = txtTimeEntryTuesday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntryWednesday"] = txtTimeEntryWednesday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntryThursday"] = txtTimeEntryThursday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntryFriday"] = txtTimeEntryFriday.Text;
                //        dtCurrentTable.Rows[i - 1]["EntrySaturday"] = txtTimeEntrySaturday.Text;
                //        dtCurrentTable.Rows[i - 1]["Total"] = lblTotal.Text;
                //        dtCurrentTable.Rows[i - 1]["Completed"] = ddlCompleted.Text;
                //        drCurrentRow = dtCurrentTable.NewRow();

                //        rowIndex++;

                //        //drCurrentRow = dtCurrentTable.NewRow();

                //        //drCurrentRow["ProjectId"] = gvEntry.Rows[rowIndex].Cells[0].Text;
                //        //drCurrentRow["MilestoneId"] = gvEntry.Rows[rowIndex].Cells[1].Text;
                //        //drCurrentRow["WBSID"] = gvEntry.Rows[rowIndex].Cells[2].Text;
                //        //drCurrentRow["activity_id"] = gvEntry.Rows[rowIndex].Cells[3].Text;


                //        //DropDownList ddlActivites = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlActivites");
                //        //drCurrentRow["Activites"] = ddlActivites.SelectedValue;

                //        ////    rowIndexes++;
                //        ////}

                //        ////for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                //        ////{
                //        ////    //extract the TextBox values


                //        //TextBox txtTimeEntrySunday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySunday");
                //        //TextBox txtTimeEntryMonday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryMonday");
                //        //TextBox txtTimeEntryTuesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryTuesday");
                //        //TextBox txtTimeEntryWednesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryWednesday");
                //        //TextBox txtTimeEntryThursday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryThursday");
                //        //TextBox txtTimeEntryFriday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryFriday");
                //        //TextBox txtTimeEntrySaturday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySaturday");
                //        //Label lblTotal = (Label)gvEntry.Rows[rowIndex].FindControl("lblTotal");
                //        //DropDownList ddlCompleted = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlCompleted");
                //        ////drCurrentRow = dtCurrentTable.NewRow();
                //        ////drCurrentRow["RowNumber"] = i + 1;

                //        //drCurrentRow["EntrySunday"] = txtTimeEntrySunday.Text;
                //        //drCurrentRow["EntryMonday"] = txtTimeEntryMonday.Text;
                //        //drCurrentRow["EntryTuesday"] = txtTimeEntryTuesday.Text;
                //        //drCurrentRow["EntryWednesday"] = txtTimeEntryWednesday.Text;
                //        //drCurrentRow["EntryThursday"] = txtTimeEntryThursday.Text;
                //        //drCurrentRow["EntryFriday"] = txtTimeEntryFriday.Text;
                //        //drCurrentRow["EntrySaturday"] = txtTimeEntrySaturday.Text;
                //        //drCurrentRow["Total"] = lblTotal.Text;
                //        //drCurrentRow["Completed"] = ddlCompleted.Text;

                //        //dtCurrentTable.Rows.Add(drCurrentRow);
                //        //rowIndex++;

                //   }

                //DataRow drCurrentRowSp = null;
                //drCurrentRowSp = dtCurrentTable.NewRow();

                //dtCurrentTable.Rows.Add(drCurrentRowSp);
                //ViewState["CurrentTable"] = dtCurrentTable;

            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["masterTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["masterTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        DropDownList ddlActivites = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlActivites");
                        TextBox txtTimeEntrySunday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySunday");
                        TextBox txtTimeEntryMonday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryMonday");
                        TextBox txtTimeEntryTuesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryTuesday");
                        TextBox txtTimeEntryWednesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryWednesday");
                        TextBox txtTimeEntryThursday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryThursday");
                        TextBox txtTimeEntryFriday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryFriday");
                        TextBox txtTimeEntrySaturday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySaturday");
                        Label lblTotal = (Label)gvEntry.Rows[rowIndex].FindControl("lblTotal");
                        DropDownList ddlCompleted = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlCompleted");

                        //drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;
                        gvEntry.Rows[rowIndex].Cells[0].Text = dt.Rows[i]["ProjectId"].ToString();
                        gvEntry.Rows[rowIndex].Cells[1].Text = dt.Rows[i]["MilestoneId"].ToString();
                        gvEntry.Rows[rowIndex].Cells[2].Text = dt.Rows[i]["WBSID"].ToString();
                        gvEntry.Rows[rowIndex].Cells[3].Text = dt.Rows[i]["activity_id"].ToString();

                        ddlActivites.SelectedValue = dt.Rows[i]["Activites"].ToString();
                        txtTimeEntrySunday.Text = dt.Rows[i]["txtTimeEntrySunday"].ToString();
                        txtTimeEntryMonday.Text = dt.Rows[i]["txtTimeEntryMonday"].ToString();
                        txtTimeEntryTuesday.Text = dt.Rows[i]["txtTimeEntryTuesday"].ToString();
                        txtTimeEntryWednesday.Text = dt.Rows[i]["txtTimeEntryWednesday"].ToString();
                        txtTimeEntryThursday.Text = dt.Rows[i]["txtTimeEntryThursday"].ToString();
                        txtTimeEntryFriday.Text = dt.Rows[i]["txtTimeEntryFriday"].ToString();
                        txtTimeEntrySaturday.Text = dt.Rows[i]["txtTimeEntrySaturday"].ToString();

                        lblTotal.Text = dt.Rows[i]["Total"].ToString();
                        ddlCompleted.SelectedValue = dt.Rows[i]["Completed"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void ddlCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {
            //GetGridData();
            AddNewRowToGrid();
        }
        //public void GetGridData()
        //{
        //    int rowIndex = 0;

        //   if (ViewState["CurrentTable"] != null)
        //        {
        //            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //            //DataTable acticityData = (DataTable)ViewState["ActivityData"];

        //            DataTable acticityData = (DataTable)ViewState["ActivityData"];
        //       DataRow drCurrentRow = null;
        //        //if (dtCurrentTable.Rows.Count > 0)
        //        //{
        //        for (int i = 1; i <= gvEntry.Rows.Count; i++)
        //        {
        //            dtCurrentTable.Rows[i - 1]["ProjectId"] = gvEntry.Rows[rowIndex].Cells[0].Text;
        //            dtCurrentTable.Rows[i - 1]["MilestoneId"] = gvEntry.Rows[rowIndex].Cells[1].Text;
        //            dtCurrentTable.Rows[i - 1]["WBSID"] = gvEntry.Rows[rowIndex].Cells[2].Text;
        //            dtCurrentTable.Rows[i - 1]["activity_id"] = gvEntry.Rows[rowIndex].Cells[3].Text;
        //            DropDownList ddlActivites = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlActivites");
        //            dtCurrentTable.Rows[i - 1]["Activites"] = ddlActivites.SelectedValue;


        //            TextBox txtTimeEntrySunday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySunday");
        //            TextBox txtTimeEntryMonday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryMonday");
        //            TextBox txtTimeEntryTuesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryTuesday");
        //            TextBox txtTimeEntryWednesday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryWednesday");
        //            TextBox txtTimeEntryThursday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryThursday");
        //            TextBox txtTimeEntryFriday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntryFriday");
        //            TextBox txtTimeEntrySaturday = (TextBox)gvEntry.Rows[rowIndex].FindControl("txtTimeEntrySaturday");
        //            Label lblTotal = (Label)gvEntry.Rows[rowIndex].FindControl("lblTotal");
        //            DropDownList ddlCompleted = (DropDownList)gvEntry.Rows[rowIndex].FindControl("ddlCompleted");
        //            //drCurrentRow = dtCurrentTable.NewRow();
        //            //drCurrentRow["RowNumber"] = i + 1;

        //            dtCurrentTable.Rows[i - 1]["EntrySunday"] = txtTimeEntrySunday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntryMonday"] = txtTimeEntryMonday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntryTuesday"] = txtTimeEntryTuesday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntryWednesday"] = txtTimeEntryWednesday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntryThursday"] = txtTimeEntryThursday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntryFriday"] = txtTimeEntryFriday.Text;
        //            dtCurrentTable.Rows[i - 1]["EntrySaturday"] = txtTimeEntrySaturday.Text;
        //            dtCurrentTable.Rows[i - 1]["Total"] = lblTotal.Text;
        //            dtCurrentTable.Rows[i - 1]["Completed"] = ddlCompleted.Text;
        //            drCurrentRow = dtCurrentTable.NewRow();
        //            if(gvEntry.Rows.Count !=i)
        //            dtCurrentTable.Rows.Add(drCurrentRow);

        //            rowIndex++;

        //        }
        //        ViewState["CurrentTable"] = dtCurrentTable;

        //    }

        //}

    }
}