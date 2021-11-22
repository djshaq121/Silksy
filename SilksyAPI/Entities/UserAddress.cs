using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    public class UserAddress
    {
        [Key]
        public int UserAddressId { get; set; }
        public int UserId { get; set; }

        public StoreUser User { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }
    }
}
