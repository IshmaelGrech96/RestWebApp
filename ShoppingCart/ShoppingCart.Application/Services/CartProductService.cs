using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartProductService : ICartProductService
    {
        private IProductsRepository _productRepo;
        private IMapper _mapper;
        private ICartProductRepository _cartProductRepo;
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        public CartProductService(IProductsRepository productRepo, IUserRepository userRepository, IMapper mapper, ICartProductRepository cartProductRepo, ICartRepository cartRepository, IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _productRepo = productRepo;
            _cartProductRepo = cartProductRepo;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }

        public void AddToCart(ProductViewModel data)
        {
            throw new NotImplementedException();
        }

        public void Checkout(string email)
        {
            if (_orderRepository.GetOrder(_userRepository.GetUser(email).Id) == null)
            {
                Order order = new Order();
                order.DateCreated = System.DateTime.Now;
                order.UserID = _userRepository.GetUser(email).Id;
                _orderRepository.CreateOrder(order);
            }
            var cartProducts = _cartProductRepo.GetCartProducts(_userRepository.GetUser(email).Id).ToList();
            foreach(CartProduct cp in cartProducts )
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.OrderId = _orderRepository.GetOrder(_userRepository.GetUser(email).Id).OrderId;
                orderDetails.Quantity = cp.Quantity;
                orderDetails.DateCreated = System.DateTime.Now;
                orderDetails.ProductId = cp.ProductId;
                _orderDetailsRepository.AddOrderDetails(orderDetails);
                _cartProductRepo.Remove(cp.CartProductId);
            }

            _cartRepository.DeleteCart(_userRepository.GetUser(email).Id);


        }

        public IQueryable<CartProductViewModel> GetCartProducts(string email)
        {
            return _cartProductRepo.GetCartProducts(_userRepository.GetUser(email).Id).ProjectTo<CartProductViewModel>(_mapper.ConfigurationProvider);
        }

    }
}
