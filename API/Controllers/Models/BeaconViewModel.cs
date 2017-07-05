
namespace API.Controllers.Models
{
    using System;

    /// <summary>
    /// Contains the information required to book a beacon
    /// </summary>
    public class BeaconViewModel
    {
        public int Id { get; set; }

        public string UUID { get; set; }

        public string BeaconId { get; set; }

        public string MinorVersion { get; set; }

        public string MajorVersion { get; set; }

        public string FriendlyName { get; set; }

        public string Location { get; set; }
    }
}