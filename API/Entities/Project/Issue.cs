using API.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Project
{
    public class Issue
    {
        [Key]
        public int IssueID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(20)]
        public string IssueKey { get; set; }

        [Required]
        [StringLength(500)]
        public string Summary { get; set; }

        public string Description { get; set; }

        [StringLength(20)]
        public string Priority { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [ForeignKey("Assignee")]
        public int? AssigneeID { get; set; }

        [ForeignKey("Reporter")]
        public int? ReporterID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual Project Project { get; set; }
        public virtual User Assignee { get; set; }
        public virtual User Reporter { get; set; }
    }
}