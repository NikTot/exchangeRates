using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExchangeRates.Models;
using ER.Service.Interfaces;
using System.Threading.Tasks;
namespace ExchangeRates.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgregateRateService agregateRateService;
        public HomeController(IAgregateRateService agregateRateService)
        {
            this.agregateRateService = agregateRateService;
        }
        public async Task<IActionResult> Index()
        {
            double[] periods = new double[] { 5, 15, 30, 60};
            var rates = await agregateRateService.GetAgregateRateAsync(periods);           
            return View(rates);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
