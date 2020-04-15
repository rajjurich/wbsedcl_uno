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
using System.Globalization;
namespace UNO
{
    public partial class ManualAttendEdit : System.Web.UI.Page
    {
        string strIdEmp;
        string strSwipdt;
        string strSwipMode;
        static string FDate = "", TDate = "";

        public static string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Literal Ltl = (Literal)Page.Master.FindControl("PopupVis");


            if (!Page.IsPostBack)
            {


                strIdEmp = Convert.ToString(Request.QueryString["id"]);
                ViewState["MA_ID"] = strIdEmp;


                strSwipdt = Convert.ToString(Request.QueryString["RequstDate"]);

                strSwipMode = Convert.ToString(Request.QueryString["Mod"]);

                //Date = txtFromDate.Text;

                //Rowid = Convert.ToString(Request.QueryString["id"]);

                //fillList();
                FillReason();
                //fillSanctionedby();


                fillModifydata(strIdEmp);
                FDate = txtFromDate.Text;
                TDate = txtToDate.Text;
                // Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");
            }

            //Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>ShowMode()</script>");

        }

        public void fillModifydata(string strIdEmp)
        {

            string strsql = "";


            strsql += "   select	ma_empid ";
            strsql += "   ,ENT_EMPLOYEE_PERSONAL_DTLS.EPD_FIRST_NAME +' ' + ENT_EMPLOYEE_PERSONAL_DTLS.EPD_LAST_NAME AS NAME ";
            strsql += "   ,ma_swipefromdate  as MA_SWIPEDATE ";
            //,ma_swipetodate 
            strsql += "  , convert(varchar,MA_SWIPEFROMDATE,103) as FromDate ";
            strsql += "  , convert(varchar,MA_SWIPETODATE,103) as ToDate ";
            strsql += "   ,CONVERT(VARCHAR(5),ma_swipefromtime,108) as IN_TIME ";
            strsql += "   ,CONVERT(VARCHAR(5),ma_swipetotime,108)  AS OUT_TIME ";

            strsql += " , ma_reasonid";
            //   strsql += " ,ma_userid";
            strsql += " ,ma_mode";

            strsql += "   from TA_Manual_Att,ENT_EMPLOYEE_PERSONAL_DTLS ";
            strsql += "   where ENT_EMPLOYEE_PERSONAL_DTLS.EPD_EMPID=TA_Manual_Att.MA_EMPID ";
            strsql += "   and MA_ISDELETED='0' and MA_RECID='" + strIdEmp + "'";



            SqlDataAdapter da = new SqlDataAdapter(strsql, m_connectons);
            DataTable dt = new DataTable();
            da.Fill(dt);


            txtEmployeeName.Text = dt.Rows[0]["name"].ToString();
            frm_time.Text = dt.Rows[0]["IN_TIME"].ToString();

            txtFromDate.Text = dt.Rows[0]["FromDate"].ToString();
            txtToDate.Text = dt.Rows[0]["ToDate"].ToString();

            To_Time.Text = dt.Rows[0]["OUT_TIME"].ToString();
            txtEmployeeCode.Text = dt.Rows[0]["ma_empid"].ToString();
            //ddlModeType.SelectedValue = dt.Rows[0]["ma_mode"].ToString();
            ddlReasonType.SelectedValue = dt.Rows[0]["ma_reasonid"].ToString();
            //  txtSanctnedCode.Text = dt.Rows[0]["ma_userid"].ToString();

            if (dt.Rows[0]["ma_mode"].ToString() == "I")
            {
                frm_time.Text = dt.Rows[0]["IN_TIME"].ToString();
                ddlModeType.SelectedValue = "I";


            }

            if (dt.Rows[0]["ma_mode"].ToString() == "N")
            {
                frm_time.Text = dt.Rows[0]["IN_TIME"].ToString();
                To_Time.Text = dt.Rows[0]["OUT_TIME"].ToString();
                ddlModeType.SelectedValue = "N";


            }
            else if (dt.Rows[0]["ma_mode"].ToString() == "O")
            {
                To_Time.Text = dt.Rows[0]["OUT_TIME"].ToString();
                ddlModeType.SelectedValue = "O";

            }


            txtEmployeeCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            txtSanctnedCode.Enabled = false;

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
          
            Response.Redirect("ManualAttendView.aspx");

        }

     

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(m_connectons);
            conn.Open();

            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;

            DateTime tascfrmdt, tascTodt;
            tascfrmdt = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            tascTodt = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            try
            {


                objcmd.CommandText = "Proc_Update_HRENTRY";
                objcmd.CommandType = CommandType.StoredProcedure;

                objcmd.Parameters.AddWithValue("@pREQ_RECID", ViewState["MA_ID"].ToString());
                objcmd.Parameters.AddWithValue("@EntityFlag", "MA");
                objcmd.Parameters.AddWithValue("@ReasonCode", ddlReasonType.SelectedValue.ToString());

                objcmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text);
                objcmd.Parameters.AddWithValue("@LeaveCode", "");

                objcmd.Parameters.AddWithValue("@leaveFromdate", DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null));
                objcmd.Parameters.AddWithValue("@leaveTodate", DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null));


                //System.Data.SqlTypes.SqlDateTime dtFromTime = System.Data.SqlTypes.SqlDateTime.Parse("01/01/1900 " + frm_time.Text + ":00");
                //System.Data.SqlTypes.SqlDateTime dtToTime = System.Data.SqlTypes.SqlDateTime.Parse("01/01/1900 " + To_Time.Text + ":00");
                
                string fromTime = "01/01/1900 " + frm_time.Text;
                string ToTime = "01/01/1900 " + To_Time.Text;

                objcmd.Parameters.AddWithValue("@from_time", fromTime);
                objcmd.Parameters.AddWithValue("@to_time", ToTime);

                objcmd.Parameters.AddWithValue("@lvDays", "");
                objcmd.Parameters.AddWithValue("@MA_Mode", "N");

                objcmd.Parameters.AddWithValue("@output", "").Direction = ParameterDirection.Output;
                objcmd.Parameters["@output"].Size = 1000;
                objcmd.ExecuteNonQuery();
                lblmsg.Text = "Record Updated Successfully";
                conn.Close();


            }

            catch (Exception ex)
            {
                //this.messageDiv.InnerHtml = ex.Message;
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ManualAtt");
                conn.Close();
                conn.Dispose();

            }

        }

    }

}
