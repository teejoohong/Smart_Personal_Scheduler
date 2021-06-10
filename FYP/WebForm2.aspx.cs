using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string day = "mon,tues";

            string[] testing = day.Split(',');

            Label1.Text = HttpContext.Current.Request.Url.AbsoluteUri;

            //Label1.Text = TimeTableGenerator.weatherInfo.daily[i].weather[0].main;
        }
    }
}