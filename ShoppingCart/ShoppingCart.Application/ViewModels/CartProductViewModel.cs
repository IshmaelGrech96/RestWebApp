using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartProductViewModel
    {
        public string ItemId { get; set; }

        public Guid CartId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
