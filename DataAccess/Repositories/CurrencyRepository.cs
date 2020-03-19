using DomainModel;
using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        protected readonly ApplicationDbContext context;

        public CurrencyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Currency GetCurrencyById(int currencyId)
        {
            var currency = context.Currencies.Where(x => x.Id == currencyId).FirstOrDefault();
            if (currency == null)
                throw new ArgumentException($"В БД не найдена валюта с ИД = {currencyId}");
            var result = new DomainModel.Currency(currency.Id, currency.Title, currency.Code);
            return result;
        }

        public void Insert(Currency currency)
        {
            throw new NotImplementedException();
        }

        public void Remove(int currencyId)
        {
            throw new NotImplementedException();
        }

        public void Update(Currency currency)
        {
            throw new NotImplementedException();
        }
    }
}
