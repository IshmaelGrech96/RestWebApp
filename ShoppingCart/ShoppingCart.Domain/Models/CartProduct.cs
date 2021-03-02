using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class CartProduct
    {
        [Key]
        public Guid CartProductId { get; set; }

        [ForeignKey("ShoppingCart")]
        public Guid CartId { get; set; }

        public virtual Cart ShoppingCart { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
