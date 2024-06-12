using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(2, ErrorMessage = "Gender can only be 2 characters long.")]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required] 
        public DateTime? DateOfBirth { get; set; } 

        [Required]
        [StringLength(10,MinimumLength = 4)]
        public string Password { get; set; }
        [MaxLength(100, ErrorMessage = "Full Name Maximum Length is 100 characters.")]
        public string FullName { get; set; }
    }
}