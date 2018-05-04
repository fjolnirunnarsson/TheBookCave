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
                /* 
                var Books = new List<Book>
                {
                new Book{
                    Title = "Goodnight Stories for Rebel Girls 2", Author = "Francesca Cavallo", Description = "Good Night Stories for Rebel Girls 2 is an entirely new collection of 100 more bedtime stories about extraordinary women from all over the world. It boasts a brand new graphic design + 100 incredible new portraits created by the best female artists of our time. Having a passionate community that spans across 70+ countries is a great way to discover incredible stories.The stories in Volume 2 are 100% stories you told us about. Now, we can all share them. We can't wait because they truly are breathtaking.",
                     Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/9978/9780997895827.jpg", Genre = "Biography", Rating = 4.56, Price = 20.42, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Subtle Art of Not Giving a F*ck", Author = "Mark Manson", Description = "In this generation-defining self-help guide, a superstar blogger cuts through the crap to show us how to stop trying to be 'positive' all the time so that we can truly become better, happier people.For decades, we've been told that positive thinking is the key to a happy, rich life. 'F**k positivity,' Mark Manson says. 'Let's be honest, shit is f**ked and we have to live with it.' In his wildly popular Internet blog, Manson doesn't sugarcoat or equivocate. He tells it like it is-a dose of raw, refreshing, honest truth that is sorely lacking today. The Subtle Art of Not Giving a F**k is his antidote to the coddling, let's-all-feel-good mindset that has infected modern society and spoiled a generation, rewarding them with gold medals just for showing up. ",
                    Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/0624/9780062457714.jpg", Genre = "Self Help", Rating = 4.01, Price = 14.18, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Tattooist of Auschwitz", Author = "Heather Morris", Description = "I tattooed a number on her arm. She tattooed her name on my heart. In 1942, Lale Sokolov arrived in Auschwitz-Birkenau. He was given the job of tattooing the prisoners marked for survival - scratching numbers into his fellow victims' arms in indelible ink to create what would become one of the most potent symbols of the Holocaust."
                    , Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/7857/9781785763649.jpg", Genre = "Historical FIction", Rating = 4.33, Price = 13.30, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Little Book of Hygge", Author = "Meik Wiking", Description = "The Danish word hygge is one of those beautiful words that doesn't directly translate into English, but it more or less means comfort, warmth or togetherness. Hygge is the feeling you get when you are cuddled up on a sofa with a loved one, in warm knitted socks, in front of the fire, when it is dark, cold and stormy outside. It that feeling when you are sharing good, comfort food with your closest friends, by candle light and exchanging easy conversation. It is those cold, crisp blue sky mornings when the light through your window is just right.", 
                    Year = 2016, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/2412/9780241283912.jpg", Genre = "Self Help", Rating = 3.72, Price = 11.71, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Macbeth", Author = "Jo Nesbo", Description = "Jo Nesbo sets his retelling of Shakespeare’s blood-drenched tragedy about a power-hungry couple in a grim, unnamed Scottish town that’s fallen on hard times. Although the time is specified as the 1970s – 25 years after the end of the Second World War, tendrils of both allegiances and betrayals from which still clutch at those left – there are few other details to date the scene, and the overall ambiance is distinctly dystopian. The town is plagued by unemployment – its factories now abandoned, its once magnificent railway now disused – and drug use had skyrocketed.", 
                    Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/7810/9781781090268.jpg", Genre = "Crime", Rating = 3.57 , Price = 19.78, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Twist of Faith", Author = "Ellen J. Green", Description = "When family secrets are unearthed, a woman's past can become a dangerous place to hide...After the death of her adoptive mother, Ava Saunders comes upon a peculiar photograph, sealed and hidden away in a crawl space. The photo shows a shuttered, ramshackle house on top of a steep hill. On the back, a puzzling inscription: Destiny calls us.", 
                    Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/5039/9781503949065.jpg", Genre = "Thriller", Rating = 3.73, Price = 23.85, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Quality of Silence", Author = "Rosamund Lupton", Description = "There are many things to love about Lupton's third novel, not least its stunning evocation of Alaska . . . The Quality of Silence is an elegant and icily unique thriller, you won't read anything like it this year. Alison Flood The Observer (thriller of the month)",
                    Year = 2013, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3494/9780349408125.jpg", Genre = "Fiction", Rating = 3.55, Price = 10.18, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Harry Potter and the Cursed Child - Parts I & II", Author = "J. K. Rowling", Description = "The Eighth Story. Nineteen Years Later. Based on an original new story by J.K. Rowling, Jack Thorne and John Tiffany, a new play by Jack Thorne, Harry Potter and the Cursed Child is the eighth story in the Harry Potter series and the first official Harry Potter story to be presented on stage. The play will receive its world premiere in London’s West End on 30th July 2016.",
                    Year = 2016, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/7515/9780751565355.jpg", Genre = "Fantasy", Rating = 3.71, Price = 23.23, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Fire and Blood : A History of the Targaryen Kings", Author = "George R. R. Martin", Description = "From the masterly imagination behind A Game of Thrones - one of the greatest fantasy epics of all time and an unmissable HBO hit series - comes a definitive history of Westeros’s past as told by Archmaester Gyldayn. Unravelling events that led to A Song of Ice and Fire, Fire and Blood is the first volume of the definitive two-part history of the Targaryens in Westeros. Revealing long-buried secrets and untold lasting enmity, it sets the scene for the heart-stopping series conclusion, The Winds of Winter.", 
                    Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/0083/9780008307738.jpg", Genre = "Fantasy", Rating = 3.66, Price = 27.15, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Theory of Everything", Author = "Stephen Hawking", Description = "Stephen Hawking is widely believed to be one of the worlds greatest minds: a brilliant theoretical physicist whose work helped to reconfigure models of the universe and to redefine whats in it. Imagine sitting in a room listening to Hawking discuss these achievements and place them in historical context. It would be like hearing Christopher Columbus on the New World.Hawking presents a series of seven lec-turescovering everything from big bang to black holes to string theorythat capture not only the brilliance of Hawkings mind but his characteristic wit as well.",
                    Year = 2007, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9788/1799/9788179925911.jpg", Genre = "Nature", Rating = 4.12, Price = 14.71, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                }
            };
            
                db.AddRange(Books);
                db.SaveChanges();
            */
            
        }
    }
}