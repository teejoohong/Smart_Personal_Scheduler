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
    public partial class Register : System.Web.UI.Page
    {
        Boolean duplicate = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);
            con.Open();

            string strSelect = "SELECT Name FROM [User]";
            //specify what is the command , what is the connection string
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);

            SqlDataReader dtr = cmdSelect.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (txtUsername.Text.Equals(dtr["Name"]))
                    {
                        duplicate = true;
                        lblDuplicate.Text = "Username existed.";

                    }
                }
            }
            con.Close();

            if (!duplicate) //no repeated username
            {
                SqlConnection newCon;
                string newStrCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                newCon = new SqlConnection(newStrCon);
                newCon.Open();

                string strSelect2 = "Select count(UserID) from [User]";
                SqlCommand cmdSelect2 = new SqlCommand(strSelect2, newCon);

                int total = (int)cmdSelect2.ExecuteScalar() + 1;
                newCon.Close();

                String userID = "US" + total.ToString();

                
                newCon.Open();

                string strInsert = "INSERT INTO [User] (UserID, Name, Email, Password,Gender) VALUES (@UserID, @Name, @Email, @Password,@Gender)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, newCon);
                cmdInsert.Parameters.AddWithValue("@UserID", userID);
                cmdInsert.Parameters.AddWithValue("@Name", txtUsername.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@Gender", ddlGender.Text.ToString());
                int n = cmdInsert.ExecuteNonQuery();

                if (n > 0) // Use to check whether the value have been insert into the database
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Account Successfully Created!" + "');", true);
                    Response.Redirect("LogIn.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed! " + "');", true);
                }

                newCon.Close();

            }
        }
    }
}