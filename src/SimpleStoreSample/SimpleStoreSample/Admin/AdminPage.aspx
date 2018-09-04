<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="SimpleStoreSample.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administration</h1>
    <hr />
    <h3>Add products:</h3>
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="LabelAddCategory">Category:</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownAndCategory" runat="server"
                     ItemType="SimpleStoreSample.Models.Category" SelectMethod="GetCategories"
                     DataTextField="CategoryName" DataValueField="CategoryID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelAddName" runat="server">Name:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AddProductName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Text="* Product name required." ControlToValidate="AddProductName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>

</asp:Content>
