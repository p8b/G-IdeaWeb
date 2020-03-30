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
    public class CommentController : Controller
    {
        private gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public CommentController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Return all the comment for the selected idea sorted by submission date
        /// </summary>
        #region *** Get, 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]/{ideaId}")]
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public IActionResult Get(int ideaId)
        {
            try
            {
                /// return the list of Comments for the selected Idea id ordered by submission date
                return Ok(DbContext.Comments.Where(c => c.Idea.Id == ideaId).OrderByDescending(c => c.SubmissionDate));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Department list not found");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new Comment
        /// </summary>
        #region *** 201 Created, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Post([FromBody] gComment newComment)
        {
            try
            {
                /// if model validation failed
                if (!TryValidateModel(newComment))
                {
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    /// return bad request with all the errors
                    return BadRequest(ErrorsList);
                }

                /// check the database to see if a Comment with the same name exists
                if (DbContext.Comments.Any(d => d.Comment == newComment.Comment
                                         && d.User.Id == newComment.User.Id))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.Error(ref ErrorsList, "Comment already exists.");
                    return BadRequest(ErrorsList);
                }

                /// else Comment object is made without any errors
                /// Add the new Comment to the EF context
                await DbContext.Comments.AddAsync(newComment).ConfigureAwait(false);

                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newComment);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
        
        /// <summary>
        /// Delete Comment
        /// </summary>
        #region *** 200 Ok, 400 BadRequest***
        [HttpDelete("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        //[Authorize(gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Delete([FromBody] gComment comment)
        {
            try
            {
                /// if the Comment record with the same id is not found
                if (!DbContext.Comments.Any(d => d.Id == comment.Id))
                {
                    gAppConst.Error(ref ErrorsList, "Comment not found");
                    return BadRequest(ErrorsList);
                }

                /// else the Comment is found
                /// now delete the Comment record
                DbContext.Comments.Remove(comment);
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok($"Comment '{comment.Comment}' was deleted");
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad Comment
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}