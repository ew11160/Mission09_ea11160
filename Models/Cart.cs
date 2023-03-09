using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_ea11160.Models
{
    public class Cart
    {
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();
        public void AddItem (Book bk, int qty)
        {
            CartLineItem Line = Items
                    .Where(b => b.Book.BookId == bk.BookId)
                    .FirstOrDefault();
            if(Line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = bk,
                    Quantity = qty
                });
            }
            else
            {
                Line.Quantity += qty;
            }
        }
        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 15);
            return sum;
        }

        public class CartLineItem
        {
            public int LineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }
        }
    }
}
