using Microsoft.AspNetCore.Mvc;
using Mission09_ea11160.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_ea11160.Controllers
{
    public class CheckOutController : Controller
    {
        private ICheckOutRepository repo { get; set; }
        private Cart cart { get; set; }

        // set up constructor with info for database
        public CheckOutController(ICheckOutRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Checkout());
        }

        [HttpPost]
        public IActionResult Checkout (Checkout checkout)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                checkout.Lines = cart.Items.ToArray();
                repo.SaveCheckout(checkout);
                cart.ClearBasket();

                return RedirectToPage("/CheckOutCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
