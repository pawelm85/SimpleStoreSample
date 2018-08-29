using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleStoreSample
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Product> GetProducts([QueryString("id")] int? categoryId)
        {
            var _db = new SimpleStoreContext();
            IQueryable<Product> products = _db.Products;
            if(categoryId.HasValue && categoryId>0)
            {
                products = products.Where(p => p.CategoryID == categoryId);
            }
            return products;            
        }
    }
}