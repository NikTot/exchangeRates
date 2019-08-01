using System;

namespace ER.DAL
{
    public class Rate
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string ConversionPairs { get; set; }
        public double Rates { get; set; }
    }
}
