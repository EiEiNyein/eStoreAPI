using System;
using System.Collections.Generic;

namespace eStoreAPI.Database.DBModels
{
    public partial class Evoucher
    {
        public Evoucher()
        {
            Customers = new HashSet<Customer>();
        }

        public string VoucherNo { get; set; } = null!;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public byte[]? Image { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentId { get; set; }
        public int? Discount { get; set; }
        public int? Quantity { get; set; }
        public string? BuyType { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
