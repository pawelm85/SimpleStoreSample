using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace SimpleStoreSample.Models
{
    public class SimpleStoreContext :DbContext
    {
        public SimpleStoreContext() : base("SimpleStoreSample")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}