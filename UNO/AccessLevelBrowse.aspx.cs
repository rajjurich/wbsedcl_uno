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
using System.Text;
using CMS.UNO.Framework.DataAccess;
using CMS.Framework.Common;
namespace UNO
{
    public partial class AccessLevelBrowse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgvAccessLevel();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvAccessLevel.ClientID + "');");
            }
        }
        public DataSet fillReaderAndZone()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string levelId = Session["levelId"].ToString();
            SqlCommand cmd = new SqlCommand("fillAccess_Settings2", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelId", levelId);
            DataSet vai = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(vai);
            return vai;
        }
        public Int16 getId()
        {
            Int16 _apID;
            _apID = Convert.ToInt16(getValue("select ISNULL(min(AL_ID),0) from ACS_ACCESSLEVEL where AL_ISDELETED='1'", conn));
            if (_apID == 0)
            {
                _apID = Convert.ToInt16(getValue("select ISNULL(max(AL_ID)+1,1) from ACS_ACCESSLEVEL where AL_ISDELETED='0'", conn));
            }
            else
            {
                Session.Add("_IDExists", "TRUE");
            }
            return _apID;
        }
        public string getValue(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);

                return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
                return "";
            }
        }
        public DataTable getDataTable(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                DataSet _ds = new DataSet();
                SqlDataAdapter _sqa = new SqlDataAdapter(_strQuery, _sqlconn);
                _result = _sqa.Fill(_ds);
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
                return null;
            }
        }
        public bool RunExecuteNonQuery(string _strQuery, SqlConnection _sqlconn)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
                int _result = 0;
                SqlCommand _sc = new SqlCommand();
                _sc.Connection = _sqlconn;
                _sc.CommandText = _strQuery;
                _result = _sc.ExecuteNonQuery();
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                if (_result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
                return false;
            }
        }
        public bool RunExecuteNonQueryWithTransaction(string _strQuery, SqlConnection _sqlconn, SqlTransaction _sqlTrans)
        {
            try
            {
                if (_sqlconn.State == ConnectionState.Closed)
                {
                    _sqlconn.Open();
                }
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
                if (_sqlconn.State == ConnectionState.Open)
                {
                    _sqlconn.Close();
                }
                return false;
            }
        }
        protected void cmdALLALRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAReaderAdd.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSReaderAdd.Items.Add(_li);

            }
            lstAReaderAdd.Items.Clear();
            lstSReaderAdd.SelectedValue = null;
        }
        protected void cmdALLALLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSReaderAdd.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAReaderAdd.Items.Add(_li);
            }
            lstSReaderAdd.Items.Clear();
            lstAReaderAdd.SelectedValue = null;
        }
        protected void cmdModifyALLReaderRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAReaderEdit.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAReaderEdit.Items.Add(_li);

            }
            lstAReaderEdit.Items.Clear();
            lstSReaderEdit.SelectedValue = null;
        }
        protected void cmdALLModifyReaderLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSReaderEdit.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAReaderEdit.Items.Add(_li);
            }
            lstSReaderEdit.Items.Clear();
            lstAReaderEdit.SelectedValue = null;
        }
        public void fillZoneReadersAdd()
        {
            lstAReaderAdd.Items.Clear();
            lstSReaderAdd.Items.Clear();
            try
            {
                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(30),AR.READER_DESCRIPTION)+ AC.CTLR_DESCRIPTION,' ',' ' )  as DESCRIPTION " +
               "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
               "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
               "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
               "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZoneAdd.SelectedValue + "' and ZONER_ISDELETED='0'" +
               ")";

                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, conn);
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstSReaderAdd.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void fillReadersAdd()
        {
            lstAReaderAdd.Items.Clear();
            lstSReaderAdd.Items.Clear();
            try
            {
                //if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                //{
                    string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(17),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,30)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,100),' ',' ' ) as DESCRIPTION " +
                                    "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                                    "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                                    "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                                    "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                                    ")";
                    DataTable _dt = new DataTable();
                    _dt = getDataTable(_strsql, conn);

                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            lstAReaderAdd.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
              //  }
                //else
                //{
                //    DataSet ds = fillReaderAndZone();

                //    if (ds.Tables[0] != null)
                //    {
                //        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                //        {
                //            lstAReaderAdd.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString()));
                //        }
                //    }
                //}
            }

            catch (Exception ex)
            { throw ex; }
        }
        public Boolean validationAdd()
        {
            try
            {
                string script = "";

                if (txtdescriptionAdd.Text.Trim() == "")
                {
                    lblErrorAdd.Text = "Please enter Description";
                    lblErrorAdd.Visible = true;
                    //this.messageDiv.InnerHtml = "Please enter Description";
                    return false;
                }
                
                else if (RBLZoneAdd.SelectedValue == "R" && lstSReaderAdd.Items.Count == 0)
                { //this.messageDiv.InnerHtml = "Please select Reader(s)."; return false; 
                    script = "<script language='javascript'>PushAlert('Please select Reader(s).');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script);
                    return false;
                }

                else if (RBLZoneAdd.SelectedValue == "Z" && lstSReaderAdd.Items.Count == 0)
                { //this.messageDiv.InnerHtml = "Please select valid Zone"; return false; 
                    script = "<script language='javascript'>PushAlert('Please select valid Zone.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", script);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void bindgvAccessLevel()
        {
            try
            {
                string strsql = "select AL.AL_ID,AL.AL_DESCRIPTION,TZ_DESCRIPTION,case when con.al_id is null then 'A' else 'P' end 'IsExists' " +
                                     "  from ACS_ACCESSLEVEL AL with(nolock) LEFT OUTER JOIN  ACS_TIMEZONE TZ with(nolock) " +
                                     //" ON AL.AL_TIMEZONE_ID=TZ.TZ_ID left outer join eal_config con with(nolock) on con.al_id=al.al_id  where AL.AL_ISDELETED='False' " +
                                     " ON AL.AL_TIMEZONE_ID=TZ.TZ_CODE left outer join eal_config con with(nolock) on con.al_id=al.al_id  where AL.AL_ISDELETED='False' " +
                                     " group by AL.AL_ID,AL.AL_DESCRIPTION,TZ_DESCRIPTION,con.al_id order by AL_ID,AL_DESCRIPTION";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvAccessLevel.DataSource = dt;
                gvAccessLevel.DataBind();

                DropDownList ddl = (DropDownList)gvAccessLevel.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvAccessLevel.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvAccessLevel.PageIndex + 1).ToString();
                Label lblcount = (Label)gvAccessLevel.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvAccessLevel.DataSource).Rows.Count.ToString() + " Records.";
                if (gvAccessLevel.PageCount == 0)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAccessLevel.PageIndex + 1 == gvAccessLevel.PageCount)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAccessLevel.PageIndex == 0)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                
                ((Label)gvAccessLevel.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvAccessLevel.PageSize * gvAccessLevel.PageIndex) + 1) + " to " + (((gvAccessLevel.PageSize * (gvAccessLevel.PageIndex + 1)) - 10) + gvAccessLevel.Rows.Count);

                gvAccessLevel.BottomPagerRow.Visible = true;

                if (dt.Rows.Count != 0)
                {
                    btnDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvAccessLevel.PageIndex = Convert.ToInt32(((DropDownList)gvAccessLevel.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                btnSearch_Click(sender, e);
                //bindgvAccessLevel();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvAccessLevel.PageIndex = gvAccessLevel.PageIndex - 1;
                btnSearch_Click(sender, e);
                //bindgvAccessLevel();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvAccessLevel.PageIndex = gvAccessLevel.PageIndex + 1;
                btnSearch_Click(sender, e);
                //bindgvAccessLevel();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblZoneAdd.Visible = false;
                cmbZoneAdd.Visible = false;

                Session.Remove("_IDExists");
                // txtalid.Text = getValue("select ISNULL(max(AL_ID)+1,1) as id from ACS_ACCESSLEVEL", _sqlConnection);
                txtalidAdd.Text = Convert.ToString(getId());
                txtdescriptionAdd.Text = "";

                cmbZoneAdd.Items.Clear();
                cmbZoneAdd.Items.Add(new ListItem("Select Zone", "-1"));
                cmbZoneAdd.SelectedValue = "-1";

                cmbTimeZoneAdd.Items.Clear();
                cmbTimeZoneAdd.Items.Add(new ListItem("Select Time Zone", "-1"));
                cmbTimeZoneAdd.SelectedValue = "-1";
                try
                {
                    string _strsql = "select ZONE_ID,ZONE_DESCRIPTION from ZONE where ZONE_ISDELETED='0'";
                    DataTable _dt = new DataTable();
                    _dt = getDataTable(_strsql, conn);
                    if (_dt.Rows.Count != 0)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            cmbZoneAdd.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    string _strsql = "select Distinct(TZ_CODE),TZ_DESCRIPTION from ACS_TIMEZONE where TZ_ISDELETED='0'";
                    DataTable _dt = new DataTable();
                    _dt = getDataTable(_strsql, conn);
                    if (_dt.Rows.Count != 0)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            cmbTimeZoneAdd.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                RBLZoneAdd.SelectedValue = null;
                lstAReaderAdd.Items.Clear();
                lstSReaderAdd.Items.Clear();
                mpeAddAccessLevel.Show();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }

        }
        protected void RBLZoneAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBLZoneAdd.SelectedValue == "Z")
                {
                    cmbZoneAdd.SelectedValue = "-1";
                    lstAReaderAdd.Items.Clear();
                    lstSReaderAdd.Items.Clear();
                    lblSelectedAdd.Visible = true;

                    lstSReaderAdd.Visible = true;

                    //lstAReaderAdd.Visible = false;


                    cmdReaderLeftAdd.Visible = true;
                    cmdReaderRightAdd.Visible = true;
                    cmdALLRDRLeft.Visible = true;
                    cmdALLRDRRight.Visible = true;

                    lstAReaderAdd.Items.Clear();
                    lstSReaderAdd.Items.Clear();
                    // lblAvailableAdd.Text = "Readers Included in Zone";
                    lblrederadd.Visible = false;
                    lblAvailableAdd.Text = "Available";
                    lblZoneAdd.Visible = true;
                    cmbZoneAdd.Visible = false;
                    fillZoneLstAdd();
                }
                else
                {
                    lblSelectedAdd.Visible = true;


                    lstSReaderAdd.Visible = true;
                    lstAReaderAdd.Visible = true;
                    cmdALLRDRLeft.Visible = true;
                    cmdALLRDRRight.Visible = true;
                    lblrederadd.Visible = true;
                    cmdReaderLeftAdd.Visible = true;
                    cmdReaderRightAdd.Visible = true;
                    lblAvailableAdd.Text = "Available";
                    lblZoneAdd.Visible = false;
                    cmbZoneAdd.Visible = false;


                    fillReadersAdd();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void cmbZoneAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbZoneAdd.SelectedValue == "-1")
                {
                    lstAReaderAdd.Items.Clear();
                    lstSReaderAdd.Items.Clear();
                }
                else
                {

                    fillZoneReadersAdd();

                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void cmdReaderRightAdd_Click(object sender, EventArgs e)
        {
            if (lstAReaderAdd.SelectedItem != null)
            {
                for (int i = lstAReaderAdd.Items.Count - 1; i >= 0; i--)
                {
                    if (lstAReaderAdd.Items[i].Selected == true)
                    {
                        lstSReaderAdd.Items.Add(lstAReaderAdd.Items[i]);
                        ListItem li = lstAReaderAdd.Items[i];
                        lstAReaderAdd.Items.Remove(li);
                    }
                }
                lstSReaderAdd.SelectedValue = null;
            }
        }
        protected void cmdReaderLeftAdd_Click(object sender, EventArgs e)
        {
            if (lstSReaderAdd.SelectedItem != null)
            {
                for (int i = lstSReaderAdd.Items.Count - 1; i >= 0; i--)
                {
                    if (lstSReaderAdd.Items[i].Selected == true)
                    {
                        lstAReaderAdd.Items.Add(lstSReaderAdd.Items[i]);
                        ListItem li = lstSReaderAdd.Items[i];
                        lstSReaderAdd.Items.Remove(li);
                    }
                }
                lstAReaderAdd.SelectedValue = null;
            }
        }
        protected void btnAccessLevelCancel_Click(object sender, EventArgs e)
        {
            mpeAddAccessLevel.Hide();
        }
        protected void btnAccessLevelAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid && validationAdd())
                {
                    string _strInsertQuery = "";
                    if (Session["_IDExists"] != null)
                    {
                        _strInsertQuery = "Update ACS_ACCESSLEVEL set AL_DESCRIPTION='" + txtdescriptionAdd.Text + "',AL_TIMEZONE_ID='" + cmbTimeZoneAdd.SelectedValue + "',AL_ISDELETED='False',AL_DELETEDDATE=null " +
                            "where AL_ID='" + txtalidAdd.Text.Trim() + "'";

                    }
                    else
                    {
                        _strInsertQuery = "insert into ACS_ACCESSLEVEL(AL_ID,AL_DESCRIPTION,AL_TIMEZONE_ID,AL_ISDELETED,AL_DELETEDDATE) " +
                        " values(" + txtalidAdd.Text.Trim() + ",'" + txtdescriptionAdd.Text + "','" + cmbTimeZoneAdd.SelectedValue + "','False',null)";

                    }
                    Boolean _result = RunExecuteNonQuery(_strInsertQuery, conn);
                    string[] _ReaderController;
                    //String _strALID = _result == true ? getValue("select SCOPE_IDENTITY()", _sqlConnection) : "";
                    if (_result == true)
                    {
                        if (RBLZoneAdd.SelectedValue == "Z")
                        {
                            //added by vaibhav  on 2-10-2015
                            for (int i = 0; lstSReaderAdd.Items.Count > i; i++)
                            {
                                string[] zoneid=lstSReaderAdd.Items[i].Value.ToString().Split('-');

                                string strsql = "SELECT * FROM ZONE_READER_REL WHERE ZONE_ID ='" + zoneid[1].ToString() + "'";
                                SqlCommand cmd = new SqlCommand(strsql, conn);
                                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                dap.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for(int j=0;j <dt.Rows.Count;j++)
                                    {
                                            string zone_Id,readerId,ControllerId;
                                            zone_Id=dt.Rows[j]["ZONE_ID"].ToString();
                                            readerId=dt.Rows[j]["READER_ID"].ToString();
                                            ControllerId=dt.Rows[j]["CONTROLLER_ID"].ToString();
                                            //added by vaibhav  2-9-2015
                                            StringBuilder accLevelAraay = new StringBuilder();
                                            SqlCommand cmd1 = new SqlCommand("select AccesLevelArray from ACS_ACCESSLEVEL_RELATION WHERE CONTROLLER_ID=" + ControllerId + " and AL_ID=" + txtalidAdd.Text + " ", conn);
                                            SqlDataAdapter dap1 = new SqlDataAdapter(cmd1);
                                            DataTable dt1 = new DataTable();
                                            dap1.Fill(dt1);
                                            if (dt1.Rows.Count > 0)
                                            {
                                                accLevelAraay.Append(dt1.Rows[0]["AccesLevelArray"].ToString());
                                            }
                                            else
                                            {
                                                accLevelAraay.Append("00000000");
                                            }
                                            if (accLevelAraay.Length == 0)
                                            {
                                                accLevelAraay.Append("00000000");
                                            }

                                            int arrayPos = Convert.ToInt32(readerId);


                                            accLevelAraay.Remove(accLevelAraay.Length - arrayPos, 1);
                                            accLevelAraay.Insert(accLevelAraay.Length - arrayPos + 1, '1');
                                            // vaibhav end

                                            _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,ZoneId,RD_ZN_ID,CONTROLLER_ID,AL_ENTITY_TYPE,ALR_ISDELETED,ALR_DELETEDDATE,Action_flag,AccesLevelArray) " +
                                 " values('" + txtalidAdd.Text.Trim() + "','" + zone_Id + "','"+readerId+"','"+ControllerId+"','Z','False',null,0,'"+accLevelAraay.ToString()+"')";

                                    _result = RunExecuteNonQuery(_strInsertQuery, conn);


                                    _strInsertQuery = "UPDATE ACS_ACCESSLEVEL_RELATION SET AccesLevelArray='" + accLevelAraay.ToString() + "' "+
                                        " WHERE AL_ID='" + txtalidAdd.Text.Trim() + "' AND ZoneId='" + zone_Id + "' AND CONTROLLER_ID=" + ControllerId + " ";
                                    _result = RunExecuteNonQuery(_strInsertQuery, conn);


                                    }
                                
                                }
                            
                            }
                        }
                        else
                        {
                            for (int i = 0; lstSReaderAdd.Items.Count > i; i++)
                            {
                               
                                _ReaderController = lstSReaderAdd.Items[i].Value.ToString().Split('-');
                                //added by vaibhav  2-9-2015
                                StringBuilder accLevelAraay = new StringBuilder();
                                SqlCommand cmd = new SqlCommand("select AccesLevelArray from ACS_ACCESSLEVEL_RELATION WHERE CONTROLLER_ID=" + Convert.ToInt32(_ReaderController[0]) + " and AL_ID=" + txtalidAdd.Text + " ", conn);
                                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                dap.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    accLevelAraay.Append(dt.Rows[0]["AccesLevelArray"].ToString());
                                }
                                else
                                {
                                    accLevelAraay.Append("00000000");
                                }
                                if (accLevelAraay.Length == 0)
                                {
                                    accLevelAraay.Append("00000000");
                                }
                                
                                int arrayPos = Convert.ToInt32(_ReaderController[1]);
                             
                               
                                accLevelAraay.Remove(accLevelAraay.Length-arrayPos,1);
                                accLevelAraay.Insert(accLevelAraay.Length - arrayPos+1, '1');
                               
                                _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,CONTROLLER_ID,ALR_ISDELETED,ALR_DELETEDDATE,Action_flag,AccesLevelArray) " +
                                    " values('" + txtalidAdd.Text.Trim() + "','" + _ReaderController[1].ToString() + "','R'," + _ReaderController[0].ToString() + ",'False',null,0,'" + accLevelAraay.ToString() + "')";
                                _result = RunExecuteNonQuery(_strInsertQuery, conn);

                                _strInsertQuery = "UPDATE ACS_ACCESSLEVEL_RELATION SET AccesLevelArray='" + accLevelAraay.ToString() + "' "+
                                        " WHERE AL_ID='" + txtalidAdd.Text.Trim() + "' AND CONTROLLER_ID=" + _ReaderController[0].ToString() + " ";
                                _result = RunExecuteNonQuery(_strInsertQuery, conn);

                            }
                        }

                        String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                        Boolean _bl = RunExecuteNonQuery(_updateStr, conn);


                        lblMessages.Text = "Record Saved Successfully";
                        lblMessages.Visible = true;

                        //this.messageDiv.InnerHtml = "Record Saved Successfully";
                        cmbZoneAdd.Items.Clear();
                        cmbTimeZoneAdd.Items.Clear();
                        lstAReaderAdd.Items.Clear();
                        lstSReaderAdd.Items.Clear();
                        lblZoneAdd.Visible = false;
                        cmbZoneAdd.Visible = false;
                        RBLZoneAdd.SelectedValue = null;
                        bindgvAccessLevel();
                        //intializeControl();
                        mpeAddAccessLevel.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void gvAccessLevel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Modify")
                {
                    lblZoneEdit.Visible = false;
                    cmbZoneEdit.Visible = false;
                    txtalidEdit.Text = e.CommandArgument.ToString();
                    DataTable _dt = new DataTable();
                    DataTable _dtALRelation = new DataTable();

                    _dt = getDataTable("select * from ACS_ACCESSLEVEL where AL_ID='" + txtalidEdit.Text + "'", conn);

                    GetEAL_Config();
                    txtdescriptionEdit.Text = _dt.Rows[0]["AL_DESCRIPTION"].ToString();

                    //String _entityType = _dt.Rows[0]["AL_ENTITY_TYPE"].ToString();
                    String _entityType = getValue("select distinct(AL_ENTITY_TYPE) from ACS_ACCESSLEVEL_RELATION where AL_ID='" + txtalidEdit.Text + "'", conn);



                    String _timeZoneID = _dt.Rows[0]["AL_TIMEZONE_ID"].ToString();

                    FillList(_entityType);

                    //Filling Time Zone
                    cmbTimeZoneEdit.Items.Clear();
                    cmbTimeZoneEdit.Items.Add(new ListItem("Select Time Zone", "-1"));
                    try
                    {
                        String _strsql = "select Distinct(TZ_CODE),TZ_DESCRIPTION from ACS_TIMEZONE where TZ_ISDELETED='0'";
                        _dt = getDataTable(_strsql, conn);
                        if (_dt.Rows.Count != 0)
                        {
                            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                            {
                                cmbTimeZoneEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    cmbTimeZoneEdit.SelectedValue = _timeZoneID;
                    mpeEditAccessLevel.Show();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        public void fillZoneLstAdd()
        {
            string _strsql;
            DataTable _dt;
            try
            {
                _strsql = "select (Cast(ZONE_DESCRIPTION as varchar) +'-' +  cast(ZONE_ID as varchar)) from ZONE where ZONE_ISDELETED='0'";
                _dt = getDataTable(_strsql, conn);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReaderAdd.Items.Add(new ListItem(_dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void fillZoneLstEdit()
        {
            string _strsql;
            DataTable _dt;
            try
            {
                _strsql = "select (Cast(ZONE_DESCRIPTION as varchar) +'-' +  cast(ZONE_ID as varchar)) from ZONE where ZONE_ISDELETED='0'";
                _dt = getDataTable(_strsql, conn);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReaderEdit.Items.Add(new ListItem(_dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void FillList(String _entityType)
        {
            lstAReaderEdit.Items.Clear();
            lstSReaderEdit.Items.Clear();
            DataTable _dt = new DataTable();
            DataTable _dtALRelation = new DataTable();
            string _strsql = "";
            _strsql = "select * from ACS_ACCESSLEVEL_RELATION where AL_ID='" + txtalidEdit.Text + "'";
            _dtALRelation = getDataTable(_strsql, conn);
            cmbZoneEdit.Items.Clear();
            try
            {
               _strsql = "select (Cast(ZONE_DESCRIPTION as varchar) +'-' +  cast(ZONE_ID as varchar)) from ZONE where ZONE_ISDELETED='0' and ZONE_ID NOT IN(SELECT DISTINCT(zoneid) FROM ACS_ACCESSLEVEL_RELATION WHERE AL_ID='" + txtalidEdit.Text + "')";
                _dt = getDataTable(_strsql, conn);
                if (_dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReaderEdit.Items.Add(new ListItem(_dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (_entityType == "Z")
            {

                lblSelectedEdit.Visible = true;
                lstSReaderEdit.Visible = true;
                cmdReaderLeftEdit.Visible = true;
                cmdReaderRightEdit.Visible = true;
                lblAvailableEdit.Text = "";
                lstAReaderEdit.Visible = true;
                lblZoneEdit.Visible = true;
                cmbZoneEdit.Visible = false;
                RBLZoneEdit.SelectedValue = "Z";
                RBLZoneEdit.Enabled = false; 

                cmbZoneEdit.SelectedValue = _dtALRelation.Rows[0]["RD_ZN_ID"].ToString();
                try
                {

                    string _strsql1 = "SELECT (Cast(ZONE_DESCRIPTION as varchar) +'-' +  cast(ZONE_ID as varchar)) FROM ZONE WHERE ZONE_ID  IN(SELECT DISTINCT(zoneid) FROM ACS_ACCESSLEVEL_RELATION WHERE AL_ID='" + txtalidEdit.Text + "')";


                    _dt = getDataTable(_strsql1, conn);
                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            
                            lstSReaderEdit.Items.Add(new ListItem(_dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }

            }
            else
            {
                RBLZoneEdit.Enabled = false; 
                lstAReaderEdit.Items.Clear();
                lstSReaderEdit.Items.Clear();
                lblSelectedEdit.Visible = true;
                lstSReaderEdit.Visible = true;
                cmdReaderLeftEdit.Visible = true;
                cmdReaderRightEdit.Visible = true;
                lblAvailableEdit.Text = "Available";
                lstAReaderEdit.Visible = true;
                lblZoneEdit.Visible = false;
                cmbZoneEdit.Visible = false;
                RBLZoneEdit.SelectedValue = "R";
                try
                {
                    _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(18),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,30)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,30),' ',' ' ) as DESCRIPTION " +
          "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
          "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
          "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
          "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0' " +
          ") " +
          "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) not in(  " +
          "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID " +
          "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='" + txtalidEdit.Text + "' and ALR_ISDELETED='0' " +
          ")";

                    _dt = getDataTable(_strsql, conn);
                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            lstAReaderEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }

                try
                {
                    _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(17),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,14)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,14),' ',' ' ) as DESCRIPTION " +
                              "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(  " +
                              "select (Cast(ALR.CONTROLLER_ID as varchar) +'-' +  cast(ALR.RD_ZN_ID as varchar)) as ID " +
                              "from ACS_ACCESSLEVEL_RELATION ALR where AL_ID='" + txtalidEdit.Text + "' and ALR_ISDELETED='0' " +
                              ")";
                    _dt = getDataTable(_strsql, conn);
                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            lstSReaderEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }

            }

        }
        public void fillZoneReadersEdit()
        {
            lstAReaderEdit.Items.Clear();
            lstSReaderEdit.Items.Clear();
            try
            {
                string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(17),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,30)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,100),' ',' ' ) as DESCRIPTION " +
              "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
              "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in(" +
              "select (Cast(ZR.CONTROLLER_ID as varchar) +'-' +  cast(ZR.READER_ID as varchar)) as ID " +
              "from ZONE_READER_REL ZR where ZONE_ID='" + cmbZoneEdit.SelectedValue + "' and ZONER_ISDELETED='0'" +
              ")";

                DataTable _dt = new DataTable();
                _dt = getDataTable(_strsql, conn);
                if (_dt != null)
                {
                    for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                    {
                        lstAReaderEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        public Boolean validationEdit()
        {
            try
            {
                if (txtalidEdit.Text.Trim() == "")
                {
                    lblErrorEdit.Text = "Please enter Access point ID";
                    lblErrorEdit.Visible = true;
                    //this.messageDiv.InnerHtml = "Please enter Access point ID"; 
                    return false;
                }
                else if (txtdescriptionEdit.Text.Trim() == "")
                {
                    lblErrorEdit.Text = "Please enter Description";
                    lblErrorEdit.Visible = true;
                    //this.messageDiv.InnerHtml = "Please enter Description";
                    return false;
                }
                else if (RBLZoneEdit.SelectedValue == "R" && lstSReaderEdit.Items.Count == 0)
                {
                    lblErrorEdit.Text = "Please select Reader(s).";
                    lblErrorEdit.Visible = true;
                    //this.messageDiv.InnerHtml = "Please select Reader(s)."; 
                    return false;
                }
                else if (RBLZoneEdit.SelectedValue == "Z" && lstAReaderEdit.Items.Count == 0)
                {
                    lblErrorEdit.Text = "Please select valid Zone";
                    lblErrorEdit.Visible = true;
                    
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void RBLZoneEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBLZoneEdit.SelectedValue == "Z")
                {
                    lstAReaderEdit.Items.Clear();
                

                    lblSelectedEdit.Visible = true;
                    lstSReaderEdit.Visible = true;
                    
                    cmdReaderLeftEdit.Visible = true;
                    cmdReaderRightEdit.Visible = true;
                    lblrederadd.Visible = false;

                    cmdALLModifyReaderLeft.Visible = true;
                    cmdModifyALLReaderRight.Visible = true;


                    lstAReaderEdit.Items.Clear();
                    lstSReaderEdit.Items.Clear();
                    lblAvailableEdit.Text = "";
                    lblZoneEdit.Visible = true;
                    cmbZoneEdit.Visible = false;
                    
                    fillZoneLstEdit();
                }
                else
                {

                    cmdALLModifyReaderLeft.Visible = true;
                    cmdModifyALLReaderRight.Visible = true;

                    lblrederadd.Visible = true;


                    lblSelectedEdit.Visible = true;
                    lstSReaderEdit.Visible = true;
                    cmdReaderLeftEdit.Visible = true;
                    cmdReaderRightEdit.Visible = true;
                    //lblrederadd.Visible = true;
                    lblAvailableEdit.Text = "Available";
                    lstAReaderEdit.Visible = true;
                    lblZoneEdit.Visible = false;
                    cmbZoneEdit.Visible = false;
                    lblReaderEdit.Visible = true;

                    fillReadersEdit();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void cmbZoneEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbZoneEdit.SelectedValue != "-1")
                {
                    fillZoneReadersEdit();
                }
                else
                {
                    lstAReaderEdit.Items.Clear();
                    lstSReaderEdit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void cmdReaderRightEdit_Click(object sender, EventArgs e)
        {
            if (lstAReaderEdit.SelectedItem != null)
            {
                for (int i = lstAReaderEdit.Items.Count - 1; i >= 0; i--)
                {
                    if (lstAReaderEdit.Items[i].Selected == true)
                    {
                        lstSReaderEdit.Items.Add(lstAReaderEdit.Items[i]);
                        ListItem li = lstAReaderEdit.Items[i];
                        lstAReaderEdit.Items.Remove(li);
                    }
                }
                lstSReaderEdit.SelectedValue = null;
            }
        }
        protected void cmdReaderLeftEdit_Click(object sender, EventArgs e)
        {
            if (lstSReaderEdit.SelectedItem != null)
            {
                for (int i = lstSReaderEdit.Items.Count - 1; i >= 0; i--)
                {
                    if (lstSReaderEdit.Items[i].Selected == true)
                    {
                        lstAReaderEdit.Items.Add(lstSReaderEdit.Items[i]);
                        ListItem li = lstSReaderEdit.Items[i];
                        lstSReaderEdit.Items.Remove(li);
                    }
                }
                lstAReaderEdit.SelectedValue = null;
            }
        }
        private void GetEAL_Config()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string strQuery = "";


                SqlCommand cmd = new SqlCommand();

                strQuery += " SELECT  CONTROLLER_ID ,RD_ZN_ID";
                strQuery += " FROM dbo.ACS_ACCESSLEVEL_RELATION ";
                strQuery += " WHERE AL_ID='" + txtalidEdit.Text + "'";

                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ViewState["AL_CONTROLLER"] = dt;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            {

            }
        }
        private DataTable GetR_C_Table_Defination()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AL_CONTROLLER_RD", typeof(System.String));
            return dt;
        }
        private void Update_EAL_Config()
        {
            try
            {
                ArrayList arrReaders = new ArrayList();
                DataTable dtOriginalReader = (DataTable)ViewState["AL_CONTROLLER"];
                DataTable dtNewReader = GetR_C_Table_Defination();

                DataRow dr = null;
                string strAffectedControllers = "";
                for (int j = 0; j < lstSReaderEdit.Items.Count; j++)
                {
                    dr = dtNewReader.NewRow();
                    dr["AL_CONTROLLER_RD"] = lstSReaderEdit.Items[j].Value.ToString();
                    dtNewReader.Rows.Add(dr);
                }

                string strCheckReaderFilter = "";


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                string strUpdate_EAL_CONFIG = "";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;


                for (int iR = 0; iR < dtOriginalReader.Rows.Count; iR++)
                {
                    strCheckReaderFilter = "AL_CONTROLLER_RD='" + dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString() + "-" + dtOriginalReader.Rows[iR]["RD_ZN_ID"].ToString() + "'";
                    DataRow[] drCheckReader = dtNewReader.Select(strCheckReaderFilter);

                    if (drCheckReader.Length < 1)
                    {
                        strAffectedControllers = strAffectedControllers + (strAffectedControllers.Length > 0 ? "," : "") + dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString();


                        strUpdate_EAL_CONFIG = "";

                        
                        //VAIBHAV AND ADIL
                        strUpdate_EAL_CONFIG += " IF EXISTS( ";
                        strUpdate_EAL_CONFIG += "         	SELECT * ";
                        strUpdate_EAL_CONFIG += "         	FROM dbo.EAL_CONFIG ";
                        strUpdate_EAL_CONFIG += "              WHERE AL_ID IN(" + txtalidEdit.Text + ") AND CONTROLLER_ID=" + dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString();
                        strUpdate_EAL_CONFIG += "          )";
                        strUpdate_EAL_CONFIG += "          BEGIN";
                        strUpdate_EAL_CONFIG += "             UPDATE EAL_CONFIG";
                        strUpdate_EAL_CONFIG += "             SET FLAG='2',ISDELETED=1";
                        strUpdate_EAL_CONFIG += "             WHERE AL_ID=" + txtalidEdit.Text + " AND CONTROLLER_ID=" + dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString();
                        strUpdate_EAL_CONFIG += "          END";
                        strUpdate_EAL_CONFIG += " ELSE";
                        strUpdate_EAL_CONFIG += "          BEGIN";
                        strUpdate_EAL_CONFIG += "             UPDATE EAL_CONFIG";
                        strUpdate_EAL_CONFIG += "             SET FLAG='3'";
                        strUpdate_EAL_CONFIG += "             WHERE AL_ID=" + txtalidEdit.Text + " AND CONTROLLER_ID=" + dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString();
                        strUpdate_EAL_CONFIG += "          END";

                        //END

                        cmd.CommandText = strUpdate_EAL_CONFIG;
                        cmd.ExecuteNonQuery();


                        //vaibahv  

                        //SqlCommand cmd1 = new SqlCommand("sp_insernewreader", conn);
                        //cmd1.CommandType = CommandType.StoredProcedure;
                        //cmd1.Parameters.AddWithValue("@controller_id", dtOriginalReader.Rows[iR]["CONTROLLER_ID"].ToString());
                        //cmd1.Parameters.AddWithValue("@al_id", txtalidEdit.Text);

                        //cmd1.ExecuteNonQuery();

                    }
                }



                ArrayList arrReader = new ArrayList();
                for (int i = 0; i < dtNewReader.Rows.Count; i++)
                {
                    string[] str = dtNewReader.Rows[i][0].ToString().Split('-');
                    arrReader.Add(str[0]);
                }
                for (int i = 0; i < dtOriginalReader.Rows.Count; i++)
                {
                    for (int j = 0; j < dtOriginalReader.Rows.Count; j++)
                    {
                        if (arrReader.Contains(dtOriginalReader.Rows[j][0].ToString()))
                        {
                            arrReader.Remove(dtOriginalReader.Rows[j][0].ToString());
                        }
                    }
                }


                if (arrReader.Count > 0)
                {
                    for (int i = 0; i <= arrReader.Count - 1; i++)
                    {
                        SqlCommand cmd1 = new SqlCommand("Proc_AL_EDIT", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@alid", txtalidEdit.Text);
                        cmd1.Parameters.AddWithValue("@ctlr", arrReader[i]);
                        cmd1.ExecuteNonQuery();
                    }
                }





                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //  return null;
            }
        }
        private void UpdateEAL_Config()
        {

            try
            {
                // ArrayList arrCo
            }
            catch (Exception ex)
            {

            }
        }
        public void fillReadersEdit()
        {
            lstAReaderEdit.Items.Clear();
            lstSReaderEdit.Items.Clear();
            try
            {

                if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                {
                   // string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID,replace(convert(char(30),AR.READER_DESCRIPTION)+ AC.CTLR_DESCRIPTION,' ',' ' )  as DESCRIPTION " +
                   //"from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                   //"and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                   //"select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                   //"from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                   //")";

                //oldcode:
                    string _strsql = "select (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) as ID, replace(convert(char(17),ltrim(SUBSTRING(AR.READER_DESCRIPTION,1,30)))+ SUBSTRING(AC.CTLR_DESCRIPTION,1,100),' ',' ' ) as DESCRIPTION " +
                "from ACS_READER AR,ACS_CONTROLLER AC where AR.CTLR_ID=AC.CTLR_ID " +
                "and (Cast(AR.CTLR_ID as varchar) +'-' +  cast(AR.READER_ID as varchar)) in( " +
                "select (Cast(APR.AP_CONTROLLER_ID as varchar) +'-' +  cast(APR.READER_ID as varchar)) as ID " +
                "from ACS_ACCESSPOINT_RELATION APR where APR_ISDELETED='0'" +
                ")";

                    DataTable _dt = new DataTable();
                    _dt = getDataTable(_strsql, conn);
                    if (_dt != null)
                    {
                        for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            lstAReaderEdit.Items.Add(new ListItem(_dt.Rows[i][1].ToString(), _dt.Rows[i][0].ToString()));
                        }
                    }
                }
                else
                {

                    DataSet ds = fillReaderAndZone();

                    if (ds.Tables[0] != null)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            lstAReaderEdit.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString()));
                        }
                    }

                }

            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void btnAccessLevelEdit_Click(object sender, EventArgs e)
        {
            Update_EAL_Config();
            try
            {
                //if (Page.IsValid && validationEdit())
                if (validationEdit())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlTransaction _trans = conn.BeginTransaction();
                    string _strInsertQuery = "";
                    Boolean _result;
                    try
                    {

                        _strInsertQuery = "";
                        _strInsertQuery = "Update ACS_ACCESSLEVEL set AL_DESCRIPTION='" + txtdescriptionEdit.Text.Trim() + "'," +
                                          " AL_TIMEZONE_ID='" + cmbTimeZoneEdit.SelectedValue + "' where AL_ID='" + txtalidEdit.Text + "'";
                        _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, conn, _trans);
                        string[] _ReaderController;

                        if (_result == true)
                        {
                            //_result = RunExecuteNonQueryWithTransaction("delete from ACS_ACCESSLEVEL_RELATION where AL_ID='" + Session["AL_ID"].ToString() + "'", conn, _trans);

                            _result = RunExecuteNonQueryWithTransaction("delete from ACS_ACCESSLEVEL_RELATION where AL_ID='" + txtalidEdit.Text + "'", conn, _trans);


                            if (_result == true)
                            {
                                if (RBLZoneEdit.SelectedValue == "Z")
                                {

                                    
                                    //added by vaibhav  on 2-10-2015
                                    for (int i = 0; lstSReaderEdit.Items.Count > i; i++)
                                    {
                                        string[] zoneid = lstSReaderEdit.Items[i].Value.ToString().Split('-');

                                        string strsql = "SELECT * FROM ZONE_READER_REL WHERE ZONE_ID ='" + zoneid[1].ToString() + "'";
                                        SqlCommand cmd = new SqlCommand(strsql, conn,_trans);
                                        SqlDataAdapter dap = new SqlDataAdapter(cmd);
                                        DataTable dt = new DataTable();
                                        dap.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dt.Rows.Count; j++)
                                            {
                                                string zone_Id, readerId, ControllerId;
                                                zone_Id = dt.Rows[j]["ZONE_ID"].ToString();
                                                readerId = dt.Rows[j]["READER_ID"].ToString();
                                                ControllerId = dt.Rows[j]["CONTROLLER_ID"].ToString();
                                                //added by vaibhav  2-9-2015
                                                StringBuilder accLevelAraay = new StringBuilder();
                                                SqlCommand cmd1 = new SqlCommand("select AccesLevelArray from ACS_ACCESSLEVEL_RELATION WHERE CONTROLLER_ID=" + ControllerId + " and AL_ID=" + txtalidEdit.Text + " ", conn,_trans);
                                                SqlDataAdapter dap1 = new SqlDataAdapter(cmd1);
                                                DataTable dt1 = new DataTable();
                                                dap1.Fill(dt1);
                                                if (dt1.Rows.Count > 0)
                                                {
                                                    accLevelAraay.Append(dt1.Rows[0]["AccesLevelArray"].ToString());
                                                }
                                                else
                                                {
                                                    accLevelAraay.Append("00000000");
                                                }
                                                if (accLevelAraay.Length == 0)
                                                {
                                                    accLevelAraay.Append("00000000");
                                                }

                                                int arrayPos = Convert.ToInt32(readerId);

                                                accLevelAraay.Remove(accLevelAraay.Length - arrayPos, 1);
                                                accLevelAraay.Insert(accLevelAraay.Length - arrayPos + 1, '1');
                                                // vaibhav end

                                                _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,ZoneId,RD_ZN_ID,CONTROLLER_ID,AL_ENTITY_TYPE,ALR_ISDELETED,ALR_DELETEDDATE,Action_flag,AccesLevelArray) " +
                                                " values('" + txtalidEdit.Text.Trim() + "','" + zone_Id + "','" + readerId + "','" + ControllerId + "','Z','False',null,0,'" + accLevelAraay.ToString() + "')";
                                                _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, conn,_trans);

                                                _strInsertQuery = " UPDATE ACS_ACCESSLEVEL_RELATION set AccesLevelArray='" + accLevelAraay.ToString() + "' " +
                                                                  " WHERE AL_ID='" + txtalidEdit.Text.Trim() + "' AND ZoneId='" + zone_Id + "' AND CONTROLLER_ID=" + ControllerId + " ";
                                                _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, conn,_trans);


                                            }

                                        }


                                    }

                                }
                                else
                                {
                                    for (int i = 0; lstSReaderEdit.Items.Count > i; i++)
                                    {
                                        StringBuilder accLevelAraay = new StringBuilder();
                                        _ReaderController = lstSReaderEdit.Items[i].Value.ToString().Split('-');
                                        //added by vaibhav  2-9-2015
                                        SqlCommand cmd = new SqlCommand("select AccesLevelArray from ACS_ACCESSLEVEL_RELATION WHERE CONTROLLER_ID=" + Convert.ToInt32(_ReaderController[0]) + " and AL_ID=" + txtalidEdit.Text + " ", conn, _trans);
                                        SqlDataAdapter dap = new SqlDataAdapter(cmd);
                                        DataTable dt = new DataTable();
                                        dap.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            accLevelAraay.Append(dt.Rows[0]["AccesLevelArray"].ToString());
                                        }
                                        else
                                        {
                                            accLevelAraay.Append("00000000");
                                        }
                                        if (accLevelAraay.Length == 0)
                                        {
                                            accLevelAraay.Append("00000000");
                                        }
                                        int arrayPos = Convert.ToInt32(_ReaderController[1]);
                                        accLevelAraay.Remove(accLevelAraay.Length - arrayPos, 1);
                                        accLevelAraay.Insert(accLevelAraay.Length - arrayPos + 1, '1');
                                        // vaibhav end

                                        _strInsertQuery = "Insert into ACS_ACCESSLEVEL_RELATION(AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,CONTROLLER_ID,ALR_ISDELETED,ALR_DELETEDDATE,Action_flag, AccesLevelArray) " +
                                                            " values('" + txtalidEdit.Text.Trim() + "','" + _ReaderController[1].ToString() + "','R'," + _ReaderController[0].ToString() + ",'False',null,0,'" + accLevelAraay.ToString() + "')";
                                        _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, conn, _trans);

                                        _strInsertQuery = " UPDATE ACS_ACCESSLEVEL_RELATION set AccesLevelArray='" + accLevelAraay.ToString() + "' " +
                                                          " where AL_ID='" + txtalidEdit.Text.Trim() + "' AND CONTROLLER_ID=" + _ReaderController[0].ToString() + " ";
                                        _result = RunExecuteNonQueryWithTransaction(_strInsertQuery, conn, _trans);

                                    }
                                }
                            }

                            _trans.Commit();
                            _trans.Dispose();

                            /***UPDATE EAL_CONFIG*******/


                            String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                            Boolean _bl = RunExecuteNonQuery(_updateStr, conn);
                            //aded by vaibhav
                            bindgvAccessLevel();
                            lblMessages.Text = "Record Updated Successfully";
                            lblMessages.Visible = true;

                            mpeEditAccessLevel.Hide();

                            //this.messageDiv.InnerHtml = "Record Saved Successfully";
                            lblZoneEdit.Visible = false;
                            cmbZoneEdit.Visible = false;
                            RBLZoneEdit.SelectedValue = null;

                        }

                    }
                    catch (Exception ex)
                    {
                        _trans.Rollback();
                    }

                }
                else
                {
                    string someScript2 = "";
                    someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",2000);</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            mpeEditAccessLevel.Hide();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StringCollection idCollection = new StringCollection();
                int index;
                for (int i = 0; i < gvAccessLevel.Rows.Count; i++)
                {
                    CheckBox delrows = (CheckBox)gvAccessLevel.Rows[i].FindControl("DeleteRows");
                    if (delrows.Checked == true)
                    {
                        index = Convert.ToInt32((gvAccessLevel.DataKeys[i].Value.ToString()));
                        idCollection.Add(index.ToString());
                    }
                }
                if (idCollection.Count != 0)
                {
                    Boolean _result = DeleteRecords(idCollection);
                    bindgvAccessLevel();
                }
                else
                {
                    lblMessages.Text = "Please select record to delete";
                    lblMessages.Visible = true;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        public Boolean DeleteRecords(StringCollection _idCollection)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlTransaction _trans;
                String _strValue = "", _strAL = "";
                Boolean _result = false, _isdeleted = false;
                for (int i = 0; _idCollection.Count > i; i++)
                {
                    _strValue = getValue("select count(*) from EAL_CONFIG where AL_ID='" + _idCollection[i].ToString().Trim() + "' and isdeleted='0'", conn);


                    if (Convert.ToInt16(_strValue) > 0)
                    {
                        if (_strAL == "")
                        { _strAL = _strAL + _idCollection[i].ToString().Trim(); }
                        else
                        { _strAL = _strAL + "," + _idCollection[i].ToString().Trim(); }
                    }
                    else
                    {
                        string _strDeleteQuery = "update ACS_ACCESSLEVEL_RELATION set action_flag=2,AccesLevelArray='00000000' where AL_ID='" + _idCollection[i].ToString().Trim() + "'";

                        string _strDeleteQuery1 = "update ACS_ACCESSLEVEL set AL_DESCRIPTION='',AL_TIMEZONE_ID='',AL_ISDELETED='True',AL_DELETEDDATE=GETDATE() where AL_ID='" + _idCollection[i].ToString().Trim() + "'";


                        SqlCommand cmd = new SqlCommand(_strDeleteQuery, conn); //changes on 15/Sept/2104 by shrinith
                        SqlCommand cmd1 = new SqlCommand(_strDeleteQuery1, conn); //changes on 15/Sept/2104 by shrinith
                        cmd.ExecuteNonQuery();   //changes on 15/Sept/2104 by shrinith
                        cmd1.ExecuteNonQuery(); //changes on 15/Sept/2104 by shrinith
                        
                        _isdeleted = true;
                    }
                }

                if (_strAL == "")
                {
                    String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                    Boolean _bl = RunExecuteNonQuery(_updateStr, conn);
                    lblMessages.Text = "Record(s) Deleted Successfully";
                    lblMessages.Visible = true;
                }
                else
                {
                    if (_isdeleted == true)
                    {
                        String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='ALC'";
                        Boolean _bl = RunExecuteNonQuery(_updateStr, conn);
                        lblMessages.Text = "Record(s) Deleted Successfully Except Access Level id(s):" + _strAL + ".As these id have referenced in user access permission.";
                        lblMessages.Visible = true;
                    }
                    else
                    {
                        lblMessages.Text = "Cannot Delete record for Access Level id(s):" + _strAL + ".As these id have referenced in user access permission.";
                        lblMessages.Visible = true;
                    }
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
               
                return true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
                return false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "select AL.AL_ID,AL.AL_DESCRIPTION,TZ_DESCRIPTION,case when con.al_id is null then 'A' else 'P' end 'IsExists' " +
                                      "  from ACS_ACCESSLEVEL AL with(nolock) LEFT OUTER JOIN  ACS_TIMEZONE TZ with(nolock) " +
                                      //" ON AL.AL_TIMEZONE_ID=TZ.TZ_ID left outer join eal_config con with(nolock) on con.al_id=al.al_id  where AL.AL_ISDELETED='False' " +
                                      " ON AL.AL_TIMEZONE_ID=TZ.TZ_CODE left outer join eal_config con with(nolock) on con.al_id=al.al_id  where AL.AL_ISDELETED='False' " +
                                      " group by AL.AL_ID,AL.AL_DESCRIPTION,TZ_DESCRIPTION,con.al_id order by AL_ID,AL_DESCRIPTION";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvAccessLevel.DataSource = dt;
                gvAccessLevel.DataBind();

                if (txtID.Text.ToString() == "" && txtDescription.Text.ToString() == "")
                {
                    gvAccessLevel.DataSource = dt;
                    gvAccessLevel.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"AL_ID~" +txtID.Text.Trim(), "I" },
				{"AL_DESCRIPTION~" +txtDescription.Text.Trim(), "I" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvAccessLevel.DataSource = _tempDT;
                    gvAccessLevel.DataBind();
                }

                DropDownList ddl = (DropDownList)gvAccessLevel.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvAccessLevel.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvAccessLevel.PageIndex + 1).ToString();
                Label lblcount = (Label)gvAccessLevel.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvAccessLevel.DataSource).Rows.Count.ToString() + " Records.";
                if (gvAccessLevel.PageCount == 0)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAccessLevel.PageIndex + 1 == gvAccessLevel.PageCount)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvAccessLevel.PageIndex == 0)
                {
                    ((Button)gvAccessLevel.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvAccessLevel.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvAccessLevel.PageSize * gvAccessLevel.PageIndex) + 1) + " to " + (gvAccessLevel.PageSize * (gvAccessLevel.PageIndex + 1));

                ((Label)gvAccessLevel.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvAccessLevel.PageSize * gvAccessLevel.PageIndex) + 1) + " to " + (((gvAccessLevel.PageSize * (gvAccessLevel.PageIndex + 1)) - 10) + gvAccessLevel.Rows.Count);

                gvAccessLevel.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AccessLevel");
            }
        }
        //Addded by Pooja Yadav
        protected void gvAccessLevel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIsAllowed = (Label)e.Row.FindControl("lblIsAllowed");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                if (string.Equals(lblIsAllowed.Text, "A", StringComparison.CurrentCultureIgnoreCase))
                    ChkDelete.Enabled = true;
                else
                    ChkDelete.Enabled = false;

            }
        }


    }
}