using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SportsStore.Domain.Entities;


namespace SportsStore.Domain.Concrete
{
    public class SportsStoreDBInitializer:DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var Products = new List<Product> 
            { 
            new Product{Name="kayak",Description="kzncj lkzndfjn",Category="watersport",Price=275},
            new Product{Name="fmkn",Description="knjkjn jbsjdb",Category="watersport",Price=234}
            };
            Products.ForEach(c => context.Products.Add(c));
            context.SaveChanges();
        }
    }
}
