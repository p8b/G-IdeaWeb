using gIdeas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace gIdeas.Controllers
{
    [Route("api/[controller]/")]
    public class AuthenticationController : Controller
    {
        private static bool auth = false;
        [HttpGet("[action]")]
        public IActionResult test()
        {
            return Ok("Works");
        }
        #region *** HttpPost, 200 OK, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public IActionResult Login([FromBody] dynamic jsonData)
        {
            bool rememberMe;
            try
            {
                /// extract the info from jsonData received
                var SUser = new gUser
                {
                    Email = jsonData.email,
                    PasswordHash = jsonData.password,
                };
                //rememberMe = (bool)jsonData.rememberMe;
            }
            catch (Exception) /// catch error if jsonData is null
            {
                return BadRequest(new { message = "Login Error" });
            }
            auth = true;
            return Ok(new { accessClaim = "Admin" });
        }

        #region *** HttpGet, 200 OK, 400 BadRequest, Authorize staff ***
        [HttpGet("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> SilentAuth()
        {
            if (auth)
                return Ok(new { accessClaim = "Admin" });
            else 
                return BadRequest(new { message = "Error S" });
        }
        #region *** HttpGet, 200 OK, 400 BadRequest, Authorize staff ***
        [HttpGet("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Logout()
        {
            try
            {
                auth = false;
                return Ok("User Logged out");
            }
            catch (Exception)
            {
                /// if there is any exception return bad request
                return BadRequest("Unable to sign-out");
            }
        }
    }
}
