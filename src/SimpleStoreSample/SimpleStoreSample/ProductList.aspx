<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="SimpleStoreSample.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                <h2><%:Page.Title %></h2>
            </hgroup>
            <asp:ListView ID="productList" runat="server" DataKeyNames="ProductID"
                 GroupItemCount="4" ItemType="SimpleStoreSample.Models.Product" SelectMethod="GetProducts">
                <%-- EMPTY DATA TEMPLATE --%>
                <EmptyDataTemplate>
                    <table >
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <%-- EMPTY ITEM TEMPLATE --%>
                <EmptyItemTemplate>
                    <td />
                </EmptyItemTemplate>

                <%-- GROUP TEMPLATE --%>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>

                <%-- ITEM TEMPLATE --%>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <a href="ProductDetails.aspx?productID=<%#: Item.ProductID %>">
                                        <img src="Catalog/Images/Thumbs/<%#: Item.ImagePath %>" width="100" height="75" style="border:solid" />
                                      
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="ProductDetails.aspx?productID=<%#: Item.ProductID %>">
                                        <span>
                                            <%#: Item.ProductName %>
                                        </span>
                                    </a>
                                    <br />
                                    <span>
                                        <strong>Price: </strong><%#: $"{Item.UnitPrice?.ToString("c")}" %>
                                    </span>
                                    <br />
                                    <a href="/AddToCart.aspx?productID=<%#:Item.ProductID %>">  
                                        <span class="ProductListItem">
                                            <b>Add to Cart</b>
                                        </span>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>

                <%-- LAYOUT TEMPLATE --%>
                <LayoutTemplate>
                    <table style="width:100%">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainter" runat="server" style="width:100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </section>
</asp:Content>
