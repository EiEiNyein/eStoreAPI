using eStoreAPI.Database;
using eStoreAPI.Database.DBModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eStoreAPI.Services.Services
{
    public class QueryDAL
    {
        eVoucherContext _context;
        private readonly IConfiguration _configuration; string _conStr;
        public QueryDAL(eVoucherContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task<DataTable> GetDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var newCon = new SqlConnection(_conStr))
                using (var adapt = new SqlDataAdapter(sSQL, newCon))
                {
                    newCon.Open();
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    if (para != null)
                        adapt.SelectCommand.Parameters.AddRange(para);

                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    newCon.Close();
                    return dt;
                }
            });
        }


        public async Task<bool> CheckUserIsValid(string userID, string secretKey)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> Parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(secretKey))
            {

                string sql = @"select UserId from [dbo].[User] where Status ='A' and UserId = @id  and AuthToken = @token";

                Parameters.Add(new SqlParameter("@id", userID));
                Parameters.Add(new SqlParameter("@token", secretKey));

                SqlParameter[] para = Parameters.ToArray();
                dt = await GetDataTableAsync(sql, para);

            }

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }


        public async Task<DataTable> GeteVouchers()
        {
            string sql = @"SELECT e.VoucherNo,e.Title,e.Description,e.ExpiryDate,e.Amount,e.Discount,e.Quantity,
e.BuyType,e.Status,c.Name CustomerName, p.PaymentMethod
FROM Evoucher e
LEFT JOIN Customer c ON c.VoucherNo = e.VoucherNo
LEFT JOIN Payment p ON p.PaymentId = e.VoucherNo";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<DataTable> GetePaymentMethodList()
        {
            string sql = @"SELECT PaymentId,PaymentMethod FROM Payment WHERE Status = 'A'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<Evoucher> GeteVoucherDetail(string id)
        {
            Evoucher evoucher = new();
            try
            {
                evoucher = await _context.Evouchers.SingleOrDefaultAsync(x => x.VoucherNo == id);
            }
            catch (Exception){}
            return evoucher;
        }
    }
}
