using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExchangeRates.Models;
using ER.Service.Interfaces;
using System.Collections.Generic;
using ER.Service.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ExchangeRates.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRateService _rateService;
        public HomeController(IRateService rateService)
        {
            _rateService = rateService;
        }
        public async Task<IActionResult> Index()
        {
            var periods = new int[]{ 5, 15, 30, 60};           
            var ratesDTO = new List<AgregateRateDTO>();
            foreach(var period in periods)
            {
                var rates = (await _rateService.GetRatesInTimeAsync((double)period)).GroupBy(r => r.ConversionPairs);               
                foreach (var rate in rates)
                {
                    ratesDTO.Add(new AgregateRateDTO()
                    {
                        ConversionPairs = rate.Key,
                        //FirstValue
                    });
                }  
            }

            return View(ratesDTO);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
