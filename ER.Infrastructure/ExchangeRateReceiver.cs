using AutoMapper;
using ER.DAL;
using ER.Infrastructure.Interfaces;
using ER.Infrastructure.Models;
using ER.Service.Interfaces;
using ER.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Сервис получение курса валют с сайта
/// </summary>
namespace ER.Infrastructure
{
    public class ExchangeRateReceiver : IExchangeRateReceiver
    {
        private IRateService rateService;

        public ExchangeRateReceiver(IRateService rateService)
        {
            this.rateService = rateService;

        }

        //private DBContext db;

        //public ExchangeRateReceiver(DBContext context)
        //{
        //    db = context;

        //}

        /// <summary>
        /// Получить курс валют с сайта
        /// </summary>
        /// <param name="url">Url сайта с курсами валют</param>
        public async Task GetRateFromSite(string url)
        {
            var builder = new UriBuilder(url);
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(builder.Uri).Result;
                using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                {                   
                    RootObject bankData = JsonConvert.DeserializeObject<RootObject>(await sr.ReadToEndAsync());
                    var entity = new List<RateDTO>()
                    {
                        new RateDTO
                        {
                            DateAdded = DateTime.Now,
                            ConversionPairs = bankData.Valute.USD.CharCode + "_RUB",
                            Rates = bankData.Valute.USD.Value                        },
                        new RateDTO
                        {
                            DateAdded = DateTime.Now,
                            ConversionPairs = bankData.Valute.EUR.CharCode + "_RUB",
                            Rates = bankData.Valute.EUR.Value
                        }
                    };
                       
                   
                    foreach (var rate in entity)
                    {
                         rateService.CreateRateAsync(rate);                       
                    }                    
                }
            }
        }
    }
}
