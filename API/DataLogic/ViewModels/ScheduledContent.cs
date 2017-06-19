namespace API.DataLogic.ViewModels
{
    using System;
    using System.Collections.Generic;
    using API.DataLogic.Models;

    public class ScheduledContent
    {
        /// <summary>
        /// Holds the friendly name for the beacon location
        /// </summary>
        /// <returns>Beacon Location</returns>
        public string Location { get; set; }

        /// <summary>
        /// Content associated with the beacon
        /// </summary>
        /// <returns>Beacon content</returns>
        public List<Content> Content { get; set; }
    }
}