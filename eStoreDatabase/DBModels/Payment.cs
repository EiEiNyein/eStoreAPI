using System;
using System.Collections.Generic;

namespace eStoreAPI.Database.DBModels
{
    public partial class Payment
    {
        public Payment()
        {
            Evouchers = new HashSet<Evoucher>();
        }

        public string PaymentId { get; set; } = null!;
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

        public virtual ICollection<Evoucher> Evouchers { get; set; }
    }
}
