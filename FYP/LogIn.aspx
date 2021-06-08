<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="FYP.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="CSS/LogIn.css" rel="stylesheet" type="text/css" />

    <table class="loginTable">
        <tr class="loginTitle">
            <td colspan="2">Login</td>
        </tr>
        <tr class="rowInput">
            <td class="text">Username :</td>
            <td class="rowInput">
                <asp:TextBox id="txtUsername" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="rowInput">
            <td class="text">Password &nbsp:</td>
            <td>
                <asp:TextBox id="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr >
            <td  colspan="2" class="rowButtonLogin">
                <asp:Button ID="btnLogin" runat="server" Text="Log in" CssClass="buttonLogin"/>
            </td>
        </tr>
        <tr class="rowLink">
            <td>
                <asp:CheckBox ID="chkBoxRememberMe" Text="Remember Me" runat="server" />
            </td>
            <td>
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
