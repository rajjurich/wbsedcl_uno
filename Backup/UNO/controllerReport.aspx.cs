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
    public partial class controllerReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData(3);
            }
        }
        public void loadData(int i)
        {
            try
            {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/controller.rdlc");          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";
            if (i==1)
            {
                cmd.Parameters.AddWithValue("@cmd", 1);
                cmd.Parameters.AddWithValue("@id", txtControlId.Text.Trim());
            }
            if (i==2)
            {
                cmd.Parameters.AddWithValue("@cmd", 2);
                cmd.Parameters.AddWithValue("@name", txtControlName.Text.Trim());
            }
            if (i == 3)
            {
                cmd.Parameters.AddWithValue("@cmd", 2);
                cmd.Parameters.AddWithValue("@name","");
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string comp = "", compAdd = "";           
            DataTable dtComp = clsCommonHandler.GetReportHeader();
          
            if (dtComp.Rows.Count > 0)
            {
                comp = dtComp.Rows[0]["value"].ToString();
                compAdd = dtComp.Rows[1]["value"].ToString();
            }

            string strReportHeader1 = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
            ReportParameter para2 = new ReportParameter("myparameter1", strReportHeader1);
            string strReportHeader = "Controller Master";
            ReportParameter para = new ReportParameter("header", strReportHeader);
            ReportParameter para4 = new ReportParameter("userName", Session["uid"].ToString() + " " + Session["loginName"].ToString());
            ReportParameter header = new ReportParameter("compHeader", comp);
            ReportParameter address = new ReportParameter("address", compAdd);
            ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para,para2,para4, header,address });
            
            ReportDataSource rptData = new ReportDataSource();            
            rptData.Name = "DataSet1";
            rptData.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(rptData);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;         
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }           
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (rdbtnControlId.Checked == true)
            {
                loadData(1);
            }
            if (rdbtnControlName.Checked == true)
            {
                loadData(2);
            }
            
        }
        protected void rdbtnControlId_CheckedChanged(object sender, EventArgs e)
        {
            txtControlName.Enabled = false;
            txtControlName.Text = "";
            txtControlId.Enabled = true;
        }
        protected void rdbtnControlName_CheckedChanged(object sender, EventArgs e)
        {
            txtControlId.Enabled = false;
            txtControlId.Text = "";
            txtControlName.Enabled = true;
        }
    }
}