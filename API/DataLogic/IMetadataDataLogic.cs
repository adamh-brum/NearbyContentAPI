namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IMetadataDataLogic
    {
        /// <summary>
        /// Gets all metadata with the given key
        /// </summary>
        /// <param name="key">Key </param>
        /// <returns>Values that match the key</returns>
        IEnumerable<string> GetMetadata(string key);

        /// <summary>
        /// Adds the metadata 
        /// </summary>
        /// <param name="key">Metadata key</param>
        /// <param name="value">Value</param>
        void AddMetadata(string key, string value);

        /// <summary>
        /// Deletes metadata that matches the given parameters
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void DeleteMetadata(string key, string value);
    }
}