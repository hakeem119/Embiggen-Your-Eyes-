using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Models
{
    public class ProcessingJob
    {
        [Key]
        public int JobId { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string JobType { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        // Navigation
        public Image Image { get; set; }
        public User User { get; set; }
    }
}
