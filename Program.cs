using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;

namespace TheBookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            /* var host = BuildWebHost(args)
                SeedData();     // Þessi gæjir hleður gagnagrunninn.
                host.Run(); */

            // Uncomment þegar við erum komin með Database link
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    /*public static void SeedData()   // Fall sem er notað til að frumstilla gagnagrunninn, þ.e. bæta upprunalegum gögnum í hann.
    {
        var db = new DataContext();
        
        if (!db.Authors.Any())  // Ef hann er tómur þá bætum við þessum gæjum í hann, annars ekki. 
        {
        
        var initialAuthors = new List<Author>()
        {
                new Author { Name = "Ernest Hemingway" },       // Gagnagrunnurinn býr til Id fyrir okkur.
                new Author { Name = "Paul Coelho" },
                new Author { Name = "J.K. Rowling" }
        };

        db.AddRange(initialAuthors);
        db.SaveChanges();
        }
    }*/

    /*public static void SeedData()
    {
        var db = new DataContext();
        
        var initialBooks = new List<Book>()
        {
            new Book { Title = "The Cuckoo's Nest", AuthorId = 1 },
            new Book { Title = "The Alchemist", AuthorId = 2 },
            new Book { Title = "Harry Potter", AuthorId = 3 }
        };

        db.AddRange(initialBooks);
        db.SaveChanges();
    }*/
}
