using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Web.Models;

namespace Web.Api
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext()
            : base("DefaultConnection")
        {
        }

        public List<Product> GetByCategory(string category)
        {
            return Products.Where(x => x.Category.Equals(category)).ToList();
        }

        internal void Insert(Product product)
        {
            Products.Add(product);
            SaveChanges();
        }
    }
}
