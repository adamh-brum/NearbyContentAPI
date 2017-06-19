using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Models;
using API.DataLogic.Models;
using API.DataLogic;
using Microsoft.AspNetCore.Mvc;

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

        // POST api/values
        [HttpPost]
        public void Post(string key, string value)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }

            this.dataLogic.AddMetadata(key, value);
        }

        /// <summary>
        /// Deletes metadata item(s) that match the given key / value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        [HttpDelete("")]
        public void Delete(string key, string value)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }

            this.dataLogic.DeleteMetadata(key, value);
        }

        /// <summary>
        /// Deletes metadata item(s) that match the given key 
        /// </summary>
        /// <param name="key">Key</param>
        [HttpDelete("")]
        public void Delete(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }

            this.dataLogic.DeleteMetadata(key, null);
        }
    }
}
