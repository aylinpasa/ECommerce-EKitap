using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Product;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {

        ECommerce_TESTEntities db = new ECommerce_TESTEntities();
        // GET: Product
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GetProductsbySubCategory(int subCatID)
        {
            var prods = db.Products.Where(y => y.SubCategoryID == subCatID).ToList();
            return View(prods);
        }

        [HttpGet]
        public ActionResult SaveProduct(int productId)
        {
            var model = new SaveProductViewModel();

            if (productId > 0)
            {
                var productEntity = db.Products.FirstOrDefault(w => w.ProductID == productId);
                model.ProductID = productEntity.ProductID;
                model.Name = productEntity.Name;
                model.Price = productEntity.Price;
                model.SubCategoryID = productEntity.SubCategoryID;
            }
            ViewBag.SubCategoryList = db.SubCategory.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.SubCategoryID.ToString()
            }).ToList();

            return View("SaveProduct", model);
        }

        [HttpPost]
        public ActionResult SaveProduct(SaveProductViewModel model, HttpPostedFileBase ImageByte)
        {

            byte[] data;
            using (Stream inputStream = ImageByte.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            if (model.ProductID <= 0)
            {
                db.Products.Add(new Products
                {
                    Name = model.Name,
                    Price = model.Price,
                    ProductID = model.ProductID,
                    SubCategoryID = model.SubCategoryID,
                    Image = data
                });
            }
            else
            {
                var productEntity = db.Products.FirstOrDefault(w => w.ProductID == model.ProductID);
                productEntity.Name = model.Name;
                productEntity.Price = model.Price;
                productEntity.SubCategoryID = model.SubCategoryID;
                productEntity.Image = data;
            }
            db.SaveChanges();

            return View("Index");
        }


        public ActionResult DeleteProduct(int productId)
        {
            var productEntity = db.Products.FirstOrDefault(w => w.ProductID == productId);

            db.Products.Remove(productEntity);

            db.SaveChanges();

            return View("Index");
        }

        public ActionResult AddProducttoBasket(int productId, int subcategoryId)
        {
            //var productEntity = db.Products.FirstOrDefault(w => w.ProductID == productId);
            CustomerBasket.Add(productId);
            return RedirectToAction("GetProductsbySubCategory", new { subCatID = subcategoryId });
        }

        public ActionResult RemoveProducttoBasket(int productId)
        {
            //var productEntity = db.Products.FirstOrDefault(w => w.ProductID == productId);
            CustomerBasket.Remove(productId);
            return RedirectToAction("GetCustomerBasket");
        }
        public ActionResult GetCustomerBasket()
        {
            var model = new List<CustomerBasketViewModel>();
            if (CustomerBasket.Items.Any())
            {
                var productIdLst = CustomerBasket.Items.Select(s => s.ProductId).ToList();
                var productList = db.Products.Where(w => productIdLst.Contains(w.ProductID)).ToList();

                foreach (var item in CustomerBasket.Items)
                {
                    var productItem = productList.FirstOrDefault(w => w.ProductID == item.ProductId);
                    model.Add(new CustomerBasketViewModel
                    {
                        Image = productItem.Image,
                        Amount = item.Amount,
                        Name = productItem.Name,
                        Price = productItem.Price,
                        ProductId = item.ProductId
                    });
                }
            }
            ViewBag.CustomerBasket = CustomerBasket.Items;
            return View(model);
        }

        public ActionResult OrderCheck()
        {
            CustomerBasket.RemoveAll();
            return View();

        }

        public ActionResult ChooseBook()
        {
            return View();
        }
        




    }
}