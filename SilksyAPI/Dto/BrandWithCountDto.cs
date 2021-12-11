using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Dto
{
    public class BrandWithCountDto :  BrandDto
    {
        public int Count { get; set; }
    }
}
