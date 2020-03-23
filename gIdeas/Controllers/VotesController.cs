using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        public gAppDbContext DbContext { get; }
        private gVotes Vote { get; set; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public VoteController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Used to get a list of all Vote
        /// </summary>
        /// <returns></returns>
        #region *** Get, 200 OK, 400 BadRequest ***
        [HttpGet("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public IActionResult Get()
        {
            try
            {
                /// return the list of Vote ordered by name
                return Ok(DbContext.Votes.OrderBy(o => o.Thumb).Take(DbContext.Votes.Count()));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Vote list not found");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new Vote
        /// </summary>
        /// <param name="jsonData">New Votet Name</param>
        #region *** Post, 201 Created, 400 BadRequest, Authorize Admin ***
        [HttpPost("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public IActionResult Post([FromBody] dynamic jsonData)
        {
            try
            {
                /// extract the info from jsonData received
                Vote = new gVotes { Thumb = jsonData.Thumb };
            }
            catch (Exception) /// catch error if jsonData is null
            {
                gAppConst.Error(ref ErrorsList, "Invalid Inputs.");
                return BadRequest(ErrorsList);
            }

            /// if model validation failed
            if (!TryValidateModel(Vote))
            {
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                /// return bad request with all the errors
                return BadRequest(ErrorsList);
            }

            /// check the database to see if a Vote with the same name exists
            if (DbContext.Votes.Any(d => d.Thumb == Vote.Thumb))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.Error(ref ErrorsList, "Vote already exists.");
                return BadRequest(ErrorsList);
            }

            /// else Vote object is made without any errors
            /// Add the new Vote to the EF context
            DbContext.Votes.Add(Vote);

            try
            {
                /// save the changes to the data base
                DbContext.SaveChanges();
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", Vote);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Unable to create new Vote");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Update Vote
        /// </summary>
        /// <param name="jsonData">
        /// {
        ///     int: id,
        ///     string: name
        /// }
        /// </param>
        #region *** Put, 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpPut("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public IActionResult Put([FromBody] dynamic jsonData)
        {
            try
            {
                /// extract the info from jsonData received
                Vote = new gVotes { UserId = jsonData.id, Thumb = jsonData.Thumb };
            }
            catch (Exception) /// catch error if jsonData is null
            {
                gAppConst.Error(ref ErrorsList, "The input is incorrect");
                return BadRequest(ErrorsList);
            }

            /// Try to validate the model and if it failed
            if (!TryValidateModel(Vote))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                return BadRequest(ErrorsList);
            }

            /// if the Vote record with the same id is not found
            if (!DbContext.Votes.Any(v => v.UserId == Vote.UserId))
            {
                gAppConst.Error(ref ErrorsList, "Vote not found");
                return BadRequest(ErrorsList);
            }

            /// else dVote object has no errors
            /// thus update Vote in the context
            DbContext.Votes.Update(Vote);

            try
            {
                /// save the changes to the database
                DbContext.SaveChanges();
                /// thus return 200 ok status with the updated object
                return Ok(Vote);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Unable to update department : {Vote.Thumb }");
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
        public IActionResult Delete([FromBody] dynamic jsonData)
        {
            try
            {
                /// extract the info from jsonData received
                Vote = new gVotes { UserId = jsonData.id };
            }
            catch (Exception)/// catch error if jsonData is null
            {
                gAppConst.Error(ref ErrorsList, "The input is incorrect");
                return BadRequest(ErrorsList);
            }

            /// if the Vote record with the same id is not found
            if (!DbContext.Votes.Any(v => v.UserId == Vote.UserId))
            {
                gAppConst.Error(ref ErrorsList, "Vote not found");
                return BadRequest(ErrorsList);
            }

            /// else the Vote is found
            /// now delete the Vote record
            DbContext.Votes.Remove(Vote);
            try
            {
                /// save the changes to the database
                DbContext.SaveChanges();
                /// return 200 Ok status
                return Ok($"Vote '{Vote.Thumb}' was deleted");
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad Vote
                gAppConst.Error(ref ErrorsList, $"Unable to delete Vote : {Vote.Thumb }");
                return BadRequest(ErrorsList);
            }
        }

    }
}