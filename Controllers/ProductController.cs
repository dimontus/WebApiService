using System.Net.Mime;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiService.Models;
using WebApiService.Clients;

namespace WebApiService.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;

        public ProductController(ILogger<ProductController> logger, IImageClient imageClient, IPriceClient priceClient)
        {
            _logger = logger;
            _imageClient = imageClient;
            _priceClient = priceClient;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var rng = new Random();
            var images = await _imageClient.GetAll();
            var prices = await _priceClient.GetAll();

            return Enumerable.Range(1, 5).Select(index => new Product{
                Id = Guid.NewGuid(),
                Name = $"Product{new Random().Next()}",
                Description = "Description of product",
                Images = images,
                Prices = prices
            }).ToArray();
        }
    }
}
