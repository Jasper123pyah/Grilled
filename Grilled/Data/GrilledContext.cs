using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grilled.Models;

namespace Grilled.Data
{
    public class GrilledContext : DbContext
    {
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<FavoriteModel> Favorite { get; set; }
        public GrilledContext(DbContextOptions options) : base(options) { }
    }
}
