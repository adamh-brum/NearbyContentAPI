namespace API.DataLogic.Models
{
    using System;

    /// <summary>
    /// A class to describe beacon availability
    /// </summary>
    public class BeaconBooking
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description { get; set; }

        public int ContentId { get; set; }
    }
}