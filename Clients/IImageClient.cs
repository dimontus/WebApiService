using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApiService.Models;

namespace WebApiService.Clients
{
    public interface IImageClient
    {
        [Get("/image")]
        Task<IEnumerable<ImageModel>> GetAll();
    }    
}