using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace UNO
{
    public partial class TEClientMasterView : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        private string SearchString = "";
        private string strOutput;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDataGrid();
                //ClientScriptManager csm = Page.ClientScript;
                //String callbackRef = csm.GetCallbackEventReference(this, "arg", "fnGetOutputFromServer", "");
                //String callbackScript = "function fnCallServerMethod(arg, context) {" + callbackRef + "; }";
                //csm.RegisterClientScriptBlock(this.GetType(), "fnCallServerMethod", callbackScript, true);
            }

            //Grid.DataSource = BindGrid();
            //Grid.DataBind();
           
        }

        public void RaiseCallbackEvent(String clientArgs)
        {
            string[] str = clientArgs.Split('|');
            FilterGrid(str[0], str[1]);
        }

        public string GetCallbackResult()
        {
            return strOutput;
        }

        private DataTable BindGrid()
        {
            string sql = "SELECT CLIENT_ID,CLIENT_NAME,CLIENT_DESC,CLIENT_SITE_ADDRESS,CLIENT_HO_ADDRESS,CLIENT_PHONE1,"+
                         " CLIENT_PHONE2,CLIENT_CONTACT_PERSON1,CLIENT_CONTPER1_PHONE1,CLIENT_CONTPER1_PHONE2"+
                         " ,CLIENT_CONTACT_PERSON2,CLIENT_CONTPER2_PHONE1,CLIENT_CONTPER2_PHONE2 FROM TE_CLIENT_FILE where CLIENT_ISDELETED = '0'";

            //string sql = "Select * from te_project_file";
          //  SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["m_connectionString"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.ViewState["Table"] = dt;
            return dt;
        }
        private void FilterGrid(string CusName, string CusCity)
        {
            DataTable dt = (DataTable)this.ViewState["Table"];
            DataView dv = dt.DefaultView;
            if (strOutput != "")
            {
                dv.RowFilter = "client_id Like '%" + CusName + "%' And Client_Desc Like '%" + CusCity + "%'";
            }
           
            Grid.DataSource = dv;
            Grid.DataBind();

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Grid.RenderControl(htw);
                    htw.Flush();
                    strOutput = sw.ToString();
                }
            }
        }

   
        
        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            Grid.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
           
        }
       

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.PageIndex = e.NewPageIndex;
     
          
        }

             

        protected void Grid_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            Grid.EditIndex = e.NewEditIndex;
            string ID = Grid.Rows[Grid.EditIndex].Cells[2].Text;
            Session["Mode"] = "Modify";
            Response.Redirect("TEClientMasterFile.aspx?id=" + ID);
        
        }


        void bindDataGrid()
        {
            try
            {
                string strsql = "SELECT CLIENT_ID,CLIENT_NAME,CLIENT_DESC,CLIENT_SITE_ADDRESS,CLIENT_HO_ADDRESS,CLIENT_PHONE1," +
                                " CLIENT_PHONE2,CLIENT_CONTACT_PERSON1,CLIENT_CONTPER1_PHONE1,CLIENT_CONTPER1_PHONE2" +
                                " ,CLIENT_CONTACT_PERSON2,CLIENT_CONTPER2_PHONE1,CLIENT_CONTPER2_PHONE2 FROM TE_CLIENT_FILE where CLIENT_ISDELETED = '0'";

                SqlDataAdapter daLVdetails = new SqlDataAdapter(strsql, conn );
                DataTable dtLVdetails = new DataTable();
                daLVdetails.Fill(dtLVdetails);

                if (dtLVdetails.Rows.Count != 0)
                {
                    Grid.DataSource = dtLVdetails;
                    Grid.DataBind();
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
                this.LblMsg.Text = ex.Message;
                LoadJScript();
            }

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
           // Changes done by Pravin Nair on 22/01/2014
            
            SqlCommand objcmd = new SqlCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    CheckBox delrow = (CheckBox)Grid.Rows[i].FindControl("DeleteRows");
                    if (delrow.Checked == true)
                    {
                        string clientCode = Grid.Rows[i].Cells[2].Text;
                        String sqlstr = "SELECT PROJECT_CODE FROM TE_PROJECT_FILE WHERE PROJECT_CLIENT_ID= '" + Grid.Rows[i].Cells[2].Text + "' AND PROJECT_ISDELETED='0' ";
                        objcmd.CommandText = sqlstr;
                        objcmd.Connection = conn;
                        SqlDataReader sqldr = objcmd.ExecuteReader();

                        if (Convert.ToInt32(sqldr.Read()) <= 1)
                        {
                            this.LblMsg.Text = "One or More Project Attached to this Client";
                            LoadJScript();
                            return;
                        }
                        objcmd.CommandText = "Update TE_CLIENT_FILE set CLIENT_ISDELETED = '1',CLIENT_DELETEDDATE = convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103) " +
                                        " where CLIENT_ID = '" + Grid.Rows[i].Cells[2].Text + "' ";
                        objcmd.ExecuteNonQuery();
                        this.LblMsg.Text = "Record Deleted Successfully.";
                        LoadJScript();

                    }
                }
                
            }
            catch (Exception ex)
            {
                this.LblMsg.Text = ex.Message;
                LoadJScript();
            }
            conn.Close();
            bindDataGrid();
            // bug 68 solve start -- Swapnil
            Response.Redirect("TEClientMasterView.aspx", true);
            // bug 68 solve End -- Swapnil
        }

        internal void LoadJScript()
        {
            ClientScriptManager script = Page.ClientScript;
            if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
            {
                script.RegisterStartupScript(this.GetType(), "HideLabel",
                "<script type='text/javascript'>HideLabel('" + LblMsg.ClientID + "')</script>");
            }
        }

       
       

       

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //  Set the value of the SearchString so it gets
            ////SearchString = txtSearch.Text;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session.Remove("Mode");
            Response.Redirect("TEClientMasterFile.aspx");
        }

        protected void Grid_DataBound(object sender, EventArgs e)
        {
            string strsql = "Select  CLIENT_ID,CLIENT_DESC FROM TE_CLIENT_FILE where CLIENT_ISDELETED = '0'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            int recordcnt = dt.Rows.Count;

            if (Grid.Rows.Count > 0)
            {
                pager.Visible = true;

                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = Grid.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }

                Label lblCount = Grid.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (Grid.PageIndex * Grid.PageSize) + Grid.PageSize;
                //int totCustomerCount = AdvWorksDB.GetCustomersCount(hfSearchCriteria.Value);
                int totCustomerCount = recordcnt;
                totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                lblCount.Text = ((Grid.PageIndex * Grid.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totCustomerCount.ToString();
                Grid.BottomPagerRow.Visible = true;

            }
            else
            {
                pager.Visible = false;
            }
        }

        protected void Grid_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Grid_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        // bug 87 solved start --swapnil
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            string strsql = "SELECT CLIENT_ID,CLIENT_NAME,CLIENT_DESC,CLIENT_SITE_ADDRESS,CLIENT_HO_ADDRESS,CLIENT_PHONE1," +
                                " CLIENT_PHONE2,CLIENT_CONTACT_PERSON1,CLIENT_CONTPER1_PHONE1,CLIENT_CONTPER1_PHONE2" +
                                " ,CLIENT_CONTACT_PERSON2,CLIENT_CONTPER2_PHONE1,CLIENT_CONTPER2_PHONE2 FROM TE_CLIENT_FILE where CLIENT_ISDELETED = '0'";

            SqlDataAdapter daLVdetails = new SqlDataAdapter(strsql, conn);
            DataTable dtLVdetails = new DataTable();
            daLVdetails.Fill(dtLVdetails);

            if (txtCompanyId.Text.ToString() == "" && txtCompanyName.Text.ToString() == "")
            {
                cmdReset_Click(sender, e);
            }
            else
            {
                String[,] values = { 
				{"CLIENT_ID-" +txtCompanyId.Text.Trim(), "S" },
				{"CLIENT_NAME-" +txtCompanyName.Text.Trim(), "S" }			
				 };
                DataTable _tempDT = new DataTable();
                Search _sc = new Search();
                if (_tempDT != null)
                { _tempDT.Rows.Clear(); }
                _tempDT = _sc.searchTable(values, dtLVdetails);
                Grid.DataSource = _tempDT;
                Grid.DataBind();

            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Text = "";
            txtCompanyName.Text = "";
            bindDataGrid();

        }
        // bug 87 solved End --swapnil
        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    SearchString = txtSearch.Text;
        //} 
    }
}