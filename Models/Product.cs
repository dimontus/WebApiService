using System.Collections.Generic;
using System;

namespace WebApiService.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ImageModel> Images { get; set; }
        public IEnumerable<PriceModel> Prices { get; set; }
    }
}
