using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductsRepository
    {
        IQueryable<Product> GetProducts();

        IQueryable<Product> GetProductsByCateg(string cat);

        Product GetProduct(Guid id);

        void AddProduct(Product p);

        void DeleteProduct(Guid id);

        void Hide(Product p);

        void Show(Product p);

        void UpdateStock(Product p,int qty);
    }
}
