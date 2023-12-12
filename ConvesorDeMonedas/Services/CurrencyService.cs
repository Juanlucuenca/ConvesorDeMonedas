using AutoMapper;
using ConvesorDeMonedas.Controllers;
using ConvesorDeMonedas.Dto;
using ConvesorDeMonedas.Interfaces;
using ConvesorDeMonedas.Models;
using ConvesorDeMonedas.Services;

namespace ConvesorDeMonedas;

public class CurrencyService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly RequestSerivce _requestSerivce;
    private readonly ConversionHistoryService _conversionHistoryService;
    private readonly IMapper _mapper;
    public CurrencyService(IRepository<Currency> currencyRepository, RequestSerivce requestSerivce, ConversionHistoryService conversionHistoryService, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _requestSerivce = requestSerivce;
        _conversionHistoryService = conversionHistoryService;
        _mapper = mapper;
    }
    public Currency GetCurrencyById(int currencyId)
    {
        if (!CurrencyExist(currencyId))
            throw new Exception($"La moneda con el id {currencyId} no existe");

        return _currencyRepository.GetById(currencyId);
    }
    public IEnumerable<Currency> GetAllCurrencies()
    {
        return _currencyRepository.GetAll();
    }

    public bool UpdateCurrency(Currency currencyToUpdate) {
        if (!CurrencyExist(currencyToUpdate.Id))
            throw new Exception($"La moneda con el id {currencyToUpdate.Id} no existe");

        Currency currencyUpdate = _currencyRepository.GetById(currencyToUpdate.Id);

        currencyUpdate.Ic = currencyToUpdate.Ic;
        currencyUpdate.Symbol = currencyToUpdate.Symbol;
        currencyUpdate.Name = currencyToUpdate.Name;


        return _currencyRepository.Update(currencyUpdate);
    }
    public bool DeleteCurrency(int currencyId) {
        if (!CurrencyExist(currencyId))
            throw new Exception($"La moneda con el id {currencyId} no existe");

        return _currencyRepository.Delete(currencyId);
    }

    public bool CreateCurrency(Currency currencyToCreate) {
        return _currencyRepository.Insert(currencyToCreate);
    }

    public double ConvertCurrency(ConvertCurrencyDto ConvertCurrencyDto)
    {

        if (!CurrencyExist(ConvertCurrencyDto.ToCurrency) || !CurrencyExist(ConvertCurrencyDto.ToCurrency))
            throw new Exception("Alguna de las monedas no existe");

        _requestSerivce.IncrementRequestCount();

        Currency? currencyFromConvert = _currencyRepository.GetById(ConvertCurrencyDto.FromCurrency);
        Currency? currencyToConvert = _currencyRepository.GetById(ConvertCurrencyDto.ToCurrency);

        double usdCount = ConvertCurrencyDto.Amout * currencyFromConvert.Ic;
        double countCurrencyDestination = usdCount / currencyToConvert.Ic;

        RegisterConvertionHistory convertionHistoryDto = new RegisterConvertionHistory()
        {
            CurrencyFromId = ConvertCurrencyDto.FromCurrency,
            CurrencyToId = ConvertCurrencyDto.ToCurrency,
            PriceToConvert = ConvertCurrencyDto.Amout,
            ConvertedPrice = countCurrencyDestination
        };


        _conversionHistoryService.GenerateRegisterOfConverison(convertionHistoryDto);

        return countCurrencyDestination;
    }

    public bool CurrencyExist(int currecyId)
    {
        return _currencyRepository.Exist(currecyId);
    }

}
