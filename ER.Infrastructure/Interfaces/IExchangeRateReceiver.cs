using System.Threading.Tasks;

namespace ER.Infrastructure.Interfaces
{
    public interface IExchangeRateReceiver
    {
        Task GetRateFromSite(string url);        
    }
}
