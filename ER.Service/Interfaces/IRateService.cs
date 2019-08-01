using ER.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ER.Service.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с курсами валют
    /// </summary>
    public interface IRateService
    {
        /// <summary>
        /// Ассинхронно получить все записи курсов валют
        /// </summary>
        /// <returns>Курсы валют</returns>
        Task<IEnumerable<RateDTO>> GetRatesAsync();

        /// <summary>
        /// Асинхронно получить курсы валют за интервал времени
        /// </summary>
        /// <param name="time">Интервал времени в миллисекундах</param>
        /// <returns>Курсы валют</returns>
        Task<IEnumerable<RateDTO>> GetRatesInTimeAsync(double time);

        /// <summary>
        /// Ассинхронно получить курс валюты по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса валюты</param>
        /// <returns></returns>
        Task<RateDTO> GetRateAsync(int id);

        /// <summary>
        /// Создать новый курс валюты
        /// </summary>
        /// <param name="rate">Создаваймый курс валюты</param>
        void CreateRateAsync(RateDTO rate);

        /// <summary>
        /// Сохраняет изменения в курсе валюты
        /// </summary>
        /// <param name="rate">Сохраняемый курс валюты</param>
        void UpdateRateAsync(RateDTO rate);

        /// <summary>
        /// Удалить курс валюты
        /// </summary>
        /// <param name="id">Идентификатор курса валюты</param>
        void DeleteRateAsync(int id);
    }
}
