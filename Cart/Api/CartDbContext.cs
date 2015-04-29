using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Messages;
using Web.Models;

namespace Web.Api
{
    public class CartDbContext : DbContext
    {
        public DbSet<CartItem> Items { get; set; }

        public CartDbContext()
            : base("DefaultConnection")
        {
        }

        public void InsertItem(CartItem cartItem)
        {
            Items.Add(cartItem);
            SaveChanges();
        }

        public List<CartItem> GetAll()
        {
            return Items.ToList();
        }

        public void RemoveItem(int id)
        {
            var deleteThis = GetAll().SingleOrDefault(i => i.Id == id);
            if (deleteThis != null)
                Items.Remove(deleteThis);
        }
    }
}