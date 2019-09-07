using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Product
{
    public class CustomerBasketViewModel
    {
        public int ProductId { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice
        {
            get
            {
                return Amount * Price;
            }
        }
    }
}