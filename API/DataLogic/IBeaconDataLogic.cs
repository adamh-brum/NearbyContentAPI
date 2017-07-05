namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IBeaconDataLogic
    {
        /// <summary>
        /// Adds the beacon
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="beaconName"></param>
        /// <param name="minorVersion"></param>
        /// <param name="majorVersion"></param>
        /// <param name="friendlyName"></param>
        /// <param name="location"></param>
        IList<string> AddBeacon(string uuid, string beaconName, string minorVersion, string majorVersion, string friendlyName, string location);

        /// <summary>
        /// Adds the beacon
        /// </summary>
        /// <param name="id">Must exist</param>
        /// <param name="uuid"></param>
        /// <param name="beaconName"></param>
        /// <param name="minorVersion"></param>
        /// <param name="majorVersion"></param>
        /// <param name="friendlyName"></param>
        /// <param name="location"></param>
        IList<string> UpdateBeacon(int id, string uuid, string beaconName, string minorVersion, string majorVersion, string friendlyName, string location);

        /// <summary>
        /// Returns a beacon that has the given UUID
        /// </summary>
        /// <param name="uuid">The identifier of the current beacon</param>
        /// <returns>Beacon, if any have the given UUID</returns>
        Beacon GetBeacon(string uuid);

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="id">The identifier of the current beacon</param>
        /// <returns></returns>
        Beacon GetBeacon(int id);

        /// <summary>
        /// Deletes a beacon with the given identifier from the system
        /// </summary>
        /// <param name="id">Beacon ID known to the syste,</param>
        void DeleteBeacon(int id);

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        List<Beacon> GetBeacons();
    }
}