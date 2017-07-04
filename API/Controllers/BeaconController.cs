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
    public class BeaconController : Controller
    {
        private IBeaconDataLogic dataLogic;

        public BeaconController()
        {
            this.dataLogic = new SqliteBeaconDataLogic(); ;
        }

        // GET api/values
        [HttpGet("")]
        public IEnumerable<Beacon> Get()
        {
            return this.dataLogic.GetBeacons();
        }

        // POST api/values
        [HttpPost]
        public SubmissionStatus Post([FromBody]BeaconViewModel beacon)
        {
            SubmissionStatus status = new SubmissionStatus();
            status.StatusCode = SubmissionStatusCode.Success;

            var r = this.HttpContext.Request;
            string documentContents;
            using (System.IO.Stream receiveStream = r.Body)
            {
                using (System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }

            try
            {

                Guid guid = Guid.Parse(beacon.Id);

                // Add the beacon
                this.dataLogic.AddBeacon(guid, beacon.Name, beacon.FriendlyName, beacon.Location);
            }
            catch (Exception ex)
            {
                status.Messages.Add(ex.Message);
                status.StatusCode = SubmissionStatusCode.Failure;
            }

            return status;
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // DELETE api/values/5
        [HttpDelete("")]
        public SubmissionStatus Delete(string id)
        {
            SubmissionStatus status = new SubmissionStatus();

            try
            {
                Guid guid = Guid.Parse(id);
                this.dataLogic.DeleteBeacon(guid);
            }
            catch (Exception ex)
            {
                status.Messages.Add(ex.Message);
                status.StatusCode = SubmissionStatusCode.Failure;
            }

            return status;
        }
    }
}
