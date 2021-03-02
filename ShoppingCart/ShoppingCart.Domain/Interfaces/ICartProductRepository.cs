using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartProductRepository
    {
        void AddToCart(CartProduct prod);

        void Remove(Guid id);

        CartProduct GetCartProduct(Guid id);

        string GetCartId();

        IQueryable<CartProduct> GetCartProducts(Guid id);


    }
}
