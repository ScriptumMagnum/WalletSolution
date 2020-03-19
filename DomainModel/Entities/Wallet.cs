using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Кошелек пользователя
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// Идентификатор кошелька
        /// </summary>
        public long Id { get; set; }

        public Wallet(long userId, Dictionary<int, decimal> records)
        {
            UserId = userId;
            this.records = records;
        }

        public Wallet(long userId)
        {
            UserId = userId;
            records = new Dictionary<int, decimal>();
        }

        /// <summary>
        /// Записи кошелька пользователя: Id валюты, сумма в кошельке в этой валюте
        /// </summary>
        private Dictionary<int, decimal> records;

        /// <summary>
        /// Идентификатор пользователя кошелька
        /// </summary>
        public long UserId { get; private set; }        

        /// <summary>
        /// Пополнить кошелек
        /// </summary>
        /// <param name="currencyId">Идентификатор валюты суммы пополнения</param>
        /// <param name="sum">Сумма пополнения кошелька</param>        
        public void Deposit(int currencyId, decimal sum)
        {
            if (records.ContainsKey(currencyId))
                records[currencyId] += sum;
            else
                records.Add(currencyId, sum);
        }

        /// <summary>
        /// Снять деньги с кошелька
        /// </summary>
        /// <param name="currencyId">Идентификатор валюты суммы</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Значение, которое получилось снять</returns>
        public void Withdraw(int currencyId, decimal sum)
        {
            var sumAvailable = records.ContainsKey(currencyId) ? records[currencyId] : 0;
            if (sumAvailable >= sum)
                records[currencyId] -= sum;
            else
                throw new Exception($"В кошельке недостаточно средств (попытка снять: {sum}, доступно: {sumAvailable}, идентификатор валюты: {currencyId}");
        }        

        public Dictionary<int, decimal> GetWalletRecords()
        {
            // Чтобы внешний код не смог изменить внутреннее состояние кошелька, делаем копию
            return records.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
