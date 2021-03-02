using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User u);

        User GetUser(string email);
    }
}
