using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shanuMVCUserRoles.Models
{
    [Table("AspNetTimesheet1")]
    public class TimeSheetViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Mark { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(13)]
        public string CNP { get; set; }

        [Required]
        [EmailAddress]
        public string TeamLeaderEmail { get; set; }

        [Required]
        public bool Flag { get; set; }
    }
}