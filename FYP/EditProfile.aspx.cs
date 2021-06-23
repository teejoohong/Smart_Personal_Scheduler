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
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null && !IsPostBack)
            {
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
                        txtEmail.Text = dtr["Email"].ToString();
                    }
                }
                con.Close();
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            DataRowView datarow = (DataRowView)e.Item.DataItem;
            string imageUrl = (datarow["ProfilePicture"]).ToString();
            (e.Item.FindControl("Image1") as Image).ImageUrl = imageUrl;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            SqlConnection con1;
            string strcon1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con1 = new SqlConnection(strcon1);

            con1.Open();
            string strUpdate = "UPDATE [User] SET Email = @email WHERE UserID = @UserID";

            SqlCommand cmdUpdate = new SqlCommand(strUpdate, con1);

         
            cmdUpdate.Parameters.AddWithValue("@email", txtEmail.Text);
            cmdUpdate.Parameters.AddWithValue("@UserID", Session["UserID"]);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + txtEmail.Text + "');", true);
            int n = cmdUpdate.ExecuteNonQuery();
            if (n > 0) // Use to check whether the value have been insert into the database
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Edit Account Successfully !" + "');", true);
                Response.Redirect("Profile.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed!" + "');", true);
            }
            con1.Close();
            
            
        }
    }
}