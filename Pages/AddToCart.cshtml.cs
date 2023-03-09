using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_ea11160.Infrastructure;
using Mission09_ea11160.Models;

namespace Mission09_ea11160.Pages
{
    public class AddToCartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }
        public AddToCartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // find book associated with the ida
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // check if they have a session
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            // set json file based on cart so we keep it per person
            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
