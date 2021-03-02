using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IProductsService
    {
        IQueryable<ProductViewModel> GetProducts();

        IQueryable<ProductViewModel> GetProductsByCateg(string cat);

        ProductViewModel GetProduct(Guid id);

        void AddProduct(ProductViewModel data);

        void DeleteProduct(Guid id);

        void AddToCart(Guid id,string email,int qty);

        void Hide(Guid id);

        void Show(Guid id);

        bool CheckStock(Guid id,int qty);
    }
}
