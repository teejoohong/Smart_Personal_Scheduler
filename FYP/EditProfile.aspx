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
            <td class="sideColumn ">
                
            </td>
            <td class="middleColumn" style="text-align:center">
                <span class="lbl" >Email :</span>
                <asp:TextBox ID="txtEmail" CssClass="txtBox" runat="server" ></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email address" Font-Size="Small" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                 <br /><asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email can not be empty." Font-Size="Small" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>
        
        <tr>
            <td class="sideColumn ">
                
            </td>
            <td class="middleColumn" style="text-align:center">
                <span class="lbl" >Age :</span>
                <asp:TextBox ID="txtAge" CssClass="txtBox" runat="server" ></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Age cannot be empty." ControlToValidate="txtAge" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                <br />
                <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Please input valid Age."  MinimumValue="0" MaximumValue="130" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtAge"></asp:RangeValidator>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>

        <tr>
            <td class="sideColumn ">
                
            </td>
            <td class="middleColumn" style="text-align:center">
                <span class="lbl" >Height :</span>
                <asp:TextBox ID="txtHeight" CssClass="txtBox" runat="server"  ></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Height cannot be empty." ControlToValidate="txtHeight" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                 <br />
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Please input valid height."  MinimumValue="120" MaximumValue="300" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtHeight"></asp:RangeValidator>
            </td>
            <td class="sideColumn">&nbsp;</td>
        </tr>

        <tr>
            <td class="sideColumn ">
                
            </td>
            <td class="middleColumn" style="text-align:center">
                <span class="lbl" >Weight :</span>
                <asp:TextBox ID="txtWeight" CssClass="txtBox" runat="server" ></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Weight cannot be empty." ControlToValidate="txtWeight" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator><br />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please input valid weight."  MinimumValue="15" MaximumValue="700" Type="Double"
                        Font-Size="Small" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtWeight"></asp:RangeValidator>
                 <br />
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
