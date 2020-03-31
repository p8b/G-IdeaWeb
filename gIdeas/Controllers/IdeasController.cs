﻿using gIdeas;
using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Ideas.Controllers
{
    [Route("api/[controller]")]
    public class IdeasController : ControllerBase
    {
        public gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public IdeasController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Used to get a list of ideas used for filtering 
        /// </summary>
        /// <returns></returns>
        #region *** Get, 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]/{submissionYear}/{categoryTagId}/{departmentId}/{roleId}/{ideaStatus}/{isAuthorAnon}")]
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done
        public async Task<IActionResult> Get(int submissionYear, string categoryTagId, string departmentId,
                                string roleId, string ideaStatus, string isAuthorAnon)
        {
            try
            {

                ///TODO: Search for the idea with the above parameters
                ///CHECK {categoryTagId}/{departmentId}/{roleId}/{ideaStatus}/{isAuthorAnon}
                ///if the value of them is the value of gAppConst.AllRecords return all records
                ///for that filter
                /// Include all the relationship connected to idea
                /// 

                int.TryParse(categoryTagId, out int CatTagId);
                int.TryParse(departmentId, out int DepId);
                int.TryParse(roleId, out int RoleId);

                /// Populate the list of ideas to be returned
                List<gIdea> IdeaList = await DbContext.Ideas.Include(i => CatTagId == 0 ? i.CategoriesToIdeas.Any() : i.CategoriesToIdeas.Any(ci => ci.CategoryId == CatTagId))
                                                            .Include(i => DepId == 0 ? i.Author.Department.Id > 0 : i.Author.Department.Id == DepId)
                                                            .Include(i => RoleId == 0 ? i.Author.Role.Id > 0 : i.Author.Role.Id == RoleId)
                                                            .Include(i => ideaStatus == gAppConst.AllRecords ? i.Status.Any() : i.Status.Contains(ideaStatus))
                                                            .Where(i => i.CreatedDate.Year == submissionYear 
                                                                    && !i.Author.IsBlocked && isAuthorAnon == gAppConst.AllRecords ? true : i.IsAnonymous.ToString().Equals(isAuthorAnon, StringComparison.CurrentCultureIgnoreCase)
                                                                    && !i.IsBlocked)
                                                            .ToListAsync()
                                                            .ConfigureAwait(false);

                foreach (gIdea idea in IdeaList)
                {
                    idea.TotalThumbDowns = DbContext.Votes.Count(v => v.Thumb.Equals("Down"));
                    idea.TotalThumbUps = DbContext.Votes.Count(v => v.Thumb.Equals("Up"));

                    string currentUserAccessClaim = User.Claims.FirstOrDefault(c => c.Type == gAppConst.AccessClaims.Type).Value;
                    if (idea.IsAnonymous && currentUserAccessClaim.Equals(gAppConst.AccessClaims.Staff))
                    {
                        idea.Author = new gUser { FirstName = "Anonymous", Surname = "Anonymous" };
                        idea.FlaggedIdeas = new List<gFlaggedIdea>();
                    }
                }

                /// return the list of ideas
                return Ok(IdeaList);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Return the selected idea 
        /// </summary>
        /// <returns></returns>
        #region *** Get, 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]/{ideaId}")]
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done
        public async Task<IActionResult> Get(int ideaId)
        {
            try
            {
                /// find the specified idea
                gIdea idea = await DbContext.Ideas.Include(i=> i.Author)
                                                  .Include(i=> i.CategoriesToIdeas)
                                                  .Include(i=> i.Comments)
                                                  .Include(i=> i.Documents)
                                                  .Include(i=> i.FlaggedIdeas)
                                                  .FirstOrDefaultAsync(i => i.Id == ideaId )
                                                  .ConfigureAwait(false);

                if (idea == null)
                {
                    gAppConst.Error(ref ErrorsList, "Idea not found");
                    return BadRequest(ErrorsList);
                }

                if (idea.Author.IsBlocked)
                {
                    gAppConst.Error(ref ErrorsList, "User is blocked");
                    return BadRequest(ErrorsList);
                }
                if (idea.IsBlocked)
                {
                    gAppConst.Error(ref ErrorsList, "Idea is blocked");
                    return BadRequest(ErrorsList);
                }


                string currentUserAccessClaim = User.Claims.FirstOrDefault(c => c.Type == gAppConst.AccessClaims.Type).Value;
                if (idea.IsAnonymous && currentUserAccessClaim.Equals(gAppConst.AccessClaims.Staff))
                {
                    idea.Author = new gUser { FirstName = "Anonymous", Surname = "Anonymous" };
                    idea.FlaggedIdeas = new List<gFlaggedIdea>();
                }

                idea.TotalThumbDowns = DbContext.Votes.Count(v => v.Thumb.Equals("Down"));
                idea.TotalThumbUps = DbContext.Votes.Count(v => v.Thumb.Equals("Up"));

                foreach (gComment comment in idea.Comments)
                    if(comment.IsAnonymous)
                        comment.User = new gUser { FirstName = "Anonymous", Surname = "Anonymous", Department = comment.User.Department };

                /// return the idea
                return Ok(idea);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new idea
        /// </summary>
        #region *** 201 Created, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done MUST BE TESTED
        public async Task<IActionResult> Post([FromBody] gIdea newIdea)
        {
            try
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out int userId);
                newIdea.Author = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(false);
                newIdea.Status = gAppConst.IdeaStatus.Pending;
                newIdea.CreatedDate = DateTime.UtcNow;

                gClosureDates closureDates = await DbContext.ClosureDates.FirstOrDefaultAsync().ConfigureAwait(false);
                newIdea.FirstClosureDate = DateTime.UtcNow.AddDays(closureDates.FirstClosure);
                newIdea.ClosureDate = DateTime.UtcNow.AddDays(closureDates.FinalClosure);

                /// if model validation failed
                if (!TryValidateModel(newIdea))
                {
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    /// return bad request with all the errors
                    return BadRequest(ErrorsList);

                }
                /// else department object is made without any errors
                /// Add the new department to the EF context
                DbContext.Ideas.Add(newIdea);
                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newIdea);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Update Idea
        /// </summary>
        /// </param>
        #region *** 200 Ok, 400 BadRequest ***
        [HttpPut("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done MUST BE TESTED
        public async Task<IActionResult> Put([FromBody] gIdea modifiedIdea)
        {

            try
            {
                /// if the idea record with the same id is not found
                if (!DbContext.Ideas.Any(d => d.Id == modifiedIdea.Id))
                {
                    gAppConst.Error(ref ErrorsList, "Idea not found");
                    return BadRequest(ErrorsList);
                }

                /// if the current user is not the author of the idea
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out int userId);
                if (!DbContext.Ideas.Any(i => i.Id == modifiedIdea.Id && i.Author.Id != userId))
                {
                    gAppConst.Error(ref ErrorsList, "Not authorised to edit!");
                    return BadRequest(ErrorsList);
                }

                /// Make sure these values cannot be updated
                gIdea oldVlaues = DbContext.Ideas.AsNoTracking().FirstOrDefault(i => i.Id == modifiedIdea.Id);
                modifiedIdea.Author = oldVlaues.Author;
                modifiedIdea.Status = oldVlaues.Status;
                modifiedIdea.CreatedDate = oldVlaues.CreatedDate;
                modifiedIdea.FirstClosureDate = oldVlaues.FirstClosureDate;
                modifiedIdea.ClosureDate = oldVlaues.ClosureDate;
                modifiedIdea.IsBlocked = oldVlaues.IsBlocked;

                /// Try to validate the model and if it failed
                if (!TryValidateModel(modifiedIdea))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    return BadRequest(ErrorsList);
                }


                /// else department object has no errors
                /// thus update department in the context
                DbContext.Ideas.Update(modifiedIdea);
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// thus return 200 ok status with the updated object
                return Ok(modifiedIdea);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Block/ unblock an idea
        /// </summary>
        /// </param>
        #region *** 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPut("[action]/{ideaId}/{isBlocked}")]
        [Authorize(gAppConst.AccessPolicies.LevelThree)] /// Done 
        public async Task<IActionResult> Put(int ideaId, bool isBlocked)
        {

            try
            {
                /// if the idea record with the same id is not found
                if (!DbContext.Ideas.Any(d => d.Id == ideaId))
                {
                    gAppConst.Error(ref ErrorsList, "Idea not found");
                    return BadRequest(ErrorsList);
                }

                gIdea idea = DbContext.Ideas.FirstOrDefault(i => i.Id == ideaId);

                idea.IsBlocked = isBlocked;

                /// save the changes to the database
                DbContext.Ideas.Update(idea);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                
                /// thus return 200 ok status with the updated object
                return Ok(idea);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Post a thumb up/down for the specified idea
        /// </summary>
        /// </param>
        #region *** 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("post/vote/{ideaId}/{upOrDown}")]
        [Authorize(gAppConst.AccessPolicies.LevelFour)] /// Done 
        public async Task<IActionResult> PostVote(int ideaId, bool upOrDown)
        {

            try
            {
                /// if the idea record with the same id is not found
                if (!DbContext.Ideas.Any(d => d.Id == ideaId))
                {
                    gAppConst.Error(ref ErrorsList, "Idea not found");
                    return BadRequest(ErrorsList);
                }

                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out int userId);

                /// if the user's vote for the idea already exists
                if (!DbContext.Votes.Any(v => v.IdeaId == ideaId && v.UserId == userId))
                {
                    gAppConst.Error(ref ErrorsList, "Vote Already registered!");
                    return BadRequest(ErrorsList);
                }

                gVotes vote = new gVotes
                {
                    IdeaId = ideaId,
                    UserId = userId,
                    Thumb = upOrDown ? "Up" : "Down"
                };

                /// save the changes to the database
                DbContext.Votes.Update(vote);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                
                /// thus return 200 ok status with the updated object
                return Ok(vote);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}