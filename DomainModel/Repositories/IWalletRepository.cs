using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Repositories
{
    public interface IWalletRepository
    {
        Wallet GetWalletByUserId(long userId);

        void Insert(Wallet wallet);

        void Update(Wallet wallet);

        void Remove(long walletId);
    }
}
