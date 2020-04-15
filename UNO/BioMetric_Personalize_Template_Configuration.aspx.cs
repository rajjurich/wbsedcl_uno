using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using CMS.UNO.Core.Handler;
using System.Web.Services;

namespace UNO
{
    public partial class BioMetric_Personalize_Template_Configuration : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        String _strList = "", _strALList = "";
        static string strSuccMsg, strErrMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["radiocount"] = 0;
                bindDataGrid();
                GetRecord();
                //fillEntityAdd();
                btnDelete.Attributes.Add("onclick", "javascript:return handleDelete('" + gvEmployeeAccess.ClientID + "');");
            }
            lblMessages.Visible = false;
        }

        private List<BioMatricTemp> GetcommonList()
        {
            List<BioMatricTemp> lstCommonView;
            lstCommonView = CMS.UNO.Core.Handler.clsBioMetrictemplateconfigurationHandler.GetCommonData("Common");
            Session["lstCommon"] = lstCommonView;
            return lstCommonView;
        }

        void GetRecord()
        {
            string[] ArrFingureCount;
            try
            {
                List<BioMatricTemp> lstCommonView = GetcommonList();
                if (lstCommonView.Count > 0)
                {
                    ArrFingureCount = lstCommonView[0].FingureCount.Split(',');

                    for (int i = 0; i < ArrFingureCount.Length; i++)
                    {
                        switch (ArrFingureCount[i])
                        {
                            case "1":
                                LHR1.Enabled = true;
                                break;
                            case "2":
                                LHR2.Enabled = true;
                                break;
                            case "3":
                                LHR3.Enabled = true;
                                break;
                            case "4":
                                LHR4.Enabled = true;
                                break;
                            case "5":
                                LHR5.Enabled = true;
                                break;
                            case "6":
                                RHR1.Enabled = true;
                                break;
                            case "7":
                                RHR2.Enabled = true;
                                break;
                            case "8":
                                RHR3.Enabled = true;
                                break;
                            case "9":
                                RHR4.Enabled = true;
                                break;
                            case "10":
                                RHR5.Enabled = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        //private void fillEntityAdd()
        //{
        //    try
        //    {
        //        lstAEntityAdd.Items.Clear();
        //        ListBox1.Items.Clear();
        //        string sql = "";
        //        sql = "SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) as NAME " +
        //             "  FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod " +
        //             "  on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1'  AND EPD_EMPID NOT IN (SELECT CC_EMP_ID FROM ACS_CARD_CONFIG WHERE ACE_isdeleted = '0')";

        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.CommandType = CommandType.Text;

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        if (Session["uid"].ToString().ToLower() == "admin")
        //        {
        //            if (dt != null)
        //            {
        //                for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                {
        //                    lstAEntityAdd.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //                    ListBox1.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            #region OPAccess
        //            ///////////Started///////////
        //            DataTable thisDataTable = new DataTable();
        //            DataSet thisDataSet = new DataSet();

        //            DataTable temp = new DataTable();
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (conn.State == ConnectionState.Closed)
        //                {
        //                    conn.Open();
        //                }
        //                string levelId = Session["levelId"].ToString();

        //                SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
        //                cmd1.CommandType = CommandType.StoredProcedure;
        //                cmd1.Parameters.AddWithValue("@levelid", levelId);
        //                SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
        //                adpt.Fill(thisDataSet);
        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //            }
        //            ///////////end////////////
        //            # endregion OPAccess


        //                    thisDataTable = thisDataSet.Tables[0];
        //                    IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
        //                                                  join comwise in thisDataTable.AsEnumerable() on acs.Field<string>("EMPID") equals comwise.Field<string>("EOD_EMPID")
        //                                                  select acs
        //                               );
        //                    dt = drRow.CopyToDataTable();
                           


        //        }
        //        if (dt != null)
        //        {
        //            for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //            {
        //                lstAEntityAdd.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //                ListBox1.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //            }
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
        //    }
        //}

        [WebMethod]
        public static string[] GetCustomers(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT epd_empid as ID,replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ ','+epd_empid,' ',' ' ) as NAME  FROM ENT_employee_personal_dtls emp inner join ENT_EMPLOYEE_OFFICIAL_DTLS eod  on emp.EPD_EMPID=eod.EOD_EMPID where emp.EPD_ISDELETED='0' and eod.EOD_ACTIVE='1'  AND epd_empid LIKE @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["NAME"], sdr["ID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }

        protected void LHR1Changed(object sender, EventArgs e)
        {
            if (LHR1.Checked == false)
            {
                LHR1.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR1.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR2Changed(object sender, EventArgs e)
        {
            if (LHR2.Checked == false)
            {
                LHR2.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR2.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR3Changed(object sender, EventArgs e)
        {
            if (LHR3.Checked == false)
            {
                LHR3.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR3.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR4Changed(object sender, EventArgs e)
        {
            if (LHR4.Checked == false)
            {
                LHR4.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR4.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void LHR5Changed(object sender, EventArgs e)
        {
            if (LHR5.Checked == false)
            {
                LHR5.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                LHR5.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR1Changed(object sender, EventArgs e)
        {
            if (RHR1.Checked == false)
            {
                RHR1.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR1.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR2Changed(object sender, EventArgs e)
        {
            if (RHR2.Checked == false)
            {
                RHR2.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR2.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR3Changed(object sender, EventArgs e)
        {
            if (RHR3.Checked == false)
            {
                RHR3.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR3.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR4Changed(object sender, EventArgs e)
        {
            if (RHR4.Checked == false)
            {
                RHR4.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR4.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }

        protected void RHR5Changed(object sender, EventArgs e)
        {
            if (RHR5.Checked == false)
            {
                RHR5.Checked = false;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) - 1;
            }
            else
            {
                RHR5.Checked = true;
                Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
            }
        }


//===================================================================================================================================================

        //----------------------Grid Start ----------------------------------

        private void bindDataGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "";

                sql = " SELECT B.ID, B.EmpId, E.EPD_FIRST_NAME +' '+E.EPD_LAST_NAME AS EMPNAME, B.FingureForTA FROM BioMetricPersonalizeTemplateConfiguration B Join ENT_EMPLOYEE_PERSONAL_DTLS E ON B.EmpId=E.EPD_EMPID ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                gvEmployeeAccess.DataSource = dt;
                gvEmployeeAccess.DataBind();

                DropDownList ddl = (DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmployeeAccess.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmployeeAccess.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmployeeAccess.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmployeeAccess.PageCount == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex + 1 == gvEmployeeAccess.PageCount)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1));

                ((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (((gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1)) - 10) + gvEmployeeAccess.Rows.Count);

                gvEmployeeAccess.BottomPagerRow.Visible = true;

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
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = Convert.ToInt32(((DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = gvEmployeeAccess.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmployeeAccess.PageIndex = gvEmployeeAccess.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        //----------------------Grid End ------------------------------------
        
//===================================================================================================================================================

        //----------------------Search Start ----------------------------------

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessages.Text = string.Empty;
            search();
        }
        
        private void search()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sql = "";

                sql = " SELECT B.ID, B.EmpId, E.EPD_FIRST_NAME +' '+E.EPD_LAST_NAME AS EMPNAME, B.FingureForTA FROM BioMetricPersonalizeTemplateConfiguration B Join ENT_EMPLOYEE_PERSONAL_DTLS E ON B.EmpId=E.EPD_EMPID ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ///////////Started///////////
                DataTable thisDataSet = new DataTable(); ;
                DataTable temp = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    string levelId = Session["levelId"].ToString();

                    SqlCommand cmd1 = new SqlCommand("spFillEntities2", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@levelid", levelId);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
                    thisDataSet = new DataTable();
                    adpt.Fill(thisDataSet);

                    IEnumerable<DataRow> drRow = (from acs in dt.AsEnumerable()
                                                  join comwise in thisDataSet.AsEnumerable() on acs.Field<string>("ID") equals comwise.Field<string>("EOD_EMPID")
                                                  select acs
                                 );
                    if (Session["uid"].ToString().ToLower() == "admin")
                    {
                        temp = dt;
                    }
                    else
                    {
                        temp = drRow.CopyToDataTable();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                ///////////end////////////
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                if (txtEntityName.Text.ToString() == "" && txtEntityId.Text.ToString() == "")
                {
                    gvEmployeeAccess.DataSource = temp;
                    gvEmployeeAccess.DataBind();
                }
                else
                {
                    String[,] values = { 
				{"EMPNAME~" +txtEntityName.Text.Trim(), "S" },
				{"EmpId~" +txtEntityId.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, temp);
                    gvEmployeeAccess.DataSource = _tempDT;
                    gvEmployeeAccess.DataBind();
                }

                DropDownList ddl = (DropDownList)gvEmployeeAccess.BottomPagerRow.FindControl("ddlPageNo");
                for (int i = 1; i <= gvEmployeeAccess.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddl.SelectedValue = (gvEmployeeAccess.PageIndex + 1).ToString();
                Label lblcount = (Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblTotal");
                lblcount.Text = ((DataTable)gvEmployeeAccess.DataSource).Rows.Count.ToString() + " Records.";
                if (gvEmployeeAccess.PageCount == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex + 1 == gvEmployeeAccess.PageCount)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                }
                if (gvEmployeeAccess.PageIndex == 0)
                {
                    ((Button)gvEmployeeAccess.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                }
                //((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1));

                ((Label)gvEmployeeAccess.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmployeeAccess.PageSize * gvEmployeeAccess.PageIndex) + 1) + " to " + (((gvEmployeeAccess.PageSize * (gvEmployeeAccess.PageIndex + 1)) - 10) + gvEmployeeAccess.Rows.Count);

                gvEmployeeAccess.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        //----------------------Search End ------------------------------------

//===================================================================================================================================================

        //----------------------Add New Start ----------------------------------        

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSelectionAdd();
                btnSubmitEdit.Visible = false;
                btnSubmitAdd.Visible = true;
                //lstAEntityAdd.Attributes.Remove("disabled");
                //txtAEntityDescAdd.Visible = true;
                mpeAddEmployeeAccess.Show();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }

        protected void btnSubmitAdd_Click(object sender, EventArgs e)
        {
            lblErrorAdd.Visible = true;
            if (Convert.ToInt16(Session["radiocount"]) != 2)
            {
                lblErrorAdd.Text = "Select at least two finger for time attendance";
            }
            else
            {
                string FingureTA = string.Empty;

                if (LHR1.Checked == true)
                    FingureTA = FingureTA + "1,";
                if (LHR2.Checked == true)
                    FingureTA = FingureTA + "2,";
                if (LHR3.Checked == true)
                    FingureTA = FingureTA + "3,";
                if (LHR4.Checked == true)
                    FingureTA = FingureTA + "4,";
                if (LHR5.Checked == true)
                    FingureTA = FingureTA + "5,";
                if (RHR1.Checked == true)
                    FingureTA = FingureTA + "6,";
                if (RHR2.Checked == true)
                    FingureTA = FingureTA + "7,";
                if (RHR3.Checked == true)
                    FingureTA = FingureTA + "8,";
                if (RHR4.Checked == true)
                    FingureTA = FingureTA + "9,";
                if (RHR5.Checked == true)
                    FingureTA = FingureTA + "10,";

                FingureTA = FingureTA.Substring(0, FingureTA.Length - 1);

                string[] FTA = FingureTA.Split(',');
                string FingureTAlen = string.Empty;
                foreach (var item in FTA)
                {
                    FingureTAlen = FingureTAlen + item;
                }
                if (FTA.Length == 2)
                {
                    BioMatricPersonalizeTemp objCommon = new BioMatricPersonalizeTemp();
                    objCommon.Id = 0;
                    objCommon.EmpId = txtSearch.Text.Split(',')[1]; // lstAEntityAdd.SelectedValue;
                    objCommon.FingureForTA = FingureTA;
                    clsBioMetricPersonalizetemplateconfigurationHandler.InsertCommonDetails(objCommon, "Insert", ref strErrMsg, ref strSuccMsg);
                    
                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblErrorAdd.Text = strErrMsg;
                    }
                    else
                    {
                        lblErrorAdd.Text = strSuccMsg;
                    }
                    GetRecord();
                }
                else
                {                    
                    lblErrorAdd.Text = "Please Select at least two finger for time attendance";
                }
            }
            btnSubmitEdit.Visible = false;
            btnSubmitAdd.Visible = false;
            bindDataGrid();
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            ClearSelectionAdd();
            mpeAddEmployeeAccess.Hide();
        }

        void ClearSelectionAdd()
        {
            Session["radiocount"] = 0;
            LHR1.Checked = false;
            LHR2.Checked = false;
            LHR3.Checked = false;
            LHR4.Checked = false;
            LHR5.Checked = false;
            RHR1.Checked = false;
            RHR2.Checked = false;
            RHR3.Checked = false;
            RHR4.Checked = false;
            RHR5.Checked = false;
            //ddlEmpAdd.SelectedIndex = 0;
            lblErrorAdd.Visible = false;
            //lstAEntityAdd.Items.Clear();
            txtSearch.Text = "";
            txtSearch.Enabled = true;
            btnSubmitEdit.Visible = false;
            btnSubmitAdd.Visible = false;
            //fillEntityAdd();
        }

        //----------------------Add New End ----------------------------------

//===================================================================================================================================================

        //----------------------Edit Start ----------------------------------   

        protected void gvEmployeeAccess_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Session["radiocount"] = 0;
                btnSubmitEdit.Visible = false;
                btnSubmitAdd.Visible = false;

                int Id = Convert.ToInt32(e.CommandArgument);

                string sql = "";
                sql = " SELECT B.ID, B.EmpId, replace(convert(char(20),ltrim(EPD_FIRST_NAME  ))+ epd_empid,' ',' ' ) AS EMPNAME, B.FingureForTA FROM BioMetricPersonalizeTemplateConfiguration B Join ENT_EMPLOYEE_PERSONAL_DTLS E ON B.EmpId=E.EPD_EMPID Where B.Id= " + Id;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                UpdateRecord(dt);
                //lstAEntityAdd.SelectedValue
                txtSearch.Text = dt.Rows[0]["EMPNAME"].ToString();
                hfId.Value = dt.Rows[0]["EmpId"].ToString();
                //lstAEntityAdd.Attributes.Add("disabled", "");
                txtSearch.Enabled = false;
                btnSubmitEdit.Visible = true;
                btnSubmitAdd.Visible = false;
                //txtAEntityDescAdd.Visible = false;
                mpeAddEmployeeAccess.Show();                
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "UserAccessConfig");
            }
        }       

        void UpdateRecord(DataTable dt)
        {
            string[] ArrFingureForTA;
            try
            {
                ArrFingureForTA = dt.Rows[0]["FingureForTA"].ToString().Split(',');

                for (int i = 0; i < ArrFingureForTA.Length; i++)
                {
                    switch (ArrFingureForTA[i])
                    {
                        case "1":
                            LHR1.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "2":
                            LHR2.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "3":
                            LHR3.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "4":
                            LHR4.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "5":
                            LHR5.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "6":
                            RHR1.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "7":
                            RHR2.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "8":
                            RHR3.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "9":
                            RHR4.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                        case "10":
                            RHR5.Checked = true;
                            Session["radiocount"] = Convert.ToInt16(Session["radiocount"]) + 1;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            lblErrorAdd.Visible = true;
            if (Convert.ToInt16(Session["radiocount"]) != 2)
            {
                lblErrorAdd.Text = "Select at least two finger for time attendance";
            }
            else
            {
                string FingureTA = string.Empty;

                if (LHR1.Checked == true)
                    FingureTA = FingureTA + "1,";
                if (LHR2.Checked == true)
                    FingureTA = FingureTA + "2,";
                if (LHR3.Checked == true)
                    FingureTA = FingureTA + "3,";
                if (LHR4.Checked == true)
                    FingureTA = FingureTA + "4,";
                if (LHR5.Checked == true)
                    FingureTA = FingureTA + "5,";
                if (RHR1.Checked == true)
                    FingureTA = FingureTA + "6,";
                if (RHR2.Checked == true)
                    FingureTA = FingureTA + "7,";
                if (RHR3.Checked == true)
                    FingureTA = FingureTA + "8,";
                if (RHR4.Checked == true)
                    FingureTA = FingureTA + "9,";
                if (RHR5.Checked == true)
                    FingureTA = FingureTA + "10,";

                FingureTA = FingureTA.Substring(0, FingureTA.Length - 1);

                string[] FTA = FingureTA.Split(',');
                string FingureTAlen = string.Empty;
                foreach (var item in FTA)
                {
                    FingureTAlen = FingureTAlen + item;
                }
                if (FTA.Length == 2)
                {
                    BioMatricPersonalizeTemp objCommon = new BioMatricPersonalizeTemp();
                    objCommon.Id = 0;
                    objCommon.EmpId = hfId.Value;// lstAEntityAdd.SelectedValue;
                    objCommon.FingureForTA = FingureTA;
                    clsBioMetricPersonalizetemplateconfigurationHandler.InsertCommonDetails(objCommon, "Update", ref strErrMsg, ref strSuccMsg);

                    if (strErrMsg.Trim().Length >= 1)
                    {
                        lblErrorAdd.Text = strErrMsg;
                    }
                    else
                    {
                        lblErrorAdd.Text = strSuccMsg;
                    }
                    GetRecord();
                }
                else
                {
                    lblErrorAdd.Text = "Please Select at least two finger for time attendance";
                }
            }
            btnSubmitEdit.Visible = false;
            btnSubmitAdd.Visible = false;
            bindDataGrid();
            //txtAEntityDescAdd.Visible = true;
        }       

        //----------------------Edit End ------------------------------------

//===================================================================================================================================================

        //----------------------Delete Start ----------------------------------
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringCollection idCollection = new StringCollection();
            String index = "";
            for (int i = 0; i < gvEmployeeAccess.Rows.Count; i++)
            {
                CheckBox delrows = (CheckBox)gvEmployeeAccess.Rows[i].FindControl("DeleteRows");
                if (delrows.Checked == true)
                {
                    index = (String)gvEmployeeAccess.DataKeys[i].Values[0].ToString();
                    index = index + "-" + (String)gvEmployeeAccess.DataKeys[i].Values[1].ToString();
                    index = index + "-" + (String)gvEmployeeAccess.DataKeys[i].Values[2].ToString();
                    idCollection.Add(index.ToString());
                }
            }

            if (idCollection.Count != 0)
            {
                _strList = "";
                string[] _ID;

                for (int i = 0; i <= idCollection.Count - 1; i++)
                {
                    _ID = idCollection[i].ToString().Trim().Split('-');
                    clsBioMetricPersonalizetemplateconfigurationHandler.DeleteCommonDetails( _ID[1].ToString(), "Delete", ref strErrMsg, ref strSuccMsg);    
                }

                lblMessages.Visible = true;
                if (strErrMsg.Trim().Length >= 1)
                {
                    lblMessages.Text = strErrMsg;
                }
                else
                {
                    lblMessages.Text = strSuccMsg;
                }

                bindDataGrid();
                ClearSelectionAdd();
            }
            else
            {
                lblMessages.Text = "Please select record to delete";
                lblMessages.Visible = true;
            }

        }       

        //----------------------Delete End ------------------------------------
              
    }
}