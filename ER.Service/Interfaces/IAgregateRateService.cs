using ER.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ER.Service.Interfaces
{
    public interface IAgregateRateService
    {
        Task<IEnumerable<AgregateRateDTO>> GetAgregateRateAsync(double[] periods);
    }
}
