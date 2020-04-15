using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CMS.UNO.Core.Handler;
using System.Text;

namespace UNO
{
    public partial class CommonViewMstr : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        static string strSuccMsg, strErrMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDataGrid();
                fillCompany(ddlCompany);
                fillCompany(ddlcom);
                FillEntity(lstFile);
                FillEntity(ddlEntity);
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvCompany.ClientID + "');");
                BindEntDropDown();
            }
        }

        public void fillCompany(DropDownList drpEdit)
        {
            string a = FillManagerEntity();

            DataSet ds = clsCommonHandler.GetCommonTableDetails("","");
            DataTable dt = ds.Tables[0];

            #region OPAccess
            ///////////Started///////////
            DataTable thisDataTable = new DataTable();
            DataSet thisDataSet = new DataSet();

            DataTable temp = new DataTable();
            if (dt.Rows.Count > 0)
            {
                string levelId = Session["levelId"].ToString();

                SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@levelid", levelId);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                adpt.Fill(thisDataSet);
                thisDataTable = thisDataSet.Tables[4];

                IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
                                              join comwise in thisDataTable.AsEnumerable() on acs.Field<string>("ID") equals comwise.Field<string>("CompanyID")
                                              select acs
                             );
                if (Session["uid"].ToString().ToLower() != "admin")
                {
                    dt = drRow.CopyToDataTable();
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            ///////////end////////////
            # endregion OPAccess

            if (dt.Rows.Count > 0)
            {
                drpEdit.DataValueField = "ID";
                drpEdit.DataTextField = "Value";
                drpEdit.DataSource = dt;
                drpEdit.DataBind();
                drpEdit.Items.Insert(0, new ListItem("Select One", "0"));
            }
        }

        private void FillEntity(DropDownList drpEdit)
        {
            try
            {
                DataTable dt = clsCommonHandler.GetEntParameterValues("COMMONMASTERS", "ENT");
                if (dt.Rows.Count > 0)
                {
                    drpEdit.DataValueField = "Code";
                    drpEdit.DataTextField = "value";
                    drpEdit.DataSource = dt;
                    drpEdit.DataBind();
                    drpEdit.Items.Insert(0, new ListItem("Select One", "0"));
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
                List<clsCommon> lstCommonView = GetcommonList();

                #region OPAccess
                ///////////Started///////////
                DataTable thisDataTable = new DataTable();
                DataSet thisDataSet = new DataSet();

                DataTable temp = new DataTable();
                if (lstCommonView.Count > 0)
                {
                    string levelId = Session["levelId"].ToString();

                    SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                    adpt.Fill(thisDataSet);
                    thisDataTable = thisDataSet.Tables[4];

                    IEnumerable<clsCommon> drRow = (from acs in lstCommonView.AsEnumerable()
                                                  join comwise in thisDataTable.AsEnumerable() 
                                                  on acs.CompanyID equals comwise.Field<string>("CompanyID")
                                                  select acs
                                 );
                    if (Session["uid"].ToString().ToLower() != "admin")
                    {
                       // lstCommonView.Clear();
                        lstCommonView = drRow.ToList();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                ///////////end////////////
                # endregion OPAccess


                Session["lstCommon"] = lstCommonView;
                gvCompany.DataSource = lstCommonView;
                gvCompany.DataBind();                




                DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvCompany.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = lstCommonView.Count.ToString() + " Records.";
                if (gvCompany.PageCount == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvCompany.PageIndex == 0)
                {
                    ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }

                ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);

                gvCompany.BottomPagerRow.Visible = true;


                if (lstCommonView.Count != 0)
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
       
        private string FillManagerEntity()
        {
             string strSql = "", str="";
            try
            {             
                strSql = "select Company from Operational_Access where LevelCode=" + Session["levelId"].ToString();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(strSql, conn);

                SqlDataReader adpt = cmd.ExecuteReader();
                while (adpt.Read())
                {
                    str = adpt[0].ToString();
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            if (str == "")
                return str;
            else
            {
                str = new String(str.Where(Char.IsLetter).ToArray());
                return str;
            }
        }

        private List<clsCommon> GetcommonList()
        {
            List<clsCommon> lstCommonView;
            lstCommonView = CMS.UNO.Core.Handler.clsCommonViewHandler.GetCommonData("Common");
            Session["lstCommon"] = lstCommonView;
            return lstCommonView;
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompany.PageIndex = e.NewPageIndex;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {

                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = Convert.ToInt32(((DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex - 1;
               //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvCompany.PageIndex = gvCompany.PageIndex + 1;
                //bindDataGrid();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "CommonMasterView");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            ddlEntity.SelectedValue = "0";
            txtID.Text = "";
            txtDesc.Text = "";
            mpeAddCommon.Show();
            txtID.Focus();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                clsCommon objCommon = new clsCommon();
                objCommon.CompanyID = ddlCompany.SelectedValue;
                objCommon.ID = txtID.Text.Trim();
                objCommon.ENTID = lstFile.SelectedValue;
                objCommon.Description = txtDesc.Text.Trim();
                objCommon.CreatedBy = Session["uid"].ToString();

                CMS.UNO.Core.Handler.clsCommonViewHandler.InsertCommonDetails(objCommon, "Insert", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Trim().Length >= 1)
                {
                    lblerror.Visible = true;
                    lblerror.Text = strErrMsg;
                    mpeAddCommon.Show();
                    return;
                }
                else
                {
                    lblerror.Text = strSuccMsg;
                    lblerror.Visible = true;
                    txtID.Text = "";
                    txtDesc.Text = "";
                    lstFile.SelectedValue = "0";
                    lstFile.Focus();
                    mpeAddCommon.Show();
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        public void FillModifyDetails(string oceid, string entity_id, string compantid)
        {
            try
            {
                List<clsCommon> lstCommonView;
                if (Session["lstCommon"] == null)
                {
                    lstCommonView = CMS.UNO.Core.Handler.clsCommonViewHandler.GetCommonData("Common");
                    Session["lstCommon"] = lstCommonView;
                }
                else
                    lstCommonView = (List<clsCommon>)Session["lstCommon"];

                IEnumerable<clsCommon> IEnlstCommonView = null;
                IEnlstCommonView = from item in clsCommonViewHandler.GetCommonData("Common")
                                   where item.ID == (oceid.Trim())
                                   && item.ENTID.ToLower() == (entity_id.ToLower())
                                   && item.CompanyID.ToLower()==compantid.ToLower()                                   
                                   select item;

                List<clsCommon> lstSearchList = IEnlstCommonView.ToList<clsCommon>();

                if (lstSearchList.Count > 0)
                {
                    //Added by Pooja Yadav
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:DisplayLabelTextEntity();", true);
                    ddlEntity.SelectedValue = lstSearchList[0].ENTID;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:DisplayLabelTextEntity();", true);
                    ddlcom.SelectedValue = lstSearchList[0].CompanyID;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Display", "javascript:DisplayLabelTextEntity();", true);
                    txtisd.Text = lstSearchList[0].ID;
                    txtName.Text = lstSearchList[0].Description;
                }
                else
                {
                    lblMessages.Visible = true;
                    lblMessages.Text = "Records not found";
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit3")
            {
                lblMessages.Text = "";
                lblEditError.Text = "";
                string Rowid = e.CommandArgument.ToString();
                txtName.Focus();
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string entityid = row.Cells[2].Text;
                string compantid = row.Cells[5].Text;
                FillModifyDetails(Rowid, entityid, compantid);
                mpModifyCommon.Show();
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            mpeAddCommon.Hide();
        }

        protected void btnModifyCancel_Click(object sender, EventArgs e)
        {
            mpModifyCommon.Hide();
        }

        protected void btnModifySave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtisd.Text;
                string strDescription = txtName.Text.Trim().Length > 0 ? (txtName.Text.Trim().First().ToString().ToUpper() + String.Join("", txtName.Text.Trim().Skip(1))) : "";

                clsCommon objCommon = new clsCommon();
                objCommon.ID = id.Trim();
                objCommon.ENTID = ddlEntity.SelectedValue;
                objCommon.Description = strDescription;
                objCommon.CreatedBy = Session["uid"].ToString();
                CMS.UNO.Core.Handler.clsCommonViewHandler.InsertCommonDetails(objCommon, "Update", "", ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());

                if (strErrMsg.Trim().Length >= 1)
                {
                    lblEditError.Visible = true;
                    lblEditError.Text = strErrMsg;
                    mpModifyCommon.Show();
                    return;
                }
                else
                {
                    mpModifyCommon.Hide();
                    //lblMessages.Text = strSuccMsg;
                    lblMessages.Enabled = true;
                    lblMessages.Text = "Record Update Successfully";
                    txtisd.Text = "";
                    txtName.Text = "";
                    lblEditError.Text = "";
                    ddlEntity.SelectedValue = "0";
                    bindDataGrid();
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsCommon> lstCommonView;
                if (Session["lstCommon"] == null)
                {
                    lstCommonView = CMS.UNO.Core.Handler.clsCommonViewHandler.GetCommonData("Common");
                    Session["lstCommon"] = lstCommonView;
                }
                else
                    lstCommonView = (List<clsCommon>)Session["lstCommon"];


                if (txtCompanyID.Text.ToString() == "" && txtCompanyName.Text.ToString() == "" && ddlSearchEnt.SelectedItem.ToString() == "Select")
                {
                    bindDataGrid();
                }
                else
                {
                    IEnumerable<clsCommon> IEnlstCommonView = null;
                    if (txtCompanyID.Text.ToString().Trim() != "" && txtCompanyName.Text.Trim() != "")

                        IEnlstCommonView = from item in clsCommonViewHandler.GetCommonData("Common")
                                           where item.ID.Contains(txtCompanyID.Text.ToString().Trim())
                                           && item.Description.ToLower().Contains(txtCompanyName.Text.Trim().ToLower())
                                           select item;

                    else if (txtCompanyID.Text.ToString().Trim() != "" && txtCompanyName.Text.Trim() == "")

                        IEnlstCommonView = from item in clsCommonViewHandler.GetCommonData("Common")
                                           where item.ID.Contains(txtCompanyID.Text.ToString().Trim())
                                           select item;
                    else if (txtCompanyID.Text.ToString().Trim() == "" && txtCompanyName.Text.Trim() != "")

                        IEnlstCommonView = from item in clsCommonViewHandler.GetCommonData("Common")
                                           where item.Description.ToLower().Contains(txtCompanyName.Text.Trim().ToLower())
                                           select item;
                    else
                    {
                        IEnlstCommonView = from item in clsCommonViewHandler.GetCommonData("Common")
                                           select item;
                    }


                    if (ddlSearchEnt.SelectedValue.ToString() != "Select")
                    {
                        IEnumerable<clsCommon> IEnlstCommonViewByCode = null;
                        IEnlstCommonViewByCode = from item in IEnlstCommonView
                                                where item.ENTID == ddlSearchEnt.SelectedValue.ToString()
                                                select item;

                        IEnlstCommonView = IEnlstCommonViewByCode;
                    }

                    gvCompany.BottomPagerRow.Visible = true;

                    List<clsCommon> lstCommonList = IEnlstCommonView.ToList<clsCommon>();
                    gvCompany.DataSource = lstCommonList;
                    gvCompany.DataBind();

                    DropDownList ddl = (DropDownList)gvCompany.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvCompany.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvCompany.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvCompany.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = (lstCommonList.Count().ToString()) + " Records.";
                    if (gvCompany.PageCount == 0)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCompany.PageIndex + 1 == gvCompany.PageCount)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvCompany.PageIndex == 0)
                    {
                        ((Button)gvCompany.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }

                    ((Label)gvCompany.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvCompany.PageSize * gvCompany.PageIndex) + 1) + " to " + (((gvCompany.PageSize * (gvCompany.PageIndex + 1)) - 10) + gvCompany.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            clsCommon objCommon = new clsCommon();
            objCommon.CreatedBy = Session["uid"].ToString();
            StringBuilder strXML = new StringBuilder();
            try
            {
                strXML.Append("<ENT_ORG_COMMON_ENTITIES>");
                for (int i = 0; i < gvCompany.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("DeleteRows");
                    if (chk.Checked)
                    {
                        strXML.Append("<ENTITIES>");
                        strXML.Append("<OCE_ID>" + gvCompany.Rows[i].Cells[3].Text + "</OCE_ID>");
                        strXML.Append("<CEM_ENTITY_ID>" + gvCompany.Rows[i].Cells[2].Text + "</CEM_ENTITY_ID>");
                        strXML.Append("</ENTITIES>");
                    }
                }
                strXML.Append("</ENT_ORG_COMMON_ENTITIES>");
                clsCommonViewHandler.InsertCommonDetails(objCommon, "Delete", strXML.ToString(), ref strErrMsg, ref strSuccMsg, clsCommonHandler.PageName());
                if (strSuccMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strSuccMsg;
                    lblMessages.Visible = true;
                    bindDataGrid();
                }             
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void BindEntDropDown()
        {
            DataSet ds = clsCommonHandler.GetCommonTableDetails("","");
            ddlSearchEnt.DataSource = ds.Tables[8];
            ddlSearchEnt.DataTextField = "CEM_ENTITY_ID";
            ddlSearchEnt.DataValueField = "CEM_ENTITY_ID";
            ddlSearchEnt.DataBind();

            ddlSearchEnt.Items.Insert(0, "Select");
        }
    }
}