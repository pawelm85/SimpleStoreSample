using SimpleStoreSample.Logic;
using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
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
                }
            }
        }

        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            return actions.GetCartItems();
        }
    }
}