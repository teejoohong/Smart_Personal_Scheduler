using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;

namespace FYP
{
    
    public partial class TimeTableGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IICalendarCollection calendars = iCalendar.LoadFromFile(timeTableFile.FileName);
            string latitude = "";
            string longitude = "";
            GetLocation(ref latitude, ref longitude);
            GetWeatherInfo();
        }

        protected void GenerationOfTimetable_Click(object sender, EventArgs e)
        {
            if (timeTableFile.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(timeTableFile.FileName);

                if (fileExtension.ToLower() != ".ics")
                {
                    //error message
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Wrong File " + "');", true);
                }
                else
                {
                    // initialization 
                    Stack<DateTime> datesStart = new Stack<DateTime>();
                    Stack<DateTime> datesEnd = new Stack<DateTime>();
                    Stack<string> recursion = new Stack<string>();

                    //use to read the ics file given by user 
                    ReadFile(ref datesStart,ref datesEnd, ref recursion);

                    string[,] dayDetails = new string[1000, 25];
                    string[] occurDays = new string[1000];
                    //Allocate all the used time as occupied space
                    CollationOfTime(ref dayDetails,ref occurDays, datesStart, datesEnd, recursion);

                    
                    Timetable(dayDetails);
                    //Label1.Text = datesStart.Count.ToString();
                    //Label2.Text = datesEnd.Count.ToString();
                }
            }
            
            else
            {
                //error message
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Please insert file first " + "');", true);
            }
        }

        protected void ReadFile(ref Stack<DateTime> datesStart, ref Stack<DateTime> datesEnd, ref Stack<string> recursion)
        {
            string inputContent;
            using (StreamReader inputStreamReader = new StreamReader(timeTableFile.PostedFile.InputStream))
            {
                inputContent = inputStreamReader.ReadToEnd();
                char[] delim = { '\n' };
                string[] lines = inputContent.Split(delim);
                delim[0] = ':';
                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("DTEND") || lines[i].Contains("DTSTART"))
                    {
                        
                        if (lines[i].Contains("DTSTART"))
                        {
                            string strDate = lines[i].Split(delim)[1].ToString();
                            strDate = strDate.Replace("\r", "");
                            DateTime result;
                            string[] formats = { "yyyyMMddTHHmmssZ", "yyyyMMddTHHmmss" };
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            result = DateTime.ParseExact(strDate, formats, provider, DateTimeStyles.AssumeLocal);
                            datesStart.Push(result);
                        }
                        else
                        {
                            string strDate = lines[i].Split(delim)[1].ToString();
                            strDate = strDate.Replace("\r", "");
                            DateTime result;
                            string[] formats = { "yyyyMMddTHHmmssZ", "yyyyMMddTHHmmss" };
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            result = DateTime.ParseExact(strDate, formats, provider, DateTimeStyles.AssumeLocal);
                            datesEnd.Push(result);
                            if (lines[i + 1].Contains("RRULE"))
                            {
                                //insert
                                string recursionData = "";
                                string[] recursionDataTemp = lines[i+1].Split(delim);
                                for(int j=1; j<recursionDataTemp.Length; j++)
                                {
                                    recursionData = recursionData + recursionDataTemp[j];
                                }
                                recursion.Push(recursionData);
                            }
                            else
                            {
                                //insert null
                                recursion.Push("0");
                            }
                        }
                    }
                }
            }
        }

        protected void CollationOfTime(ref string[,] dayDetails,ref string[] occurDays,Stack<DateTime> datesStart, Stack<DateTime> datesEnd, Stack<string> recursion)
        {
            
            while (datesEnd.Count != 0)
            {
                string dateDataEnd = datesEnd.Pop().ToString();
                string dateEnd = dateDataEnd.Split(' ')[0].ToString();
                string timeEnd = dateDataEnd.Split(' ')[1].ToString() + " " + dateDataEnd.Split(' ')[2].ToString();

                //round up the time
                if (int.Parse(timeEnd.Split(':')[1]) > 0)
                {
                    //int endTime = int.Parse(DateTime.Parse(timeEnd).ToString("HH:mm:ss").Split(':')[0]);
                    timeEnd = DateTime.Parse(timeEnd).AddHours(1).ToString();

                }

                string dateDataStart = datesStart.Pop().ToString();
                string dateStart = dateDataStart.Split(' ')[0].ToString();
                string timeStart = dateDataStart.Split(' ')[1].ToString().Split(':')[0] + ":00:00" + " " + dateDataStart.Split(' ')[2].ToString();
                //string ts = (DateTime.Parse(dateDataEnd) - DateTime.Parse(dateDataStart)).TotalHours.ToString();
                //string ts = ((DateTime.Parse(dateDataStart) - DateTime.Today).TotalDays.ToString());
                //Label1.Text = (int.Parse(DateTime.Parse(timeStart).ToString("HH:mm:ss").ToString().Split(':')[0]) - 1).ToString();
                //Label2.Text = dateDataEnd;

                string recursionData = recursion.Pop();
                
                if (occurDays.Contains(dateStart))
                {
                    for (int i = 0; i < occurDays.Length; i++)
                    {
                        if (dayDetails[i, 24].Equals(dateStart))
                        {
                            int start = int.Parse(DateTime.Parse(timeStart).ToString("HH:mm:ss").Split(':')[0]);
                            int end = int.Parse(DateTime.Parse(timeEnd).ToString("HH:mm:ss").Split(':')[0]);

                            string occupiedTime = (DateTime.Parse(timeEnd) - DateTime.Parse(timeStart)).TotalHours.ToString();
                            if (recursionData.Equals("0"))
                            {
                                for (int j = 0; j < Math.Ceiling(double.Parse(occupiedTime)); j++)
                                {
                                    dayDetails[i, start + j] = "1";

                                }
                            }
                            else
                            {
                                //recursive time
                                string[] recursionDataDetail = recursionData.Split(';');
                                if (recursionDataDetail.Contains("FREQ=WEEKLY"))
                                {
                                    recursiveWeekly(i, ref occurDays, ref dayDetails, recursionDataDetail, start, occupiedTime, dateStart);                            
                                }
                            }
                            
                            break;
                        }
                    }
                }
                else
                {
                    
                    for (int j = 0; j < occurDays.Length; j++)
                    {
                        if (occurDays[j] == null)
                        {
                            occurDays[j] = dateStart;
                            dayDetails[j, 24] = dateStart;

                            int start = int.Parse(DateTime.Parse(timeStart).ToString("HH:mm:ss").Split(':')[0]);

                            string occupiedTime = (DateTime.Parse(timeEnd) - DateTime.Parse(timeStart)).TotalHours.ToString();
                            
                            if (recursionData.Equals("0")) { 
                                for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                                {
                                    dayDetails[j, start + k] = "1";
                                }
                            }
                            else
                            {
                                //recursive time
                                string[] recursionDataDetail = recursionData.Split(';');
                                
                                if (recursionDataDetail.Contains("FREQ=WEEKLY"))
                                {
                                    recursiveWeekly(j, ref occurDays, ref dayDetails, recursionDataDetail, start, occupiedTime, dateStart);
                                }
                            }
                            //Label1.Text = dayDetails[0,0];                                      
                            break;
                        }

                    }
                }
                string all = "";

                for (int i = 0; i < occurDays.Length; i++)
                {
                    if (dayDetails[i, 24] != null)
                    {

                        for (int j = 0; j < 25; j++)
                        {
                            if (dayDetails[i, j] != null)
                            {
                                all = all + dayDetails[i, j] + " ";
                            }
                            else
                            {
                                all = all + " 0 ";
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    all = all + "\n\\n";
                }
                Label1.Text = all;
            }


        }

        protected void recursiveWeekly(int i, ref string[] occurDays, ref string[,] dayDetails, string[] recursionDataDetail, int start, string occupiedTime, string dateStart)
        {
            string count = "";
            string dateUntil = "";
            //check for total count recursive
            
            for (int j = 0; j < recursionDataDetail.Length; j++)
            {
                if (recursionDataDetail[j].Contains("COUNT"))
                {
                    count = recursionDataDetail[j].Split('=')[1];
                    break;
                }
                else if(recursionDataDetail[j].Contains("UNTIL")){

                    dateUntil = recursionDataDetail[j].Split('=')[1];
                    dateUntil = dateUntil.Replace("\r", "");
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string[] formats = { "yyyyMMddTHHmmssZ", "yyyyMMddTHHmmss" };
                    dateUntil = DateTime.ParseExact(dateUntil, formats, provider, DateTimeStyles.AssumeLocal).ToString().Split(' ')[0];
                    break;
                    
                }
            }
            
            if (count!="")
            {
                string nextDateStart = "";
                // insert time for loop
                for (int j = 0; j < int.Parse(count); j++)
                {
                   
                    if (j == 0)
                    {
                        nextDateStart = dateStart;
                    }
                    else
                    {
                        nextDateStart = DateTime.Parse(nextDateStart).AddDays(7).ToString("d");
                    }
                    
                    if (occurDays.Contains(nextDateStart))
                    {
                        for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                        {
                            dayDetails[i, start + k] = "1";
                        }
                    }
                    else
                    {   
                        for (int k = 0; k < occurDays.Length; k++)
                        {
                            if (occurDays[k] == null)
                            {
                                occurDays[k] = nextDateStart;
                                dayDetails[k, 24] = nextDateStart;
                                
                                for (int l = 0; l < Math.Ceiling(double.Parse(occupiedTime)); l++)
                                {
                                    dayDetails[k, start + l] = "1";
                                }
                                break;
                            }
                        }
                    }
                }
            }
            else if (dateUntil!="")
            {

                //check for recursive until what date
                string nextDateStart = "";
                double loopTime = (DateTime.Parse(dateUntil) - DateTime.Parse(dateStart)).TotalDays;
                
                loopTime = Math.Floor(loopTime / 7);

                if(loopTime == 0)
                {
                    for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                    {
                        dayDetails[i, start + k] = "1";

                    }
                }
                //insert occupied time for loop
                for (int j = 0; j < loopTime; j++)
                {
                    
                    if (j == 0)
                    {
                        nextDateStart = dateStart;
                    }
                    else
                    {
                        nextDateStart = DateTime.Parse(nextDateStart).AddDays(7).ToString("d");
                    }

                    if (occurDays.Contains(nextDateStart))
                    {
                        for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                        {
                            dayDetails[i, start + k] = "1";

                        }
                    }
                    else
                    {
                        for (int k = 0; k < occurDays.Length; k++)
                        {
                            if (occurDays[k] == null)
                            {
                                occurDays[k] = nextDateStart;
                                dayDetails[k, 24] = nextDateStart;
                                for (int l = 0; l < Math.Ceiling(double.Parse(occupiedTime)); l++)
                                {
                                    dayDetails[k, start + l] = "1";
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void Timetable(string[,] dayDetails)
        {
                     
        }

        protected void GetLocation(ref string latitude, ref string longitude)
        {
            
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            string APIKey = "a4f70f75711c87fd41f368b493ebaa38c8f3d532122fe3887fdb5efc6c55585a";
            string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, "124.13.82.236");//ipAddress
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                List<Location> locations = new List<Location>();
                locations.Add(location);
                latitude = location.Latitude.ToString();
                longitude = location.Longitude.ToString();
            }
        }
        protected void GetWeatherInfo()
        {
            string appId = "016a11fa3fe4510a79916761e9219c33";
            string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude={2}&units=metric&appid={3}", "3.073281", "101.518463", "current,minutely,hourly,alerts",appId); // lat, long
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                WeatherInfo weatherInfo = new JavaScriptSerializer().Deserialize<WeatherInfo>(json);
                List<WeatherInfo> weatherInfos = new List<WeatherInfo>();
                weatherInfos.Add(weatherInfo);
                //string weather = weatherInfo.city.name;
               // Label1.Text = weather;
               

            }
        }

        public class Location
        {
            public string IPAddress { get; set; }
            public string CountryName { get; set; }
            public string CountryCode { get; set; }
            public string CityName { get; set; }
            public string RegionName { get; set; }
            public string ZipCode { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string TimeZone { get; set; }
        }

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
            public string main { get; set; }
        }

        public class List
        {
            public Temp temp { get; set; }
            public int humidity { get; set; }
            public List<Weather> weather { get; set; }
        }
    }
}