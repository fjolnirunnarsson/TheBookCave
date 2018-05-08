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

        // Skipanir til að keyra eftir að við fáum DataBase link:
        // dotnet ef migrations add initialMigration
        // dotnet ef database update

        public DbSet<Author> Authors { get; set; }
        
        // Skipanir eftir að Authors hefur verið bætt við Database:
        // dotnet ef migrations add AuthorsTable_added
        // dotnet ef database update
        // Með þessu erum við að segja: Ég vil að gagnagrunnurinn uppfærist í samræmi við þetta migration. Nú ættu að vera komnar tvær töflur í gagnagrunninn.

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        //public DbSet<CartItem> CartItems { get; set; }
        //public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H28;Persist Security Info=False;User ID=VLN2_2018_H28_usr;Password=meg@Ray14;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}


