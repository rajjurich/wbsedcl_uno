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
using System.Collections;
namespace UNO
{

    public partial class RestoreSql : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        string strFileLocation;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDataGrid();
                    //strFileLocation = GetFileLocation();
                    //if (strFileLocation.Trim().Length > 1)
                    //    FillddlSqlList(strFileLocation);
                    //else
                    //    ClientScript.RegisterStartupScript(this.GetType(), "IsExists", "<script language='javascript'>PushAlert('File Path Does not Exists');</script>", true);
                    btnRestore.Attributes.Add("onclick", "javascript:return validateCheckBoxes('" + gvBackupData.ClientID + "');");
                    

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }   
   
        protected void FillddlSqlList(string strLocation)
        {
            string[] filePaths = Directory.GetFiles(strLocation, "*.bak");
            string[] filename = new string[filePaths.GetLength(0)];
            List<string[]> lstfile = new List<string[]>();
            for (int i = 0; i < filePaths.Length; i++)
            {
                filename[i] = Path.GetFileName(filePaths[i]);
                lstfile.Add(new string[] { filename[i], filePaths[i] });
            }
 
        }

        protected string GetFileLocation()
        {
            SqlDataAdapter da = new SqlDataAdapter("select  BackupPath from AutoScheduledBackupConfig", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count >= 1)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {

            mpePassword.Show();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            chkPassword();

        }



        protected void btnConfirm_Click(object sender,EventArgs e)
        {

            if (txtConfirm.Text.ToUpper() == "YES")
            {
                Restore();
            }
            else
            {
                txtConfirm.Focus();
            }
        }

        private bool chkPassword()
        {
            string pwd = null;

            pwd = txtPassword.Text;

            string password = Encryption.EncryptDecrypt.Decrypt(Session["Password"].ToString(), true);

            if (password == pwd)
            {

                tblConfirm.Visible = true;
                tblpassword.Visible = false;
                
            }
            else
            {
                lblMsg.Text = "Inavlid password , please try again";

                tblConfirm.Visible = false;
                tblpassword.Visible = true;
                
                return false;
            }

            return true;

        }

        private bool Restore()
        {

            bool Record = false;
            try
            {

                for (int i = 0; i < gvBackupData.Rows.Count; i++)
                {
                    CheckBox chkRows = (CheckBox)gvBackupData.Rows[i].FindControl("DeleteRows");
                    Label lblname = (Label)gvBackupData.Rows[i].FindControl("lblname");
                    if (chkRows.Checked == true)
                    {
                        Record = true;
                        bool check = File.Exists(lblname.Text);
                        if (check)
                        {
                            string dbName = Path.GetFileName(lblname.Text);
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "USP_Backup";
                            cmd.Connection = conn;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@database", conn.Database);
                            cmd.Parameters.AddWithValue("@strRestorePath", lblname.Text);
                            cmd.Parameters.AddWithValue("@strCommand", "Restore");
                            cmd.Parameters.AddWithValue("@userId", Session["uid"].ToString());
                            cmd.Parameters.AddWithValue("@Rerror", "0");
                            cmd.Parameters["@Rerror"].Direction = ParameterDirection.Output;
                            int j = cmd.ExecuteNonQuery();
                            string Result = Convert.ToString(cmd.Parameters["@Rerror"].Value);
                            conn.Close();
                            if (Result == "0")
                            {
                                lblMsg.Text = "Restore database successfully";
                                return true;
                            }
                            else
                            {
                                lblMsg.Text = "Error while restoring database";
                                return false;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "File Path Does not Exists";
                            return false;
                        }

                    }

                }
                if (Record == false)
                {
                    lblMsg.Text = "Please select database";
                    return false;
                }
                bindDataGrid();
            }
            catch (SqlException ex)
            {
                lblMsg.Text = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
                return false;
            }
            finally
            {

            }

            return false;
        }

        protected void bindDataGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_Backup";
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@strCommand", "selectAll");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (txtTodate.Text.ToString() == "" && txtDbName.Text.ToString() == "")
                {
                    gvBackupData.DataSource = dt.Tables[0];
                    gvBackupData.DataBind();
                   
                }
                else
                {
                    String[,] values = { 
				{"dbName~" +txtDbName.Text.Trim(), "S" },
				{"createdDate~" +txtTodate.Text.Trim(), "D" }			
				 };
                    DataTable _tempDT = new DataTable();
                    Search _sc = new Search();
                    if (_tempDT != null)
                    { _tempDT.Rows.Clear(); }
                    _tempDT = _sc.searchTable(values, dt.Tables[0]);
                    gvBackupData.DataSource = _tempDT;
                    gvBackupData.DataBind();
                }
                if (dt.Tables[0].Rows.Count != 0)
                {
                    //  btnApprove.Enabled = true;
                    // btnDelete.Enabled = true;
                    DropDownList ddl = (DropDownList)gvBackupData.BottomPagerRow.FindControl("ddlPageNo");
                    for (int i = 1; i <= gvBackupData.PageCount; i++)
                    {
                        ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddl.SelectedValue = (gvBackupData.PageIndex + 1).ToString();
                    Label lblcount = (Label)gvBackupData.BottomPagerRow.FindControl("lblTotal");
                    lblcount.Text = ((DataTable)gvBackupData.DataSource).Rows.Count.ToString() + " Records.";
                    if (gvBackupData.PageCount == 0)
                    {
                        ((Button)gvBackupData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                        ((Button)gvBackupData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvBackupData.PageIndex + 1 == gvBackupData.PageCount)
                    {
                        ((Button)gvBackupData.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                    }
                    if (gvBackupData.PageIndex == 0)
                    {
                        ((Button)gvBackupData.BottomPagerRow.FindControl("btnPrevious")).Enabled = false;
                    }
                    ((Label)gvBackupData.BottomPagerRow.FindControl("lblShowing")).Text = "Showing " + ((gvBackupData.PageSize * gvBackupData.PageIndex) + 1) + " to " + (gvBackupData.PageSize * (gvBackupData.PageIndex + 1));

                    gvBackupData.BottomPagerRow.Visible = true;
                }
                else
                {
                    // btnDelete.Enabled = false;
                    btnRestore.Enabled = false;
                }
                if(dt.Tables[1].Rows.Count>0)
                {
                    Label2.Text = "Last restored backup file - " + dt.Tables[1].Rows[0]["DbName"] + " Filepath: " + dt.Tables[1].Rows[0]["DbPath"] + " at " + dt.Tables[1].Rows[0]["RestoreTime"] + " by " + dt.Tables[1].Rows[0]["UserId"];
                }
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, clsCommonHandler.PageName());
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpePassword.Hide();
            txtPassword.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            strFileLocation = GetFileLocation();
            if (strFileLocation.Trim().Length > 1)
                FillddlSqlList(strFileLocation);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "IsExists", "<script language='javascript'>PushAlert('File Path Does not Exists');</script>", true);

        }

        protected void gvNext(object sender, EventArgs e)
        {
            try
            {
                gvBackupData.PageIndex = gvBackupData.PageIndex + 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "RestoreSql");
            }
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            try
            {
                gvBackupData.PageIndex = Convert.ToInt32(((DropDownList)gvBackupData.BottomPagerRow.FindControl("ddlPageNo")).SelectedValue) - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvPrevious(object sender, EventArgs e)
        {
            try
            {
                gvBackupData.PageIndex = gvBackupData.PageIndex - 1;
                bindDataGrid();
            }
            catch (Exception ex)
            {
                UNOException.UNO_DBErrorLog(ex.Message, ex.StackTrace, "RestoreSql");
            }
        }
     
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvBackupData.Rows.Count; i++)
            {
                try
                {
                    CheckBox chkRows = (CheckBox)gvBackupData.Rows[i].FindControl("DeleteRows");
                    Label lblname = (Label)gvBackupData.Rows[i].FindControl("lblname");
                    if (chkRows.Checked == true)
                    {
                        bool check = File.Exists(lblname.Text);
                        if (check)
                        {
                            string dbName = Path.GetFileName(lblname.Text);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "IsExists", "<script language='javascript'>PushAlert('File Path Does not Exists');</script>", true);
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }   

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindDataGrid();
        }
    }
}