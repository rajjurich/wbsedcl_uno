using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Security.Cryptography;

namespace UNO
{
    public partial class ACS_Events_Upload : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string XlsPath = Server.MapPath(@"~/Uploaded Folder/Transaction_data.dat");
                FileInfo fileDet = new System.IO.FileInfo(XlsPath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
                Response.AddHeader("Content-Length", fileDet.Length.ToString());
                Response.ContentType = "application/notepad";
                Response.WriteFile(fileDet.FullName);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["connection_string"].ToString();
            SqlConnection con = new SqlConnection(sqlConnectionString);
            if (fileuploadExcel.HasFile)
            {

                try
                {
                    string extension = Path.GetExtension(fileuploadExcel.FileName).ToLower();

                    string path = string.Concat(Server.MapPath("~/Uploaded Folder/" + fileuploadExcel.FileName));
                    fileuploadExcel.SaveAs(path);

                    DataTable dt = new DataTable("info");

                    //trace no
                    dt.Columns.Add(new DataColumn("Event_Datetime", typeof(DateTime)));//Swipe_Time+Swipe_Date
                    dt.Columns.Add(new DataColumn("Event_Employee_Code", typeof(string)));//Employee_Code
                    dt.Columns.Add(new DataColumn("Event_Card_Code", typeof(string)));//Card_Code
                    //dt.Columns.Add(new DataColumn("Event_ID", typeof(int)));//Event_ID
                    dt.Columns.Add(new DataColumn("Event_Type", typeof(char)));//Event_Type
                    dt.Columns.Add(new DataColumn("Event_Trace", typeof(int)));//Event_Trace        
                    dt.Columns.Add(new DataColumn("Event_Controller_ID", typeof(int)));//tid  
                    dt.Columns.Add(new DataColumn("Event_Status", typeof(double)));//Status
                    dt.Columns.Add(new DataColumn("Event_Alarm_Type", typeof(double)));//Alarm_Type
                    dt.Columns.Add(new DataColumn("Event_Alarm_Action", typeof(char)));//Alarm_Action
                    //dt.Columns.Add(new DataColumn("EVENT_FLAG", typeof(int))); //Upload_Flag
                    dt.Columns.Add(new DataColumn("Event_mode", typeof(string)));//InOutMode 

                    int counter = 1;
                    string line;
                    string decryptedString;
                    System.IO.StreamReader file = new System.IO.StreamReader(path);

                    string encrypt = file.ReadToEnd();
                    encrypt = Decrypt(encrypt);
                    string[] splitstring = encrypt.Split('|');

                    foreach (string a in splitstring)
                    {
                        if (a != "")
                        {
                            if (counter != 1)
                            {
                                // decryptedString = Decrypt(line);
                                string[] arrstring = a.Split(',');
                                if (CheckData(arrstring))
                                {
                                    dt.Rows.Add(Convert.ToDateTime(arrstring[2] + " " + arrstring[1]), arrstring[3], arrstring[4], arrstring[6], arrstring[7], arrstring[8], arrstring[9], arrstring[10], arrstring[11], arrstring[13]);
                                }
                                else
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid file, Please select proper file.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                counter++;
                            }
                            else
                            {
                                //decryptedString = Decrypt(line);
                                string[] arrstring = a.Split(',');
                                if (arrstring[0].ToString() != "Trace_No")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Trace_No header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[1].ToString() != "Swipe_Time")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Swipe time header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[2].ToString() != "Swipe_Date")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Swipe Date header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[3].ToString() != "Employee_Code")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Employee Code header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[4].ToString() != "Card_Code")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Card Code header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[5].ToString() != "Event_ID")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Event ID header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[6].ToString() != "Event_Type")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Event Type header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[7].ToString() != "Event_Trace")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Event Trace header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[8].ToString() != "TID")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check TID header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[9].ToString() != "Status")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Alarm Action header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[10].ToString() != "Alarm_Type")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Alarm Type header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[11].ToString() != "Alarm_Action")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Alarm Action header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[12].ToString() != "Upload_Flag")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check Upload Flag header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }
                                else if (arrstring[13].ToString() != "InOutMode")
                                {
                                    lblBulkErrorMessage.Text = "Not a vlid header, Please check In Out Mode header.";
                                    lblBulkErrorMessage.Visible = true;
                                    return;
                                }

                                counter++;
                            }
                        }
                    }
                    file.Close();

                    if (dt.Rows.Count > 0)
                    {
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        var xmlvar = ds.GetXml();

                        SqlCommand cmd = new SqlCommand("sp_ACS_Event_Upload", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@xmlString", SqlDbType.VarChar).Value = xmlvar;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        cmd.ExecuteNonQuery();
                        lblBulkErrorMessage.Text = "Records saved successfully";
                        lblBulkErrorMessage.Visible = true;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        //SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
                        //bulkInsert.DestinationTableName = "ACS_Events";
                        //bulkInsert.ColumnMappings.Add("Event_Datetime", "Event_Datetime");
                        //bulkInsert.ColumnMappings.Add("Event_Employee_Code", "Event_Employee_Code");
                        //bulkInsert.ColumnMappings.Add("Event_Card_Code", "Event_Card_Code");
                        //bulkInsert.ColumnMappings.Add("Event_ID", "Event_ID");
                        //bulkInsert.ColumnMappings.Add("Event_Type", "Event_Type");
                        //bulkInsert.ColumnMappings.Add("Event_Trace", "Event_Trace");
                        //bulkInsert.ColumnMappings.Add("Event_Status", "Event_Status");
                        //bulkInsert.ColumnMappings.Add("Event_Alarm_Type", "Event_Alarm_Type");
                        //bulkInsert.ColumnMappings.Add("Event_Alarm_Action", "Event_Alarm_Action");                        
                        //bulkInsert.ColumnMappings.Add("Event_mode", "Event_mode");

                        //con.Open();
                        //bulkInsert.WriteToServer(dt);
                        //lblBulkErrorMessage.Text = "Records saved successfully";
                        //lblBulkErrorMessage.Visible = true;
                        //con.Close();
                    }
                    else
                    {
                        lblBulkErrorMessage.Text = "Empty file selected";
                        lblBulkErrorMessage.Visible = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    conn.Close();
                    lblBulkErrorMessage.Text = "Records not saved ";
                    lblBulkErrorMessage.Visible = true;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Uno_Dashboard.aspx");
        }

        //Encryption Key
        /// Encrypts a text block
        public static string Encrypt(string textToEncrypt)
        {
            string EncryptionKey = "XB13347FE570DC4FFB13647F";
            string IV = "abhijeet";

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = Encoding.ASCII.GetBytes(EncryptionKey);
            tdes.IV = Encoding.ASCII.GetBytes(IV);
            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// Decrypts an encrypted text block
        public static string Decrypt(string textToDecrypt)
        {
            string EncryptionKey = "XB13347FE570DC4FFB13647F";
            string IV = "abhijeet";
            byte[] buffer = Convert.FromBase64String(textToDecrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(EncryptionKey);
            des.IV = Encoding.ASCII.GetBytes(IV);
            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        private bool CheckData(string[] arrstring)
        {
            try
            {
                if (arrstring.Length == 14)
                {
                    if (arrstring[0].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Trace Number not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[1].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Swipe Time not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[2].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Swipe Date not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[3].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Employee Code not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[4].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " CARD CODE not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[5].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Event ID not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[6].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Event Type not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[7].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " TID not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[8].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Status not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[9].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Alarm Type not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[10].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Alarm Action ID not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[11].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Event Count not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[12].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " Upload FLAG not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                    if (arrstring[13].Length < 0)
                    {
                        lblBulkErrorMessage.Text = " In out mode not found";
                        lblBulkErrorMessage.Visible = true;
                        return false;
                    }
                }
                else
                {
                    lblBulkErrorMessage.Text = " Please check .dat file before proceed.";
                    lblBulkErrorMessage.Visible = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblBulkErrorMessage.Text = ex.Message;
                return false;
            }
            return true;
        }
    }
}