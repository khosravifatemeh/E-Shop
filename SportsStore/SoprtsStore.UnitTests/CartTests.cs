using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SoprtsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_Item()
        {
            Product p1 = new Product { Name="p1",ProductId=1};
            Product p2 = new Product { Name="p2",ProductId=2};
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();
            Assert.AreEqual(result.Length,2);
            Assert.AreEqual(result[0].product, p1);
            Assert.AreEqual(result[1].product, p2);
        }
        [TestMethod]
        public void Can_Add_Quantity() 
        {
            Product p1 = new Product { ProductId = 1, Name = "p1" };
            Product p2 = new Product { ProductId=2,Name="p2"};
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(e => e.product.ProductId).ToArray();
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].quantity,11);
            Assert.AreEqual(results[1].quantity,1);
        }
        [TestMethod]
        public void Can_Remove_Line() 
        {
            Product p1 = new Product { ProductId = 1, Name = "p1" };
            Product p2 = new Product { ProductId = 2, Name = "p2" };
            Product p3 = new Product { ProductId = 2, Name = "p3" };
            Cart target = new Cart();
            target.AddItem(p1,1);
            target.AddItem(p2, 3);
            target.AddItem(p2, 1);
            target.RemoveLine(p2);
            Assert.AreEqual(target.Lines.Where(c=>c.product==p2).Count(),0);
            Assert.AreEqual(target.Lines.Count(),1);
        }
        [TestMethod]
        public void Calculate_Cart_Total() 
        {
            Product p1 = new Product { ProductId = 1, Name = "p1",Price=100M };
            Product p2 = new Product { ProductId = 2, Name = "p2" ,Price=50M};
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            decimal result=target.ComputeTotalVlue();
            Assert.AreEqual(result,150M);
        }
        [TestMethod]
        public void Can_Clear_Contents() 
        {
            Product p1 = new Product { ProductId = 1, Name = "p1" };
            Product p2 = new Product { ProductId = 2, Name = "p2" };
            Product p3 = new Product { ProductId = 2, Name = "p3" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);
            target.Clear();
            Assert.AreEqual(target.Lines.Count(),0);
        }
    }
}
