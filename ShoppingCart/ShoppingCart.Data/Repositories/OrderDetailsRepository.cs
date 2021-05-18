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

        public IQueryable<OrderDetails> GetOrderProducts(Guid userId)
        {
            var list = _context.OrderDetails
                .Join(_context.Orders, od => od.OrderId, o => o.OrderId, (od, o) => new { od, o })
                .Where(order => order.o.UserID == userId)
                .Select(orderD => new OrderDetails
                {
                    OrderDetailsId = orderD.od.OrderDetailsId,
                    OrderId = orderD.o.OrderId,
                    Quantity = orderD.od.Quantity,
                    DateCreated = orderD.od.DateCreated,
                    ProductId = orderD.od.ProductId,
                    Product =  _context.Products.SingleOrDefault(x=> x.Id == orderD.od.ProductId)
                });

            return list;
        }
    }
}
