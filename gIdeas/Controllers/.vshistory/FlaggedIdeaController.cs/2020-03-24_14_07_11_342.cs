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
        /// Get a list flagged records for an idea
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]/{ideaId}")]
        public async Task<IActionResult> Get(int ideaId)
        {
            try
            {
                /// return the list of gFlaggedIdeas ordered by name
                return Ok(await DbContext.Ideas.Include(i => i.FlaggedIdeas)
                                          .Where(i => i.Id == ideaId)
                                          .FirstOrDefaultAsync()
                                          .ConfigureAwait(false));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
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
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Post([FromBody] FlaggedIdea newflaggedIdea)
        {
            /// if model validation failed
            if (!TryValidateModel(newflaggedIdea))
            {
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                /// return bad request with all the errors
                return BadRequest(ErrorsList);
            }

            /// check the database if the same user has already posted flag with the same type
            if (DbContext.FlaggedIdeas.Any(f => f.Type == newflaggedIdea.Type 
                                              && f.Users.Id == newflaggedIdea.Users.Id))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.Error(ref ErrorsList, "Request already received.");
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

        #region *** Post, 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpDelete("[action]")]
        // [Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Delete([FromBody] FlaggedIdea flaggedIdea)
        {
            /// if the FlaggedIdeas record with the same id is not found
            if (!DbContext.FlaggedIdeas.Any(f => f.Id == flaggedIdea.Id))
            {
                gAppConst.Error(ref ErrorsList, "flagged Idea not found");
                return BadRequest(ErrorsList);
            }

            /// else the FlaggedIdeas is found
            /// now delete the FlaggedIdeas record
            DbContext.FlaggedIdeas.Remove(flaggedIdea);
            try
            {
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok(flaggedIdea);
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad FlaggedIdeas
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}