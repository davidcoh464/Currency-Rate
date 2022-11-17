using System.ComponentModel.DataAnnotations;

namespace CurrencyBE
{
    public class CurrencyHistory
    {
        public string Date { get; set; }

        [Display(Name = "US Dollar")]
        public double USDValue { get; set; }

        [Display(Name = "Euro")]
        public double EURValue { get; set; }
        
        [Display(Name = "Israeli Shekel")]
        public double ILSValue { get; set; }

        [Display(Name = "British Pound")]
        public double GBPValue { get; set; }

        [Display(Name = "Australian Dollar")]
        public double AUDValue { get; set; }
    }
}
