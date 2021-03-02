using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepo;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserViewModel u)
        {
            _userRepo.AddUser(_mapper.Map<User>(u));
        }

        public User GetUser(string email)
        {
            return _userRepo.GetUser(email);
        }
    }
}
