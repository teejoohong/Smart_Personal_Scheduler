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
                    <th colspan="2"><h2>Timetable</h2></th>
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
        <table style="width:100%">
            <tr>
                <td style="width:30%"></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <p><b><u>Generate your own timetable now!!!</u></b></p>
                    <asp:RadioButtonList ID="modeGeneration" runat="server" OnSelectedIndexChanged="modeGeneration_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem>Study Mode</asp:ListItem>
                    <asp:ListItem>Training Mode</asp:ListItem>
                    <asp:ListItem>Relax Mode</asp:ListItem>
                </asp:RadioButtonList>
                    
                </td>
                <td style="text-align:center">
                    <div id="chartdiv" runat="server" visible="false">
                    <asp:Chart ID="chartTotalActivities" runat="server" Width="400px" Height="241px">
                        <Titles>
                             <asp:Title Text="Total activities"></asp:Title>  
                        </Titles>
                        <Series>
                            <asp:Series Name="Series1"  YValueType="Int32" YValuesPerPoint="2" ChartType="Pie">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                 <AxisX Title="Activity"></AxisX>  
                                 <AxisY Title="Total"></AxisY>  
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                        </div>
                </td>
            </tr>
        </table>

        
         
        <asp:CheckBox ID="FileUploading" runat="server" OnCheckedChanged="FileUploading_CheckedChanged" text="Click here to include your ics file" AutoPostBack="true"/>
                    <br />
        <div id="fileUpload" runat="server" visible="false" >
            <br />
            <asp:FileUpload ID="timeTableFile" runat="server" Width="279px"/>
        </div>
         <br />
        <div runat="server" id="generateBtn" visible="false">
            <asp:Button runat="server" Text="Generate" OnClick="GenerationOfTimetable_Click" CssClass="btnGenerate" ID="Button2"/>

        </div>
        
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
