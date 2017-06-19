
namespace API.Controllers.Models
{
    using System;

    /// <summary>
    /// Contains the information required to book a beacon
    /// </summary>
    public class BeaconBookingModel
    {
        public int ContentId { get; set; }

        public Guid BeaconId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}