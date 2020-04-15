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
    public partial class LeaveAppEdit : System.Web.UI.Page
    {


        string strId;
        string strSwipdt;
        string strSwipMode;
       
        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Literal Ltl = (Literal)Page.Master.FindControl("PopupVis");
         

            if (!Page.IsPostBack)
            {


                strId = Convert.ToString(Request.QueryString["id"]);
                ViewState["LV_ID"] = strId;
                //strSwipdt = Convert.ToString(Request.QueryString["RequstDate"]);

                //strSwipMode = Convert.ToString(Request.QueryString["Mod"]);
               

                //Rowid = Convert.ToString(Request.QueryString["id"]);
             
          


              
                //fillList();
                FillReason();
                //fillSanctionedby();
                FillLeaveTypes();

                fillModifydata(strId);

                // Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");
            }
            //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");
            txtLeaveFrDt.Attributes.Add("readonly", "readonly");
            TxtLeaveTodt.Attributes.Add("readonly", "readonly");
    

        }

        private void FillLeaveTypes()
        {
            string strSql = "select Leave_ID,Leave_Description  from TA_Leave_File where  Leave_ISDELETED = 'FALSE'";
            SqlDataAdapter daType = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtType = new DataTable();
            daType.Fill(dtType);
            ddlLAType.DataValueField = "Leave_ID";
            ddlLAType.DataTextField = "Leave_Description";
            ddlLAType.DataSource = dtType;
            ddlLAType.DataBind();
            ddlLAType.Items.Insert(0, "Select One");

        }
        public void fillModifydata(string strId)
        {
            //string strsql = "select ma_recid,ma_empid, (Epd_first_name + ' ' + epd_last_name)as name ,convert(varchar(5),ma_swipetime,114) as ma_swipetime,Convert(datetime,ma_swipedate,103) as ma_swipedate, ma_mode,ma_processflag,ma_reasonid,ma_userid from TA_Manual_att,ENT_EMPLOYEE_PERSONAL_DTLS where ma_isdeleted='0' and ta_manual_att.ma_empid=ENT_EMPLOYEE_PERSONAL_DTLS.epd_empid and ta_manual_att.ma_recid='" + strIdEmp +  "'";


            string strsql = "SELECT LVREQ_RECID,LVREQ_EMPID,ENT_EMPLOYEE_PERSONAL_DTLS.EPD_FIRST_NAME+ ' ' + epd_last_name as EmpName,convert(varchar(10),LVREQ_REQDATE,103) as LVREQ_REQDATE ,LVREQ_MODE,LVREQ_LV_ID, lvreq_reasonid, " +
                                "convert(varchar(10),LVREQ_FROMDATE,103) as LVREQ_FROMDATE,convert(varchar(10),LVREQ_TODATE,103) as LVREQ_TODATE, " +
                                "LVREQ_LVDAYS,LVTLEV_STATUS,LVREQ_NOTE,LVREQ_SANCTION_EMPID FROM TA_LEAVE_REQ,ENT_EMPLOYEE_PERSONAL_DTLS  where LVREQ_IsDeleted = 'False' " +
                                "and TA_LEAVE_REQ.LVREQ_EMPID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and LVREQ_RECID='" + strId + "'  ORDER BY LVREQ_RECID DESC ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);



            // ddlHolidayType.Enabled = false;
            //   txtHolidayid.ReadOnly = true;
            //MA_SWIPEDATE.Text = Convert.ToDateTime(dt.Rows[0]["ma_swipedate"]).ToString("dd/MM/yyyy");

            txtEmployeeCode.Text = dt.Rows[0]["LVREQ_EMPID"].ToString();

            txtEmployeeName.Text = dt.Rows[0]["EmpName"].ToString();

            //txtLeaveFrDt.Text = Convert.ToDateTime(dt.Rows[0]["LVREQ_FROMDATE"]).ToString("dd/MM/yyyy");

            //TxtLeaveTodt.Text = Convert.ToDateTime(dt.Rows[0]["LVREQ_TODATE"]).ToString("dd/MM/yyyy");

            //Session["LeaveRequestDt"] = Convert.ToDateTime(dt.Rows[0]["LVREQ_REQDATE"]).ToString("dd/MM/yyyy");

            //Session["OldLeaveFrDate"] = Convert.ToDateTime(dt.Rows[0]["LVREQ_FROMDATE"]).ToString("dd/MM/yyyy");

            //Session["OldLeaveToDate"] = Convert.ToDateTime(dt.Rows[0]["LVREQ_TODATE"]).ToString("dd/MM/yyyy");


            txtLeaveFrDt.Text = (dt.Rows[0]["LVREQ_FROMDATE"]).ToString();

            TxtLeaveTodt.Text = (dt.Rows[0]["LVREQ_TODATE"]).ToString();

            Session["LeaveRequestDt"] =(dt.Rows[0]["LVREQ_REQDATE"]).ToString();

            Session["OldLeaveFrDate"] = (dt.Rows[0]["LVREQ_FROMDATE"]).ToString();

            Session["OldLeaveToDate"] = (dt.Rows[0]["LVREQ_TODATE"]).ToString();

            ddlLAType.Text = dt.Rows[0]["LVREQ_LV_ID"].ToString();

            ddlReasonType.SelectedValue = dt.Rows[0]["lvreq_reasonid"].ToString();

            txt_Remarks.Text = dt.Rows[0]["LVREQ_NOTE"].ToString();

            ddlSanctionCode.SelectedValue = dt.Rows[0]["LVREQ_SANCTION_EMPID"].ToString();




            txtEmployeeCode.Enabled = false;

            txtEmployeeName.Enabled = false;


        






            //holidate = Convert.ToDateTime(dt.Rows[0][3]).ToString("dd/MM/yyyy");
            //txtholidaydate.Value = holidate;

            //holiswap = Convert.ToDateTime(dt.Rows[0][4]).ToString("dd/MM/yyyy");

        }
        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                            "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'LA' ";
            SqlDataAdapter daReason = new SqlDataAdapter(strSql, AccessController.m_connecton);
            DataTable dtReason = new DataTable();
            daReason.Fill(dtReason);
            ddlReasonType.DataValueField = "Reason_ID";
            ddlReasonType.DataTextField = "Reason_Description";
            ddlReasonType.DataSource = dtReason;
            ddlReasonType.DataBind();
            ddlReasonType.Items.Insert(0, "Select One");

        }
        protected void Btnclear_Click(object sender, EventArgs e)
        {
          //  strId = Convert.ToString(Request.QueryString["id"]);
          //  fillModifydata(strId);
           // this.messageDiv.InnerHtml = "";
            Response.Redirect("LeaveAppView.aspx");

        }
        protected void lstEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtEmployeeCode.Text = lstEmp.SelectedItem.ToString().Substring(lstEmp.SelectedItem.ToString().Length - 10);
            //txtEmployeeName.Text = lstEmp.SelectedItem.ToString().Substring(0, lstEmp.SelectedItem.ToString().Length - 10);
            //fillSanctionedby();
            //fillAbsentStatus();
            ////bindDataGrid();
        }

        protected void gvStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvStatus.PageIndex = e.NewPageIndex;
            //fillAbsentStatus();
        }

        protected void txtSanctndCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lstSanctioned_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtSanctndCode.Text = lstSanctioned.SelectedItem.ToString().Substring(lstSanctioned.SelectedItem.ToString().Length - 10);
            //txtSanctiondName.Text = lstSanctioned.SelectedItem.ToString().Substring(0, lstSanctioned.SelectedItem.ToString().Length - 10);

        }
        public void UpdateLeaveData()
        {
            DateTime dtfromdt = DateTime.ParseExact(txtLeaveFrDt.Text, "dd/MM/yyyy", null);

            DateTime dtttodt = DateTime.ParseExact(TxtLeaveTodt.Text, "dd/MM/yyyy", null);

            TimeSpan difference = dtttodt - dtfromdt;
            int LvDays = difference.Days + 1;

            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();

            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;

            try
            {
                /*
                   objcmd.CommandType = CommandType.Text;
                    objcmd.CommandText = "Update TA_LEAVE_REQ set LVREQ_FROMDATE = convert(datetime,'" + txtLeaveFrDt.Text + "',103), LVREQ_TODATE = convert(datetime,'" + TxtLeaveTodt.Text + "',103), " +
                                         "LVREQ_LVDAYS = " + LvDays + ",LVREQ_LV_ID = '" + ddlLAType.SelectedValue.ToString() + "',LVREQ_REASONID = '" + ddlReasonType.SelectedValue.ToString() + "', " +
                                         "LVREQ_NOTE = '" + txt_Remarks.Text.ToString() + "',LVREQ_IsDeleted = 'False' where LVREQ_EMPID = '" + txtEmployeeCode.Text + "' and LVREQ_REQDATE = convert(datetime,'" + Session["LeaveRequestDt"] + "',103) " +
                                         "and LVREQ_FROMDATE = convert(datetime,'" + Session["OldLeaveFrDate"] + "',103) and LVREQ_TODATE = convert(datetime,'" + Session["OldLeaveToDate"] + "',103) ";


                    int result=objcmd.ExecuteNonQuery();
                    lblMessage.Text = "Record Updated Successfully";                  
                    conn.Close();
                   if (result > 0)
                    {
                        //ClientScript.RegisterClientScriptBlock(GetType(), "Message", "alert('Record updated successfully');", true);
                        lblMessage.Text = "Record Updated Successfully";
                    }
                */


                    
                     objcmd.CommandText = "Proc_Update_HRENTRY";
                     objcmd.CommandType = CommandType.StoredProcedure;
                    

                             

                   string txtEntity = "Leave";
                   objcmd.Parameters.AddWithValue("@pREQ_RECID", ViewState["LV_ID"].ToString());
                   objcmd.Parameters.AddWithValue("@EntityFlag", txtEntity);
                   objcmd.Parameters.AddWithValue("@ReasonCode", ddlReasonType.SelectedValue.ToString());
                   objcmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                   objcmd.Parameters.AddWithValue("@LeaveCode", ddlLAType.SelectedValue.ToString());
                   // cmd.Parameters.AddWithValue("@SanctionedCode", ddSanctionedID.SelectedValue);

                   objcmd.Parameters.AddWithValue("@leaveFromdate", DateTime.ParseExact(txtLeaveFrDt.Text, "dd/MM/yyyy", null));
                   objcmd.Parameters.AddWithValue("@leaveTodate", DateTime.ParseExact(TxtLeaveTodt.Text, "dd/MM/yyyy", null));
                   objcmd.Parameters.AddWithValue("@from_time", "");
                   objcmd.Parameters.AddWithValue("@to_time", "");
                   objcmd.Parameters.AddWithValue("@lvDays", "full");      
                   objcmd.Parameters.AddWithValue("@MA_Mode", "");  
                   objcmd.Parameters.AddWithValue("@output", '0').Direction = ParameterDirection.Output;
                   objcmd.Parameters["@output"].Size = 1000;
                   objcmd.ExecuteNonQuery();
                   string strMessage = objcmd.Parameters["@output"].Value.ToString();
                   lblMessage.Text = strMessage;

                   this.messageDiv.InnerHtml = strMessage;
               }
            
                 catch(Exception ex)
                 {

                 this.messageDiv.InnerHtml=ex.ToString();
                 conn.Close();
                 conn.Dispose();

                 }

   

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateLeaveData();
        }

       



       
          

        }

        
       
    }
