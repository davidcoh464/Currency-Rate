using CurrencyBE;
using System.Data.Entity;

namespace CurrencyDL
{
    public class CurrencyDB : DbContext
    {
        public CurrencyDB() : base("CurrencyDataBase")
        { }
        public DbSet<USDHistory> USD_History { get; set; }
    }
}
