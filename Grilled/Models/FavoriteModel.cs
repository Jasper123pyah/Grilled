using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grilled.Data;
namespace Grilled.Models
{
    public class FavoriteModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public void AddFav(GrilledContext context, AccountModel account)
        {
            if (account.Favorites == null)
                account.Favorites = new List<FavoriteModel>();

            account.Favorites.Add(this);
            context.SaveChanges();
        }
    }
}
