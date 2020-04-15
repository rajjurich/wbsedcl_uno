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

namespace UNO
{
    public partial class EventBrowser_old : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
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
                    gvEvent.PageSize = 10;
                    setDecodeString();
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
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

                String _strString = "select * from ENT_PARAMS where MODULE='ACS' and IDENTIFIER in('CARDSTATUS','ALARMTYPE','ALARMACTION','EVENTTYPE')";
                DataTable _dtParams = new DataTable();
                _dtParams = getDataTable(_strString, conn);
                //DataRow _dr=new DataRow();
                DataRow[] _dr = _dtParams.Select("IDENTIFIER= 'CARDSTATUS'");

                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForCARDSTATUS = _strDecodeForCARDSTATUS + " WHEN '" + Convert.ToInt64(_dr[i]["CODE"].ToString()) + "' THEN '" + _dr[i]["VALUE"] + "'";
                }
                _strDecodeForCARDSTATUS = _strDecodeForCARDSTATUS + " ELSE NULL END AS Event_Status";

                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'ALARMTYPE'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForALARMTYPE = _strDecodeForALARMTYPE + " WHEN '" + Convert.ToInt64(_dr[i]["CODE"].ToString()) + "' THEN '" + _dr[i]["VALUE"] + "'";
                }
                _strDecodeForALARMTYPE = _strDecodeForALARMTYPE + " ELSE NULL END AS Event_Alarm_Type";

                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'ALARMACTION'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForALARMACTION = _strDecodeForALARMACTION + " WHEN '" + _dr[i]["CODE"] + "' THEN '" + _dr[i]["VALUE"] + "'";
                }
                _strDecodeForALARMACTION = _strDecodeForALARMACTION + " ELSE NULL END AS Event_Alarm_Action";


                _dr = null;
                _dr = _dtParams.Select("IDENTIFIER= 'EVENTTYPE'");
                for (int i = 0; i < (_dr.GetLength(0)); i++)
                {
                    _strDecodeForEVENTTYPE = _strDecodeForEVENTTYPE + " WHEN '" + _dr[i]["CODE"] + "' THEN '" + _dr[i]["VALUE"] + "'";
                }
                _strDecodeForEVENTTYPE = _strDecodeForEVENTTYPE + " ELSE NULL END AS Event_Type";
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }


        }

        public void bindDataGrid()
        {
            try
            {            
                String _strWhereClause="";
                if (RBLDataType.SelectedValue.ToString() == "A")
                {_strWhereClause = "where AE.Event_Type in('01','02','1','2')";}
                if (RBLDataType.SelectedValue.ToString() == "01")
                {_strWhereClause = "where AE.Event_Type IN ('01','1')";}
                if (RBLDataType.SelectedValue.ToString() == "02")
                {_strWhereClause = "where AE.Event_Type IN ('02','2')";}

                //+EMP.EPD_MIDDLE_NAME+' '

                string strsql = "select AE.Event_ID," + _strDecodeForEVENTTYPE + ",AE.Event_Datetime ," + //AE.Event_Trace,"+
                                "Case when AE.Event_Employee_Code=NULL then NULL " +
                                "else ( select EMP.EPD_FIRST_NAME+' '+EMP.EPD_LAST_NAME from ENT_EMPLOYEE_PERSONAL_DTLS EMP where (EMP.EPD_EMPID) = AE.Event_Employee_Code) " +
                                "End as Empname, " +
                                "Case when AE.Event_Employee_Code=NULL then NULL else AE.Event_Employee_Code END as Event_Employee_Code, " +
                                "Case  When AE.Event_Reader_ID=NULL then NULL "+
                                "else ( Select READER_DESCRIPTION from ACS_READER where READER_ID=AE.Event_Reader_ID and CTLR_ID=AE.Event_Controller_ID) END as ReaderD ," +
                                "Case When AE.Event_Controller_ID=NULL then NULL " +
                                "else ( Select CTLR_DESCRIPTION from ACS_CONTROLLER where CTLR_ID= AE.Event_Controller_ID) END as CtrlD, "+
                                _strDecodeForCARDSTATUS + ","+ _strDecodeForALARMTYPE +","  + _strDecodeForALARMACTION + " "+
                                " ,Event_Card_Code " +
                                "from ACS_Events AE " + _strWhereClause + " and Convert(varchar(20), AE.Event_Datetime, 101)= Convert(varchar(20), getdate(), 101) "+
                                "order by  AE.Event_Datetime desc ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable _dt = new DataTable();
                _dt=getDataTable(strsql,conn);



                if (_dt.Rows.Count != 0)
                {

                    gvEvent.DataSource = _dt;
                    gvEvent.DataBind();


                    if (RBLDataType.SelectedValue.ToString() == "01")
                    {
                        gvEvent.HeaderRow.Cells[0].Visible = false;
                        gvEvent.HeaderRow.Cells[8].Visible = false;
                        gvEvent.HeaderRow.Cells[9].Visible = false;

                        gvEvent.Columns[0].Visible = false;
                        gvEvent.Columns[8].Visible = false;
                        gvEvent.Columns[9].Visible = false;

                        //Unit p = Unit.Pixel(800);
                        //gvEvent.Width = p;
                    }
                    if (RBLDataType.SelectedValue.ToString() == "02")
                    {
                        
                        gvEvent.HeaderRow.Cells[0].Visible = false;
                        gvEvent.HeaderRow.Cells[5].Visible = false;
                        gvEvent.HeaderRow.Cells[6].Visible = false;
                        gvEvent.HeaderRow.Cells[7].Visible = false;

                        gvEvent.Columns[0].Visible = false;
                        gvEvent.Columns[5].Visible = false;
                        gvEvent.Columns[6].Visible = false;
                        gvEvent.Columns[7].Visible = false;
                    }

                    if (RBLDataType.SelectedValue.ToString() == "A")
                    {
                        //Unit p = Unit.Pixel(1000);
                        //gvEvent.Width = p;
                    }
                }
                else
                {
                    gvEvent.DataSource = null;
                    gvEvent.DataBind();
                }
                DropDownList ddl = (DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEvent.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEvent.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEvent.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEvent.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEvent.PageCount == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex + 1 == gvEvent.PageCount)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (gvEvent.PageSize * (gvEvent.PageIndex + 1));

                ((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (((gvEvent.PageSize * (gvEvent.PageIndex + 1)) - 10) + gvEvent.Rows.Count);

                gvEvent.BottomPagerRow.Visible = true;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String _strWhereClause = "";
                if (RBLDataType.SelectedValue.ToString() == "A")
                { _strWhereClause = "where AE.Event_Type in('01','02')"; }
                if (RBLDataType.SelectedValue.ToString() == "01")
                { _strWhereClause = "where AE.Event_Type='01'"; }
                if (RBLDataType.SelectedValue.ToString() == "02")
                { _strWhereClause = "where AE.Event_Type='02'"; }
                string strsql = "select AE.Event_ID," + _strDecodeForEVENTTYPE + ",AE.Event_Datetime ," + //AE.Event_Trace,"+
                                "Case when AE.Event_Employee_Code=NULL then NULL " +
                                "else ( select EMP.EPD_FIRST_NAME+' '+EMP.EPD_LAST_NAME from ENT_EMPLOYEE_PERSONAL_DTLS EMP where EMP.EPD_EMPID=AE.Event_Employee_Code) " +
                                "End as Empname, " +
                                "Case when AE.Event_Employee_Code=NULL then NULL else AE.Event_Employee_Code END as Event_Employee_Code, " +
                                "Case  When AE.Event_Reader_ID=NULL then NULL " +
                                "else ( Select READER_DESCRIPTION from ACS_READER where READER_ID=AE.Event_Reader_ID and CTLR_ID=AE.Event_Controller_ID) END as ReaderD ," +
                                "Case When AE.Event_Controller_ID=NULL then NULL " +
                                "else ( Select CTLR_DESCRIPTION from ACS_CONTROLLER where CTLR_ID= AE.Event_Controller_ID) END as CtrlD, " +
                                _strDecodeForCARDSTATUS + "," + _strDecodeForALARMTYPE + "," + _strDecodeForALARMACTION + " " +
                                " ,Event_Card_Code " +
                                "from ACS_Events AE " + _strWhereClause + " and Convert(varchar(20), AE.Event_Datetime, 101)= Convert(varchar(20), getdate(), 101) " +
                                "order by  AE.Event_Datetime desc ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtUserID.Text.ToString() == "" && txtLevelID.Text.ToString() == "")
                {
                    gvEvent.DataSource = dt;
                    gvEvent.DataBind();
                }
                else
                {
                    String[,] values = { 
                {"Event_Employee_Code-" +txtUserID.Text.Trim(), "S" },
                {"Empname-" +txtLevelID.Text.Trim(), "S" }			
                 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEvent.DataSource = _tempDT;
                    gvEvent.DataBind();
                }
                DropDownList ddl = (DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEvent.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEvent.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEvent.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEvent.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEvent.PageCount == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex + 1 == gvEvent.PageCount)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEvent.PageIndex == 0)
                {
                    ((Button)gvEvent.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (gvEvent.PageSize * (gvEvent.PageIndex + 1));

                ((Label)gvEvent.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEvent.PageSize * gvEvent.PageIndex) + 1) + " to " + (((gvEvent.PageSize * (gvEvent.PageIndex + 1)) - 10) + gvEvent.Rows.Count);

                gvEvent.BottomPagerRow.Visible = true;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "HolidayView");
            }
        }

        protected void OnTimerIntervalElapse(object sender, EventArgs e)
        {
           
                bindDataGrid();         
        }

        protected void RBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindDataGrid();
                
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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
                return null;
            }
        }

        public string getValue(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                    _sqlconn.Open();
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }

            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
            UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            return "";
            }
        }

        protected void CmdPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmdPause.Text == "Release")
                {
                    ctlTimer.Enabled = false;
                    CmdPause.Text = "Start";
                }
                else
                {
                    ctlTimer.Enabled = true;
                    CmdPause.Text = "Release";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }

        }

        protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string _lsDataKeyValue = (String)gvEvent.DataKeys[e.Row.RowIndex].Values[1].ToString();
                    if (_lsDataKeyValue == "CARD")
                    {
                        if (e.Row.Cells[7].Text.Trim() == "Access Granted")
                        { e.Row.BackColor = System.Drawing.Color.Green; }
                        else
                        { e.Row.BackColor = System.Drawing.Color.Red; }


                        if (RBLDataType.SelectedValue.ToString() == "01")
                        {
                            e.Row.Cells[0].Visible = false;
                            e.Row.Cells[8].Visible = false;
                            e.Row.Cells[9].Visible = false;
                        }
                    }
                    else if (_lsDataKeyValue == "ALARM")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        if (RBLDataType.SelectedValue.ToString() == "02")
                        {
                            e.Row.Cells[0].Visible = false;
                            e.Row.Cells[5].Visible = false;
                            e.Row.Cells[6].Visible = false;
                            e.Row.Cells[7].Visible = false;
                        }
                    }

                    int i = 0;

                    foreach (TableCell cell in e.Row.Cells)
                    {
                        i++;
                        string s = cell.Text;

                        if (RBLDataType.SelectedValue == "A")
                        {
                            if (cell.Text.Length > 10 && (i == 2))
                                cell.Text = cell.Text.Substring(0, 10) + "....";
                            else if (cell.Text.Length > 10 && (i == 4))
                                cell.Text = cell.Text.Substring(0, 10) + "....";
                            else if (cell.Text.Length > 10 && (i == 7))
                                cell.Text = cell.Text.Substring(0, 10) + "....";
                            else if (cell.Text.Length > 10 && (i == 8))
                                cell.Text = cell.Text.Substring(0, 10) + "....";
                            else if (cell.Text.Length > 10 && (i == 9))
                                cell.Text = cell.Text.Substring(0, 10) + "....";
                        }

                        cell.ToolTip = s;
                    }




                }
            }
            catch(Exception ex)
            {
              
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }
 
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = Convert.ToInt32(((DropDownList)gvEvent.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = gvEvent.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEvent.PageIndex = gvEvent.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "EventBrowser");
            }
        }
    }
}