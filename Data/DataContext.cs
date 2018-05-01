/*using Microsoft.EntityFrameworkCore;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Data
{
    public class DataContext :DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "SERVER-LINK");
            )
    }
    
}*/


// Skipanir til að keyra eftir að við fáum DataBase link:
// dotnet ef migrations add initialMigration
// dotnet ef database update

