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
    public class ScheduleController : Controller
    {
        private IScheduleDataLogic dataLogic;

        public ScheduleController()
        {
            this.dataLogic = new SqliteScheduleDataLogic();
        }

        /// <summary>
        /// Determines every content for the specified location at the current time of day
        /// </summary>
        /// <param name="locationId">Beacon UUID</param>
        /// <returns>Content at the given location</returns>
        [HttpGet("{locationId}")]
        public IEnumerable<ContentModel> Get(string locationId)
        {
            bool parsed = false;
            Guid beaconId = Guid.Empty;
            parsed = Guid.TryParse(locationId, out beaconId);
            if(!parsed)
            {
                return null;
            }

            DateTime requestTime = DateTime.Now;
            var content = this.dataLogic.GetScheduledContent(beaconId, requestTime);
            return content.Content.Select(c => new ContentModel()
            {
                Id = c.Id,
                LocationName = content.Location,
                RequestDateTime = requestTime,
                ContentShortDescription = c.Title,
                Content = c.Value
            });
        }

        /// <summary>
        /// Returns the availability for every single beacon, based on the schedule, from today onwards
        /// Historical bookings are filtered out to reduce response size
        /// </summary>
        /// <returns>All future bookings and availability of beacons</returns>
        [HttpGet]
        public IEnumerable<BeaconSchedule> Get()
        {
            var schedule = new List<BeaconSchedule>();
            var availability = this.dataLogic.GetFutureScheduledContent();

            return schedule;
        }

        /// <summary>
        /// Shcedules content onto the system using the bookings
        /// </summary>
        /// <param name="bookings">The bookings</param>
        [HttpPost]
        public SubmissionStatus Post(IEnumerable<BeaconBookingModel> bookings)
        {
            return this.dataLogic.ScheduleContent(bookings);
        }
    }
}
