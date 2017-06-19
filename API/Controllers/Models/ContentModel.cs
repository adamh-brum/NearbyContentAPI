namespace API.Controllers.Models
{
    using System;

    public class ContentModel
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public DateTime RequestDateTime { get; set; }

        public string ContentShortDescription { get; set; }

        public string Content { get; set; }
    }
}