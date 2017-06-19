namespace API.Controllers.Models
{
    using System;
    using System.Collections.Generic;
    using API.DataLogic.Models;

    public class BeaconSchedule
    {
        public Guid BeaconId { get; set; }

        public string BeaconFriendlyName { get; set; }

        public string BeaconLocation { get; set; }

        public IList<Timeslot> Timeslots { get; set; }
    }
}