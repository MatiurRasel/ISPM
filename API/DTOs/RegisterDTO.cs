using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required] 
        public DateTime? DateOfBirth { get; set; } 

        [Required]
        [StringLength(10,MinimumLength = 4)]
        public string Password { get; set; }
    }
}