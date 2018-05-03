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
            
            if (!db.Books.Any())
            {
            
                var initialBooks = new List<Book>()
                {
                        new Book { Image = "https://img1.od-cdn.com/ImageType-400/1523-1/CE6/AD9/B9/%7BCE6AD9B9-6435-45E8-9A0E-74DCB44E6E4F%7DImg400.jpg", Title = "Cuckoo's Nest", Author = "Ken Kesey", Rating = 4.3, Price = 23.99, Genre = "Fiction", 
                        Description = "Proin sagittis commodo molestie. Sed ac tempus nibh, et fermentum arcu. Duis ut congue turpis. Sed vitae mi molestie, posuere massa ut, elementum mauris. Etiam blandit, velit eget lacinia feugiat, mauris mauris lacinia magna, non dignissim diam ligula in est."},
                        new Book { Image = "https://images-na.ssl-images-amazon.com/images/I/41MeC94AxIL._SX324_BO1,204,203,200_.jpg", Title = "The Alchemist", Author = "Paul Coelho", Rating = 4.7, Price = 19.99, Genre = "Philosophy", 
                        Description = "Proin sagittis commodo molestie. Sed ac tempus nibh, et fermentum arcu. Duis ut congue turpis. Sed vitae mi molestie, posuere massa ut, elementum mauris. Etiam blandit, velit eget lacinia feugiat, mauris mauris lacinia magna, non dignissim diam ligula in est."},
                        new Book { Image = "https://www.forlagid.is/wp-content/uploads/2017/04/Stofuhiti_72.jpg", Title = "Stofuhiti", Author = "Bergur Ebbi", Rating = 4.1, Price = 14.99, Genre = "Philosophy", 
                        Description = "Proin sagittis commodo molestie. Sed ac tempus nibh, et fermentum arcu. Duis ut congue turpis. Sed vitae mi molestie, posuere massa ut, elementum mauris. Etiam blandit, velit eget lacinia feugiat, mauris mauris lacinia magna, non dignissim diam ligula in est."}
                };
            

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}