using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{[Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        //
        // GET: /Admin/
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View(repository.Products);
        }
        public ViewResult Edit(int ProductId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
            return View(product);

        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been save", product.Name);
                return RedirectToAction("Index");

            }
            else
            {
                return View(product);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
           Product deleteProduct= repository.DeleteProduct(productId);
           if (deleteProduct != null)
           {
               TempData["message"] = string.Format("{0} removed", deleteProduct.Name);
           }
           return RedirectToAction("Index");

        }
    }
}
