namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Contains the SQLite implementation of the IMetadataLogic
    /// </summary>
    public class SqliteMetadataDataLogic : IMetadataDataLogic
    {
        /// <summary>
        /// Gets all metadata values that match the specified key
        /// Does not return the key as the caller already knows this!
        /// </summary>
        /// <param name="key">Any metadata values with this key will be returned</param>
        /// <returns>All values matching the key</returns>
        public IEnumerable<string> GetMetadata(string key)
        {
            List<string> metadataValues = new List<string>();

            using (var db = new ApplicationDbContext())
            {
                metadataValues = db.Metadata.Where(m => m.Key == key)?.Select(m => m.Value).ToList();
            }

            return metadataValues;
        }

        /// <summary>
        /// Adds a new metadata row
        /// </summary>
        /// <param name="key">Identifies what the value is descriptively. Not required to be unique</param>
        /// <param name="value">Value for the given key</param>
        public void AddMetadata(string key, string value)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Metadata.Add(new Metadata()
                {
                    Key = key,
                    Value = value
                });

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes metadata that matches the passed parameter(s)
        /// </summary>
        /// <param name="key">Required, if present will delete any item with this key</param>
        /// <param name="value">Optional, if present deletion will only occur on items with the specified key and value</param>
        public void DeleteMetadata(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            using (var db = new ApplicationDbContext())
            {
                var criteria = db.Metadata.Where(metadata => metadata.Key == key);

                if(!string.IsNullOrEmpty(value))
                {
                    criteria = criteria.Where(metadata => metadata.Value == value);
                }

                db.Metadata.RemoveRange(criteria);
                db.SaveChanges();
            }
        }
    }
}