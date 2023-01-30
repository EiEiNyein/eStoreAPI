using eStoreAPI.Database.DBModels;
using eStoreAPI.Models;
using eStoreAPI.Services.Services;
using eStoreAPIUtilities.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class eVouchereAPIController : ControllerBase
    {
        private readonly QueryDAL _queryDAL;
        private readonly UpdateDAL _updateDAL;

        public eVouchereAPIController(QueryDAL queryDAL, UpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GeteVouchers()
        {
            try
            {
                DataTable dt = await _queryDAL.GeteVouchers();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GeteVoucherDetail(string id)
        {
            try
            {
                Evoucher data = await _queryDAL.GeteVoucherDetail(id);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveVoucher(eVoucherInfo info)
        {
            try
            {
                ResponseMessage data = await _updateDAL.SaveVoucher(info);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetePaymentMethodList()
        {
            try
            {
                DataTable dt = await _queryDAL.GetePaymentMethodList();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
