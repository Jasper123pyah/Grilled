using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<FavoriteModel> Favorites { get; set; }
    }
}
