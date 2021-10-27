using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public StoreUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
