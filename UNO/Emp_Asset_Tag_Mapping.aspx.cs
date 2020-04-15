using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace UNO
{
    public partial class Emp_Asset_Tag_Mapping : System.Web.UI.Page
    {
        generalCls g = new generalCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGrid();
                fillController();
                // mpeAddEdit.Hide();
            }
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + grdMapping.ClientID + "');");
            }
            ScriptManager.RegisterClientScriptBlock(upBody, upBody.GetType(), "TestScript", "fnTestScript('this');", true);

        }
        public DataTable fillDDL(string param, string cmd, string procname)
        {
            StringDictionary sd = new StringDictionary();
            sd.Add(param, cmd);
            DataSet ds = ExecuteSQL.ExecuteDataSet(procname, sd);
            return ds.Tables[0];
        }
        public void fillAsset()
        {
            DataTable dt = fillDDL("@cmd", "5", "PROC_ASSET_MASTER");
            ddlAsset.DataValueField = "Asset_Code";
            ddlAsset.DataTextField = "Asset_Desc";
            ddlAsset.DataSource = dt;
            ddlAsset.DataBind();
            ddlAsset.Items.Insert(0, "Select");
        }
        public void fillEmployee()
        {
            DataTable dt = fillDDL("@cmd", "6", "PROC_ASSET_MASTER");
            ddlEmp.DataValueField = "EPD_EMPID";
            ddlEmp.DataTextField = "NAME";
            ddlEmp.DataSource = dt;
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, "Select");
        }
        public void fillTag()
        {
            DataTable dt = fillDDL("@cmd", "7", "PROC_ASSET_TAG_MASTER");
            ddlTag.DataValueField = "TagEPCID";
            ddlTag.DataTextField = "TagEPCID";
            ddlTag.DataSource = dt;
            ddlTag.DataBind();
            ddlTag.Items.Insert(0, "Select");
        }
        public void fillController()
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "0");
            DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_CTLR_MASTER", sd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdController.DataSource = ds.Tables[0];
                grdController.DataBind();
            }
        }
        public void fillGrid()
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("@cmd", "0");
            DataSet ds = ExecuteSQL.ExecuteDataSet("PROC_ASSET_EMP_TAG_MAPPING", sd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdMapping.DataSource = ds.Tables[0];
                grdMapping.DataBind();

                DropDownList ddl = (DropDownList)grdMapping.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= grdMapping.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (grdMapping.PageIndex + 1).ToString();
                Label lblcount = (Label)grdMapping.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)grdMapping.DataSource).Rows.Count.ToString() + " Records.";
                if (grdMapping.PageCount == 0)
                {
                    ((Button)grdMapping.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)grdMapping.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (grdMapping.PageIndex + 1 == grdMapping.PageCount)
                {
                    ((Button)grdMapping.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (grdMapping.PageIndex == 0)
                {
                    ((Button)grdMapping.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                ((Label)grdMapping.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((grdMapping.PageSize * grdMapping.PageIndex) + 1) + " to " + (((grdMapping.PageSize * (grdMapping.PageIndex + 1)) - grdMapping.PageSize) + grdMapping.Rows.Count);

                grdMapping.BottomPagerRow.Visible = true;
            }
        }
        public DataSet hdnValueToTable()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns[0].DataType = typeof(string);
            string[] val = hdnConcatenatedValue.Value.Split(',');

            foreach (string str in val)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = str;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
            
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            generalCls.SaveEditFlag = "Save";
            mpeAddEdit.Show();

            fillTag();
            fillAsset();
            fillEmployee();
            ddlEmp.Enabled = true;
            ddlTag.Enabled = true;
            ddlAsset.Enabled = true;
            
            ScriptManager.RegisterClientScriptBlock(upPopUp, upPopUp.GetType(), "Script", "validateChosen();", true);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            try
            {
                bool Check = false;
                bool marked = false;
                for (int i = 0; i < grdMapping.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox delrows = (CheckBox)grdMapping.Rows[i].FindControl("chkDelete");
                        Label lblRowId = (Label)grdMapping.Rows[i].FindControl("lblRowID");
                        if (delrows.Checked == true)
                        {
                            if (marked == false)
                            {
                                marked = true;
                            }
                            
                            Hashtable ht = new Hashtable();
                            ht.Add("@cmd", 3);
                            ht.Add("@EATM_Id", lblRowId.Text);
                            ht.Add("@flag", "NO");
                            
                            DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);

                            Check = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessages.Text = ex.Message;
                    }
                }
                if (Check == true)
                {
                    lblMessages.Text = "Record Deleted Successfully";

                }
                else if (marked == false)
                {
                    lblMessages.Text = "Please select record to Delete";
                }
                fillGrid();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {

        }
        protected void gvPrevious(object sender, EventArgs e)
        {
        }
        protected void gvNext(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (generalCls.SaveEditFlag == "Save")
            {
                DataSet ds = hdnValueToTable();
                Hashtable ht = new Hashtable();
                ht.Add("@cmd", "1");
                ht.Add("@Emp_Id", ddlEmp.SelectedValue);
                ht.Add("@Tag_Id", ddlTag.SelectedValue);
                ht.Add("@userId", Session["uid"].ToString());
                ht.Add("@xmlString", ds.GetXml());
                ht.Add("@ValidFrom", DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                ht.Add("@ValidTill", DateTime.ParseExact(txtValidTo.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                ht.Add("@Asset_Id", ddlAsset.SelectedValue);
                DataSet dsOut = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);

                if (dsOut.Tables[0].Rows.Count > 0)
                {
                    msg = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            if (generalCls.SaveEditFlag == "Modify")
            {
                DataSet ds = hdnValueToTable();
                Hashtable ht = new Hashtable();
                ht.Add("@cmd", "2");
                ht.Add("@EATM_Id", generalCls.MappingId);
                ht.Add("@Emp_Id", ddlEmp.SelectedValue);
                ht.Add("@Tag_Id", ddlTag.SelectedValue);
                ht.Add("@userId", Session["uid"].ToString());
                ht.Add("@xmlString", ds.GetXml());
                ht.Add("@ValidFrom", DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                ht.Add("@ValidTill", DateTime.ParseExact(txtValidTo.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                ht.Add("@Asset_Id", ddlAsset.SelectedValue);
                DataSet dsOut = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);
                if (dsOut.Tables[0].Rows.Count > 0)
                {
                    msg = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            if (msg == "Record Saved Successfully")
            {
                lblMsg.Text = msg;
                mpeAddEdit.Show();
            }
            else
                lblMessages.Text = msg;

            fillGrid();
            reset();
        }
        protected void grdMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                generalCls.SaveEditFlag = "Modify";

                fillEmployee();

                string str = e.CommandArgument.ToString();
                generalCls.MappingId = Convert.ToInt16(str);
                Hashtable ht = new Hashtable();
                ht.Add("@cmd", 4);
                ht.Add("@EATM_Id", str);

                DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                    ddlEmp.Enabled = false;

                    string astDesc = ds.Tables[0].Rows[0]["Asset_ID"].ToString();
                    ddlAsset.Items.Insert(0, astDesc);
                    ddlAsset.SelectedIndex = 0;
                    ddlAsset.Enabled = false;

                    string tag = ds.Tables[0].Rows[0]["Tag_id"].ToString();
                    ddlTag.Items.Insert(0, tag);
                    ddlTag.SelectedIndex = 0;
                    ddlTag.Enabled = false;

                    txtValidFrom.Text = ds.Tables[0].Rows[0]["Validfrom"].ToString();
                    txtValidTo.Text = ds.Tables[0].Rows[0]["ValidTill"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (GridViewRow row in grdController.Rows)
                        {
                            CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                            HiddenField hdn = row.FindControl("hdnController") as HiddenField;
                            DataRow[] dr = ds.Tables[1].Select("Ctlr_Id='" + hdn.Value.ToString() + "'");

                            if (dr.Length > 0)
                            {
                                chk.Checked = true;
                                hdnConcatenatedValue.Value += hdn.Value.ToString() + ",";
                            }
                        }
                        hdnConcatenatedValue.Value = hdnConcatenatedValue.Value.Remove(hdnConcatenatedValue.Value.Length - 1, 1);
                    }

                    mpeAddEdit.Show();
                    ScriptManager.RegisterClientScriptBlock(upPopUp, upPopUp.GetType(), "Script", "validateChosen();", true);
                }
            }
            if (e.CommandName == "Return")
            {
                string str = e.CommandArgument.ToString();
                generalCls.MappingId = Convert.ToInt16(str);
                mpeReturnConfirm.Show();
            }
        }
        protected void ddlAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@cmd", 7);
            ht.Add("@Asset_Code", ddlAsset.SelectedValue.ToString());

            DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_MASTER", ht);

            ViewState["tag"] = ds.Tables[0].Rows[0][0].ToString();

            if (!(ViewState["tag"].ToString() == "" || ViewState["tag"].ToString() == null))
            {
                if (!ddlTag.Items.Contains(new ListItem(ViewState["tag"].ToString())))
                {
                    ddlTag.Items.Insert(ddlTag.Items.Count, ViewState["tag"].ToString());
                }
                ddlTag.SelectedValue = ViewState["tag"].ToString();
                ddlTag.Enabled = false;
            }
            else
            {
                ddlTag.SelectedIndex = 0;
                ddlTag.Enabled = true;
            }
            mpeAddEdit.Show();
            ScriptManager.RegisterClientScriptBlock(upPopUp, upPopUp.GetType(), "Script", "validateChosen();", true);
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string flag = "";
            if (chkReturn.Checked)
                flag = "YES";
            else
                flag = "NO";

            Hashtable ht = new Hashtable();
            ht.Add("@cmd", 3);
            ht.Add("@EATM_Id", generalCls.MappingId);
            ht.Add("@flag", flag);

            DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);
            mpeReturnConfirm.Hide();
            fillGrid();
        }
        public void reset()
        {
            ddlEmp.SelectedIndex = 0;
            ddlAsset.SelectedIndex = 0;
            ddlTag.SelectedIndex = 0;
            txtValidFrom.Text = "";
            txtValidTo.Text = "";

            for (int i = 0; i < grdController.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)grdController.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    chkSelect.Checked = false;
                }
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            chkReturn.Checked = false;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@cmd", 5);
            ht.Add("@searchText", txtSearch.Text.ToString().Trim());
            ht.Add("@searchVal", ddlSearch.SelectedValue.ToString());

            DataSet ds = ExecuteSQL.ExecuteDataSetHashTable("PROC_ASSET_EMP_TAG_MAPPING", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdMapping.DataSource = ds.Tables[0];
                grdMapping.DataBind();
            }
            else
            {
                grdMapping.DataSource = null;
                grdMapping.DataBind();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}