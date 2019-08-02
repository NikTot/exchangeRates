using AutoMapper;
using ER.DAL;
using ER.Infrastructure.Interfaces;
using ER.Infrastructure.Models;
using ER.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ER.Infrastructure
{
    public class ExchangeRateReceiver : IExchangeRateReceiver
    {
        private DBContext db;

        public ExchangeRateReceiver(DBContext context)
        {
            db = context;

        }
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
                       
                    var rates = Mapper.Map<IEnumerable<RateDTO>, IList<Rate>>(entity);
                    foreach (var rate in rates)
                    {
                        await db.Rates.AddAsync(rate);
                        db.SaveChanges();
                    }                    
                }
            }
        }
        //}
    }
}
