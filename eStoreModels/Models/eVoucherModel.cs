namespace eStoreAPI.Models
{

    public class ResponseMessage
    {
        public string MessageContent { get; set; }
        public bool Status { get; set; }
    }
    public class eVoucherInfo
    {
        public string VoucherNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentId { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string BuyType { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string MaxBalance { get; set; }
        public string GiftPerUserLimit { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerName { get; set; }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AuthToken { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
