using System.ComponentModel.DataAnnotations;

namespace ConvesorDeMonedas.Dto
{
    public class RegisterConvertionHistory
    {
        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }
        public decimal PriceToConvert { get; set; }
        public double ConvertedPrice { get; set; }
    }
}
