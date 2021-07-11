using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["view"].Equals("0"))
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                DateTime timeSend = DateTime.Parse(Request.QueryString["time"]);
                if (DateTime.Now.Subtract(timeSend).TotalMinutes <= 15)
                {
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 2;
                }
               // if(timeSend.CompareTo(DateTime.Now))

            }
            
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);

            con.Open();
            string strSelect = "SELECT * FROM [User] WHERE Email = @Email";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@Email", txtEmail.Text); //supply value to @Username

            SqlDataReader dtr = cmdSelect.ExecuteReader();

            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    if (txtEmail.Text.Equals(dtr["Email"]))
                    {
                        if (dtr["Email"].ToString().Equals(txtEmail.Text))
                        {
                            //---cookie
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("testingg726@gmail.com");
                                mail.To.Add(txtEmail.Text);
                                mail.Subject = "EzBuddy reset password";
                                mail.Body = "Dear, " + dtr["UserID"].ToString() + "<br /><br /> Please click this link to reset your password: <br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                                    + HttpContext.Current.Request.Url.AbsoluteUri + "?view=1&?id=" + dtr["Name"].ToString() +"&?time=" + DateTime.Now.ToString("s") + " <br/>Thank you! <br /><br /> Best Regards, <br /> EzBuddy Team.";
                                mail.IsBodyHtml = true;

                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    smtp.Credentials = new NetworkCredential("testingg726@gmail.com", "abcd.1234");
                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                }
                            }

                            Response.Redirect("HomePage.aspx");

                        }                    
                    }
                }
            }
            con.Close();

            
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text.Equals(TextBox3.Text))
            {
                SqlConnection con;
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);

                con.Open();
                string strUpdate = "UPDATE User SET Password = @Password WHERE UserID = @UserID";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@UserID", Request.QueryString["id"]);
                cmdUpdate.Parameters.AddWithValue("@Password", TextBox2.Text);
                int numRowAffected = cmdUpdate.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Please insert the same password. " + "');", true);
            }   

        }
    }
}