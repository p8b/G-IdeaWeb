using gIdeas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gIdeas.DataGenerator
{
    [Route("api/[controller]/")] /// URL: api/User
    public class DataGenController : Controller
    {

        private gAppDbContext DbContext { get; }
        private UserManager<gUser> UserManager { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        /// <param name="um)">Receive the UserManager instance from the ASP.Net Pipeline</param>
        public DataGenController(gAppDbContext db, UserManager<gUser> um)
        {
            DbContext = db;
            UserManager = um;
        }

        // Create new User - api/user/post
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUsers()
        {
            try
            {
                DataGen dataGenerator = new DataGen();

                foreach (var department in dataGenerator.Departments)
                {
                    await DbContext.Departments.AddAsync(new gDepartment { Name = department.Name }).ConfigureAwait(false);
                }
                foreach (var role in dataGenerator.Roles)
                {
                    await DbContext.Roles.AddAsync(new gRole { Name = role.Name, AccessClaim = role.AccessClaim }).ConfigureAwait(false);
                }
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                DbContext.Departments.Load();
                DbContext.Roles.Load();
                var userList = dataGenerator.GetUserList();
                foreach (var newUser in userList)
                {
                    ModelState.Clear();
                    /// if model validation failed
                    if (!TryValidateModel(newUser))
                    {
                        gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                        /// return bad request with all the errors
                        return BadRequest(ErrorsList);
                    }
                    /// Create the new user
                    IdentityResult newUserResult = await UserManager.CreateAsync(newUser, newUser.PasswordHash).ConfigureAwait(false);


                    /// If result failed
                    if (!newUserResult.Succeeded)
                    {
                        /// Add the error below to the error list and return bad request
                        foreach (var error in newUserResult.Errors)
                        {
                            gAppConst.Error(ref ErrorsList, error.Description, error.Code);
                        }
                        return BadRequest(ErrorsList);
                    }

                    /// else result is successful the try to add the access claim for the user
                    IdentityResult addedClaimResult = await UserManager.AddClaimAsync(
                            newUser,
                            new Claim(gAppConst.AccessClaims.Type, newUser.Role.AccessClaim)
                        ).ConfigureAwait(false);
                }
                return Ok($"{userList.Count} user were created");
            }
            catch (Exception eee) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, eee.Message);
                gAppConst.Error(ref ErrorsList, eee.InnerException.Message);
                return BadRequest(ErrorsList);
            }
        }
    }
}
