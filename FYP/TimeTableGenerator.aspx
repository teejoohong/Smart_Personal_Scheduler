<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TimeTableGenerator.aspx.cs" Inherits="FYP.TimeTableGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/TimeTableGenerator.css" rel="stylesheet" type="text/css" />
<style type="text/css">

</style>
    
      <% if (Session["UserID"] == null)
          { %>
            <div >
            <p>Please log in first.</p>
            </div>
        <%}
            else
            { %>
    <div>
         <asp:Button runat="server" Text="Generate" OnClick="GenerationOfTimetable_Click" CssClass="btnGenerate"/>
        
        <br />
        <asp:CheckBox ID="FileUploading" runat="server" OnCheckedChanged="FileUploading_CheckedChanged" text="Click to include ics file upload" AutoPostBack="true"/>
             <br />
        <div id="fileUpload" runat="server" visible="false">
            <asp:FileUpload ID="timeTableFile" runat="server" Width="279px" />
        </div>
            

        <asp:RadioButtonList ID="modeGeneration" runat="server">
            <asp:ListItem>Study Mode</asp:ListItem>
            <asp:ListItem>Training Mode</asp:ListItem>
            <asp:ListItem>Relax Mode</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        
           

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

            <asp:Button ID="Button1" runat="server" Text="Download" style="" OnClick="Button1_Click" CssClass="btnDownload"/>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
       
    
  </div>
    

  

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
