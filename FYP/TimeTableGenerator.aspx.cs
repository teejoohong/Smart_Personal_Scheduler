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
using System.Data.SqlClient;
using System.Configuration;

namespace FYP
{
    
    public partial class TimeTableGenerator : System.Web.UI.Page
    {//activity
        string[] weatherWeeklyForecast = new string[8];
        protected void Page_Load(object sender, EventArgs e)
        {
                               
            
        }

        protected void GenerationOfTimetable_Click(object sender, EventArgs e)
        {
            string latitude = HiddenField1.Value;
            string longitude = HiddenField2.Value;

            GetWeatherInfo(latitude, longitude);

            string[,] dayDetails = new string[1000, 25];

            if (modeGeneration.SelectedItem == null)
            {
                //javascript error message
            }
            else
            {
                if(FileUploading.Checked == true)
                {
                    // with ics file
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
                               
                            Stack<DateTime> datesStart = new Stack<DateTime>();
                            Stack<DateTime> datesEnd = new Stack<DateTime>();
                            Stack<string> recursion = new Stack<string>();

                            //use to read the ics file given by user 
                            ReadFile(ref datesStart, ref datesEnd, ref recursion);
                           
                            string[] occurDays = new string[1000];


                            //Allocate all the used time as occupied space
                            CollationOfTime(ref dayDetails, ref occurDays, datesStart, datesEnd, recursion);
                            string all = "";
                            for(int i =0; i<1000; i++)
                            {
                                for(int j=0; j<25; j++)
                                {
                                    all = all + dayDetails[i, j] + " ";
                                }
                            }
                            Label1.Text = all;
                            //Label1.Text = dayDetails[0, 24];
                            Stack<Stack<string[]>> timeTablesWeekly = Timetable(dayDetails, modeGeneration.SelectedValue);


                            FileUpload(timeTablesWeekly);
                                
                        }
                    }
                }
                else
                {
                    //without ics file
                    

                    Stack<Stack<string[]>> timeTablesWeekly = Timetable(dayDetails, modeGeneration.SelectedValue);
                    FileUpload(timeTablesWeekly);
                }
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
                string timeEnd = dateDataEnd.Split(' ')[1].ToString() + " " + dateDataEnd.Split(' ')[2].ToString();

                //round up the time
                if (int.Parse(timeEnd.Split(':')[1]) > 0)
                {
                    
                    timeEnd = DateTime.Parse(timeEnd).AddHours(1).ToString();

                }

                string dateDataStart = datesStart.Pop().ToString();
                string dateStart = dateDataStart.Split(' ')[0].ToString();
                string timeStart = dateDataStart.Split(' ')[1].ToString().Split(':')[0] + ":00:00" + " " + dateDataStart.Split(' ')[2].ToString();
                

                string recursionData = recursion.Pop();
                
                if (occurDays.Contains(dateStart))
                {
                    for (int i = 0; i < occurDays.Length; i++)
                    {
                        if (dayDetails[i, 24].Equals(dateStart))
                        {
                            int start = int.Parse(DateTime.Parse(timeStart).ToString("HH:mm:ss").Split(':')[0]);
                           
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
                                }else if (recursionDataDetail.Contains("FREQ=DAILY"))
                                {
                                    recursiveDaily(ref occurDays, ref dayDetails, recursionDataDetail, start, occupiedTime, dateStart);
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
                                else if (recursionDataDetail.Contains("FREQ=DAILY"))
                                {
                                    recursiveDaily(ref occurDays, ref dayDetails, recursionDataDetail, start, occupiedTime, dateStart);
                                }
                            }
                                                                
                            break;
                        }

                    }
                }
               
                for (int i = 0; i < occurDays.Length; i++)
                {
                    if (dayDetails[i, 24] != null)
                    {

                        for (int j = 0; j < 25; j++)
                        {
                            if (dayDetails[i, j] != null)
                            {
                                continue;
                            }
                            else
                            {
                                dayDetails[i, j] = "0";
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                    
                }
               
            }


        }

        protected void recursiveDaily( ref string[] occurDays, ref string[,] dayDetails, string[] recursionDataDetail, int start, string occupiedTime, string dateStart)
        {

            string[] byday = new string[7];

            for (int o = 0; o < recursionDataDetail.Length; o++)
            {
                if (recursionDataDetail[o].Contains("BYDAY"))
                {
                    byday = recursionDataDetail[o].Split('=')[1].Split(',');
                    break;
                }
            }

            for (int o = 0; o < byday.Length; o++)
            {
                byday[o] = byday[o].Replace("\r", "");
            }

            for (int o = 0; o < byday.Length; o++)
            {
                DateTime nextDateStart = new DateTime();
                if (byday[o] == null)
                {
                    break;
                }
                else
                {
                    nextDateStart = DateTime.Today;
                    if (byday[o].Equals("SU"))
                    {

                        while (!nextDateStart.ToString("dddd").Equals("Sunday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                    }
                    else if (byday[o].Equals("MO"))
                    {
                        while (!nextDateStart.ToString("dddd").Equals("Monday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                    }
                    else if (byday[o].Equals("TU"))
                    {

                        while (!nextDateStart.ToString("dddd").Equals("Tuesday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                    }
                    else if (byday[o].Equals("WE"))
                    {
                        while (!nextDateStart.ToString("dddd").Equals("Wednesday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                    }
                    else if (byday[o].Equals("TH"))
                    {
                        while (!nextDateStart.ToString("dddd").Equals("Thursday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                    }
                    else if (byday[o].Equals("FR"))
                    {
                        while (!nextDateStart.ToString("dddd").Equals("Friday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                    }
                    else
                    {
                        while (!nextDateStart.ToString("dddd").Equals("Saturday"))
                        {
                            nextDateStart = nextDateStart.AddDays(1);
                        }
                        countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                    }
                }
            }

        }

        protected void recursiveWeekly(int i, ref string[] occurDays, ref string[,] dayDetails, string[] recursionDataDetail, int start, string occupiedTime, string dateStart)
        {
            string count = "";
            string dateUntil = "";
            string[] byday = new string[7];
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
                
                string nextDateStartTemp = "";

                //calculate for maximum date until
                for (int o =0; o < int.Parse(count); o++)
                {

                    if (o == 0)
                    {
                        nextDateStartTemp = dateStart;
                    }
                    else
                    {
                        nextDateStartTemp = DateTime.Parse(nextDateStartTemp).AddDays(7).ToString("d");
                    }
                }

                if((DateTime.Parse(nextDateStartTemp) - DateTime.Today).TotalDays >= 0)
                {
                    // insert time for loop
                    for(int o = 0; o < recursionDataDetail.Length; o++)
                    {
                        if (recursionDataDetail[o].Contains("BYDAY"))
                        {
                            byday = recursionDataDetail[o].Split('=')[1].Split(',');
                            break;
                        }
                    }

                    for (int o = 0; o < byday.Length; o++)
                    {
                        byday[o] = byday[o].Replace("\r", "");
                    }

                    for (int o=0; o < byday.Length; o++)
                    {
                        DateTime nextDateStart = new DateTime();

                        if (byday[o] == null)
                        {
                            break;
                        }
                        else
                        {
                            nextDateStart = DateTime.Today;
                            if (byday[o].Equals("SU"))
                            {
                                
                                while (!nextDateStart.ToString("dddd").Equals("Sunday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);       
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);                             

                            }
                            else if (byday[o].Equals("MO"))
                            {
                                while (!nextDateStart.ToString("dddd").Equals("Monday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                            }
                            else if (byday[o].Equals("TU"))
                            {
                               
                                while (!nextDateStart.ToString("dddd").Equals("Tuesday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            }
                            else if (byday[o].Equals("WE"))
                            {
                                while (!nextDateStart.ToString("dddd").Equals("Wednesday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            }
                            else if (byday[o].Equals("TH"))
                            {
                                while (!nextDateStart.ToString("dddd").Equals("Thursday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            }
                            else if (byday[o].Equals("FR"))
                            {
                                while (!nextDateStart.ToString("dddd").Equals("Friday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            }                          
                            else
                            {
                                while (!nextDateStart.ToString("dddd").Equals("Saturday"))
                                {
                                    nextDateStart = nextDateStart.AddDays(1);
                                }
                                countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            }
                        }
                    }
                }
                
            }
            else if (dateUntil!="")
            {

                //check for recursive until what date
               
                string nextDateStartTemp = "";
                double loopTime = (DateTime.Parse(dateUntil) - DateTime.Parse(dateStart)).TotalDays;
                
                loopTime = Math.Floor(loopTime / 7);
                
                //0 equal to less than a week
                if (loopTime == 0)
                {
                    for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                    {
                        dayDetails[i, start + k] = "1";

                    }
                }

                //get the maximum date until
                for(int j = 0; j<loopTime; j++)
                {
                    if (j == 0)
                    {
                        nextDateStartTemp = dateStart;
                    }
                    else
                    {
                        nextDateStartTemp = DateTime.Parse(nextDateStartTemp).AddDays(7).ToString("d");
                    }
                }
                

                if ((DateTime.Parse(nextDateStartTemp) - DateTime.Today).TotalDays >= 0)
                {
                    // insert time for loop
                    for (int o = 0; o < recursionDataDetail.Length; o++)
                    {
                        if (recursionDataDetail[o].Contains("BYDAY"))
                        {
                            
                            byday = recursionDataDetail[o].Split('=')[1].Split(',');                          
                            break;
                        }
                    }

                    for(int o = 0; o < byday.Length; o++)
                    {
                        byday[o] = byday[o].Replace("\r", "");
                    }
                   
                    for (int o = 0; o < byday.Length; o++)
                    {
                        
                        DateTime nextDateStart = new DateTime();

                        
                        nextDateStart = DateTime.Today;
                        
                        if (byday[o].Equals("SU"))
                        {

                            while (!nextDateStart.ToString("dddd").Equals("Sunday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                        }
                        else if (byday[o].Equals("MO"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Monday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                        }
                        else if (byday[o].Equals("TU"))
                        {
                            
                            while (!nextDateStart.ToString("dddd").Equals("Tuesday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                           
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                            
                        }
                        else if (byday[o].Equals("WE"))
                        {                         
                            while (!nextDateStart.ToString("dddd").Equals("Wednesday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else if (byday[o].Equals("TH"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Thursday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else if (byday[o].Equals("FR"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Friday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else
                        {
                            
                            while (!nextDateStart.ToString("dddd").Equals("Saturday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        
                    }
                }

            }
            else
            {
                    
            // insert time for loop
                for (int o = 0; o < recursionDataDetail.Length; o++)
                {
                    if (recursionDataDetail[o].Contains("BYDAY"))
                    {
                        byday = recursionDataDetail[o].Split('=')[1].Split(',');                      
                        break;
                    }
                }

                for (int o = 0; o < byday.Length; o++)
                {
                    byday[o] = byday[o].Replace("\r", "");
                }

                for (int o = 0; o < byday.Length; o++)
                {
                    DateTime nextDateStart = new DateTime();
                    if (byday[o] == null)
                    {
                        break;
                    }
                    else
                    {
                        nextDateStart = DateTime.Today;
                        if (byday[o].Equals("SU"))
                        {

                            while (!nextDateStart.ToString("dddd").Equals("Sunday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                        }
                        else if (byday[o].Equals("MO"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Monday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);

                        }
                        else if (byday[o].Equals("TU"))
                        {
                            
                            while (!nextDateStart.ToString("dddd").Equals("Tuesday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else if (byday[o].Equals("WE"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Wednesday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else if (byday[o].Equals("TH"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Thursday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else if (byday[o].Equals("FR"))
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Friday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                        else
                        {
                            while (!nextDateStart.ToString("dddd").Equals("Saturday"))
                            {
                                nextDateStart = nextDateStart.AddDays(1);
                            }
                            countWeekly(ref occurDays, ref dayDetails, nextDateStart, start, occupiedTime);
                        }
                    }
                }
                
            }
        }

        protected void countWeekly(ref string[] occurDays, ref string[,] dayDetails, DateTime nextDateStart,int start, string occupiedTime)
        {
            int reference = 0;
            bool contain = false;
            for(int k = 0; k < occurDays.Length; k++)
            {
                if(occurDays[k] != null)
                {
                    if (occurDays[k].Equals(nextDateStart.ToString("d")))
                    {
                        reference = k;
                        contain = true;
                    }
                }
                
            }


            if (contain)
            {
                for (int k = 0; k < Math.Ceiling(double.Parse(occupiedTime)); k++)
                {
                    dayDetails[reference, start + k] = "1";
                }
            }
            else
            {
                for (int k = 0; k < occurDays.Length; k++)
                {
                    if (occurDays[k] == null)
                    {
                        occurDays[k] = nextDateStart.ToString("d");
                        dayDetails[k, 24] = nextDateStart.ToString("d");

                        for (int l = 0; l < Math.Ceiling(double.Parse(occupiedTime)); l++)
                        {
                            dayDetails[k, start + l] = "1";
                        }
                        break;
                    }
                }
            }
        }

        protected Stack<Stack<string[]>> Timetable(string[,] dayDetails, string selectedMode)
        {
            string date = DateTime.Today.ToString("d");
            Stack<Stack<string[]>> timeTablesWeekly = new Stack<Stack<string[]>>();

            string[] randomMode = new string[7]{ "a", "a","a","a","b","b","c"};
            List<string> sunnyActivity = new List<string>();
            Queue<string> rainnyActivity = new Queue<string>();

            Random r = new Random();

            randomMode = randomMode.OrderBy(x => r.Next()).ToArray();

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
                    rainnyActivity.Enqueue( dtr["Activity_1"].ToString());
                    rainnyActivity.Enqueue(dtr["Activity_2"].ToString());
                    rainnyActivity.Enqueue(dtr["Activity_3"].ToString());
                    sunnyActivity.Add(dtr["Activity_1"].ToString());
                    sunnyActivity.Add(dtr["Activity_2"].ToString());
                    sunnyActivity.Add(dtr["Activity_3"].ToString());
                }
            }
            con.Close();

            con.Open();
            string strSelect1 = "Select * From OutdoorPreference Where UserID = @UserID1";
            SqlCommand cmdSelect1 = new SqlCommand(strSelect1, con);
            cmdSelect1.Parameters.AddWithValue("@UserID1", Session["User"]);
            SqlDataReader dtr1 = cmdSelect1.ExecuteReader();
            if (dtr1.HasRows)
            {
                while (dtr1.Read())
                {
                    //dtr["Activity_1"].ToString()
                    sunnyActivity.Add(dtr1["Activity_1"].ToString());
                    sunnyActivity.Add(dtr1["Activity_2"].ToString());
                    sunnyActivity.Add(dtr1["Activity_3"].ToString());
                }
            }
            con.Close();

            string[] indoor = new string[6];
            string[] outdoor = new string[12];

            for (int i = 0; i < 7; i++)
            {
                if(i != 0)
                {
                    date = DateTime.Parse(date).AddDays(1).ToString();
                }


                
                int referenceDate = new int();
                bool containedDate = false;
                
                for (int j = 0; j < 1000; j++)
                {
                    if(dayDetails[j, 24] == null)
                    {
                        
                        break;
                    }
                    else
                    {
                        if (dayDetails[j, 24].Equals(date.Split(' ')[0]))
                        {
                            
                            containedDate = true;
                            
                            referenceDate = j;
                            break;
                        }
                    }
                   
                }
                
                if (weatherWeeklyForecast[i].Equals("clear sky")|| weatherWeeklyForecast[i].Equals("few clouds") || weatherWeeklyForecast[i].Equals("scattered clouds") || weatherWeeklyForecast[i].Equals("broken clouds"))
                {
                    if (selectedMode.Equals("Study Mode"))
                    {
                        if (randomMode[i].Equals("a"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }else if (randomMode[i].Equals("b"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));
                        }
                        
                    }
                    else if (selectedMode.Equals("Training Mode"))
                    {
                        if (randomMode[i].Equals("a"))
                        {                            
                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));
                        }
                        else if (randomMode[i].Equals("b"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));
                        }
                    }
                    else
                    {
                        if (randomMode[i].Equals("a"))
                        {

                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));
                            
                        }
                        else if (randomMode[i].Equals("b"))
                        {

                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));                         
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }
                    }
                }
                else
                {
                    if (selectedMode.Equals("Study Mode"))
                    {
                        if (randomMode[i].Equals("a"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }
                        else if (randomMode[i].Equals("b"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));
                        }

                    }
                    else if (selectedMode.Equals("Training Mode"))
                    {
                        if (randomMode[i].Equals("a"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));
                        }
                        else if (randomMode[i].Equals("b"))
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));
                        }
                    }
                    else
                    {
                        if (randomMode[i].Equals("a"))
                        {

                            if (containedDate)
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(RelaxMode(date, ref dayDetails));

                        }
                        else if (randomMode[i].Equals("b"))
                        {

                            if (containedDate)
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(TrainingMode(date, ref dayDetails));
                        }
                        else
                        {
                            if (containedDate)
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails, referenceDate));
                            else
                                timeTablesWeekly.Push(StudyMode(date, ref dayDetails));
                        }
                    }
                }
            }
            return timeTablesWeekly;
        }

        protected void GetWeatherInfo(string latitude, string longitude)
        {
            string appId = "016a11fa3fe4510a79916761e9219c33";
            string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude={2}&units=metric&appid={3}", latitude, longitude, "current,minutely,hourly,alerts",appId); // lat, long
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                WeatherInfo weatherInfo = new JavaScriptSerializer().Deserialize<WeatherInfo>(json);
                List<WeatherInfo> weatherInfos = new List<WeatherInfo>();
                weatherInfos.Add(weatherInfo);

                for(int i = 0; i<8; i++)
                {
                    weatherWeeklyForecast[i]= weatherInfo.daily[i].weather[0].main;
                }   
            }
        }

        protected void FileUpload(Stack<Stack<string[]>> timeTablesWeekly)
        {
            DateTime DateStart = DateTime.Now;
           
            string FileName = "CalendarItem";

            //create a new stringbuilder instance
            string sb = "";

            //start the calendar item
            sb = sb +"BEGIN:VCALENDAR" + Environment.NewLine;
            sb += "VERSION:2.0" + Environment.NewLine;
            sb += "PRODID:stackoverflow.com" + Environment.NewLine;
            sb += "CALSCALE:GREGORIAN" + Environment.NewLine;
            sb += "METHOD:PUBLISH" + Environment.NewLine;

            //create a time zone if needed, TZID to be used in the event itself
            sb += "BEGIN:VTIMEZONE" + Environment.NewLine;
            sb += "TZID:Asia/Kuala_Lumpur" + Environment.NewLine;
            sb += "BEGIN:STANDARD" + Environment.NewLine;
            sb += "TZOFFSETTO:+0800" + Environment.NewLine;
            sb += "TZOFFSETFROM:+0800" + Environment.NewLine;
            sb += "END:STANDARD" + Environment.NewLine;
            sb += "END:VTIMEZONE" + Environment.NewLine;

            //add the event
            while (timeTablesWeekly.Count != 0)
            {
                Stack<string[]> timeTablesDetail = timeTablesWeekly.Pop();

                while (timeTablesDetail.Count != 0)
                {
                    string[] timetableDeatils = timeTablesDetail.Pop();

                    sb += "BEGIN:VEVENT" + Environment.NewLine;
                    //for (int j = 0; j < timetableDeatils.Length; j++)
                    //{
                        sb += "DTSTAMP:" + timetableDeatils[0] + Environment.NewLine;
                        sb += "DTSTART;TZID=Asia/Kuala_Lumpur:" + timetableDeatils[1] + Environment.NewLine;
                        sb += "DTEND;TZID=Asia/Kuala_Lumpur:" + timetableDeatils[2] + Environment.NewLine;
                        sb += "UID:" + timetableDeatils[3] + Environment.NewLine;
                        sb += "SUMMARY:" + timetableDeatils[4] + Environment.NewLine;
                        sb += "DESCRIPTION:" + timetableDeatils[5] + Environment.NewLine;
                    sb += "END:VEVENT" + Environment.NewLine;
                    

                }

                

                //with time zone specified
                //sb.AppendLine("DTSTART;TZID=Europe/Amsterdam:" + DateStart.ToString("yyyyMMddTHHmm00"));
                //sb.AppendLine("DTEND;TZID=Europe/Amsterdam:" + DateEnd.ToString("yyyyMMddTHHmm00"));
                //or without
                

                /*sb += "SUMMARY:" + Summary + Environment.NewLine;
                sb += "LOCATION:" + Location + Environment.NewLine;
                sb += "DESCRIPTION:" + Description + Environment.NewLine;
                sb += "PRIORITY:3" + Environment.NewLine;*/
                
                //end calendar item
               
            }
            sb += "END:VCALENDAR" + Environment.NewLine;
            //create a string from the stringbuilder
            string CalendarItem = sb;
           
            //send the calendar item to the browser
/*            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/calendar";
            Response.AddHeader("content-length", CalendarItem.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + ".ics\"");
            Response.Write(CalendarItem);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();*/

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-length", CalendarItem.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + ".ics\"");
            Response.ContentType = "text/calendar";
            Response.Write(CalendarItem);
            Response.Flush();
            //Response.TransmitFile(file.FullName);
            Response.End();
        }

        private Stack<string[]> StudyMode(string date,ref string[,] dayDetails, int referenceDate)
        {
            int studyTime = 4;
            int exercise = 1;
            int houseChore = 1;
            int lunch = 1, dinner = 1;
            Stack<string[]> timeTables = new Stack<string[]>();

            for (int i = 0; i < 17; i++)
            {
                if(i == 0)
                {
                    /*string[] timeTable = new string[5];
                    timeTable[0] = DateTime.Parse(date).ToString("yyyyMMddTHH0000Z"); // DStamp
                    timeTable[1] = DateTime.Parse(date).AddHours(7).ToString("yyyyMMddTHH0000Z"); // DStart
                    timeTable[2] = DateTime.Parse(date).AddHours(8).ToString("yyyyMMddTHH0000Z"); //DEnd
                    timeTable[3] = Guid.NewGuid().ToString() + DateTime.Parse(date).ToString("yyyyMMddTHHmm00Z"); //UID
                    timeTable[4] = "Breakfast"; //Summary*/
                    timeTables.Push(ScheduleTimeTable(7,8,"breakfast",date));
                    dayDetails[referenceDate, 7] = "1";
                    //timeTables.Push(timeTable);
                }
                else if(i == 1){
                    if(dayDetails[referenceDate, 8].Equals("0"))
                    {                     
                        timeTables.Push(ScheduleTimeTable(8, 9, "breakfast", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 8] = "1";
                    }
                }else if(i == 2)
                {
                    if (dayDetails[referenceDate, 9].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(9, 10, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 9] = "1";
                        
                    }
                }else if(i == 3)
                {
                    if (dayDetails[referenceDate, 10].Equals("0") && studyTime > 2)
                    {
                       
                        timeTables.Push(ScheduleTimeTable(10, 11, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 10] = "1";
                        
                    }
                }else if(i == 4)
                {
                    if(dayDetails[referenceDate, 11].Equals("0"))
                    {
                        if(dayDetails[referenceDate,11].Equals("0") && studyTime > 2)
                        {
                            timeTables.Push(ScheduleTimeTable(11, 12, "Study", date));
                            studyTime = studyTime - 1;
                            dayDetails[referenceDate, 11] = "1";
                        }
                    }
                }else if(i == 5)
                {
                    if(dayDetails[referenceDate,12].Equals("1") && dayDetails[referenceDate, 13].Equals("1"))
                    {
                        timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 12] = "1";
                    }
                    else if(dayDetails[referenceDate, 12].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 12] = "1";
                    }
                }else if( i == 6)
                {
                    if (dayDetails[referenceDate, 13].Equals("0") && lunch == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                    else if (dayDetails[referenceDate, 13].Equals("0") && lunch == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                }else if( i == 7)
                {
                    if (dayDetails[referenceDate, 14].Equals("0")&& houseChore == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }
                    else if(dayDetails[referenceDate, 14].Equals("0") && studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(14, 15, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }
                }
                else if(i == 8)
                {   
                    if (dayDetails[referenceDate, 15].Equals("0") && houseChore == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(15, 16, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 15] = "1";
                    }
                    else if (dayDetails[referenceDate, 15].Equals("0") && studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(15, 16, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 15] = "1";
                    }
                    
                }
                else if (i == 9)
                {
                    if(dayDetails[referenceDate,16].Equals("0")&& dayDetails[referenceDate, 17].Equals("0"))
                    {
                        // bath + exercise at 16-17
                        timeTables.Push(ScheduleTimeTable(16, 17, "Activity", date));
                        exercise = exercise - 1;
                        dayDetails[referenceDate, 16] = "1";
                    }
                    
                }else if(i == 10)
                {
                    if (dayDetails[referenceDate, 17].Equals("0") && exercise == 0)
                    {
                        // bath + exercise at 16-17
                        timeTables.Push(ScheduleTimeTable(16, 17, "Bath", date));                      
                        dayDetails[referenceDate, 17] = "1";
                    }else if(dayDetails[referenceDate, 17].Equals("0") && dayDetails[referenceDate, 18].Equals("0"))
                    {
                        // bath + exercise at 17-18
                        timeTables.Push(ScheduleTimeTable(17, 18, "Activity", date));
                        exercise = exercise - 1;
                        dayDetails[referenceDate, 17] = "1";
                    }
                }else if( i == 11)
                {
                    if (dayDetails[referenceDate, 18].Equals("0") && exercise == 0)
                    {
                        // bath + exercise at 16-17
                        timeTables.Push(ScheduleTimeTable(18, 19, "Bath", date));
                        dayDetails[referenceDate, 18] = "1";
                    }
                    else if(dayDetails[referenceDate, 18].Equals("0") && dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 18] = "1";
                    }
                }else if(i == 12)
                {
                    if(dayDetails[referenceDate, 19].Equals("0") && dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(19, 20, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 19] = "1";
                    }
                }else if(i == 13)
                {
                    if (dayDetails[referenceDate, 19].Equals("1") && dayDetails[referenceDate,20].Equals("1") && dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(20, 21, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 20] = "1";
                    }
                }else if(i == 14)
                {
                    if(dayDetails[referenceDate, 20].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(20, 21, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 21] = "1";
                    }
                }else if( i == 15)
                {
                    if (dayDetails[referenceDate, 21].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 22] = "1";
                    }
                }else if(i == 16)
                {
                    timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up + Bath", date));
                    dayDetails[referenceDate, 6] = "1";
                }
            }

            return timeTables;
        }

        private Stack<string[]> StudyMode(string date, ref string[,] dayDetails)
        {
            int studyTime = 6;
            int exercise = 1;
            int houseChore = 1;
            int lunch = 1, dinner = 1;
            int bath = 1;
            Stack<string[]> timeTables = new Stack<string[]>();
            
            for (int i = 0; i < 17; i++)
            {
               if(i == 0)
                {
                    timeTables.Push(ScheduleTimeTable(7, 8, "breakfast", date));
                }else if(i == 1)
                {
                    timeTables.Push(ScheduleTimeTable(8, 9, "Study", date));
                    studyTime = studyTime - 1;
                }else if(i == 3)
                {
                    timeTables.Push(ScheduleTimeTable(9, 10, "Study", date));
                    studyTime = studyTime - 1;
                }else if(i == 3)
                {
                    //10-11
                    continue;
                }else if(i == 4)
                {
                    //11-12
                    continue;
                }else if (i == 5)
                {
                    //12-13
                    timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                    lunch = lunch - 1;
                }else if( i == 6)
                {
                    //13-14
                    timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                    houseChore = houseChore - 1;
                }else if( i == 7)
                {   //14-15
                    timeTables.Push(ScheduleTimeTable(14, 15, "Study", date));
                    studyTime = studyTime - 1;
                }
                else if (i == 8)
                {   //15-16
                    timeTables.Push(ScheduleTimeTable(15, 16, "Study", date));
                    studyTime = studyTime - 1;
                }else if(i == 9)
                {
                    //16-17
                    int number = RandomNumber(1, 2);
                    if(number == 10)
                    {
                        timeTables.Push(ScheduleTimeTable(16, 17, "Exercise", date));
                        exercise = exercise - 1;
                    }
                    else
                    {
                        continue;
                    }
                   
                }else if(i == 11)
                {
                    //17-18
                    if(exercise == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Exercise", date));
                        exercise = exercise - 1;
                    }
                    else
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Dinner", date));
                        dinner = dinner - 1;
                    }
                    
                }else if (i == 12)
                {
                    //18-19
                    if(dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                        dinner = dinner - 1;
                    }
                    else
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Bath", date));
                        bath = bath - 1;
                    }
                    
                }else if(i == 13)
                {
                    //19-20
                    if(bath == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(19, 20, "Bath", date));
                    }
                    else
                    {
                        continue;
                    }
                    

                }else if(i == 14)
                {
                    //20-21
                    timeTables.Push(ScheduleTimeTable(20, 21, "Study", date));
                    studyTime = studyTime - 1;
                }else if(i == 15)
                {
                    //21-22
                    timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                    studyTime = studyTime - 1;
                }else if(i == 16)
                {
                    timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up + Bath", date));
                    
                }
            }

            return timeTables;
        }    

        private Stack<string[]> TrainingMode(string date, ref string[,] dayDetails, int referenceDate)
        {
            int studyTime = 2;
            int exerciseTime = 2;
            int  lunch = 1, dinner = 1;
            int bath = 2;
            int houseChore = 1;
            Stack<string[]> timeTables = new Stack<string[]>();
            for(int i = 0; i<16; i++)
            {
                if (i == 0)
                {
                    //6-7
                    //5-6 alternative
                    if (dayDetails[referenceDate, 8].Equals("0") || dayDetails[referenceDate, 9].Equals("0"))
                    {
                        //6-7
                        timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up + breakfast", date));
                        dayDetails[referenceDate, 6] = "1";
                    }
                    else
                    {
                        //5-6
                        timeTables.Push(ScheduleTimeTable(5, 6, "Wake Up + breakfast", date));
                        dayDetails[referenceDate, 5] = "1";
                    }

                }
                else if (i == 1)
                {   //7-8
                    //6-7 alternative
                    if (dayDetails[referenceDate, 8].Equals("0") || dayDetails[referenceDate, 9].Equals("0"))
                    {
                        //7-8
                        timeTables.Push(ScheduleTimeTable(7, 8, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                    }
                    else
                    {   //6-7
                        timeTables.Push(ScheduleTimeTable(6, 7, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                    }

                }
                else if (i == 2)
                {   //8-9
                    //7-8 alternative
                    if (dayDetails[referenceDate, 8].Equals("0") || dayDetails[referenceDate, 9].Equals("0"))
                    {
                        //8-9
                        if (dayDetails[referenceDate, 8].Equals("0"))
                        {
                            timeTables.Push(ScheduleTimeTable(8, 9, "Bath", date));
                            bath = bath - 1;
                        }

                    }
                    else
                    {   //7-8
                        timeTables.Push(ScheduleTimeTable(7, 8, "Bath", date));
                        bath = bath - 1;
                    }
                }
                else if (i == 3)
                {//9-10
                 
                    if (dayDetails[referenceDate, 8].Equals("0") || dayDetails[referenceDate, 9].Equals("0"))
                    {
                        if (dayDetails[referenceDate, 9].Equals("0") && bath == 2)
                        {
                            timeTables.Push(ScheduleTimeTable(9, 10, "Bath", date));
                            bath = bath - 1;
                        }
                    }
                }else if(i == 4)
                {
                    //10-11
                }else if(i == 5)
                {
                    //11-12
                    if(dayDetails[referenceDate, 11].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(11, 12, "Study", date));
                        studyTime = studyTime - 1;
                    }
                }else if( i == 6)
                {
                    //12-13
                    if (dayDetails[referenceDate, 12].Equals("1") && dayDetails[referenceDate, 13].Equals("1"))
                    {
                        timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 12] = "1";
                    }
                    else if (dayDetails[referenceDate, 12].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 12] = "1";
                    }
                }
                else if (i == 7)
                {
                    //13-14
                    if (dayDetails[referenceDate, 13].Equals("0") && lunch == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                    else if (dayDetails[referenceDate, 13].Equals("0") && lunch == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                }else if(i == 8)
                {
                    //14-15
                    if(dayDetails[referenceDate, 14].Equals("0") && studyTime == 2)
                    {
                        timeTables.Push(ScheduleTimeTable(14, 15, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }else if(houseChore == 1 && dayDetails[referenceDate, 15].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(14, 15, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }
                }else if(i == 9){
                    //15-16
                }

                else if(i == 10)
                {
                    int totalTimeOccupied = 0;
                    //16-17
                    for(int j = 0; j < 3; j++)
                    {
                        if(dayDetails[referenceDate, j+16].Equals("1"))
                        {
                            totalTimeOccupied = totalTimeOccupied + 1;
                        }
                        
                    }

                    if (totalTimeOccupied > 1)
                    {
                        if (dayDetails[referenceDate, 18].Equals("0"))
                        {
                            i = 12;
                        }
                        else
                        {
                            i = 13;
                        }
                        
                        continue;
                    }
                    else
                    {
                        if (dayDetails[referenceDate, 16].Equals("0"))
                        {
                            timeTables.Push(ScheduleTimeTable(16, 17, "Exercise", date));
                            exerciseTime = exerciseTime - 1;
                            dayDetails[referenceDate, 16] = "1";
                        }
                    }
                     
                }else if( i == 11)
                {
                    //17-18
                    if(dayDetails[referenceDate, 17].Equals("0") && exerciseTime == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                        dayDetails[referenceDate, 17] = "1";
                    }
                    else if(dayDetails[referenceDate, 17].Equals("0") && exerciseTime == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 17] = "1";
                    }
                }else if(i == 12)
                {//18-19
                    if (dayDetails[referenceDate, 18].Equals("0") && bath == 1 && exerciseTime == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 18] = "1";
                    }
                    else if(dayDetails[referenceDate, 18].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 18] = "1";
                    }
                }else if(i == 13)
                {//19-20
                    if(dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(19, 20, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 19] = "1";
                    }
                }else if( i == 14)
                {// 20-21
                    if(studyTime == 2)
                    {
                        timeTables.Push(ScheduleTimeTable(20, 21, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 20] = "1";
                    }
                }else if(i == 15)
                {// 21-22
                    timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                    studyTime = studyTime - 1;
                    dayDetails[referenceDate, 21] = "1";
                }


            }
            return timeTables;
        }

        private Stack<string[]> TrainingMode(string date, ref string[,] dayDetails)
        {
            int studyTime = 3;
            int exerciseTime = 3;
            int houseChore = 1;
            int lunch = 1, dinner = 1;
            int bath = 2;

            Stack<string[]> timeTables = new Stack<string[]>();

            for(int i =0; i < 16; i++)
            {
                if (i == 0)
                {//6-7
                    timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up + breakfast", date));
                }
                else if (i == 1)
                {//7-8
                    timeTables.Push(ScheduleTimeTable(7, 8, "Exercise", date));
                }
                else if (i == 2)
                {
                    //8-9
                    timeTables.Push(ScheduleTimeTable(8, 9, "Exercise", date));
                    // i == 1
                }
                else if (i == 3)
                {
                    //9-10
                    timeTables.Push(ScheduleTimeTable(9, 10, "Bath", date));
                    bath = bath - 1;
                }
                else if (i == 4)
                {
                    //rest
                    //10-11
                    continue;
                }
                else if (i == 5)
                {
                    //11-12
                    timeTables.Push(ScheduleTimeTable(11, 12, "Study Time", date));
                    studyTime = studyTime - 1;
                }
                else if (i == 6)
                {
                    //12-13
                    timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                    lunch = lunch - 1;
                }
                else if (i == 7)
                {
                    //13-14
                    timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                    houseChore = houseChore - 1;
                }
                else if (i == 8)
                {
                    continue;
                    //14-15
                }
                else if (i == 9)
                {
                    //15-16
                    int random = RandomNumber(1, 2);
                    if (random == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(15, 16, "Study Time", date));
                        studyTime = studyTime - 1;
                    }
                }
                else if(i == 10)
                {
                    //16-17
                    timeTables.Push(ScheduleTimeTable(16, 17, "Exercise", date));
                    exerciseTime = exerciseTime - 1;
                }else if( i == 11)
                {
                    //17-18
                    timeTables.Push(ScheduleTimeTable(17, 18, "Bath", date));
                    bath = bath - 1;
                }else if (i == 12)
                {
                    //18-19
                    timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                    dinner = dinner - 1;
                }else if(i == 13)
                {
                    //19-20
                    //relax 
                }else if(i == 14)
                {//20-21
                    if(studyTime == 1)
                    {
                        continue;
                    }
                    else
                    {
                        timeTables.Push(ScheduleTimeTable(20, 21, "Study", date));
                        studyTime = studyTime - 1;
                    }
                }else if (i == 15)
                {
                    timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                    studyTime = studyTime - 1;
                }
            }

            return timeTables;
        }

        private Stack<string[]> RelaxMode(string date, ref string[,] dayDetails, int referenceDate){
            int studyTime = 3;
            int exerciseTime = 1;
            int houseChore = 1;
            int lunch = 1, dinner = 1;
            int bath = 2;

            Stack<string[]> timeTables = new Stack<string[]>();
            for(int i = 0; i < 16; i++)
            {
                if(i == 0)
                {
                    //6-7
                    timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up + bath", date));
                    dayDetails[referenceDate, 6] = "1";
                }else if(i == 1)
                {//7-8
                    timeTables.Push(ScheduleTimeTable(7, 8, "Breakfast", date));
                    dayDetails[referenceDate, 7] = "1";
                }else if(i == 2)
                {
                    //8-9
                    if (dayDetails[referenceDate, 8].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(8, 9, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 8] = "1";
                    }
                }else if(i == 3)
                {
                    //9-10
                    if (dayDetails[referenceDate, 9].Equals("0") && studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(9, 10, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 9] = "1";
                    }
                }else if(i == 4)
                {
                    //10-11
                    if (dayDetails[referenceDate, 10].Equals("0") && studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(10, 11, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 10] = "1";
                    }
                }else if(i == 5)
                {
                    //11-12
                    if (dayDetails[referenceDate, 11].Equals("0") && studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(11, 12, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 11] = "1";
                    }
                }else if(i == 6)
                {
                    //12-13
                    if (dayDetails[referenceDate, 12].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                        lunch = lunch- 1;
                        dayDetails[referenceDate, 12] = "1";
                    }
                }else if(i == 7)
                {
                    //13-14
                    if (dayDetails[referenceDate, 13].Equals("0") && lunch == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "Lunch", date));
                        lunch = lunch - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                    else if(dayDetails[referenceDate, 13].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 13] = "1";
                    }
                }else if (i == 8)
                {
                    //14-15
                   if (dayDetails[referenceDate, 14].Equals("0") && houseChore == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(14, 15, "House Chore", date));
                        houseChore = houseChore - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }
                    else if(dayDetails[referenceDate, 14].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(14, 15, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 14] = "1";
                    }
                }else if(i == 9)
                {
                    //15-16
                    if(dayDetails[referenceDate, 15].Equals("0") && studyTime > 1)
                    {
                        timeTables.Push(ScheduleTimeTable(15, 16, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 15] = "1";
                    }
                }else if(i == 10)
                {

                    //16-17
                    int totalTimeOccupied = 0;

                    for (int j = 0; j < 3; j++)
                    {
                        if (dayDetails[referenceDate, j + 16].Equals("1"))
                        {
                            totalTimeOccupied = totalTimeOccupied + 1;
                        }

                    }

                    if (totalTimeOccupied > 1)
                    {
                        if (dayDetails[referenceDate, 18].Equals("0"))
                        {
                            i = 12;
                        }
                        else
                        {
                            i = 13;
                        }

                        continue;
                    }
                    else
                    {
                        if (dayDetails[referenceDate, 16].Equals("0"))
                        {
                            timeTables.Push(ScheduleTimeTable(16, 17, "Exercise", date));
                            exerciseTime = exerciseTime - 1;
                            dayDetails[referenceDate, 16] = "1";
                        }
                    }
                }
                else if(i == 11)
                {
                    //17-18
                    if (dayDetails[referenceDate, 17].Equals("0") && exerciseTime == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 17] = "1";
                    }else if (dayDetails[referenceDate, 17].Equals("0") && exerciseTime > 0)
                    {
                        timeTables.Push(ScheduleTimeTable(17, 18, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                        dayDetails[referenceDate, 17] = "1";
                    }
                }
                else if(i == 12)
                {
                    //18-19
                    if (dayDetails[referenceDate, 18].Equals("0") && bath == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 18] = "1";
                    }else if(dayDetails[referenceDate, 18].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 18] = "1";
                    }
                }else if( i == 13)
                {
                    //19-20
                    if (dayDetails[referenceDate, 19].Equals("0") && dinner == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(19, 20, "Dinner", date));
                        dinner = dinner - 1;
                        dayDetails[referenceDate, 19] = "1";
                    }
                }else if(i == 14)
                {
                    //20-21
                    if(bath == 1 && dayDetails[referenceDate, 20].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(20, 21, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 20] = "1";
                    }
                }
                else if (i == 15)
                {
                    //21-22
                    if (bath == 1 && dayDetails[referenceDate, 21].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(21, 22, "Bath", date));
                        bath = bath - 1;
                        dayDetails[referenceDate, 21] = "1";
                    }else if(dayDetails[referenceDate, 21].Equals("0"))
                    {
                        timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                        studyTime = studyTime - 1;
                        dayDetails[referenceDate, 21] = "1";
                    }
                }
            }
            return timeTables;
        }

        private Stack<string[]> RelaxMode(string date, ref string[,] dayDetails)
        {
            int studyTime = 3;
            int exerciseTime = 1;
            int houseChore = 1;
            int lunch = 1, dinner = 1;
           // int bath = 1;

            Stack<string[]> timeTables = new Stack<string[]>();
            for (int i = 0; i < 16; i++)
            {
               if(i == 0)
                {
                    //6-7
                    timeTables.Push(ScheduleTimeTable(6, 7, "Wake Up", date));
                }else if(i == 1)
                {//7-8
                    timeTables.Push(ScheduleTimeTable(7, 8, "Breakfast", date));
                }else if(i == 2)
                {
                    //8-9
                    int random = RandomNumber(1, 2);
                    if(random == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(8, 9, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                    }
                    

                }else if(i == 3)
                {//9-10
                    if(exerciseTime == 0)
                    {
                        timeTables.Push(ScheduleTimeTable(9, 10, "Bath", date));
                        exerciseTime = exerciseTime - 1;
                    }
                    else
                    {
                        timeTables.Push(ScheduleTimeTable(9, 10, "Study", date));
                        studyTime = studyTime - 1;
                    }
                    
                }else if(i == 4)
                {
                    //10-11
                    if (studyTime > 2)
                    {
                        timeTables.Push(ScheduleTimeTable(10, 11, "Study", date));
                        studyTime = studyTime - 1;
                    }
                }else if(i == 5)
                {
                    //11-12
                }else if(i == 6)
                {
                    //12-13
                    timeTables.Push(ScheduleTimeTable(12, 13, "Lunch", date));
                    lunch = lunch - 1;
                }else if(i == 7)
                {
                    //13-14
                    timeTables.Push(ScheduleTimeTable(13, 14, "House Chore", date));
                    houseChore = houseChore - 1;
                }else if(i == 8)
                {
                    //14-15
                    timeTables.Push(ScheduleTimeTable(14, 15, "Study", date));
                    studyTime = studyTime - 1;
                }else if( i == 9)
                {
                    //15-16
                }else if( i == 10)
                {//16-17
                    if(exerciseTime == 1)
                    {
                        timeTables.Push(ScheduleTimeTable(16, 17, "Exercise", date));
                        exerciseTime = exerciseTime - 1;
                    }
                }else if(i == 11)
                {
                    //17-18
                    timeTables.Push(ScheduleTimeTable(17, 18, "Bath", date));
                    
                }else if(i == 12)
                {
                    //18-19
                    timeTables.Push(ScheduleTimeTable(18, 19, "Dinner", date));
                    dinner = dinner - 1;
                }else if( i == 13)
                {
                    //19-20
                }else if(i == 14)
                {
                    //20-21
                }else if(i == 15)
                {
                    //21-22
                    timeTables.Push(ScheduleTimeTable(21, 22, "Study", date));
                    studyTime = studyTime - 1;
                }
            }
            return timeTables;
        }

        private string[] ScheduleTimeTable(int startTime, int endTime, string activitity, string date)// 7,8,"breakfast",dat
        {
            string[] timeTable = new string[6];
            timeTable[0] = DateTime.Parse(date).ToString("yyyyMMddTHH0000Z"); // DStamp
            timeTable[1] = DateTime.Parse(date).AddHours(startTime).ToString("yyyyMMddTHH0000"); // DStart
            timeTable[2] = DateTime.Parse(date).AddHours(endTime).ToString("yyyyMMddTHH0000"); //DEnd
            timeTable[3] = Guid.NewGuid().ToString() + DateTime.Parse(date).ToString("yyyyMMddTHHmm00Z"); //UID
            timeTable[4] = activitity; //Summary
            timeTable[5] = HttpContext.Current.Request.Url.AbsoluteUri.Replace("TimeTableGenerator", "InfoCenter") + "?name=" + "activity";   //description         
            return timeTable;
        }

        public class Temp
        {
            public double day { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }

        public class FeelsLike
        {
            public double day { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Daily
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
            public int moonrise { get; set; }
            public int moonset { get; set; }
            public double moon_phase { get; set; }
            public Temp temp { get; set; }
            public FeelsLike feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public double wind_speed { get; set; }
            public int wind_deg { get; set; }
            public double wind_gust { get; set; }
            public List<Weather> weather { get; set; }
            public int clouds { get; set; }
            public double pop { get; set; }
            public double rain { get; set; }
            public double uvi { get; set; }
        }

        public class WeatherInfo
        {
            public double lat { get; set; }
            public double lon { get; set; }
            public string timezone { get; set; }
            public int timezone_offset { get; set; }
            public List<Daily> daily { get; set; }
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private readonly Random _random = new Random();

        protected void FileUploading_CheckedChanged(object sender, EventArgs e)
        {
            if(FileUploading.Checked == true)
            {
                fileUpload.Visible = true;
            }
            else
            {
                fileUpload.Visible = false;
            }
        }
    }

}