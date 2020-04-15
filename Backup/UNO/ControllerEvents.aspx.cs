using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;


namespace UNO.RDLC
{
    public partial class ControllerEvents : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDL();
                loadDDlController();
            }
        }

        void loadDDL()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";

            cmd.Parameters.AddWithValue("@cmd", 10);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlMonth.Items.Add("Select One");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlMonth.Items.Add(dt.Rows[i][0].ToString());
                }
            }
            con.Close();
        }

        public void loadDDlController()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";
            cmd.Parameters.AddWithValue("@cmd", 12);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlControllerId.Items.Add("All");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlControllerId.Items.Add(dt.Rows[i]["CTLR_ID"].ToString());
                }
            }

        }

        public void loadReport()
        {
            try
            {
              
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "procReport";

                cmd.Parameters.AddWithValue("@cmd", 11);
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@fromdate", txtFrom.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@todate", txtTo.SelectedItem.Text.Trim());

                if (ddlControllerId.SelectedItem.Text != "All")
                    cmd.Parameters.AddWithValue("@id", ddlControllerId.SelectedItem.Text);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/CotrollerEvent.rdlc");

                string comp = "", compAdd = "";
                DataTable dtComp = clsCommonHandler.GetReportHeader();

                if (dtComp.Rows.Count > 0)
                {
                    comp = dtComp.Rows[0]["value"].ToString();
                    compAdd = dtComp.Rows[1]["value"].ToString();
                }

                string strReportHeader = "Controller Events Detail";
                ReportParameter para = new ReportParameter("header", strReportHeader);
                ReportParameter header = new ReportParameter("compHeader", comp);
                ReportParameter address = new ReportParameter("address", compAdd);
                string strReportHeader1 = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
                ReportParameter para2 = new ReportParameter("myparameter1", strReportHeader1);
                ReportParameter para3 = new ReportParameter("count", dt.Rows.Count.ToString());
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para2, para3, header, address });
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.Visible = true;

                ReportDataSource rptData = new ReportDataSource();

                rptData.Name = "DataSet1";
                rptData.Value = dt;
                ReportViewer1.LocalReport.DataSources.Add(rptData);
                ReportViewer1.LocalReport.Refresh();
               
            }
            catch (Exception ex)
            {                
               UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnView_Click1(object sender, EventArgs e)
        {
            Body.Visible = true;
            Header.Visible = false;
            loadReport();
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Body.Visible = false;
            Header.Visible = true;
        }

    }
}