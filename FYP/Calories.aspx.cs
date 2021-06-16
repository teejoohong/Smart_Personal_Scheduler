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
            string strUpdate = "UPDATE [User] SET Weight = @Weight WHERE Name = @Name";

            SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

            cmdUpdate.Parameters.AddWithValue("@Name", Session["UserName"]);
            cmdUpdate.Parameters.AddWithValue("@Weight", txtWeight.Text);

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
            kungfu = 10.0,ropeJumpingMin = 10.0, ropeJumpingMax=10.0,badminton = 7.0;

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
        double time = 45.0;


        static string testData ;

        string[] testArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["UserID"] = "US1";
            //Session["UserName"] = "ali123";

            if (Session["UserName"] != null && Session["UserID"]!= null)
            {
                // get weight
                SqlConnection con;
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);
                con.Open();

                string strSelect = "SELECT Weight FROM [User] Where Name = @Name";
                //specify what is the command , what is the connection string
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Name", Session["UserName"]);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                while (dtr.Read()) {
                    if ((double)dtr["Weight"] == 0.0)
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

                while (dtr.Read()) {
                    if ((string)dtr["Activity"] == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found, please go and generate a timetable.')", true);
                        //No record found navigate to generate timetable
                        chgTimetableView();
                    }
                    else {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record found')", true);
                        testData = (string)dtr["Activity"];
                        testArray = testData.Split(',');
                    }
                }

                con.Close();

                if (testArray != null && testKG != 0) {
                    displayChart();
                    chgCaloriesView();
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
            double calories = 0;
            double maxCalories,total = 0;
            foreach (string item in testArray)
            {
                //basketball,badminton,swimming,jogging,running,gym
                if (item == "basketball")
                {
                    calories = calculateActivityCalories(time, basketball, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "badminton")
                {
                    calories = calculateActivityCalories(time, badminton, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "swimming")
                {
                    calories = calculateActivityCalories(time, swimmingMin, testKG);
                    maxCalories = calculateActivityCalories(time, swimmingMax, testKG);
                    series.Points.AddXY("Min" + item, calories);
                    series.Points.AddXY("Max" + item, maxCalories);
                }
                else if (item == "jogging")
                {
                    calories = calculateActivityCalories(time, jogging, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "running")
                {
                    calories = calculateActivityCalories(time, runningMin, testKG);
                    maxCalories = calculateActivityCalories(time, runningMax, testKG);
                    series.Points.AddXY("Min " + item, calories);
                    series.Points.AddXY("Max " + item, maxCalories);
                }
                else if (item == "gym")
                {
                    calories = calculateActivityCalories(time, bodybuilding, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "volleyball") {
                    calories = calculateActivityCalories(time, volleyball, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "tennis")
                {
                    calories = calculateActivityCalories(time, tennis, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "kungfu")
                {
                    calories = calculateActivityCalories(time, kungfu, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "football")
                {
                    calories = calculateActivityCalories(time, football, testKG);
                    series.Points.AddXY(item, calories);
                }
                else if (item == "ropejumping")
                {
                    calories = calculateActivityCalories(time, ropeJumpingMin, testKG);
                    maxCalories = calculateActivityCalories(time, ropeJumpingMax, testKG);
                    series.Points.AddXY("Min " + item, calories);
                    series.Points.AddXY("Max " + item, maxCalories);
                }
                else
                {
                    series.Points.AddXY(item, 200);
                }
                total += calories; 
            }
            
        }

        private double calculateActivityCalories(double duration, double MET, double KG)
        {
            double activityCalories;

            activityCalories = duration * (MET * 3.5 * KG) / 200;

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