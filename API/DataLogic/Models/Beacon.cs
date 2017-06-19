namespace API.DataLogic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Beacon
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FriendlyName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}

