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
    public class DepartmentController : ControllerBase
    {
        public gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public DepartmentController(gAppDbContext db)
        {
            DbContext = db;
        }

        /// <summary>
        /// Used to get a list of all departments
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        [Authorize(gAppConst.AccessPolicies.LevelFour)]  /// Done
        public IActionResult Get()
        {
            try
            {
                /// return the list of departments ordered by name
                return Ok(DbContext.Departments.OrderBy(o => o.Name).Take(DbContext.Departments.Count()));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Department list not found");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        /// Used to get a list of all departments including their statistics
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                await DbContext.Departments.LoadAsync().ConfigureAwait(false);
                List<gDepartment> departments = await DbContext.Departments.OrderBy(o => o.Name)
                                                                           .ToListAsync()
                                                                           .ConfigureAwait(false);

                foreach (var dep in departments)
                {
                    dep.TotalNumberOfContributors = await DbContext.Users.CountAsync(u => u.Ideas.Any() || u.Comments.Any()).ConfigureAwait(false);
                    dep.TotalNumberOfIdeas = await DbContext.Ideas.CountAsync(i => i.Author.Department == dep).ConfigureAwait(false);
                    dep.TotalPercentageOfIdeas = (dep.TotalNumberOfIdeas / DbContext.Ideas.Count()) * 100;
                }
                /// return the list of departments ordered by name
                return Ok(departments);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Create a new Department
        /// </summary>
        #region *** 201 Created, 400 BadRequest ***
        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> Post([FromBody] gDepartment newDepartment)
        {
            try
            {
                /// if model validation failed
                if (!TryValidateModel(newDepartment))
                {
                    gAppConst.ExtractErrors(ModelState, ref ErrorsList);
                    /// return bad request with all the errors
                    return BadRequest(ErrorsList);
                }

                /// check the database to see if a department with the same name exists
                if (await DbContext.Departments.AnyAsync(d => d.Name == newDepartment.Name).ConfigureAwait(false))
                {
                    /// extract the errors and return bad request containing the errors
                    gAppConst.Error(ref ErrorsList, "Department already exists.");
                    return BadRequest(ErrorsList);
                }

                /// else department object is made without any errors
                /// Add the new department to the EF context
                await DbContext.Departments.AddAsync(newDepartment).ConfigureAwait(false);

                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 201 created status with the new object
                /// and success message
                return Created("Success", newDepartment);
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
        
        /// <summary>
        /// Delete Department
        /// </summary>
        #region *** 200 Ok, 400 BadRequest ***
        [HttpDelete("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [Authorize(gAppConst.AccessPolicies.LevelOne)]  /// Done
        public async Task<IActionResult> Delete([FromBody] gDepartment department)
        {
            try
            {
                /// if the Department record with the same id is not found
                if (!await DbContext.Departments.AnyAsync(d => d.Id == department.Id).ConfigureAwait(false))
                {
                    gAppConst.Error(ref ErrorsList, "Department not found");
                    return BadRequest(ErrorsList);
                }


                /// If the department is in use by any user then do not allow delete
                if (await DbContext.Users.AnyAsync(u => u.Department.Id == department.Id).ConfigureAwait(false))
                {
                    gAppConst.Error(ref ErrorsList, "Category tag is in use by an idea.");
                    return BadRequest(ErrorsList);
                }

                /// else the department is found
                /// now delete the department record
                DbContext.Departments.Remove(department);
                /// save the changes to the database
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 Ok status
                return Ok($"Department '{department.Name}' was deleted");
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