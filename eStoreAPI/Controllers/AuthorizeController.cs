using eStoreAPI.Services.Services;
using eStoreAPIUtilities.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace eStoreAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly QueryDAL _queryDAL;
        private readonly JWTToken _jwtToken;

        public AuthorizeController(QueryDAL queryDAL,JWTToken jwtToken ) 
        {
           _queryDAL = queryDAL;
            _jwtToken= jwtToken;
        }


        /// <summary>
        /// Get API Key based on your merchant ID and Secret Key.
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="authToken">Secret Key</param>
        /// <returns>API Key</returns>
        /// <response code = "400"> Invalid Merchant</response>
        /// <response code="500">Internal Server Error - An internal server error occurred.</response>
        [HttpGet]
        [Route("{userID}/{authToken}")]
        //[ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetAPIKey([FromRoute] string userID, [FromRoute] string authToken)
        {
            var checkToken = await _queryDAL.CheckUserIsValid(userID, authToken);
            if (checkToken == false)
            {
                return StatusCode(400, "Invalid User");
            }
            else
            {
                var tokenKey = _jwtToken.GenerateAccessToken(userID, authToken);

                return Ok(tokenKey);
            }
        }


        [HttpGet]
        public string Test()
        {
            return "testing";
        }
    }
}
