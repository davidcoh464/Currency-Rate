using System.ComponentModel.DataAnnotations;

namespace CurrencyDL
{
    public class USDHistory
    {
        [Key]
        public string Date { get; set; }
        public double EURValue { get; set; }
        public double ILSValue { get; set; }
        public double GBPValue { get; set; }
        public double AUDValue { get; set; }

    }
}