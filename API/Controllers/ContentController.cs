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

        /// <summary>
        /// The data logic to access content schedule
        /// </summary>
        private IMetadataDataLogic metadataDataLogic;

        public ContentController()
        {
            this.contentDataLogic = new SqliteContentDataLogic();
            this.scheduleDataLogic = new SqliteScheduleDataLogic();
            this.metadataDataLogic = new SqliteMetadataDataLogic();
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
        public IEnumerable<ContentViewModel> Get()
        {
            var content = this.contentDataLogic.GetContent().ToList();
            return content.Select(c => new ContentViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Content = c.Value,
                Tags = string.IsNullOrEmpty(c.Tags) ? null : c.Tags.Split(',').ToList()
            });
        }

        // POST api/values
        [HttpPost]
        public SubmissionStatus Post([FromBody]ContentViewModel content)
        {
            // Create content
            SubmissionStatus status = new SubmissionStatus()
            {
                StatusCode = SubmissionStatusCode.Success
            };

            int contentId = 0;

            if (content.Tags != null)
            {
                var contentTags = this.metadataDataLogic.GetMetadata("ContentTags");
                if (contentTags == null || contentTags.Count() == 0)
                {
                    contentTags = content.Tags;
                }

                contentTags = contentTags.Intersect(content.Tags);
                foreach (var contentTag in contentTags)
                {
                    this.metadataDataLogic.AddMetadata("ContentTags", contentTag);
                }
            }

            try
            {
                contentId = this.contentDataLogic.AddContent(content.Title, content.Content, content.Tags);
                if (contentId == 0)
                {
                    status.StatusCode = SubmissionStatusCode.Failure;
                    status.Messages.Add("Write to database failed for new content");
                }
            }
            catch (Exception ex)
            {
                status.StatusCode = SubmissionStatusCode.Failure;
                status.Messages.Add($"Critical error. Exception {ex.Message}. Inner: {ex.InnerException?.Message}");
            }

            return status;
        }
    }
}
