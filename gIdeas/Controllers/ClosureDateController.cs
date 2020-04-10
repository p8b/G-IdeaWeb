using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gIdeas.Models;
using System.Net.Mime;

namespace gIdeas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosureDateController : ControllerBase
    {
        private gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public ClosureDateController(gAppDbContext db) => DbContext = db;

        /// <summary>
        /// Get a list of closure dates.
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                /// return the list of Closure Dates objects
                return Ok(DbContext.ClosureDates.Take(DbContext.ClosureDates.Count()));
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Post a new closure dates record
        /// </summary>
        #region *** 200 Ok, 400 BadRequest ***
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("[action]")]
        public async Task<IActionResult> Post(gClosureDates newClosureDates)
        {
            try
            {
                gClosureDates currentClosureDates = null;
                try
                {
                    currentClosureDates = await DbContext.ClosureDates.FirstAsync(c => c.Year == newClosureDates.Year).ConfigureAwait(false);
                }
                catch (Exception) { }
                if (currentClosureDates == null)
                {
                    await DbContext.ClosureDates.AddAsync(newClosureDates).ConfigureAwait(false);
                }
                else
                {
                    currentClosureDates.FirstClosure = newClosureDates.FirstClosure;
                    currentClosureDates.FinalClosure = newClosureDates.FinalClosure;
                    DbContext.ClosureDates.Update(currentClosureDates);
                }

                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 ok status
                return Ok(currentClosureDates);
            }
            catch (Exception eee) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error.Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}
