<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="FYP.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="CSS/LogIn.css" rel="stylesheet" type="text/css" />

    <table class="loginTable" >
        <tr class="loginTitle">
            <td colspan="2"><h2>Login</h2></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <img alt="person" src="BuildInPicturess/baseline_person_black_48dp.png" style="width:20%;height:140px; margin-top:5px"/></td>
        </tr>
        <tr class="rowInput">
            <td class="text">Username :</td>
            <td class="rowInput">
                <asp:TextBox id="txtUsername" runat="server" CssClass="inputBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username Required" ControlToValidate="txtUsername" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="rowInput">
            <td class="text">Password &nbsp:</td>
            <td>
                <asp:TextBox id="txtPassword" runat="server" TextMode="Password" CssClass="inputBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password Required" ControlToValidate="txtPassword" Font-Size="Small" ForeColor="Red" SetFocusOnError="True" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr >
            <td  colspan="2" class="rowButtonLogin">
                <asp:Button ID="btnLogin" runat="server" Text="Log in" CssClass="buttonLogin" OnClick="btnLogin_Click"/>

            </td>
        </tr>
        <tr class="rowLink">
            <td>
                <asp:CheckBox ID="chkBoxRememberMe" Text="Remember Me" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblError" runat="server" Font-Size="Small" ForeColor="red" Text="" ></asp:Label>
                </td>
        </tr>
        <tr class="rowLink">
            <td>
                <asp:HyperLink ID="linkRegister" runat="server" NavigateUrl="~/Register.aspx">Register</asp:HyperLink>
            </td>
            <td class="linkForgetPassword" >
                <asp:HyperLink ID="linkForgetPassword" CssClass="" runat="server" >Forget Password</asp:HyperLink>
            </td>
        </tr>
    </table>

</asp:Content>
