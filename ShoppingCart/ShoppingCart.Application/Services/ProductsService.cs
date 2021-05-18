using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;
        private IMapper _mapper;
        private ICartProductRepository _cartProductRepo;
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private IOrderDetailsRepository _orderDetails;

        public ProductsService(IProductsRepository productRepo, IOrderDetailsRepository orderDetails, IMapper mapper,ICartProductRepository cartProductRepo, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _productRepo = productRepo;
            _cartProductRepo = cartProductRepo;
            _userRepository = userRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _orderDetails = orderDetails;
        }

        public void AddProduct(ProductViewModel data)
        {
            data.Availability = "Available";
            _productRepo.AddProduct(_mapper.Map<Product>(data));
        }

        public void AddToCart(Guid id,string email,int qty)
        {
            //_cartProductRepo.AddToCart(_mapper.Map<CartProduct>(_productRepo.GetProduct(id)));
            //string userLogged = User.Identity.Name;
            //Create cart if none is available
            if (_cartRepository.GetCart(_userRepository.GetUser(email).Id) == null)
            {
                Cart c = new Cart();
                c.DateCreated = System.DateTime.Now;
                c.UserID = _userRepository.GetUser(email).Id;
                _cartRepository.CreateCart(c);
            }

            CartProduct cp = new CartProduct();
            cp.CartId = _cartRepository.GetCart(_userRepository.GetUser(email).Id).Cartid;
            cp.Quantity = qty;
            cp.DateCreated = System.DateTime.Now;
            cp.ProductId = _productRepo.GetProduct(id).Id;
            _cartProductRepo.AddToCart(cp);
            _productRepo.UpdateStock(_productRepo.GetProduct(id), qty);
        }

        public bool CheckStock(Guid id, int qty)
        {
            if(qty > _productRepo.GetProduct(id).Stock || qty <= 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public IQueryable<OrderDetailsViewModel> GetOrderDetailProducts(string email)
        {
           Guid id = _userRepository.GetUser(email).Id;
            
           return _orderDetails.GetOrderProducts(id).ProjectTo<OrderDetailsViewModel>(_mapper.ConfigurationProvider);
        }

        public void DeleteProduct(Guid id)
        {
            if (_productRepo.GetProduct(id) != null)
            {
                _productRepo.DeleteProduct(id);
            }
        }

        public ProductViewModel GetProduct(Guid id)
        {
            Product product = _productRepo.GetProduct(id);
            var resultingProductViewModel = _mapper.Map<ProductViewModel>(product);
            return resultingProductViewModel;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            return _productRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ProductViewModel> GetProductsByCateg(string cat)
        {
            return _productRepo.GetProductsByCateg(cat).ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
        }

        public void Hide(Guid id)
        {
            var prd = _productRepo.GetProduct(id);
            if(prd != null)
            {
                _productRepo.Hide(prd);
            }          
        }

        public void Show(Guid id)
        {
            var prd = _productRepo.GetProduct(id);
            if (prd != null)
            {
                _productRepo.Show(prd);
            }
        }


    }
}
