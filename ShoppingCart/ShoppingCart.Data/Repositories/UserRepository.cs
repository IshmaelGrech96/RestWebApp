using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ShoppingCartDbContext _context;

        public UserRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void AddUser(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
        }

        public User GetUser(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
