namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IBeaconDataLogic
    {
        /// <summary>
        /// Adds a beacon 
        /// </summary>
        /// <param name="beaconId">Beacon ID from the new beacon</param>
        /// <param name="friendlyName">The friendly name users will view when they manage beacons</param>
        /// <param name="location">Defines the location</param>
        void AddBeacon(Guid beaconId, string beaconName, string friendlyName, string location);

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns></returns>
        Beacon GetBeacon(Guid beaconId);

        /// <summary>
        /// Deletes a beacon with the given identifier from the system
        /// </summary>
        /// <param name="id">Beacon UUID known to the syste,</param>
        void DeleteBeacon(Guid uuid);

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        List<Beacon> GetBeacons();
    }
}