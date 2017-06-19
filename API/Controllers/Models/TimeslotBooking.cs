namespace API.Controllers.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents what a timeslot is booked by
    /// </summary>
    public class TimeslotBooking
    {
        /// <summary>
        /// The identifier of the content that has booked this timeslot
        /// </summary>
        /// <returns>Content ID</returns>
        public int ContentId { get; set; }

        /// <summary>
        /// Title of the content that has booked this slot
        /// </summary>
        /// <returns>Content title</returns>
        public string ContentTitle { get; set; }
    }
}