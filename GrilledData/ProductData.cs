using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrilledData
{
    public class ProductData
    {
        public List<ProductModel> GetAllProducts(GrilledContext context)
        {
            List<ProductModel> productList = new List<ProductModel>();
            foreach (ProductModel product in context.Product)
            {
                productList.Add(product);
            }
            return productList;
        }
        public ProductModel GetProduct(Guid productId, GrilledContext context)
        {
            return context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == productId);
        }
        public bool AddProduct(GrilledContext context, List<Image> images, ProductModel product, string accId)
        {
            AccountModel account = context.Account.FirstOrDefault(a => a.Id == accId);     

            if (account != null)
            {
                if (account.Products == null)
                    account.Products = new List<ProductModel>();

                account.Products.Add(new ProductModel()
                {
                    Name = product.Name,
                    Category = product.Category,
                    Designer = product.Designer,
                    Size = product.Size,
                    Color = product.Color,
                    Condition = product.Condition,
                    Description = product.Description,
                    Price = product.Price,
                    Shipping = product.Shipping,
                    Images = images,
                    OwnerName = account.Username
                });
                context.SaveChanges();

                return true;
            }
            return false;
        }
        public ProductModel EditProduct(ProductModel product, GrilledContext context)
        {
            ProductModel model = context.Product.FirstOrDefault(p => p.Id == product.Id);

            model.Name = product.Name;
            model.Condition = product.Condition;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Shipping = product.Shipping;

            context.SaveChanges();
            return product;
        }
        public void DeleteProduct(ProductModel product, GrilledContext context)
        {
            ProductModel model = context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == product.Id);
            context.Product.Remove(model);
            context.SaveChanges();
        }
    }
}
