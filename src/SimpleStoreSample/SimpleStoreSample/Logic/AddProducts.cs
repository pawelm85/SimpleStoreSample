using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStoreSample.Logic
{
    public class AddProducts
    {
        internal bool AddProduct(string productName, string productDesc, string productPrice, string productCategory, string productImagePath)
        {
            var myProduct = new Product
            {
                ProductName = productName,
                Description = productDesc,
                UnitPrice = Convert.ToDouble(productPrice),
                ImagePath = productImagePath,
                CategoryID = Convert.ToInt32(productCategory)
            };

            using (var db = new SimpleStoreContext())
            {
                db.Products.Add(myProduct);
                db.SaveChanges();
            }
            return true;
        }
    }
}