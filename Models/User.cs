using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorLesson.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "User name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Incorrect email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IList<UserRoles> UserRoles { get; set; }
    }
}
