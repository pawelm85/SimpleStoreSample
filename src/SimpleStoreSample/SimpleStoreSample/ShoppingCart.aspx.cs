using SimpleStoreSample.Logic;
using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleStoreSample
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ShoppingCartActions userShoppingCart = new ShoppingCartActions())
            {
                decimal cartTotal = 0;
                cartTotal = userShoppingCart.GetTotal();
                if(cartTotal > 0)
                {
                    // Display total value.
                    lblTotal.Text = cartTotal.ToString("c");
                }
                else
                {
                    LabelTotalText.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                    ShoppingCartTitle.InnerText = "Shopping cart is empty.";
                    UpdateBtn.Visible = false;
                }
            }
        }

        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            return actions.GetCartItems();
        }



        public List<CartItem> UpdateCartItems()
        {
            using (ShoppingCartActions userShoppingCart = new ShoppingCartActions())
            {
                string cartId = userShoppingCart.GetCartId();
                int cartLinesCount = CartList.Rows.Count;
                ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[cartLinesCount];
                for (int i = 0; i < cartLinesCount; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                    cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text);
                }

                userShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = userShoppingCart.GetTotal().ToString("c");
                return userShoppingCart.GetCartItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach(DataControlFieldCell cell in row.Cells)
            {
                if(cell.Visible)
                {
                    // Extract values  from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }
    }
}