using AutoMapper;
using ER.DAL;
using ER.Infrastructure.Interfaces;
using ER.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace ER.Infrastructure
{
    public  class ExchangeRateReceiver : IExchangeRateReceiver
    {
        private DBContext db;

        public ExchangeRateReceiver(DBContext context)
        {
            db = context;

        }
        public async void GetRateFromSite(string url, string currencyPair, string apiKey)
        {
            var builder = new UriBuilder(url);            
            builder.Query = "q=" + currencyPair + "&compact=ultra&apiKey=" + apiKey;
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(builder.Uri).Result;
                using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                {
                    var excangeRate = JsonConvert.DeserializeObject<KeyValuePair<string, double>>(sr.ReadToEnd());
                    var entity = new RateDTO
                        {
                            DateAdded = DateTime.Now,
                            ConversionPairs = excangeRate.Key,
                            Rates = excangeRate.Value
                        };
                    var rate = Mapper.Map<RateDTO, Rate>(entity);
                    await db.Rates.AddAsync(rate);
                    db.SaveChanges();
                }
            }
        }
        //public async void CreateRateToBase(RateDTO entity)
        //{
            
        //}
    }
}
