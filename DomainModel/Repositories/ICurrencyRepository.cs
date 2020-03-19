using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Repositories
{
    public interface ICurrencyRepository
    {
        Currency GetCurrencyById(int currencyId);

        void Insert(Currency currency);

        void Update(Currency currency);

        void Remove(int currencyId);
    }
}
