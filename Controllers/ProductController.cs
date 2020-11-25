using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApiService.Clients;
using WebApiService.Models;

namespace WebApiService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductContext _productContext;
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, ProductContext productContext,
            IImageClient imageClient, IPriceClient priceClient, IMapper mapper)
        {
            _logger = logger;
            _productContext = productContext;
            _imageClient = imageClient;
            _priceClient = priceClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            var prices = await _priceClient.GetAll(token);
            var images = await _imageClient.GetAll();
            var dbProducts = _productContext.Product.ToList();
            var products = _mapper.Map<IEnumerable<Product>>(dbProducts);

            return products.Select(product =>
            {
                product.Images = images;
                product.Prices = prices;

                return product;
            });
        }
    }
}
