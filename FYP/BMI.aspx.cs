using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class BMI : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            double w = double.Parse(weight.Text);
            double h = double.Parse(height.Text);
            h = h / 100;
            double result = w / (h * h);

            Label1.Text = result.ToString("0.00");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alertUser('result');", true);

        }
    }
}