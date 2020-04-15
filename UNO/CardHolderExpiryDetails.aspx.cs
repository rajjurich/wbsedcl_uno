using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;


namespace UNO
{
    public partial class CardHolderExpiryDetails : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.Visible = true;
                ReportViewer2.Visible = false;
                rdbtnEmployeeWise.Checked = true;
                loadReport(7);
            }
        }

        public void loadReport(int i)
        {
            ReportViewer1.Visible = true;
            ReportViewer2.Visible = false;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";

            cmd.Parameters.AddWithValue("@cmd", i);


            if (i == 5)
            {
                cmd.Parameters.AddWithValue("@empname", txtName.Text.Trim());
            }
            if (i == 6)
            {
                cmd.Parameters.AddWithValue("@empcode", txtEmpCode.Text.Trim());
            }
            if (i == 5 || i == 6)
            {
                cmd.Parameters.AddWithValue("@fromdate", txtFrom.Text.Trim());
                cmd.Parameters.AddWithValue("@todate", txtTo.Text.Trim());
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/CardholderActivendetails.rdlc");
          
            string strReportHeader = "Card Holder Expiry Details";
            ReportParameter para = new ReportParameter("header", strReportHeader);
            ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });




            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.Visible = true;

            ReportDataSource rptData = new ReportDataSource();

            rptData.Name = "DataSet1";
            rptData.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(rptData);
            ReportViewer1.LocalReport.Refresh();
            con.Close();


        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (rdbtnEmployeeWise.Checked == true)
            {
                if (rdbtnEmpCode.Checked == true)
                {
                    loadReport(6);
                }
                if (rdbtnEmpName.Checked == true)
                {
                    loadReport(5);
                }
            }
            else
            {
                if (rdbtnEmpCode.Checked == true)
                {
                    loadReport2(6);
                }
                if (rdbtnEmpName.Checked == true)
                {
                    loadReport2(5);
                }
            }
        }
        public void loadReport2(int i)
        {
            ReportViewer1.Visible = false;
            ReportViewer2.Visible = true;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";

            cmd.Parameters.AddWithValue("@cmd", i);


            if (i == 5)
            {
                cmd.Parameters.AddWithValue("@empname", txtName.Text.Trim());
            }
            if (i == 6)
            {
                cmd.Parameters.AddWithValue("@empcode", txtEmpCode.Text.Trim());
            }
            if (i == 5 || i == 6)
            {
                cmd.Parameters.AddWithValue("@fromdate", txtFrom.Text.Trim());
                cmd.Parameters.AddWithValue("@todate", txtTo.Text.Trim());
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/RDLC/CardholderActivationdetails2.rdlc");


            //string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
            string strReportHeader = "Card Holder Expiry Details";
            ReportParameter para = new ReportParameter("header", strReportHeader);
            ReportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para });




            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.Visible = true;

            ReportDataSource rptData = new ReportDataSource();

            rptData.Name = "DataSet1";
            rptData.Value = dt;
            ReportViewer2.LocalReport.DataSources.Add(rptData);
            ReportViewer2.LocalReport.Refresh();
            con.Close();


        }
        protected void rdbtnEmpCode_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtName.Enabled = false;
            txtEmpCode.Enabled = true;
        }

        protected void rdbtnEmpName_CheckedChanged(object sender, EventArgs e)
        {
            txtEmpCode.Text = "";
            txtEmpCode.Enabled = false;
            txtName.Enabled = true;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmpCode.Text = "";
            txtFrom.Text = "";
            txtName.Text = "";
            txtTo.Text = "";
            ReportViewer1.Visible = true;
            ReportViewer2.Visible = false;
            rdbtnEmployeeWise.Checked = true;
            loadReport(7);

        }
    }
}