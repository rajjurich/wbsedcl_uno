using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Text;

namespace UNO
{
    public partial class SACCapturePersoDetails : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvEmpDetails();
            }
        }

        private void BindgvEmpDetails()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("getEmployeePersoDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (txtEmpID.Text.ToString() == "" && txtEmpName.Text.ToString() == "")
                {
                    gvEmpDetails.DataSource = dt;
                    gvEmpDetails.DataBind();

                }
                else
                {
                    String[,] values = { 
				{"EPD_EMPID~" +txtEmpID.Text.Trim(), "S" },
				{"Emp_Name~" +txtEmpName.Text.Trim(), "S" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt);
                    gvEmpDetails.DataSource = _tempDT;
                    gvEmpDetails.DataBind();
                }
                if (dt.Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)gvEmpDetails.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvEmpDetails.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvEmpDetails.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvEmpDetails.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvEmpDetails.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvEmpDetails.PageCount == 0)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmpDetails.PageIndex + 1 == gvEmpDetails.PageCount)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvEmpDetails.PageIndex == 0)
                    {
                        ((Button)gvEmpDetails.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvEmpDetails.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvEmpDetails.PageSize * gvEmpDetails.PageIndex) + 1) + " to " + (gvEmpDetails.PageSize * (gvEmpDetails.PageIndex + 1));

                    gvEmpDetails.BottomPagerRow.Visible = true;
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void gvEmpDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblImage = (Label)e.Row.FindControl("lblImage");

                    Label lblSign = (Label)e.Row.FindControl("lblSign");
                    Label lblBio1 = (Label)e.Row.FindControl("lblBio1");
                    Label lblBio2 = (Label)e.Row.FindControl("lblBio2");
                    Label lblName = (Label)e.Row.FindControl("lblName");
                    LinkButton lnkImage = (LinkButton)e.Row.FindControl("lnkImage");
                    LinkButton lnkSign = (LinkButton)e.Row.FindControl("lnkSign");
                    LinkButton lnkBio = (LinkButton)e.Row.FindControl("lnkBio");
                    LinkButton lnkWriteOnCard = (LinkButton)e.Row.FindControl("lnkWriteOnCard");

                    lnkImage.Attributes.Add("onclick", "return Image('" + lnkImage.CommandArgument + "');");
                    lnkSign.Attributes.Add("onclick", "return Sign('" + lnkSign.CommandArgument + "');");
                    lnkBio.Attributes.Add("onclick", "return Finger('" + lnkBio.CommandArgument + "');");
                    //   lnkWriteOnCard.Attributes.Add("onclick", "return WriteOnCard('" + lnkWriteOnCard.CommandArgument + "','" + lblName.Text + "');");

                    if (lblImage.Text != "")
                    {
                        lnkImage.Text = "Modify";
                    }
                    if (lblSign.Text != "")
                    {
                        lnkSign.Text = "Modify";
                    }
                    if (lblBio1.Text != "" && lblBio2.Text != "")
                    {
                        lnkBio.Text = "Modify";
                        lnkWriteOnCard.Visible = true;
                    }



                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvEmpDetails();
        }
        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = gvEmpDetails.PageIndex - 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }
        }
        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = gvEmpDetails.PageIndex + 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "ESS_TA_MA_View");
            }
        }
        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvEmpDetails.PageIndex = Convert.ToInt32(((DropDownList)gvEmpDetails.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                BindgvEmpDetails();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindgvEmpDetails();
        }

        protected void gvEmpDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Write")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string strName = Convert.ToString(((Label)row.FindControl("lblName")).Text.ToString()); //this store
                BindEmpData(e.CommandArgument.ToString(), strName);
                mpeAddCall.Show();
            }
            else
            {
                BindgvEmpDetails();
            }
        }
        protected void BindEmpData(string empCode, string empName)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("USP_GetEmpDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@Empcode", empCode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblEmpCode.Text = empCode;
                    lblEmpName.Text = empName;
                    lblDep.Text = Convert.ToString(dt.Rows[0]["DEP"]);
                    lblDesig.Text = Convert.ToString(dt.Rows[0]["DESIG"]);
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
        [WebMethod]
        public static string GetISOTemplate(string EmployeeCode)
        {
            string json = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("spGetISOTemplate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = EmployeeCode;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                json = "False";
            }
            else
            {
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("ISOTemplate1", typeof(string));
                dt.Columns.Add("ISOTemplate2", typeof(string));
                dt.Columns.Add("ActivationDate", typeof(int));
                dt.Columns.Add("ExpiryDate", typeof(int));
                dt.Columns.Add("AadharNo", typeof(string));
                dt.Columns.Add("CenterCode", typeof(string));
                dt.Columns.Add("LocationCode", typeof(string));
                DataRow dr = dt.NewRow();
                dr["ISOTemplate1"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOTemplate1"]);
                dr["ISOTemplate2"] = ByteArrayToString((byte[])ds.Tables[0].Rows[0]["ISOTemplate2"]);
                dr["ActivationDate"] = ds.Tables[0].Rows[0]["ActivationDate"];
                dr["ExpiryDate"] = ds.Tables[0].Rows[0]["ExpiryDate"];
                dr["AadharNo"] = ds.Tables[0].Rows[0]["AadharNo"];
                dr["CenterCode"] = ds.Tables[0].Rows[0]["CenterCode"];
                dr["LocationCode"] = ds.Tables[0].Rows[0]["LocationCode"];
                dt.Rows.Add(dr);
                ds1.Tables.Add(dt);
                ds1.AcceptChanges();
                json = ds1.GetXml();
            }
            return json;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
        }

        protected void btnSaveManualAtt_Click(object sender, EventArgs e)
        {
            mpeAddCall.Hide();
            Page.ClientScript.RegisterStartupScript(typeof(System.String), this.ClientID, "<script>WriteOnCard()</script>");
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            try
            {
                txtEmpID.Text = "";
                txtEmpName.Text = "";
                gvEmpDetails.PageIndex = 0;
                BindgvEmpDetails();
            }
            catch(Exception ex)
            {
            }
        }

    }
}