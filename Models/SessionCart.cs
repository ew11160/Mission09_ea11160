using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission09_ea11160.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission09_ea11160.Models
{
    // create independent object to hold session information; also use services
    public class SessionCart : Cart // inherit from Cart model
    {
        public static Cart GetCart(IServiceProvider services) // retrieves service object
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // old session or new?
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        [JsonIgnore] // prevents from being serialized or unserialized
        public ISession Session { get; set; }
        public override void AddItem(Book bk, int qty)
        {
            base.AddItem(bk, qty);
            Session.SetJson("Cart", this); // refers to current instance of class
        }

        public override void RemoveItem(Book bk)
        {
            base.RemoveItem(bk);
            Session.SetJson("Cart", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Cart");
        }

        // we only bring in the things we want to add or change
    }
}
