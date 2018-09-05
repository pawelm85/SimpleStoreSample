using SimpleStoreSample.Logic;
using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleStoreSample.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["ProductAction"];
            if(productAction=="add")
            {
                LabelAddStatus.Text = "Product added!";
            }
            if(productAction=="remove")
            {
                LabelRemoveStatus.Text = "ProductRemoved";
            }
        }


        protected void AddProductButton_Click(object sender, EventArgs e)
        {
            bool fileOK = false;
            string path = Server.MapPath("~/Catalog/Images/");
            if (ProductImage.HasFile)
            {
                string fileExtension = Path.GetExtension(ProductImage.FileName).ToLower();
                string[] allowedExtensions = { ".gif", ".jpg", ".png", "jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        break;
                    }
                }
            }

            if(fileOK)
            {
                try
                {
                    // Save to image folder.
                    ProductImage.PostedFile.SaveAs($"{path}{ProductImage.FileName}");

                    // Save to Image/Thumb folder
                    ProductImage.PostedFile.SaveAs($"{path}Thumbs/{ProductImage.FileName}");
                }
                catch(Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }


                // Adding product to DB
                AddProducts products = new AddProducts();
                bool addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text, AddProductPrice.Text, DropDownAndCategory.SelectedValue, ProductImage.FileName);

                if(addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect($"{pageUrl}?ProductAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Unable to add new product to the database";
                }
            }
            else
            {
                LabelAddStatus.Text = "Unable to accept file type";

            }
        }


        public IQueryable GetCategories()
        {
            using (var db = new SimpleStoreContext())
            {
                IQueryable query = db.Categories;
                return query;
            }
        }

        public IQueryable GetProducts()
        {
            using (var db = new SimpleStoreContext())
            {
                IQueryable query = db.Products;
                return query;
            }
        }

        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            using (var db = new SimpleStoreContext())
            {
                int productId = Convert.ToInt32(DropDownRemoveProduct.SelectedValue);
                var myItem = (from c in db.Products where c.ProductID == productId select c).FirstOrDefault();

                if(myItem!=null)
                {
                    db.Products.Remove(myItem);
                    db.SaveChanges();

                    // Reaload the page
                    string pageUrl = Request.Url.AbsolutePath.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect($"{pageUrl}?ProductAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Unable to locate product";
                }
            }
        }
    }
}