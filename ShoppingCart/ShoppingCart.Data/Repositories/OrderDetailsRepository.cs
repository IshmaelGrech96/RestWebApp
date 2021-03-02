using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{

    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private ShoppingCartDbContext _context;

        public OrderDetailsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void AddOrderDetails(OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            _context.SaveChanges();
        }

        public IQueryable<OrderDetails> GetCartProducts(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
