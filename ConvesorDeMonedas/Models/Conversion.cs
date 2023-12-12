namespace ConvesorDeMonedas.Models
{
    public class Conversion
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal PriceToConvert { get; set; }
        public double ConvertedPrice { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public Currency? ToCurrency { get; set; }
        public Currency? FromCurrency { get; set; }
    }
}
