using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrilledCommon.Models
{
    public class FavoriteModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public void AddFavToAccount( AccountModel account)
        {
            if (account.Favorites == null)
                account.Favorites = new List<FavoriteModel>();

            account.Favorites.Add(this);
        }
    }
}
