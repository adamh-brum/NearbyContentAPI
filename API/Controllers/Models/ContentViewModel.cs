
namespace API.Controllers.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the information required to book a beacon
    /// </summary>
    public class ContentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<string> Tags { get; set; }
    }
}