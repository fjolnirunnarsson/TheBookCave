using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db;

        public CartRepo()
        {
            _db = new DataContext();
        }

        public List<CartItem> CreateFakeItem()
        {
            var fakeItem = new CartItem
            {
                CartId = 1,
                Count = 1,
                BookItem = new Book { Id = 100, Title = "Stofuhiti", Author = "Bergur Ebbi", Year = 2017, 
                Image = "https://www.forlagid.is/wp-content/uploads/2017/04/Stofuhiti_72.jpg", Rating = 5, Price = 12.99, Genre = "Philosophy", BoughtCopies = 2, Description = "Skemmtileg", AuthorId = 23 }
            };

            var fakeItemList = new List<CartItem>();

            fakeItemList.Add(fakeItem);

            return fakeItemList;
        }

        public double CreateFakeTotalPrice()
        {
            var item = CreateFakeItem();
            var fakeTotal = item[0].BookItem.Price;
            return fakeTotal;
        }

        public ShoppingCartViewModel CreateFakeCart()
        {
            var fakeCart = new ShoppingCartViewModel();

            fakeCart.CartItems = CreateFakeItem();
            fakeCart.CartTotal = CreateFakeTotalPrice();
       
            return fakeCart;
        }


                public List<CartItem> CartItems { get; set; }
        public double CartTotal { get; set; }


        /*public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart()
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controllers controller)
        {
            return GetCart(controller.HttpContext)
        }*/
    }
}
