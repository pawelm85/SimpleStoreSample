using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace SimpleStoreSample.Models
{
    public class SimpleStoreContext : DbContext
    {
        public SimpleStoreContext():base("SimpleStoreConnection")
        {
            //InitializeDatabase();

        }

        //protected virtual void InitializeDatabase()
        //{
        //    if (!Database.Exists())
        //    {
        //        Database.Initialize(true);
        //        Database.SetInitializer(new ProductDatabaseInitializer());
        //        //new ProductDatabaseInitializer().Seed(this);
        //    }
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}