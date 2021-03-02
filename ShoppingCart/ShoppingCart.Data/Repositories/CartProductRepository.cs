using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartProductRepository : ICartProductRepository
    {
        public string ShoppingCartId { get; set; }

        private ShoppingCartDbContext _context;

        public CartProductRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public const string CartSessionKey = "CartId";


        public void Remove(Guid id)
        {
            var cartProduct = GetCartProduct(id);
            _context.ShoppingCartProducts.Remove(cartProduct);
            _context.SaveChanges();
        }

        public string GetCartId()
        {
            throw new NotImplementedException();
        }

        public IQueryable<CartProduct> GetCartProducts(Guid id)
        {

            return _context.ShoppingCartProducts.Where(x=> x.ShoppingCart.User.Id == id);
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void AddToCart(CartProduct prod)
        {
            var prd = _context.ShoppingCartProducts.SingleOrDefault(x => x.ProductId == prod.ProductId);
            if (prd == null)
            {
                _context.ShoppingCartProducts.Add(prod);
            } else
            {
                prd.Quantity += 1;
            }
            _context.SaveChanges();
        }

        public CartProduct GetCartProduct(Guid id)
        {
            return _context.ShoppingCartProducts.SingleOrDefault(x => x.CartProductId == id);
        }
    }
}
