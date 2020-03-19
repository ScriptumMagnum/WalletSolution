using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DomainServices
{
    public class WalletService
    {
        protected readonly IWalletRepository walletRepository;
        protected readonly IExchangeRateService exchangeRateService;
        protected readonly ICurrencyRepository currencyRepository;

        public WalletService(IWalletRepository walletRepository, IExchangeRateService exchangeRateService, ICurrencyRepository currencyRepository)
        {
            this.walletRepository = walletRepository;
            this.exchangeRateService = exchangeRateService;
            this.currencyRepository = currencyRepository;
        }

        /// <summary>
        /// Пополнить кошелек
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <param name="sum"></param>
        public void Deposit(long userId, int currencyId, decimal sum)
        {
            var wallet = walletRepository.GetWalletByUserId(userId);
            
            if (wallet == null) // В системе у пользователя еще нет кошелька
            {
                // Нужно завести
                wallet = new Wallet(userId); 
                wallet.Deposit(currencyId, sum);
                walletRepository.Insert(wallet);
            }
            else
            {
                wallet.Deposit(currencyId, sum);
                walletRepository.Update(wallet);
            }
        }

        /// <summary>
        /// Снять деньги
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <param name="sum"></param>
        public void Withdraw(long userId, int currencyId, decimal sum)
        {
            var wallet = walletRepository.GetWalletByUserId(userId);

            if (wallet == null) // В системе у пользователя еще нет кошелька
                throw new Exception("Ошибка списания средств. У пользователя нет кошелька");
            else
            {
                wallet.Withdraw(currencyId, sum);
                walletRepository.Update(wallet);
            }
        }

        /// <summary>
        /// Обменять валюту
        /// </summary>
        /// <param name="sourceCurrencyId">Идентификатор исходящей валюты</param>
        /// <param name="destinationCurrencyId">Идентификатор входящей валюты</param>
        /// <param name="sumInSourceCurrency">Сумма в исходящей валюте</param>
        public void Exchange(long userId, int sourceCurrencyId, int destinationCurrencyId, decimal sumInSourceCurrency)
        {
            var wallet = walletRepository.GetWalletByUserId(userId);

            if (wallet == null)
                throw new ArgumentException($"В системе не заведен кошелек для пользователя с ИД = {userId}");

            var sourceCurrency = currencyRepository.GetCurrencyById(sourceCurrencyId);
            var destinationCurrency = currencyRepository.GetCurrencyById(destinationCurrencyId);

            if (sourceCurrency == null)
                throw new ArgumentException($"В БД не найдена валюта с ИД = {sourceCurrencyId}");

            if (destinationCurrency == null)
                throw new ArgumentException($"В БД не найдена валюта с ИД = {destinationCurrencyId}");

            var exchangeRate = exchangeRateService.GetExchangeRate(sourceCurrency.Code, destinationCurrency.Code);

            wallet.Withdraw(sourceCurrencyId, sumInSourceCurrency);

            var sumInDestinationCurrency = sumInSourceCurrency * exchangeRate;

            wallet.Deposit(destinationCurrencyId, sumInDestinationCurrency);
            walletRepository.Update(wallet);
        }

        /// <summary>
        /// Получить состояние кошелька
        /// </summary>
        /// <param name="userId">Идентификатор пользователя кошелька</param>
        /// <returns></returns>
        public List<WalletRecordDTO> GetWalletStatement(long userId)
        {
            var result = new List<WalletRecordDTO>();
            var wallet = walletRepository.GetWalletByUserId(userId);
            var records = wallet.GetWalletRecords();
            foreach(var record in records)
            {
                var currency = currencyRepository.GetCurrencyById(record.Key);
                var dto = new WalletRecordDTO { CurrencyTitle = currency.Title, Sum = record.Value };
                result.Add(dto);
            }
            return result;
        }
    }
}
