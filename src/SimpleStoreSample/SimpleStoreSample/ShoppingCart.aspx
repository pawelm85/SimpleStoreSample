<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="SimpleStoreSample.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ShoppingCartTitle" runat="server" class="ContentHead">
        <h1>Shopping Cart</h1>
    </div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="false" ShowFooter="true" GridLines="Vertical" CellPadding="4"
        ItemType="SimpleStoreSample.Models.CartItem" SelectMethod="GetShoppingCartItems"
        CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" />
            <asp:BoundField DataField="Product.ProductName" HeaderText="Name" />
            <asp:BoundField DataField="Product.UnitPrice" HeaderText="Unit Price" DataFormatString="{0:c}" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Total">
                <ItemTemplate>
                    <%#: ((Convert.ToDouble(Item.Quantity)) * (Convert.ToDouble(Item.Product.UnitPrice))).ToString("c") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove">
                <ItemTemplate>
                    <asp:CheckBox ID="Remove" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelTotalText" runat="server" Text="Order Total:"></asp:Label>
            <asp:Label ID="lblTotal" runat="server"  EnableViewState="false"></asp:Label>
        </strong>
    </div>
    <br />

    <table>
        <tr>
            <td>
                <asp:Button ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
            </td>
            <td>
                <asp:ImageButton runat ="server" ID="CheckoutImageButton" ImageUrl="https://static.antyweb.pl/uploads/2016/02/Paypal-logo-20141-1420x670.png"
                        Width="145" AlternateText="Check out with PayPal" OnClick="CheckoutBtn_Click" BackColor="Transparent" BorderWidth="0" />
            </td>
        </tr>
    </table>
</asp:Content>
