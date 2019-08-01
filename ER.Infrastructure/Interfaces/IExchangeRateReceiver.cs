using ER.Service.Models;
using System.Threading.Tasks;

namespace ER.Infrastructure.Interfaces
{
    public interface IExchangeRateReceiver
    {
        void GetRateFromSite(string url, string currencyPair, string apiKey);
        //void CreateRateToBase(RateDTO rate);
    }
}
