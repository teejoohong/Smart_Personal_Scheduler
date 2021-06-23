<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="FYP.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="CSS/Profile.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table class="profileTable">
        <tr>
            <th colspan="3"><h1>Profile</h1></th>
        </tr>
        <tr>
            <td class="sideColumn leftCss" >
                <asp:HyperLink ID="linkProfile" runat="server" NavigateUrl="~/Profile.aspx">Profile</asp:HyperLink>
            </td>
            <td class="middleColumn" rowspan="3">

                            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="DataList1_ItemDataBound" CssClass="item">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" Width="200"  Height="250" CssClass="profilePic" runat="server" ImageUrl='<%# Eval("ProfilePicture") %>' />
                                    
                                    <br />
                                    <br />

                                </ItemTemplate>

                            </asp:DataList>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn leftCss">
                <asp:HyperLink ID="Preference" runat="server" NavigateUrl="~/UserPreference.aspx">Preference</asp:HyperLink>
            </td>
            <td class="middleColumn">&nbsp;</td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn leftCss">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/EditProfile.aspx">Edit Profile</asp:HyperLink>
            </td>
            <td class="middleColumn">&nbsp;</td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn leftCss">
                 <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/EditProfilePicture.aspx">Edit Profile Picture</asp:HyperLink>
            </td>
            <td class="middleColumn">
                <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label><br /><hr />
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>

        <tr>
            <td class="sideColumn leftCss">
                
            </td>
            <td class="middleColumn" style="text-align:left">
                <span class="lbl" >Email :</span>
                <asp:TextBox ID="txtEmail" CssClass="txtBox" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email address" Font-Size="Small" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                 <br /><asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email can not be empty." Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn">
                
            </td>
            <td class="middleColumn" >
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click"/>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
    </table>

                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
                         <SelectParameters>
                             <asp:SessionParameter Name="UserID" SessionField="UserID" />     
                        </SelectParameters>
                </asp:SqlDataSource>
</asp:Content>
