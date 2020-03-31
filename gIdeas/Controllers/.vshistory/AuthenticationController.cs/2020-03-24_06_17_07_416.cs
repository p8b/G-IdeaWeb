using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public gAppDbContext DbContext { get; }
        public SignInManager<gUser> LoginManager { get; }
        public UserManager<gUser> UserManager { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the gAppDbContext instance from the ASP.Net Pipeline</param>
        /// <param name="sm)">Receive the SignInManager instance from the ASP.Net Pipeline</param>
        /// <param name="um)">Receive the UserManager instance from the ASP.Net Pipeline</param>
        public AuthenticationController(gAppDbContext db, SignInManager<gUser> sm, UserManager<gUser> um)
        {
            DbContext = db;
            LoginManager = sm;
            UserManager = um;
        }

        #region *** 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] dynamic jsonData)
        {
            gAppConst.GetBrowserDetails(Request.Headers["User-Agent"].ToString(), out string BrowserName);
            bool rememberMe;
            gUser user;
            gLoginRecord loginRecord;
            try
            {
                /// extract the info from jsonData received
                user = new gUser
                {
                    Email = jsonData.email,
                    PasswordHash = jsonData.password
                };
                loginRecord = new gLoginRecord
                {
                    BrowserName = BrowserName,
                    User = user,
                };
                rememberMe = (bool)jsonData.rememberMe;
            }
            catch (Exception) /// catch error if jsonData is null
            {
                gAppConst.Error(ref ErrorsList, "Invalid Input. Please Contact Admin.");
                return BadRequest(ErrorsList);
            }
            try
            {
                /// check if the email address exists. if not return bad request
                if (!DbContext.Users.Any(u => u.Email == user.Email))
                {
                    gAppConst.Error(ref ErrorsList, "Wrong Email.", "Email");
                    return BadRequest(ErrorsList.ToArray());
                }

                /// else user with the specified email is found. then get the user object from database
                gUser userDetails = DbContext.Users.Include(u => u.Department)
                                                    .Include(u => u.Role)
                                                    .Where(u => u.Email == user.Email)
                                                    .FirstOrDefault();

                /// try to sign-in the user by using the password provided
                var loginResult = await LoginManager.PasswordSignInAsync(userDetails, user.PasswordHash, rememberMe, false).ConfigureAwait(false);

                /// if sign-in succeeded then return OK status
                if (loginResult.Succeeded)
                {
                    /// get the last login record
                    gLoginRecord LastLogin = await DbContext.LoginRecords.LastAsync(l => l.User.Id == userDetails.Id).ConfigureAwait(false);
                    /// If it is not the user's first login then populate the current users last login date property
                    if (LastLogin != null)
                        userDetails.LastLoginDate = LastLogin.TimeStamp;

                    /// Add the login record and return the user's details
                    await DbContext.LoginRecords.AddAsync(loginRecord).ConfigureAwait(false);
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);
                    return Ok(userDetails);
                }

                /// else user has entered the wrong password
                gAppConst.Error(ref ErrorsList, "Wrong Password", "Password");
                return BadRequest(ErrorsList);
            }
            catch (Exception eee)
            {
                /// if there are any exception return login failed error
                gAppConst.Error(ref ErrorsList, "Login Failed");
                return BadRequest(ErrorsList);
            }
        }

        #region *** HttpGet, 200 OK, 400 BadRequest, Authorize staff ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        [Authorize(Policy = gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Silent()
        {
            try
            {
                // return 200 response with 
                return Ok(await UserManager.GetUserAsync(User).ConfigureAwait(false));
            }
            catch (Exception)
            {
                /// if there are any exception return login failed error
                gAppConst.Error(ref ErrorsList, "Authentication Failed");
                return Unauthorized(ErrorsList);
            }
        }

        #region *** 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        [Authorize(Policy = gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                /// try to sign-out the user
                await LoginManager.SignOutAsync().ConfigureAwait(false);
                /// and return ok status
                return Ok("User Logged out");
            }
            catch (Exception)
            {
                /// if there are any exception return login failed error
                gAppConst.Error(ref ErrorsList, "Unable to sign-out");
                return Unauthorized(ErrorsList);
            }
        }
    }
}
