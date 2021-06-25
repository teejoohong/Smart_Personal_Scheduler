﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BMI.aspx.cs" Inherits="FYP.BMI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/BMI.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div >
    <table style="width: 37%;">
        <tr>
            <td >Weigth (kg)</td>
            <td ><asp:TextBox ID="weight" runat="server" Width="90%"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please insert valid number" ControlToValidate="weight" ForeColor="red" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate="weight" ForeColor="red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
           
        </tr>
        <tr>
            <td>Height (cm)</td>
            <td><asp:TextBox ID="height" runat="server" Width="90%"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please insert valid number" ControlToValidate="height" ForeColor="red" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate="height" ForeColor="red" SetFocusOnError="true"></asp:RequiredFieldValidator>
               

                </td>
           
        </tr>
        <tr>
            <td>BMI value</td>
            <td>
               <%-- <p id="value"></p></td>--%>
            
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            
        </tr>
    </table>
       
   <br />
        <%--<p id="validate1"></p>--%>
    <%--<input id="btnCalculate" type="button" value="Calculate"  OnClick="validate()"/>--%>
        <asp:Button ID="calculate" runat="server" Text="Calculate" OnClick="Button1_Click" CssClass="btnCalculate"/>
    
        <br />
    
        <br />
     </div>
   
    
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
     
    <script  type="text/javascript">
        function validate() {
            if (document.getElementById('<%= weight.ClientID %>').value == "" || document.getElementById('<%= height.ClientID %>').value == "") {
                document.getElementById('validate1').innerHTML = ("Please enter some value.").fontcolor("red");
            } 
        }
        function calculate() {
            var weight = document.getElementById('<%= weight.ClientID %>').value;
            var height = document.getElementById('<%= height.ClientID %>').value;
            height = height / 100;
            var bmi = weight / (height * height);
            document.getElementById('value').innerHTML = bmi;
            alertUser(bmi)
        }
        function alertUser() {
           
            
            document.getElementById('demo').innerHTML = bmi;
            if (bmi < 18) {
                swal("Underweight. You are not recommend to use training mode for your timetable.");
            } else if (bmi > 18) {
                swal("Nice. You have a normal BMI value. Please keep it.");
            } else {
                swal("Overweight. You are recommend to use training mode on your timetable.");
            }
            
        }
    </script>
</asp:Content>
