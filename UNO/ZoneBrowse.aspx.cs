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
using CMS.UNO.Sentinel.Handler;
using System.Text;

namespace UNO
{
    public partial class ZoneBrowse : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
      
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            getzoneId();
            txtzoneid.ReadOnly = true;
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                intializeControl();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvZone.ClientID + "');");
            }
        }

        public void getzoneId()
        {
            try
            {             
                txtzoneid.Text = clsZoneBrowseHandler.GetCount().ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void cmdALLALRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstAReader.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstSReader.Items.Add(_li);

            }
            lstAReader.Items.Clear();
            lstSReader.SelectedValue = null;
        }

        protected void cmdALLALLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = lstSReader.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                lstAReader.Items.Add(_li);
            }
            lstSReader.Items.Clear();
            lstAReader.SelectedValue = null;
        }

        protected void cmdModifyALLReaderRight_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = ModifyLstAREADER.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                ModifyLstAREADER.Items.Add(_li);

            }
            ModifyLstAREADER.Items.Clear();
            ModifyLstAREADER.SelectedValue = null;
        }

        protected void cmdALLModifyReaderLeft_Click(object sender, EventArgs e)
        {
            IEnumerator _ie = ModifyLstSREADER.Items.GetEnumerator();
            while (_ie.MoveNext())
            {
                ListItem _li = (ListItem)_ie.Current;
                ModifyLstAREADER.Items.Add(_li);
            }
            ModifyLstSREADER.Items.Clear();
            ModifyLstAREADER.SelectedValue = null;
        }

        public void intializeControl()
        {

            txtdescription.Text = "";
            lstAReader.Items.Clear();
            lstSReader.Items.Clear();
            try
            {
                DataTable dt = clsZoneBrowseHandler.GetAllDetails("Initialize", "1");
                if (dt != null)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        lstAReader.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        void bindDataGrid()
        {
            try
            {
                // DataTable dt = clsZoneBrowseHandler.GetAllDetails("All", Session["levelId"].ToString());
                //  if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                // {
                //  }
                //    else
                // {
                //SqlCommand cmd = new SqlCommand("fillzone", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //string levelId = Session["uid"].ToString();
                //cmd.Parameters.AddWithValue("@levelid", levelId);
                //SqlDataAdapter dap = new SqlDataAdapter(cmd);
                //dap.Fill(dt);
                //}

                string strsql = "select [ZONE_ID],[ZONE_DESCRIPTION] from ZONE where ZONE_ISDELETED='false' order by ZONE_ID,ZONE_DESCRIPTION";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvZone.DataSource = dt;
                gvZone.DataBind();
                DropDownList ddl = (DropDownList)gvZone.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvZone.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvZone.PageIndex + 1).ToString();
                Label lblcount = (Label)gvZone.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvZone.DataSource).Rows.Count.ToString() + " Records.";
                if (gvZone.PageCount == 0)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvZone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvZone.PageIndex + 1 == gvZone.PageCount)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvZone.PageIndex == 0)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvZone.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvZone.PageSize * gvZone.PageIndex) + 1) + " to " + (((gvZone.PageSize * (gvZone.PageIndex + 1)) - 10) + gvZone.Rows.Count);

                gvZone.BottomPagerRow.Visible = true;


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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvZone.PageIndex = e.NewPageIndex;
                bindDataGrid();

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvZone.PageIndex = Convert.ToInt32(((DropDownList)gvZone.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvZone.PageIndex = gvZone.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvZone.PageIndex = gvZone.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }    

        public void ModifyZoneReader(string zoneid)
        {

            try
            {
                DataSet ds = clsZoneBrowseHandler.GetAllDetails("fillOnEdit", Session["levelId"].ToString(), zoneid);
                txtModifyZoneID.Text = ds.Tables[0].Rows[0]["zone_id"].ToString();
                txtModifyZoneDesc.Text = ds.Tables[0].Rows[0]["zone_description"].ToString();
                //if (Session["uid"].ToString() == "ADMIN" || Session["uid"].ToString() == "admin")
                //{

                    if (ds.Tables[1] != null)
                    {
                        ModifyLstAREADER.Items.Clear();
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            ModifyLstAREADER.Items.Add(new ListItem(ds.Tables[1].Rows[i][1].ToString(), ds.Tables[1].Rows[i][0].ToString()));
                        }
                    }

                    if (ds.Tables[2] != null)
                    {
                        ModifyLstSREADER.Items.Clear();
                        for (int i = 0; i <= ds.Tables[2].Rows.Count - 1; i++)
                        {
                            ModifyLstSREADER.Items.Add(new ListItem(ds.Tables[2].Rows[i][1].ToString(), ds.Tables[2].Rows[i][0].ToString()));
                        }
                    }
             //   }
                //else
                //{

                //    ModifyLstSREADER.Items.Clear();

                //    if (ds.Tables[1] != null)
                //    {
                //        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                //        {
                //            ModifyLstSREADER.Items.Add(new ListItem(ds.Tables[1].Rows[i][1].ToString(), ds.Tables[1].Rows[i][0].ToString()));
                //        }
                //    }


                //}

            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }


        }

        protected void gvZone_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string Rowid = e.CommandArgument.ToString();
                ModifyZoneReader(Rowid);
                txtModifyZoneID.ReadOnly = true;
                mpeModifyZone.Show();
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtdescription.Text = "";
            //lstSReader.Items.Clear();
            mpeAddZone.Show();
        }

        protected void cmdReaderRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAReader.SelectedItem != null)
                {
                    //lstSReader.Items.Add(lstAReader.SelectedItem);
                    //lstAReader.Items.Remove(lstAReader.SelectedItem);               
                    for (int i = lstAReader.Items.Count - 1; i >= 0; i--)
                    {
                        if (lstAReader.Items[i].Selected == true)
                        {
                            lstSReader.Items.Add(lstAReader.Items[i]);
                            ListItem li = lstAReader.Items[i];
                            lstAReader.Items.Remove(li);
                        }
                    }
                    lstSReader.SelectedValue = null;
                }
            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void cmdReaderLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSReader.SelectedItem != null)
                {
                    //lstAReader.Items.Add(lstSReader.SelectedItem);
                    //lstSReader.Items.Remove(lstSReader.SelectedItem);
                    for (int i = lstSReader.Items.Count - 1; i >= 0; i--)
                    {
                        if (lstSReader.Items[i].Selected == true)
                        {
                            lstAReader.Items.Add(lstSReader.Items[i]);
                            ListItem li = lstSReader.Items[i];
                            lstSReader.Items.Remove(li);
                        }
                    }
                    lstAReader.SelectedValue = null;
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            string[] ReaderController;
            try
            {


                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ZONE_READER_REL>");
                for (int i = 0; i < lstSReader.Items.Count; i++)
                {
                    ReaderController = lstSReader.Items[i].Value.ToString().Split('-');
                    strXML.Append("<READER_REL>");
                    strXML.Append("<controller_id>" + ReaderController[0].ToString() + "</controller_id>");
                    strXML.Append("<reader_id>" + ReaderController[1].ToString() + "</reader_id>");
                    strXML.Append("</READER_REL>");
                }
                strXML.Append("</ZONE_READER_REL>");
                clsZoneBrowse objData = new clsZoneBrowse();
                objData.ZONE_DESCRIPTION = txtdescription.Text.Trim();
                objData.CreatedBy = Session["uid"].ToString();
                clsZoneBrowseHandler.InsertZoneBrowseDetails(objData, "Insert", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Length >= 1)
                {
                }
                else
                {

                    mpeAddZone.Hide();
                    bindDataGrid();
                    lblMessages.Visible = true;
                    lblMessages.Text = strSuccMsg.Trim();
                }

            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void btnModifySaveZone_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReaderController;
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ZONE_READER_REL>");
                for (int i = 0; i < ModifyLstSREADER.Items.Count; i++)
                {
                    ReaderController = ModifyLstSREADER.Items[i].Value.ToString().Split('-');
                    strXML.Append("<READER_REL>");
                    strXML.Append("<controller_id>" + ReaderController[0].ToString() + "</controller_id>");
                    strXML.Append("<reader_id>" + ReaderController[1].ToString() + "</reader_id>");
                    strXML.Append("</READER_REL>");
                
                }
                strXML.Append("</ZONE_READER_REL>");
                clsZoneBrowse objData = new clsZoneBrowse();
                objData.Zone_ID = txtModifyZoneID.Text.Trim();
                objData.ZONE_DESCRIPTION = txtModifyZoneDesc.Text.Trim();
                objData.CreatedBy = Session["uid"].ToString();
                clsZoneBrowseHandler.UpdateZoneBrowseDetails(objData, strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());


                if (strErrMsg.Length >= 1)
                {
                }
                else
                {
                    mpeModifyZone.Hide();

                    bindDataGrid();
                    lblMessages.Visible = true;
                    lblMessages.Text = strSuccMsg.Trim();
                }


            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void cmdModifyReaderRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModifyLstAREADER.SelectedItem != null)
                {
                    //lstSReader.Items.Add(lstAReader.SelectedItem);
                    //lstAReader.Items.Remove(lstAReader.SelectedItem);               
                    for (int i = ModifyLstAREADER.Items.Count - 1; i >= 0; i--)
                    {
                        if (ModifyLstAREADER.Items[i].Selected == true)
                        {
                            ModifyLstSREADER.Items.Add(ModifyLstAREADER.Items[i]);
                            ListItem li = ModifyLstAREADER.Items[i];
                            ModifyLstAREADER.Items.Remove(li);
                        }
                    }
                    ModifyLstAREADER.SelectedValue = null;
                }
            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void cmdModifyReaderLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModifyLstSREADER.SelectedItem != null)
                {
                    //lstAReader.Items.Add(lstSReader.SelectedItem);
                    //lstSReader.Items.Remove(lstSReader.SelectedItem);
                    for (int i = ModifyLstSREADER.Items.Count - 1; i >= 0; i--)
                    {
                        if (ModifyLstSREADER.Items[i].Selected == true)
                        {
                            ModifyLstAREADER.Items.Add(ModifyLstSREADER.Items[i]);
                            ListItem li = ModifyLstSREADER.Items[i];
                            ModifyLstSREADER.Items.Remove(li);
                        }
                    }
                    lstAReader.SelectedValue = null;
                }
            }

            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnModifyCancelZone_Click(object sender, EventArgs e)
        {
            mpeModifyZone.Hide();

        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            mpeAddZone.Hide();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessages.Text = string.Empty;
                DataTable dt = clsZoneBrowseHandler.GetAllDetails("All", Session["levelId"].ToString());
                if (textzonename.Text.ToString() == "" && textzoneid.Text.ToString() == "")
                {
                    gvZone.DataSource = dt;
                    gvZone.DataBind();

                }
                else

                {
                     String[,] values = { 
				{"ZONE_ID~" +textzoneid.Text.Trim(), "S" },
				{"ZONE_DESCRIPTION~" +textzonename.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvZone.DataSource = _tempDT;
                    gvZone.DataBind();
                }
                DropDownList ddl = (DropDownList)gvZone.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvZone.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvZone.PageIndex + 1).ToString();
                Label lblcount = (Label)gvZone.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvZone.DataSource).Rows.Count.ToString() + " Records.";
                if (gvZone.PageCount == 0)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvZone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvZone.PageIndex + 1 == gvZone.PageCount)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvZone.PageIndex == 0)
                {
                    ((Button)gvZone.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }               

                ((Label)gvZone.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvZone.PageSize * gvZone.PageIndex) + 1) + " to " + (((gvZone.PageSize * (gvZone.PageIndex + 1)) - 10) + gvZone.Rows.Count);

                gvZone.BottomPagerRow.Visible = true;
            }


            catch (Exception ex)
            {
               
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                delete();

            }
            catch (Exception ex)
            {
              
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        private void delete()
        {
            try
            {
                int cnt = 0;
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<ZONE_READER_REL>");
                for (int i = 0; i < gvZone.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvZone.Rows[i].FindControl("DeleteRows");
                    string s1 = gvZone.Rows[i].Cells[2].Text;
                    if (chk.Checked == true)
                    {
                        cnt++;
                        strXML.Append("<READER_REL>");
                        strXML.Append("<RD_ZN_ID>" + s1 + "</RD_ZN_ID>");
                        strXML.Append("</READER_REL>");
                    }

                }
                strXML.Append("</ZONE_READER_REL>");
                if (cnt > 0)
                {
                    clsZoneBrowse objData = new clsZoneBrowse();                   
                    objData.CreatedBy = Session["uid"].ToString();
                    clsZoneBrowseHandler.InsertZoneBrowseDetails(objData, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                    if (strErrMsg.Length >= 1)
                    {
                        bindDataGrid();
                        lblMessages.Visible = true;
                        lblMessages.Text = strSuccMsg.Trim();
                    }
                    else
                    {
                        bindDataGrid();
                        lblMessages.Visible = true;
                        lblMessages.Text = strSuccMsg.Trim();
                    }
                }
            }

            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvZone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("DeleteRows");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                //if (string.Equals(hdnFlag.Value, "A", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    ChkDelete.Enabled = true;
                //    // lnkEdit.Enabled = true;
                //}
                //else
                //{
                //    ChkDelete.Enabled = false;
                //    //lnkEdit.Enabled = false;
                //}
            }
        }

    }
}