using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Name"] != null)
                {
                    txtUsername.Text = Request.Cookies["Name"].Value;
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);

            con.Open();
            string strSelect = "SELECT * FROM [User] WHERE Name = @Name";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@Name", txtUsername.Text); //supply value to @Username

            SqlDataReader dtr = cmdSelect.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (txtUsername.Text.Equals(dtr["Name"]))
                    {
                        if (dtr["Password"].ToString().Equals(txtPassword.Text))
                        {

                            Session["UserID"] = dtr["UserID"].ToString();
                            Session["UserName"] = dtr["Name"].ToString();
                            //---cookie
                            if (chkBoxRememberMe.Checked)
                            {
                                Response.Cookies["Name"].Value = txtUsername.Text;
                                Response.Cookies["Name"].Expires = DateTime.Now.AddDays(7);
                            }
                            else
                            {
                                Response.Cookies["Name"].Expires = DateTime.Now.AddMinutes(-1);
                            }

                            Response.Redirect("HomePage.aspx");

                        }
                        else
                        {
                            txtPassword.Text = "";
                            lblError.Text = "Incorrect Password";
                         }
                    }
                }
            }
            else
            {
                txtPassword.Text = "";
                txtUsername.Text = "";
                lblError.Text = "Username not existed.";
            }
            con.Close();
        }
    }

}