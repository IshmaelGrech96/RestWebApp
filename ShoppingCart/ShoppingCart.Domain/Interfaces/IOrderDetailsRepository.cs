using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrderDetailsRepository
    {
        void AddOrderDetails(OrderDetails orderDetails);

        IQueryable<OrderDetails> GetCartProducts(Guid id);

        IQueryable<OrderDetails> GetOrderProducts(Guid userId);
    }
}
