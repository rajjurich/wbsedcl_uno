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
using System.Drawing;

namespace UNO
{
    public partial class ESS_PendingRequest : System.Web.UI.Page
    {
        //DataTable dt;

        String status = "";
        //int cmd=1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
                Response.Redirect("Login.aspx",true);

            if (!IsPostBack)
            {
            //    BindGrid();
                bindGrid();
            }
        }


        public void bindGrid()
        {
            //string query = "";
            //if(Request.QueryString["Att"] !=null)
            //{

            //     query = Request.QueryString["Att"];

            //}


            string query = Request.QueryString["Att"];

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Proc_EssDashboard_Report";

            if (query == "0")
            {
                cmd.Parameters.Add("@cmd", SqlDbType.Int).Value = Convert.ToInt32(query);
                hide.Visible = false;
                //hide.Attributes.CssStyle.Add("Visibility", "false");
                divSearch.Attributes.CssStyle.Add("padding-left", "46%");
                heading.Text = "<h3 class=\"heading\" style=\"margin-bottom: 0px;\">Attendance Report<h3>";
                imgExpotBtn.Visible = true;
            }
            else
            {
                cmd.Parameters.Add("@cmd", SqlDbType.Int).Value = Convert.ToInt32(ddlEntityType.SelectedValue);
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddlStatus.SelectedValue.ToString();
            }
            cmd.Parameters.Add("@empId", SqlDbType.VarChar).Value = Session["uid"].ToString();
            if (txtFrom.Text != "")
            {
                cmd.Parameters.Add("@fromDate", SqlDbType.VarChar).Value = txtFrom.Text;
                cmd.Parameters.Add("@toDate", SqlDbType.VarChar).Value = txtTo.Text;
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);



            //for(int i=0;i<dt.Columns.Count;i++)
            //{
            //    string colName = dt.Columns[i].ColumnName.ToString();

            //    BoundField nameColumn = new BoundField();
            //    nameColumn.DataField = colName;
            //    nameColumn.HeaderText = colName;
            //    gvData.Columns.Add(nameColumn);
            //}

            gvData.DataSource = dt;
            gvData.DataBind();
            if (dt.Rows.Count > 0)
            {
                DropDownList ddl = (DropDownList)gvData.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvData.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvData.PageIndex + 1).ToString();
                Label lblcount = (Label)gvData.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvData.DataSource).Rows.Count.ToString() + " Records.";
                if (gvData.PageCount == 0)
                {
                    ((Button)gvData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvData.PageIndex + 1 == gvData.PageCount)
                {
                    ((Button)gvData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvData.PageIndex == 0)
                {
                    ((Button)gvData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)gvData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvData.PageSize * gvData.PageIndex) + 1) + " to " + (gvData.PageSize * (gvData.PageIndex + 1));

                gvData.BottomPagerRow.Visible = true;
            }
            
        }




        //private void BindGrid()
        //{
        //    if (ddlEntityType.SelectedValue == "A")
        //    {
        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("Select Tday_empcde as [E Code],m.epd_first_name+ ' ' +m.epd_last_name as Name , Convert(Char(12), TDay_Date, 103) As [Date], IsNull(Convert(Char(12), TDay_InTime, 108), '--:--') As [IN Time], IsNull(Convert(Char(12), TDay_Late, 108), '--:--') As [Late By], IsNull(Convert(Char(12), TDay_OuTime, 108), '--:--') As [OUT Time], IsNull(Convert(Char(12), TDay_Early, 108), '--:--') As [Early By], IsNull(Convert(Char(12), TDay_WrkHr, 108), '--:--') As [Work Hr], IsNull(Convert(Char(12), TDay_ExHr, 108), '--:--') As [Extra Hr], case TDay_Status when 'AB' then 'Absent' when 'PR' then 'Present'  when 'PRAB' then 'Halfday'  when 'ABWO' then 'WeekOff' when 'ABW2'  THEN 'WeekOff' when 'ABHO' THEN 'Holiday' when  'OD' THEN 'Out on Duty'  when 'PRWO' THEN 'Present in Weekoff' when 'PRW2' then 'Present on Weekend'  when 'PRHO' then 'Present on Holiday'  when 'ODWO' THEN 'Out on Duty' when 'ODW2'  THEN 'Out on Duty'  when 'ABOD' THEN 'Out on Duty' when 'ODHO' THEN 'Out on Duty'  when 'AP' then 'Adoption Leave' when 'CF' then 'Compensatory Leave' when 'CL' then 'Casual Leave' when 'ML' then 'Maternity Leave' when 'PL' then 'Privilege Leave' when 'PT' then 'Paternity Leave' when 'SL' then 'Sick Leave' else 'Leave' end As Status From TDay T,  ent_employee_personal_dtls M Where M.epd_empid = T.TDay_EmpCde And T.TDay_Date Between Convert(DateTime, DateAdd(Month, -12, GetDate()), 103) And Convert(DateTime, GetDate(), 103) And T.TDay_EmpCde = '" + Session["uid"].ToString() + "' Order By T.TDay_Date Desc", conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        gvEntity.PageSize = 5;
        //        gvEntity.DataSource = dt;
        
        //        gvEntity.DataBind();
        //    }
        //    else if (ddlEntityType.SelectedValue == "LA")
        //    {
        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("select ess_LA_empid as EmpCode, ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,  ESS_LA_LVCD as [Leave Code]  ,convert(VARCHAR(20),Ess_LA_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_LA_Todt,103)= convert(VARCHAR(20),Ess_LA_fromdt,103) " +
        //                        " THEN '' ELSE CONVERT(VARCHAR(20),ESS_LA_TODT,103)  END AS ToDate, Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
        //                       " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as [Status] from ESS_TA_LA,ent_employee_personal_dtls " +
        //                       " where  ess_ta_la.ess_LA_empid=ent_employee_personal_dtls.epd_empid " +
        //                         "and ess_la_empid= '" + Session["uid"].ToString() + "' And ess_la_isdeleted='0'  ", conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //         dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        gvEntity.PageSize = 5;
        //        gvEntity.DataSource = dt;
         
        //        gvEntity.DataBind();

        //    }
        //    else if (ddlEntityType.SelectedValue == "MA")
        //    {
        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("select ess_ma_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_ma_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_ma_Todt,103)= convert(VARCHAR(20),Ess_ma_fromdt,103) " +
        //                                          " then ''else convert(VARCHAR(20),Ess_ma_Todt,103)  end as ToDate,convert(char(5),ESS_MA_FROMTM,8) as [In],convert(char(5),ESS_MA_TOTM,8) as [Out], Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
        //                                         " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as [Status] from ESS_TA_MA,ent_employee_personal_dtls where ess_ma_empid='" + Session["uid"].ToString() + "' " +
        //                       "  and  ess_ta_ma.ess_ma_empid=ent_employee_personal_dtls.epd_empid and  ess_ma_isdeleted='0'   ", conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        gvEntity.PageSize = 5;
        //        gvEntity.DataSource = dt;
          
        //        gvEntity.DataBind();

        //    }
        //    else if (ddlEntityType.SelectedValue == "OD")
        //    {
        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("select ess_OD_empid as [E Code], ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name, ESS_OD_ODCD as [Type],convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
        //                        " then '' else convert(VARCHAR(20),Ess_OD_Todt,103) end as ToDate , Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' " +
        //                       " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as [Status] from ESS_TA_OD,ent_employee_personal_dtls  where" +
        //            "   ess_ta_od.ess_OD_empid=ent_employee_personal_dtls.epd_empid " +
        //              "  and ess_od_empid='" + Session["uid"].ToString() + "'  and ess_od_isdeleted='0' ", conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        gvEntity.PageSize = 5;
        //        gvEntity.DataSource = dt;
            
        //        gvEntity.DataBind();

        //    }
        //    else if (ddlEntityType.SelectedValue == "GP")
        //    {
        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("select ess_GP_empid as EmpCode,ent_employee_personal_dtls.Epd_First_Name+ ' ' +ent_employee_personal_dtls.Epd_Last_Name as Name,convert(VARCHAR(20),Ess_GP_fromdt,103) as FromDate, case when convert(VARCHAR(20),Ess_GP_Todt,103)= convert(VARCHAR(20),Ess_GP_fromdt,103) " +
        //                  " then '' else convert(VARCHAR(20),Ess_GP_Todt,103) end as ToDate,convert(char(5),ESS_GP_FROMTM,8) as [In],convert(char(5),ESS_GP_TOTM,8) as [Out], Case  ESS_GP_Status WHEN 'N' then 'Pending For Approval' " +
        //                 " when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as [Status] from ESS_TA_GP,ent_employee_personal_dtls  " +
        //                  " where  ess_ta_gp.ess_gp_empid=ent_employee_personal_dtls.epd_empid " +
        //                 " and ess_gp_empid= '" + Session["uid"].ToString() + "' and ess_gp_isdeleted='0'", conn);
        //        cmd.CommandType = CommandType.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        gvEntity.PageSize = 5;
        //        gvEntity.DataSource = dt;
          
        //        gvEntity.DataBind();

        //    }

        //}

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {

           // BindGrid();
        }

        protected void gvEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          //  gvEntity.PageIndex = e.NewPageIndex;
           // BindGrid();
        }

        //protected void imbExport_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (gvData.Rows.Count > 0)
        //    {

        //        Response.ClearContent();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", string.Format("attachment; FileName=Employee.xls"));
        //        Response.ContentType = "application/ms-excel";
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        gvData.AllowPaging = false;
        //        bindGrid();
        //        //Change the Header Row back to white color
        //        gvData.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //        //Applying stlye to gridview header cells
        //        for (int i = 0; i < gvData.HeaderRow.Cells.Count; i++)
        //        {
        //            gvData.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        //        }
        //        gvData.RenderControl(htw);
        //        Response.Write(sw.ToString());
        //        Response.End();
        //    }

        //}







            //string attachment = "attachment; filename=Emp.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gvEntity.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "EmployeesData.xls"));
            //Response.ContentType = "application/ms-excel";

            //StringWriter stringWriter = new StringWriter();
            //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            //gvEntity.AllowPaging = false;
            //gvEntity.DataBind();

            ////This will change the header background color
            //gvEntity.HeaderRow.Style.Add("background-color", "#FFFFFF");

            ////This will apply style to gridview header cells
            //for (int index = 0; index < gvEntity.HeaderRow.Cells.Count; index++)
            //{
            //    gvEntity.HeaderRow.Cells[index].Style.Add("background-color", "#d17250");
            //}

            //int index2 = 1;
            ////This will apply style to alternate rows
            //foreach (GridViewRow gridViewRow in gvEntity.Rows)
            //{
            //    gridViewRow.BackColor = System.Drawing.Color.White;
            //    if (index2 <= gvEntity.Rows.Count)
            //    {
            //        if (index2 % 2 != 0)
            //        {
            //            for (int index3 = 0; index3 < gridViewRow.Cells.Count; index3++)
            //            {
            //                gridViewRow.Cells[index3].Style.Add("background-color", "#eed0bb");
            //            }
            //        }
            //    }
            //    index2++;
            //}

            //gvEntity.RenderControl(htmlTextWriter);

            //Response.Write(stringWriter.ToString());
            //Response.End();


            //Response.Clear();
            //Response.AddHeader("content-disposition",
            //   string.Format("attachment;filename={0}.xls", "Employee"));
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.xls";

            //StringWriter stringWrite = new StringWriter();
            //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //gvEntity.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
  
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gvEntity_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            status = ddlStatus.SelectedValue.ToString();
            bindGrid();
        }

        protected void ddlEntityType_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //cmd = Convert.ToInt16(ddlEntityType.SelectedValue);
            bindGrid();
        }

        protected void imgExpotBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (gvData.Rows.Count > 0)
            {

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; FileName=Employee.xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvData.AllowPaging = false;
                bindGrid();
                //Change the Header Row back to white color
                gvData.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < gvData.HeaderRow.Cells.Count; i++)
                {
                    gvData.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                gvData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.ContentType = "application/octet-stream";
                Response.ContentType = "TEXT/CSV";
                Response.End();
                /////////
               

           

                //Response.ContentType = ContentType;
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv");
               // Response.AddHeader("Content-Length", newFile.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.ContentType = "TEXT/CSV";

                //newFile.Close();



                //Response.WriteFile(fpath);
              


            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvData.PageIndex = Convert.ToInt32(((DropDownList)gvData.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindGrid();
                
            }
            catch (Exception ex)
            {

            }
        }

        
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvData.PageIndex = gvData.PageIndex - 1;
                bindGrid();
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
                gvData.PageIndex = gvData.PageIndex + 1;
                bindGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_Sanc_View");
            }
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }
    }
}