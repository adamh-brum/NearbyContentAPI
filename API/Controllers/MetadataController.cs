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
    public class MetadataController : Controller
    {
        private IMetadataDataLogic dataLogic;

        public MetadataController()
        {
            this.dataLogic = new SqliteMetadataDataLogic();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get(string key)
        {
            return this.dataLogic.GetMetadata(key);
        }

        // GET api/values
        [HttpGet]
        [Route("All")]
        public IEnumerable<Metadata> Get()
        {
            return this.dataLogic.GetMetadata();
        }

        // POST api/values
        [HttpPost]
        public SubmissionStatus Post(string key, string value)
        {
            var status = new SubmissionStatus();

            try
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }

                this.dataLogic.AddMetadata(key, value);
            }
            catch (Exception ex)
            {
                status.Messages.Add(ex.Message);
                status.StatusCode = SubmissionStatusCode.Success;
            }

            return status;
        }

        /// <summary>
        /// Deletes metadata item(s) that match the given key / value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        [HttpDelete("")]
        public SubmissionStatus Delete(string key, string value)
        {
            var status = new SubmissionStatus();

            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException();
                }

                this.dataLogic.DeleteMetadata(key, value);
            }
            catch (Exception ex)
            {
                status.Messages.Add(ex.Message);
                status.StatusCode = SubmissionStatusCode.Success;
            }

            return status;
        }
    }
}
