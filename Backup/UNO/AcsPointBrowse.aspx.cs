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
    public partial class AcsPointBrowse : System.Web.UI.Page
    {
        public SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            this.messageDiv.InnerHtml = ""; 
            if (Page.IsPostBack == false)
            {
               // Grid.PageSize = 5;
                bindDataGrid();               
            }
        }    
        public void bindDataGrid()
        {
            try
            {
                SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
                string strsql = "select AP_ID,AP_DESCRIPTION,PRM.VALUE as AP_TYPE from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM " +
                                "where ACS.AP_ISDELETED='False' and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' order by AP_ID,AP_DESCRIPTION";
                SqlDataAdapter da = new SqlDataAdapter(strsql, _sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    Grid.DataSource = dt;
                    Grid.DataBind();
                }
                else
                {
                    btnDelete.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }       
        protected void Grid_DataBound(object sender, EventArgs e)
        {
            if ( Grid.Rows.Count > 0)
            {
                pager.Visible = true;
                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = Grid.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }
                Label lblCount = Grid.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (Grid.PageIndex * Grid.PageSize) + Grid.PageSize;
                //string strsql = "select count(*) from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM,ACS_CONTROLLER CTRL " +
                //                "where ACS.AP_ISDELETED='False' and ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' and CTRL.CTLR_ID=ACS.AP_CONTROLLER_ID";
                string strsql = "select count(*) from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM " +
                               "where ACS.AP_ISDELETED='False' and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE'";
       
                int totaccessPointCount = Convert.ToInt32(getValue(strsql, _sqlConnection));

                totRecords = totRecords > totaccessPointCount ? totaccessPointCount : totRecords;
                lblPageCount.Text = ((Grid.PageIndex * Grid.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totaccessPointCount.ToString();
                Grid.BottomPagerRow.Visible = true;                

            }
            else
            {
                pager.Visible = false;
            }
        }
        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //SaveCheckedValues();
            Grid.PageIndex = e.NewPageIndex;
            bindDataGrid();            
            //PopulateCheckedValues();
        }
        private void PopulateCheckedValues()
        {
            ArrayList _apdetails = (ArrayList)Session["CHECKED_ITEMS"];
            if (_apdetails != null && _apdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in Grid.Rows)
                {
                    //String index = (String)Grid.DataKeys[gvrow.RowIndex].Value;
                    //Int index = (Int64)Grid.DataKeys[gvrow.RowIndex].Value;
                    int index = Convert.ToInt32(Grid.DataKeys[gvrow.RowIndex].Value.ToString());
                    if (_apdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("DeleteRows");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList _apdetails = new ArrayList();
            //String index = "";
            int index;
            foreach (GridViewRow gvrow in Grid.Rows)
            {
                //index = (String)Grid.DataKeys[gvrow.RowIndex].Value;
                //index = (Int64)Grid.DataKeys[gvrow.RowIndex].Value;
                index = Convert.ToInt32(Grid.DataKeys[gvrow.RowIndex].Value.ToString());
                bool result = ((CheckBox)gvrow.FindControl("DeleteRows")).Checked;

                // Check in the Session
                if (Session["CHECKED_ITEMS"] != null)
                    _apdetails = (ArrayList)Session["CHECKED_ITEMS"];
                if (result)
                {
                    if (!_apdetails.Contains(index))
                        _apdetails.Add(index);
                }
                else
                    _apdetails.Remove(index);
            }
            if (_apdetails != null && _apdetails.Count > 0)
                Session["CHECKED_ITEMS"] = _apdetails;
        }
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            Grid.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            bindDataGrid();
        }
        //protected void DeleteRows_CheckedChanged(object sender, EventArgs e)
        //{
            
            
        //}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (getId() > 128)
            {
                this.messageDiv.InnerHtml = "System can contain atmost 128 access point.";
            }
            else
            {
                Response.Redirect("~/AcsPointAdd.aspx");
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
                      
            // Response.Redirect("~/AcsPointAdd.aspx"); 
            StringCollection idCollection = new StringCollection();
            //string strID = string.Empty;
            ////Loop through GridView rows to find checked rows 
            //for (int i = 0; i < Grid.Rows.Count; i++)
            //{
            //    CheckBox chkDelete = (CheckBox)
            //       Grid.Rows[i].Cells[1].FindControl("DeleteRows");
            //    if (chkDelete != null)
            //    {
            //        if (chkDelete.Checked)
            //        {
            //            strID = Grid.Rows[i].Cells[2].Text;
            //            idCollection.Add(strID);
            //        }
            //    }
            //}

            //SaveCheckedValues();
            //ArrayList _apdetails = (ArrayList)Session["CHECKED_ITEMS"];
            //if (_apdetails != null && _apdetails.Count > 0)
            //{                
            //        //foreach (String _str in _apdetails)
            //        foreach (int _str in _apdetails)
            //        {
            //            idCollection.Add(_str.ToString());
            //        }                
            //}
            int index;
            bool Check = false;
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                CheckBox delrows = (CheckBox)Grid.Rows[i].FindControl("DeleteRows");
                if (delrows.Checked == true)
                {
                    index = Convert.ToInt32(Grid.DataKeys[i].Value.ToString());
                    idCollection.Add(index.ToString());
                }
            }

            if (idCollection.Count != 0)
            {
                Boolean _result = DeleteRecords(idCollection);
                if (_result == true)
                    //this.messageDiv.InnerHtml = "Record Deleted Successfully";
                bindDataGrid();
                //Session["CHECKED_ITEMS"] = null;
                //PopulateCheckedValues();
            }
                
            else
                this.messageDiv.InnerHtml = "Please select record to delete";

            string someScript2 = "";
            someScript2 = "<script language='javascript'>setTimeout(\"clearFunction()\",5000);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
        }
        public Int16 getId()
        {
            Int16 _apID;
            _apID = Convert.ToInt16(getValue("select ISNULL(min(AP_ID),0) from ACS_ACCESSPOINT where AP_ISDELETED='1'", _sqlConnection));
            if (_apID == 0)
            {
                _apID = Convert.ToInt16(getValue("select ISNULL(max(AP_ID)+1,1) from ACS_ACCESSPOINT where AP_ISDELETED='0'", _sqlConnection));               
            }
            return _apID;
        }
        public Boolean DeleteRecords(StringCollection _idCollection)
          {
              try
              {
                  String _strValueAL = "", _strValueZone = "", _strAP = "";
                  if (_sqlConnection.State == ConnectionState.Closed)
                                  _sqlConnection.Open();

                  SqlTransaction _trans ;
                  Boolean _result = false, _isdeleted = false;
                  //= _sqlConnection.BeginTransaction();      
               
                      for (int i = 0; _idCollection.Count > i; i++)
                      {

                         // _strValueAL = getValue("select COUNT(*) from ACS_ACCESSLEVEL_RELATION where RD_ZN_ID in (select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where AP_ID='" + _idCollection[i].ToString().Trim() + "' and  APR_ISDELETED='0') and ALR_ISDELETED='0' ", _sqlConnection);
                          _strValueAL = getValue("select COUNT(*) from ACS_ACCESSLEVEL_RELATION where cast (RD_ZN_ID as varchar) + '-' + cast(CONTROLLER_ID as varchar) in (select (cast(READER_ID as varchar) + '-' + cast(AP_CONTROLLER_ID as varchar)) from ACS_ACCESSPOINT_RELATION where AP_ID='" + _idCollection[i].ToString().Trim() + "' and  APR_ISDELETED='0') and ALR_ISDELETED='0'", _sqlConnection);
                          //_strValueZone = getValue("select COUNT(*) from ZONE_READER_REL where READER_ID in (select distinct(READER_ID) from ACS_ACCESSPOINT_RELATION where AP_ID='" + _idCollection[i].ToString().Trim() + "' and  APR_ISDELETED='0') and ZONER_ISDELETED='0'", _sqlConnection);
                          _strValueZone = getValue("select COUNT(*) from ZONE_READER_REL where cast (READER_ID as varchar) + '-' + cast(CONTROLLER_ID as varchar) in (select (cast(READER_ID as varchar) + '-' + cast(AP_CONTROLLER_ID as varchar)) from ACS_ACCESSPOINT_RELATION where AP_ID='" + _idCollection[i].ToString().Trim() + "' and  APR_ISDELETED='0') and ZONER_ISDELETED='0'", _sqlConnection);

                          if (Convert.ToInt16(_strValueAL) > 0 || Convert.ToInt16(_strValueZone) > 0)
                          {
                              if (_strAP == "")
                              { _strAP = _strAP + _idCollection[i].ToString().Trim(); }
                              else
                              { _strAP = _strAP + "," + _idCollection[i].ToString().Trim(); }
                          }
                          else
                          {

                              try
                              {



                                  _trans = _sqlConnection.BeginTransaction();

                                  //string _strDeleteQuery = "update ACS_ACCESSPOINT_RELATION set  APR_ISDELETED='True', APR_DELETEDDATE=GETDATE() where AP_ID=" + _idCollection[i].ToString().Trim() + "";
                                  string _strDeleteQuery = "delete from ACS_ACCESSPOINT_RELATION where AP_ID=" + _idCollection[i].ToString().Trim() + "";
                                 _result = RunExecuteNonQueryWithTransaction(_strDeleteQuery, _sqlConnection, _trans);
                                  //if (_result == true)
                                  //{
                                      //_strDeleteQuery = "delete from ACS_ACCESSPOINT_RELATION where AP_ID='" + _idCollection[i].ToString().Trim() + "'";
                                      _strDeleteQuery = "update ACS_ACCESSPOINT set  AP_ISDELETED='True',AP_DESCRIPTION='',AP_TYPE='',AP_DELETEDDATE=GETDATE() where AP_ID=" + _idCollection[i].ToString().Trim() + "";
                                      _result = RunExecuteNonQueryWithTransaction(_strDeleteQuery, _sqlConnection, _trans);
                                  //}
                                  _trans.Commit();
                                  _isdeleted = true;
                              }
                              catch (Exception ex)
                              {
                                  //_trans.Rollback();
                              }
                          }
                      }

                      if (_strAP == "")
                      {
                          this.messageDiv.InnerHtml = "Record Deleted Successfully";
                          String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='APC'";
                          Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                         
                      }
                      else
                      {
                          if (_isdeleted == true)
                          {
                              this.messageDiv.InnerHtml = "Record Deleted Successfully Except AP id(s):" + _strAP + ".As these id have referenced in access level or in zone.";
                              String _updateStr = "update ENT_PARAMS set [VALUE]='1' where MODULE='ACS' and IDENTIFIER='CONFIGCHANGE' and CODE='APC'";
                              Boolean _bl = RunExecuteNonQuery(_updateStr, _sqlConnection);
                              
                          }
                          {
                              this.messageDiv.InnerHtml = "Cannot Delete record for AP id(s):" + _strAP + ".As these id have referenced in access level or in zone.";
                          }
                      }

                 // _trans.Commit();
                  return true;
              }
              catch (Exception ex)
              {

                  return false;
              }
          }
        public bool RunExecuteNonQuery(string _strQuery, SqlConnection _sqlconn)
          {
              try
              {
                  if (_sqlconn.State == ConnectionState.Closed)
                      _sqlconn.Open();
                  int _result = 0;
                  SqlCommand _sc = new SqlCommand();
                  _sc.Connection = _sqlconn;
                  _sc.CommandText = _strQuery;
                  _result = _sc.ExecuteNonQuery();
                  if (_result == 0)
                      return false;
                  else
                      return true;
              }
              catch (Exception ex)
              { return false; }
          }
        protected void Grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void Grid_PreRender(object sender, EventArgs e)        
        { }
        protected void Grid_SelectedIndexChanged(object sender, EventArgs e){
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
                return _ds.Tables[0].Rows[0][0].ToString().Trim();

            }
            catch (Exception ex)
            { return ""; }
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
        protected void Grid_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());

            string strsql1 = "";
            DataTable dt1;

            SqlDataAdapter da1;
            try
            {




                string strsql = "select AP_ID,AP_DESCRIPTION,PRM.VALUE as AP_TYPE from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM " +
                                "where ACS.AP_ISDELETED='False' and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' order by AP_ID,AP_DESCRIPTION";
                SqlDataAdapter da = new SqlDataAdapter(strsql, _sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
                {
                    cmdReset_Click(sender, e);
                }
                else
                {
                    String[,] values = { 
				{"AP_ID-" +txtCompanyId.Text.Trim(), "S" },
				{"AP_DESCRIPTION-" +txtCompanyName.Text.Trim(), "S" }			
				 };

                    //commented by and changes made on 15/Sept/2014 by shrinith start

                    //DataTable _tempDT = new DataTable();
                    //Search _sc = new Search();
                    //if (_tempDT != null)
                    //{ _tempDT.Rows.Clear(); }
                    //_tempDT = _sc.searchTable(values, dt);


                    
                    if (_sqlConnection.State == ConnectionState.Closed)
                    {
                        _sqlConnection.Open();
                    }
                    if (txtCompanyId.Text.ToString() != "" && txtCompanyName.Text.ToString() == "")
                    {
                        strsql1 = " select AP_ID,AP_DESCRIPTION,PRM.VALUE as AP_TYPE from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM where ACS.AP_ISDELETED='False' " +
                                 " and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' " +
                                 " AND AP_ID Like '" + txtCompanyId.Text + "%'";
                        da1 = new SqlDataAdapter(strsql1, _sqlConnection);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() != "")
                    {
                        strsql1 = " select AP_ID,AP_DESCRIPTION,PRM.VALUE as AP_TYPE from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM where ACS.AP_ISDELETED='False' " +
                                  " and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' " +
                                  " AND AP_DESCRIPTION Like '%" + txtCompanyName.Text + "%'";
                        da1 = new SqlDataAdapter(strsql1, _sqlConnection);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);
                    }
                    else
                    {
                        strsql1 = "  select AP_ID,AP_DESCRIPTION,PRM.VALUE as AP_TYPE from ACS_ACCESSPOINT ACS,ENT_PARAMS PRM where ACS.AP_ISDELETED='False' " +
                                   " and  ACS.AP_TYPE=PRM.CODE and PRM.MODULE='ACS' and PRM.IDENTIFIER='ACCESSPOINTTYPE' AND " +
                                  " AP_DESCRIPTION Like '%" + txtCompanyName.Text + "%'   AND AP_ID Like '" + txtCompanyId.Text + "%'";
                        da1 = new SqlDataAdapter(strsql1, _sqlConnection);
                        dt1 = new DataTable();
                        da1.SelectCommand.CommandTimeout = 0;
                        da1.Fill(dt1);

                    }
                    if (_sqlConnection.State == ConnectionState.Open)
                    {
                        _sqlConnection.Close();
                    }

                    //DataTable _tempDT = new DataTable();
                    //Search _sc = new Search();
                    //if (_tempDT != null)
                    //{ _tempDT.Rows.Clear(); }
                    //_tempDT = _sc.searchTable(values, dt1);
                    //gvController.DataSource = dt1;
                    //gvController.DataBind();


                    //gvZone.DataSource = dt1;
                    //gvZone.DataBind();



                    Grid.DataSource = dt1;
                    Grid.DataBind();


                    //commented by and changes made on 15/Sept/2014 by shrinith end
                }
               
            }
            catch (Exception ex)
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "AcsPointBrowse");
            }

        }
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Text = "";
            txtCompanyName.Text = "";
            bindDataGrid();

        }

    }
}