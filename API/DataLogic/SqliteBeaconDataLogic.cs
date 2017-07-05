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
        /// Adds a beacon to SQLite
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="beaconId"></param>
        /// <param name="minorVersion"></param>
        /// <param name="majorVersion"></param>
        /// <param name="friendlyName"></param>
        /// <param name="location"></param>
        public IList<string> AddBeacon(string uuid, string beaconId, string minorVersion, string majorVersion, string friendlyName, string location)
        {
            var errors = this.ValidateBeaconParameters(uuid, beaconId, majorVersion, minorVersion);

            if (errors.Count > 0)
            {
                return errors;
            }

            var newObject = new Beacon()
            {
                UUID = uuid,
                BeaconId = beaconId,
                MinorVersion = minorVersion,
                MajorVersion = majorVersion,
                FriendlyName = friendlyName,
                Location = location
            };

            using (var db = new ApplicationDbContext())
            {
                db.Beacons.Add(newObject);
                db.SaveChanges();
            }

            return new List<string>();
        }
        /// <summary>
        /// Adds a beacon to SQLite
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uuid"></param>
        /// <param name="beaconId"></param>
        /// <param name="minorVersion"></param>
        /// <param name="majorVersion"></param>
        /// <param name="friendlyName"></param>
        /// <param name="location"></param>
        public IList<string> UpdateBeacon(int id, string uuid, string beaconId, string minorVersion, string majorVersion, string friendlyName, string location)
        {
            using (var db = new ApplicationDbContext())
            {
                var beacon = db.Beacons.FirstOrDefault(b => b.Id == id);
                if (beacon == null)
                {
                    return new List<string>() { $"The beacon ID {id} does not exist" };
                }

                var errors = this.ValidateBeaconParameters(uuid, beaconId, majorVersion, minorVersion);
                if (errors.Count > 0)
                {
                    return errors;
                }

                beacon.UUID = uuid;
                beacon.BeaconId = beaconId;
                beacon.MinorVersion = minorVersion;
                beacon.MajorVersion = majorVersion;
                db.SaveChanges();
            }

            return new List<string>();
        }

        /// <summary>
        /// Deletes a beacon by the given UUID
        /// </summary>
        /// <param name="id">The beacon ID</param>
        public void DeleteBeacon(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var objectToDelete = db.Beacons.FirstOrDefault(beacon => beacon.Id == id);

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
        /// <param name="uuid"></param>
        /// <returns>Beacon that matches the specified beacon ID.!-- If none found, none returned</returns>
        public Beacon GetBeacon(string uuid)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Beacons.FirstOrDefault(b => b.UUID == uuid);
            }
        }

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns>Beacon that matches the specified beacon ID.!-- If none found, none returned</returns>
        public Beacon GetBeacon(int beaconId)
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

        /// <summary>
        /// Validates the beacon model
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="beaconId"></param>
        /// <param name="minor"></param>
        /// <param name="major"></param>
        /// <returns>Any errors identified in the provided data</returns>
        private IList<string> ValidateBeaconParameters(string uuid, string beaconId, string minor, string major)
        {
            List<string> errors = new List<string>();

            using (var db = new ApplicationDbContext())
            {
                var beaconExists = db.Beacons.FirstOrDefault(b => b.UUID == uuid) != null;

                if (beaconExists)
                {
                    errors.Add("The UUID provided is already in use by another beacon");
                }

                beaconExists = db.Beacons.FirstOrDefault(b => b.BeaconId == beaconId) != null;

                if (beaconExists)
                {
                    errors.Add("The Beacon ID provided is already in use by another beacon");
                }

                int majorInt = 0;
                bool majorValid = int.TryParse(major, out majorInt);
                if (!majorValid || majorInt <= 0)
                {
                    errors.Add("The Major value must be a whole number greater than 0");
                }

                int minorInt = 0;
                bool minorValid = int.TryParse(major, out majorInt);
                if (!minorValid || minorInt <= 0)
                {
                    errors.Add("The Minor value must be a whole number greater than 0");
                }
            }

            return errors;
        }
    }
}