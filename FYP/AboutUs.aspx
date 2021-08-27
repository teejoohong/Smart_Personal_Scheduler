﻿<%@ Page Title="About Us" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="FYP.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!--CSS -->
    <link href="CSS/AboutUs.css" rel="stylesheet" type="text/css" />
    <h1 style="text-align:center">About Us</h1> <br />
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
        <h2>Emails</h2>
        <hr />
        <p>teejh-wp18@student.tarc.edu.my</p>
        <p>yongwh-wp18@student.tarc.edu.my</p>
      </div>
</div>

    <table class="aboutMeTable">
        <tr>
            
            <td ><h2>Tee Joo Hong<br /></h2><hr /></td>
            <td ><h2>Yong Wei Han<br /></h2><hr /></td>
        </tr>
        <tr>
            
            <td><asp:Image ID="Image3" ImageUrl="~/BuildInPicturess/TJH.jpg" CssClass="aboutMePicture" runat="server" /></td>
            <td><asp:Image ID="Image2" ImageUrl="~/BuildInPicturess/ywh.jpg" CssClass="aboutMePicture" runat="server" /></td>
        </tr>
        <tr>
            
            <td><p>Project Leader</p>
                <p>Currently studying Bachelor Degree in Software Engineering in University Tunku Abdul Rahman.

                With CGPA &gt;3.8(Xue Ba)</p></td>
            <td>
                <p>Da gong zai</p>
                <p>Currently studying Bachelor Degree in Software Engineering in University Tunku Abdul Rahman.

                </p>
                
            </td>

        </tr>

        
    </table>

    
</asp:Content>
