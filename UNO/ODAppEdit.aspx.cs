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
    public partial class ODAppEdit : System.Web.UI.Page
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

                txt_Remarks.Attributes.Add("maxlength", txt_Remarks.MaxLength.ToString());
                strId = Convert.ToString(Request.QueryString["id"]);

                ViewState["OD_ID"] = strId;

                //strSwipdt = Convert.ToString(Request.QueryString["RequstDate"]);

                //strSwipMode = Convert.ToString(Request.QueryString["Mod"]);


                //Rowid = Convert.ToString(Request.QueryString["id"]);





                //fillList();
                FillReason();
                //fillSanctionedby();


                fillModifydata(strId);
                caltxtLeaveFrDt.EndDate = DateTime.Now.Date;
                calTxtLeaveTodt.EndDate = DateTime.Now.Date;

                // Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");
            }
            //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");

        }


        public void fillModifydata(string strIdEmp)
        {
            //string strsql = "select ma_recid,ma_empid, (Epd_first_name + ' ' + epd_last_name)as name ,convert(varchar(5),ma_swipetime,114) as ma_swipetime,Convert(datetime,ma_swipedate,103) as ma_swipedate, ma_mode,ma_processflag,ma_reasonid,ma_userid from TA_Manual_att,ENT_EMPLOYEE_PERSONAL_DTLS where ma_isdeleted='0' and ta_manual_att.ma_empid=ENT_EMPLOYEE_PERSONAL_DTLS.epd_empid and ta_manual_att.ma_recid='" + strIdEmp +  "'";


            string strsql = "SELECT REC_ID,OD_REQ_EMPLOYEE_ID,ENT_EMPLOYEE_PERSONAL_DTLS.EPD_FIRST_NAME+ ' ' + epd_last_name as EmpName,convert(varchar(10),OD_REQ_DATE,103) as OD_REQ_DATE ,OD_REQ_TYPE, " +
                                "convert(varchar(10),OD_REQ_START_DATE,103) as OD_REQ_START_DATE,convert(varchar(10),OD_Req_End_Date,103) as OD_REQ_TODATE, " +
                                "OD_REQ_DURATION,OD_REQ_REMARK,OD_Req_SanctionedBy ,OD_Req_Remark FROM TA_ODFDTR,ENT_EMPLOYEE_PERSONAL_DTLS  where OD_IsDeleted = 0" +
                                "and TA_ODFDTR.OD_REQ_EMPLOYEE_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID and REC_ID='" + strId + "'  ORDER BY REC_ID DESC ";

            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);



            // ddlHolidayType.Enabled = false;
            //   txtHolidayid.ReadOnly = true;
            //MA_SWIPEDATE.Text = Convert.ToDateTime(dt.Rows[0]["ma_swipedate"]).ToString("dd/MM/yyyy");

            txtEmployeeCode.Text = dt.Rows[0]["OD_REQ_EMPLOYEE_ID"].ToString();

            txtEmployeeName.Text = dt.Rows[0]["EmpName"].ToString();

            txtLeaveFrDt.Text = Convert.ToDateTime(dt.Rows[0]["OD_REQ_START_DATE"]).ToString("dd/MM/yyyy");

            TxtLeaveTodt.Text = Convert.ToDateTime(dt.Rows[0]["OD_REQ_TODATE"]).ToString("dd/MM/yyyy");

            Session["LeaveRequestDt"] = Convert.ToDateTime(dt.Rows[0]["OD_REQ_DATE"]).ToString("dd/MM/yyyy");

            Session["OldLeaveFrDate"] = Convert.ToDateTime(dt.Rows[0]["OD_REQ_START_DATE"]).ToString("dd/MM/yyyy");

            Session["OldLeaveToDate"] = Convert.ToDateTime(dt.Rows[0]["OD_REQ_TODATE"]).ToString("dd/MM/yyyy");

            ddlODType.Text = dt.Rows[0]["OD_REQ_TYPE"].ToString();


            //ddlReasonType.SelectedValue = dt.Rows[0]["EmpName"].ToString();

            txt_Remarks.Text = dt.Rows[0]["OD_Req_Remark"].ToString();

            ddlSanctionCode.SelectedValue = dt.Rows[0]["OD_Req_SanctionedBy"].ToString();




            txtEmployeeCode.Enabled = false;

            txtEmployeeName.Enabled = false;









            //holidate = Convert.ToDateTime(dt.Rows[0][3]).ToString("dd/MM/yyyy");
            //txtholidaydate.Value = holidate;

            //holiswap = Convert.ToDateTime(dt.Rows[0][4]).ToString("dd/MM/yyyy");

        }




        private void FillReason()
        {
            string strSql = "select Reason_ID,Reason_Description from ENT_PARAMS,ENT_Reason where CODE = Reason_Type " +
                            "and  MODULE = 'ENT' and IDENTIFIER = 'REASONTYPE' and  Reason_Type = 'MA' ";
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
            //strId = Convert.ToString(Request.QueryString["id"]);
            //fillModifydata(strId);
            //this.messageDiv.InnerHtml = "";
            Response.Redirect("ODAppView.aspx");

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
                objcmd.CommandText = "Update TA_ODFDTR set OD_REQ_START_DATE = convert(datetime,'" + txtLeaveFrDt.Text + "',103), OD_REQ_END_DATE = convert(datetime,'" + TxtLeaveTodt.Text + "',103), " +
                                      "OD_REQ_DURATION = " + LvDays + ",OD_REQ_TYPE = '" + ddlODType.SelectedValue.ToString() + "',OD_REQ_REASONID = '" + ddlReasonType.SelectedValue.ToString() + "', " +
                                      "OD_REQ_REMARK = '" + txt_Remarks.Text.ToString() + "',OD_IsDeleted = 'False' where OD_REQ_EMPLOYEE_ID = '" + txtEmployeeCode.Text + "' and OD_REQ_DATE = convert(datetime,'" + Session["LeaveRequestDt"] + "',103) " +
                                      "and OD_REQ_START_DATE = convert(datetime,'" + Session["OldLeaveFrDate"] + "',103) and OD_REQ_END_DATE = convert(datetime,'" + Session["OldLeaveToDate"] + "',103) ";
                */


                string txtEntity = "OD";

                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.CommandText = "Proc_Update_HRENTRY";

                objcmd.Parameters.AddWithValue("@pREQ_RECID", ViewState["OD_ID"].ToString());
                objcmd.Parameters.AddWithValue("@EntityFlag", txtEntity);
                objcmd.Parameters.AddWithValue("@ReasonCode", ddlReasonType.SelectedValue.ToString());
                objcmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                objcmd.Parameters.AddWithValue("@LeaveCode", ddlODType.SelectedValue.ToString());

                // objcmd.Parameters.AddWithValue("@LeaveCode", LvDays);

                objcmd.Parameters.AddWithValue("@leaveFromdate", DateTime.ParseExact(txtLeaveFrDt.Text, "dd/MM/yyyy", null));

                if (TxtLeaveTodt.Text != "")
                {
                    objcmd.Parameters.AddWithValue("@leaveTodate", DateTime.ParseExact(TxtLeaveTodt.Text, "dd/MM/yyyy", null));
                }
                else
                {
                    objcmd.Parameters.AddWithValue("@leaveTodate", DateTime.ParseExact(txtLeaveFrDt.Text, "dd/MM/yyyy", null));
                }

                objcmd.Parameters.AddWithValue("@from_time", "");
                objcmd.Parameters.AddWithValue("@to_time", "");
                objcmd.Parameters.AddWithValue("@MA_Mode", "");


                objcmd.ExecuteNonQuery();
                // this.messageDiv.InnerHtml = "Record Saved Successfully";
                lblMessages.Text = "Record Updated Successfully";
                conn.Close();

                string someScript3 = "";
                // someScript3 = "<script language='javascript'>setTimeout(\"clearMessageDiv('messageDiv')\",2000);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript3);

            }

            catch (Exception ex)
            {
                //this.messageDiv.InnerHtml = ex.ToString();
                conn.Close();
                conn.Dispose();
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "OdEdit");

            }



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                UpdateLeaveData();
            }
        }








    }



}
