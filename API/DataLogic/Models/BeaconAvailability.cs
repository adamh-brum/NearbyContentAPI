namespace  API.DataLogic.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A class to describe beacon availability
    /// </summary>
    public class BeaconAvailability
    {
        public Guid BeaconId { get; set; }

        public string Location { get; set; }

        public string FriendlyName { get; set; }

        public IList<BeaconBooking> Bookings { get; set; }

        public IList<DateTime> FutureDates { get; set; }
    }
}