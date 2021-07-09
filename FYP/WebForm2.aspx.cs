using System;
using System.Collections.Generic;
using System.Globalization;
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
            string day = DateTime.Now.AddMinutes(-15).ToString("s");



            /*string[] formats = { "yyyyMMddTHHmmssZ", "yyyyMMddTHHmmss" };
            CultureInfo provider = CultureInfo.InvariantCulture;
            day = DateTime.ParseExact(day, formats, provider, DateTimeStyles.AssumeLocal).ToString();*/


            Label1.Text = DateTime.Parse(day).Subtract(DateTime.Now).TotalMinutes.ToString(); 

           
            //Label1.Text = TimeTableGenerator.weatherInfo.daily[i].weather[0].main;
        }
    }
}