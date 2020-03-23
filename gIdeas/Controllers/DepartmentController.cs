﻿using gIdeas.Models;
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
        [Authorize(gAppConst.AccessPolicies.LevelFour)]
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
        [HttpGet("[action]/statistics")]
        [Authorize(gAppConst.AccessPolicies.LevelOne)]
        public IActionResult Get(bool isNull = false)
        {
            try
            {
                List<gDepartment> departments = DbContext.Departments.OrderBy(o => o.Name)
                                                                     .Take(DbContext.Departments.Count())
                                                                     .ToList();

                foreach (var dep in departments)
                {
                    dep.TotalNumberOfContributors = DbContext.Users.Count(u => u.Ideas.Any() || u.Comments.Any());
                    dep.TotalNumberOfIdeas = DbContext.Ideas.Count(i => i.Author.Department == dep);
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
        #region *** 201 Created, 400 BadRequest, Authorize Admin ***
        [HttpPost("[action]")]
        //[Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
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
                if (DbContext.Departments.Any(d => d.Name == newDepartment.Name))
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
        #region *** 200 Ok, 400 BadRequest, Authorize Admin ***
        [HttpDelete("[action]")]
        // [Authorize(gAppConst.AccessClaims.Admin)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        public async Task<IActionResult> Delete([FromBody] gDepartment department)
        {
            try
            {
                /// if the Department record with the same id is not found
                if (!DbContext.Departments.Any(d => d.Id == department.Id))
                {
                    gAppConst.Error(ref ErrorsList, "Department not found");
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