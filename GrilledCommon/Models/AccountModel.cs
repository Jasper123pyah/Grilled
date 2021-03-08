using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrilledCommon.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<FavoriteModel> Favorites { get; set; }
        public List<ChatModel> SellChats { get; set; }
        public List<ChatModel> BuyChats { get; set; }
    }
}
