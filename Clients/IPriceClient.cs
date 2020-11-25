using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using WebApiService.Models;

namespace WebApiService.Clients
{
    public interface IPriceClient
    {
        [Get("/price")]
        Task<IEnumerable<PriceModel>> GetAll(CancellationToken token);
    }
}