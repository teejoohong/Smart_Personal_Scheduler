<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FYP.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 40%;
            text-align: right;
            height: 80px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/Register.css" rel="stylesheet" type="text/css" />
    <table style="width: 50%; margin: 50px auto 100px auto; border:1px solid black;">
        <tr style="background-color:black; color:white; text-align:center; height: 80px;font-size:18px;">
            <td  colspan="2"><h2>Register</h2></td>
        </tr>
       <tr class="rowInput">
            <td class="text">Username :</td>
            <td class="rowInput">
                <asp:TextBox id="txtUsername" runat="server" CssClass="inputBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username Required" ControlToValidate="txtUsername" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
                <tr class="rowInput">
            <td class="text">Email &nbsp:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Cssclass="form inputBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email can not be empty." Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                
                
            </td>
        </tr>
        <tr style="text-align:center">
            <td colspan="2"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email address" Font-Size="Small" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="rowInput">
            <td class="text">Gender &nbsp:</td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Selected="True">Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Gender Cannot be empty" ControlToValidate="ddlGender" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
          
        <tr class="rowInput">
            <td class="auto-style1">Password &nbsp:</td>
            <td class="rowInput">
                <asp:TextBox id="txtPassword" TextMode="Password" runat="server" CssClass="inputBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password Required" ControlToValidate="txtPassword" Font-Size="Small" ForeColor="Red" SetFocusOnError="True" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="rowInput">
            <td class="text">Confirm Password &nbsp:</td>
            <td>
                <asp:TextBox id="txtConfirmPassword" TextMode="Password" runat="server" CssClass="inputBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm Password Required" ControlToValidate="txtConfirmPassword" Font-Size="Small" ForeColor="Red" SetFocusOnError="True" ></asp:RequiredFieldValidator><br />
                  <asp:CompareValidator ID="comparePass" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Password miss-match, please re-enter the password" Font-Size="Small" ForeColor="Red" Operator="Equal" SetFocusOnError="True"></asp:CompareValidator>
            </td>
        </tr>
        <tr >
            <td  colspan="2" class="rowButtonLogin">
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="buttonLogin" OnClick="btnRegister_Click"/>
                
            </td>
        </tr>
       
        <tr >
            <td  colspan="2" class="rowButtonLogin">
               <asp:Label ID="lblDuplicate" runat="server" Font-Size="Small" ForeColor="Red" Text=""></asp:Label></td>
        </tr>
       
        <tr class="rowLink">
            <td>
                <asp:HyperLink ID="linkLogIn" runat="server" NavigateUrl="~/LogIn.aspx">Log In</asp:HyperLink>
            </td>
            <td class="linkForgetPassword" >
                <asp:HyperLink ID="linkForgetPassword" CssClass="" runat="server" >Forget Password</asp:HyperLink>
            </td>
        </tr>
    </table>

</asp:Content>
