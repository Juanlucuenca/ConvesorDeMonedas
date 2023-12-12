namespace ConvesorDeMonedas.Dto
{
    public class ConvertHistoryDto
    {
        public DateTime Date { get; set; }
        public string CurrencyFromSymbol { get; set; } = string.Empty;
        public string CurrencyToSymbol { get; set; } = string.Empty;
        public decimal PriceToConvert { get; set; }
        public double ConvertedPrice { get; set; }
    }
}
