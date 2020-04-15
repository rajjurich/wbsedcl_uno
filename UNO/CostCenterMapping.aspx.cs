using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;
using CMS.UNO.Core.Handler;
using System.Web.Script.Serialization;

namespace UNO
{
    public partial class CostCenterMapping : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/costcentermapping.xls");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(fileDet.FullName);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    string extension = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string excelConnectionString = string.Empty;
                    string date = DateTime.Now.ToString("ddMMMyyyyhhmmss");
                    string file = Path.GetFileNameWithoutExtension(fileuploadExcel.FileName).ToLower();

                    string filename = string.Format("{0}_{1}{2}", file, date, extension);
                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + filename));
                    fileuploadExcel.SaveAs(path);
                    if (extension == ".xls")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }
                    else if (extension == ".xlsx")
                    {
                        excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.12.0;;Data Source={0};Extended Properties=Excel 8.0", path);
                    }
                    else
                    {
                        throw new Exception("Invalid File");
                    }
                    DataTable dt = new DataTable();

                    using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
                    {                        
                        connection.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();                       
                        DataTable dtExcelData = new DataTable();
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + SheetName + "]", connection))
                        {
                            oda.Fill(dtExcelData);
                        }
                        connection.Close();

                        dt = dtExcelData;
                    }

                    SendDataTable(dt);

                    lblMessages.Text = "Successfully uploaded Data";
                    lblMessages.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblBulkErrorMessage.Text = ex.Message;
                    lblBulkErrorMessage.Visible = true;
                    mpeAddZone.Show();
                    return;
                }
            }
        }

        private void SendDataTable(DataTable dt)
        {
            try
            {
                string query = "uspInsertCostCenterMapppingData";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CostCenterMappingTable", dt));                

                var ds = ExecuteProcedure(query, parameters.ToArray());               
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteProcedure(string commandText, params SqlParameter[] sqlparams)
        {
            try
            {
                using (conn)
                {
                    SqlCommand command = new SqlCommand();
                    command.Parameters.Clear();
                    command.Parameters.AddRange(sqlparams);
                    command.Connection = conn;
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}