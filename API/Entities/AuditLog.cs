using API.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}