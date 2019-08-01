using AutoMapper;
using ER.DAL;
using ER.Service.Interfaces;
using ER.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ER.Service
{
    /// <summary>
    /// Сервис курса валют
    /// </summary>
    public class RateService : IRateService
    {
        private DBContext db;
        public RateService(DBContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<RateDTO>> GetRatesAsync()
        {
            var rate = await db.Rates.ToListAsync();
            return Mapper.Map<IList<Rate>, IEnumerable<RateDTO>>(rate);
        }
        public async Task<IEnumerable<RateDTO>> GetRatesInTimeAsync(double time)
        {
            var rate = await db.Rates.Where(r => r.DateAdded > DateTime.Now.AddMinutes(-time) && r.DateAdded < DateTime.Now).ToListAsync();
            return Mapper.Map<IList<Rate>, IEnumerable<RateDTO>>(rate);
        }
        public async Task<RateDTO> GetRateAsync(int id)
        {
            var rate = await db.Rates.FirstOrDefaultAsync(r => r.Id == id);
            return Mapper.Map<Rate, RateDTO>(rate);
        }
        public async void CreateRateAsync(RateDTO entity)
        {
            var rate = Mapper.Map<RateDTO, Rate>(entity);
            await db.Rates.AddAsync(rate);
            db.SaveChanges();
        }
        public async void UpdateRateAsync(RateDTO entity)
        {
            var rate = Mapper.Map<RateDTO, Rate>(entity);
            var rateDB = await db.Rates.FirstOrDefaultAsync(r => r.Id == rate.Id);
            if (rateDB != null)
            {
                rateDB.ConversionPairs = rate.ConversionPairs;
                rateDB.Rates = rate.Rates;
            }
            db.Entry(rateDB).State = EntityState.Modified;
            db.SaveChanges();
        }
        public async void DeleteRateAsync(int id)
        {
            var rate = await db.Rates.FirstOrDefaultAsync(r => r.Id == id);
            if (rate != null) db.Rates.Remove(rate);
            db.SaveChanges();
        }
    }
}
