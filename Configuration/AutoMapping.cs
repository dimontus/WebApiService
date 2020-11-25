using AutoMapper;
using WebApiService.Models;
using WebApiService.Repository;

namespace WebApiService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDbModel, Product>();
        }
    }
}