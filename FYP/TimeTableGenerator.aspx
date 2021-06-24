<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TimeTableGenerator.aspx.cs" Inherits="FYP.TimeTableGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/TimeTableGenerator.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Profile.css" rel="stylesheet" type="text/css" />
<style type="text/css">

</style>
    
      <% if (Session["UserID"] == null)
          { %>
            <div >
            <table id="loginForm" class="inputForm">
                <tr>
                    <th colspan="2"><h2>Profile</h2></th>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr style="text-align:center">
                    <td colspan="2">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="buttonLogin" OnClick="btnLogin_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr style="text-align:center">
                    <td colspan="2">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="buttonLogin" OnClick="btnRegister_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
            </div>
        <%}
            else
            { %>
    <div>
        
        <p><b><u>Generate your own time table now!!!</u></b></p>
        <asp:RadioButtonList ID="modeGeneration" runat="server">
            <asp:ListItem>Study Mode</asp:ListItem>
            <asp:ListItem>Training Mode</asp:ListItem>
            <asp:ListItem>Relax Mode</asp:ListItem>
        </asp:RadioButtonList>
         <br />
        <asp:CheckBox ID="FileUploading" runat="server" OnCheckedChanged="FileUploading_CheckedChanged" text="Click here to include your ics file" AutoPostBack="true"/>
             <br />
        <div id="fileUpload" runat="server" visible="false" >
            <br />
            <asp:FileUpload ID="timeTableFile" runat="server" Width="279px"/>
        </div>
         <br />
         <asp:Button runat="server" Text="Generate" OnClick="GenerationOfTimetable_Click" CssClass="btnGenerate" ID="Button2"/>
        
         <br />
        <br />
        
           
        </div>

        <div runat="server" visible="false" id="previewTable">
            <p>Preview</p>

            <table class="preview">
                <tr>
                    
                     <td><asp:Label ID="day1" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day2" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day3" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day4" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day5" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day6" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="day7" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="detail1" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail2" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail3" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail4" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail5" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail6" runat="server" Text="Label"></asp:Label></td>
                    <td><asp:Label ID="detail7" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>

            <br />

            <asp:Button ID="Button1" runat="server" Text="Download" style="" OnClick="Button1_Click" CssClass="btnDownload"/>
        </div>
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
       
    

    

  

<script>


    

    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            x.innerHTML = "Geolocation is not supported by this browser.";
        }
    }

    function showPosition(position) {
        document.getElementById("<%=HiddenField1.ClientID%>").value = position.coords.latitude;
        document.getElementById("<%=HiddenField2.ClientID%>").value = position.coords.latitude;
    }
    getLocation()
</script>

    <%} %>
       
  
</asp:Content>
