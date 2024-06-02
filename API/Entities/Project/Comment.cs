using API.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities.Project
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("Issue")]
        public int IssueID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Issue Issue { get; set; }
        public virtual User User { get; set; }
    }
}
