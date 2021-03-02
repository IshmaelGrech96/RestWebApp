using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserViewModel u);

        User GetUser(string email);
    }
}
