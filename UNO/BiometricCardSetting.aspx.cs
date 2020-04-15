using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace UNO
{
    public partial class BiometricCardSetting : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindddlNativeSector();
                BindddlISOSector();
                DataTable Settings = CheckSettings();
                if (Settings.Rows.Count > 0)
                {
                    txtScore.Text = Settings.Rows[0]["FingerQualityScore"].ToString();
                    txtRetries.Text = Settings.Rows[0]["NoOfRetries"].ToString();
                    txtTimeout.Text = Settings.Rows[0]["Timeout"].ToString();
                    ddlNativeSector.SelectedValue = Settings.Rows[0]["NativeSector"].ToString();
                    ddlISOSector.SelectedValue = Settings.Rows[0]["ISOSector"].ToString();
                    #region Switch Case
                    switch (Settings.Rows[0]["FVal"].ToString())
                    {
                        case "1":
                            {
                                Left1.Checked = true;
                                break;
                            }
                        case "2":
                            {
                                Left2.Checked = true;
                                break;
                            }
                        case "3":
                            {
                                Left3.Checked = true;
                                break;
                            }
                        case "4":
                            {
                                Left4.Checked = true;
                                break;
                            }
                        case "5":
                            {
                                Left5.Checked = true;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    switch (Settings.Rows[0]["SVal"].ToString())
                    {
                        case "6":
                            {
                                Right6.Checked = true;
                                break;
                            }
                        case "7":
                            {
                                Right7.Checked = true;
                                break;
                            }
                        case "8":
                            {
                                Right8.Checked = true;
                                break;
                            }
                        case "9":
                            {
                                Right9.Checked = true;
                                break;
                            }
                        case "10":
                            {
                                Right10.Checked = true;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    #endregion
                    Left1.Enabled = false;
                    Left2.Enabled = false;
                    Left3.Enabled = false;
                    Left4.Enabled = false;
                    Left5.Enabled = false;

                    Right6.Enabled = false;
                    Right7.Enabled = false;
                    Right8.Enabled = false;
                    Right9.Enabled = false;
                    Right10.Enabled = false;

                    txtScore.Enabled = false;
                    txtRetries.Enabled = false;
                    txtTimeout.Enabled = false;
                    ddlNativeSector.Enabled = false;
                    ddlISOSector.Enabled = false;

                    btnModify.Enabled = true;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    txtScore.Text = "";
                    txtRetries.Text = "";
                    txtTimeout.Text = "";
                    ddlNativeSector.SelectedValue = "0";
                    ddlISOSector.SelectedValue = "0";

                    txtScore.Enabled = true;
                    txtRetries.Enabled = true;
                    txtTimeout.Enabled = true;
                    ddlNativeSector.Enabled = true;
                    ddlISOSector.Enabled = true;

                    Left1.Enabled = true;
                    Left2.Enabled = true;
                    Left3.Enabled = true;
                    Left4.Enabled = true;
                    Left5.Enabled = true;

                    Right6.Enabled = true;
                    Right7.Enabled = true;
                    Right8.Enabled = true;
                    Right9.Enabled = true;
                    Right10.Enabled = true;

                    btnModify.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = true;
                }


            }
        }
        private void BindddlNativeSector()
        {
            ListItem select = new ListItem("Select", "0", true);
            ddlNativeSector.Items.Add(select);
            for (int i = 16; i <= 19; i++)
            {
                ListItem temp = new ListItem(i.ToString(), i.ToString(), true);
                ddlNativeSector.Items.Add(temp);
            }
        }
        private void BindddlISOSector()
        {
            ListItem select = new ListItem("Select", "0", true);
            ddlISOSector.Items.Add(select);
            for (int i = 32; i <= 38; i++)
            {
                ListItem temp = new ListItem(i.ToString(), i.ToString(), true);
                ddlISOSector.Items.Add(temp);
            }
        }
        private DataTable CheckSettings()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from dbo.BioMatricCardSetting", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return new DataTable();
            }
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            DataTable Settings = CheckSettings();
            if (Settings.Rows.Count > 0)
            {
                txtScore.Enabled = true;
                txtRetries.Enabled = true;
                txtTimeout.Enabled = true;
                ddlISOSector.Enabled = false;
                ddlNativeSector.Enabled = true;
                #region Switch Case
                switch (Settings.Rows[0]["FVal"].ToString())
                {
                    case "1":
                        {
                            Left1.Checked = true;
                            break;
                        }
                    case "2":
                        {
                            Left2.Checked = true;
                            break;
                        }
                    case "3":
                        {
                            Left3.Checked = true;
                            break;
                        }
                    case "4":
                        {
                            Left4.Checked = true;
                            break;
                        }
                    case "5":
                        {
                            Left5.Checked = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                switch (Settings.Rows[0]["SVal"].ToString())
                {
                    case "6":
                        {
                            Right6.Checked = true;
                            break;
                        }
                    case "7":
                        {
                            Right7.Checked = true;
                            break;
                        }
                    case "8":
                        {
                            Right8.Checked = true;
                            break;
                        }
                    case "9":
                        {
                            Right9.Checked = true;
                            break;
                        }
                    case "10":
                        {
                            Right10.Checked = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                #endregion

                Left1.Enabled = true;
                Left2.Enabled = true;
                Left3.Enabled = true;
                Left4.Enabled = true;
                Left5.Enabled = true;

                Right6.Enabled = true;
                Right7.Enabled = true;
                Right8.Enabled = true;
                Right9.Enabled = true;
                Right10.Enabled = true;

                btnModify.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                txtScore.Enabled = true;
                txtRetries.Enabled = true;
                txtTimeout.Enabled = true;
                ddlISOSector.Enabled = true;
                ddlNativeSector.Enabled = true;

                btnModify.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable Setting = CheckSettings();
                string Fval = "";
                string Sval = "";
                #region Fval, Sval
                if (Left1.Checked == true)
                {
                    Fval = "1";
                }
                if (Left2.Checked == true)
                {
                    Fval = "2";
                }
                if (Left3.Checked == true)
                {
                    Fval = "3";
                }
                if (Left4.Checked == true)
                {
                    Fval = "4";
                }
                if (Left5.Checked == true)
                {
                    Fval = "5";
                }
                if (Right6.Checked == true)
                {
                    Sval = "6";
                }
                if (Right7.Checked == true)
                {
                    Sval = "7";
                }
                if (Right8.Checked == true)
                {
                    Sval = "8";
                }
                if (Right9.Checked == true)
                {
                    Sval = "9";
                }
                if (Right10.Checked == true)
                {
                    Sval = "10";
                }
                #endregion
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                if (Setting.Rows.Count > 0)
                {
                    cmd.CommandText = "update BioMatricCardSetting set FingerQualityScore = '" + txtScore.Text + "',NativeSector = '" + ddlNativeSector.SelectedItem.Text + "',ISOSector = '" + ddlISOSector.SelectedItem.Text + "', NoOfRetries = '" + txtRetries.Text + "', Timeout = '" + txtTimeout.Text + "', FVal = '" + Fval + "', SVal = '" + Sval + "'";
                }
                else
                {
                    cmd.CommandText = "insert into BioMatricCardSetting values ('" + txtScore.Text + "', '" + txtRetries.Text + "', '" + txtTimeout.Text + "', '" + ddlNativeSector.SelectedItem.Text + "', '" + ddlISOSector.SelectedItem.Text + "', '" + Fval + "', '" + Sval + "')";
                }
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();

                Left1.Enabled = false;
                Left2.Enabled = false;
                Left3.Enabled = false;
                Left4.Enabled = false;
                Left5.Enabled = false;

                Right6.Enabled = false;
                Right7.Enabled = false;
                Right8.Enabled = false;
                Right9.Enabled = false;
                Right10.Enabled = false;

                txtScore.Enabled = false;
                txtRetries.Enabled = false;
                txtTimeout.Enabled = false;
                ddlNativeSector.Enabled = false;
                ddlISOSector.Enabled = false;

                btnSave.Enabled = false;
                btnModify.Enabled = true;
                btnCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable Settings = CheckSettings();
            if (Settings.Rows.Count > 0)
            {
                txtScore.Text = Settings.Rows[0]["FingerQualityScore"].ToString();
                txtRetries.Text = Settings.Rows[0]["NoOfRetries"].ToString();
                txtTimeout.Text = Settings.Rows[0]["Timeout"].ToString();
                ddlNativeSector.SelectedValue = Settings.Rows[0]["NativeSector"].ToString();
                ddlISOSector.SelectedValue = Settings.Rows[0]["ISOSector"].ToString();

                #region Switch Case
                switch (Settings.Rows[0]["FVal"].ToString())
                {
                    case "1":
                        {
                            Left1.Checked = true;
                            break;
                        }
                    case "2":
                        {
                            Left2.Checked = true;
                            break;
                        }
                    case "3":
                        {
                            Left3.Checked = true;
                            break;
                        }
                    case "4":
                        {
                            Left4.Checked = true;
                            break;
                        }
                    case "5":
                        {
                            Left5.Checked = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                switch (Settings.Rows[0]["SVal"].ToString())
                {
                    case "6":
                        {
                            Right6.Checked = true;
                            break;
                        }
                    case "7":
                        {
                            Right7.Checked = true;
                            break;
                        }
                    case "8":
                        {
                            Right8.Checked = true;
                            break;
                        }
                    case "9":
                        {
                            Right9.Checked = true;
                            break;
                        }
                    case "10":
                        {
                            Right10.Checked = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                #endregion
                Left1.Enabled = false;
                Left2.Enabled = false;
                Left3.Enabled = false;
                Left4.Enabled = false;
                Left5.Enabled = false;

                Right6.Enabled = false;
                Right7.Enabled = false;
                Right8.Enabled = false;
                Right9.Enabled = false;
                Right10.Enabled = false;

                txtScore.Enabled = false;
                txtRetries.Enabled = false;
                txtTimeout.Enabled = false;
                ddlNativeSector.Enabled = false;
                ddlISOSector.Enabled = false;

                btnModify.Enabled = true;
                btnCancel.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                txtScore.Text = "";
                txtRetries.Text = "";
                txtTimeout.Text = "";
                ddlNativeSector.SelectedValue = "0";
                ddlISOSector.SelectedValue = "0";

                txtScore.Enabled = true;
                txtRetries.Enabled = true;
                txtTimeout.Enabled = true;
                ddlNativeSector.Enabled = true;
                ddlISOSector.Enabled = true;

                btnModify.Enabled = false;
                btnCancel.Enabled = false;
                btnSave.Enabled = true;
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("CardDashboard.aspx");
        }
    }
}