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
    public partial class ODAppView : System.Web.UI.Page
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
            this.Form.DefaultButton = btnSearch.UniqueID;
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();
            }


            if (!Page.IsPostBack)
            {


                bindDataGrid();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvLVDetails.ClientID + "');");
         
            }
            //manualattnddeletedate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        void bindDataGrid()
        {
            try
            {
                //string strsql = "SELECT OD_Req_Employee_ID,convert(varchar(10),OD_Req_Date,103) as OD_Req_Date ,OD_Req_Mode,convert(varchar(10),OD_Req_Start_Date,103) as OD_Req_Start_Date," +
                //    "convert(varchar(10),OD_Req_End_Date,103) as OD_Req_End_Date,OD_Req_Duration,OD_Req_Type,Reason_Description,OD_Req_SanctionedBy " +
                //    "FROM TA_ODFDTR,ENT_Reason where OD_Req_ReasonID = Reason_ID AND Reason_Type = 'OD' and OD_IsDeleted = 'False' ORDER BY TA_ODFDTR.Rec_ID DESC";

                string strsql = "";

                strsql += "  select   Rec_ID ,od_req_employee_id , " +
                          " convert(varchar(10),od_req_date,103) as od_req_date, " +
                          " convert(varchar(10),od_req_start_date,103) as od_req_start_date, " +
                          " convert(varchar(10),od_req_end_date,103) as od_req_end_date ,EPD_FIRST_NAME+' '+EPD_LAST_NAME    as NAME" +
                          " from TA_ODFDTR O  INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS E " +
                          " ON E.EPD_EMPID=O.od_req_employee_id " +
                          " INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS EOD " +
                          " ON E.EPD_EMPID= EOD.EOD_EMPID " +
                          " and OD_IsDeleted = 0 AND EPD_ISDELETED='0' and EOD_ACTIVE='1' ";

                SqlConnection conn = new SqlConnection(m_connectons);
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandTimeout = 0;
                SqlDataAdapter daLVdetails = new SqlDataAdapter(cmd);
                DataTable dtLVdetails = new DataTable();
                daLVdetails.Fill(dtLVdetails);
                gvLVDetails.DataSource = dtLVdetails;
                gvLVDetails.DataBind();
                if (dtLVdetails.Rows.Count != 0)
                {
                 
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;

                    DropDownList ddl = (DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvLVDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLVDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLVDetails.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvLVDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLVDetails.PageCount == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex + 1 == gvLVDetails.PageCount)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1));

                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (((gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1)) - 10) + gvLVDetails.Rows.Count);

                    gvLVDetails.BottomPagerRow.Visible = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                this.messageDiv.InnerHtml = ex.Message;
                string someScript1 = "";
                someScript1 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "onload", someScript1);
            }

        }

        protected void gvLVDetails_DataBound(object sender, EventArgs e)
        {
            string strsql = "  select   Rec_ID ,od_req_employee_id , " +
                          " convert(varchar(10),od_req_date,103) as od_req_date, " +
                          " convert(varchar(10),od_req_start_date,103) as od_req_start_date, " +
                          " convert(varchar(10),od_req_end_date,103) as od_req_end_date " +
                          " from TA_ODFDTR O  INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS E " +
                          " ON E.EPD_EMPID=O.od_req_employee_id " +
                          " INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS EOD " +
                          " ON E.EPD_EMPID= EOD.EOD_EMPID " +
                          " and OD_IsDeleted = 0 AND EPD_ISDELETED='0' and EOD_ACTIVE='1' ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;

        }

        protected void gvLVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLVDetails.PageIndex = e.NewPageIndex;
            bindDataGrid();
        }

        protected void gvLVDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLVDetails.EditIndex = e.NewEditIndex;
            string strRecID = ((Label)gvLVDetails.Rows[gvLVDetails.EditIndex].FindControl("lblId")).Text;
            //string strRecID = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[2].Text;
            string ID = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[3].Text;
            // string ReqDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[4].Text;
            string FrmDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[4].Text;
            string ToDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[5].Text;

            Response.Redirect("ODAppEdit.aspx?id=" + strRecID);
        }

        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvLVDetails.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ODAdd.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool Check = false;
            bool marked = false;
            SqlConnection conn = new SqlConnection(AccessController.m_connecton);
            conn.Open();
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            for (int i = 0; i < gvLVDetails.Rows.Count; i++)
            {
                try
                {

                    CheckBox delrows = (CheckBox)gvLVDetails.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        string rowid = ((Label)gvLVDetails.Rows[i].FindControl("lblId")).Text;
                        if (marked == false)
                        {
                            marked = true;
                        }

                       // objcmd.CommandText = "Update TA_ODFDTR set OD_IsDeleted = '1',OD_DeletedDate = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) " +
                         //                    "where OD_Req_Employee_ID = '" + gvLVDetails.Rows[i].Cells[3].Text + "'  " +
                           //                  "and Rec_ID = '" + rowid + "'";

                        //objcmd.ExecuteNonQuery();

                        objcmd.CommandType = CommandType.StoredProcedure;
                        objcmd.CommandText = "Proc_Delete_HRENTRY";
                        objcmd.Parameters.AddWithValue("@pREQ_RECID", rowid);
                        objcmd.Parameters.AddWithValue("@EntityFlag", "OD");
                        objcmd.ExecuteNonQuery();

                        Check = true;
                    
                    }

                }

                catch (Exception ex)
                {
                    this.messageDiv.InnerHtml = ex.Message;
                    string someScript1 = "";
                    someScript1 = "<script language='javascript'>setTimeout(\"clearFunction('messageDiv')\",2000);</script>";
                    Page.ClientScript.RegisterStartupScript(GetType(), "onload", someScript1);
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
            conn.Close();
            bindDataGrid();

        }

        protected void gvLVDetails_sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            string strsql = "  select   Rec_ID ,od_req_employee_id , " +
                          " convert(varchar(10),od_req_date,103) as od_req_date, " +
                          " convert(varchar(10),od_req_start_date,103) as od_req_start_date, " +
                          " convert(varchar(10),od_req_end_date,103) as od_req_end_date,EPD_FIRST_NAME+' '+EPD_LAST_NAME as NAME " +
                          " from TA_ODFDTR O  INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS E " +
                          " ON E.EPD_EMPID=O.od_req_employee_id " +
                          " INNER JOIN ENT_EMPLOYEE_OFFICIAL_DTLS EOD " +
                          " ON E.EPD_EMPID= EOD.EOD_EMPID " +
                          " and OD_IsDeleted = 0 AND EPD_ISDELETED='0' and EOD_ACTIVE='1' ";

            SqlDataAdapter daLVdetails = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dtLVdetails = new DataTable();
            daLVdetails.Fill(dtLVdetails);
            if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"od_req_employee_id~" +txtCompanyId.Text.Trim(), "S" },
				{"NAME~" +txtCompanyName.Text.Trim(), "S" }			
				 };
                DataTable _tempDT = new DataTable();
                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, dtLVdetails);
                gvLVDetails.DataSource = _tempDT;
                gvLVDetails.DataBind();
                if (_tempDT.Rows.Count > 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;

                    DropDownList ddl = (DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvLVDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvLVDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvLVDetails.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvLVDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvLVDetails.PageCount == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex + 1 == gvLVDetails.PageCount)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvLVDetails.PageIndex == 0)
                    {
                        ((Button)gvLVDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    //((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1));

                    ((Label)gvLVDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvLVDetails.PageSize * gvLVDetails.PageIndex) + 1) + " to " + (((gvLVDetails.PageSize * (gvLVDetails.PageIndex + 1)) - 10) + gvLVDetails.Rows.Count);
                    
                    gvLVDetails.BottomPagerRow.Visible = true;
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
            bindDataGrid();
            lblMessages.Text = "";

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = Convert.ToInt32(((DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
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
                gvLVDetails.PageIndex = gvLVDetails.PageIndex - 1;
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
                gvLVDetails.PageIndex = gvLVDetails.PageIndex + 1;
                bindDataGrid();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {
               
            }
        }



    }

}
