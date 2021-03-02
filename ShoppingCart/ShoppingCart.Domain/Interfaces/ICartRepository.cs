using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartRepository
    {
        void CreateCart(Cart c);
        Cart GetCart(Guid id);

        void DeleteCart(Guid id);
    }
}
