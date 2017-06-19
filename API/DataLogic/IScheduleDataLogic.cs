namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;
    using API.Controllers.Models;

    public interface IScheduleDataLogic
    {
        /// <summary>
        /// Schedules content as defined by the bookings
        /// </summary>
        /// <param name="bookings">Bookings to be scheduled</param>
        SubmissionStatus ScheduleContent(IEnumerable<BeaconBookingModel> bookings);

        /// <summary>
        /// Returns content currently scheduled for a given beacon
        /// </summary>
        /// <param name="beaconId"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        ViewModels.ScheduledContent GetScheduledContent(Guid beaconId, DateTime currentTime);

        /// <summary>
        /// A complex report, which weighs a range of factors to determine beacon availability
        /// </summary>
        /// <returns>Returns all beacons, and what content is booked to use them</returns>
        IEnumerable<BeaconAvailability> GetFutureScheduledContent();
    }
}