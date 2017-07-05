namespace API.DataLogic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Beacon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UUID { get; set; }

        [Required]
        public string BeaconId { get; set; }

        public string MinorVersion { get; set; }

        public string MajorVersion { get; set; }

        public string FriendlyName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}

