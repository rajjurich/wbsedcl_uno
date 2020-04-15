using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace UNO
{
    public partial class TEClientMasterFile : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ToString());
        public DataTable dtStakeHolder = new DataTable();
        public string ClientCode = "";
        public string Entity_Mode = "";
        bool Flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientCode = Convert.ToString(Request.QueryString["id"]);
            if (Session["Mode"] != null)
            {
                Entity_Mode = Session["Mode"].ToString();
            }
            else
            {
                Entity_Mode = "Add";
            }
            if (!Page.IsPostBack)
            {
                Session["NAME"] = null;
                if (ClientCode != null && Entity_Mode == "Modify")
                {
                    Flag = false;
                    Modify_Data(ClientCode);
                }
                else
                {

                }
            }
            else
            {
                if (Entity_Mode == "Modify" && Flag == true)
                {
                    Modify_Data(ClientCode);
                }
                else
                {

                }
            }
        }
        protected void Modify_Data(string Employeeid)
        {
            string strsql = " SELECT CLIENT_ID,CLIENT_NAME,CLIENT_DESC,CLIENT_SITE_ADDRESS,CLIENT_HO_ADDRESS,CLIENT_PHONE1," +
                            " CLIENT_PHONE2,CLIENT_CONTACT_PERSON1,CLIENT_CONTPER1_PHONE1,CLIENT_CONTPER1_PHONE2" +
                            " ,CLIENT_CONTACT_PERSON2,CLIENT_CONTPER2_PHONE1,CLIENT_CONTPER2_PHONE2 FROM TE_CLIENT_FILE where CLIENT_ID='" + ClientCode + "' AND  CLIENT_ISDELETED = '0'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            DataTable dt = new DataTable();
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                txtClientID.Text = dt.Rows[i]["CLIENT_ID"].ToString();
                txtClientName.Text = dt.Rows[i]["CLIENT_NAME"].ToString();
                txtClientDesc.Text = dt.Rows[i]["CLIENT_DESC"].ToString();
                txtContPerson1.Text = dt.Rows[i]["CLIENT_CONTACT_PERSON1"].ToString();
                txtContPerson2.Text = dt.Rows[i]["CLIENT_CONTACT_PERSON2"].ToString();
                txtClientAddress.Text = dt.Rows[i]["CLIENT_SITE_ADDRESS"].ToString();
                txtClientHOAddress.Text = dt.Rows[i]["CLIENT_HO_ADDRESS"].ToString();
                txtClientPh1.Text = dt.Rows[i]["CLIENT_PHONE1"].ToString();
                txtClientPh2.Text = dt.Rows[i]["CLIENT_PHONE2"].ToString();
                txtContFPerNm1.Text = dt.Rows[i]["CLIENT_CONTPER1_PHONE1"].ToString();
                txtContFPerNm2.Text = dt.Rows[i]["CLIENT_CONTPER1_PHONE2"].ToString();
                txtContSPerNm1.Text = dt.Rows[i]["CLIENT_CONTPER2_PHONE1"].ToString();
                txtContSPerNm2.Text = dt.Rows[i]["CLIENT_CONTPER2_PHONE2"].ToString();

            }

            txtClientID.Enabled = false;
        }



        protected void LnkBtn0_Click(object sender, EventArgs e)
        {
            if (txtClientID.Text != "" && txtClientID.Enabled == false)
            {
                LblMsg.Text = "Save the Records or click cancel";
                LoadJScript();
                return;

            }
            else
            {
                Response.Redirect("ClientMasterView.aspx");
                LoadJScript();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Entity_Mode == "Add")
            {
                resetall();

            }
            else
            {
                Response.Redirect("TEClientMasterView.aspx", true);
                //LblMsg.Text = "Modify page could not cancelled the page.please go back ";
                //LoadJScript();
            }

        }

        internal void LoadJScript()
        {
            ClientScriptManager script = Page.ClientScript;
            //prevent duplicate script
            if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
            {
                script.RegisterStartupScript(this.GetType(), "HideLabel",
                "<script type='text/javascript'>HideLabel('" + LblMsg.ClientID + "')</script>");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand objcmd = new SqlCommand();
            objcmd.Connection = conn;
            try
            {
                if (txtClientHOAddress.Text == "")
                {
                    txtClientHOAddress.Text = "";
                }
                if (txtClientPh2.Text == "")
                {
                    txtClientPh2.Text = "0";
                }
                if (txtContFPerNm2.Text == "")
                {
                    txtContFPerNm2.Text = "0";
                }
                if (txtContSPerNm2.Text == "")
                {
                    txtContSPerNm2.Text = "0";
                }
                if (Entity_Mode == "Add")
                {

                    int rows = 0;
                    DataTable dtseldates = new DataTable();

                    objcmd.CommandType = CommandType.Text;
                    objcmd.CommandText = "INSERT INTO TE_CLIENT_FILE([CLIENT_ID],[CLIENT_NAME],[CLIENT_DESC],[CLIENT_SITE_ADDRESS]" +
                                       ",[CLIENT_HO_ADDRESS],[CLIENT_PHONE1],[CLIENT_PHONE2],[CLIENT_CONTACT_PERSON1],[CLIENT_CONTPER1_PHONE1]," +
                                       " [CLIENT_CONTPER1_PHONE2],[CLIENT_CONTACT_PERSON2],[CLIENT_CONTPER2_PHONE1],[CLIENT_CONTPER2_PHONE2]," +
                                       " [CLIENT_ISDELETED],[CLIENT_DELETEDDATE])VALUES ('" + txtClientID.Text + "','" + txtClientName.Text + "'," +
                                       " '" + txtClientDesc.Text + "','" + txtClientAddress.Text + "'," +
                                       " '" + txtClientHOAddress.Text + "','" + txtClientPh1.Text + "','" + txtClientPh2.Text + "','" + txtContPerson1.Text + "'," +
                                       " '" + txtContFPerNm1.Text + "','" + txtContFPerNm2.Text + "','" + txtContPerson2.Text + "','" + txtContSPerNm1.Text + "'," +
                                       " '" + txtContSPerNm2.Text + "', '0',NULL)";


                    rows = objcmd.ExecuteNonQuery();
                    this.LblMsg.Text = "Records Saved Successfully.";
                    LoadJScript();
                    Response.Redirect("TEClientMasterView.aspx", false);

                }
                else
                {
                    int rows = 0;
                    DataTable dtseldates = new DataTable();

                    objcmd.CommandType = CommandType.Text;
                    objcmd.CommandText = "UPDATE TE_CLIENT_FILE SET CLIENT_NAME='" + txtClientName.Text + "',CLIENT_DESC='" + txtClientDesc.Text + "'" +
                                         " ,CLIENT_SITE_ADDRESS='" + txtClientAddress.Text + "',CLIENT_HO_ADDRESS='" + txtClientHOAddress.Text + "'" +
                                         " ,CLIENT_PHONE1='" + txtClientPh1.Text + "',CLIENT_PHONE2='" + txtClientPh2.Text + "',CLIENT_CONTACT_PERSON1='" + txtContPerson1.Text + "'," +
                                         " CLIENT_CONTPER1_PHONE1='" + txtContFPerNm1.Text + "',CLIENT_CONTPER1_PHONE2='" + txtContFPerNm2.Text + "'," +
                                         " CLIENT_CONTACT_PERSON2='" + txtContPerson2.Text + "',CLIENT_CONTPER2_PHONE1='" + txtContSPerNm1.Text + "'," +
                                         " CLIENT_CONTPER2_PHONE2='" + txtContSPerNm2.Text + "' WHERE CLIENT_ID='" + ClientCode + "'";

                    rows = objcmd.ExecuteNonQuery();
                    this.LblMsg.Text = "Records Modified Successfully.";
                    LoadJScript();
                    Response.Redirect("TEClientMasterView.aspx", false);
                }
            }
            catch (Exception ex)
            {
                this.LblMsg.Text = ex.Message;
            }


            resetall();
            conn.Close();
        }
        public void resetall()
        {

            txtClientID.Text = "";
            txtClientDesc.Text = "";
            txtClientName.Text = "";
            txtClientAddress.Text = "";
            txtClientHOAddress.Text = "";
            txtClientPh1.Text = "";
            txtClientPh2.Text = "";
            txtContFPerNm1.Text = "";
            txtContFPerNm2.Text = "";
            txtContSPerNm1.Text = "";
            txtContSPerNm2.Text = "";
            txtContPerson1.Text = "";
            txtContPerson2.Text = "";
        }
    }
}







