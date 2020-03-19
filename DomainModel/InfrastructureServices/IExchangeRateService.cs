using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    /// <summary>
    /// Фасад, которым в предметной области отгородимся от реализации сервиса (которых может быть несколько)
    /// </summary>
    public interface IExchangeRateService
    {
        decimal GetExchangeRate(string sourceCurrencyCode, string destinationCurrencyCode);
    }
}
