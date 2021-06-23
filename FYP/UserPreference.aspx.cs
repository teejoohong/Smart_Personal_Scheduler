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
    public partial class UserPreference : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);
            con.Open();
            string strSelect = "Select * From IndoorPreference Where UserID = @UserID";
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@UserID", Session["User"]);
            SqlDataReader dtr = cmdSelect.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    //dtr["Activity_1"].ToString()
                    if(dtr["Activity_1"]!= null)
                        DropDownList4.SelectedValue = dtr["Activity_1"].ToString();
                    if (dtr["Activity_2"] != null)
                        DropDownList5.SelectedValue = dtr["Activity_2"].ToString();
                    if (dtr["Activity_3"] != null)
                        DropDownList6.SelectedValue = dtr["Activity_3"].ToString();
                }
            }
            con.Close();

            con.Open();
            string strSelect1 = "Select * From OutdoorPreference Where UserID = @UserID1";
            SqlCommand cmdSelect1 = new SqlCommand(strSelect1, con);
            cmdSelect.Parameters.AddWithValue("@UserID1", Session["User"]);
            SqlDataReader dtr1 = cmdSelect1.ExecuteReader();
            if (dtr1.HasRows)
            {
                while (dtr1.Read())
                {
                    //dtr["Activity_1"].ToString()
                    if (dtr1["Activity_1"] != null)
                        DropDownList1.SelectedValue = dtr1["Activity_1"].ToString();
                    if (dtr1["Activity_2"] != null)
                        DropDownList2.SelectedValue = dtr1["Activity_2"].ToString();
                    if (dtr1["Activity_3"] != null)
                        DropDownList3.SelectedValue = dtr1["Activity_3"].ToString();
                }
            }
            con.Close();

        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        protected void save_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);
            con.Open();
            string strSelect = "Select * From IndoorPreference Where UserID = @UserID";
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@UserID", Session["User"]);
            SqlDataReader dtr = cmdSelect.ExecuteReader();
            if (dtr.HasRows)
            {
                SqlConnection conn;
                string strconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn = new SqlConnection(strconn);
                conn.Open();

                string strDelete = "Delete * From IndoorPreference Where UserID = @UserID";
                SqlCommand cmdDelete = new SqlCommand(strDelete, conn);
                cmdDelete.Parameters.AddWithValue("@UserID", Session["User"]);
                int numRowAffected = cmdDelete.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                string strDelete1 = "Delete * From OutdoorPreference Where UserID = @UserID1";
                SqlCommand cmdDelete1 = new SqlCommand(strDelete1, conn);
                cmdDelete1.Parameters.AddWithValue("@UserID1", Session["User"]);
                int numRowAffected1 = cmdDelete1.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                string strInsert = "Insert into IndoorPreference (Activity_1, Activity_2, Activity_3, UserID) Values (@Activity_1, @Activity_2, @Activity_3, @UserID2)";
                SqlCommand cmdInsert = new SqlCommand(strInsert, conn);
                cmdInsert.Parameters.AddWithValue("@UserID2", Session["Value"]);
                cmdInsert.Parameters.AddWithValue("@Activity_1", DropDownList4.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@Activity_2", DropDownList5.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@Activity_3", DropDownList6.SelectedValue);
                int numRowAffected2 = cmdInsert.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                string strInsert1 = "Insert into OutdoorPreference (Activity_1, Activity_2, Activity_3, UserID) Values (@Activity_1, @Activity_2, @Activity_3, @UserID3)";
                SqlCommand cmdInsert1 = new SqlCommand(strInsert1, conn);
                cmdInsert1.Parameters.AddWithValue("@UserID3", Session["Value"]);
                cmdInsert1.Parameters.AddWithValue("@Activity_1", DropDownList1.SelectedValue);
                cmdInsert1.Parameters.AddWithValue("@Activity_2", DropDownList2.SelectedValue);
                cmdInsert1.Parameters.AddWithValue("@Activity_3", DropDownList3.SelectedValue);
                int numRowAffected3 = cmdInsert1.ExecuteNonQuery();
                conn.Close();

            }
            else
            {
                SqlConnection conn;
                string strconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn = new SqlConnection(strconn);

                conn.Open();
                string strInsert = "Insert into IndoorPreference (Activity_1, Activity_2, Activity_3, UserID) Values (@Activity_1, @Activity_2, @Activity_3, @UserID)";
                SqlCommand cmdInsert = new SqlCommand(strInsert, conn);
                cmdInsert.Parameters.AddWithValue("@UserID", Session["Value"]);
                cmdInsert.Parameters.AddWithValue("@Activity_1", DropDownList4.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@Activity_2", DropDownList5.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@Activity_3", DropDownList6.SelectedValue);
                int numRowAffected = cmdInsert.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                string strInsert1 = "Insert into OutdoorPreference (Activity_1, Activity_2, Activity_3, UserID) Values (@Activity_1, @Activity_2, @Activity_3, @UserID1)";
                SqlCommand cmdInsert1 = new SqlCommand(strInsert1, conn);
                cmdInsert1.Parameters.AddWithValue("@UserID1", Session["Value"]);
                cmdInsert1.Parameters.AddWithValue("@Activity_1", DropDownList1.SelectedValue);
                cmdInsert1.Parameters.AddWithValue("@Activity_2", DropDownList2.SelectedValue);
                cmdInsert1.Parameters.AddWithValue("@Activity_3", DropDownList3.SelectedValue);
                int numRowAffected1 = cmdInsert1.ExecuteNonQuery();
                conn.Close();

            }
            con.Close();
            
        }
    }
}