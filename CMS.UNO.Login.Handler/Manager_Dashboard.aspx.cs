using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Syncfusion.JavaScript.DataVisualization.Models;
using System.Threading;
using System.Configuration;

namespace UNO
{
    public partial class Manager_Dashboard : System.Web.UI.Page
    {
        //static SqlConnection conString = new SqlConnection(@"data source=172.19.63.113\sqlexpress;Initial Catalog=UNO_Prod;Persist Security Info=True;User ID=sa;Password=sysPass@345");
        static SqlConnection conString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                userid = Session["uid"].ToString();

            }
            if (!IsPostBack)
            {
                BindCharts();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            }

        }
        private void BindCharts()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("USP_MANAGER_DASHBOARD", conString);
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "InitialData";
                cmd.Parameters.Add("@SHIFT_ID", SqlDbType.VarChar).Value = SelectShift.SelectedValue;
                cmd.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = userid;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    SelectShift.DataSource = ds.Tables[0];
                    SelectShift.DataTextField = "SHIFT_DESCRIPTION";
                    SelectShift.DataValueField = "SHIFT_ID";
                    SelectShift.DataBind();

                    TreeGridPresent.DataSource = ds.Tables[1];
                    TreeGridPresent.DataBind();

                    TreeGridAbsent.DataSource = ds.Tables[2];
                    TreeGridAbsent.DataBind();

                    TreeGridOutdoor.DataSource = ds.Tables[3];
                    TreeGridOutdoor.DataBind();

                    TreeGridLeave.DataSource = ds.Tables[4];
                    TreeGridLeave.DataBind();
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        private static string GetData()
        {
            string json = "";
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("USP_MANAGER_DASHBOARD", conString);
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "DrillDown";
                cmd.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["uid"]);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                json = JsonConvert.SerializeObject(ds, new DataSetConverter());
            }
            catch (Exception ex)
            { }
            return json;
        }
        [WebMethod]
        public static string Dowork()
        {
            return GetData();
        }
        [WebMethod]
        public static string BindClientSide()
        {
            string json = "";
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("USP_MANAGER_DASHBOARD", conString);
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "SplineArea";
                cmd.Parameters.Add("@SHIFT_ID", SqlDbType.VarChar).Value = "GN";
                cmd.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["uid"]);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                json = JsonConvert.SerializeObject(ds, new DataSetConverter());
            }
            catch (Exception ex)
            {
            }
            return json;

        }
        [WebMethod]
        public static string GetShiftValue(string value)
        {
            string json = "";
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("USP_MANAGER_DASHBOARD", conString);
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "Pie3D";
                cmd.Parameters.Add("@SHIFT_ID", SqlDbType.VarChar).Value = value;
                cmd.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["uid"]);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                json = JsonConvert.SerializeObject(ds, new DataSetConverter());
            }
            catch (Exception ex)
            {
            }
            return json;

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtToDate.Text == "")
                {
                    txtFromDate.Text = txtToDate.Text;
                }

                SqlCommand cmd = new SqlCommand("USP_MANAGER_DASHBOARD", conString);
                cmd.Parameters.Add("@strCommand", SqlDbType.VarChar).Value = "GetReport";
                cmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = txtFromDate.Text;
                cmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = txtToDate.Text;
                cmd.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = userid;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string filename = "EmployeeExcel.xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = dt;
                    dgGrid.DataBind();

                    //Get the HTML for the control.
                    dgGrid.RenderControl(hw);
                    //Write the HTML back to the browser.
                    //Response.ContentType = application/vnd.ms-excel;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    this.EnableViewState = false;
                    Response.Write(tw.ToString());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }


    }
}