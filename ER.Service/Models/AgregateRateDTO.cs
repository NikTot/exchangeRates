using System.ComponentModel.DataAnnotations;
namespace ER.Service.Models
{
    public class AgregateRateDTO
    {
        [Display(Name = "Валютная пара")]
        public string ConversionPairs { get; set; }

        [Display(Name = "Период агрегации")]
        public int Period { get; set; }

        [Display(Name = "Максимальное значение")]
        public double MaxValue { get; set; }

        [Display(Name = "Минимально значение")]
        public double Minvalue { get; set; }

        [Display(Name = "Первое значение")]
        public double FirstValue { get; set; }

        [Display(Name = "Последнее значение")]
        public double LastValue { get; set; }
    }
}
