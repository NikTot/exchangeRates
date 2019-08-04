using ER.Service.Interfaces;
using ER.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ER.Service
{
    public class AgregateRateService : IAgregateRateService
    {
        private IRateService rateService;

        public AgregateRateService(IRateService rateService)
        {
            this.rateService = rateService;
        }
        public async Task<IEnumerable<AgregateRateDTO>> GetAgregateRateAsync(double[] periods)
        {
            try
            {
                var agregateRateDTO = new List<AgregateRateDTO>();
                foreach (var period in periods)
                {
                    var rateByPeriod = await rateService.GetRatesInTimeAsync(period);
                    var agregateRateByConversionPairs = rateByPeriod.GroupBy(r => r.ConversionPairs);
                    foreach (var rateByConversionPairs in agregateRateByConversionPairs)
                    {
                        agregateRateDTO.Add(new AgregateRateDTO()
                        {
                            ConversionPairs = rateByConversionPairs.Key,
                            Period = period,
                            MaxValue = rateByConversionPairs.Max(r => r.Rates),
                            Minvalue = rateByConversionPairs.Min(r => r.Rates),
                            FirstValue = rateByConversionPairs.OrderBy(d => d.DateAdded).FirstOrDefault().Rates,
                            LastValue = rateByConversionPairs.OrderByDescending(d => d.DateAdded).FirstOrDefault().Rates,
                        });
                    }
                }
                return agregateRateDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
