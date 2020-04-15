using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;

namespace UNO
{
    public partial class BackUpAndRestore : System.Web.UI.Page
    {
        private const String ENT_EMPLOYEE_PERSONAL_DTLS_code = "EEPD";
        private const String ENT_EMPLOYEE_OFFICIAL_DTLS_code = "EEOD";
        private const String ENT_EMPLOYEE_ADDRESS_code = "EEA";
        private const String ENT_COMPANY_code = "EC";
        private const String ENT_ORG_COMMON_ENTITIES_code = "EOCE";
        private const String ZONE_code = "ZONE";
        private const String ZONE_READER_REL_code = "ZONER";
        private const String SCHEDULER_code = "SCHEDULER";
        private const String ENT_Level_Menu_Relation_code = "ELMR";
        private const String ENT_Level_code = "EL";
        private const String EAL_CONFIG_code = "EALC";
        private const String ACS_TIMEZONE_code = "ACST";
        private const String ACS_TIMEZONE_RELATION_code = "ACSTR";
        private const String ACS_READER_code = "ACSR";
        private const String ACS_DOOR_code = "ACSD";
        private const String ACS_CONTROLLER_code = "ACSC";
        private const String ACS_CARD_CONFIG_code = "ACSCC";
        private const String ACS_ACCESSPOINT_RELATION_code = "AAPR";
        private const String ACS_ACCESSPOINT_code = "AAP";
        private const String ACS_ACCESSLEVEL_RELATION_code = "AALR";
        private const String ACS_ACCESSLEVEL_code = "AAL";
        private const String ENT_PARAMS_code = "EP";

        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.Enctype = "multipart/form-data";
        }

        protected void CmdDownLoad_Click(object sender, EventArgs e)
        {
            string _strResult = "";
            DataTable _dtRecord = new DataTable();
            String _strSelect = "";
            _strSelect = "select * from ENT_EMPLOYEE_PERSONAL_DTLS";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = CreateToCSV(_dtRecord, ENT_EMPLOYEE_PERSONAL_DTLS_code);

            _strSelect = "select * from ENT_EMPLOYEE_OFFICIAL_DTLS";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_EMPLOYEE_OFFICIAL_DTLS_code);

            _strSelect = "select * from ENT_EMPLOYEE_ADDRESS";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_EMPLOYEE_ADDRESS_code);

            _strSelect = "select * from ENT_COMPANY";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_COMPANY_code);

            _strSelect = "select * from ENT_ORG_COMMON_ENTITIES";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_ORG_COMMON_ENTITIES_code);

            _strSelect = "select * from ZONE";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ZONE_code);

            _strSelect = "select * from ZONE_READER_REL";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ZONE_READER_REL_code);

            _strSelect = "select * from SCHEDULER";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, SCHEDULER_code);

            _strSelect = "select * from ENT_Level_Menu_Relation";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_Level_Menu_Relation_code);

            _strSelect = "select * from ENT_Level";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_Level_code);

            _strSelect = "select * from EAL_CONFIG";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, EAL_CONFIG_code);

            _strSelect = "select * from ACS_TIMEZONE";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_TIMEZONE_code);

            _strSelect = "select * from ACS_TIMEZONE_RELATION";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_TIMEZONE_RELATION_code);

            _strSelect = "select * from ACS_READER";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_READER_code);

            _strSelect = "select * from ACS_DOOR";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_DOOR_code);

            _strSelect = "select * from ACS_CONTROLLER";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_CONTROLLER_code);

            _strSelect = "select * from ACS_CARD_CONFIG";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_CARD_CONFIG_code);

            _strSelect = "select * from ACS_ACCESSPOINT_RELATION";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_ACCESSPOINT_RELATION_code);

            _strSelect = "select * from ACS_ACCESSPOINT";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_ACCESSPOINT_code);

            _strSelect = "select * from ACS_ACCESSLEVEL_RELATION";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_ACCESSLEVEL_RELATION_code);

            _strSelect = "select * from ACS_ACCESSLEVEL";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ACS_ACCESSLEVEL_code);

            _strSelect = "select * from ENT_PARAMS";
            _dtRecord = getDataTable(_strSelect, _sqlConnection);
            _strResult = _strResult + CreateToCSV(_dtRecord, ENT_PARAMS_code);

            handleDownLoad(_strResult);
            this.messageDiv.InnerHtml = "Records downloaded successfully.";
            clearDiv();
            RBOperation.SelectedValue = null;
           // lblFilePath.Text = string.Empty;
            return;
        }
        
        public string CreateToCSV( DataTable table, String _strSuffix)
        {
            StringBuilder _result = new StringBuilder();
            //for (int i = 0; i < table.Columns.Count; i++)
            //{
            //    result.Append(table.Columns[i].ColumnName);
            //    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            //}
            //string _result ="";
            foreach (DataRow row in table.Rows)
            {
                _result.Append(_strSuffix+",");
               // _result = _result + "EMPP,";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    _result.Append(row[i].ToString());
                    _result.Append(",");
                  
                    //_result = _result + row[i].ToString() + ",";
                    if (i == table.Columns.Count - 1)
                    {
                        //_result = _result.Substring(0, (_result.Length - 1));
                        //_result = _result + System.Environment.NewLine; 
                        _result = _result.Remove((_result.Length - 1), 1);
                        _result.Append(System.Environment.NewLine);
                    }                    
                }
            }

            return _result.ToString();
           //return _result;
        }

        public void handleDownLoad(string _str)
        {
            try
            {
                String reportType = "EXCEL";
                String mimeType;
                String encoding;                
                String[] streams;
                Byte[] renderedBytes;//This variable is used to store report viewer content in bytes to load the same in excel.
                //renderedBytes = GetBytes(_str);              
                renderedBytes = System.Text.Encoding.ASCII.GetBytes(_str);              
                
                String fpath1 = this.Server.MapPath("~//BackAndRestore");   //changes on 13/Sept/2014 by shrinith

                String FileName = DateTime.Now.ToString("dd-MM-yyyy");   
                String fpath = this.Server.MapPath("~//BackAndRestore//" + FileName + ".csv");


                if (!Directory.Exists(fpath1))   //changes on 13/Sept/2014 by shrinith
                {
                    Directory.CreateDirectory(fpath1 + "/");
                }

                lblFilePath.Text = fpath;
                FileStream newFile = new FileStream(fpath, FileMode.Create);
                newFile.Write(renderedBytes, 0, renderedBytes.Length);


                //Commented and changes made by Shrinith on 11/Sept/2014 - start

                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Buffer = true;
                //// HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);
                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv");
                //HttpContext.Current.Response.AddHeader("Content-Length", newFile.Length.ToString());
                ////HttpContext.Current.Response.ContentType = "application/octet-stream";
                //HttpContext.Current.Response.ContentType = "TEXT/CSV";

               Response.ContentType = ContentType;
               Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv");
               Response.AddHeader("Content-Length", newFile.Length.ToString());
               Response.ContentType = "application/octet-stream";
               Response.ContentType = "TEXT/CSV";

                newFile.Close();


                //HttpContext.Current.Response.Write(_str);
                //HttpContext.Current.Response.WriteFile(fpath1);
                //HttpContext.Current.Response.Flush();

                Response.WriteFile(fpath);
               // File.Delete(fpath);
               //Page.Response

                //Commented and changes made by Shrinith on 11/Sept/2014 - end
            
                //HttpContext.Current.Response.End();

                
            }
            catch (Exception e)
            {
               
            }


        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);            
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static byte[] ConvertByteStringToByteArray(string byteString)
        {
            byte[] HexAsBytes = new byte[byteString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = byteString.Substring(index * 2, 2);                
            }
            return HexAsBytes;
        }

        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                return _ds.Tables[0];
            }
            catch (Exception ex)
            { return null; }
        }

        public bool RunExecuteNonQueryWithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.Transaction = _sqlTrans;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                _sqlTrans.Rollback();
                return false;
            }
        }

        public void clearDiv()
        {
            
            string someScript = "";
            someScript = "<script language='javascript'>setTimeout(\"clearFunctionMessageDiv()\",3000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
        }

        public Boolean UploadRecords(DataTable _dt,String _tableName,SqlTransaction _trans)
        {
            try
            {
                Boolean _result;
                string _strDeleteQuery = "delete "+ _tableName ;
                _result = RunExecuteNonQueryWithTransaction(_strDeleteQuery, _sqlConnection, _trans);                
                    try
                    {
                        
                        SqlBulkCopy s = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.KeepIdentity, _trans);
                        s.DestinationTableName = _tableName;
                        s.WriteToServer(_dt);
                        s.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _trans.Rollback();
                        _sqlConnection.Close();
                        this.messageDiv.InnerText = "Please select file to upload.";
                        clearDiv();
                        return false;
                    }
                
            }
            catch (Exception ex)
            { return false; }
        }
        
        public object EvaluateNullity(object entity)
        {
            return entity ?? DBNull.Value;
        }

        public DataTable fillEEPDTable(string[] data, DataTable _dt)
        {
            //try
            //{              
                //if (data.Length > 0)
                //{              
                //    if (_dt.Columns.Count == 0)
                //    {
                //        foreach (object item in data)
                //        {
                //            _dt.Columns.Add(new System.Data.DataColumn());
                //        }
                //    }                 
                //    System.Data.DataRow row = _dt.NewRow();
                //    row.ItemArray = data;
                //    _dt.Rows.Add(row);
                //}
                //return _dt;
            //}
            //catch (Exception ex)
            //{ return null; }

            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_EMPID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_SALUTATION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_FIRST_NAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_MIDDLE_NAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_LAST_NAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_NICKNAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_PREVIOUS_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_CARD_EXPIRY_DATE", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_TEMP_CARD_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_GENDER", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_MARITAL_STATUS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_DOB", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_RELIGION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_REFERENCE_ONE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_REFERENCE_TWO", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_DOMICILE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_BLOODGROUP", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_EMAIL", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_PAN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_DOCTOR", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_PREVIOUS_CARD_STATUS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_CARD_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_NUMCARDS", typeof(int)));                       
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_PERDATE", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_APPLN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_PERSO_FLAG", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EPD_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["EPD_EMPID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["EPD_SALUTATION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["EPD_FIRST_NAME"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["EPD_MIDDLE_NAME"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["EPD_LAST_NAME"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["EPD_NICKNAME"] = EvaluateNullity((data[5] == "") ? null : data[5]);                    
                    row["EPD_PREVIOUS_ID"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["EPD_CARD_EXPIRY_DATE"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["EPD_TEMP_CARD_ID"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["EPD_GENDER"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    row["EPD_MARITAL_STATUS"] = EvaluateNullity((data[10] == "") ? null : data[10]);
                    row["EPD_DOB"] = EvaluateNullity((data[11] == "") ? null : data[11]);
                    row["EPD_RELIGION"] = EvaluateNullity((data[12] == "") ? null : data[12]);
                    row["EPD_REFERENCE_ONE"] = EvaluateNullity((data[13] == "") ? null : data[13]);
                    row["EPD_REFERENCE_TWO"] = EvaluateNullity((data[14] == "") ? null : data[14]);
                    row["EPD_DOMICILE"] = EvaluateNullity((data[15] == "") ? null : data[15]);
                    row["EPD_BLOODGROUP"] = EvaluateNullity((data[16] == "") ? null : data[16]);
                    row["EPD_EMAIL"] = EvaluateNullity((data[17] == "") ? null : data[17]);
                    row["EPD_PAN"] = EvaluateNullity((data[18] == "") ? null : data[18]);
                    row["EPD_DOCTOR"] = EvaluateNullity((data[19] == "") ? null : data[19]);
                    row["EPD_PREVIOUS_CARD_STATUS"] = EvaluateNullity((data[20] == "") ? null : data[20]);
                    row["EPD_CARD_ID"] = EvaluateNullity((data[21] == "") ? null : data[21]);
                    row["EPD_NUMCARDS"] = EvaluateNullity((data[22] == "") ? null : data[22]);
                    row["EPD_PERDATE"] = EvaluateNullity((data[23] == "") ? null : data[23]);
                    row["EPD_APPLN"] = EvaluateNullity((data[24] == "") ? null : data[24]);
                    row["EPD_PERSO_FLAG"] = EvaluateNullity((data[25] == "") ? null : data[25]);
                    row["EPD_ISDELETED"] = EvaluateNullity((data[26] == "") ? null : data[26]);
                    row["EPD_DELETEDDATE"] = EvaluateNullity((data[27] == "") ? null : data[27]);                  

                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }        

        public DataTable fillEEODTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {                       
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_EMPID",typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_JOINING_DATE", typeof(DateTime)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_CONFIRM_DATE", typeof(DateTime)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_RETIREMENT_DATE", typeof(DateTime)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_RETIREMENT_REASON_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_COMPANY_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_LOCATION_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_DIVISION_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_DEPARTMENT_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_DESIGNATION_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_CATEGORY_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_GROUP_ID", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_GRADE_ID", typeof(String))); 
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_STATUS", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_ACTIVE", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_ISDELETED", typeof(String)));
                            _dt.Columns.Add(new System.Data.DataColumn("EOD_DELETEDDATE", typeof(DateTime)));                        
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["EOD_EMPID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["EOD_JOINING_DATE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["EOD_CONFIRM_DATE"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["EOD_RETIREMENT_DATE"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["EOD_RETIREMENT_REASON_ID"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["EOD_COMPANY_ID"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["EOD_LOCATION_ID"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["EOD_DIVISION_ID"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["EOD_DEPARTMENT_ID"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["EOD_DESIGNATION_ID"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    row["EOD_CATEGORY_ID"] = EvaluateNullity((data[10] == "") ? null : data[10]);
                    row["EOD_GROUP_ID"] = EvaluateNullity((data[11] == "") ? null : data[11]);
                    row["EOD_GRADE_ID"] = EvaluateNullity((data[12] == "") ? null : data[12]);
                    row["EOD_STATUS"] = EvaluateNullity((data[13] == "") ? null : data[13]);
                    row["EOD_ACTIVE"] = EvaluateNullity((data[14] == "") ? null : data[14]);
                    row["EOD_ISDELETED"] = EvaluateNullity((data[15] == "") ? null : data[15]);
                    row["EOD_DELETEDDATE"] = EvaluateNullity((data[16] == "") ? null : data[16]);

                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillEEATable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_EMPID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_ADDRESS_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_ADDRESS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_CITY", typeof(String)));                        
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_PIN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_STATE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_COUNTRY", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_PHONE_ONE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_PHONE_TWO", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EAD_DELETEDDATE", typeof(DateTime)));                        
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["EAD_EMPID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["EAD_ADDRESS_TYPE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["EAD_ADDRESS"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["EAD_CITY"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["EAD_PIN"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["EAD_STATE"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["EAD_COUNTRY"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["EAD_PHONE_ONE"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["EAD_PHONE_TWO"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["EAD_ISDELETED"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    row["EAD_DELETEDDATE"] = EvaluateNullity((data[10] == "") ? null : data[10]);                    

                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillECTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_NAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_ADDRESS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_CITY", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_PIN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_PHONE1", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_PHONE2", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_STATE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_ADDRESS1", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_CITY", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_PIN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_STATE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_PHONE1", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_RO_PHONE2", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("COMPANY_DELETEDDATE", typeof(String)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["COMPANY_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["COMPANY_NAME"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["COMPANY_ADDRESS"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["COMPANY_CITY"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["COMPANY_PIN"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["COMPANY_PHONE1"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["COMPANY_PHONE2"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["COMPANY_STATE"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["COMPANY_RO_ADDRESS1"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["COMPANY_RO_CITY"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    row["COMPANY_RO_PIN"] = EvaluateNullity((data[10] == "") ? null : data[10]);
                    row["COMPANY_RO_STATE"] = EvaluateNullity((data[11] == "") ? null : data[11]);
                    row["COMPANY_RO_PHONE1"] = EvaluateNullity((data[12] == "") ? null : data[12]);
                    row["COMPANY_RO_PHONE2"] = EvaluateNullity((data[13] == "") ? null : data[13]);
                    row["COMPANY_ISDELETED"] = EvaluateNullity((data[14] == "") ? null : data[14]);
                    row["COMPANY_DELETEDDATE"] = EvaluateNullity((data[15] == "") ? null : data[15]);

                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillEOCETable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("CEM_ENTITY_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("OCE_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("OCE_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("OCE_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("OCE_DELETEDDATE", typeof(DateTime)));                        
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["CEM_ENTITY_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["OCE_ID"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["OCE_DESCRIPTION"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["OCE_ISDELETED"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["OCE_DELETEDDATE"] = EvaluateNullity((data[4] == "") ? null : data[4]);                    
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
    
        public DataTable fillZONETable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("ZONE_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("ZONE_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ZONE_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ZONE_DELETEDDATE", typeof(DateTime)));                       
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["ZONE_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["ZONE_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["ZONE_ISDELETED"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["ZONE_DELETEDDATE"] = EvaluateNullity((data[3] == "") ? null : data[3]);         
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillZONERTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("ZONE_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("CONTROLLER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("ZONER_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ZONER_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["ZONE_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["READER_ID"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["CONTROLLER_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["ZONER_ISDELETED"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["ZONER_DELETEDDATE"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillSCHEDULERTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_TASK_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_FREQUENCY", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_TIME", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("SCHEDULER_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["SCHEDULER_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["SCHEDULER_TASK_TYPE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["SCHEDULER_DESCRIPTION"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["SCHEDULER_FREQUENCY"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["SCHEDULER_TIME"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["SCHEDULER_ISDELETED"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["SCHEDULER_DELETEDDATE"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillELMRTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("LevelID", typeof(float)));
                        _dt.Columns.Add(new System.Data.DataColumn("MenuID", typeof(float)));                        
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["LevelID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["MenuID"] = EvaluateNullity((data[1] == "") ? null : data[1]);                    
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillELTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("LevelID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("LevelName", typeof(String)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["LevelID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["LevelName"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
      
        public DataTable fillEAL_CONFIGTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("EAL_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("ENTITY_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ENTITY_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("EMPLOYEE_CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CARD_CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("FLAG", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["EAL_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["ENTITY_TYPE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["ENTITY_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["EMPLOYEE_CODE"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["CARD_CODE"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["AL_ID"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["FLAG"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["ISDELETED"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["DELETEDDATE"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillACSTTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_NAME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["TZ_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["TZ_CODE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["TZ_DESCRIPTION"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["TZ_NAME"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["TZ_ISDELETED"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["TZ_DELETEDDATE"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillACSTRTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_DAYOFWEEK", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_FROMTIME", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZ_TOTIME", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZR_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("TZR_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["TZ_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["TZ_TYPE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["TZ_DAYOFWEEK"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["TZ_FROMTIME"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["TZ_TOTIME"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["TZR_ISDELETED"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["TZR_DELETEDDATE"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillACSRTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("READER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_MODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_PASSES_FROM", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_PASSES_TO", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["READER_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["READER_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["CTLR_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["READER_MODE"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["READER_TYPE"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["READER_PASSES_FROM"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["READER_PASSES_TO"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["READER_ISDELETED"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["READER_DELETEDDATE"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
        
        public DataTable fillACSDTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_OPEN_DURATION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_FEEDBACK_DURATION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_DELETEDDATE", typeof(DateTime)));                       
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["DOOR_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["DOOR_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["CTLR_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["DOOR_TYPE"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["DOOR_OPEN_DURATION"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["DOOR_FEEDBACK_DURATION"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["DOOR_ISDELETED"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["DOOR_DELETEDDATE"] = EvaluateNullity((data[7] == "") ? null : data[7]);                    
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
       
        public DataTable fillACSCTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_IP", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_SUBNET_MASK", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_GATEWAY_IP", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_SERVER_IP", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_MAC_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_INCOMING_PORT", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_OUTGOING_PORT", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FIRMWARE_VERSION_NO", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_HARDWARE_VERSION_NO", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_CHK_FACILITY_CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE1", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE2", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE3", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE4", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE5", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_FACILITY_CODE6", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_CHK_APB", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_APB_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_APB_TIME", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_AUTHENTICATION_MODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_CHK_TOC", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_EVENTS_STORED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_MAX_TRANSACTIONS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_CURRENT_USER_CNT", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_MAX_USER_CNT", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_DELETEDDATE", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("CTLR_CONN_STATUS", typeof(String))); //changes on 13/Sept/2014 by Shrinith
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["CTLR_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["CTLR_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["CTLR_TYPE"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["CTLR_IP"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["CTLR_SUBNET_MASK"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["CTLR_GATEWAY_IP"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["CTLR_SERVER_IP"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["CTLR_MAC_ID"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["CTLR_INCOMING_PORT"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["CTLR_OUTGOING_PORT"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    row["CTLR_FIRMWARE_VERSION_NO"] = EvaluateNullity((data[10] == "") ? null : data[10]);
                    row["CTLR_HARDWARE_VERSION_NO"] = EvaluateNullity((data[11] == "") ? null : data[11]);
                    row["CTLR_CHK_FACILITY_CODE"] = EvaluateNullity((data[12] == "") ? null : data[12]);
                    row["CTLR_FACILITY_CODE1"] = EvaluateNullity((data[13] == "") ? null : data[13]);
                    row["CTLR_FACILITY_CODE2"] = EvaluateNullity((data[14] == "") ? null : data[14]);
                    row["CTLR_FACILITY_CODE3"] = EvaluateNullity((data[15] == "") ? null : data[15]);
                    row["CTLR_FACILITY_CODE4"] = EvaluateNullity((data[16] == "") ? null : data[16]);
                    row["CTLR_FACILITY_CODE5"] = EvaluateNullity((data[17] == "") ? null : data[17]);
                    row["CTLR_FACILITY_CODE6"] = EvaluateNullity((data[18] == "") ? null : data[18]);
                    row["CTLR_CHK_APB"] = EvaluateNullity((data[19] == "") ? null : data[19]);
                    row["CTLR_APB_TYPE"] = EvaluateNullity((data[20] == "") ? null : data[20]);
                    row["CTLR_APB_TIME"] = EvaluateNullity((data[21] == "") ? null : data[21]);
                    row["CTLR_AUTHENTICATION_MODE"] = EvaluateNullity((data[22] == "") ? null : data[22]);
                    row["CTLR_CHK_TOC"] = EvaluateNullity((data[23] == "") ? null : data[23]);
                    row["CTLR_EVENTS_STORED"] = EvaluateNullity((data[24] == "") ? null : data[24]);
                    row["CTLR_MAX_TRANSACTIONS"] = EvaluateNullity((data[25] == "") ? null : data[25]);
                    row["CTLR_CURRENT_USER_CNT"] = EvaluateNullity((data[26] == "") ? null : data[26]);
                    row["CTLR_MAX_USER_CNT"] = EvaluateNullity((data[27] == "") ? null : data[27]);
                    row["CTLR_ISDELETED"] = EvaluateNullity((data[28] == "") ? null : data[28]);
                    row["CTLR_DELETEDDATE"] = EvaluateNullity((data[29] == "") ? null : data[29]);
                    row["CTLR_CONN_STATUS"] = EvaluateNullity((data[30] == "") ? null : data[30]); //changes on 13/Sept/2014 by Shrinith
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
        
        public DataTable fillACSCCTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("CC_EMP_ID", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CARD_CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("PIN", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("USECOUNT", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("IGNORE_APB", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("STATUS", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ACTIVATION_DATE", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("EXPIRY_DATE", typeof(DateTime)));
                        _dt.Columns.Add(new System.Data.DataColumn("ACE_isdeleted", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ACE_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["CC_EMP_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["CARD_CODE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["PIN"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["USECOUNT"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["IGNORE_APB"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["STATUS"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    row["ACTIVATION_DATE"] = EvaluateNullity((data[6] == "") ? null : data[6]);
                    row["EXPIRY_DATE"] = EvaluateNullity((data[7] == "") ? null : data[7]);
                    row["ACE_isdeleted"] = EvaluateNullity((data[8] == "") ? null : data[8]);
                    row["ACE_DELETEDDATE"] = EvaluateNullity((data[9] == "") ? null : data[9]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillAAPRTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("AP_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("READER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("DOOR_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("AP_CONTROLLER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("APR_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("APR_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["AP_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["READER_ID"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["DOOR_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["AP_CONTROLLER_ID"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["APR_ISDELETED"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["APR_DELETEDDATE"] = EvaluateNullity((data[5] == "") ? null : data[5]);                   
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillAAPTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("AP_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("AP_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AP_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AP_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AP_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["AP_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["AP_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["AP_TYPE"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["AP_ISDELETED"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["AP_DELETEDDATE"] = EvaluateNullity((data[4] == "") ? null : data[4]);                   
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }
        
        public DataTable fillAALRTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("AL_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("RD_ZN_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_ENTITY_TYPE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CONTROLLER_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("ALR_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("ALR_DELETEDDATE", typeof(DateTime)));
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["AL_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["RD_ZN_ID"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["AL_ENTITY_TYPE"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["CONTROLLER_ID"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["ALR_ISDELETED"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    row["ALR_DELETEDDATE"] = EvaluateNullity((data[5] == "") ? null : data[5]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillAALTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("AL_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_DESCRIPTION", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_TIMEZONE_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_ISDELETED", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("AL_DELETEDDATE", typeof(DateTime)));                        
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["AL_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["AL_DESCRIPTION"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["AL_TIMEZONE_ID"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["AL_ISDELETED"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["AL_DELETEDDATE"] = EvaluateNullity((data[4] == "") ? null : data[4]);                    
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        public DataTable fillEPTable(string[] data, DataTable _dt)
        {
            try
            {
                if (data.Length > 0)
                {
                    if (_dt.Columns.Count == 0)
                    {
                        _dt.Columns.Add(new System.Data.DataColumn("PARAM_ID", typeof(Int64)));
                        _dt.Columns.Add(new System.Data.DataColumn("MODULE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("IDENTIFIER", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("CODE", typeof(String)));
                        _dt.Columns.Add(new System.Data.DataColumn("VALUE", typeof(String)));                       
                    }
                    System.Data.DataRow row = _dt.NewRow();
                    //row.ItemArray = data;
                    row["PARAM_ID"] = EvaluateNullity((data[0] == "") ? null : data[0]);
                    row["MODULE"] = EvaluateNullity((data[1] == "") ? null : data[1]);
                    row["IDENTIFIER"] = EvaluateNullity((data[2] == "") ? null : data[2]);
                    row["CODE"] = EvaluateNullity((data[3] == "") ? null : data[3]);
                    row["VALUE"] = EvaluateNullity((data[4] == "") ? null : data[4]);
                    _dt.Rows.Add(row);
                }
                return _dt;
            }
            catch (Exception ex)
            { return null; }
        }

        protected void CmdUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.FileName == "")
            {
                this.messageDiv.InnerText = "Please select file to upload.";
                clearDiv();
                return;

            }
            else
            {
                string[] FileExt = FileUpload1.FileName.Split('.');
                string FileEx = FileExt[FileExt.Length - 1];
                if (FileEx.ToLower() == "csv")
                {
                    FileUpload1.SaveAs(Server.MapPath("BackAndRestore//" + FileUpload1.FileName));
                }
                else
                {
                    this.messageDiv.InnerText = "Please select csv file.";
                    clearDiv();
                    return;
                }

                DataTable dtENT_EMPLOYEE_PERSONAL_DTLS = new DataTable();
                DataTable dtENT_EMPLOYEE_OFFICIAL_DTLS = new DataTable();
                DataTable dtENT_EMPLOYEE_ADDRESS = new DataTable();
                DataTable dtENT_COMPANY = new DataTable();
                DataTable dtENT_ORG_COMMON_ENTITIES = new DataTable();
                DataTable dtZONE = new DataTable();
                DataTable dtZONE_READER_REL = new DataTable();
                DataTable dtSCHEDULER = new DataTable();
                DataTable dtENT_Level_Menu_Relation = new DataTable();
                DataTable dtENT_Level = new DataTable();
                DataTable dtEAL_CONFIG = new DataTable();
                DataTable dtACS_TIMEZONE = new DataTable();
                DataTable dtACS_TIMEZONE_RELATION = new DataTable();
                DataTable dtACS_READER = new DataTable();
                DataTable dtACS_DOOR = new DataTable();
                DataTable dtACS_CONTROLLER = new DataTable();
                DataTable dtACS_CARD_CONFIG = new DataTable();
                DataTable dtACS_ACCESSPOINT_RELATION = new DataTable();
                DataTable dtACS_ACCESSPOINT = new DataTable();
                DataTable dtACS_ACCESSLEVEL_RELATION = new DataTable();
                DataTable dtACS_ACCESSLEVEL = new DataTable();
                DataTable dtENT_PARAMS = new DataTable();

                String line = null;
                String _table = "";
                int i = 0, _position = 0;
                StreamReader sr = File.OpenText(Server.MapPath("BackAndRestore//" + FileUpload1.FileName));

                //while ((line = sr.ReadLine()) != null)
                //{
                //    _position=line.IndexOf(",");
                //    _table = line.Substring(0, _position);
                //    line = line.Substring(_position+1);
                //   string[] data = line.Split(',');

                //    if (data.Length > 0)
                //    {
                //        if (i == 0)
                //        {
                //            foreach (object item in data)
                //            {
                //                dt.Columns.Add(new System.Data.DataColumn());
                //            }
                //            i++;
                //        }
                //        System.Data.DataRow row = dt.NewRow();
                //        row.ItemArray = data;
                //        dt.Rows.Add(row);
                //    }
                //}

                //if (_sqlConnection.State == ConnectionState.Closed)
                //{ _sqlConnection.Open(); }

                //Boolean _result = false;
                //SqlTransaction _trans;
                //_trans = _sqlConnection.BeginTransaction();
                //string _strDeleteQuery = "delete ENT_EMPLOYEE_PERSONAL_DTLS";
                //_result = RunExecuteNonQueryWithTransaction(_strDeleteQuery, _sqlConnection, _trans);
                //if (_result == true)
                //{
                //    try
                //    {
                //        SqlBulkCopy s = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.Default, _trans);
                //        s.DestinationTableName = "ENT_EMPLOYEE_PERSONAL_DTLS";
                //        s.WriteToServer(dt);
                //        s.Close();
                //        _trans.Commit();

                //        // _trans.Rollback();
                //        _sqlConnection.Close();

                //    }
                //    catch (Exception ex)
                //    {
                //        _trans.Rollback();
                //    }
                //}

                byte[] bytes;  
              
                while ((line = sr.ReadLine()) != null)
                {
                    //bytes = Encoding.Unicode.GetBytes(line);
                    //line = System.Text.Encoding.ASCII.GetString(bytes); 
                    _position = line.IndexOf(",");
                    _table = line.Substring(0, _position);
                    line = line.Substring(_position + 1);
                    string[] data = line.Split(',');
                    if (_table == ENT_EMPLOYEE_PERSONAL_DTLS_code)
                    { dtENT_EMPLOYEE_PERSONAL_DTLS = fillEEPDTable(data, dtENT_EMPLOYEE_PERSONAL_DTLS); }
                    else if (_table == ENT_EMPLOYEE_OFFICIAL_DTLS_code)
                    { dtENT_EMPLOYEE_OFFICIAL_DTLS = fillEEODTable(data, dtENT_EMPLOYEE_OFFICIAL_DTLS); }
                    else if (_table == ENT_EMPLOYEE_ADDRESS_code)
                    { dtENT_EMPLOYEE_ADDRESS = fillEEATable(data, dtENT_EMPLOYEE_ADDRESS); }
                    else if (_table == ENT_COMPANY_code)
                    { dtENT_COMPANY = fillECTable(data, dtENT_COMPANY); }
                    else if (_table == ENT_ORG_COMMON_ENTITIES_code)
                    { dtENT_ORG_COMMON_ENTITIES = fillEOCETable(data, dtENT_ORG_COMMON_ENTITIES); }
                    else if (_table == ZONE_code)
                    { dtZONE = fillZONETable(data, dtZONE); }
                    else if (_table == ZONE_READER_REL_code)
                    { dtZONE_READER_REL = fillZONERTable(data, dtZONE_READER_REL); }
                    else if (_table == SCHEDULER_code)
                    { dtSCHEDULER = fillSCHEDULERTable(data, dtSCHEDULER); }
                    else if (_table == ENT_Level_Menu_Relation_code)
                    { dtENT_Level_Menu_Relation = fillELMRTable(data, dtENT_Level_Menu_Relation); }
                    else if (_table == ENT_Level_code)
                    { dtENT_Level = fillELTable(data, dtENT_Level); }
                    else if (_table == EAL_CONFIG_code)
                    { dtEAL_CONFIG = fillEAL_CONFIGTable(data, dtEAL_CONFIG); }
                    else if (_table == ACS_TIMEZONE_code)
                    { dtACS_TIMEZONE = fillACSTTable(data, dtACS_TIMEZONE); }
                    else if (_table == ACS_TIMEZONE_RELATION_code)
                    { dtACS_TIMEZONE_RELATION = fillACSTRTable(data, dtACS_TIMEZONE_RELATION); }
                    else if (_table == ACS_READER_code)
                    { dtACS_READER = fillACSRTable(data, dtACS_READER); }
                    else if (_table == ACS_DOOR_code)
                    { dtACS_DOOR = fillACSDTable(data, dtACS_DOOR); }
                    else if (_table == ACS_CONTROLLER_code)
                    { dtACS_CONTROLLER = fillACSCTable(data, dtACS_CONTROLLER); }
                    else if (_table == ACS_CARD_CONFIG_code)
                    { dtACS_CARD_CONFIG = fillACSCCTable(data, dtACS_CARD_CONFIG); }
                    else if (_table == ACS_ACCESSPOINT_RELATION_code)
                    { dtACS_ACCESSPOINT_RELATION = fillAAPRTable(data, dtACS_ACCESSPOINT_RELATION); }
                    else if (_table == ACS_ACCESSPOINT_code)
                    { dtACS_ACCESSPOINT = fillAAPTable(data, dtACS_ACCESSPOINT); }
                    else if (_table == ACS_ACCESSLEVEL_RELATION_code)
                    { dtACS_ACCESSLEVEL_RELATION = fillAALRTable(data, dtACS_ACCESSLEVEL_RELATION); }
                    else if (_table == ACS_ACCESSLEVEL_code)
                    { dtACS_ACCESSLEVEL = fillAALTable(data, dtACS_ACCESSLEVEL); }
                    else if (_table == ENT_PARAMS_code)
                    { dtENT_PARAMS = fillEPTable(data, dtENT_PARAMS); }
                }



                Boolean _result = false;
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();
                SqlTransaction _trans;
                String _UseCase = "";
                _trans = _sqlConnection.BeginTransaction();
                _result = UploadRecords(dtENT_EMPLOYEE_PERSONAL_DTLS, "ENT_EMPLOYEE_PERSONAL_DTLS", _trans);
                if (_result == false) { _UseCase = "ENT_EMPLOYEE_PERSONAL_DTLS"; }
                if (_result == true)
                {
                    _result = UploadRecords(dtENT_EMPLOYEE_OFFICIAL_DTLS, "ENT_EMPLOYEE_OFFICIAL_DTLS", _trans);                   
                    if (_result == false) { _UseCase = "ENT_EMPLOYEE_OFFICIAL_DTLS"; }
                    if (_result == true)
                    {
                        _result = UploadRecords(dtENT_EMPLOYEE_ADDRESS, "ENT_EMPLOYEE_ADDRESS", _trans);
                        if (_result == false) { _UseCase = "ENT_EMPLOYEE_ADDRESS"; }
                        if (_result == true)
                        {
                            _result = UploadRecords(dtENT_COMPANY, "ENT_COMPANY", _trans);
                            if (_result == false) { _UseCase = "ENT_COMPANY"; }
                            if (_result == true)
                            {
                                _result = UploadRecords(dtENT_ORG_COMMON_ENTITIES, "ENT_ORG_COMMON_ENTITIES", _trans);
                                if (_result == false) { _UseCase = "ENT_ORG_COMMON_ENTITIES"; }
                                if (_result == true)
                                {
                                    _result = UploadRecords(dtZONE, "ZONE", _trans);
                                    if (_result == false) { _UseCase = "ZONE"; }
                                    if (_result == true)
                                    {
                                        _result = UploadRecords(dtZONE_READER_REL, "ZONE_READER_REL", _trans);
                                        if (_result == false) { _UseCase = "ZONE_READER_REL"; }
                                        if (_result == true)
                                        {
                                            _result = UploadRecords(dtSCHEDULER, "SCHEDULER", _trans);
                                            if (_result == false) { _UseCase = "SCHEDULER"; }
                                            if (_result == true)
                                            {
                                                _result = UploadRecords(dtENT_Level_Menu_Relation, "ENT_Level_Menu_Relation", _trans);
                                                if (_result == false) { _UseCase = "ENT_Level_Menu_Relation"; }
                                                if (_result == true)
                                                {
                                                    _result = UploadRecords(dtENT_Level, "ENT_Level", _trans);
                                                    if (_result == false) { _UseCase = "ENT_Level"; }
                                                    if (_result == true)
                                                    {
                                                        _result = UploadRecords(dtEAL_CONFIG, "EAL_CONFIG", _trans);
                                                        if (_result == false) { _UseCase = "EAL_CONFIG"; }
                                                        if (_result == true)
                                                        {
                                                            _result = UploadRecords(dtACS_TIMEZONE, "ACS_TIMEZONE", _trans);
                                                            if (_result == false) { _UseCase = "ACS_TIMEZONE"; }
                                                            if (_result == true)
                                                            {
                                                                //_result = UploadRecords(dtACS_TIMEZONE_RELATION, "ACS_TIMEZONE_RELATION", _trans);
                                                                //if (_result == false) { _UseCase = "ACS_TIMEZONE_RELATION"; }
                                                                if (_result == true)
                                                                {
                                                                    _result = UploadRecords(dtACS_READER, "ACS_READER", _trans);
                                                                    if (_result == false) { _UseCase = "ACS_READER"; }
                                                                    if (_result == true)
                                                                    {
                                                                        _result = UploadRecords(dtACS_DOOR, "ACS_DOOR", _trans);
                                                                        if (_result == false) { _UseCase = "ACS_DOOR"; }
                                                                        if (_result == true)
                                                                        {
                                                                            _result = UploadRecords(dtACS_CONTROLLER, "ACS_CONTROLLER", _trans);
                                                                            if (_result == false) { _UseCase = "ACS_CONTROLLER"; }
                                                                            if (_result == true)
                                                                            {
                                                                                _result = UploadRecords(dtACS_CARD_CONFIG, "ACS_CARD_CONFIG", _trans);
                                                                                if (_result == false) { _UseCase = "ACS_CARD_CONFIG"; }
                                                                                if (_result == true)
                                                                                {
                                                                                    _result = UploadRecords(dtACS_ACCESSPOINT_RELATION, "ACS_ACCESSPOINT_RELATION", _trans);
                                                                                    if (_result == false) { _UseCase = "ACS_ACCESSPOINT_RELATION"; }
                                                                                    if (_result == true)
                                                                                    {
                                                                                        _result = UploadRecords(dtACS_ACCESSPOINT, "ACS_ACCESSPOINT", _trans);
                                                                                        if (_result == false) { _UseCase = "ACS_ACCESSPOINT"; }
                                                                                        if (_result == true)
                                                                                        {
                                                                                            _result = UploadRecords(dtACS_ACCESSLEVEL_RELATION, "ACS_ACCESSLEVEL_RELATION", _trans);
                                                                                            if (_result == false) { _UseCase = "ACS_ACCESSLEVEL_RELATION"; }
                                                                                            if (_result == true)
                                                                                            {
                                                                                                _result = UploadRecords(dtACS_ACCESSLEVEL, "ACS_ACCESSLEVEL", _trans);
                                                                                                if (_result == false) { _UseCase = "ACS_ACCESSLEVEL"; }
                                                                                                if (_result == true)
                                                                                                {
                                                                                                    _result = UploadRecords(dtENT_PARAMS, "ENT_PARAMS", _trans);
                                                                                                    if (_result == false) { _UseCase = "ENT_PARAMS"; }
                                                                                                    if (_result == true)
                                                                                                    { _trans.Commit(); _sqlConnection.Close(); }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }


                sr.Close();
                File.Delete(Server.MapPath("BackAndRestore//" + FileUpload1.FileName));

                if (_result == false)
                {
                    this.messageDiv.InnerText = "Error Occured while uploading records in " + _UseCase + " .";
                    clearDiv();
                    return;
                }
                else 
                {
                    this.messageDiv.InnerText = "Restore Successfully Done.";
                    clearDiv();
                    RBOperation.SelectedValue = null;
                    lblFilePath.Text = string.Empty;
                    return;
                }
                
            }
        }

        protected void RBOperation_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblFilePath.Text = string.Empty;
        }

    }
}