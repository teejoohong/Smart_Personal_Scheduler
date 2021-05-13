<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="FYP.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--CSS -->
    <link href="HomePage.css" rel="stylesheet" type="text/css" />

    <div class="container">
        <div class="row">
          <div class="column">
            <h2>BMI</h2>
            <hr />

            <div class="whiteContainer">
                <asp:Image ID="Image2" runat="server" 
                    ImageUrl="~/BuildInPicturess/calculator.jpg"
                AlternateText="Calculator" Width ="94%" Height="70%" /><br />
                <asp:Button ID="btnBMI" CssClass="btnContainer" runat="server" Text="Calculate now" />
            </div>
          </div>
          <div class="column" >
            <h2>Timetable</h2>
            <hr />
            <div class="blackContainer">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/BuildInPicturess/logo.JPG" 
                AlternateText="Smart Personal Scheduler" Width ="100%" Height="70%" /><br />
                <asp:Button ID="btnTimetable" CssClass="btnContainer" runat="server" Text="Generate now" />
            
            </div>
          </div>
          <div class="column" >
            <h2>Calories</h2>
            <hr />
            <div class="whiteContainer" >
                  <asp:Image ID="Image3" runat="server" 
                    ImageUrl="~/BuildInPicturess/calculator.jpg"
                AlternateText="Calculator" Width ="94%" Height="70%" /><br />
                <asp:Button ID="btnCalories" CssClass="btnContainer" runat="server" Text="Estimate now" />
            </div>
            </div>
        </div>
    </div>
    
    <div class="description">
        <h2>Timetable</h2>
        <p>Get you google Timetable created automatically now by inserting the ICS file.</p>

        <h2>BMI</h2>
        <p>Calculate your Body Weight Index (BMI) to see if you are healthy!</p>

        <h2>Calories</h2>
        <p>Estimate calories needed for your daily activities.</p>
    </div>


</asp:Content>
