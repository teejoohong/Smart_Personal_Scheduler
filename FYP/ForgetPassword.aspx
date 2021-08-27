<%@ Page Title="Forget Password" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="FYP.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    

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
                <asp:TextBox id="txtEmail" runat="server" CssClass="inputBox"></asp:TextBox>&nbsp
                
            </td>
        </tr>
        <tr style="text-align:center">
            <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email Required" ControlToValidate="txtEmail" Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <br /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email address" Font-Size="Small" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
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
            <br />
            <p>
                
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HomePage.aspx" Text="">The link has been expired. You will be redirected to homepage in 5 seconds. Click here to redirect now</asp:HyperLink>
            </p>
            <br />
        </asp:View>

        <asp:View ID="Delay" runat="server">
            <br />
                <p>
                    <asp:HyperLink ID="redirectHome" runat="server" NavigateUrl="~/HomePage.aspx" Text="">You will be redirected to homepage in 5 seconds. Click here to redirect now</asp:HyperLink>
                </p>
            <br />
        </asp:View>


    </asp:MultiView>

    <script>
    function delaySent() {
        var delay = 5000;
        setTimeout(function () {
            window.location.href = "HomePage.aspx";
        }, delay);
        alert("An link has been successfully sent out to an email if the email has been created before. Please check your inbox.");

        }

        function delaySavePass() {
            var delay = 5000;
            setTimeout(function () {
                window.location.href = "HomePage.aspx";
            }, delay);
            alert("The password has been reset.");

        }

    </script>
</asp:Content>