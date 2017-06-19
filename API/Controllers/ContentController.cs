using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Models;
using API.DataLogic.Models;
using API.DataLogic;
using Microsoft.AspNetCore.Mvc;
using API.DataLogic.ViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        /// <summary>
        /// Data logic for accessing the content
        /// </summary>
        private IContentDataLogic contentDataLogic;

        /// <summary>
        /// The data logic to access content schedule
        /// </summary>
        private IScheduleDataLogic scheduleDataLogic;

        public ContentController()
        {
            this.contentDataLogic = new SqliteContentDataLogic();
            this.scheduleDataLogic = new SqliteScheduleDataLogic();
        }

        // GET api/values
        [HttpGet("{contentId}")]
        public Content Get([FromQuery] int contentId)
        {
            return this.contentDataLogic.GetContent(contentId);
        }

        // GET api/values
        [HttpGet]
        [Route("All")]
        public IEnumerable<Content> Get()
        {
            var content = this.contentDataLogic.GetContent().ToList();
            return content;
        }

        // POST api/values
        [HttpPost]
        public SubmissionStatus Post(string shortDescription, string content)
        {
            // Create content
            SubmissionStatus status = new SubmissionStatus()
            {
                StatusCode = SubmissionStatusCode.Success
            };

            try
            {
                int contentId = this.contentDataLogic.AddContent(shortDescription, content);
                if (contentId == 0)
                {
                    status.StatusCode = SubmissionStatusCode.Failure;
                    status.Messages.Add("Write to database failed for new content");
                }
            }
            catch
            {
                status.StatusCode = SubmissionStatusCode.Failure;
                status.Messages.Add("Critical error.");
            }

            return status;
        }
    }
}
