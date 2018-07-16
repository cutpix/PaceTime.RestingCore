using System.ComponentModel.DataAnnotations;

namespace PaceTime.API.Models
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
