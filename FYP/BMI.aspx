<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BMI.aspx.cs" Inherits="FYP.BMI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/BMI.css" rel="stylesheet" type="text/css" />
    <div >
    <table style="width: 37%;">
        <tr>
            <td >Weigth (kg)</td>
            <td ><asp:TextBox ID="weight" runat="server"></asp:TextBox></td>
           
        </tr>
        <tr>
            <td>Height (cm)</td>
            <td><asp:TextBox ID="height" runat="server"></asp:TextBox></td>
           
        </tr>
        <tr>
            <td>BMI value</td>
            <td>
                <p id="value"></p></td>
            
        </tr>
    </table>
    <br />
    <input id="btnCalculate" type="button" value="Calculate"  OnClick="calculate()"/>
     </div>
    <br />
    
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
     
    <script  type="text/javascript">
        function calculate() {
            var weight = document.getElementById('<%= weight.ClientID %>').value;
            var height = document.getElementById('<%= height.ClientID %>').value;
            height = height / 100;
            var bmi = weight / (height * height);
            document.getElementById('value').innerHTML = bmi;
            alertUser(bmi)
        }
        function alertUser(bmi) {
            if (bmi < 18) {
                swal("Underweight.");
            } else if (bmi > 18) {
                swal("Nice.");
            } else {
                swal("Overweight.");
            }
            
        }
    </script>
</asp:Content>
