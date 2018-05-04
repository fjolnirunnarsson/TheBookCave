using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class ShoppingCartController : Controller
    {
        private CartService _cartService;
        
        public IActionResult Index()
        {
            
            var cart = _cartService.CreateFakeCart();
            return View(cart);
            
        }

        public ShoppingCartController()
        {
            _cartService = new CartService();
        }
    }
}
