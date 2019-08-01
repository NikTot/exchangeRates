using System;
using System.ComponentModel.DataAnnotations;

namespace ER.Service.Models
{
    public class RateDTO
    {
        [Required]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string ConversionPairs { get; set; }
        public double Rates { get; set; }
    }
}
