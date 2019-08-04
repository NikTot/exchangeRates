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

        /// <summary>
        /// Возвращает весь список курса валют
        /// </summary>
        /// <returns>Список курса валют</returns>
        public async Task<IEnumerable<RateDTO>> GetRatesAsync()
        {
            var rate = await db.Rates.ToListAsync();
            return Mapper.Map<IList<Rate>, IEnumerable<RateDTO>>(rate);
        }

        /// <summary>
        /// Возвращает весь список курса валют за определенный период
        /// </summary>
        /// <param name="time">Период в минутах</param>
        /// <returns>Список курса валют</returns>
        public async Task<IEnumerable<RateDTO>> GetRatesInTimeAsync(double time)
        {
            var rate = await db.Rates.Where(r => r.DateAdded > DateTime.Now.AddMinutes(-time)).ToListAsync();
            return Mapper.Map<IList<Rate>, IEnumerable<RateDTO>>(rate);
        }

        /// <summary>
        /// Возвращает курс валюты по id
        /// </summary>
        /// <param name="id">Id курса валюты</param>
        /// <returns>Курс валюты</returns>
        public async Task<RateDTO> GetRateAsync(int id)
        {
            var rate = await db.Rates.FirstOrDefaultAsync(r => r.Id == id);
            return Mapper.Map<Rate, RateDTO>(rate);
        }

        /// <summary>
        /// Записывает кур валюты в базу данных
        /// </summary>
        /// <param name="entity">Курс валюты</param>
        public async void CreateRateAsync(RateDTO entity)
        {
            var rate = Mapper.Map<RateDTO, Rate>(entity);
            await db.Rates.AddAsync(rate);
            db.SaveChanges();
        }

        /// <summary>
        /// Обновляет запись курса валюты
        /// </summary>
        /// <param name="entity">Курс валюты</param>
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

        /// <summary>
        /// Удаляет запись курса валютя 
        /// </summary>
        /// <param name="id">Id курса валюты</param>
        public async void DeleteRateAsync(int id)
        {
            var rate = await db.Rates.FirstOrDefaultAsync(r => r.Id == id);
            if (rate != null) db.Rates.Remove(rate);
            db.SaveChanges();
        }
    }
}
