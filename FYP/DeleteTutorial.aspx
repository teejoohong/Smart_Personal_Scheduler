<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DeleteTutorial.aspx.cs" Inherits="FYP.DeleteTutorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="CSS/Tutorial.css" rel="stylesheet" type="text/css" />
    <h1><u>Help Center</u></h1>
    <hr />
    <div class="content">

    <table class="table1">
        <tr>

            <td class="menu"><div class="vl">Help menu
                </div>
            </td>
            <td>

                <p class="font"><b><u>How to delete your imported your ics file?</u></b></p>


            </td>
        </tr>

        <tr >
            <td rowspan="3" class="menu">
                <div class="vl">
                        <a href="Tutorial.aspx">1.How to get your ics file?</a><br /><br />
                        <a href="Tutorial2.aspx">2.How to import your ics file?</a><br /><br />
                        <a href="DeleteTutorial.aspx">3.How to delete your ics file?</a>                   
                 </div>
            </td>
            <td><p>1. Go to your goole calendar and follow the the below instruction.</p>
                <asp:Image CssClass="imageStyle" ID="Image1" runat="server" ImageUrl="~/BuildInPicturess/google calendar1.png"/>

            </td>

        </tr>
        <tr>
            <td><hr /></td>
        </tr>

        <tr>
            <td><p>2.</p>
                <asp:Image CssClass="imageStyle" ID="Image2" runat="server" ImageUrl="~/BuildInPicturess/delete.png"/>

            </td>
        </tr>


    </table>
        <br />
        <br />
        </div>
</asp:Content>