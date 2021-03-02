using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private ShoppingCartDbContext _context;
        public CartRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void CreateCart(Cart c)
        {
            _context.ShoppingCart.Add(c);
            _context.SaveChanges();
        }

        public void DeleteCart(Guid id)
        {
            _context.ShoppingCart.Remove(GetCart(id));
            _context.SaveChanges();
        }

        public Cart GetCart(Guid id)
        {
            return _context.ShoppingCart.SingleOrDefault(x => x.UserID == id);
        }
    }
}
