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
    public partial class AccessLevelReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadReport(3);
                fillDescription();
            }
        }
        public void fillDescription()
        {
        
            SqlCommand cmd = new SqlCommand("select AL_DESCRIPTION from ACS_ACCESSLEVEL where isnull(al_isdeleted,0)=0 ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddDescription.DataSource = dt;
            ddDescription.DataTextField = "AL_DESCRIPTION";
            ddDescription.DataBind();
        
        }


        public void loadReport(int i)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procReport";

            cmd.Parameters.AddWithValue("@cmd", i);


            if (i == 4)
            {
                cmd.Parameters.AddWithValue("@description",ddDescription.SelectedItem.Text.Trim());
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLC/AccessLevelDetail.rdlc");


          
            string strReportHeader = "Access Level Details";
            ReportParameter para = new ReportParameter("header", strReportHeader);

            string strReportHeader1 = "Generated on " + DateTime.Today.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm") + " Hrs ";
            
            string comp = "", compAdd = "";
           
            DataTable dtComp = clsCommonHandler.GetReportHeader();
           
            if (dtComp.Rows.Count > 0)
            {
                comp = dtComp.Rows[0]["value"].ToString();
                compAdd = dtComp.Rows[1]["value"].ToString();
            }

            ReportParameter para2 = new ReportParameter("myparameter1", strReportHeader1);
            ReportParameter para4 = new ReportParameter("user", Session["uid"].ToString());
            ReportParameter header = new ReportParameter("compHeader", comp);
            ReportParameter address = new ReportParameter("address", compAdd);
         

            ReportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { para, para2, para4 , header, address });
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.Visible = true;

            ReportDataSource rptData = new ReportDataSource();

            rptData.Name = "DataSet1";
            rptData.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(rptData);
            ReportViewer1.LocalReport.Refresh();
          

            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            loadReport(4);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            loadReport(3);
            ddDescription.SelectedItem.Text = "";
        }
    }
}