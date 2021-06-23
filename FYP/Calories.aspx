<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Calories.aspx.cs" Inherits="FYP.Calories" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/Calories.css" rel="stylesheet" type="text/css" />
    <asp:MultiView ID="caloriesMultiView" runat="server">
        <asp:View ID="inputView" runat="server">
            <table id="inputForm" class="inputForm">
                <tr>
                    <th colspan="2"><h2>Calories</h2></th>
                </tr>
               <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:right" >Weight (KG) :</td>
                    <td>
                        <asp:TextBox ID="txtWeight" CssClass="txtWeight" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Weight cannot be empty." ControlToValidate="txtWeight" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please input valid weight."  MinimumValue="15" MaximumValue="700" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtWeight"></asp:RangeValidator>
                    </td>
                </tr>
                                <tr>
                    <td style="text-align:right" >Height(cm) :</td>
                    <td>
                        <asp:TextBox ID="txtHeight" CssClass="txtWeight" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Height cannot be empty." ControlToValidate="txtHeight" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Please input valid height."  MinimumValue="120" MaximumValue="300" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtHeight"></asp:RangeValidator>
                    </td>
                </tr>
                                <tr>
                    <td style="text-align:right" >Age (years) :</td>
                    <td>
                        <asp:TextBox ID="txtAge" CssClass="txtWeight" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Age cannot be empty." ControlToValidate="txtAge" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Please input valid Age."  MinimumValue="0" MaximumValue="130" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtAge"></asp:RangeValidator>
                    </td>
                </tr>
                <tr> 
                    <td  style="text-align:center"><asp:Button ID="btnCancel" CssClass="buttonLogin" runat="server" Text="Cancel" width="40%" OnClick="btnCancel_Click" ValidationGroup="none"/></td>
                    <td  style="text-align:center"><asp:Button ID="btnSave" CssClass="buttonLogin" runat="server" Text="Save" width="40%" OnClick="btnSave_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="loginView" runat="server">
           <table id="loginForm" class="inputForm">
                <tr>
                    <th colspan="2"><h2>Calories</h2></th>
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
        </asp:View>

        <asp:View ID="caloriesView" runat="server">
            <div class="caloriesSection">
                  <h1 class="caloriesHeader">Calories</h1>
            
                    <table class="caloriesTable">
                       <tr style="height:500px ; vertical-align:top ; text-align:center">
                            <td colspan="2">
                                <h3>Estimate your needed calories</h3>
                                <asp:DropDownList ID="ddlWork" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged" AutoPostBack="true" >
                                    <asp:ListItem Value="1.0">Basal metabolic rate</asp:ListItem>
                                    <asp:ListItem Value="1.1">Bed rest (Bed ridden - Unconscious)</asp:ListItem>
                                    <asp:ListItem Value="1.2">Sedentary (Little to no exercise )</asp:ListItem>
                                    <asp:ListItem Value="1.3">Light exercise (1-3 days per week)</asp:ListItem>
                                    <asp:ListItem Value="1.5">Moderate exercise (3-5 days per week)</asp:ListItem>
                                    <asp:ListItem Value="1.7">Heavy exercise (6-7 days per week)</asp:ListItem>
                                    <asp:ListItem Value="1.9">Very heavy exercise (twice per day, extra heavy workouts)</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList><br /><br />
                                <asp:Button ID="btnWeight" runat="server" Text="Change Info" CssClass="btnInfo" Width="50%" Height="60px" OnClick="btnWeight_Click" /><br /><br />
                                <div id="resultContainer">
                                    <h3 style="text-decoration:underline">Result</h3>
                                    <div style="text-align:left; padding-left:1%">
                                        Calories needed / day : &nbsp
                                        <asp:Label ID="lblEstimation" runat="server" CssClass="lblEstimation" Text=""></asp:Label><br /><br />
                                
                                        <asp:Label ID="lblWeightLostMild" runat="server" Text=""></asp:Label><br /><br />
                               
                                        <asp:Label ID="lblWeightLostMed" runat="server" Text=""></asp:Label><br /><br />
                               
                                        <asp:Label ID="lblWeightLostMax" runat="server" Text=""></asp:Label><br />
                                    </div>   
                                </div>
                        
                            </td>
                        </tr>
                    </table>
            </div>
            <div class="aCalSection">
                <h1 class="caloriesHeader" style="margin : 50px auto 50px auto">Allocated Event's Calories</h1>
            <table class="caloriesTable">
                <tr>
                    <td>
                       <div id="chartdiv" style="overflow-x: scroll;">
                            <asp:Chart ID="chartCalories" runat="server" CssClass="chartCalories" Width="760px" Height="500px" >
                                <Titles>  
                                    <asp:Title Text="Estimated Calory burn for event allocated"></asp:Title>  
                                </Titles>  
                                <Series>
                                    <asp:Series Name="Series1" YValueType="Int32" YValuesPerPoint="4">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX Title="Activity"></AxisX>  
                                        <AxisY Title="Calories"></AxisY>  
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </td>
                    <td style="vertical-align:top ; padding-left:2% ;">
                        <asp:Label ID="lblTitle" runat="server" Text="Total Estimated Activity Calories <br/>" CssClass="lblTitle"></asp:Label>
                        <asp:Label ID="lblActivityCalories" runat="server" Text="" CssClass="lblActivityCalories"></asp:Label><br /><br />
                    </td>
                </tr>

                <!--Second chart -->
                <tr style="height : 700px">
                    <td >
                        <div id="chartdiv1"  style="overflow-x: scroll;">
                            <asp:Chart ID="chartEachEvent" runat="server" CssClass="chartCalories" Width="760px" Height="500px" >
                                <Titles>  
                                    <asp:Title Text="Estimated Calory burn for event allocated"></asp:Title>  
                                </Titles>  
                                <Series>
                                    <asp:Series Name="Series1" YValueType="Int32" YValuesPerPoint="4">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX Title="Activity"></AxisX>  
                                        <AxisY Title="Calories"></AxisY>  
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                      
                    </td>
                    <td style="vertical-align:top ; padding-top: 5.8% ;padding-left:2% ;">
                         <asp:Label ID="Label1" runat="server" Text="Estimated Activity Calories<br/>" CssClass="lblTitle"></asp:Label>
                        <asp:Label ID="lblEachActivity" runat="server" Text="" CssClass="lblActivityCalories"></asp:Label><br /><br />
                        
                    </td>
                </tr>

            </table>
            </div>
            
          
        </asp:View>

        <asp:View ID="timetableView" runat="server">
           <table id="timetableForm" class="inputForm">
                <tr>
                    <th colspan="2"><h2>Calories</h2></th>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr style="text-align:center">
                    <td colspan="2">
                        <asp:Button ID="btnTimetable" runat="server" Text="Generate" CssClass="buttonLogin" OnClick="btnTimetable_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>

    <script class="code" language="javascript" type="text/javascript">
        $(document).ready(function () {
            var window_width = $(window).width()/2;
            $("#chartdiv").css("width", window_width + "px");
            $("#chartdiv1").css("width", window_width + "px");
        });

        $(window).resize(function () {
            var window_width = $(window).width()/2;
            $("#chartdiv").css("width", window_width + "px");
            $("#chartdiv1").css("width", window_width + "px");
        });
    </script>

</asp:Content>
