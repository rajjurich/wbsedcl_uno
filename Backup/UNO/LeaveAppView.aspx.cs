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
    public partial class LeaveAppView : System.Web.UI.Page
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
                string strsql = " SELECT LVREQ_RECID,LVREQ_EMPID,convert(varchar(10),LVREQ_REQDATE,103) as LVREQ_REQDATE, "+
                                " LVREQ_LV_ID, convert(varchar(10),LVREQ_FROMDATE,103) as LVREQ_FROMDATE,convert(varchar(10),LVREQ_TODATE,103) as LVREQ_TODATE, "+
                                "  LVREQ_LVDAYS,LVTLEV_STATUS,LVREQ_SANCTION_EMPID  FROM TA_LEAVE_REQ inner join ENT_EMPLOYEE_PERSONAL_DTLS e "+
                                "  on LVREQ_EMPID=e.EPD_EMPID inner join ENT_EMPLOYEE_OFFICIAL_DTLS o "+
                                "  on e.EPD_EMPID=o.EOD_EMPID where LVREQ_IsDeleted = '0' and EPD_ISDELETED='0' "+
                                "  and EOD_ACTIVE='1' ORDER BY LVREQ_RECID DESC "; 

                SqlDataAdapter daLVdetails = new SqlDataAdapter(strsql, AccessController.m_connecton);
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
            string strsql = " SELECT LVREQ_RECID,LVREQ_EMPID,convert(varchar(10),LVREQ_REQDATE,103) as LVREQ_REQDATE, " +
                                " LVREQ_LV_ID, convert(varchar(10),LVREQ_FROMDATE,103) as LVREQ_FROMDATE,convert(varchar(10),LVREQ_TODATE,103) as LVREQ_TODATE, " +
                                "  LVREQ_LVDAYS,LVTLEV_STATUS,LVREQ_SANCTION_EMPID  FROM TA_LEAVE_REQ inner join ENT_EMPLOYEE_PERSONAL_DTLS e " +
                                "  on LVREQ_EMPID=e.EPD_EMPID inner join ENT_EMPLOYEE_OFFICIAL_DTLS o " +
                                "  on e.EPD_EMPID=o.EOD_EMPID where LVREQ_IsDeleted = '0' and EPD_ISDELETED='0' " +
                                "  and EOD_ACTIVE='1' ORDER BY LVREQ_RECID DESC ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;

            if (gvLVDetails.Rows.Count > 0)
            {
               

            }
            else
            {
                
            }
        }

        protected void gvLVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLVDetails.PageIndex = e.NewPageIndex;
            bindDataGrid();
        }

        protected void gvLVDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLVDetails.EditIndex = e.NewEditIndex;
            string ID = ((Label)gvLVDetails.Rows[gvLVDetails.EditIndex].FindControl("lblId")).Text;
           // string ID = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[2].Text;
            string ReqDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[3].Text;
            string FrmDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[6].Text;
            string ToDate = gvLVDetails.Rows[gvLVDetails.EditIndex].Cells[7].Text;

            Response.Redirect("LeaveAppEdit.aspx?id=" + ID  );
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
            Response.Redirect("LeaveAdd.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
             SqlConnection conn = new SqlConnection(AccessController.m_connecton);
             if (conn.State == ConnectionState.Closed)
             {
                 conn.Open();
             }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            bool Check = false;
            bool marked = false;
            for (int i = 0; i < gvLVDetails.Rows.Count; i++)
            {
                try
                {

                    CheckBox delrows = (CheckBox)gvLVDetails.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        if (marked == false)
                        {
                            marked = true;
                        }



                        string id = ((Label)gvLVDetails.Rows[i].FindControl("lblId")).Text;
                       // objcmd.CommandText = "Update TA_LEAVE_REQ set LVREQ_IsDeleted = '1',LVREQ_DeletedDate = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) " +
                       //                      "where LVREQ_RECID = '" + id + "' ";
                       // int result= objcmd.ExecuteNonQuery();




                        objcmd.CommandType = CommandType.StoredProcedure;
                        objcmd.CommandText = "Proc_Delete_HRENTRY";
                        objcmd.Parameters.AddWithValue("@pREQ_RECID", id);
                        objcmd.Parameters.AddWithValue("@EntityFlag", "Leave");
                        objcmd.ExecuteNonQuery();

                        Check = true;
                    }

                }

                catch (Exception ex)
                {
                    this.messageDiv.InnerHtml = ex.Message;
                    string someScript1 = "";
                    someScript1 = "<script language='javascript'>setTimeout(\"clearFunction('ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$lblMessages')\",2000);</script>";
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
            string someScript11 = "";
            someScript11 = "<script language='javascript'>setTimeout(\"clearFunction('ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$lblMessages')\",3000);</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "onload", someScript11);
            conn.Close();
            bindDataGrid();
        }

        protected void gvLVDetails_sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            string strsql = " SELECT LVREQ_RECID,LVREQ_EMPID,convert(varchar(10),LVREQ_REQDATE,103) as LVREQ_REQDATE, " +
                                " LVREQ_LV_ID, convert(varchar(10),LVREQ_FROMDATE,103) as LVREQ_FROMDATE,convert(varchar(10),LVREQ_TODATE,103) as LVREQ_TODATE, " +
                                "  LVREQ_LVDAYS,LVTLEV_STATUS,LVREQ_SANCTION_EMPID  FROM TA_LEAVE_REQ inner join ENT_EMPLOYEE_PERSONAL_DTLS e " +
                                "  on LVREQ_EMPID=e.EPD_EMPID inner join ENT_EMPLOYEE_OFFICIAL_DTLS o " +
                                "  on e.EPD_EMPID=o.EOD_EMPID where LVREQ_IsDeleted = '0' and EPD_ISDELETED='0' " +
                                "  and EOD_ACTIVE='1' ORDER BY LVREQ_RECID DESC ";

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
				{"LVREQ_EMPID~" +txtCompanyId.Text.Trim(), "S" },
				{"LVREQ_LV_ID~" +txtCompanyName.Text.Trim(), "S" }			
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
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;

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

        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = Convert.ToInt32(((DropDownList)gvLVDetails.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveAppView");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = gvLVDetails.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveAppView");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvLVDetails.PageIndex = gvLVDetails.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "LeaveAppView");
            }
        }

        
    
      }

    }
