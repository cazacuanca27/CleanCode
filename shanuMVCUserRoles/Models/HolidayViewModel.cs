using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shanuMVCUserRoles.Models
{
    [Table("AspNetHoliday1")]
    public class HolidayViewModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string TeamLeaderName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, 21)]
        public int DaysOff { get; set; }

        [Required]
        [EmailAddress]
        public string TLEmail { get; set; }

        [Required]
        public bool Flag { get; set; }

        

    }
}