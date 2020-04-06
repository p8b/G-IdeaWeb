using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    public class FlaggedIdeaController : ControllerBase
    {
        public gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public FlaggedIdeaController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        ///     Create a new Flag idea record
        /// </summary>
        #region *** 201 Created, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done
        public async Task<IActionResult> Post([FromBody] gFlaggedIdea newflaggedIdea)
        {

            int.TryParse(User.Claims.First(c => c.Type == "UserId").Value, out int userId);
            newflaggedIdea.User.Id = userId;

            /// check the database if the same user has already posted flag with the same type
            if (await DbContext.FlaggedIdeas.AnyAsync(f => f.Type == newflaggedIdea.Type 
                                              && f.User.Id == newflaggedIdea.User.Id).ConfigureAwait(false))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.Error(ref ErrorsList, "Request already received.");
                return BadRequest(ErrorsList);
            }

            /// if model validation failed
            if (!TryValidateModel(newflaggedIdea))
            {
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                /// return bad request with all the errors
                return BadRequest(ErrorsList);
            }

            /// else gFlaggedIdeas object is made without any errors
            /// Add the new gFlaggedIdeas to the EF context
            await DbContext.FlaggedIdeas.AddAsync(newflaggedIdea).ConfigureAwait(false);

            try
            {
                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newflaggedIdea);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}