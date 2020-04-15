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
using System.Collections.Generic;

namespace UNO
{
    public partial class NumberofVisitorsReport : System.Web.UI.Page
    {
        public string m_connectons = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                txtFromDate.Text = "01" + "/" + strMonth + "/" + DateTime.Now.Year;
                FillDropdown();
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MM/yyyy");
                btnView.Attributes.Add("onclick", "javascript:return ValidateForm('" + txtFromDate.ClientID + "','" + txtToDate.ClientID + "' );");
            }
        }
        private void FillDropdown()
        {
            //Main Status
            ddlVisitorType.DataSource = UNO.CallManagementHandler.TicketStatusAndCallType("VisitorType", "Visitor");
            ddlVisitorType.DataValueField = "Code";
            ddlVisitorType.DataTextField = "Value";
            ddlVisitorType.DataBind();
            ddlVisitorType.Items.Insert(0, new ListItem("All", "All"));

            chkOptionalColumns.DataSource = getColumnsType();                   
            chkOptionalColumns.DataValueField = "Code";
            chkOptionalColumns.DataTextField = "Value";
            chkOptionalColumns.DataBind();
           
            foreach (ListItem item in chkOptionalColumns.Items)
            {
                item.Attributes.Add("style", "margin-left:3px;");
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
                DataTable dtCompany;
                DataSet dsData;
                List<String> lstOptionalColumns = new List<string>();
                string[] par;
                foreach (ListItem item in chkOptionalColumns.Items)
                {
                    if (item.Selected)
                        lstOptionalColumns.Add(item.Value);
                }

                string comp = string.Empty, compAdd = string.Empty;
                ReportViewer1.LocalReport.ReportPath = "RDLC\\Number Of Visitors.rdlc";
                ReportViewer1.Visible = true;

                string FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                string ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                dsData = new DataSet();
                da = new SqlDataAdapter("exec USP_rpt_GetVisitorsDetails @RptType='NOV',@Nationality='" + ddlNationality.SelectedValue.ToString() + "',@dtFromDate='" + FromDate + "',@dtToDate='" + ToDate + "',@VisitorType='" + ddlVisitorType.SelectedValue.ToString() + "',@userid='" + Session["uid"].ToString() + "'", m_connectons);
                da.Fill(dsData);

                //OPEN RDLC,GO TO REPORT MENU ->DATASOURCES ,CHECK NAME OF DATASOURCE USED IN THIS RDLC,
                String dataSetName = "dsNoOfVisitors";

                da = new SqlDataAdapter("select * from ent_params where module='ENT' and identifier='GLOBALREPORTPARAM'", m_connectons);
                dtCompany = new DataTable();
                da.Fill(dtCompany);

                if (dtCompany != null && dtCompany.Rows.Count > 0)
                {
                    comp = dtCompany.Rows[0]["value"].ToString();
                    compAdd = dtCompany.Rows[1]["value"].ToString();
                }

                string strReportHeader = "Report generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs from " + txtFromDate.Text + " to " + txtToDate.Text;
                ReportParameter para = new ReportParameter("ReportHeader", strReportHeader);
                ReportParameter header = new ReportParameter("Company", comp);
                ReportParameter address = new ReportParameter("address", compAdd);                               
                ReportParameter prmdesignation = new ReportParameter("prmdesignation", lstOptionalColumns.Contains("0") ? "P" : "A");
                ReportParameter prmAddress = new ReportParameter("prmAddress", lstOptionalColumns.Contains("1") ? "P" : "A");
                ReportParameter prmCity = new ReportParameter("prmCity", lstOptionalColumns.Contains("2") ? "P" : "A");                
                ReportParameter prmPassportValidTill = new ReportParameter("prmPassportValidTill", lstOptionalColumns.Contains("7") ? "P" : "A");
                ReportParameter prmPassportDetails = new ReportParameter("prmPassportDetails", lstOptionalColumns.Contains("3") ? "P" : "A");
                ReportParameter prmPhone1 = new ReportParameter("prmPhone1", lstOptionalColumns.Contains("4") ? "P" : "A");
                ReportParameter prmPhone2 = new ReportParameter("prmPhone2", lstOptionalColumns.Contains("5") ? "P" : "A");
                ReportParameter prmMobile = new ReportParameter("prmMobile", lstOptionalColumns.Contains("6") ? "P" : "A");
                ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, header, address,
                     prmCity,prmdesignation, prmAddress,prmPassportValidTill,prmPhone1,prmPhone2,prmMobile,prmPassportDetails});

                ReportDataSource datasource = new ReportDataSource();
                datasource.Name = dataSetName;
                datasource.Value = dsData.Tables[0];
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                HeadPanel.Visible = false;
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, new FileInfo(this.Request.Url.LocalPath).Name);
            }
        }
        private DataTable getColumnsType()
        {
            DataSet dsData = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("exec USP_rpt_GetVisitorsDetails @RptType='NOV',@Nationality='" + ddlNationality.SelectedValue.ToString() + "',@dtFromDate='" + "" + "',@dtToDate='" + "" + "',@VisitorType='" + ddlVisitorType.SelectedValue.ToString() + "'", m_connectons);
            da.Fill(dsData);
            return dsData.Tables[1];
        }
    }
}