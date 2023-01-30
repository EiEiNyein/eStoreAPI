using System;
using System.Collections.Generic;

namespace eStoreAPI.Database.DBModels
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? AuthToken { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
