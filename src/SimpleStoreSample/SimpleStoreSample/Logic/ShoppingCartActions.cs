using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreSample.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        private string ShoppingCartId { get; set; }
        private SimpleStoreContext _db = new SimpleStoreContext();
        public readonly string CartSessionKey = "CartId";

        public void AddToCart(int id)
        {
            // Retrieve the product from the database.
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId 
                && c.ProductId == id);

            if(cartItem==null)
            {
                // Create new cart item if no cart item exists.
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exists in the cart,
                //then add one to the quantity
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public string GetCartId()
        {
            if(HttpContext.Current.Session[CartSessionKey]==null)
            {
                if(!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }


        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return _db.ShoppingCartItems.Where(c => c.CartId == ShoppingCartId).ToList();
        }


        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();
            // Multiple product price by quantity of that product to get the current price for each of those 
            // products in the cart.
            // Sum all product price totals to get the cart total
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Quantity * cartItems.Product.UnitPrice).Sum();
            return total ?? decimal.Zero;
        }


        public ShoppingCartActions GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }


        public void UpdateShoppingCartDatabase(string cartId, ShoppingCartUpdates[] cartItemUpdates)
        {
            using (var db = new SimpleStoreContext())
            {
                try
                {
                    int cartItemCount = cartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach(var cartItem in myCart)
                    {
                        // Iterate through all rows within shopping cart list.
                        for(int i=0; i < cartItemCount; i++)
                        {
                            // Check if product in cart is on the cartItemUpdates.
                            if(cartItem.Product.ProductID == cartItemUpdates[i].ProductId)
                            {
                                if (cartItemUpdates[i].PurchaseQuantity < 1 || cartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductId);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductId, cartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }catch(Exception e)
                {
                    throw new Exception($"ERROR: Unable to update cart database -{e.Message}", e);
                }
            }
        }



        private void UpdateItem(string updateCartId, int productId, int purchaseQuantity)
        {
            using (var db = new SimpleStoreContext())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCartItems where c.CartId == updateCartId && c.Product.ProductID == productId select c).FirstOrDefault();
                    if(myItem!=null)
                    {
                        myItem.Quantity = purchaseQuantity;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"ERROR: Unable to update CartItem {e.Message}", e);
                }
            }
        }

        private void RemoveItem(string cartId, int productId)
        {
            using (var db = new SimpleStoreContext())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCartItems where c.CartId == cartId && c.Product.ProductID == productId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        db.ShoppingCartItems.Remove(myItem);
                        db.SaveChanges();
                    }
                }
                catch(Exception e)
                {
                    throw new Exception($"ERROR: Unable to remove cart item {e.Message}", e);
                }
            }
        }


        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            using (var db = new SimpleStoreContext())
            {
                db.Entry(new CartItem { CartId = ShoppingCartId }).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public int GetCount()
        {
            ShoppingCartId = GetCartId();
            int? count = (from cartItem in _db.ShoppingCartItems where cartItem.CartId == ShoppingCartId select (int?)cartItem.Quantity).Sum();
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public int ProductId { get; set; }
            public int PurchaseQuantity { get; set; }
            public bool RemoveItem { get; set; }
        }



        public void MigrateCart(string cartId, string username)
        {
            using (var db = new SimpleStoreContext())
            {
                var shoppingCart = db.ShoppingCartItems.Where(c => c.CartId == cartId);
                foreach(CartItem item in shoppingCart)
                {
                    item.CartId = username;
                }
                HttpContext.Current.Session[CartSessionKey] = username;
                db.SaveChanges();
            }
        }



        public void Dispose()
        {
            if(_db!=null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }

  
}