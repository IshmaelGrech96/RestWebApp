using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }
    }
}
