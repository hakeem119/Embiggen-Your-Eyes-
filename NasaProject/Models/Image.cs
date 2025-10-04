using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // One-to-One
        public ImageMetadata Metadata { get; set; }

        // One-to-Many
        public ICollection<Annotation> Annotations { get; set; }
        public ICollection<ProcessingJob> ProcessingJobs { get; set; }
    }
}
