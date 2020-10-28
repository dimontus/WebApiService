using System.Threading.Tasks;
using Refit;
using WebApiService.Models;

namespace WebApiService.Clients
{
    public interface IPriceClient
    {
        [Get("/")]
        Task<PriceModel> GetAll();
    }
}