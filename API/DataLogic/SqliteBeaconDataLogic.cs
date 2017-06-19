namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Contains the SQLite implementation of the IBeaconDataLogic
    /// </summary>
    public class SqliteBeaconDataLogic : IBeaconDataLogic
    {
        /// <summary>
        /// Adds a beacon 
        /// </summary>
        /// <param name="beaconId">Beacon ID from the new beacon</param>
        /// <param name="friendlyName">The friendly name users will view when they manage beacons</param>
        /// <param name="location">Defines the location</param>
        public void AddBeacon(Guid beaconId, string beaconName, string friendlyName, string location)
        {
            var newObject = new Beacon()
            {
                Id = beaconId,
                Name = beaconName,
                FriendlyName = friendlyName,
                Location = location
            };

            using (var db = new ApplicationDbContext())
            {
                db.Beacons.Add(newObject);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a beacon by the given UUID
        /// </summary>
        /// <param name="beaconUUID">UUID</param>
        public void DeleteBeacon(Guid beaconUUID)
        {
            using (var db = new ApplicationDbContext())
            {
                var objectToDelete = db.Beacons.FirstOrDefault(beacon => beacon.Id == beaconUUID);

                if (objectToDelete != null)
                {
                    db.Beacons.Remove(objectToDelete);
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns>Beacon that matches the specified beacon ID.!-- If none found, none returned</returns>
        public Beacon GetBeacon(Guid beaconId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Beacons.FirstOrDefault(b => b.Id == beaconId);
            }
        }

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        public List<Beacon> GetBeacons()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Beacons.ToList();
            }
        }
    }
}