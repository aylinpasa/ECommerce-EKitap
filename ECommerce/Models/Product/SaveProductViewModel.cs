using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Product
{
    public class SaveProductViewModel
    {
        public int ProductID { get; set; }
        public int SubCategoryID { get; set; }
        public string Name { get; set; }        
        public double Price { get; set; }
    }
}