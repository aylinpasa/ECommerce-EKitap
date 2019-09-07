using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class CustomerBasket
    {
        public static List<CustomerBasketItem> Items
        {
            get
            {
                if (HttpContext.Current.Session["CustomerBasketItems"] == null)
                    HttpContext.Current.Session["CustomerBasketItems"] = new List<CustomerBasketItem>();
                return (List<CustomerBasketItem>)HttpContext.Current.Session["CustomerBasketItems"];
            }
            set {
                HttpContext.Current.Session["CustomerBasketItems"] = value;
            }
        }

        public static void Add(int productId)
        {
            var productItem = Items.FirstOrDefault(w => w.ProductId == productId);
            if (productItem == null)
            {
                Items.Add(new CustomerBasketItem
                {
                    ProductId = productId,
                    Amount = 1
                });
            }
            else
            {
                productItem.Amount += 1;
            }
        }

        public static void Remove(int productId)
        {
            var productItem = Items.FirstOrDefault(w => w.ProductId == productId);
            if (productItem != null)
            {
                productItem.Amount -= 1;
                if (productItem.Amount == 0)
                {
                    Items.Remove(productItem);
                }
            }
        }

        public static void RemoveAll()
        {

            Items = new List<CustomerBasketItem>();

        }
    }
    public class CustomerBasketItem
    {
        public int ProductId { get; set; }

        public int Amount { get; set; }
    }
}