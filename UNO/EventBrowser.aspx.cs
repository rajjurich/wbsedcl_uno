using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Collections.Specialized;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;

namespace UNO
{
    public partial class EventBrowser : System.Web.UI.Page
    {
       static  SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public static String _strDecodeForCARDSTATUS = "CASE AE.Event_Status ";
        public static String _strDecodeForALARMTYPE = "CASE AE.Event_Alarm_Type ";
        public static String _strDecodeForALARMACTION = "CASE AE.Event_Alarm_Action ";
        public static String _strDecodeForEVENTTYPE = "CASE AE.Event_Type ";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                {

                    setDecodeString();
                    FillData(RBLDataType.SelectedValue.ToString(), txtEmpID.Text, txtEmpName.Text);
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }
        public DataTable getDataTable(string _strQuery, SqlConnection con)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, con);
                _result = _sqa.Fill(_ds);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();

                }
                return _ds.Tables[0];
            }
            catch (Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }
                //UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
                return null;
            }
        }
        public void setDecodeString()
        {
            try
            {

               
                _strDecodeForCARDSTATUS = "CASE AE.Event_Status ";
                _strDecodeForALARMTYPE = "CASE AE.Event_Alarm_Type ";
                _strDecodeForALARMACTION = "CASE AE.Event_Alarm_Action ";
                _strDecodeForEVENTTYPE = "CASE AE.Event_Type ";

                String _strString = "select * from ENT_PARAMS with(nolock) where MODULE='ACS' and IDENTIFIER in('CARDSTATUS','ALARMTYPE','ALARMACTION','EVENTTYPE')";
                DataTable _dtParams = new DataTable();
                _dtParams = getDataTable(_strString, conn);
                //DataRow _dr=new DataRow();
                DataRow[] _dr = _dtParams.Select("IDENTIFIER= 'CARDSTATUS'");

                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForCARDSTATUS = _strDecodeForCARDSTATUS + " WHEN ''" + Convert.ToInt64(_dr[i]["CODE"].ToString()) + "'' THEN ''" + _dr[i]["VALUE"] + "''";
                }
                _strDecodeForCARDSTATUS = _strDecodeForCARDSTATUS + " ELSE NULL END AS Event_Status";

                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'ALARMTYPE'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForALARMTYPE = _strDecodeForALARMTYPE + " WHEN ''" + Convert.ToInt64(_dr[i]["CODE"].ToString()) + "'' THEN ''" + _dr[i]["VALUE"] + "''";
                }
                _strDecodeForALARMTYPE = _strDecodeForALARMTYPE + " ELSE NULL END AS Event_Alarm_Type";

                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'ALARMACTION'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForALARMACTION = _strDecodeForALARMACTION + " WHEN ''" + _dr[i]["CODE"] + "'' THEN ''" + _dr[i]["VALUE"] + "''";
                }
                _strDecodeForALARMACTION = _strDecodeForALARMACTION + " ELSE NULL END AS Event_Alarm_Action";


                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'EVENTTYPE'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForEVENTTYPE = _strDecodeForEVENTTYPE + " WHEN ''" + _dr[i]["CODE"] + "'' THEN ''" + _dr[i]["VALUE"] + "''";
                }
                _strDecodeForEVENTTYPE = _strDecodeForEVENTTYPE + " ELSE NULL END AS Event_Type";
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }


        }   
     
        protected void btnFillData_Click(object sender, EventArgs e)
        {
            hdnRecords.Value = FillData(RBLDataType.SelectedValue.ToString(), txtEmpID.Text, txtEmpName.Text);
        }     
        public  string FillData(string RBLDataType, string txtUserID, string txtLevelID)
        {
            try
            {

                SqlDataAdapter da;
                DataTable dt;
                string _strLevel=string.Empty;
                _strLevel=ddlLevel.SelectedIndex==0?"":ddlLevel.SelectedIndex==1?"Ta":ddlLevel.SelectedIndex==2?"P":"";
                da = new SqlDataAdapter("EXEC USP_Get_EventViewerDetails @strDecodeForCARDSTATUS='" + _strDecodeForCARDSTATUS + "',@strDecodeForALARMTYPE='" + _strDecodeForALARMTYPE + "',@strDecodeForALARMACTION='" + (_strDecodeForALARMACTION) + "',@strDecodeForEVENTTYPE='" + (_strDecodeForEVENTTYPE) + "',@strEventTypeType='" + (RBLDataType.ToString()) + "',@strLevelType='" + _strLevel + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
                
                
                
                
                hdnRecords.Value = GetJSONString(dt);
                return hdnRecords.Value.ToString();

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
                return null;
            }
        }
        public static string GetJSONString(DataTable Dt)
        {
            string[] StrDc = new string[Dt.Columns.Count];
            string HeadStr = string.Empty;


            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }
            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
            StringBuilder Sb = new StringBuilder();
            Sb.Append(Dt.TableName + "[");
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string TempStr = HeadStr;
                Sb.Append("{");


                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    switch (Dt.Columns[j].DataType.ToString())
                    {
                        case "System.DateTime":
                            DateTime cv = (DateTime)Dt.Rows[i][j];
                            //TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", cv.Day + "," + (cv.Month) + "," + cv.Year + "," + cv.Hour + "," + cv.Minute + "," + cv.Second);
                            TempStr = TempStr.Replace(("\""+Dt.Columns[j]+ j.ToString() + "¾"+"\""),new JavaScriptSerializer().Serialize(cv));

                            break;




                        case "System.Boolean":
                            TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().ToLower());
                            break;


                        default:
                            string str = Dt.Rows[i][j].ToString();
                            //str = str.Replace("\n", "\\\\n");
                            //str = str.Replace("\\", "\\\\\\\\");
                            //str = str.Replace("\"", "ppp");
                            //str = str.Replace("'", "\\\'");
                            //str = str.Replace("\r", "\\\\r");


                            //TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", str);
                            TempStr = TempStr.Replace(("\"" + Dt.Columns[j] + j.ToString() + "¾" + "\""), new JavaScriptSerializer().Serialize(str));
                            break;
                    }
                }


                Sb.Append(TempStr + "},");
            }


            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            Sb.Append("]");


            return Sb.ToString();
        }        
    }

}