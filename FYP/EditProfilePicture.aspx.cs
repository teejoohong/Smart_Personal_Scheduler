using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace FYP
{
    public partial class EditProfilePicture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
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
                    }
                }
                con.Close();
            }
        }

        //ITEM BOUND
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            DataRowView datarow = (DataRowView)e.Item.DataItem;
            string imageUrl = (datarow["ProfilePicture"]).ToString();
            (e.Item.FindControl("Image1") as Image).ImageUrl = imageUrl;

        }

        protected void Change_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);



            int length = FileUpload1.PostedFile.ContentLength;
            byte[] pic = new byte[length];
            FileUpload1.PostedFile.InputStream.Read(pic, 0, length);

            con.Open();

            string strUpdate = "UPDATE [User] SET ProfilePicture = @ProfilePicture WHERE UserID = @UserID";

            SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string filePath = "ProfilePic/" + fileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath(filePath));

            try
            {
                cmdUpdate.Parameters.AddWithValue("@ProfilePicture", filePath);
                cmdUpdate.Parameters.AddWithValue("@UserID", Session["UserID"]);
                int n = cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }

            con.Close();

            Response.Redirect("EditProfilePicture.aspx");
        }
    }
}