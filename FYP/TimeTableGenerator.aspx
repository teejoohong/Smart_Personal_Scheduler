<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TimeTableGenerator.aspx.cs" Inherits="FYP.TimeTableGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <div>
        <asp:FileUpload ID="timeTableFile" runat="server" />
         <asp:Button runat="server" Text="Generate" OnClick="GenerationOfTimetable_Click"/>
        <br />
        <asp:RadioButtonList ID="modeGeneration" runat="server">
            <asp:ListItem>Study Mode</asp:ListItem>
            <asp:ListItem>Training Mode</asp:ListItem>
            <asp:ListItem>Relax Mode</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />

        <p id="demo"></p>
    </div>
    

   

  

<script>


    var x = document.getElementById("demo");

    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            x.innerHTML = "Geolocation is not supported by this browser.";
        }
    }

    function showPosition(position) {
        x.innerHTML = "Latitude: " + position.coords.latitude +
            "<br>Longitude: " + position.coords.longitude;

        document.getElementById("<%=HiddenField1.ClientID%>").value = "1";
    }
    getLocation()
</script>


       

</asp:Content>
