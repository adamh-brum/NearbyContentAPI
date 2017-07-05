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

            try
            {
                // Add the beacon
                IList<string> errors = this.dataLogic.AddBeacon(beacon.UUID, beacon.BeaconId, beacon.MinorVersion, beacon.MajorVersion, beacon.FriendlyName, beacon.Location);
                if(errors.Count > 0){
                    status.Messages = errors.ToList();
                    status.StatusCode = SubmissionStatusCode.Failure;
                }
            }
            catch (Exception ex)
            {
                status.Messages.Add(ex.Message);
                status.StatusCode = SubmissionStatusCode.Failure;
            }

            return status;
        }

        // POST api/values
        [HttpPut]
        public SubmissionStatus Put([FromBody]BeaconViewModel beacon)
        {
            SubmissionStatus status = new SubmissionStatus();
            status.StatusCode = SubmissionStatusCode.Success;

            try
            {
                // Add the beacon
                IList<string> errors = this.dataLogic.UpdateBeacon(beacon.Id, beacon.UUID, beacon.BeaconId, beacon.MinorVersion, beacon.MajorVersion, beacon.FriendlyName, beacon.Location);
                if(errors.Count > 0){
                    status.Messages = errors.ToList();
                    status.StatusCode = SubmissionStatusCode.Failure;
                }
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
        public SubmissionStatus Delete(int id)
        {
            SubmissionStatus status = new SubmissionStatus();

            try
            {
                this.dataLogic.DeleteBeacon(id);
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
