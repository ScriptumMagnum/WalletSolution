using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class WalletRecord
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }

        public int CurrencyId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sum { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
