﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class OrderDetailsViewModel
    {
        [Key]
        public Guid OrderDetailsId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
