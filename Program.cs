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
using TheBookCave.Models.ViewModels;

namespace TheBookCave
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            SeedData();     // Þessi gæjir hleður gagnagrunninn.
            host.Run(); 
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void SeedData()   // Fall sem er notað til að frumstilla gagnagrunninn, þ.e. bæta upprunalegum gögnum í hann.
        {
            var db = new DataContext();
            
            /*if (!db.Books.Any())
            {*/
            
                var initialBooks = new List<Book>()
                {
                    
                };
            
                db.AddRange(initialBooks);
                db.SaveChanges();
            
        }
    }
}