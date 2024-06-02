using API.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities.Project
{
    public class Attachment
    {
        [Key]
        public int AttachmentID { get; set; }

        [ForeignKey("Issue")]
        public int IssueID { get; set; }

        [Required]
        [StringLength(255)]
        public string Filename { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public int? Filesize { get; set; }

        [ForeignKey("User")]
        public int? UploadedByUserID { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public virtual Issue Issue { get; set; }
        public virtual User User { get; set; }
    }
}
