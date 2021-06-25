<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="FYP.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--CSS -->
    <link href="CSS/HomePage.css" rel="stylesheet" type="text/css" />
    <table class="tableThreeContainer">
        <tr>
            <td><h2>BMI</h2><hr /></td>
            <td><h2>Timetable</h2><hr /></td>
            <td><h2>Calories</h2><hr /></td>
        </tr>
        <tr>
            <td>
            <div class="whiteContainer">
                <asp:Image ID="Image2" runat="server" 
                    ImageUrl="~/BuildInPicturess/calculator.jpg"
                AlternateText="Calculator" Width ="94%" Height="50%" /><br />
                <asp:Button ID="btnBMI" CssClass="btnContainer" runat="server" Text="Calculate now" OnClick="btnBMI_Click" />
            </div>
            </td>
            <td>
                <div class="blackContainer">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/BuildInPicturess/logo.JPG" 
                AlternateText="Smart Personal Scheduler" Width ="100%" Height="70%" /><br />
                <asp:Button ID="btnTimetable" CssClass="btnContainer" runat="server" Text="Generate now" OnClick="btnTimetable_Click" />
            
            </div>
            </td>
            <td><div class="whiteContainer" >
                  <asp:Image ID="Image3" runat="server" 
                    ImageUrl="~/BuildInPicturess/calculator.jpg"
                AlternateText="Calculator" Width ="94%" Height="70%" /><br />
                <asp:Button ID="btnCalories" CssClass="btnContainer" runat="server" Text="Estimate now" OnClick="btnCalories_Click" />
            </div></td>
        </tr>
        <tr class="description">
            <td> 
            <h2>Timetable</h2>
            <p>Get you google Timetable created automatically now by inserting the ICS file.</p>
            </td>
            <td>
                <h2>BMI</h2>
                <p>Calculate your Body Weight Index (BMI) to see if you are healthy!</p>
            </td>
            <td>
                <h2>Calories</h2>
                <p>Estimate calories needed for your daily activities.</p>
            </td>
        </tr>
    </table>

</asp:Content>
