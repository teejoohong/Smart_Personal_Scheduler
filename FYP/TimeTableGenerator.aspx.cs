using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    
    public partial class TimeTableGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IICalendarCollection calendars = iCalendar.LoadFromFile(timeTableFile.FileName);

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
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
                    

                    //use to read the ics file given by user 
                    ReadFile(ref datesStart,ref datesEnd);

                    string[,] dayDetails = new string[datesEnd.Count, 25];
                    string[] occurDays = new string[datesEnd.Count];
                    //Allocate all the used time as occupied space
                    CollationOfTime(ref dayDetails,ref occurDays, datesStart, datesEnd);

                    
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

        protected void ReadFile(ref Stack<DateTime> datesStart, ref Stack<DateTime> datesEnd)
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
                        }
                    }
                }
            }
        }

        protected void CollationOfTime(ref string[,] dayDetails,ref string[] occurDays,Stack<DateTime> datesStart, Stack<DateTime> datesEnd)
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
                
                if (occurDays.Contains(dateStart))
                {
                    for (int i = 0; i < occurDays.Length; i++)
                    {
                        if (dayDetails[i, 24].Equals(dateStart))
                        {
                            int start = int.Parse(DateTime.Parse(timeStart).ToString("HH:mm:ss").Split(':')[0]);
                            int end = int.Parse(DateTime.Parse(timeEnd).ToString("HH:mm:ss").Split(':')[0]);

                            string ts = (DateTime.Parse(timeEnd) - DateTime.Parse(timeStart)).TotalHours.ToString();
                            for (int j = 0; j < Math.Ceiling(double.Parse(ts)); j++)
                            {
                                dayDetails[i, start + j] = "1";

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

                            string ts = (DateTime.Parse(timeEnd) - DateTime.Parse(timeStart)).TotalHours.ToString();
                            for (int k = 0; k < Math.Ceiling(double.Parse(ts)); k++)
                            {
                                dayDetails[j, start + k] = "1";
                            }
                            //Label1.Text = dayDetails[0,0];                                      
                            break;
                        }

                    }
                }
                /*string all = "";

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
                }
                Label1.Text = all;*/
            }


        }
        protected void Timetable(string[,] dayDetails)
        {
                     
        }
    }
}