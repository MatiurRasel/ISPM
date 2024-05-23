using API.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Project
{
    public class Project
    {
        [Key]
        public long ProjectID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProjectKey { get; set; }

        [Required]
        [StringLength(500)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public long? LeadUserID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual User LeadUser { get; set; }
    }
}