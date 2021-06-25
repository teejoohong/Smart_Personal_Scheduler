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

            if (result < 18.5)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "swal('Underweight.You are not recommend to use training mode for your timetable.');", true);
            }
            else if (result >= 18.5 && result <25)
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "swal('Nice.You have a normal BMI value.Please keep it.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "swal('Overweight.You are recommend to use training mode on your timetable.');", true);
                
            }
           

        }
    }
}