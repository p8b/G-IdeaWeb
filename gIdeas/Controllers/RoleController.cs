using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public RoleController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Used to get a list of all Roles
        /// </summary>
        /// <returns></returns>
        #region *** 200 OK, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                /// return the list of Role ordered by name
                return Ok(DbContext.Roles.OrderBy(o => o.Name).Take(DbContext.Roles.Count()));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new Role
        /// </summary>
        #region *** 201 Created, 400 BadRequest, Authorize Admin ***
        [HttpPost("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Post([FromBody] gRole newRole)
        {
            /// if model validation failed
            if (!TryValidateModel(newRole))
            {
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                /// return bad request with all the errors
                return BadRequest(ErrorsList);
            }

            /// check the database to see if a Role with the same name exists
            if (DbContext.Roles.Any(r => r.Name == newRole.Name))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.Error(ref ErrorsList, "Role already exists.");
                return BadRequest(ErrorsList);
            }

            /// else Role object is made without any errors
            /// Add the new Role to the EF context
            await DbContext.Roles.AddAsync(newRole).ConfigureAwait(false);

            try
            {
                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newRole);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error.Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Delete the Role
        /// </summary>
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpDelete("[action]")]
        // [Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Delete([FromBody] gRole role)
        {
            /// if the Role record with the same id is not found
            if (!DbContext.Roles.Any(d => d.Id == role.Id))
            {
                gAppConst.Error(ref ErrorsList, "Role not found");
                return BadRequest(ErrorsList);
            }

            /// else the Role is found
            /// now delete the Role record
            DbContext.Roles.Remove(role);
            try
            {
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok(role);
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad Role
                gAppConst.Error(ref ErrorsList, "Server Error.Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}