<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditProfilePicture.aspx.cs" Inherits="FYP.EditProfilePicture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Profile.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table class="profileTable">
        <tr>
            <th colspan="3"><h1>Profile</h1></th>
        </tr>
        <tr>
            <td class="sideColumn leftCss" onclick="location.href='Profile.aspx';">
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
            <td class="sideColumn leftCss" onclick="location.href='UserPreference.aspx';">
                <asp:HyperLink ID="Preference" runat="server" NavigateUrl="~/UserPreference.aspx">Preference</asp:HyperLink>
            </td>
            <td class="middleColumn">&nbsp;</td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn leftCss" onclick="location.href='EditProfile.aspx';">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/EditProfile.aspx">Edit Profile</asp:HyperLink>
            </td>
            <td class="middleColumn">&nbsp;</td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn leftCss" onclick="location.href='EditProfilePicture.aspx';">
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
            <td class="middleColumn">
                <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" CssClass="button"/> <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required To Upload a photo." ControlToValidate="FileUpload1" ValidationGroup="va1" ForeColor="red" Font-Size="small" CssClass ="validator">

                </asp:RequiredFieldValidator>
                           
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        <tr>
            <td class="sideColumn">
                
            </td>
            <td class="middleColumn">
                <asp:Button ID="Change" runat="server" Text="Upload" ValidationGroup="va1" OnClick="Change_Click" CssClass ="button" PostBackUrl="~/EditProfilePicture.aspx"/>                           
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
