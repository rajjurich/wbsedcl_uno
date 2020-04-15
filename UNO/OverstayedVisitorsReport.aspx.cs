using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using Microsoft.Reporting.WebForms;


namespace UNO
{
    public partial class OverstayedVisitorsReport : System.Web.UI.Page
    {
        public string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                txtFromDate.Text = "01" + "/" + strMonth + "/" + DateTime.Now.Year;
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MM/yyyy");
                txtTime.Text = "17:00";
                btnView.Attributes.Add("onclick", "javascript:InProgressLoading(true);");

            }
        }
        protected void View_Click(object sender, EventArgs e)
        {
            btnClose.Visible = true;
            viewer.Visible = true;
            ReportViewer1.Visible = true;
            ShowReport();
        }
        private void ShowReport()
        {
            try
            {
                SqlDataAdapter da;
                DataTable dtCompany,dtData;
                string comp = string.Empty, compAdd = string.Empty;
                ReportViewer1.LocalReport.ReportPath = "RDLC\\Over Stayed Vsitors.rdlc";
                ReportViewer1.Visible = true;
                string FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                string ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                dtData = new DataTable();
                da = new SqlDataAdapter("exec USP_rpt_GetVisitorsDetails @RptType='Over',@dtFromDate='" + FromDate + "',@dtToDate='" + ToDate + "',@dtTime='"+txtTime.Text.Trim()+"'", m_connectons);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dtData);

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "dsOverStayed";
                
                da = new SqlDataAdapter("select * from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", m_connectons);
                dtCompany = new DataTable();
                da.Fill(dtCompany);

                if (dtCompany!=null && dtCompany.Rows.Count > 0)
                {
                    comp = dtCompany.Rows[0]["value"].ToString();
                    compAdd = dtCompany.Rows[1]["value"].ToString();
                }

                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs from " + txtFromDate.Text + " to " + txtToDate.Text;
                ReportParameter para = new ReportParameter("ReportHeader", strReportHeader);
                ReportParameter header = new ReportParameter("Company", comp);
                ReportParameter address = new ReportParameter("address", compAdd);

                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, header, address });

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dtData;
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                HeadPanel.Visible = false;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, new FileInfo(this.Request.Url.LocalPath).Name);
            }
        }
    }
}