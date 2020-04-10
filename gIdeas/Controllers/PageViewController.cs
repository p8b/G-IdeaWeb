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
    public class PageViewController : Controller
    {
        private gAppDbContext DbContext { get; }
        private List<gError> ErrorsList = new List<gError>();

        /// <summary>
        ///     Class Constructor. Set the local properties
        /// </summary>
        /// <param name="db">Receive the AppDbContext instance from the ASP.Net Pipeline</param>
        public PageViewController(gAppDbContext db) => DbContext = db;

        /// <summary>
        /// Get a list of page view object which shows their view count.
        /// </summary>
        #region *** 200 OK, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await DbContext.PageViews.LoadAsync().ConfigureAwait(false);
                /// return the list of Role ordered by name
                return Ok(DbContext.PageViews);
            }
            catch (Exception)
            {
                /// in the case any exceptions return the following error
                gAppConst.Error(ref ErrorsList, "Server Error. Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }

        /// <summary>
        ///     Post a new page view record
        /// </summary>
        #region *** 200 ok, 400 BadRequest ***
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("[action]/{ideaId?}")]
        public async Task<IActionResult> Post(int ideaId = 0)
        {
            try
            {
                string[] referer = Request.Headers["Referer"].ToString().Split("/");

                string requestedPageName = "Unknown";
                try
                {
                    requestedPageName = referer[3];
                    if (string.IsNullOrWhiteSpace(requestedPageName))
                        requestedPageName = "Home";
                }
                catch (Exception) { }
                gPageView currentPageView = null;
                try
                {
                    currentPageView = await DbContext.PageViews.FirstAsync(p => p.PageName.Equals(requestedPageName, StringComparison.CurrentCultureIgnoreCase)).ConfigureAwait(false);
                }catch (Exception){}
                if (currentPageView == null)
                {
                    currentPageView = new gPageView { PageName = requestedPageName, PageCount = 1 };
                    await DbContext.PageViews.AddAsync(currentPageView).ConfigureAwait(false);
                }
                else
                {
                    currentPageView.PageCount++;
                    DbContext.PageViews.Update(currentPageView);
                }

                if(ideaId > 0)
                {
                    gIdea idea = await DbContext.Ideas.FindAsync(ideaId).ConfigureAwait(false);
                    idea.ViewCount++;
                    DbContext.Ideas.Update(idea);
                }

                /// save the changes to the data base
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                /// return 200 ok status
                return Ok();
            }
            catch (Exception) // DbUpdateException, DbUpdateConcurrencyException
            {
                /// Add the error below to the error list and return bad request
                gAppConst.Error(ref ErrorsList, "Server Error.Please Contact Administrator.");
                return BadRequest(ErrorsList);
            }
        }
    }
}
