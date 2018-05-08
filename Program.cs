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

            var cart = new Cart
            {
                CartId = "stefansdottir8@gmail.com", BookId =  22, Quantity = 1, DateCreated = DateTime.Now, 
                Book = new Book { Title = "Sample", Author = "Halldor Logi", Description = "SampleBook",
                           Year = 2018, Image = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/9978/9780997895827.jpg", 
                           Genre = "Biography", Rating = 4.56, Price = 20.42, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1 }
            };
            db.AddRange(cart);
            db.SaveChanges();
        }
    }
}

            /*var db = new DataContext();
                
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
                },
                new Book{
                    Title = "Steve Jobs", Author = "Walter Isaacson", Description = "Based on more than forty interviews with Jobs conducted over two years—as well as interviews with more than a hundred family members, friends, adversaries, competitors, and colleagues—Walter Isaacson has written a riveting story of the roller-coaster life and searingly intense personality of a creative entrepreneur whose passion for perfection and ferocious drive revolutionized six industries: personal computers, animated movies, music, phones, tablet computing, and digital publishing.",
                     Year = 2011, Image = "https://images-na.ssl-images-amazon.com/images/I/418oH6YjpFL.jpg", Genre = "Biography", Rating = 4.4, Price = 14.20, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Killing Forest", Author = "Sara Blaedel", Description = "Following an extended leave, Louise Rick returns to work at the Special Search Agency, an elite unit of the National Police Department. She's assigned a case involving a fifteen-year-old who vanished a week earlier. When Louise realizes that the missing teenager is the son of a butcher from Hvalsoe, she seizes the opportunity to combine the search for the teen with her personal investigation of her boyfriend's long-ago death.",
                     Year = 2016, Image = "https://images-na.ssl-images-amazon.com/images/I/51zSh1imxIL.jpg", Genre = "Fiction", Rating = 4.4, Price = 7.0, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Maker of Patterns", Author = "Freeman Dyson", Description = "Having penned hundreds of letters to his family over four decades, Freeman Dyson has framed them with the reflections made by a man now in his nineties. While maintaining that “the letters record the daily life of an ordinary scientist doing ordinary work,” Dyson nonetheless has worked with many of the twentieth century’s most renowned physicists, mathematicians, and intellectuals, so that Maker of Patterns presents not only his personal story but chronicles through firsthand accounts an exciting era of twentieth-century science.",
                     Year = 2018, Image = "https://images-na.ssl-images-amazon.com/images/I/417h1kgQPuL._SX330_BO1,204,203,200_.jpg", Genre = "Biography", Rating = 5, Price = 15.72, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Light of the Fireflies", Author = "Paul Pen", Description = "For his whole life, the boy has lived underground, in a basement with his parents, grandmother, sister, and brother. Before he was born, his family was disfigured by a fire. His sister wears a white mask to cover her burns. He spends his hours with his cactus, reading his book on insects, or touching the one ray of sunlight that filters in through a crack in the ceiling. Ever since his sister had a baby, everyone’s been acting very strangely. The boy begins to wonder why they never say who the father is, about what happened before his own birth, about why they’re shut away.",
                     Year = 2016, Image = "https://images-na.ssl-images-amazon.com/images/I/51ZM2Qv8x5L.jpg", Genre = "Fiction", Rating = 3.9, Price = 5.54, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Gray House", Author = "Mariam Petrosyan", Description = "The Gray House is an astounding tale of how what others understand as liabilities can be leveraged into strengths. Bound to wheelchairs and dependent on prosthetic limbs, the physically disabled students living in the House are overlooked by the Outsides. Not that it matters to anyone living in the House, a hulking old structure that its residents know is alive. From the corridors and crawl spaces to the classrooms and dorms, the House is full of tribes, tinctures, scared teachers, and laws—all seen and understood through a prismatic array of teenagers’ eyes.",
                     Year = 2017, Image = "https://images-na.ssl-images-amazon.com/images/I/51er7mbWBkL.jpg", Genre = "Children", Rating = 3.9, Price = 15.95, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The House by the River", Author = "Lena Manta", Description = "Theodora knows she can’t keep her five beautiful daughters at home forever—they’re too curious, too free spirited, too like their late father. And so, before each girl leaves the small house on the riverside at the foot of Mount Olympus, Theodora makes sure they know they are always welcome to return.",
                     Year = 2017, Image = "https://images-na.ssl-images-amazon.com/images/I/51XhM-nToHL.jpg", Genre = "Fiction", Rating = 4.3, Price = 14.95, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "A Higher Loyalty: Truth, Lies, and Leadership", Author = "James Comey", Description = "In his book, former FBI director James Comey shares his never-before-told experiences from some of the highest-stakes situations of his career in the past two decades of American government, exploring what good, ethical leadership looks like, and how it drives sound decisions. His journey provides an unprecedented entry into the corridors of power, and a remarkable lesson in what makes an effective leader.",
                     Year = 2018, Image = "https://images-na.ssl-images-amazon.com/images/I/415TMcCGi-L.jpg", Genre = "History", Rating = 4.8, Price = 16.64, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Educated", Author = "Tara Westover", Description = "Tara Westover grew up preparing for the End of Days, watching for the sun to darken, for the moon to drip as if with blood. She spent her summers bottling peaches and her winters rotating emergency supplies, hoping that when the World of Men failed, her family would continue on, unaffected. She hadn’t been registered for a birth certificate. She had no school records because she’d never set foot in a classroom, and no medical records because her father didn’t believe in doctors or hospitals. According to the state and federal government, she didn’t exist.",
                     Year = 2018, Image = "https://images-na.ssl-images-amazon.com/images/I/51kkgdDmtfL.jpg", Genre = "Education", Rating = 4.7, Price = 14.42, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Teaching to Transgress", Author = "Bell Hooks", Description = "In Teaching to Transgress,bell hooks--writer, teacher, and insurgent black intellectual--writes about a new kind of education, education as the practice of freedom.  Teaching students to 'transgress' against racial, sexual, and class boundaries in order to achieve the gift of freedom is, for hooks, the teacher's most important goal. bell hooks speaks to the heart of education today: how can we rethink teaching practices in the age of multiculturalism? What do we do about teachers who do not want to teach, and students who do not want to learn? How should we deal with racism and sexism in the classroom?",
                     Year = 1994, Image = "https://images-na.ssl-images-amazon.com/images/I/319y3q0RCzL._SX331_BO1,204,203,200_.jpg", Genre = "Education", Rating = 4.5, Price = 28.32, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "The Wounded Murderer", Author = "Stephen Randorf", Description = "Detective Bass was too kindhearted at times. An elderly veteran had been murdered. Detective Bass had the 9mm pistol issued during the Korean War and the female suspect’s confession. The case was about to be wrapped up, and then . . . . it wasn’t enough!",
                     Year = 2015, Image = "https://images-na.ssl-images-amazon.com/images/I/51N1nyAw9yL.jpg", Genre = "Crime", Rating = 3.2, Price = 4.30, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                },
                new Book{
                    Title = "Bones Don't Lie", Author = "Melinda Leigh", Description = "Private investigator Lance Kruger was just a boy when his father vanished twenty-three years ago. Since then he’s lived under the weight of that disappearance—until his father’s car is finally dredged up from the bottom of Grey Lake. It should be a time for closure, except for the skeleton found in the trunk. A missing person case gone cold has become one of murder, and Lance and attorney Morgan Dane must face the deadly past that’s risen to the surface.",
                     Year = 2018, Image = "https://images-na.ssl-images-amazon.com/images/I/51REf2zbwHL.jpg", Genre = "Crime", Rating = 4.7, Price = 6.65, Discount = 0, BoughtCopies = 0, Quantity = 100, AuthorId = 1
                }
            };
            
                db.AddRange(Books);
                db.SaveChanges();

        }
    }
}*/
