using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_ea11160.Models
{
    public class Cart
    {
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();
        // add item to cart
        public virtual void AddItem(Book bk, int qty)
        {
            CartLineItem Line = Items
                    .Where(b => b.Book.BookId == bk.BookId)
                    .FirstOrDefault();
            if (Line == null)
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

        // grab instance of the book, remove it when the bookids match
        public virtual void RemoveItem(Book bk)
        {
            Items.RemoveAll(x => x.Book.BookId == bk.BookId);
        }

        // clear method to empty the whole basket
        public virtual void ClearBasket()
        {
            Items.Clear();
        }
        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 15);
            return sum;
        }
    }

    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
