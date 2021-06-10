<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TimeTableGenerator.aspx.cs" Inherits="FYP.TimeTableGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <div>
        
        
       
         <asp:Button runat="server" Text="Generate" OnClick="GenerationOfTimetable_Click"/>
        <br />
        <asp:CheckBox ID="FileUploading" runat="server" OnCheckedChanged="FileUploading_CheckedChanged" text="Click to include ics file upload" AutoPostBack="true"/>
             <br />
        <div id="fileUpload" runat="server" visible="false">
            <asp:FileUpload ID="timeTableFile" runat="server" />
        </div>
            

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
        document.getElementById("<%=HiddenField1.ClientID%>").value = position.coords.latitude;
        document.getElementById("<%=HiddenField2.ClientID%>").value = position.coords.latitude;
    }
    getLocation()
</script>


       

</asp:Content>
