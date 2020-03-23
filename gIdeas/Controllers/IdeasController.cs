using gIdeas;
using gIdeas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public IActionResult Get(int submissionYear, string categoryTagId, string departmentId, 
                                string roleId, string ideaStatus, string isAuthorAnon)
        {
            try
            {
                /// Populate the list of ideas to be returned
                List<gIdea> IdeaList = new List<gIdea>();


                ///TODO: Search for the idea with the above parameters
                ///CHECK {categoryTagId}/{departmentId}/{roleId}/{ideaStatus}/{isAuthorAnon}
                ///if the value of them is the value of gAppConst.AllRecords return all records
                ///for that filter
                /// Include all the relationship connected to idea
                /// 

                /// return the list of departments ordered by name
                return Ok(IdeaList);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Department list not found");
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
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Post([FromBody] gIdea newIdea)
        {
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

            try
            {
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
        ///     Update Department
        /// </summary>
        /// <param name="jsonData">
        /// {
        ///     int: id,
        ///     string: name
        /// }
        /// </param>
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpPut("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public async Task<IActionResult> Put([FromBody] gIdea modifiedIdea)
        {

            /// Try to validate the model and if it failed
            if (!TryValidateModel(modifiedIdea))
            {
                /// extract the errors and return bad request containing the errors
                gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                return BadRequest(ErrorsList);
            }

            /// if the department record with the same id is not found
            if (!DbContext.Ideas.Any(d => d.Id == modifiedIdea.Id))
            {
                gAppConst.Error(ref ErrorsList, "Department not found");
                return BadRequest(ErrorsList);
            }

            /// else department object has no errors
            /// thus update department in the context
            DbContext.Ideas.Update(modifiedIdea);

            try
            {
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
        /// Delete an idea record
        /// </summary>
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpDelete("[action]")]
        // [Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
        public IActionResult Delete([FromBody] gIdea Idea)
        {
            /// if the Department record with the same id is not found
            if (!DbContext.Ideas.Any(d => d.Id == Idea.Id))
            {
                gAppConst.Error(ref ErrorsList, "Idea not found");
                return BadRequest(ErrorsList);
            }

            /// else the department is found
            /// now delete the department record
            DbContext.Ideas.Remove(Idea);
            try
            {
                /// save the changes to the database
                DbContext.SaveChanges();
                /// return 200 Ok status
                return Ok($"Idea was deleted");
            }
            catch (Exception)
            {
                /// Add the error below to the error list and return bad Department
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}