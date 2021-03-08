using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrilledData
{
    public class AccountData
    {
        AccountModel account = new AccountModel();
        public AccountModel GetAccount(string accId, GrilledContext context)
        {
            account = context.Account.Include(a => a.Favorites)
                                     .Include(a => a.Products)
                                     .ThenInclude(b => b.Images)
                                     .FirstOrDefault(a => a.Id == accId);
            return account;
        }
        public List<ProductModel> GetItems(string accId, GrilledContext context)
        {           
            account = context.Account.Include(a => a.Favorites)
                                     .Include(a => a.Products)
                                     .ThenInclude(b => b.Images)
                                     .FirstOrDefault(a => a.Id == accId);
            List<ProductModel> productList = account.Products;
            return productList;
        }
        public string LoginCheck(LoginModel login, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == login.Username);
            if (account != null && login.Password == account.Password)
                return account.Id;
            return null;
        }
        public bool AddAccount(RegistrationModel registration, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == registration.Username);
            if (account == null)
            {
                context.Account.Add(new AccountModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = registration.Username,
                    Password = registration.Password,
                    Email = registration.Email
                });
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public void AddFavorite(string accId, ProductModel product, GrilledContext context)
        {
            account = context.Account.Include(a => a.Favorites)
                                     .FirstOrDefault(a => a.Id == accId);

            if (account.Favorites.FirstOrDefault(a => a.ProductId == product.Id) != null)
                context.Favorite.Remove(context.Favorite.Where(f => f.ProductId == product.Id).FirstOrDefault());

            else
            {
                FavoriteModel favorite = new FavoriteModel() { ProductId = product.Id };
                favorite.AddFavToAccount(account);
            }
            context.SaveChanges();

        }
        public List<ProductModel> GetAllFavorites(string accId, GrilledContext context)
        {
            account = context.Account.Include(a => a.Favorites)
                                     .Include(a => a.Products)
                                     .ThenInclude(b => b.Images)
                                     .FirstOrDefault(a => a.Id == accId);

            List<ProductModel> productList = new List<ProductModel>();
            if (account == null)
                return productList;
            if (account.Favorites == null)
                return productList;
            foreach (FavoriteModel favorite in account.Favorites)
            {
                ProductModel product = context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == favorite.ProductId);
                productList.Add(product);
            }
            return productList;
        }
    }
}
