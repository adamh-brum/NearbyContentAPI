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
    public class BeaconController : Controller
    {
        private IBeaconDataLogic dataLogic;

        public BeaconController()
        {
            this.dataLogic = new SqliteBeaconDataLogic();;
        }

        // GET api/values
        [HttpGet("")]
        public IEnumerable<Beacon> Get()
        {
            return this.dataLogic.GetBeacons();
        }

        // POST api/values
        [HttpPost]
        public void Post(string id, string name, string friendlyName, string location)
        {
            Guid guid = Guid.Parse(id);

            // Add the beacon
            this.dataLogic.AddBeacon(guid, name, friendlyName, location);
        
            // Return success response
            //return CreatedAtRoute("Get", new { id = beacon.Id });
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // DELETE api/values/5
        [HttpDelete("")]
        public void Delete(string id)
        {
            Guid guid = Guid.Parse(id);
            this.dataLogic.DeleteBeacon(guid);
        }
    }
}
