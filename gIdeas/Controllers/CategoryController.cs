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
    public class CategoryController : ControllerBase
    {
        public gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public CategoryController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Used to get a list of all categories
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                /// return the list of Category ordered by name
                return Ok(DbContext.Categories.OrderBy(o => o.Name).Take(DbContext.Categories.Count()));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new Category
        /// </summary>
        #region *** 201 Created, 400 BadRequest, Authorize Admin ***
        [HttpPost("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Post([FromBody] gCategoryTag newCategory)
        {
            try
            {
                /// if model validation failed
                if (!TryValidateModel(newCategory))
                {
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    /// return bad request with all the errors
                    return BadRequest(ErrorsList);
                }

                /// check the database to see if a department with the same name exists
                if (!DbContext.Categories.Any(d => d.Name == newCategory.Name))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.Error(ref ErrorsList, "Category already exists.");
                    return BadRequest(ErrorsList);
                }

                /// else Category object is made without any errors
                /// Add the new Category to the EF context
                await DbContext.Categories.AddAsync(newCategory).ConfigureAwait(false);

                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newCategory);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Update Category
        /// </summary>
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpPut("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Put([FromBody] gCategoryTag modifiedCategory)
        {
            try
            {
                /// Try to validate the model and if it failed
                if (!TryValidateModel(modifiedCategory))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    return BadRequest(ErrorsList);
                }

                /// if the Category record with the same id is not found
                if (!DbContext.Categories.Any(d => d.Id == modifiedCategory.Id))
                {
                    gAppConst.Error(ref ErrorsList, "Category not found");
                    return BadRequest(ErrorsList);
                }

                /// else department object has no errors
                /// thus update department in the context
                DbContext.Categories.Update(modifiedCategory);

                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// thus return 200 ok status with the updated object
                return Ok(modifiedCategory);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpDelete("[action]")]
        // [Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Delete([FromBody] gCategoryTag category)
        {
            /// if the Category record with the same id is not found
            if (!DbContext.Categories.Any(d => d.Id == category.Id))
            {
                gAppConst.Error(ref ErrorsList, "Category not found");
                return BadRequest(ErrorsList);
            }

            /// else the Category is found
            /// now delete the Category record
            DbContext.Categories.Remove(category);
            try
            {
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok($"Department '{category.Name}' was deleted");
            }
            catch (Exception)
            {
                /// Add the error below to the error list
                gAppConst.Error(ref ErrorsList, $"Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}