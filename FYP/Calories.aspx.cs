using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class Calories : System.Web.UI.Page
    {
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strcon);

            con.Open();
            string strUpdate = "UPDATE [User] SET Weight = @Weight, Age = @Age, Height = @Height WHERE Name = @Name";

            SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

            cmdUpdate.Parameters.AddWithValue("@Name", Session["UserName"]);
            cmdUpdate.Parameters.AddWithValue("@Weight", txtWeight.Text);
            cmdUpdate.Parameters.AddWithValue("@Age", txtAge.Text);
            cmdUpdate.Parameters.AddWithValue("@Height", txtHeight.Text);

            int n = cmdUpdate.ExecuteNonQuery();
            if (n > 0) // Use to check whether the value have been insert into the database
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data saved!" + "');", true);
                Response.Redirect("Calories.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data is not saved!" + "');", true);
                Response.Redirect("Calories.aspx");
            }
            con.Close();
        }

        //METS
        const double basketball = 8.0, runningMin = 13.5, runningMax =16.0, jogging = 8.0,football = 7.0,
            volleyball = 4.0, tennis = 7.0, bodybuilding = 7.0, swimmingMin = 6.0, swimmingMax =10.0,
            kungfu = 10.0,ropeJumpingMin = 10.0, ropeJumpingMax=10.0,badminton = 7.0,dancing =6.0,
            cycling = 5.0, gymnastics = 5.0, eating = 1.005, studying = 1.01,houseChores = 3.0 ;

        private double calculateBasal() {
            double basal = 0;
            if (gender == "Male")
            {
                basal = 10 * testKG + 6.25 * height - 5 * age + 5;
            }
            else { 
                basal = 10 * testKG + 6.25 * height - 5 * age - 161;
            }
            return basal;
        }

        private void estimationResult() {
            double basal = calculateBasal();
            double resultCal = basal * double.Parse(ddlWork.SelectedValue);
            lblEstimation.Text = resultCal.ToString() + " calories";
        }

        protected void ddlWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            double basal = calculateBasal();
            double resultCal = basal * double.Parse(ddlWork.SelectedValue);
            lblEstimation.Text = resultCal.ToString() + " calories";
            if (ddlWork.Text != "Basal metabolic rate") {
                lblWeightLostMild.Text = "Mild weight loss(0.25 kg / week) :&nbsp" + (resultCal - 250).ToString() + " calories";
                lblWeightLostMed.Text = "Weight weight loss (0.5 kg/week) :&nbsp" + (resultCal - 500).ToString() + " calories";
                lblWeightLostMax.Text = "Extreme weight loss (1.0 kg/week) :&nbsp" + (resultCal - 1000).ToString() + " calories";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Calories.aspx");
        }

        protected void btnWeight_Click(object sender, EventArgs e)
        {
            chgInputView();
        }

        protected void btnTimetable_Click(object sender, EventArgs e)
        {
            Response.Redirect("TimeTableGenerator.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

        double testKG = 0;
        double height = 0; double age = 0;
        double time = 45.0;
        string gender = "";

        static string testData ;

        string[] testArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = "US1";
            Session["UserName"] = "ali123";

            if (Session["UserName"] != null && Session["UserID"]!= null )
            {
                // get weight
                SqlConnection con;
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);
                con.Open();

                string strSelect = "SELECT Weight, Height, Age,Gender FROM [User] Where Name = @Name";
                //specify what is the command , what is the connection string
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Name", Session["UserName"]);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                while (dtr.Read()) {
                    if ((double)dtr["Weight"] == 0.0 || (double)dtr["Age"] == 0.0 || (double)dtr["Height"] == 0.0)
                    {
                        // do something
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found')", true);
                        chgInputView();
                    }
                    else
                    {
                        // do something else
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('data found')", true);
                        testKG = (double)dtr["Weight"];
                        age = (double)dtr["Age"];
                        height = (double)dtr["Height"];
                        gender = (string)dtr["Gender"];
                    }
                }  
                con.Close();

                //get activities
                con.Open();

                strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);
                con.Open();

                strSelect = "SELECT Activity FROM [AllocatedActivities] Where UserID = @UserID";
                //specify what is the command , what is the connection string
                cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@UserID", Session["UserID"]);
                dtr = cmdSelect.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record found')", true);
                        testData = (string)dtr["Activity"];
                        testArray = testData.Split(',');
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found, please go and generate a timetable.')", true);
                    //No record found navigate to generate timetable
                    if(Session["UserID"] != null)
                        chgTimetableView();
                }
                

                con.Close();

                if (testArray != null && testKG != 0 && age != 0 && height != 0) {
                    displayChart();
                    chgCaloriesView();
                    if (!IsPostBack)
                        estimationResult();
                }
            }
            else {
                //display login form
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No user login')", true);
                chgLoginView();
            }
        }

        private void displayChart() {
            Series series = chartCalories.Series["Series1"];
            Series series2 = chartEachEvent.Series["Series1"];
            double calories = 0;
            double total = 0;
            double totalEating = 0,totalHouseChores = 0 , totalStudying = 0,
                totalBasketball = 0, totalBadminton = 0, totalSwimming = 0,
                totalJogging = 0,totalRunning = 0,totalGym = 0,totalVolleyball = 0
                ,totalTennis = 0,totalKungfu = 0,totalFootball = 0,totalRopeJumping = 0
                ,totalDancing = 0,totalGymnastics = 0,totalCycling = 0;
            if (IsPostBack)
            {
                lblActivityCalories.Text = "";
                lblEachActivity.Text = "";
            }
            foreach (string item in testArray)
            {
                //basketball,badminton,swimming,jogging,running,gym
                if (item == "basketball")
                {
                    calories = calculateActivityCalories(time, basketball, testKG);
                    totalBasketball += calories;
                }
                else if (item == "badminton")
                {
                    calories = calculateActivityCalories(time, badminton, testKG);
                   totalBadminton += calories;
                }
                else if (item == "swimming")
                {
                    calories = calculateActivityCalories(time, swimmingMin, testKG);
                    totalSwimming += calories;
                }
                else if (item == "jogging")
                {
                    calories = calculateActivityCalories(time, jogging, testKG);
                    totalJogging += calories;
                }
                else if (item == "running")
                {
                    calories = calculateActivityCalories(time, runningMin, testKG);
                    totalRunning += calories;
                }
                else if (item == "gym")
                {
                    calories = calculateActivityCalories(time, bodybuilding, testKG);
                    totalGym += calories;
                }
                else if (item == "volleyball")
                {
                    calories = calculateActivityCalories(time, volleyball, testKG);
                    totalVolleyball += calories;
                }
                else if (item == "tennis")
                {
                    calories = calculateActivityCalories(time, tennis, testKG);
                    totalTennis += calories;
                }
                else if (item == "kungfu")
                {
                    calories = calculateActivityCalories(time, kungfu, testKG);
                    totalKungfu += calories;
                }
                else if (item == "football")
                {
                    calories = calculateActivityCalories(time, football, testKG);
                    totalFootball += calories;
                }
                else if (item == "ropejumping")
                {
                    calories = calculateActivityCalories(time, ropeJumpingMin, testKG);
                    totalRopeJumping += calories;
                }
                else if (item == "dancing")
                {
                    calories = calculateActivityCalories(time, dancing, testKG);
                    totalDancing += calories;
                }
                else if (item == "gymnastics")
                {
                    calories = calculateActivityCalories(time, gymnastics, testKG);
                    totalGymnastics += calories;
                }
                else if (item == "cycling")
                {
                    calories = calculateActivityCalories(time, cycling, testKG);
                    totalCycling += calories;
                }
                else if (item == "eating") {
                    calories = calculateActivityCalories(time, eating, testKG);
                    totalEating += calories;
                    
                }
                else if (item == "studying") {
                    calories = calculateActivityCalories(time, studying, testKG);
                    totalStudying += calories;
                    
                }
                else if (item == "houseChores") {
                    calories = calculateActivityCalories(time, houseChores, testKG);
                    
                    totalHouseChores += calories;
                }
                
                else
                {
                    series.Points.AddXY("LALA", 200);
                }
                total += calories;
                //lblActivityCalories.Text += "<br/>" + item + " : " + calories.ToString("0.00") + " calories <br/> ";
            }

            

            if (!isZero(totalBasketball)) {
                series.Points.AddXY("Basketball", totalBasketball);
                assignEvent(series2, "Basketball",basketball);
                lblActivityCalories.Text += "<br/>Basketball  : " + totalBasketball.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalBadminton))
            {
                series.Points.AddXY("Badminton", totalBadminton);
                assignEvent(series2, "Badminton", badminton);
                lblActivityCalories.Text += "<br/>Badminton  : " + totalBadminton.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalSwimming))
            {
                series.Points.AddXY("Swimming", totalSwimming);
                assignEvent(series2, "Swimming", swimmingMin);
                lblActivityCalories.Text += "<br/>Swimming  : " + totalSwimming.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalJogging))
            {
                series.Points.AddXY("Jogging", totalJogging);
                assignEvent(series2, "Jogging", jogging);
                lblActivityCalories.Text += "<br/>Jogging  : " + totalJogging.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalRunning))
            {
                series.Points.AddXY("Running", totalRunning);
                assignEvent(series2, "Running", runningMin);
                lblActivityCalories.Text += "<br/>Running  : " + totalRunning.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalVolleyball))
            {
                series.Points.AddXY("Volleyball", totalVolleyball);
                assignEvent(series2, "Volleyball", volleyball);
                lblActivityCalories.Text += "<br/>Volleyball  : " + totalVolleyball.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalTennis))
            {
                series.Points.AddXY("Tennis", totalTennis);
                assignEvent(series2, "Tennis", tennis);
                lblActivityCalories.Text += "<br/>Tennis  : " + totalTennis.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalKungfu))
            {
                series.Points.AddXY("Kungfu", totalKungfu);
                assignEvent(series2, "Kungfu", kungfu);
                lblActivityCalories.Text += "<br/>Kungfu  : " + totalKungfu.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalFootball))
            {
                series.Points.AddXY("Football", totalFootball);
                assignEvent(series2, "Football", football);
                lblActivityCalories.Text += "<br/>Football  : " + totalFootball.ToString("0.00") + " calories <br/> ";
            }

            if (!isZero(totalRopeJumping))
            {
                series.Points.AddXY("RopeJumping", totalRopeJumping);
                assignEvent(series2, "RopeJumping", ropeJumpingMin);
                lblActivityCalories.Text += "<br/>RopeJumping  : " + totalRopeJumping.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalDancing))
            {
                series.Points.AddXY("Dancing", totalDancing);
                assignEvent(series2, "Dancing", dancing);
                lblActivityCalories.Text += "<br/>Dancing  : " + totalDancing.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalGymnastics))
            {
                series.Points.AddXY("Gymnastics", totalGymnastics);
                assignEvent(series2, "Gymnastics", gymnastics);
                lblActivityCalories.Text += "<br/>Gymnastics  : " + totalGymnastics.ToString("0.00") + " calories <br/> ";
            }
            if (!isZero(totalCycling))
            {
                series.Points.AddXY("Cycling", totalCycling);
                assignEvent(series2, "Cycling", cycling);
                lblActivityCalories.Text += "<br/>Cycling  : " + totalCycling.ToString("0.00") + " calories <br/> ";
            }



            series.Points.AddXY("Eating", totalEating);
            lblActivityCalories.Text += "<br/>Eating  : " + totalEating.ToString("0.00") + " calories <br/> ";
            assignEvent(series2, "Eating", eating);

            series.Points.AddXY("Studying", totalStudying);
            lblActivityCalories.Text += "<br/>Studying  : " + totalStudying.ToString("0.00") + " calories <br/> ";
            assignEvent(series2, "Studying", studying);

            series.Points.AddXY("House Chores", totalHouseChores);
            lblActivityCalories.Text += "<br/>House Chores  : " + totalHouseChores.ToString("0.00") + " calories <br/> ";
            assignEvent(series2, "House Chores", houseChores);

            lblActivityCalories.Text += "<br/>Total : " + total.ToString("0.00") + " calories.";
            lblActivityCalories.Text += "<br/><br/> *The calories burned is computed for 45 mins of exercising. ";
            lblEachActivity.Text += "<br/><br/> *The calories burned is computed for 45 mins of exercising. ";
        }

        private void assignEvent(Series series , String name , double met )
        {
            double calories;
            calories = calculateActivityCalories(time, met, testKG);
            series.Points.AddXY(name, calories);
            lblEachActivity.Text += "<br/>" + name + " : " + calories.ToString("0.00") + " calories <br/> ";
        }

        private Boolean isZero(double count)
        {
            Boolean isZero = false;

            if (count == 0) {
                isZero = true;
            }

            return isZero;
        }

        private double calculateActivityCalories(double duration, double MET, double KG)
        {
            double activityCalories;

            activityCalories = duration * (MET * 3.5    * KG) / 200;

            return activityCalories;
        }

        private void chgLoginView() {
            caloriesMultiView.ActiveViewIndex = 1;
        }

        private void chgInputView()
        {
            caloriesMultiView.ActiveViewIndex = 0;
        }
        private void chgCaloriesView()
        {
            caloriesMultiView.ActiveViewIndex = 2;
        }

        private void chgTimetableView()
        {
            caloriesMultiView.ActiveViewIndex = 3;
        }
    }

    //Page.Response.Write("<script>console.log('" + testArray[1] + "');</script>");
}