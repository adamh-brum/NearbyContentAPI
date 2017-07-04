
namespace API.Controllers.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains the information required to book a beacon
    /// </summary>
    public class BeaconBookingsModel
    {
        public IEnumerable<BeaconBookingModel> Bookings { get; set; }
    }
}