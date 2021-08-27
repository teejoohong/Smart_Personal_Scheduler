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
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace FYP
{
    public partial class InfoCenter : System.Web.UI.Page
    {
        String[] testArray = new string[6] ;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            divButton.Style.Add("display", "none");
            btnFirstPreference.Style.Add("background-color", "#1c87c9");
            btnSecPreference.Style.Add("background-color", "#1c87c9");
            btnThirdPreference.Style.Add("background-color", "#1c87c9");
            btnFirstPreferenceIn.Style.Add("background-color", "#1c87c9");
            btnSecPreferenceIn.Style.Add("background-color", "#1c87c9");
            btnThirdPreferenceIn.Style.Add("background-color", "#1c87c9");

            if (Session["UserName"] != null && Session["UserID"] != null) {

                SqlConnection con;
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);
                //get activities
                con.Open();

                strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strcon);
                con.Open();

                string strSelect = "SELECT O.Activity_1, O.Activity_2, O.Activity_3, I.Activity_1 AS InAct1, I.Activity_2 AS InAct2, I.Activity_3 AS InAct3, I.UserID " +
                    "FROM OutdoorPreference AS O INNER JOIN IndoorPreference AS I " +
                    "ON O.UserID = I.UserID " +
                    "WHERE (I.UserID = @UserID)";

                //specify what is the command , what is the connection string
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@UserID", Session["UserID"]);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        //retrieve preference
                        testArray[0] = dtr["Activity_1"].ToString();
                        testArray[1] = dtr["Activity_2"].ToString();
                        testArray[2] = dtr["Activity_3"].ToString();
                        testArray[3] = dtr["InAct1"].ToString();
                        testArray[4] = dtr["InAct2"].ToString();
                        testArray[5] = dtr["InAct3"].ToString();
                    }
                    //display buttons
                    divButton.Style.Add("display", "block");
                    btnFirstPreference.Text = ToTitleCase(testArray[0]);
                    btnSecPreference.Text = ToTitleCase(testArray[1]);
                    btnThirdPreference.Text = ToTitleCase(testArray[2]);
                    btnFirstPreferenceIn.Text = ToTitleCase(testArray[3]);
                    btnSecPreferenceIn.Text = ToTitleCase(testArray[4]);
                    btnThirdPreferenceIn.Text = ToTitleCase(testArray[5]);

                    string name = Request.QueryString["name"];
                    if (name != null) {
                        if (name == AcitivityPlace(btnFirstPreference.Text.ToLower().ToString()))
                            btnFirstPreference.Style.Add("background-color", "darkslateblue");
                        else if (name == AcitivityPlace(btnSecPreference.Text.ToLower().ToString()))
                            btnSecPreference.Style.Add("background-color", "darkslateblue");
                        else if (name == AcitivityPlace(btnThirdPreference.Text.ToLower().ToString()))
                            btnThirdPreference.Style.Add("background-color", "darkslateblue");
                        else if (name == AcitivityPlace(btnFirstPreferenceIn.Text.ToLower().ToString()))
                            btnFirstPreferenceIn.Style.Add("background-color", "darkslateblue");
                        else if (name == AcitivityPlace(btnSecPreferenceIn.Text.ToLower().ToString()))
                            btnSecPreferenceIn.Style.Add("background-color", "darkslateblue");
                        else if (name == AcitivityPlace(btnThirdPreferenceIn.Text.ToLower().ToString()))
                            btnThirdPreferenceIn.Style.Add("background-color", "darkslateblue");
                    } 
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Set up your preferences to show preference buttons.')", true);
                }

                con.Close();

                
            }
            
        }
        protected void btnFirstPreference_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[0]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        protected void btnSecPreference_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[1]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        protected void btnThirdPreference_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[2]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        protected void btnFirstPreferenceIn_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[3]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        protected void btnSecPreferenceIn_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[4]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        protected void btnThirdPreferenceIn_Click(object sender, EventArgs e)
        {
            string place = AcitivityPlace(testArray[5]);
            Response.Redirect("InfoCenter.aspx?name=" + place);
        }

        private string AcitivityPlace(string activity)
        {
            
            string activityPlace = "";
            if (activity.Equals("basketball"))
                activityPlace = "basketball court";
            else if (activity.Equals("football"))
                activityPlace = "football court";
            else if (activity.Equals("futsal"))
                activityPlace = "futsal court";
            else if (activity.Equals("jogging"))
                activityPlace = "jogging";
            else if (activity.Equals("running"))
                activityPlace = "park";
            else if (activity.Equals("tennis"))
                activityPlace = "tennis court";
            else if (activity.Equals("badminton"))
                activityPlace = "badminton court";
            else if (activity.Equals("swimming"))
                activityPlace = "swimming";
            else if (activity.Equals("ping pong"))
                activityPlace = "ping pong court";
            else if (activity.Equals("gym"))
                activityPlace = "gym";
            else if (activity.Equals("gymnastic"))
                activityPlace = "gymastics";
            else if (activity.Equals("kungfu"))
                activityPlace = "Chinese martial arts";
            else if (activity.Equals("volleyball"))
                activityPlace = "volleyball court";
            else if (activity.Equals("ropejumping"))
                activityPlace = "";
            else if (activity.Equals("dancing"))
                activityPlace = "Dance";

            return activityPlace;
        }

        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        
    }
   

}