using Microsoft.EntityFrameworkCore;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Purchased> Purchased { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H28;Persist Security Info=False;User ID=VLN2_2018_H28_usr;Password=meg@Ray14;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}


