using eStoreAPI.Database;
using eStoreAPI.Database.DBModels;
using eStoreAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreAPI.Services.Services
{
    public class UpdateDAL
    {
        eVoucherContext _context;
        private readonly IConfiguration _configuration; string _conStr;
        public UpdateDAL(eVoucherContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<ResponseMessage> SaveVoucher(eVoucherInfo info)
        {
            //var transaction = await _context.Database.BeginTransactionAsync();
            ResponseMessage msg = new ResponseMessage();
            msg.Status = false;
            try
            {
                Evoucher exist = await _context.Evouchers.SingleOrDefaultAsync(x => x.VoucherNo == info.VoucherNo);
                if (exist != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Dulpicated data!";
                }
                else
                {
                    Evoucher evoucher = new Evoucher()
                    {
                        VoucherNo = new Guid().ToString().Replace("-", "").Substring(0, 10).ToUpper(),
                        Title = info.Title,
                        Description = info.Description,
                        Amount = info.Amount,
                        PaymentId = info.PaymentId,
                        Discount = info.Discount,
                        Quantity = info.Quantity,
                        Status = "Draft",
                        UpdatedDate = DateTime.UtcNow.ToLocalTime(),
                        UpdatedUser = ""
                    };
                    _context.Evouchers.Add(evoucher);
                    try
                    {
                        await _context.SaveChangesAsync();
                        //await transaction.CommitAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully saved";
                    }
                    catch (Exception e)
                    {
                        msg.MessageContent += e.Message;
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
               // await transaction.RollbackAsync();
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        msg.Status = false;
                        msg.MessageContent += message;
                    }
                }

                return msg;

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                foreach (var error in e.Entries)
                {
                    msg.Status = false;
                    msg.MessageContent += error.Entity.GetType().Name;
                }

                return msg;
            }
            catch (Exception e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
    }
}
