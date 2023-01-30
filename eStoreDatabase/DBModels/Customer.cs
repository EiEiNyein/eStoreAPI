using System;
using System.Collections.Generic;

namespace eStoreAPI.Database.DBModels
{
    public partial class Customer
    {
        public string CustomerId { get; set; } = null!;
        public string? VoucherNo { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public decimal? MaxBalance { get; set; }
        public decimal? GiftPerUserLimit { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

        public virtual Evoucher? VoucherNoNavigation { get; set; }
    }
}
