using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarSharing.Areas.Administration.Models
{
    public class EditUserModel
    {
        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Cognome")]
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public bool Active { get; set; }

    }
}