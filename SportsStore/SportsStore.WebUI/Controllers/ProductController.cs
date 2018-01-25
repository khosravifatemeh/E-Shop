using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        //
        // GET: /Product/

        public ActionResult List(string category,int Page=1)
        {
            ProductsListViewModels model = new ProductsListViewModels 
            {
                Products = repository.Products.Where(p=>category==null||p.Category==category).OrderBy(p => p.ProductId).Skip((Page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo 
                {
                CurrentPage=Page,
                ItemsPerPage=PageSize,
                TotalItems=category==null?repository.Products.Count():repository.Products.Where(e=>e.Category==category).Count()
                },
                CurrentCategory=category
            };
            return View(model);
            //return View(repository.Products.OrderBy(p=>p.ProductId).Skip((Page-1)*PageSize).Take(PageSize));
        }

    }
}
