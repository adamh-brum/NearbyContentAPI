namespace API.Controllers.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A timeslot is a unit of time to describe available times beacons can be booked at
    /// </summary>
    public class Timeslot
    {
        /// <summary>
        /// The unit for this timeslot
        /// </summary>
        /// <returns>Current timeslot unit (e.g.weeks)</returns>
        public TimeslotUnit Unit { get; set; }

        /// <summary>
        /// The timeslots within this timeslot (if any)
        /// E.g. within a week you have days, within days you have hours
        /// </summary>
        /// <returns>Timeslots within this timeslot</returns>
        public IList<Timeslot> Timeslots { get; set; }

        /// <summary>
        /// The value of this timeslot starts
        /// </summary>
        /// <returns>DateTime that represents this timeslot</returns>
        public DateTime Start { get; set; }

        /// <summary>
        /// The value of when this timeslot ends
        /// </summary>
        /// <returns>DateTime that represents this timeslot</returns>
        public DateTime End { get; set; }

        /// <summary>
        /// Any bookings that cover the entire duration oft his timeslot
        /// </summary>
        /// <returns>Bookings</returns>
        public IList<TimeslotBooking> Bookings { get; set; }
    }

    /// <summary>
    /// An enum to make it more friendly to describe different timeslots
    /// </summary>
    public enum TimeslotUnit
    {
        Hours = 1,
        Days = 2,
        Weeks = 3
    }
}