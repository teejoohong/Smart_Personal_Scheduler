<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="FYP.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!--CSS -->
    <link href="CSS/AboutUs.css" rel="stylesheet" type="text/css" />

    <div class="aboutUs">
       
        <p class="imgLogo"> <asp:Image ID="Image1" runat="server" ImageUrl="~/BuildInPicturess/logo.JPG" AlternateText="Smart Personal Scheduler" Height="270px" Width="250px" /></p>
        <article><br />
            <h2>Smart Personal Scheduler</h2>
            <p>Smart Personal Scheduler provide you an easy way for you to create
                a timetable!
            </p>
            <p>
                We aimed to assist users in changing from unhealty living lifestyle
                to a healtheir lifestyle and make users more discipline in term of 
                their daily activities to get rid of unhealthy living lifestyle!
            </p>
            <p> Try it out now! Tutorial is also available to guide
                you through out the timetable generation process!
            </p><br /><br /><br /><br /><br />
           
        </article>
    </div>

  <div class="row">
      <div class="column">
        <h2>Contacts</h2>
        <hr />
        <p>+6018-319 9792 (TEE)</p>
        <p>+6019-358 9809 (YONG)</p>
      </div>
      <div class="column" >
        <h2>Email</h2>
        <hr />
        <p>teejh-wp18@student.tarc.edu.my</p>
        <p>yongwh-wp18@student.tarc.edu.my</p>
      </div>
</div>
    
</asp:Content>
