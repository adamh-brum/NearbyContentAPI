
namespace API.Controllers.Models
{
    using System;

    /// <summary>
    /// Contains the information required to book a beacon
    /// </summary>
    public class BeaconViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public string Location { get; set; }
    }
}