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
                    <td colspan="2" style="text-align:center"><asp:Button ID="btnSave" CssClass="buttonLogin" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
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
            <table id="caloriesTable">
                <tr>
                    <td>
                       <div id="chartdiv" style="overflow-x: scroll;">
                            <asp:Chart ID="chartCalories" runat="server" CssClass="chartCalories" Width="500px" Height="500px" >
                                <Titles>  
                                    <asp:Title Text="Estimated Calory burn for event allocated"></asp:Title>  
                                </Titles>  
                                <Series>
                                    <asp:Series Name="Series1" YValueType="Int32" YValuesPerPoint="2">
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
                    <td style="vertical-align:top ; padding-top:3.5%">
                        <asp:Label ID="lblTitle" runat="server" Text="Estimated Activity Calories <br/>" CssClass="lblTitle"></asp:Label>
                        <asp:Label ID="lblActivityCalories" runat="server" Text="" CssClass="lblActivityCalories"></asp:Label>
                    </td>
                </tr>
            </table>


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
            $("#chartCalories").css("width", window_width + "px");
        });

        $(window).resize(function () {
            var window_width = $(window).width()/2;
            $("#chartdiv").css("width", window_width + "px");
            $("#chartCalories").css("width", window_width + "px");
        });
    </script>

</asp:Content>
