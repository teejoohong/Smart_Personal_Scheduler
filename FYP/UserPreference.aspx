<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserPreference.aspx.cs" Inherits="FYP.UserPreference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/UserPreference.css" rel="stylesheet" type="text/css" />

    <div>
        <p class="title">User Preference</p>
    
        <table  class="table">
           
            <tr>
                <td class="title">Oudoor Activity</td>
            </tr>
            <tr>
                <td>1. What is your favourite first outdoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList1" runat="server">
                      <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>basketball</asp:ListItem>
                        <asp:ListItem>football</asp:ListItem>
                        <asp:ListItem>futsal</asp:ListItem>
                        <asp:ListItem>jogging</asp:ListItem>
                        <asp:ListItem>running</asp:ListItem>
                        <asp:ListItem>cycling</asp:ListItem>
                        <asp:ListItem>tennis</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>2. What is your favourite second outdoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList2" runat="server">
                       <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>basketball</asp:ListItem>
                        <asp:ListItem>football</asp:ListItem>
                        <asp:ListItem>futsal</asp:ListItem>
                        <asp:ListItem>jogging</asp:ListItem>
                        <asp:ListItem>running</asp:ListItem>
                        <asp:ListItem>cycling</asp:ListItem>
                        <asp:ListItem>tennis</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>3. What is your favourite third outdoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList3" runat="server">
                       <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>basketball</asp:ListItem>
                        <asp:ListItem>football</asp:ListItem>
                        <asp:ListItem>futsal</asp:ListItem>
                        <asp:ListItem>jogging</asp:ListItem>
                        <asp:ListItem>running</asp:ListItem>
                        <asp:ListItem>cycling</asp:ListItem>
                        <asp:ListItem>tennis</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="title">Indoor Activity</td>
            </tr>
            <tr>
                <td>1. What is your favourite first indoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList4" runat="server">
                      
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>badminton</asp:ListItem>
                        <asp:ListItem>swimming</asp:ListItem>
                        <asp:ListItem>ping pong</asp:ListItem>
                        <asp:ListItem>gym</asp:ListItem>
                        <asp:ListItem>gymnastic</asp:ListItem>
                        <asp:ListItem>kungfu</asp:ListItem>
                        <asp:ListItem>volleyball</asp:ListItem>
                         <asp:ListItem>ropejumping</asp:ListItem>
                        <asp:ListItem>dancing</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>2. What is your favourite second indoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList5" runat="server">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>badminton</asp:ListItem>
                        <asp:ListItem>swimming</asp:ListItem>
                        <asp:ListItem>ping pong</asp:ListItem>
                        <asp:ListItem>gym</asp:ListItem>
                        <asp:ListItem>gymnastic</asp:ListItem>
                        <asp:ListItem>kungfu</asp:ListItem>
                        <asp:ListItem>volleyball</asp:ListItem>
                         <asp:ListItem>ropejumping</asp:ListItem>
                        <asp:ListItem>dancing</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>3. What is your favourite third indoor sport?</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList CssClass="list" ID="DropDownList6" runat="server">
                         <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>badminton</asp:ListItem>
                        <asp:ListItem>swimming</asp:ListItem>
                        <asp:ListItem>ping pong</asp:ListItem>
                        <asp:ListItem>gym</asp:ListItem>
                        <asp:ListItem>gymnastic</asp:ListItem>
                        <asp:ListItem>kungfu</asp:ListItem>
                        <asp:ListItem>volleyball</asp:ListItem>
                         <asp:ListItem>ropejumping</asp:ListItem>
                        <asp:ListItem>dancing</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="btnAlign">
                    <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" CssClass="btn"/> &nbsp&nbsp&nbsp
                     <asp:Button ID="save" runat="server" Text="Save" OnClick="save_Click" CssClass="btn"/>
                </td>
            </tr>
        </table>
        
        <br />
    </div>
</asp:Content>
