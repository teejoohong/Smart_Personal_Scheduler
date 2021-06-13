using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                if (Session["UserID"] != null) {
                    CustomerUsername.Text = Session["Username"].ToString();

                }
            }
        }


        protected void ltnButton1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = "0";
            Response.Redirect("LogIn.aspx");
        }
    }
}