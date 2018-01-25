using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SportsStore.Domain.Entities;



namespace SportsStore.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public EFDbContext(): base("name=EFDbContext")
        {
            Database.SetInitializer<EFDbContext>(new SportsStoreDBInitializer());
        }
        public DbSet<Product> Products { get; set; }
    }
}
