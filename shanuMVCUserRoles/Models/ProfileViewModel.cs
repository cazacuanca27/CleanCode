using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shanuMVCUserRoles.Models
{
    [Table("AspNetProfile1")]
    public class ProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Mark { get; set; }

        [Required]
        [StringLength(13)]
        public string CNP { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        [MaxLength(50)]
        public string Team { get; set; }

        [Required]
        [EmailAddress]
        public string TeamLeaderEmail { get; set; }
    }
}