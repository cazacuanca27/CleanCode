using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace shanuMVCUserRoles.Models
{
    [Table("AspNetOOHRequest1")]
    public class OOHRequestViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        [MaxLength(50)]
        public string TicketNUmber { get; set; }

        [Required]
        [EmailAddress]
        public string TeamLeaderEmail { get; set; }

        [Required]
        public bool Flag { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}