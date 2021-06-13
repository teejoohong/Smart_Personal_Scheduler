using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.ComponentModel;



namespace FYP
{
    public partial class InfoCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string latitude = "27.2046", longitude = "77.4977";
            //GetLocation(ref latitude,ref longitude);
            //http://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${key}
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&appid=f3f718fb3d54bf852baf842135e157c5");
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                Page.Response.Write("<script>console.log('" + json + "');</script>");
                Page.Response.Write("<script>console.log('" +"value = "+ hiddenLatitude.Value + "');</script>");
                
                WeatherInfo weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherInfo>(json);
                lblLocation.Text = weatherInfo.city.name + "," + weatherInfo.city.country;
                lblTemperatureDescription.Text = weatherInfo.list[0].weather[0].description;
                string minTemp = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.min, 1));
                string maxTemp = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.max, 1));
                lblTemperatureValue.Text = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.day, 1));
                string nightTempt = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.night, 1));
                string humidity = weatherInfo.list[0].humidity.ToString();*/
            }

        }
    /*
        [System.Security.SecurityCritical]
        public class GeoCoordinateWatcher : IDisposable, System.ComponentModel.INotifyPropertyChanged, System.Device.Location.IGeoPositionWatcher<System.Device.Location.GeoCoordinate>


        public class WeatherInfo
        {
            public City city { get; set; }
            public List<List> list { get; set; }
        }

        public class City
        {
            public string name { get; set; }
            public string country { get; set; }
        }

        public class Temp
        {
            public double day { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double night { get; set; }
        }

        public class Weather
        {
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class List
        {
            public Temp temp { get; set; }
            public int humidity { get; set; }
            public List<Weather> weather { get; set; }
        }

    }*/

}