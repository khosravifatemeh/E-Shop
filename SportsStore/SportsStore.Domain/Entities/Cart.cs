using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection=new List<CartLine>();
        public void AddItem(Product product,int quantity) 
        {
            CartLine line = lineCollection.Where(p => p.product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null) 
            {
                lineCollection.Add(new CartLine { product = product, quantity = quantity });
            }
            else
            {
                line.quantity += quantity;
            }
        }
        public void RemoveLine(Product product) 
        {
            lineCollection.RemoveAll(p => p.product.ProductId == product.ProductId);
        }
        public decimal ComputeTotalVlue() 
        {
            return lineCollection.Sum(e => e.product.Price * e.quantity);
        }
        public void Clear() 
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

    }
    public class CartLine
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}
