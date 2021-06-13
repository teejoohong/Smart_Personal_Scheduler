<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </p>
     <asp:DropDownList CssClass="list" ID="DropDownList6" runat="server">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>Badminton</asp:ListItem>
                        <asp:ListItem>Swimming</asp:ListItem>
                        <asp:ListItem>Ping Pong</asp:ListItem>
                        <asp:ListItem>GYM</asp:ListItem>
                    </asp:DropDownList>
    
</asp:Content>
