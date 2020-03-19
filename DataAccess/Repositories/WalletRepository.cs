using DomainModel;
using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        protected readonly ApplicationDbContext context;

        public WalletRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Wallet GetWalletByUserId(long userId)
        {            
            var records = context.WalletRecords.Where(x => x.UserId == userId)
                .GroupBy(x => x.CurrencyId)
                .Select(x => new { CurrencyId = x.Key, Sum = x.Sum(g => g.Sum) }).ToDictionary(x => x.CurrencyId, x => x.Sum);
            if (records.Count() == 0) return null;
            var result = new Wallet(userId, records);
            return result;
        }

        public void Insert(Wallet wallet)
        {
            if (context.WalletRecords.Any(x => x.UserId == wallet.UserId))
                throw new Exception($"В БД уже существует кошелек для пользователя UserId = {wallet.UserId}");

            // Проследим, чтобы не было дублирований
            var records = wallet.GetWalletRecords().ToList().GroupBy(x => x.Key).Select(g => new KeyValuePair<int, decimal>(g.Key, g.Sum(x => x.Value)));

            foreach (var record in records)            
                context.WalletRecords.Add(new Entities.WalletRecord { UserId = wallet.UserId, CurrencyId = record.Key, Sum = record.Value });

            context.SaveChanges();
        }

        public void Remove(long walletId)
        {
            throw new NotImplementedException();
        }

        public void Update(Wallet wallet)
        {
            // Проследим, чтобы не было дублирований
            var records = wallet.GetWalletRecords().ToList().GroupBy(x => x.Key).Select(g => new KeyValuePair<int, decimal>(g.Key, g.Sum(x => x.Value)));

            var currencyIds = records.Select(x => x.Key).ToList();

            var recsToRemove = context.WalletRecords.Where(x => x.UserId == wallet.UserId && !currencyIds.Contains(x.CurrencyId)).ToList();

            context.WalletRecords.RemoveRange(recsToRemove);

            var recsToUpdate = context.WalletRecords.Where(x => x.UserId == wallet.UserId && currencyIds.Contains(x.CurrencyId)).ToList();

            foreach(var rec in recsToUpdate)            
                rec.Sum = records.Where(x => x.Key == rec.CurrencyId).First().Value;            

            var recsToInsert = records.Where(x => !context.WalletRecords.Any(r => r.UserId == wallet.UserId && r.CurrencyId == x.Key)).ToList();

            foreach (var rec in recsToInsert)
                context.WalletRecords.Add(new Entities.WalletRecord { UserId = wallet.UserId, CurrencyId = rec.Key, Sum = rec.Value });

            context.SaveChanges();
        }
    }
}
