<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="FYP.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="CSS/LogIn.css" rel="stylesheet" type="text/css" />
    <asp:MultiView ID="MultiView1" runat="server"> 
        <asp:View ID="inputView" runat="server">

                                                   
        
    <table class="loginTable" >
        <tr class="loginTitle">
            <td colspan="2"><h2>Forget Password</h2></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                &nbsp;</td>
        </tr>
        <tr class="rowInput">
            <td class="text">Email :</td>
            <td class="rowInput">
                <asp:TextBox id="txtEmail" runat="server" CssClass="inputBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email Required" ControlToValidate="txtEmail" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr >
            <td  colspan="2" class="rowButtonLogin">
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="buttonLogin" OnClick="btnSend_Click"  />

            </td>
        </tr>
        
        
    </table>
            </asp:View>

        <asp:View ID="ChangePassword" runat="server">

             <table class="loginTable" >
        <tr class="loginTitle">
            <td colspan="2"><h2>Forget Password</h2></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                &nbsp;</td>
        </tr>
       
        <tr class="rowInput">
            <td class="text">New password :</td>
            <td class="rowInput">
                <asp:TextBox id="TextBox2" runat="server" CssClass="inputBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password Required" ControlToValidate="TextBox2" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
                  <tr class="rowInput">
            <td class="text">Confirm Password :</td>
            <td class="rowInput">
                <asp:TextBox id="TextBox3" runat="server" CssClass="inputBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password Required" ControlToValidate="TextBox3" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr >
            <td  colspan="2" class="rowButtonLogin">
                <asp:Button ID="btnConfirm" runat="server" Text="Save" CssClass="buttonLogin" OnClick="btnConfirm_Click"  />

            </td>
        </tr>
        
        
    </table>
        </asp:View>

        <asp:View ID="exceedTime" runat="server">
            <p>The link has been expired. Please try again with a new link.</p>
        </asp:View>
        

    </asp:MultiView>

</asp:Content>