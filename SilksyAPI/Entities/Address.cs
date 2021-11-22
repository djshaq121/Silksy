using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(200)]
        public string AddressLine1 { get; set; }

        [Required]
        [MaxLength(200)]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(10)]
        public string Postcode { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}
