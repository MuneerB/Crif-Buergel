using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrifAustria.Models
{
    public class SuccessRequestModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [Required]
        public string Industry { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string Company { get; set; }

        [RegularExpression(@"^\+?\d*$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true")]
        public bool Agreement { get; set; }


        public bool Newsletter { get; set; }

        public string ResourceName { get; set; }
        public string TyPageCase { get; set; }
        public string linkedinProfileUrl { get; set; }
    }
}