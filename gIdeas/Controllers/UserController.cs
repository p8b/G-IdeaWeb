﻿using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    [Authorize(gAppConst.AccessPolicies.LevelFour)]
    public class UserController : Controller
    {
        private gAppDbContext DbContext { get; }
        private UserManager<gUser> UserManager { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        /// <param name="um)">Receive the UserManager instance from the ASP.Net Pipeline</param>
        public UserController(gAppDbContext db, UserManager<gUser> um)
        {
            DbContext = db;
            UserManager = um;
        }

        /// <summary>
        /// Used to get a list of all users
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]/{roleId}/{departmentId}/{searchValue?}")]
        [Authorize(gAppConst.AccessPolicies.LevelTwo)] /// Done
        public async Task<IActionResult> Get(string roleId, string departmentId, string searchValue = "")
        {
            try
            {
                //1.Check the search parameter and filters and return the appropriate user list
                //      a.If search value is empty or null then return the filtered users
                //          Note(Default value for parameters)
                //                    searchValue = null
                //ALL OTHER PARAMETERS = ***GET - ALL ***
                List<gUser> userList;
                /// populate the list to be returned
                if (string.IsNullOrWhiteSpace(searchValue))
                {

                    userList = await DbContext.Users.Include(u => departmentId == gAppConst.AllRecords ? u.Department.Id > 0 : u.Department.Id.ToString() == departmentId)
                                                    .Include(u => roleId == gAppConst.AllRecords ? u.Role.Id > 0 : u.Role.Id.ToString() == roleId)
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);
                }
                else
                {
                    int.TryParse(searchValue, out int userId);
                    userList = await DbContext.Users.Where(u => u.Id == userId
                                                             || u.FirstName.Contains(searchValue,StringComparison.CurrentCultureIgnoreCase) 
                                                             || u.Surname.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)
                                                             || u.Email.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase))
                                                    .Include(u => departmentId == gAppConst.AllRecords ? u.Department.Id > 0 : u.Department.Id.ToString() == departmentId)
                                                    .Include(u => roleId == gAppConst.AllRecords ? u.Role.Id > 0 : u.Role.Id.ToString() == roleId)
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);
                }

                foreach (gUser user in userList)
                {
                    user.TotalNumberOfIdeas = DbContext.Ideas.Count(i => i.Author == user);
                    user.TotalNumberOfComments = DbContext.Comments.Count(i => i.User == user);
                }

                /// return the list of Role ordered by name
                return Ok(userList);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new User
        /// </summary>
        #region *** 201 Created, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> Post([FromBody] gUser newUser)
        {
            try
            {
                newUser.PasswordHash = newUser.NewPassword;
                ModelState.Clear();
                /// if model validation failed
                if (!TryValidateModel(newUser))
                {
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    /// return bad request with all the errors
                    return BadRequest(ErrorsList);
                }

                /// check the database to see if a user with the same email exists
                if (DbContext.Users.Any(d => d.Email == newUser.Email))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.Error(ref ErrorsList, "Email already exists.");
                    return BadRequest(ErrorsList);
                }
                newUser.Department = DbContext.Departments.Find(newUser.Department.Id);
                newUser.Role = DbContext.Roles.Find(newUser.Role.Id);
                /// else user object is made without any errors
                /// Create the new user
                IdentityResult newUserResult = await UserManager.CreateAsync(newUser, newUser.PasswordHash)
                                                                .ConfigureAwait(false);


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

                /// if claim failed to be created
                if (!addedClaimResult.Succeeded)
                {
                    /// remove the user account and return appropriate error
                    DbContext.Users.Remove(newUser);
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);
                    gAppConst.Error(ref ErrorsList, "Server Issue. Please Contact Administrator.");
                    return BadRequest(ErrorsList);
                }

                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newUser);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Update user record
        /// </summary>
        #region *** Put, 200 Ok, 400 BadRequest ***
        [HttpPut("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> Put([FromBody] gUser modifiedUser)
        {
            try
            {
                /// Try to validate the model
                TryValidateModel(modifiedUser);
                /// remove the passwordHash and confrimPassword since
                /// the password update gets handled by another method in this class
                ModelState.Remove("PasswordHash");
                if (!ModelState.IsValid)
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    return BadRequest(ErrorsList);
                }

                /// if the user record with the same id is not found
                if (!DbContext.Users.Any(u => u.Id == modifiedUser.Id))
                {
                    gAppConst.Error(ref ErrorsList, "User not found");
                    return BadRequest(ErrorsList);
                }
                modifiedUser.Department = DbContext.Departments.Find(modifiedUser.Department.Id);
                modifiedUser.Role = DbContext.Roles.Find(modifiedUser.Role.Id);
                /// find the current user details from the database
                gUser userDetails = DbContext.Users.Find(modifiedUser.Id);

                /// update the user details with the new details
                userDetails.FirstName = modifiedUser.FirstName;
                userDetails.Surname = modifiedUser.Surname;
                userDetails.Email = modifiedUser.Email;
                userDetails.PhoneNumber = modifiedUser.PhoneNumber;
                userDetails.Role = modifiedUser.Role;
                userDetails.Department = modifiedUser.Department;

                /// thus update user in the context
                DbContext.Users.Update(userDetails);

                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// thus return 200 ok status with the updated object
                return Ok(userDetails);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Check if the user exists then block the user
        /// </summary>
        #region *** Put, 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelTwo)]
        [HttpPut("put/{userId}/{isBlocked}")]  /// Done
        public async Task<IActionResult> Put(int userId, bool isBlocked)
        {
            try
            {
                /// if the user with the same id is not found
                gUser user = await DbContext.Users.FindAsync(userId).ConfigureAwait(false);
                if (user == null)
                {
                    gAppConst.Error(ref ErrorsList, "User not found");
                    return BadRequest(ErrorsList);
                }

                user.IsBlocked = isBlocked;

                /// Try to validate the model
                TryValidateModel(user);
                if (!ModelState.IsValid)
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    return BadRequest(ErrorsList);
                }

                /// update user in the context
                DbContext.Users.Update(user);

                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// thus return 200 ok status with the updated object
                return Ok(user);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Update the password of current user
        /// </summary>
        #region *** Put, 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)]  /// Done
        [HttpPut("[action]")]
        public async Task<IActionResult> PutMyPassword([FromBody] gUser modifiedUser)
        {
            try
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out int userId);
                if(modifiedUser.Id != userId)
                {
                    gAppConst.Error(ref ErrorsList, "Not Authorised!");
                    return BadRequest(ErrorsList);
                }

                gUser result = await UpdatePassword(modifiedUser).ConfigureAwait(false);
                if (result != null)
                    return Ok(result);

                return BadRequest(ErrorsList);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Update the password of the user
        /// </summary>
        #region *** Put, 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        [HttpPut("[action]")]
        public async Task<IActionResult> PutPassword([FromBody] gUser modifiedUser)
        {
            try
            {
                gUser result = await UpdatePassword(modifiedUser).ConfigureAwait(false);
                if (result != null)
                    return Ok(result);

                return BadRequest(ErrorsList);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        #region *** Delete, 200 Ok, 400 BadRequest ***
        [HttpDelete("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> Delete([FromBody] gUser thisUser)
        {
            try
            {
                /// if the User record with the same id is not found
                if (!DbContext.Users.Any(u => u.Id == thisUser.Id))
                {
                    gAppConst.Error(ref ErrorsList, "User not found");
                    return BadRequest(ErrorsList);
                }

                /// else the User is found
                /// now delete the user record
                DbContext.Users.Remove(DbContext.Users.Find(thisUser.Id));

                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok($"User ID ('{thisUser.Id}') was deleted");
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad Department
                gAppConst.Error(ref ErrorsList, $"Server Issue. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        private async Task<gUser> UpdatePassword(gUser SelectedUser)
        {
            /// find the current user details from the database
            gUser userDetails = DbContext.Users.Find(SelectedUser.Id);

            if (userDetails == null)
            {
                gAppConst.Error(ref ErrorsList, "User not found!");
                return null;
            }

            /// generate new password reset token
            string passResetToken = await UserManager.GeneratePasswordResetTokenAsync(userDetails).ConfigureAwait(false);
            /// reset user's password
            IdentityResult result = await UserManager.ResetPasswordAsync(
                        userDetails, passResetToken, SelectedUser.NewPassword).ConfigureAwait(false);

            /// if result is Failed
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                    ErrorsList.Add(new gError(item.Code, item.Description));

                return null;
            }

            /// else the result is a success.
            return userDetails;
        }
    }
}