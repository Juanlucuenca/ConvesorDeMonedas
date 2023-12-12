using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Migrations;
using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Services
{
    public class ConversionHistoryService
    {
        private readonly IRepository<Conversion> _repository;
        private readonly SessionService _sessionService;
        public ConversionHistoryService(IRepository<Conversion> repository, SessionService sessionService)
        {
            _repository = repository;
            _sessionService = sessionService;
        }

        public void GenerateRegisterOfConverison(RegisterConvertionHistory registerConvertionData)
        {
            int userId = _sessionService.GetUserId();

            Conversion conversion = new Conversion()
            {
                UserId = userId,
                Date = DateTime.Now,
                FromCurrencyId = registerConvertionData.CurrencyFromId,
                ToCurrencyId = registerConvertionData.CurrencyToId,
                PriceToConvert = registerConvertionData.PriceToConvert,
                ConvertedPrice = registerConvertionData.ConvertedPrice
            };

            _repository.Insert(conversion);
        }

        public IEnumerable<ConvertHistoryDto> GetUserHistory()
        {

            int userId = _sessionService.GetUserId();

            List<Conversion> history = _repository
                .GetAllIncluding(x => x.FromCurrency,
                                x => x.ToCurrency)
                .Where(x => x.UserId == userId)
                .ToList();

            return history.Select(x => new ConvertHistoryDto
            {
                Date = x.Date,
                CurrencyFromSymbol = x.FromCurrency.Symbol,
                CurrencyToSymbol = x.ToCurrency.Symbol,
                PriceToConvert = x.PriceToConvert,
                ConvertedPrice = x.ConvertedPrice
            }).ToList();
        }
    }
}
