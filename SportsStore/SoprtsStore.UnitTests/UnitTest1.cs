using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Controllers;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SoprtsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
            new Product{Name="p1",ProductId=1},
            new Product{Name="p2",ProductId=2},
            new Product{Name="p3",ProductId=3},
            new Product{Name="p4",ProductId=4}
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            ProductsListViewModels result = (ProductsListViewModels)controller.List(2).Model;
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name , "P3");
            Assert.AreEqual(prodArray[1].Name, "P4");
        }
        [TestMethod]
        public void Can_Generate_Page_Links() 
        {
            HtmlHelper myHelper = null;
            PagingInfo PagingInfo = new PagingInfo 
            {
            ItemsPerPage=10,
            TotalItems=28,
            CurrentPage=2
            };
            Func<int, string> PageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = myHelper.PageLinks(PagingInfo, PageUrlDelegate);
            Assert.AreEqual(result.ToString(),@"<a href=""Page1"">1</a>"+@"<a href=""Page2"" class=""selected"">2</a>"+@"<a href=""Page3"">3</a>");

        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model() 
        {
        Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[]
            {
            new Product{Name="p1",ProductId=1},
            new Product{Name="p2",ProductId=2},
            new Product{Name="p3",ProductId=3},
            new Product{Name="p4",ProductId=4}
            }.AsQueryable());
            ProductController controller=new ProductController(mock.Object);
            controller.PageSize=3;
            ProductsListViewModels result=(ProductsListViewModels)controller.List(2).Model;
            PagingInfo PagingInfo=result.PagingInfo;
            Assert.AreEqual(PagingInfo.CurrentPage,2);
            Assert.AreEqual(PagingInfo.ItemsPerPage,3);
            Assert.AreEqual(PagingInfo.TotalItems,5);
            Assert.AreEqual(PagingInfo.TotalPages,2);

        }
       [TestMethod]
        public void Can_Filter_Products() 
       {
           Mock<IProductRepository> mock = new Mock<IProductRepository>();
           mock.Setup(m => m.Products).Returns(new Product[]{
               new Product{ProductId=1,Name="p1",Category="Cat1"},
               new Product{ProductId=2,Name="p2",Category="Cat2"},
               new Product{ProductId=3,Name="p3",Category="Cat1"},
               new Product{ProductId=4,Name="p4",Category="Cat2"}
           }.AsQueryable());
           ProductController controller = new ProductController(mock.Object);
           controller.PageSize = 2;
           Product[] result = ((ProductsListViewModels)controller.List("Cat2", 1).Model).Products.ToArray();
           Assert.AreEqual(result.Length ,2);
           Assert.IsTrue(result[0].Name=="p2"&&result[0].Category=="Cat2");
           Assert.IsTrue(result[1].Name=="p4"&&result[1].Category=="Cat2");

       }
       [TestMethod]
       public void Can_Create_Category() 
       {
           Mock<IProductRepository> mock = new Mock<IProductRepository>();
           mock.Setup(m => m.Products).Returns(new Product[]{
           new Product{ProductId=1,Name="p1",Category="Apple"},
           new Product{ProductId=2,Name="p2",Category="Apple"},
           new Product{ProductId=3,Name="p3",Category="Oranges"}
           }.AsQueryable());
           NavController target = new NavController(mock.Object);
           string[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();
           Assert.AreEqual(result.Length,2);
           Assert.AreEqual(result[0], "Apples");
           Assert.AreEqual(result[1], "Oranges");
           
       }
        [TestMethod]
       public void Indicate_Selected_Category() 
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
           new Product{ProductId=1,Name="p1",Category="Apple"},
           new Product{ProductId=2,Name="p2",Category="Apple"},
           new Product{ProductId=3,Name="p3",Category="Oranges"}
           }.AsQueryable());
            NavController target = new NavController(mock.Object);
            string CategoryToselect = "Apples";
            string result = target.Menu(CategoryToselect).ViewBag.SelectedCategory;
            Assert.AreEqual(CategoryToselect,result);
        } 
        [TestMethod]
        public void Return_Count_Category_Product() 
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
               new Product{ProductId=1,Name="p1",Category="Cat1"},
               new Product{ProductId=2,Name="p2",Category="Cat2"},
               new Product{ProductId=3,Name="p3",Category="Cat1"},
               new Product{ProductId=4,Name="p4",Category="Cat2"}
           }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;
            int res1 = ((ProductsListViewModels)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModels)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModels)controller.List(null).Model).PagingInfo.TotalItems;
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 4);
        }

    }
}
