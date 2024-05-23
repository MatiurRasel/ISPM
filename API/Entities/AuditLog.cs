using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}