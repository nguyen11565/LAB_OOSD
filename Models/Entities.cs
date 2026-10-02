using System;
using System.Collections.Generic;

namespace eShopping.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
        public string TechnicalSpecs { get; set; }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => Product.CurrentPrice * Quantity;
    }

    public class Cart
    {
        public List<CartItem> Items { get; } = new List<CartItem>();

        public decimal TotalAmount
        {
            get
            {
                decimal sum = 0;
                foreach (var item in Items) sum += item.SubTotal;
                return sum;
            }
        }

        public void AddItem(Product p, int qty)
        {
            var found = Items.Find(i => i.Product.ProductId == p.ProductId);
            if (found != null)
                found.Quantity += qty;
            else
                Items.Add(new CartItem { Product = p, Quantity = qty });
        }

        public void UpdateQuantity(string productId, int qty)
        {
            var found = Items.Find(i => i.Product.ProductId == productId);
            if (found != null)
            {
                if (qty <= 0) Items.Remove(found);
                else found.Quantity = qty;
            }
        }

        public void Clear() => Items.Clear();
    }

    public class CreditCardInfo
    {
        public string CardType { get; set; }      // VISA, MASTERCARD, DISCOVER, AMEX
        public string CardNumber { get; set; }    // 16 hoặc 15 số
        public string CardholderName { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string CSV { get; set; }           // 3 hoặc 4 số
    }
}