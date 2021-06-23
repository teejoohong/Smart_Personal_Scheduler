using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
            {
                MultiView1.ActiveViewIndex = 0;
                SqlConnection con;
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);

                //profile pic command
                SqlDataSource1.SelectCommand = "SELECT [ProfilePicture] FROM [User] WHERE UserID = @UserID";

                con.Open();
                string strSelect = "SELECT * FROM [User] WHERE UserID = @UserID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@UserID", Session["UserID"]);
                SqlDataReader dtr = cmdSelect.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        lblUsername.Text = dtr["Name"].ToString();
                        lblEmail.Text = dtr["Email"].ToString();
                    }
                }
                con.Close();
            }
            else {
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            DataRowView datarow = (DataRowView)e.Item.DataItem;
            string imageUrl = (datarow["ProfilePicture"]).ToString();
            (e.Item.FindControl("Image1") as Image).ImageUrl = imageUrl;

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

    }
}