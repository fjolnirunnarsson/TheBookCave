/*using Microsoft.EntityFrameworkCore;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        // Skipanir til að keyra eftir að við fáum DataBase link:
        // dotnet ef migrations add initialMigration
        // dotnet ef database update

        public DbSet<Book> Books { get; set; }

        // Skipanir eftir að Books hefur verið bætt við Database:
        // dotnet ef migrations add BooksTable_added
        // dotnet ef database update
        // Með þessu erum við að segja: Ég vil að gagnagrunnurinn uppfærist í samræmi við þetta migration. Nú ættu að vera komnar tvær töflur í gagnagrunninn.

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

