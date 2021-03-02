using Microsoft.EntityFrameworkCore.Internal;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ShoppingCartDbContext _context;
        public ProductsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            _context.Products.Remove(GetProduct(id));
            _context.SaveChanges();

        }

        public Product GetProduct(Guid id)
        {
            return _context.Products.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }

        public IQueryable<Product> GetProductsByCateg(string cat)
        {
            var list = _context.Products
                .Join(_context.Categories, p => p.CategoryId, c => c.Id, (p, c) => new { p, c })
                .Where(categ => categ.c.Name == cat)
                .Select(products => new Product { 
                    Id = products.p.Id,
                    Name = products.p.Name,
                    Price = products.p.Price,
                    Description = products.p.Description,
                    Availability = products.p.Availability,
                    Stock = products.p.Stock,
                    ImageUrl = products.p.ImageUrl,
                    CategoryId = products.p.CategoryId
                });;

            return list;
        }

        public void Hide(Product p)
        {
            var prod = _context.Products.SingleOrDefault(x => x.Id == p.Id);
            prod.Availability = "Hidden";
            _context.SaveChanges();
        }

        public void Show(Product p)
        {
            var prod = _context.Products.SingleOrDefault(x => x.Id == p.Id);
            prod.Availability = "Available";
            _context.SaveChanges();
        }

        public void UpdateStock(Product p, int qty)
        {
            var prod = _context.Products.SingleOrDefault(x => x.Id == p.Id);
            prod.Stock -= qty;
            _context.SaveChanges();
        }
    }
}
